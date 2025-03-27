using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;

namespace MediaTekDocuments.controller
{
    public class FrmSuiviCommandesController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        private ToutesCommandes toutesCommandes;

        public FrmSuiviCommandesController()
        {
            access = Access.GetInstance();
            ChargerToutesCommandes();
        }

        /// <summary>
        /// Charge toutes les commandes depuis l'API et les stocke dans le modèle ToutesCommandes
        /// </summary>
        public void ChargerToutesCommandes()
        {
            List<Commande> commandes = access.GetToutesCommandes();
            toutesCommandes = new ToutesCommandes(commandes);
        }

        public bool CreerCommandeLivre(Commande commande)
        {
            return Access.GetInstance().CreerCommandeComplete(commande);
        }


        public List<Dvd> GetAllDvd()
        {
            return Access.GetInstance().GetAllDvd();
        }

        public List<Livre> GetAllLivres()
        {
            return Access.GetInstance().GetAllLivres();
        }

        /// <summary>
        /// Renvoie toutes les commandes stockées en mémoire
        /// </summary>
        public List<Commande> GetToutesCommandes()
        {
            return toutesCommandes.GetToutesCommandes();
        }

        /// <summary>
        /// Renvoie les commandes filtrées selon un mot-clé (titre, id, statut)
        /// </summary>

        public List<Commande> FiltrerCommandes(string motCle)
        {
            return toutesCommandes.Filtrer(motCle);
        }

        /// <summary>
        /// Supprime une commande (si elle n'est pas livrée ou réglée)
        /// </summary>
        public bool SupprimerCommande(string idCommande)
        {
            return access.SupprimerCommande(idCommande);
        }

        /// <summary>
        /// Modifie le statut d'une commande (en respectant les règles définies côté BDD)
        /// </summary>
        public bool ModifierStatutCommande(string idCommande, string nouvelIdSuivi)
        {
            return access.ModifierStatutCommande(idCommande, nouvelIdSuivi);
        }

        /// <summary>
        /// Crée une nouvelle commande complète
        /// </summary>
        public bool CreerNouvelleCommande(Commande commande)
        {
            return access.CreerCommandeComplete(commande);
        }
    }
}