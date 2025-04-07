using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur qui gère la logique de connexion de l'application.
    /// </summary>
    public static class FrmConnexionController
    {
        /// <summary>
        /// Authentifie un utilisateur avec ses identifiants.
        /// </summary>
        /// <param name="login">Identifiant</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns>True si connexion réussie, sinon false</returns>
        public static bool SeConnecter(string login, string mdp)
        {
            Utilisateur? utilisateur = Access.GetInstance().SeConnecter(login, mdp);
            if (utilisateur is not null)
            {
                Session.Utilisateur = utilisateur;
                return true;
            }
            return false;
        }
    }
}
