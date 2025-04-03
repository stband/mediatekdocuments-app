using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Formulaire principal de l'application.
    /// Affiche un accueil personnalisé et redirige vers les différentes fonctionnalités
    /// (documents, commandes, abonnements) selon les droits de l'utilisateur.
    /// </summary>
    public partial class FrmAccueil : Form
    {
        private readonly Access access = Access.GetInstance();

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="FrmAccueil"/>.
        /// Configure l'accès aux boutons en fonction du rôle de l'utilisateur
        /// et affiche un message de bienvenue comportant des informations sur la session en cours.
        /// </summary>
        public FrmAccueil()
        {
            InitializeComponent();
            RestrictionAccesEnFonctionDesRoles();
            lblBienvenue.Text = $"Bienvenue {Session.Utilisateur.Nom}, vous êtes connecté(e) en tant que {Session.Utilisateur.Service}.";
        }

        /// <summary>
        /// Vérifie si des abonnements expirent dans les 30 jours à venir
        /// et affiche une notification si c'est le cas.
        /// </summary>
        private void VerifierAbonnementsExpirants()
        {
            List<Abonnement> abonnements = access.GetAllAbonnements();
            List<String> abonnementsExpirants = Abonnement.GetAbonnementsExpirantDans30Jours(abonnements);

            if (abonnementsExpirants.Any())
            {
                string message = "ATTENTION : certains abonnements expirent dans moins de 30 jours :\n" + string.Join("\n", abonnementsExpirants);
                MessageBox.Show(message, "Abonnements à renouveler", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Active ou désactive les boutons du formulaire selon le rôle de l'utilisateur.
        /// Les services "prêts" n'ont pas accès aux commandes ni aux abonnements.
        /// Les services "administratif" et "administrateur" déclenchent la vérification d'abonnements
        /// expirants à l'affichage du formulaire.
        /// </summary>
        private void RestrictionAccesEnFonctionDesRoles()
        {
            string service = Session.Utilisateur.Service.ToLower();

            if (service == "prêts")
            {
                btnCommandes.Enabled = false;
                btnAbonnements.Enabled = false;
                btnCommandes.Visible = false;
                btnAbonnements.Visible = false;
            }

            if (service == "administratif")
            {
                this.Shown += FrmAccueil_Shown;
            }

            if (service == "administrateur")
            {
                this.Shown += FrmAccueil_Shown;
            }
        }

        // <summary>
        /// Ouvre la fenêtre de gestion des documents.
        /// </summary>
        /// <param name="sender">Le bouton "Documents".</param>
        /// <param name="e">Les données de l'événement.</param>
        private void btnDocuments_Click(object sender, EventArgs e)
        {
            FrmMediatek frmDocuments = new FrmMediatek();
            frmDocuments.ShowDialog();
        }

        /// <summary>
        /// Ouvre la fenêtre de suivi des commandes.
        /// </summary>
        /// <param name="sender">Le bouton "Commandes".</param>
        /// <param name="e">Les données de l'événement.</param>
        private void btnCommandes_Click(object sender, EventArgs e)
        {
            FrmSuiviCommandes frmCommandes = new FrmSuiviCommandes();
            frmCommandes.ShowDialog();
        }

        /// <summary>
        /// Ouvre la fenêtre de gestion des abonnements aux revues.
        /// </summary>
        /// <param name="sender">Le bouton "Abonnements".</param>
        /// <param name="e">Les données de l'événement.</param>
        private void btnAbonnements_Click(object sender, EventArgs e)
        {
            FrmAbonnementRevues frmAbonnements = new FrmAbonnementRevues();
            frmAbonnements.ShowDialog();
        }

        /// <summary>
        /// Événement déclenché une fois que le formulaire est visible.
        /// Lance la vérification des abonnements expirants.
        /// </summary>
        /// <param name="sender">Le formulaire <see cref="FrmAccueil"/>.</param>
        /// <param name="e">Les données de l'événement.</param>
        private void FrmAccueil_Shown(object sender, EventArgs e)
        {
            VerifierAbonnementsExpirants();
        }

        /// <summary>
        /// Déconnecte l'utilisateur et retourne au formulaire d'authentification.
        /// Icon créer par SumberRejeki - Flaticon : https://www.flaticon.com/free-icons/log-out
        /// </summary>
        /// <param name="sender">La PictureBox de déconnexion.</param>
        /// <param name="e">Les données de l'événement.</param>
        private void pbLogout_Click(object sender, EventArgs e)
        {
            Session.Utilisateur = null;
            this.Hide();
            FrmConnexion frmConnexion = new FrmConnexion();
            frmConnexion.ShowDialog();
            this.Close();
        }
    }
}
