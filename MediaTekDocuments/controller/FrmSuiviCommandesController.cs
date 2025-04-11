using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur pour la gestion des commandes de livres et DVD : chargement, filtrage, création, suppression, etc.
    /// </summary>
    public class FrmSuiviCommandesController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// Obtient l'instance unique de la classe Access.
        /// </summary>
        private readonly Access access = Access.GetInstance();

        /// <summary>
        /// Récupère la liste complète des commandes depuis la base de données.
        /// Filtre les commandes pour exclure les commandes liées aux revues.
        /// Les commandes liées aux revues sont des abonnements, et elles sont
        /// gérées dans FrmAbonnementRevues.
        /// </summary>
        /// <returns>Liste filtrée de <see cref="Commande"/> sans les commandes liées aux revues.</returns>
        public List<Commande> GetToutesCommandes()
        {
            var commandes = access.GetToutesCommandes();

            // Exclut les commandes dont l’ID Livre/DVD commence par '1' (les revues).
            var commandesFiltrees = commandes
                .Where(c => !c.IdLivreDvd.ToString().StartsWith('1'))
                .ToList();

            return commandesFiltrees;
        }

        /// <summary>
        /// Crée une commande de livre via l’API.
        /// </summary>
        /// <param name="commande">Objet contenant les infos de la commande.</param>
        /// <returns>True si la création a réussi, sinon false.</returns>
        public bool CreerCommandeLivre(Commande commande)
        {
            return access.CreerCommandeComplete(commande);
        }

        /// <summary>
        /// Récupère tous les DVD via l’API.
        /// </summary>
        /// <returns>Listz de <see cref="Dvd"/>.</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// Récupère tous les Livres via l’API.
        /// </summary>
        /// <returns>Listz de <see cref="Livre"/>.</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// Supprime une commande (si elle n'est pas livrée ou réglée).
        /// </summary>
        /// <param name="idCommande">Identifiant de la commande à supprimer.</param>
        /// <returns>True si suppression réussie, sinon false.</returns>
        public bool SupprimerCommande(string idCommande)
        {
            return access.SupprimerCommande(idCommande);
        }

        /// <summary>
        /// Modifie le statut d'une commande (en respectant les règles définies côté BDD).
        /// </summary>
        /// <param name="idCommande">Identifiant de la commande.</param>
        /// <param name="nouvelIdSuivi">Nouveau statut du suivi.</param>
        /// <returns>True si modification réussie, sinon false.</returns>
        public bool ModifierStatutCommande(string idCommande, string nouvelIdSuivi)
        {
            return access.ModifierStatutCommande(idCommande, nouvelIdSuivi);
        }

        /// <summary>
        /// Crée une nouvelle commande complète (Livre ou DVD).
        /// </summary>
        /// <param name="commande">Objet Commande à créer.</param>
        /// <returns>True si création réussie, sinon false.</returns>
        public bool CreerNouvelleCommande(Commande commande)
        {
            return access.CreerCommandeComplete(commande);
        }
    }
}