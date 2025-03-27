using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe contenant une collection de commandes et des méthodes de manipulation.
    /// </summary>
    public class ToutesCommandes
    {
        private List<Commande> commandes;

        public ToutesCommandes(List<Commande> commandes)
        {
            this.commandes = commandes;
        }

        /// <summary>
        /// Retourne une liste filtrée de commandes selon un mot-clé.
        /// </summary>
        public List<Commande> Filtrer(string motCle)
        {
            if (string.IsNullOrWhiteSpace(motCle) || motCle == "Rechercher")
                return commandes;

            motCle = motCle.ToLower();
            return commandes.Where(c =>
        (!string.IsNullOrEmpty(c.Id) && c.Id.ToLower().Contains(motCle)) ||
        (!string.IsNullOrEmpty(c.Titre) && c.Titre.ToLower().Contains(motCle)) ||
        (!string.IsNullOrEmpty(c.Suivi) && c.Suivi.ToLower().Contains(motCle))
        ).ToList();
        }

        /// <summary>
        /// Retourne la liste complète de toutes les commandes.
        /// </summary>
        public List<Commande> GetToutesCommandes()
        {
            return commandes;
        }
    }
}
