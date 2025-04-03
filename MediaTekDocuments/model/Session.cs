namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe statique représentant la session utilisateur actuelle.
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// Objet de l'utilisateur actuellement connecté.
        /// </summary>
        public static Utilisateur Utilisateur { get; set; }
    }
}
