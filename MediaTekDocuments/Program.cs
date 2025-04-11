using MediaTekDocuments.view;
using Microsoft.Extensions.Configuration;

namespace MediaTekDocuments
{
    /// <summary>
    /// Point d�entr�e principal de l�application.
    /// Configure l�environnement Windows Forms et lance le formulaire de connexion.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Configuration globale de l�application, charg�e depuis appsettings.json.
        /// </summary>
        public static IConfiguration Configuration { get; private set; } = null!;

        /// <summary>
        /// M�thode principale d�marrant l�application.
        /// Initialise la configuration Windows Forms et affiche le formulaire de connexion qui permet � l'utilisateur de s'identifier.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            /// <summary>
            /// Initialise la configuration de l�application en chargeant les param�tres depuis � appsettings.json �.
            /// </summary>
            /// <remarks>
            /// D�finit la racine de recherche sur le r�pertoire de l�ex�cutable (AppContext.BaseDirectory).  
            /// Charge le fichier JSON ou les variables d'environnements avec optional: true.
            /// Les variables d'env sont prioriaires sur le fichier config json.
            /// Active le rechargement automatique si le fichier est modifi� (reloadOnChange : true).  
            /// </remarks>
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Application.Run(new FrmConnexion());
        }
    }
}