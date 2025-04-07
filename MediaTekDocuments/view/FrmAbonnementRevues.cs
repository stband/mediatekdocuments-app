using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.helper;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.view
{
    public partial class FrmAbonnementRevues : Form
    {
        #region constantes de la classe

        private const string PlaceholderText = "Rechercher";
        private const string MessageSucces = "Succès";
        private const string MessageErreur = "Erreur";
        private const string MessageInformation = "Information";

        private const string ColIdAbonnement = "IdAbonnement";
        private const string ColTitreRevue = "TitreRevue";
        private const string ColDateFinAbonnement = "DateFinAbonnement";
        private const string ColIdRevue = "IdRevue";
        private const string ColTitre = "Titre";
        private const string ColGenre = "Genre";


        #endregion

        #region bindingsource et données locales

        // BindingSource pour centraliser la liaison des données pour chaque onglet.
        private readonly BindingSource bindingSourceAbonnements = [];
        private readonly BindingSource bindingSourceRevues = [];

        // Copie en mémoire pour reset les listes originales après un trie ou un filtre.
        private List<Abonnement> abonnementsOriginaux = [];
        private List<Revue> revuesOriginaux = [];

        // Contrôleur du formulaire unique
        private readonly FrmAbonnementRevuesController controller = new();

        #endregion

        /// <summary>
        /// Constructeur : initialise les composants du formulaire.
        /// </summary>
        public FrmAbonnementRevues()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Événement initial du chargement de la fenêtre.
        /// Lance <see cref="ChargerAbonnements"/> pour afficher les abonnements.
        /// </summary>
        private void FrmAbonnementRevues_Load(object? sender, EventArgs e) => ChargerAbonnements();


        ///////////////////////////////////////////////////
        //             Méthodes générales                //
        ///////////////////////////////////////////////////

        #region Gestion des onglets

        /// <summary>
        /// Gère le changement d’onglet :  
        /// <list type="bullet">
        ///   <item><description>Onglet 1 : charge tous les abonnements.</description></item>
        ///   <item><description>Onglet 2 : charge les revues.</description></item>
        /// </list>
        /// </summary>
        private void tabAbonnements_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (tabAbonnements.SelectedIndex)
            {
                case 0: // Onglet 1 : Gestion des abonnements.
                    ChargerAbonnements();
                    break;
                case 1: // Onglet 2 : Gestion des revues.
                    ChargerRevues();
                    break;
            }
        }

        #endregion

        //////////////////////////////////////////////////////////
        //  Méthodes pour l'onglet 1 : Gestion des abonnements  //
        //////////////////////////////////////////////////////////

        #region Onglet 1 — Gestion des abonnements

        /// <summary>
        /// Vide le champ de recherche et recharge l'ensemble des abonnements.
        /// Utilise <see cref="UIHelper.ClearTextBox"/> pour réinitialiser le placeholder.
        /// </summary>
        private void btnClearRecherche_Click(object? sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche, PlaceholderText);
            ChargerAbonnements();
        }

        /// <summary>
        /// Dès qu’on clique sur une ligne du <see cref="dgvAbonnements"/>, on :  
        /// <list type="bullet">
        ///   <item><description>Récupère l’abonnement sélectionné.</description></item>
        ///   <item><description>Mécanisme anti-chevauchement lors du renouvellement : prépare les DateTimePicker pour un renouvellement automatique : 
        ///   la date de début de renouvellement est mise en place un jour après la date de fin de l'abonnement. La date de fin de renouvellement est
        ///   mise en place un an après la date de début de renouvellement.
        ///   </description></item>
        ///   <item><description>Met à jour le label pour afficher le numéro de commande de la revue.</description></item>
        /// </list>
        /// </summary>
        private void dgvAbonnements_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow ligne = dgvAbonnements.Rows[e.RowIndex];
                Abonnement abonnement = (Abonnement)ligne.DataBoundItem;

                // Préparer les champs pour le renouvellement automatique.
                DateTime dateDebutRenouvellement = abonnement.DateFinAbonnement.AddDays(1);
                DateTime dateFinRenouvellement = dateDebutRenouvellement.AddYears(1);
                dtpDateDebutRenouvellement.Value = dateDebutRenouvellement;
                dtpDateFinRenouvellement.Value = dateFinRenouvellement;

                // Afficher l'ID de l'abonnement actuel.
                lblAbonnementID.Text = abonnement.IdAbonnement;
            }
        }

        /// <summary>
        /// Charge la liste complète des abonnements et configure le DataGridView pour affichage, tri et mise en forme.
        /// </summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Récupère les données via le contrôleur <see cref="FrmAbonnementRevuesController"/>.</description>
        ///         </item>
        ///         <item>
        ///             <description>Utilise <see cref="SortableBindingList{T}"/> pour activer le tri sur chaque colonne.</description>
        ///         </item>
        ///         <item>
        ///             <description>Définit l’ordre et le texte des en-têtes de colonnes.</description>
        ///         </item>
        ///         <item>
        ///             <description>Masque la colonne technique (ID) non pertinente pour l’utilisateur.</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        private void ChargerAbonnements()
        {
            abonnementsOriginaux = controller.GetAllAbonnements();

            // Rendre triable le BindingSourcee au travers de la classe helper SortableBindingList.
            var sortableAbonnements = new SortableBindingList<Abonnement>(abonnementsOriginaux);
            bindingSourceAbonnements.DataSource = sortableAbonnements;
            dgvAbonnements.DataSource = bindingSourceAbonnements;

            // Assurer le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvAbonnements.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvAbonnements.Columns[ColIdAbonnement].HeaderText = "N° commande";
            dgvAbonnements.Columns[ColTitreRevue].HeaderText = "Titre";
            dgvAbonnements.Columns["DateCommande"].HeaderText = "Date de début";
            dgvAbonnements.Columns[ColDateFinAbonnement].HeaderText = "Date de fin";
            dgvAbonnements.Columns["Montant"].HeaderText = "Montant (€)";

            dgvAbonnements.Columns[ColIdAbonnement].DisplayIndex = 0;
            dgvAbonnements.Columns[ColTitreRevue].DisplayIndex = 1;

            dgvAbonnements.Columns[ColIdRevue].Visible = false;
        }

        /// <summary>
        /// Applique une couleur de fond aux lignes du DataGridView selon l'état de l'abonnement (actif ou expiré).
        /// Utilisé pour améliorer la lisibilité.
        /// </summary>
        private void dgvAbonnements_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var ligne = dgvAbonnements.Rows[e.RowIndex];
            var abonnement = (Abonnement)ligne.DataBoundItem;

            if (abonnement == null) return;

            if (abonnement.DateFinAbonnement < DateTime.Now)
            {
                ligne.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton "Renouveler", permettant de renouveler l'abonnement sélectionné.
        /// Vérifie si un abonnement actif existe déjà pour la revue et crée un nouvel abonnement via le contrôleur.
        /// </summary>
        private void btnRenouvelerAbonnement_Click(object? sender, EventArgs e)
        {
            if (dgvAbonnements.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Veuillez sélectionner un abonnement dans la liste pour le renouveler.",
                    MessageInformation,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // Récupérer l'abonnement sélectionné.
            var abonnement = (Abonnement)dgvAbonnements.SelectedRows[0].DataBoundItem;

            // Vérification : empêche le renouvellement si un abonnement actif existe dans la période choisie.
            if (controller.AbonnementActifExiste(abonnement.IdRevue, dtpDateDebutRenouvellement.Value, dtpDateFinRenouvellement.Value))
            {
                MessageBox.Show("Un abonnement actif existe déjà pour cette revue. Vous devez choisir une date de début et de fin qui ne rentre pas en colision avec l'abonnement toujours en cours.",
                    "Renouvellement impossible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Construction du nouvel abonnement.
            Abonnement nouveauAbo = new()
            {
                IdRevue = abonnement.IdRevue,
                TitreRevue = abonnement.TitreRevue,
                DateCommande = dtpDateDebutRenouvellement.Value,
                DateFinAbonnement = dtpDateFinRenouvellement.Value,
                Montant = abonnement.Montant
            };

            var message = $"""
                Êtes-vous sûr de vouloir renouveler l'abonnement n°{abonnement.IdAbonnement} ?
                Du {nouveauAbo.DateCommande:dd/MM/yyyy} au {nouveauAbo.DateFinAbonnement:dd/MM/yyyy} ?
            """;

            DialogResult confirm = MessageBox.Show(
                message,
                "Confirmation de suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            bool result = controller.CreerAbonnement(nouveauAbo);
                
            if (result)
            {
                MessageBox.Show("Abonnement renouvelé avec succès.", MessageSucces, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerAbonnements();
            } else
            {
                MessageBox.Show("Erreur lors du renouvellement de l'abonnement.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Supprime l'abonnement sélectionné après confirmation.
        /// La suppression est gérée par : <see cref="FrmAbonnementRevuesController.SupprimerAbonnement"/>.
        /// </summary>
        private void btnSupprimerAbonnement_Click(object? sender, EventArgs e)
        {
            if (dgvAbonnements.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvAbonnements.SelectedRows[0];
                string idAbonnement = row.Cells[ColIdAbonnement].Value?.ToString() ?? string.Empty;

                DialogResult confirmation = MessageBox.Show($"Voulez-vous vraiment supprimer l'abonnement n°{idAbonnement} ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (confirmation == DialogResult.Yes)
                {
                    bool result = controller.SupprimerAbonnement(idAbonnement);
                    
                    if (result)
                    {
                        MessageBox.Show("Abonnement supprimé avec succès.", MessageSucces, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ChargerAbonnements();
                    }
                    else
                    {
                        MessageBox.Show("Suppression impossible : cet abonnement est lié à des exemplaires.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un abonnement dans la liste pour le supprimer.", MessageInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Efface le placeholder "Rechercher" et modifie la couleur de la police pour permettre la saisie.
        /// Utilise <see cref="UIHelper.GererEntreeTextBox"/>.
        /// </summary>
        private void txtRecherche_Enter(object? sender, EventArgs e) => UIHelper.GererEntreeTextBox(txtRecherche, PlaceholderText);

        /// <summary>
        /// Restaure le placeholder "Rechercher" si le textBox est vide et ajuste la couleur.
        /// Utilise <see cref="UIHelper.GererSortieTextBox"/>.
        /// </summary>
        private void txtRecherche_Leave(object? sender, EventArgs e) => UIHelper.GererSortieTextBox(txtRecherche, PlaceholderText);

        /// <summary>
        /// Filtre dynamiquement la liste des abonnements affichés dans le DataGridView
        /// en fonction du texte saisi.
        /// Si le champ de recherche est vide ou contient le placeholder, le filtre est réinitialisé.
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Le filtrage est réalisé par la méthode helper : <see cref="UIHelper.FiltrerBindingSource"/>.</description></item>
        ///     </list>
        /// </remarks>
        /// </summary>
        private void txtRecherche_TextChanged(object? sender, EventArgs e)
        {
            string texte = txtRecherche.Text.Trim();

            if (string.IsNullOrEmpty(texte) || texte == PlaceholderText)
            {
                // Remet la liste complète si champ vide.
                bindingSourceAbonnements.DataSource = new SortableBindingList<Abonnement>(abonnementsOriginaux);
            }
            else
            {
                UIHelper.FiltrerBindingSource(bindingSourceAbonnements, txtRecherche, abonnementsOriginaux, ColIdAbonnement, ColTitreRevue, "Montant");
            }
        }

        #endregion

        //////////////////////////////////////////////////////////
        //    Méthodes pour l'onglet 2 : Gestion des revues     //
        //////////////////////////////////////////////////////////

        #region Onglet 2 - Gestion des revues

        /// <summary>
        /// Réinitialise la textBox de recherche et recharge la liste complète des revues.
        /// </summary>.
        private void btnClearRecherche2_Click(object? sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRechercheRevues, PlaceholderText);
            ChargerRevues();
        }

        /// <summary>
        /// Gère le clic sur une cellule du DataGridView des revues (dgvRevues).
        /// Met à jour <see cref="cbRevue"/> avec la revue sélectionnée afin de faciliter la souscription à un abonnement.
        /// </summary>
        private void dgvRevues_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow ligne = dgvRevues.Rows[e.RowIndex];
                Revue revue = (Revue)ligne.DataBoundItem;

                // Préparer les champs pour la souscription.
                cbRevue.SelectedValue = revue.Id;
            }
        }

        /// <summary>
        /// Charge la liste complète des revues et configure le DataGridView pour affichage, tri et mise en forme.
        /// Configure le ComboBox cbRevue.
        /// </summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Récupère les données via le contrôleur <see cref="FrmAbonnementRevuesController"/>.</description>
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
        ///             <description>Configure le ComboBox <see cref="cbRevue"/> avec la liste complète des revues.</description>
        ///         </item>  
        ///     </list>
        /// </remarks>
        private void ChargerRevues()
        {
            revuesOriginaux = controller.GetAllRevues();

            // Rendre triable le BindingSourcee au travers de la classe helper SortableBindingList.
            var sortableRevues = new SortableBindingList<Revue>(revuesOriginaux);
            bindingSourceRevues.DataSource = sortableRevues;
            dgvRevues.DataSource = bindingSourceRevues;

            // Active le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvRevues.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Configuration des colonnes et de l'ordre d'affichage.
            dgvRevues.Columns["Id"].HeaderText = "N°";
            dgvRevues.Columns[ColTitre].HeaderText = "Titre";
            dgvRevues.Columns[ColGenre].HeaderText = "Genre";
            dgvRevues.Columns["Public"].HeaderText = "Public";
            dgvRevues.Columns["Rayon"].HeaderText = "Rayon";
            dgvRevues.Columns["Periodicite"].HeaderText = "Périodicité";
            dgvRevues.Columns["DelaiMiseADispo"].HeaderText = "Mise à dispo (jours)";

            dgvRevues.Columns["Id"].DisplayIndex = 0;
            dgvRevues.Columns[ColTitre].DisplayIndex = 1;
            dgvRevues.Columns[ColGenre].DisplayIndex = 2;

            dgvRevues.Columns["Image"].Visible = false;
            dgvRevues.Columns["IdGenre"].Visible = false;
            dgvRevues.Columns["IdPublic"].Visible = false;
            dgvRevues.Columns["IdRayon"].Visible = false;

            // Configuration du comboBoxRevue.
            cbRevue.DataSource = revuesOriginaux;
            cbRevue.DisplayMember = ColTitre;
            cbRevue.ValueMember = "Id";
        }

        /// <summary>
        /// Efface le placeholder "Rechercher" et modifie la couleur de la police pour permettre la saisie.
        /// Utilise <see cref="UIHelper.GererEntreeTextBox"/>.
        /// </summary>
        private void txtRechercheRevues_Enter(object? sender, EventArgs e) => UIHelper.GererEntreeTextBox(txtRechercheRevues, PlaceholderText);

        /// <summary>
        /// Restaure le placeholder "Rechercher" si le textBox est vide et ajuste la couleur.
        /// Utilise <see cref="UIHelper.GererSortieTextBox"/>.
        /// </summary>
        private void txtRechercheRevues_Leave(object? sender, EventArgs e) => UIHelper.GererSortieTextBox(txtRechercheRevues, PlaceholderText);


        /// <summary>
        /// Filtre dynamiquement la liste des revues affichées dans le DataGridView
        /// en fonction du texte saisi.
        /// Si le champ de recherche est vide ou contient le placeholder, le filtre est réinitialisé.
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Le filtrage est réalisé par la méthode helper : <see cref="UIHelper.FiltrerBindingSource"/>.</description></item>
        ///     </list>
        /// </remarks>
        /// </summary>
        private void txtRechercheRevues_TextChanged(object? sender, EventArgs e)
        {
            string texte = txtRechercheRevues.Text.Trim();

            if (string.IsNullOrEmpty(texte) || texte == PlaceholderText)
            {
                // Remet la liste complète si champ vide.
                bindingSourceRevues.DataSource = new SortableBindingList<Revue>(revuesOriginaux);
            }
            else
            {
                UIHelper.FiltrerBindingSource(bindingSourceRevues, txtRechercheRevues, revuesOriginaux, "Id", ColTitre, ColGenre, "Public");
            }
        }

        /// <summary>
        /// Crée un nouvel abonnement via <see cref="FrmAbonnementRevuesController.CreerAbonnement"/> en se basant sur la revue sélectionnée dans <see cref="cbRevue"/>
        /// et les données saisies dans les contrôles de souscription.
        /// <remarks>
        ///     <list type="bullet">
        ///         <item><description>Validation au niveau des dates, du format des données.</description></item>
        ///         <item><description>Vérifie qu'aucun abonnement actif similaire (pour la même revue) est déjà existant dans la même période pour éviter le chevauchement.</description></item>
        ///         <item><description>Demande une confirmation à l'utilisateur pour valider le processus.</description></item>
        ///     </list>
        /// </remarks>
        /// </summary>
        private void btnSouscrireAbonnement_Click(object? sender, EventArgs e)
        {
            // Vérifie que l'élément sélectionné du comboBox soit bien une instance de Revue.
            // Puis l'affecte à la variable locale revueSelectionnee.
            if (cbRevue.SelectedItem is not Revue revueSelectionnee)
            {
                MessageBox.Show("Veuillez sélectionner une revue.", MessageInformation, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Définition des champs pour préparer l'abonnement.
            string idRevue = revueSelectionnee.Id;
            string titreRevue = revueSelectionnee.Titre;
            DateTime dateDebut = dtpDateDebutSouscription.Value;
            DateTime dateFin = dtpDateFinSouscription.Value;
            double montant;

            if (dateDebut >= dateFin)
            {
                MessageBox.Show("La date de début de la souscription doit être antérieure à la date de fin.", "Erreur de date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier que le montant est dans un format correct.
            if (!double.TryParse(txtMontant.Text, out montant) || montant <= 0)
            {
                MessageBox.Show("Veuillez entrer un montant valide supérieur à 0.", "Montant invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier qu'aucun abonnement n'existe déjà dans cette période.
            if (controller.AbonnementActifExiste(idRevue, dateDebut, dateFin))
            {
                MessageBox.Show("Un abonnement existe déjà dans la période sélectionnée pour cette revue.", "Abonnement existant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = $"""
                Confirmez-vous la souscription à la revue "{titreRevue}"
                du {dateDebut:dd/MM/yyyy} au {dateFin:dd/MM/yyyy} pour {montant:0.00} € ?
            """;

            DialogResult confirmation = MessageBox.Show(
                message,
                "Confirmer la souscription",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation == DialogResult.No)
                return;

            // Création de l'objet.
            Abonnement nouvelAbo = new()
            {
                IdRevue = idRevue,
                TitreRevue = titreRevue,
                DateCommande = dateDebut,
                DateFinAbonnement = dateFin,
                Montant = montant
            };

            bool resultat = controller.CreerAbonnement(nouvelAbo);
            
            if (resultat)
            {
                MessageBox.Show("Souscription enregistrée avec succès.", MessageSucces, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erreur lors de la création de l'abonnement.", MessageErreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}