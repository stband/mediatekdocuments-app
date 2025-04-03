using MediaTekDocuments.view;
using System;
using System.Windows.Forms;

namespace MediaTekDocuments
{
    /// <summary>
    /// Point d�entr�e principal de l�application.
    /// Configure l�environnement Windows Forms et lance le formulaire de connexion.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// M�thode principale d�marrant l�application.
        /// Initialise la configuration Windows Forms et affiche le formulaire de connexion qui permet � l'utilisateur de s'identifier.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmConnexion());
        }
    }
}