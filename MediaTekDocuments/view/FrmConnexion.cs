using MediaTekDocuments.controller;
using MediaTekDocuments.helper;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Formulaire de connexion de l'application.
    /// Permet à l'utilisateur de saisir ses identifiants, de vérifier l'accès et
    /// de rediriger vers l'accueil ou afficher un message d'erreur.
    /// C'est le formulaire d'initialisation de l'application.
    /// </summary>
    public partial class FrmConnexion : Form
    {
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="FrmConnexion"/>.
        /// </summary>
        public FrmConnexion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gestionnaire de l'événement Click du bouton "Se connecter".
        /// Valide les champs, appelle la méthode `seConnecter(login, mdp)` du contrôleur pour vérifier les identifiants de connexion, 
        /// puis ouvre l'accueil ou affiche un message d'erreur selon le résultat.
        /// </summary>
        /// <param name="sender">Le bouton de connexion.</param>
        /// <param name="e">Les données de l'événement.</param>
        public void btnSeConnecter_Click(object sender, EventArgs e)
        {
            string login = txtIdentifiant.Text;
            string mdp = txtMotDePasse.Text;

            if (login == "Identifiant") login = "";
            if (mdp == "Mot de passe") mdp = "";

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(mdp))
            {
                lblMessage.Text = "Veuillez remplir tous les champs";
                return;
            }

            FrmConnexionController controller = new FrmConnexionController();
            bool connexionOk = controller.SeConnecter(login, mdp);

            if (connexionOk)
            {
                // Gérer les services qui sont interdits à la connexion (seulement culture pour l'instant).
                if (Session.Utilisateur.Service == "Culture")
                {
                    MessageBox.Show($"Accès refusé : Le service {Session.Utilisateur.Service} n’est pas autorisé à utiliser cette application.", "Accès Refusé", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FrmAccueil accueil = new FrmAccueil();
                    this.Hide();
                    accueil.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                lblMessage.Text = "Identifiants ou mot de passe incorrects";
            }

        }

        /// <summary>
        /// Gestionnaire de l'événement Enter pour le TextBox des identifiants.
        /// Efface le message d'erreur potentiel et permet la saisie.
        /// </summary>
        /// <param name="sender">Le TextBox txtIdentifiant.</param>
        /// <param name="e">Les données de l'événement.</param>
        public void txtIdentifiant_Enter(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            UIHelper.GererEntreeTextBox(txtIdentifiant, "Identifiant");
        }

        /// <summary>
        /// Gestionnaire de l'événement Leave pour le TextBox des identifiants.
        /// Restaure le placeholder si le champ est vide.
        /// </summary>
        /// <param name="sender">Le TextBox txtIdentifiant.</param>
        /// <param name="e">Les données de l'événement.</param>
        public void txtIdentifiant_Leave(object sender, EventArgs e)
        {
            UIHelper.GererSortieTextBox(txtIdentifiant, "Identifiant");
        }

        // <summary>
        /// Gestionnaire de l'événement Enter pour le TextBox du mot de passe.
        /// Efface le message d'erreur potentiel et permet la saisie, active aussi le masquage du mot de passe.
        /// </summary>
        /// <param name="sender">Le TextBox txtMotDePasse.</param>
        /// <param name="e">Les données de l'événement.</param>
        public void txtMotDePasse_Enter(object sender, EventArgs e)
        {
            UIHelper.GererEntreeTextBox(txtMotDePasse, "Mot de passe");
            lblMessage.Text = "";
            txtMotDePasse.UseSystemPasswordChar = true;
        }

        // <summary>
        /// Gestionnaire de l'événement Leave pour le TextBox du mot de passe.
        /// Restaure le placeholder si le champ est vide.
        /// </summary>
        /// <param name="sender">Le TextBox txtMotDePasse.</param>
        /// <param name="e">Les données de l'événement.</param>
        public void txtMotDePasse_Leave(object sender, EventArgs e)
        {
            UIHelper.GererSortieTextBox(txtMotDePasse, "Mot de passe");
        }
    }
}
