using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{

    public class Commande
    {
        [JsonProperty("idCommande")]
        public string Id { get; }
        public string Titre { get; }
        
        public string IdLivreDvd { get; } // "Livre" ou "DVD"
        public DateTime DateCommande { get; }
        public double Montant { get; }
        
        [JsonProperty("nbExemplaire")]
        public int NbExemplaires { get; }

        [JsonProperty("statut")]
        public string Suivi { get; }

        public Commande(string id, string titre, string idLivreDvd, DateTime dateCommande, double montant, int nbExemplaires, string suivi)
        {
            Id = id;
            Titre = titre;
            IdLivreDvd = idLivreDvd;
            DateCommande = dateCommande;
            Montant = montant;
            NbExemplaires = nbExemplaires;
            Suivi = suivi;
        }
    }
}
