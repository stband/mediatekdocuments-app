using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Controller pour la gestion des abonnements et des revues.
    /// Communique avec la couche d'accès aux données via la classe Access.
    /// </summary>
    public class FrmAbonnementRevuesController
    {
        private readonly Access access;

        /// <summary>
        /// Initialise une nouvelle instance de FrmAbonnementRevuesController.
        /// Obtient l'instance unique de la classe Access.
        /// </summary>
        public FrmAbonnementRevuesController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Vérifie si un abonnement actif existe pour une revue,
        /// en détectant un chevauchement entre la période d’un abonnement existant
        /// et la période que l’on souhaite créer.
        /// </summary>
        /// <param name="idRevue">Identifiant de la revue concernée.</param>
        /// <param name="dateDebut">Date de début de l’abonnement souhaité.</param>
        /// <param name="dateFin">Date de fin de l’abonnement souhaité.</param>
        /// <returns>True s’il y a un conflit de période, sinon false.</returns>
        public bool AbonnementActifExiste(string idRevue, DateTime dateDebut, DateTime dateFin)
        {
            return access.GetAllAbonnements().Any(abo => abo.IdRevue == idRevue && dateDebut <= abo.DateFinAbonnement && dateFin >= abo.DateCommande);
        }

        /// <summary>
        /// Crée un nouvel abonnement dans la base de données.
        /// </summary>
        /// <param name="abonnement">L'objet Abonnement à créer.</param>
        /// <returns>True si l'abonnement a été créé avec succès, sinon false.</returns>
        public bool CreerAbonnement(Abonnement abonnement)
        {
            return access.CreerAbonnement(abonnement);
        }

        /// <summary>
        /// Récupère la liste complète des abonnements depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets Abonnement.</returns>
        public List<Abonnement> GetAllAbonnements()
        {
            return access.GetAllAbonnements();
        }

        /// <summary>
        /// Récupère la liste complète des revues depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets Revue.</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// Supprime un abonnement de la base de données.
        /// </summary>
        /// <param name="idAbonnement">L'identifiant de l'abonnement à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon false.</returns>
        public bool SupprimerAbonnement(string idAbonnement)
        {
            return access.SupprimerAbonnement(idAbonnement);
        }
    }
}
