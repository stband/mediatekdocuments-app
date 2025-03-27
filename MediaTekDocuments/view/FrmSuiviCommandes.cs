using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using MediaTekDocuments.helper;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace MediaTekDocuments.view
{
    public partial class FrmSuiviCommandes : Form
    {
        // BindingSource pour centraliser la liaison des données pour chaque onglet.
        private BindingSource bindingSourceCommandes = new BindingSource();
        private BindingSource bindingSourceLivres = new BindingSource();
        private BindingSource bindingSourceDVD = new BindingSource();
        
        private FrmSuiviCommandesController controller;


        /// <summary>
        /// Événement de chargement du formulaire.
        /// Initialise l'affichage en chargeant toutes les commandes.
        /// </summary>
        private void FrmSuiviCommandes_Load(object sender, EventArgs e)
        {
            ChargerToutesCommandes();
        }

        /// <summary>
        /// Constructeur du formulaire.
        /// Appelle InitializeComponent pour initialiser les composants graphiques.
        /// </summary>
        public FrmSuiviCommandes()
        {
            InitializeComponent();
        }


        ///////////////////////////////////////////////////
        //             Méthodes générales                //
        ///////////////////////////////////////////////////


        /// <summary>
        /// Gère le changement d'onglet.
        /// Selon l'index de l'onglet sélectionné, recharge les données correspondantes.
        /// </summary>
        private void tabCommandes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabCommandes.SelectedIndex)
            {
                case 0: // Onglet 1 : Toutes les commandes
                    ChargerToutesCommandes();
                    break;

                case 1: // Onglet 2 : Commandes Livres
                    ChargerLivres();
                    break;

                case 2: // Onglet 3 : Commandes DVD
                    ChargerDVD();
                    break;
            }
        }


        ///////////////////////////////////////////////////
        // Méthodes pour l'onglet 1 : commandes livres   //
        ///////////////////////////////////////////////////


        /// <summary>
        /// Gère l'affichage des commandes dans le DataGridView.
        /// Configure les titres des colonnes et le mode de tri.
        /// </summary>
        private void AfficherToutesCommandes(List<Commande> commandes)
        {
            bindingSourceCommandes.DataSource = new BindingList<Commande>(commandes);
            dgvToutesCommandes.DataSource = bindingSourceCommandes;

            dgvToutesCommandes.Columns["Id"].HeaderText = "N° Commande";
            dgvToutesCommandes.Columns["Titre"].HeaderText = "Titre";
            dgvToutesCommandes.Columns["DateCommande"].HeaderText = "Date";
            dgvToutesCommandes.Columns["Montant"].HeaderText = "Montant (€)";
            dgvToutesCommandes.Columns["NbExemplaires"].HeaderText = "Exemplaires";
            dgvToutesCommandes.Columns["Suivi"].HeaderText = "Statut";
        }

        /// <summary>
        /// Réinitialise la TextBox de recherche et recharge les données.
        /// </summary>
        private void btnClear1_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche1, "Rechercher");
            ChargerToutesCommandes();
        }

        /// <summary>
        /// Bouton permettant de modifier le statut d'une commande sélectionnée.
        /// Vérifie les règles métier et demande confirmation avant de procéder.
        /// Utilise un mapping pour convertir le statut en code numérique.
        /// </summary>
        private void btnModifierStatut_Click(object sender, EventArgs e)
        {
            if (dgvToutesCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande.", "Alerte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Commande commande = (Commande)dgvToutesCommandes.SelectedRows[0].DataBoundItem;
            string statutActuel = commande.Suivi.ToLower();
            string nouveauStatut = cbStatutCommande.SelectedItem.ToString().ToLower();

            if (statutActuel == nouveauStatut)
            {
                MessageBox.Show("Le statut sélectionné est déjà celui de la commande.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Règle : on ne peut pas revenir à un statut antérieur une fois la commande livrée ou réglée.
            if ((statutActuel == "livrée" || statutActuel == "réglée") &&
                (nouveauStatut == "en cours" || nouveauStatut == "relancée"))
            {
                MessageBox.Show("Impossible de revenir à un statut antérieur une fois la commande livrée ou réglée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Règle : une commande ne peut être réglée que si elle est d'abord livrée.
            if (nouveauStatut == "réglée" && statutActuel != "livrée")
            {
                MessageBox.Show("Une commande ne peut être réglée que si elle est d’abord livrée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Règle : une commande réglée ne peut plus être modifiée.
            if (statutActuel == "réglée")
            {
                MessageBox.Show("Une commande réglée ne peut plus être modifiée.", "Action interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mapping pour convertir le texte du statut en code numérique (important pour la BDD).
            Dictionary<string, string> statutMapping = new Dictionary<string, string>()
            {
                { "en cours", "00001" },
                { "relancée", "00004" },
                { "livrée", "00002" },
                { "réglée", "00003" }
            };

            if (!statutMapping.ContainsKey(nouveauStatut))
            {
                MessageBox.Show("Le statut sélectionné est invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nouveauStatutCode = statutMapping[nouveauStatut];

            DialogResult confirm = MessageBox.Show(
                $"Êtes-vous sûr de vouloir modifier le statut de la commande {commande.Id} de '{statutActuel}' vers '{nouveauStatut}' ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            bool success = controller.ModifierStatutCommande(commande.Id, nouveauStatutCode);

            if (success)
            {
                MessageBox.Show("Statut modifié avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerToutesCommandes();
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification du statut.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bouton permettant de supprimer une commande.
        /// Seule une commande en cours peut être supprimée.
        /// </summary>
        private void btnSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (dgvToutesCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande à supprimer.", "Alerte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvToutesCommandes.SelectedRows[0];
            Commande commande = (Commande)selectedRow.DataBoundItem;

            // Si la commande n'est pas "en cours" suppression interdite.
            if (commande.Suivi.ToLower() != "en cours")
            {
                MessageBox.Show("Impossible de supprimer une commande livrée ou réglée.", "Suppression refusée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer la commande {commande.Id} ?",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            bool success = controller.SupprimerCommande(commande.Id);

            if (success)
            {
                MessageBox.Show("Commande supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerToutesCommandes();
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppression de la commande.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Charge et affiche toutes les commandes.
        /// Initialise le contrôleur et met à jour l'interface avec le nombre de commandes.
        /// </summary>
        private void ChargerToutesCommandes()
        {
            controller = new FrmSuiviCommandesController();
            List<Commande> commandes = controller.GetToutesCommandes();
            AfficherToutesCommandes(commandes);
            lblResultat.Text = $"{commandes.Count} commande(s) affichée(s)";
        }

        /// <summary>
        /// Lorsqu'une cellule du DataGridView est cliquée,
        /// met à jour le ComboBox du statut et affiche l'ID de la commande sélectionnée.
        /// </summary>
        private void dgvToutesCommandes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow ligne = dgvToutesCommandes.Rows[e.RowIndex];
                Commande commande = (Commande)ligne.DataBoundItem;
                cbStatutCommande.SelectedItem = commande.Suivi;
                lblIDCommandeSelectionne.Text = commande.Id;
            }
        }

        // Variables pour gérer le tri des commandes.
        private string currentSortColumn = "";
        private ListSortDirection currentSortDirection = ListSortDirection.Ascending;

        /// <summary>
        /// Permet de trier dynamiquement les commandes en cliquant sur l'en-tête des colonnes.
        /// Le tri alterne entre croissant et décroissant.
        /// </summary>
        private void dgvToutesCommandes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgvToutesCommandes.Columns[e.ColumnIndex].Name;

            // Si la même colonne est cliquée, on inverse la direction ; sinon, on passe en Ascendant.
            if (currentSortColumn == columnName)
            {
                currentSortDirection = currentSortDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                currentSortColumn = columnName;
                currentSortDirection = ListSortDirection.Ascending;
            }

            // Récupère la liste actuelle des commandes depuis le BindingSource.
            List<Commande> commandes;
            if (bindingSourceCommandes.DataSource is BindingList<Commande> bindingList)
            {
                commandes = bindingList.ToList();
            }
            else if (bindingSourceCommandes.DataSource is List<Commande> list)
            {
                commandes = list;
            }
            else
            {
                return;
            }

            // Trie la liste à l'aide de LINQ et reflection.
            if (currentSortDirection == ListSortDirection.Ascending)
            {
                commandes = commandes.OrderBy(c => GetPropertyValue(c, columnName)).ToList();
            }
            else
            {
                commandes = commandes.OrderByDescending(c => GetPropertyValue(c, columnName)).ToList();
            }

            // Met à jour le BindingSource avec la liste triée.
            bindingSourceCommandes.DataSource = new BindingList<Commande>(commandes);
            dgvToutesCommandes.Refresh();

            // Met à jour les glyphes de tri sur les en-têtes.
            foreach (DataGridViewColumn col in dgvToutesCommandes.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            dgvToutesCommandes.Columns[columnName].HeaderCell.SortGlyphDirection = currentSortDirection == ListSortDirection.Ascending
                ? SortOrder.Ascending
                : SortOrder.Descending;
        }

        /// <summary>
        /// Méthode utilitaire qui utilise la réflexion pour récupérer la valeur d'une propriété d'un objet.
        /// Utile pour le tri dynamique.
        /// </summary>
        private object GetPropertyValue(object obj, string propertyName)
        {
            PropertyInfo propInfo = obj.GetType().GetProperty(propertyName);
            return propInfo != null ? propInfo.GetValue(obj, null) : null;
        }

        /// <summary>
        /// Efface le placeholder et modifie la couleur de la police.
        /// </summary>
        private void txtRecherche1_Enter(object sender, EventArgs e)
        {
            UIHelper.GererEntreeTextBox(txtRecherche1, "Rechercher");
        }

        /// <summary>
        /// Réaffiche le placeholder si le champ est vide.
        /// </summary>
        private void txtRecherche1_Leave(object sender, EventArgs e)
        {
            UIHelper.GererSortieTextBox(txtRecherche1, "Rechercher");
        }

        /// <summary>
        /// Gestion de la recherche dynamique dans la TextBox.
        /// Au fur et à mesure de la saisie, le DataGridView est filtré et le nombre de résultats est mis à jour.
        /// </summary>
        private void txtRecherche1_TextChanged(object sender, EventArgs e)
        {
            if (txtRecherche1.Text != "Rechercher")
            {
                txtRecherche1.ForeColor = Color.Black;

                // Appel au filtre via le contrôleur
                List<Commande> resultat = controller.FiltrerCommandes(txtRecherche1.Text);
                bindingSourceCommandes.DataSource = resultat;
                dgvToutesCommandes.Refresh();

                // Mise à jour du label avec le nombre de résultats
                lblResultat.Text = $"{resultat.Count} commande(s) affichée(s)";
            }
        }


        ///////////////////////////////////////////////////
        // Méthodes pour l'onglet 2 : commandes livres   //
        ///////////////////////////////////////////////////


        /// <summary>
        /// Réinitialise la TextBox de recherche et recharge les données.
        /// </summary>
        private void btnClear2_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche2, "Rechercher");
            ChargerLivres();
        }

        /// <summary>
        /// Bouton "Commander" de l'onglet Livres.
        /// Vérifie la sélection et les données saisies, puis crée une nouvelle commande.
        /// </summary>
        private void btnCommander1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblID.Text) || lblID.Text == "00000")
            {
                MessageBox.Show("Veuillez sélectionner un livre à commander.", "Alerte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtMontant.Text, out double montant) || montant <= 0)
            {
                MessageBox.Show("Veuillez entrer un montant valide.", "Montant invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nbExemplaires = (int)nudNbExemplaires.Value;

            // Confirmation utilisateur.
            DialogResult confirmation = MessageBox.Show(
                $"Confirmer la commande de {nbExemplaires} exemplaire(s) pour un montant total de {montant} € ?",
                "Confirmation de commande",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            // Création de la commande.
            Commande nouvelleCommande = new Commande(
                id: "", // L’API génère l’id.
                titre: "", // Le titre n’est pas requis pour l’envoi.
                idLivreDvd: lblID.Text,
                dateCommande: DateTime.Now,
                montant: montant,
                nbExemplaires: nbExemplaires,
                suivi: "en cours"
            );

            bool success = controller.CreerCommandeLivre(nouvelleCommande);

            if (success)
            {
                MessageBox.Show("Commande effectuée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontant.Text = "";
                nudNbExemplaires.Value = 1;
                lblID.Text = "00000";
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Charge les livres dans le DataGridView de l'onglet Livres.
        /// Configure également l'ordre et la visibilité de certaines colonnes.
        /// </summary>
        private void ChargerLivres()
        {
            List<Livre> livres = controller.GetAllLivres();
            bindingSourceLivres.DataSource = new BindingList<Livre>(livres);
            dgvCommandesLivre.DataSource = bindingSourceLivres;

            dgvCommandesLivre.Columns["Id"].HeaderText = "ID";
            dgvCommandesLivre.Columns["Titre"].HeaderText = "Titre";
            dgvCommandesLivre.Columns["Auteur"].HeaderText = "Auteur";
            dgvCommandesLivre.Columns["Isbn"].HeaderText = "ISBN";
            dgvCommandesLivre.Columns["Genre"].HeaderText = "Genre";
            dgvCommandesLivre.Columns["Public"].HeaderText = "Public";
            dgvCommandesLivre.Columns["Rayon"].HeaderText = "Rayon";

            dgvCommandesLivre.Columns["Id"].DisplayIndex = 0;
            dgvCommandesLivre.Columns["Titre"].DisplayIndex = 1;

            dgvCommandesLivre.Columns["IdGenre"].Visible = false;
            dgvCommandesLivre.Columns["IdPublic"].Visible = false;
            dgvCommandesLivre.Columns["IdRayon"].Visible = false;
            dgvCommandesLivre.Columns["Image"].Visible = false;
        }

        /// <summary>
        /// Lorsqu'une ligne du DataGridView des livres est cliquée,
        /// récupère l'ID du livre et l'affiche dans le label dédié.
        /// </summary>
        private void dgvCommandesLivre_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow ligne = dgvCommandesLivre.Rows[e.RowIndex];
            string idLivre = ligne.Cells["Id"].Value.ToString();
            lblID.Text = idLivre;
        }

        // Variables pour gérer le tri des livres.
        private string currentSortColumnLivres = "";
        private ListSortDirection currentSortDirectionLivres = ListSortDirection.Ascending;

        /// <summary>
        /// Permet le tri dynamique des livres dans le DataGridView en cliquant sur l'en-tête.
        /// </summary>
        private void dgvCommandesLivre_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgvCommandesLivre.Columns[e.ColumnIndex].Name;

            if (currentSortColumnLivres == columnName)
            {
                currentSortDirectionLivres = currentSortDirectionLivres == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                currentSortColumnLivres = columnName;
                currentSortDirectionLivres = ListSortDirection.Ascending;
            }

            List<Livre> livres;
            if (bindingSourceLivres.DataSource is BindingList<Livre> bindingList)
            {
                livres = bindingList.ToList();
            }
            else if (bindingSourceLivres.DataSource is List<Livre> list)
            {
                livres = list;
            }
            else
            {
                return;
            }

            if (currentSortDirectionLivres == ListSortDirection.Ascending)
            {
                livres = livres.OrderBy(l => GetPropertyValue(l, columnName)).ToList();
            }
            else
            {
                livres = livres.OrderByDescending(l => GetPropertyValue(l, columnName)).ToList();
            }

            bindingSourceLivres.DataSource = new BindingList<Livre>(livres);
            dgvCommandesLivre.Refresh();

            foreach (DataGridViewColumn col in dgvCommandesLivre.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            dgvCommandesLivre.Columns[columnName].HeaderCell.SortGlyphDirection = currentSortDirectionLivres == ListSortDirection.Ascending
                ? SortOrder.Ascending
                : SortOrder.Descending;
        }

        /// <summary>
        /// Efface le placeholder et modifie la couleur de la police.
        /// </summary>
        private void txtRecherche2_Enter(object sender, EventArgs e)
        {
            UIHelper.GererEntreeTextBox(txtRecherche2, "Rechercher");
        }

        /// <summary>
        /// Réaffiche le placeholder si le champ est vide.
        /// </summary>
        private void txtRecherche2_Leave(object sender, EventArgs e)
        {
            UIHelper.GererSortieTextBox(txtRecherche2, "Rechercher");
        }

        /// <summary>
        /// Gestion de la recherche dynamique dans la TextBox de l'onglet Livres.
        /// Filtre la liste des livres via ID.
        /// </summary>
        private void txtRecherche2_TextChanged(object sender, EventArgs e)
        {
            if (txtRecherche2.Text != "Rechercher")
            {
                txtRecherche2.ForeColor = Color.Black;
                string motCle = txtRecherche2.Text.ToLower();

                List<Livre> livres = controller.GetAllLivres();
                var resultats = livres.Where(l => l.Id.ToLower().Contains(motCle)).ToList();

                bindingSourceLivres.DataSource = new BindingList<Livre>(resultats);
                dgvCommandesLivre.Refresh();
            }
        }


        ///////////////////////////////////////////////////
        // Méthodes pour l'onglet 3 : commandes DVD      //
        ///////////////////////////////////////////////////


        /// <summary>
        /// Réinitialise la TextBox de recherche et recharge les données.
        /// </summary>
        private void btnClear3_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche3, "Rechercher");
            ChargerDVD();
        }

        /// <summary>
        /// Bouton "Commander" de l'onglet DVD.
        /// Valide la sélection et les informations saisies, puis crée une nouvelle commande DVD.
        /// </summary>
        private void btnCommanderDVD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblID_DVD.Text) || lblID_DVD.Text == "00000")
            {
                MessageBox.Show("Veuillez sélectionner un DVD à commander.", "Alerte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtMontantDVD.Text, out double montant) || montant <= 0)
            {
                MessageBox.Show("Veuillez entrer un montant valide.", "Montant invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nbExemplaires = (int)nudNbExemplairesDVD.Value;

            DialogResult confirmation = MessageBox.Show(
                $"Confirmer la commande de {nbExemplaires} exemplaire(s) pour un montant total de {montant} € ?",
                "Confirmation de commande",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
                return;

            Commande nouvelleCommande = new Commande(
                id: "",
                titre: "",
                idLivreDvd: lblID_DVD.Text,
                dateCommande: DateTime.Now,
                montant: montant,
                nbExemplaires: nbExemplaires,
                suivi: "en cours"
            );

            bool success = controller.CreerNouvelleCommande(nouvelleCommande);

            if (success)
            {
                MessageBox.Show("Commande de DVD effectuée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontantDVD.Text = "";
                nudNbExemplairesDVD.Value = 1;
                lblID_DVD.Text = "00000";
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Charge les DVD dans le DataGridView de l'onglet DVD.
        /// Configure l'affichage des colonnes et masque celles inutiles.
        /// </summary>
        private void ChargerDVD()
        {
            List<Dvd> dvds = controller.GetAllDvd();
            bindingSourceDVD.DataSource = new BindingList<Dvd>(dvds);
            dgvCommandesDVD.DataSource = bindingSourceDVD;

            dgvCommandesDVD.Columns["Id"].HeaderText = "ID";
            dgvCommandesDVD.Columns["Titre"].HeaderText = "Titre";
            dgvCommandesDVD.Columns["Realisateur"].HeaderText = "Réalisateur";
            dgvCommandesDVD.Columns["Duree"].HeaderText = "Durée (min)";
            dgvCommandesDVD.Columns["Genre"].HeaderText = "Genre";
            dgvCommandesDVD.Columns["Public"].HeaderText = "Public";
            dgvCommandesDVD.Columns["Rayon"].HeaderText = "Rayon";

            dgvCommandesDVD.Columns["Id"].DisplayIndex = 0;
            dgvCommandesDVD.Columns["Titre"].DisplayIndex = 1;

            dgvCommandesDVD.Columns["IdGenre"].Visible = false;
            dgvCommandesDVD.Columns["IdPublic"].Visible = false;
            dgvCommandesDVD.Columns["IdRayon"].Visible = false;
            dgvCommandesDVD.Columns["Image"].Visible = false;
            dgvCommandesDVD.Columns["Synopsis"].Visible = false;
        }

        /// <summary>
        /// Lorsqu'une ligne du DataGridView des DVD est cliquée,
        /// récupère l'ID du DVD et l'affiche dans le label dédié.
        /// </summary>
        private void dgvCommandesDVD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow ligne = dgvCommandesDVD.Rows[e.RowIndex];
            string idDVD = ligne.Cells["Id"].Value.ToString();
            lblID_DVD.Text = idDVD;
        }

        // Variables pour gérer le tri des DVD
        private string currentSortColumnDVD = "";
        private ListSortDirection currentSortDirectionDVD = ListSortDirection.Ascending;

        /// <summary>
        /// Permet le tri dynamique des DVD dans le DataGridView en cliquant sur l'en-tête.
        /// Alterne entre tri croissant et décroissant.
        /// </summary>
        private void dgvCommandesDVD_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgvCommandesDVD.Columns[e.ColumnIndex].Name;

            if (currentSortColumnDVD == columnName)
            {
                currentSortDirectionDVD = currentSortDirectionDVD == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                currentSortColumnDVD = columnName;
                currentSortDirectionDVD = ListSortDirection.Ascending;
            }

            List<Dvd> dvds = (bindingSourceDVD.DataSource as BindingList<Dvd>).ToList();

            if (currentSortDirectionDVD == ListSortDirection.Ascending)
            {
                dvds = dvds.OrderBy(d => GetPropertyValue(d, columnName)).ToList();
            }
            else
            {
                dvds = dvds.OrderByDescending(d => GetPropertyValue(d, columnName)).ToList();
            }

            bindingSourceDVD.DataSource = new BindingList<Dvd>(dvds);
            dgvCommandesDVD.Refresh();
        }

        /// <summary>
        /// Efface le placeholder et modifie la couleur de la police.
        /// </summary>
        private void txtRecherche3_Enter(object sender, EventArgs e)
        {
            UIHelper.GererEntreeTextBox(txtRecherche3, "Rechercher");
        }

        /// <summary>
        /// Réaffiche le placeholder si le champ est vide.
        /// </summary>
        private void txtRecherche3_Leave(object sender, EventArgs e)
        {
            UIHelper.GererSortieTextBox(txtRecherche3, "Rechercher");
        }

        /// <summary>
        /// Gestion de la recherche dynamique dans la TextBox de l'onglet DVD.
        /// Filtre la liste des DVD en fonction de son ID.
        /// </summary>
        private void txtRecherche3_TextChanged(object sender, EventArgs e)
        {
            if (txtRecherche3.Text != "Rechercher")
            {
                txtRecherche3.ForeColor = Color.Black;
                string motCle = txtRecherche3.Text.ToLower();

                List<Dvd> dvds = controller.GetAllDvd();
                var resultats = dvds.Where(d => d.Id.ToLower().Contains(motCle)).ToList();

                bindingSourceDVD.DataSource = new BindingList<Dvd>(resultats);
                dgvCommandesDVD.Refresh();
            }
        }
    }
}