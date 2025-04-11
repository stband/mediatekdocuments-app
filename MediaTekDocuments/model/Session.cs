namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe statique représentant la session utilisateur actuelle.
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// Objet de l'utilisateur actuellement connecté.
        /// Initialisé par le contrôleur de connexion avant tout usage.
        /// </summary>
        public static Utilisateur Utilisateur { get; set; } = null!;
    }
}
