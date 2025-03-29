using MediaTekDocuments.dal;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
    public partial class FrmAccueil : Form
    {
        private readonly Access access = Access.GetInstance();

        public FrmAccueil()
        {
            InitializeComponent();
            this.Shown += FrmAccueil_Shown;
        }

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

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            FrmMediatek frmDocuments = new FrmMediatek();
            frmDocuments.ShowDialog();
        }

        private void btnCommandes_Click(object sender, EventArgs e)
        {
            FrmSuiviCommandes frmCommandes = new FrmSuiviCommandes();
            frmCommandes.ShowDialog();
        }

        private void btnAbonnements_Click(object sender, EventArgs e)
        {
            FrmAbonnementRevues frmAbonnements = new FrmAbonnementRevues();
            frmAbonnements.ShowDialog();
        }

        /// <summary>
        /// Événement déclenché juste après l'affichage du formulaire FrmAccueil.
        /// Permet de lancer les vérifications une fois que l'UI est visible.
        /// </summary>
        private void FrmAccueil_Shown(object sender, EventArgs e)
        {
            VerifierAbonnementsExpirants();
        }

    }
}
