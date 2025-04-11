namespace MediaTekDocuments.model
{
    /// <summary>
    /// Représente un utilisateur de l’application.
    /// Contient les informations d’identité et le service associé.
    /// </summary>
    public class Utilisateur
    {
        /// <summary>
        /// Identifiant unique de l’utilisateur.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom complet de l’utilisateur.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Login (identifiant) utilisé pour la connexion.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Service de l'utilisateur (lié à ses droits).
        /// </summary>
        public string Service { get; set; }

        public Utilisateur(int id, string nom, string login, string service)
        {
            Id = id;
            Nom = nom;
            Login = login;
            Service = service;
        }
    }
}