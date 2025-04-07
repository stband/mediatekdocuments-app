using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using MediaTekDocuments.helper;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Fenêtre de suivi et création des commandes (toutes, livres, DVD).
    /// </summary>
    public partial class FrmSuiviCommandes : Form
    {

        #region constantes de la classe

        private const string PlaceholderText = "Rechercher";
        private const string MessageErreur = "Erreur";
        private const string MessageAlerte = "Alerte";
        private const string MessageInformation = "Information";
        private const string MessageSucces = "Succès";
        private const string IdBase = "00000";
        
        private const string StatutEnCours = "en cours";
        private const string StatutReglee = "réglée";
        private const string StatutLivree = "livrée";
        private const string StatutRelancee = "relancée";

        private const string ColRayon = "Rayon";
        private const string ColGenre = "Genre";
        private const string ColPublic = "Public";
        private const string ColTitre = "Titre";
        private const string ColSuivi = "Suivi";

        #endregion

        #region bindingsource et données locales

        // BindingSource pour centraliser la liaison des données pour chaque onglet.
        private readonly BindingSource bindingSourceCommandes = [];
        private readonly BindingSource bindingSourceLivres = [];
        private readonly BindingSource bindingSourceDVD = [];

        // Copie en mémoire pour reset les listes originales après un trie ou un filtre.
        private List<Commande> commandesOriginaux = [];
        private List<Livre> livresOriginaux = [];
        private List<Dvd> dvdOriginaux = [];

        // Contrôleur du formulaire unique
        private readonly FrmSuiviCommandesController controller = new();

        #endregion

        /// <summary>
        /// Constructeur du formulaire.
        /// Appelle InitializeComponent pour initialiser les composants graphiques.
        /// </summary>
        public FrmSuiviCommandes() => InitializeComponent();


        /// <summary>
        /// Gestionnaire de l'événement Load du formulaire.
        /// Charge les commandes lors de l'initialisation.
        /// </summary>
        private void FrmSuiviCommandes_Load(object sender, EventArgs e) => ChargerToutesCommandes();


        ///////////////////////////////////////////////////
        //             Méthodes générales                //
        ///////////////////////////////////////////////////

        #region Gestion des onglets

        /// <summary>
        /// Gère le changement d’onglet :  
        /// <list type="bullet">
        ///   <item><description>Onglet 1 : charge toutes les commandes.</description></item>
        ///   <item><description>Onglet 2 : charge les livres.</description></item>
        ///   <item><description>Onglet 3 : charge les DVD.</description></item>
        /// </list>
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

        #endregion

        /////////////////////////////////////////////////////
        // Méthodes pour l'onglet 1 : toutes les commandes //
        /////////////////////////////////////////////////////

        #region Onglet 1 — Toutes les commandes

        /// <summary>
        /// Réinitialise la textBox de recherche et recharge la liste complète des commandes.
        /// </summary>.
        private void btnClear1_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche1, PlaceholderText);
            ChargerToutesCommandes();
        }

        /// <summary>
        /// Modifie le statut de la commande sélectionnée.
        /// Vérifie les règles métier et demande confirmation avant de procéder.
        /// Utilise un mapping pour convertir le texte du statut en code numérique,
        /// pour assurer la compatibilité avec la BDD.
        /// </summary>
        private void btnModifierStatut_Click(object sender, EventArgs e)
        {
            if (dgvToutesCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande.", MessageAlerte, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Commande commande = (Commande)dgvToutesCommandes.SelectedRows[0].DataBoundItem;
            
            string statutActuel = commande.Suivi.ToLower();
            string nouveauStatut = cbStatutCommande.Text.ToLower();

            if (statutActuel == nouveauStatut)
            {
                MessageBox.Show("Le statut sélectionné est déjà celui de la commande.", MessageInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Règle : une commande réglée ne peut plus être modifiée.
            if (statutActuel == StatutReglee)
            {
                MessageBox.Show("Une commande réglée ne peut plus être modifiée.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Règle : une commande livrée peut seulement être payée.
            if (statutActuel == StatutLivree && nouveauStatut != StatutReglee)
            {
                MessageBox.Show("Une commande livrée ne peut plus retourner à un statut antérieur.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Règle : une commande ne peut être réglée que si elle est d'abord livrée.
            if ((statutActuel == StatutEnCours || statutActuel == StatutRelancee) && nouveauStatut == StatutReglee)
            {
                MessageBox.Show("Une commande ne peut pas être réglée sans être préalablement livrée.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mapping pour convertir le texte du statut en code numérique (important pour la BDD).
            Dictionary<string, string> statutMapping = new()
            {
                { StatutEnCours, "00001" },
                { StatutRelancee, "00004" },
                { StatutLivree, "00002" },
                { StatutReglee, "00003" }
            };

            if (!statutMapping.TryGetValue(nouveauStatut, out string? value))
            {
                MessageBox.Show("Le statut sélectionné est invalide.", MessageAlerte, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // L'équivalent du statut en code numérique (ex : "en cours" == 00001).
            string statutCodeNumerique = value;

            DialogResult confirm = MessageBox.Show(
                $"Êtes-vous sûr de vouloir modifier le statut de la commande {commande.Id} de '{statutActuel}' vers '{nouveauStatut}' ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            bool success = controller.ModifierStatutCommande(commande.Id, statutCodeNumerique);

            if (success)
            {
                MessageBox.Show("Statut modifié avec succès.", MessageSucces, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerToutesCommandes();
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification du statut.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Supprime la commande sélectionnée, sauf si son statut est "livrée" ou "réglée".
        /// Affiche une confirmation et gère les erreurs.
        /// </summary>
        private void btnSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (dgvToutesCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande à supprimer.", MessageAlerte, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifie si la commande à un statut pouvant être supprimé.
            var commande = (Commande)dgvToutesCommandes.SelectedRows[0].DataBoundItem;
            string statutActuel = commande.Suivi.ToLower();

            if (statutActuel == StatutLivree || statutActuel == StatutReglee)
            {
                MessageBox.Show("Impossible de supprimer une commande livrée ou réglée.", "Suppression refusée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer la commande n°{commande.Id} ?",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            bool success = controller.SupprimerCommande(commande.Id);

            if (success)
            {
                MessageBox.Show("Commande supprimée avec succès.", MessageSucces, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerToutesCommandes();
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppression de la commande.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Charge la liste complète des commandes (liées aux livres et DVD) et configure le DataGridView pour affichage, tri et mise en forme.
        /// Configure le ComboBox cbRevue.
        /// </summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Récupère les données via le contrôleur <see cref="FrmSuiviCommandesController"/>.</description>
        ///         </item>
        ///         <item>
        ///             <description>Utilise <see cref="SortableBindingList{T}"/> pour activer le tri sur chaque colonne.</description>
        ///         </item>
        ///         <item>
        ///             <description>Définit l’ordre et le texte des en-têtes de colonnes.</description>
        ///         </item>
        ///         <item>
        ///             <description>Masque les colonnes techniques (IDs et images) non pertinentes pour l’utilisateur.</description>
        ///         </item>
        ///         <item>
        ///             <description>Appelle <see cref="AfficherNombreCommandes"/> qui affiche le nombre de commande du DataGridView.</description>
        ///         </item>  
        ///     </list>
        /// </remarks>
        private void ChargerToutesCommandes()
        {
            // Récupère la liste des commandes via le contrôleur.
            commandesOriginaux = controller.GetToutesCommandes();

            // Rendre triable le BindingSourcee au travers de la classe helper SortableBindingList.
            var sortableCommandes = new SortableBindingList<Commande>(commandesOriginaux);
            bindingSourceCommandes.DataSource = sortableCommandes;
            dgvToutesCommandes.DataSource = bindingSourceCommandes;

            // Assurer le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvToutesCommandes.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvToutesCommandes.Columns["Id"].HeaderText = "N° commande";
            dgvToutesCommandes.Columns[ColTitre].HeaderText = ColTitre;
            dgvToutesCommandes.Columns["DateCommande"].HeaderText = "Date";
            dgvToutesCommandes.Columns["Montant"].HeaderText = "Montant (€)";
            dgvToutesCommandes.Columns["NbExemplaires"].HeaderText = "Exemplaires";
            dgvToutesCommandes.Columns[ColSuivi].HeaderText = "Statut";

            dgvToutesCommandes.Columns["Id"].DisplayIndex = 0;
            dgvToutesCommandes.Columns[ColSuivi].DisplayIndex = 1;

            AfficherNombreCommandes();
        }

        /// <summary>
        /// Applique une couleur de fond aux lignes du DataGridView selon le statut de la commande.
        /// Utilisé pour améliorer la lisibilité et signaler visuellement l'état d'avancement d'une commande.
        /// </summary>
        private void dgvToutesCommandes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dgvToutesCommandes.Rows[e.RowIndex];
            var statut = row.Cells[ColSuivi].Value as string;

            // 1) Choix de la couleur de base
            Color baseColor = statut switch
            {
                StatutEnCours => Color.Gold,
                StatutRelancee => Color.Goldenrod,
                StatutLivree => Color.SkyBlue,
                StatutReglee => Color.LightGreen,
                _ => Color.White
            };

            row.DefaultCellStyle.BackColor = baseColor;
        }

        /// <summary>
        /// Met à jour le label du nombre de commandes affichées.
        /// </summary>
        private void AfficherNombreCommandes() => lblResultat.Text = $"{bindingSourceCommandes.Count} commande(s) affichée(s)";

        /// <summary>
        /// Lorsqu’une commande (ligne) du <see cref="dgvToutesCommandes"/> est cliquée :  
        /// <list type="bullet">
        ///   <item><description>Récupère l’objet <see cref="Commande"/> associé à la ligne sélectionnée.</description></item>
        ///   <item><description>Synchronise le <see cref="ComboBox"/> <c>cbStatutCommande</c> pour refléter le statut (<c>Suivi</c>) de la commande.</description></item>
        ///   <item><description>Met à jour le <c>Label</c> <c>lblIDCommandeSelectionne</c> pour afficher l’ID de la commande.</description></item>
        /// </list>
        /// </summary>
        private void dgvToutesCommandes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow ligne = dgvToutesCommandes.Rows[e.RowIndex];
                Commande commande = (Commande)ligne.DataBoundItem;

                // Synchronise le ComboBox et le Label avec la commande sélectionnée.
                cbStatutCommande.SelectedItem = commande.Suivi;
                lblIDCommandeSelectionne.Text = commande.Id;
            }
        }

        /// <summary>
        /// Efface le placeholder "Rechercher" et modifie la couleur de la police pour permettre la saisie.
        /// Utilise <see cref="UIHelper.GererEntreeTextBox"/>.
        /// </summary>
        private void txtRecherche1_Enter(object sender, EventArgs e) => UIHelper.GererEntreeTextBox(txtRecherche1, PlaceholderText);

        /// <summary>
        /// Restaure le placeholder "Rechercher" si le textBox est vide et ajuste la couleur.
        /// Utilise <see cref="UIHelper.GererSortieTextBox"/>.
        /// </summary>
        private void txtRecherche1_Leave(object sender, EventArgs e) => UIHelper.GererSortieTextBox(txtRecherche1, PlaceholderText);

        /// <summary>
        /// Filtre dynamiquement la liste des commandes affichées dans le DataGridView
        /// en fonction du texte saisi.
        /// Si le champ de recherche est vide ou contient le placeholder, le filtre est réinitialisé.
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Le filtrage est réalisé par la méthode helper : <see cref="UIHelper.FiltrerBindingSource"/>.</description></item>
        ///     </list>
        /// </remarks>
        /// </summary>
        private void txtRecherche1_TextChanged(object sender, EventArgs e)
        {
            string texte = txtRecherche1.Text.Trim();

            if (string.IsNullOrEmpty(texte) || texte == PlaceholderText)
            {
                // Remet la liste complète si champ vide.
                bindingSourceCommandes.DataSource = new SortableBindingList<Commande>(commandesOriginaux);
            }
            else
            {
                UIHelper.FiltrerBindingSource(bindingSourceCommandes, txtRecherche1, commandesOriginaux, "Id", ColTitre, ColSuivi);
                AfficherNombreCommandes();
            }
        }

        #endregion


        ///////////////////////////////////////////////////
        // Méthodes pour l'onglet 2 : commandes livres   //
        ///////////////////////////////////////////////////

        #region Onglet 2 - Commandes de livre

        /// <summary>
        /// Réinitialise la textBox de recherche et recharge la liste complète des livres.
        /// </summary>.
        private void btnClear2_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche2, PlaceholderText);
            ChargerLivres();
        }

        /// <summary>
        /// Bouton "Commander" de l'onglet Livres.
        /// Vérifie la sélection et les données saisies, puis créer une nouvelle commande.
        /// </summary>
        private void btnCommander1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblID.Text) || lblID.Text == IdBase)
            {
                MessageBox.Show("Veuillez sélectionner un livre à commander.", MessageAlerte, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                suivi: StatutEnCours
            );

            bool success = controller.CreerCommandeLivre(nouvelleCommande);

            if (success)
            {
                MessageBox.Show("Commande effectuée avec succès.", MessageSucces, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontant.Text = "";
                nudNbExemplaires.Value = 1;
                lblID.Text = IdBase;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Charge la liste complète des livres et configure le DataGridView pour affichage, tri et mise en forme.
        /// Configure le ComboBox cbRevue.
        /// </summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Récupère les données via le contrôleur <see cref="FrmSuiviCommandesController"/>.</description>
        ///         </item>
        ///         <item>
        ///             <description>Utilise <see cref="SortableBindingList{T}"/> pour activer le tri sur chaque colonne.</description>
        ///         </item>
        ///         <item>
        ///             <description>Définit l’ordre et le texte des en-têtes de colonnes.</description>
        ///         </item>
        ///         <item>
        ///             <description>Masque les colonnes techniques (IDs et images) non pertinentes pour l’utilisateur.</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        private void ChargerLivres()
        {
            // Récupère la liste des livres via le contrôleur.
            livresOriginaux = controller.GetAllLivres();

            // Rendre triable le BindingSource au travers de la classe helper SortableBindingList.
            var sortableLivres = new SortableBindingList<Livre>(livresOriginaux);
            bindingSourceLivres.DataSource = sortableLivres;
            dgvCommandesLivre.DataSource = bindingSourceLivres;

            // Assurer le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvCommandesLivre.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvCommandesLivre.Columns["Id"].HeaderText = "ID";
            dgvCommandesLivre.Columns[ColTitre].HeaderText = ColTitre;
            dgvCommandesLivre.Columns["Auteur"].HeaderText = "Auteur";
            dgvCommandesLivre.Columns["Isbn"].HeaderText = "ISBN";
            dgvCommandesLivre.Columns[ColGenre].HeaderText = ColGenre;
            dgvCommandesLivre.Columns[ColPublic].HeaderText = ColPublic;
            dgvCommandesLivre.Columns[ColRayon].HeaderText = ColRayon;

            dgvCommandesLivre.Columns["Id"].DisplayIndex = 0;
            dgvCommandesLivre.Columns[ColTitre].DisplayIndex = 1;

            dgvCommandesLivre.Columns["IdGenre"].Visible = false;
            dgvCommandesLivre.Columns["IdPublic"].Visible = false;
            dgvCommandesLivre.Columns["IdRayon"].Visible = false;
            dgvCommandesLivre.Columns["Image"].Visible = false;
        }

        /// <summary>
        /// Lorsqu’un livre est cliqué dans le <see cref="dgvCommandesLivre"/> :  
        /// <list type="bullet">
        ///   <item><description>Récupère l’ID du livre dans la cellule <c>Id</c> de la ligne sélectionnée.</description></item>
        ///   <item><description>Met à jour le <c>Label</c> <c>lblID</c> pour afficher cet ID.</description></item>
        /// </list>
        /// </summary>
        private void dgvCommandesLivre_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow ligne = dgvCommandesLivre.Rows[e.RowIndex];
            var idLivre = ligne.Cells["Id"].Value.ToString();
            lblID.Text = idLivre;
        }

        /// <summary>
        /// Efface le placeholder "Rechercher" et modifie la couleur de la police pour permettre la saisie.
        /// Utilise <see cref="UIHelper.GererEntreeTextBox"/>.
        /// </summary>
        private void txtRecherche2_Enter(object sender, EventArgs e) => UIHelper.GererEntreeTextBox(txtRecherche2, PlaceholderText);

        /// <summary>
        /// Restaure le placeholder "Rechercher" si le textBox est vide et ajuste la couleur.
        /// Utilise <see cref="UIHelper.GererSortieTextBox"/>.
        /// </summary>
        private void txtRecherche2_Leave(object sender, EventArgs e) => UIHelper.GererSortieTextBox(txtRecherche2, PlaceholderText);

        /// <summary>
        /// Filtre dynamiquement la liste des livres affichés dans le DataGridView
        /// en fonction du texte saisi.
        /// Si le champ de recherche est vide ou contient le placeholder, le filtre est réinitialisé.
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Le filtrage est réalisé par la méthode helper : <see cref="UIHelper.FiltrerBindingSource"/>.</description></item>
        ///     </list>
        /// </remarks>
        /// </summary>
        private void txtRecherche2_TextChanged(object sender, EventArgs e)
        {
            string texte = txtRecherche2.Text.Trim();

            if (string.IsNullOrEmpty(texte) || texte == PlaceholderText)
            {
                // Remet la liste complète si champ vide.
                bindingSourceLivres.DataSource = new SortableBindingList<Livre>(livresOriginaux);
            }
            else
            {
                UIHelper.FiltrerBindingSource(bindingSourceLivres, txtRecherche2, livresOriginaux, "Id", ColTitre, "Auteur", ColGenre, ColPublic);
            }
        }

        #endregion

        ///////////////////////////////////////////////////
        // Méthodes pour l'onglet 3 : commandes DVD      //
        ///////////////////////////////////////////////////

        #region Onglet 3 - Commande de DVD

        /// <summary>
        /// Réinitialise la textBox de recherche et recharge la liste complète des DVD.
        /// </summary>.
        private void btnClear3_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche3, PlaceholderText);
            ChargerDVD();
        }

        /// <summary>
        /// Bouton "Commander" de l'onglet DVD.
        /// Valide la sélection et les informations saisies, puis crée une nouvelle commande DVD.
        /// </summary>
        private void btnCommanderDVD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblID_DVD.Text) || lblID_DVD.Text == IdBase)
            {
                MessageBox.Show("Veuillez sélectionner un DVD à commander.", MessageAlerte, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            Commande nouvelleCommande = new(
                id: "",
                titre: "",
                idLivreDvd: lblID_DVD.Text,
                dateCommande: DateTime.Now,
                montant: montant,
                nbExemplaires: nbExemplaires,
                suivi: StatutEnCours
            );

            bool success = controller.CreerNouvelleCommande(nouvelleCommande);

            if (success)
            {
                MessageBox.Show("Commande de DVD effectuée avec succès.", MessageSucces, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontantDVD.Text = "";
                nudNbExemplairesDVD.Value = 1;
                lblID_DVD.Text = IdBase;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Charge la liste complète des DVD et configure le DataGridView pour affichage, tri et mise en forme.
        /// Configure le ComboBox cbRevue.
        /// </summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Récupère les données via le contrôleur <see cref="FrmSuiviCommandesController"/>.</description>
        ///         </item>
        ///         <item>
        ///             <description>Utilise <see cref="SortableBindingList{T}"/> pour activer le tri sur chaque colonne.</description>
        ///         </item>
        ///         <item>
        ///             <description>Définit l’ordre et le texte des en-têtes de colonnes.</description>
        ///         </item>
        ///         <item>
        ///             <description>Masque les colonnes techniques (IDs, images et synopsis) non pertinentes pour l’utilisateur.</description>
        ///         </item> 
        ///     </list>
        /// </remarks>
        private void ChargerDVD()
        {
            // Récupère la liste des livres via le contrôleur.
            dvdOriginaux = controller.GetAllDvd();

            // Rendre triable le BindingSource au travers de la classe helper SortableBindingList. Et l'assigner au DGV.
            var sortableDVD = new SortableBindingList<Dvd>(dvdOriginaux);
            bindingSourceDVD.DataSource = sortableDVD;
            dgvCommandesDVD.DataSource = bindingSourceDVD;

            // Assurer le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvCommandesDVD.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvCommandesDVD.Columns["Id"].HeaderText = "ID";
            dgvCommandesDVD.Columns[ColTitre].HeaderText = ColTitre;
            dgvCommandesDVD.Columns["Realisateur"].HeaderText = "Réalisateur";
            dgvCommandesDVD.Columns["Duree"].HeaderText = "Durée (min)";
            dgvCommandesDVD.Columns[ColGenre].HeaderText = ColGenre;
            dgvCommandesDVD.Columns[ColPublic].HeaderText = ColPublic;
            dgvCommandesDVD.Columns[ColRayon].HeaderText = ColRayon;

            dgvCommandesDVD.Columns["Id"].DisplayIndex = 0;
            dgvCommandesDVD.Columns[ColTitre].DisplayIndex = 1;

            dgvCommandesDVD.Columns["IdGenre"].Visible = false;
            dgvCommandesDVD.Columns["IdPublic"].Visible = false;
            dgvCommandesDVD.Columns["IdRayon"].Visible = false;
            dgvCommandesDVD.Columns["Image"].Visible = false;
            dgvCommandesDVD.Columns["Synopsis"].Visible = false;
        }

        /// <summary>
        /// Lorsqu’un DVD est cliqué dans le <see cref="dgvCommandesDVD"/> :  
        /// <list type="bullet">
        ///   <item><description>Récupère l’ID du DVD dans la cellule <c>Id</c> de la ligne sélectionnée.</description></item>
        ///   <item><description>Met à jour le <c>Label</c> <c>lblID_DVD</c> pour afficher cet ID.</description></item>
        /// </list>
        /// </summary>
        private void dgvCommandesDVD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow ligne = dgvCommandesDVD.Rows[e.RowIndex];
            var idDVD = ligne.Cells["Id"].Value.ToString();
            lblID_DVD.Text = idDVD;
        }

        /// <summary>
        /// Efface le placeholder "Rechercher" et modifie la couleur de la police pour permettre la saisie.
        /// Utilise <see cref="UIHelper.GererEntreeTextBox"/>.
        /// </summary>
        private void txtRecherche3_Enter(object sender, EventArgs e) => UIHelper.GererEntreeTextBox(txtRecherche3, PlaceholderText);

        /// <summary>
        /// Restaure le placeholder "Rechercher" si le textBox est vide et ajuste la couleur.
        /// Utilise <see cref="UIHelper.GererSortieTextBox"/>.
        /// </summary>
        private void txtRecherche3_Leave(object sender, EventArgs e) => UIHelper.GererSortieTextBox(txtRecherche3, PlaceholderText);

        /// <summary>
        /// Filtre dynamiquement la liste des DVD affichés dans le DataGridView
        /// en fonction du texte saisi.
        /// Si le champ de recherche est vide ou contient le placeholder, le filtre est réinitialisé.
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Le filtrage est réalisé par la méthode helper : <see cref="UIHelper.FiltrerBindingSource"/>.</description></item>
        ///     </list>
        /// </remarks>
        /// </summary>
        private void txtRecherche3_TextChanged(object sender, EventArgs e)
        {
            string texte = txtRecherche3.Text.Trim();

            if (string.IsNullOrEmpty(texte) || texte == PlaceholderText)
            {
                // Remet la liste complète si champ vide. Si l'utilisateur sélectinne la ligne entière et la supprime.
                bindingSourceDVD.DataSource = new SortableBindingList<Dvd>(dvdOriginaux);
            }
            else
            {
                UIHelper.FiltrerBindingSource(bindingSourceDVD, txtRecherche3, dvdOriginaux, "Id", ColTitre, "Realisateur", ColGenre, ColPublic);
            }
        }

        #endregion
    }
}