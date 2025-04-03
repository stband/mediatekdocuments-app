using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    public class FrmConnexionController
    {
        /// <summary>
        /// Vérifie les informations de l'utilisateur avant de l'authentifier.
        /// </summary>
        /// <param name="login">Identifiant</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns>True si connexion réussie, sinon false</returns>
        public bool SeConnecter(string login, string mdp)
        {
            Utilisateur utilisateur = Access.GetInstance().SeConnecter(login, mdp);
            if (utilisateur != null)
            {
                Session.Utilisateur = utilisateur;
                return true;
            }
            return false;
        }
    }
}
