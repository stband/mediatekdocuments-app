using Newtonsoft.Json;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Représente un abonnement à une revue.
    /// </summary>
    public class Abonnement
    {
        [JsonProperty("idAbonnement")]
        public string IdAbonnement { get; set; } = null!;

        [JsonProperty("idRevue")]
        public string IdRevue { get; set; } = null!;

        [JsonProperty("titreRevue")]
        public string TitreRevue { get; set; } = null!;

        [JsonProperty("dateCommande")]
        public DateTime DateCommande { get; set; }

        [JsonProperty("dateFinAbonnement")]
        public DateTime DateFinAbonnement { get; set; }

        [JsonProperty("montant")]
        public double Montant { get; set; }

        /// <summary>
        /// Instancie un nouvel abonnement avec toutes ses informations.
        /// </summary>
        /// <param name="idAbonnement">Identifiant unique de l'abonnement.</param>
        /// <param name="idRevue">Identifiant de la revue.</param>
        /// <param name="titreRevue">Titre de la revue.</param>
        /// <param name="dateCommande">Date de commande.</param>
        /// <param name="dateFinAbonnement">Date de fin d'abonnement.</param>
        /// <param name="montant">Montant de l'abonnement.</param>
        public Abonnement(
            string id,
            string idRevue,
            string titreRevue,
            DateTime dateCommande,
            DateTime dateFinAbonnement,
            double montant)
        {
            IdAbonnement = id;
            IdRevue = idRevue;
            TitreRevue = titreRevue;
            DateCommande = dateCommande;
            DateFinAbonnement = dateFinAbonnement;
            Montant = montant;
        }

        public Abonnement() { }

        /// <summary>
        /// Sélectionne et formate la liste des abonnements expirant dans les 30 prochains jours.
        /// </summary>
        /// <param name="abonnements">Liste complète des abonnements.</param>
        /// <returns>
        /// Liste de chaînes de la forme "- TitreRevue (expire dans N jours)".
        /// </returns>
        public static List<string> GetAbonnementsExpirantDans30Jours(List<Abonnement> abonnements)
        {
            DateTime dateLimite = DateTime.Now.AddDays(30);
            
            return abonnements
                .Where(ab => ab.DateFinAbonnement <= dateLimite && ab.DateFinAbonnement >= DateTime.Now)
                .Select(ab => $"- {ab.TitreRevue} (expire dans {(ab.DateFinAbonnement - DateTime.Now).Days} jours)")
                .ToList();
        }
    }
}
