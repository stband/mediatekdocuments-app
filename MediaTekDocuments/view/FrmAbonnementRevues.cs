using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MediaTekDocuments.dal;
using MediaTekDocuments.model;
using MediaTekDocuments.helper;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.view
{
    public partial class FrmAbonnementRevues : Form
    {
        private readonly FrmAbonnementRevuesController controller;

        public FrmAbonnementRevues()
        {
            InitializeComponent();
            controller = new FrmAbonnementRevuesController();
        }

        /// <summary>
        /// Gestionnaire de l'événement Load du formulaire.
        /// Charge les abonnements lors de l'initialisation.
        /// </summary>
        private void FrmAbonnementRevues_Load(object sender, EventArgs e)
        {
            LoadAbonnements();
        }


        ///////////////////////////////////////////////////
        //             Méthodes générales                //
        ///////////////////////////////////////////////////


        /// <summary>
        /// Gère le changement d'onglet dans le TabControl.
        /// Recharge les données correspondantes en fonction de l'onglet sélectionné :
        /// - Onglet 0 : Chargement des abonnements.
        /// - Onglet 1 : Chargement des revues.
        /// </summary>
        private void tabAbonnements_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabAbonnements.SelectedIndex)
            {
                case 0: // Onglet 1 : Gestion des abonnements.
                    LoadAbonnements();
                    break;

                case 1: // Onglet 2 : Gestion des revues.
                    LoadRevues();
                    break;
            }
        }


        //////////////////////////////////////////////////////////
        //  Méthodes pour l'onglet 1 : Gestion des abonnements  //
        //////////////////////////////////////////////////////////


        /// <summary>
        /// Liste complète des abonnements sans filtre.
        /// Sert à réinitialiser le filtrage dans le DataGridView.
        /// </summary>
        private List<Abonnement> abonnementsOriginaux;

        /// <summary>
        /// Réinitialise le TextBox de recherche en remettant le placeholder "Rechercher"
        /// et recharge la liste des abonnements.
        /// </summary>
        /// <param name="sender">Bouton "Effacer" (btnClearRecherche).</param>
        /// <param name="e">Données de l'événement.</param>
        private void btnClearRecherche_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRecherche, "Rechercher");
            LoadAbonnements();
        }

        /// <summary>
        /// Gère le clic sur une cellule du DataGridView des abonnements.
        /// Met à jour les contrôles graphiques (DateTimePickers et label) pour préparer le renouvellement.
        /// </summary>
        /// <param name="sender">Le DataGridView (dgvAbonnements).</param>
        /// <param name="e">Données associées (incluant l'indice de la ligne).</param>
        private void dgvAbonnements_CellClick(object sender, DataGridViewCellEventArgs e)
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
        /// Charge les abonnements depuis la base de données et configure le DataGridView pour
        /// afficher et trier les abonnements.
        /// La méthode utilise une SortableBindingList pour permettre un tri automatique.
        /// </summary>
        private void LoadAbonnements()
        {
            abonnementsOriginaux = controller.GetAllAbonnements();

            // Créer le BindingSource et le rendre triable au travers de la classe helper SortableBindingList.
            var sortableAbonnements = new SortableBindingList<Abonnement>(abonnementsOriginaux);
            BindingSource bs = new BindingSource();
            bs.DataSource = sortableAbonnements;
            dgvAbonnements.DataSource = bs;

            // Assurer le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvAbonnements.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Configuration des colonnes et de l'ordre d'affichage.
            dgvAbonnements.Columns["IdAbonnement"].HeaderText = "N° commande";
            dgvAbonnements.Columns["TitreRevue"].HeaderText = "Titre";
            dgvAbonnements.Columns["DateCommande"].HeaderText = "Date de souscription";
            dgvAbonnements.Columns["DateFinAbonnement"].HeaderText = "Date de fin";
            dgvAbonnements.Columns["Montant"].HeaderText = "Montant (€)";

            dgvAbonnements.Columns["IdAbonnement"].DisplayIndex = 0;
            dgvAbonnements.Columns["TitreRevue"].DisplayIndex = 1;

            dgvAbonnements.Columns["IdRevue"].Visible = false;

            // Colorer les lignes dont la date de fin est passée (abonnements expirés).
            foreach (DataGridViewRow row in dgvAbonnements.Rows)
            {
                if (row.Cells["DateFinAbonnement"].Value != null)
                {
                    DateTime dateFin = Convert.ToDateTime(row.Cells["DateFinAbonnement"].Value);
                    if (dateFin < DateTime.Now)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton "Renouveler", permettant de renouveler l'abonnement sélectionné.
        /// Vérifie si un abonnement actif existe déjà pour la revue et crée un nouvel abonnement via le contrôleur.
        /// </summary>
        /// <param name="sender">Bouton "Renouveler" (btnRenouvelerAbonnement).</param>
        /// <param name="e">Données de l'événement.</param>
        private void btnRenouvelerAbonnement_Click(object sender, EventArgs e)
        {
            if (dgvAbonnements.SelectedRows.Count > 0)
            {
                // Récupérer l'abonnement sélectionné.
                DataGridViewRow row = dgvAbonnements.SelectedRows[0];
                string idRevue = row.Cells["IdRevue"].Value.ToString();
                string titreRevue = row.Cells["TitreRevue"].Value.ToString();
                string idAbonnement = row.Cells["IdAbonnement"].Value.ToString();
                double montant = Convert.ToDouble(row.Cells["Montant"].Value);
                DateTime dateFinPrecedent = Convert.ToDateTime(row.Cells["DateFinAbonnement"].Value);

                // Vérification : empêche le renouvellement si un abonnement actif existe déjà.
                if (controller.AbonnementActifExiste(idRevue, dtpDateDebutRenouvellement.Value, dtpDateFinRenouvellement.Value))
                {
                    MessageBox.Show("Impossible de renouveler : un abonnement actif existe déjà pour cette revue. Vous devez choisir une date de début et de fin qui ne rentre pas en colision avec l'abonnement toujours en cours.", "Abonnement actif", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Construction du nouvel abonnement.
                Abonnement nouveauAbo = new Abonnement
                {
                    IdRevue = idRevue,
                    TitreRevue = titreRevue,
                    DateCommande = dtpDateDebutRenouvellement.Value,
                    DateFinAbonnement = dtpDateFinRenouvellement.Value,
                    Montant = montant
                };

                bool result = controller.CreerAbonnement(nouveauAbo);
                if (result)
                {
                    MessageBox.Show("Abonnement renouvelé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAbonnements();
                }
                else
                {
                    MessageBox.Show("Erreur lors du renouvellement de l'abonnement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un abonnement dans la liste pour le renouveler.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton "Supprimer" afin de supprimer l'abonnement sélectionné après confirmation.
        /// </summary>
        /// <param name="sender">Bouton "Supprimer" (btnSupprimerAbonnement).</param>
        /// <param name="e">Données de l'événement.</param>
        private void btnSupprimerAbonnement_Click(object sender, EventArgs e)
        {
            if (dgvAbonnements.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvAbonnements.SelectedRows[0];
                string idAbonnement = row.Cells["IdAbonnement"].Value.ToString();

                DialogResult confirmation = MessageBox.Show("Voulez-vous vraiment supprimer cet abonnement ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmation == DialogResult.Yes)
                {
                    bool result = controller.SupprimerAbonnement(idAbonnement);
                    if (result)
                    {
                        MessageBox.Show("Abonnement supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAbonnements();
                    }
                    else
                    {
                        MessageBox.Show("Suppression impossible : cet abonnement est lié à des exemplaires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un abonnement dans la liste pour le supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Gère l'événement Enter du TextBox txtRecherche.
        /// Efface le placeholder "Rechercher" et modifie la couleur de la police pour permettre la saisie.
        /// </summary>
        /// <param name="sender">Le TextBox txtRecherche.</param>
        /// <param name="e">Données de l'événement.</param>
        private void txtRecherche_Enter(object sender, EventArgs e)
        {
            UIHelper.GererEntreeTextBox(txtRecherche, "Rechercher");
        }

        /// <summary>
        /// Gère l'événement Leave du TextBox txtRecherche.
        /// Restaure le placeholder "Rechercher" si le TextBox est vide et ajuste la couleur.
        /// </summary>
        /// <param name="sender">Le TextBox txtRecherche.</param>
        /// <param name="e">Données de l'événement.</param>
        private void txtRecherche_Leave(object sender, EventArgs e)
        {
            UIHelper.GererSortieTextBox(txtRecherche, "Rechercher");
        }

        /// <summary>
        /// Filtre dynamiquement la liste des abonnements affichée dans le DataGridView
        /// en fonction du texte saisi dans txtRecherche.
        /// Si le champ de recherche est vide ou contient le placeholder, le filtre est réinitialisé.
        /// </summary>
        /// <param name="sender">Le TextBox txtRecherche.</param>
        /// <param name="e">Données de l'événement.</param>
        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            if (dgvAbonnements.DataSource is BindingSource bs)
            {
                string texte = txtRecherche.Text.Trim();

                if (string.IsNullOrEmpty(texte) || texte == "Rechercher")
                {
                    // Remet la liste complète si champ vide.
                    bs.DataSource = new SortableBindingList<Abonnement>(abonnementsOriginaux);
                }
                else
                {
                    UIHelper.FiltrerBindingSource<Abonnement>(bs, txtRecherche, abonnementsOriginaux, "IdAbonnement", "TitreRevue");
                }
            }
        }


        //////////////////////////////////////////////////////////
        //    Méthodes pour l'onglet 2 : Gestion des revues     //
        //////////////////////////////////////////////////////////


        // Stocke la liste complète des revues, utilisée pour réinitialiser le filtrage.
        private List<Revue> revuesOriginaux;

        /// <summary>
        /// Réinitialise le TextBox de recherche pour les revues (txtRechercheRevues)
        /// en lui assignant le placeholder "Rechercher" et recharge la liste complète des revues.
        /// </summary>
        /// <param name="sender">Bouton "Effacer" (btnClearRecherche2).</param>
        /// <param name="e">Données de l'événement.</param>
        private void btnClearRecherche2_Click(object sender, EventArgs e)
        {
            UIHelper.ClearTextBox(txtRechercheRevues, "Rechercher");
            LoadRevues();
        }


        /// <summary>
        /// Gère le clic sur une cellule du DataGridView des revues (dgvRevues).
        /// Met à jour le ComboBox (cbRevue) avec la revue sélectionnée afin de faciliter la souscription.
        /// </summary>
        /// <param name="sender">Le DataGridView dgvRevues.</param>
        /// <param name="e">Données de l'événement, incluant l'indice de la ligne.</param>
        private void dgvRevues_CellClick(object sender, DataGridViewCellEventArgs e)
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
        /// Charge les revues depuis la base de données et configure le DataGridView dgvRevues
        /// pour afficher et trier la liste des revues.
        /// Met aussi à jour le ComboBox cbRevue avec la liste complète des revues.
        /// </summary>
        private void LoadRevues()
        {
            revuesOriginaux = controller.GetAllRevues();

            // Crée une liste triable (SortableBindingList) et l'assigne à un BindingSource.
            var sortableRevues = new SortableBindingList<Revue>(revuesOriginaux);
            BindingSource bs = new BindingSource { DataSource = sortableRevues };
            dgvRevues.DataSource = bs;

            // Active le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvRevues.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Configuration des colonnes et de l'ordre d'affichage.
            dgvRevues.Columns["Id"].HeaderText = "N°";
            dgvRevues.Columns["Titre"].HeaderText = "Titre";
            dgvRevues.Columns["Genre"].HeaderText = "Genre";
            dgvRevues.Columns["Public"].HeaderText = "Public";
            dgvRevues.Columns["Rayon"].HeaderText = "Rayon";
            dgvRevues.Columns["Periodicite"].HeaderText = "Périodicité";
            dgvRevues.Columns["DelaiMiseADispo"].HeaderText = "Délai de mise à dispo (jours)";

            dgvRevues.Columns["Id"].DisplayIndex = 0;
            dgvRevues.Columns["Titre"].DisplayIndex = 1;
            dgvRevues.Columns["Genre"].DisplayIndex = 2;

            dgvRevues.Columns["Image"].Visible = false;
            dgvRevues.Columns["IdGenre"].Visible = false;
            dgvRevues.Columns["IdPublic"].Visible = false;
            dgvRevues.Columns["IdRayon"].Visible = false;

            // Configuration du comboBoxRevue.
            cbRevue.DataSource = revuesOriginaux;
            cbRevue.DisplayMember = "Titre";
            cbRevue.ValueMember = "Id";
        }

        /// <summary>
        /// Gère l'événement Enter du TextBox txtRechercheRevues.
        /// Efface le placeholder "Rechercher" et ajuste la couleur pour permettre la saisie.
        /// </summary>
        /// <param name="sender">Le TextBox txtRechercheRevues.</param>
        /// <param name="e">Les données de l'événement.</param>
        private void txtRechercheRevues_Enter(object sender, EventArgs e)
        {
            UIHelper.GererEntreeTextBox(txtRechercheRevues, "Rechercher");
        }

        /// <summary>
        /// Gère l'événement Leave du TextBox txtRechercheRevues.
        /// Restaure le placeholder "Rechercher" si le TextBox est vide.
        /// </summary>
        /// <param name="sender">Le TextBox txtRechercheRevues.</param>
        /// <param name="e">Données de l'événement.</param>
        private void txtRechercheRevues_Leave(object sender, EventArgs e)
        {
            UIHelper.GererSortieTextBox(txtRechercheRevues, "Rechercher");
        }

        /// <summary>
        /// Filtre dynamiquement la liste des revues affichée dans dgvRevues
        /// en fonction du texte saisi dans txtRechercheRevues.
        /// Si le texte est vide ou correspond au placeholder, le filtre est supprimé.
        /// </summary>
        /// <param name="sender">Le TextBox txtRechercheRevues.</param>
        /// <param name="e">Données de l'événement.</param>
        private void txtRechercheRevues_TextChanged(object sender, EventArgs e)
        {
            if (dgvRevues.DataSource is BindingSource bs)
            {
                string texte = txtRechercheRevues.Text.Trim();

                if (string.IsNullOrEmpty(texte) || texte == "Rechercher")
                {
                    // Remet la liste complète si champ vide.
                    bs.DataSource = new SortableBindingList<Revue>(revuesOriginaux);
                }
                else
                {
                    UIHelper.FiltrerBindingSource<Revue>(bs, txtRechercheRevues, revuesOriginaux, "Id", "Titre", "Genre", "Public");
                }
            }
        }

        /// <summary>
        /// Crée un nouvel abonnement en se basant sur la revue sélectionnée dans cbRevue 
        /// et les données saisies dans les contrôles de souscription.
        /// Valide les dates et le montant, et vérifie qu'aucun abonnement actif n'existe déjà pour la période.
        /// En cas de succès, l'abonnement est créé via le contrôleur et la liste des abonnements est rechargée.
        /// </summary>
        /// <param name="sender">Le bouton btnSouscrireAbonnement.</param>
        /// <param name="e">Données de l'événement.</param>
        private void btnSouscrireAbonnement_Click(object sender, EventArgs e)
        {
            Revue revueSelectionnee = cbRevue.SelectedItem as Revue;

            if (revueSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une revue.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idRevue = revueSelectionnee.Id;
            string titreRevue = revueSelectionnee.Titre;
            DateTime dateDebut = dtpDateDebutSouscription.Value;
            DateTime dateFin = dtpDateFinSouscription.Value;

            if (dateDebut >= dateFin)
            {
                MessageBox.Show("La date de début doit être antérieure à la date de fin.", "Erreur de date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier que le montant est dans un format correct.
            if (!double.TryParse(txtMontant.Text, out double montant) || montant <= 0)
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

            DialogResult confirmation = MessageBox.Show(
                $"Confirmez-vous la souscription à la revue \"{titreRevue}\" du {dateDebut.ToShortDateString()} au {dateFin.ToShortDateString()} pour {montant} € ?",
                "Confirmer la souscription",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation == DialogResult.No)
                return;

            // Création de l'objet.
            Abonnement nouvelAbo = new Abonnement
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
                MessageBox.Show("Souscription enregistrée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAbonnements(); // Recharge les abonnements dans l'onglet 1 pour mettre à jour la liste des abonnements dynamiquement.
            }
            else
            {
                MessageBox.Show("Erreur lors de la création de l'abonnement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}