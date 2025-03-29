using Newtonsoft.Json;
using System;

namespace MediaTekDocuments.model
{
    public class Abonnement
    {
        [JsonProperty("idAbonnement")]
        public string IdAbonnement { get; set; }

        [JsonProperty("idRevue")]
        public string IdRevue { get; set; }

        [JsonProperty("titreRevue")]
        public string TitreRevue { get; set; }

        [JsonProperty("dateCommande")]
        public DateTime DateCommande { get; set; }

        [JsonProperty("dateFinAbonnement")]
        public DateTime DateFinAbonnement { get; set; }

        [JsonProperty("montant")]
        public double Montant { get; set; }

        public Abonnement(string id, string idRevue, string titreRevue, DateTime dateCommande, DateTime dateFinAbonnement, double montant)
        {
            IdAbonnement = id;
            IdRevue = idRevue;
            TitreRevue = titreRevue;
            DateCommande = dateCommande;
            DateFinAbonnement = dateFinAbonnement;
            Montant = montant;
        }

        public Abonnement() { }

        // filtre les abonnements qui expirent bientôt et retourne une liste de chaînes de caractères à afficher.
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
