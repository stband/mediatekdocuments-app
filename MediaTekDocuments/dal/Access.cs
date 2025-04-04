using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NLog;

namespace MediaTekDocuments.dal
{

    /// <summary>
    /// Classe d'accès aux données. 
    /// Gère la communication avec l'API REST pour réaliser les opérations CRUD sur les entités.
    /// </summary>
    public class Access
    {
        /// <summary>
        /// Objet pour gérer les logs via Nlog.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Adresse de l'API REST.
        /// </summary>
        private static readonly string uriApi = "http://localhost/rest_mediatekdocuments/";

        /// <summary>
        /// Instance unique de la classe Access (singleton).
        /// </summary>
        private static Access instance = null;

        /// <summary>
        /// Instance de ApiRest utilisée pour envoyer les requêtes et récupérer les réponses de l'API.
        /// </summary>
        private readonly ApiRest api = null;

        /// <summary>
        /// Constante représentant la méthode HTTP GET.
        /// </summary>
        private const string GET = "GET";

        /// <summary>
        /// Constante représentant la méthode HTTP POST.
        /// </summary>
        private const string POST = "POST";

        /// <summary>
        /// Constante représentant la méthode HTTP PUT.
        /// </summary>
        private const string PUT = "PUT";

        /// <summary>
        /// Constante représentant la méthode HTTP DELETE.
        /// </summary>
        private const string DELETE = "DELETE";


        /// <summary>
        /// Constructeur privé afin d'implémenter le pattern singleton.
        /// Initialise l'accès à l'API avec une chaîne d'authentification.
        /// </summary>
        private Access()
        {
            logger.Info("Constructeur Access private appelé, instance créée");
            String authenticationString;
            try
            {
                authenticationString = "admin:adminpwd";
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                logger.Error(e, "Initialisation de l'API REST échouée");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Retourne l'instance unique de la classe Access.
        /// Créée lors du premier appel (singleton).
        /// </summary>
        /// <returns>L'instance unique d'Access.</returns>
        public static Access GetInstance()
        {
            if(instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Genre convertis en liste d'objets Categorie.</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre", null);
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Rayon convertis en liste d'objets Categorie.</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon", null);
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Public convertis en liste d'objets Categorie.</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public", null);
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne tous les livres à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Livre.</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre", null);
            return lesLivres;
        }

        /// <summary>
        /// Retourne tous les DVD à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Dvd.</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd", null);
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Revue.</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue", null);
            return lesRevues;
        }

        /// <summary>
        /// Retourne les exemplaires d'une revue spécifiée.
        /// </summary>
        /// <param name="idDocument">Identifiant de la revue concernée.</param>
        /// <returns>Liste d'objets Exemplaire.</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            String jsonIdDocument = convertToJson("id", idDocument);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument, null);
            return lesExemplaires;
        }

        /// <summary>
        /// Insère un nouvel exemplaire dans la base de données.
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire à insérer.</param>
        /// <returns>True si l'insertion est réussie, sinon false.</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire", "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Création d'exemplaire échouée");
            }
            return false;
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée dans l'url</param>
        /// <param name="parametres">paramètres à envoyer dans le body, au format "chp1=val1&chp2=val2&..."</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T> (String methode, String message, String parametres)
        {
            // trans
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message, parametres);
                // extraction du code retourné
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    // dans le cas du GET (select), récupération de la liste d'objets
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        // construction de la liste d'objets à partir du retour de l'api
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    logger.Warn("API a répondu code={0}, message={1}", code, retour["message"]);
                }
            }catch(Exception e)
            {
                logger.Error(e, "Exception dans TraitementRecup<{0}>", typeof(T).Name);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Convertit un couple nom/valeur en une chaîne JSON.
        /// </summary>
        /// <param name="nom">Le nom de la propriété.</param>
        /// <param name="valeur">La valeur associée.</param>
        /// <returns>Une chaîne JSON représentant le couple nom/valeur.</returns>
        private String convertToJson(Object nom, Object valeur)
        {
            Dictionary<Object, Object> dictionary = new Dictionary<Object, Object>();
            dictionary.Add(nom, valeur);
            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Convertisseur personnalisé de dates pour le sérialiseur JSON.
        /// Utilise le format "yyyy-MM-dd".
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        /// <summary>
        /// Retourne toutes les commandes à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Commande.</returns>
        public List<Commande> GetToutesCommandes()
        {
            return TraitementRecup<Commande>(GET, "commande", null);
        }

        /// <summary>
        /// Retourne les commandes associées à un livre ou DVD en fonction de son identifiant.
        /// </summary>
        /// <param name="idLivreDvd">L'identifiant du livre ou DVD.</param>
        /// <returns>Liste d'objets Commande.</returns>
        public List<Commande> GetCommandesById(string idLivreDvd)
        {
            string json = convertToJson("idLivreDvd", idLivreDvd);
            return TraitementRecup<Commande>(GET, "commandeparid/" + json, null);
        }

        public Utilisateur SeConnecter(string login, string mdp)
        {
            var donnees = new Dictionary<string, string>
            {
                { "login", login },
                { "mdp", mdp }
            };

            string json = JsonConvert.SerializeObject(donnees);

            try
            {
                JObject retour = api.RecupDistant(POST, "connexion", "champs=" + json);

                if (retour["code"].ToString() == "200")
                {
                    string resultString = JsonConvert.SerializeObject(retour["result"]);
                    Utilisateur utilisateur = JsonConvert.DeserializeObject<Utilisateur>(resultString);
                    return utilisateur;
                }
                else
                {
                    logger.Warn("Erreur de connexion : " + retour["message"]);
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Erreur API");
                return null;
            }
        }

        /// <summary>
        /// Crée une commande complète en enregistrant les informations de commande.
        /// </summary>
        /// <param name="commande">L'objet Commande à créer.</param>
        /// <returns>True si l'insertion a réussi, sinon false.</returns>
        public bool CreerCommandeComplete(Commande commande)
        {
            var donnees = new
            {
                idLivreDvd = commande.IdLivreDvd,
                nbExemplaire = commande.NbExemplaires,
                montant = commande.Montant,
                dateCommande = commande.DateCommande.ToString("yyyy-MM-dd")
            };

            string json = JsonConvert.SerializeObject(donnees, new CustomDateTimeConverter());
            try
            {
                var result = TraitementRecup<Commande>(POST, "commandetotale", "champs=" + json);
                return result != null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Échec création de commande");
                return false;
            }
        }

        /// <summary>
        /// Modifie le statut d'une commande en mettant à jour son identifiant de suivi.
        /// </summary>
        /// <param name="idCommande">L'identifiant de la commande à modifier.</param>
        /// <param name="nouvelIdSuivi">Le nouvel identifiant de suivi à affecter.</param>
        /// <returns>True si la mise à jour a réussi, sinon false.</returns>
        public bool ModifierStatutCommande(string idCommande, string nouvelIdSuivi)
        {
            var data = new Dictionary<string, string>
            {
                { "idSuivi", nouvelIdSuivi }
            };
            string json = JsonConvert.SerializeObject(data);
            try
            {
                var result = TraitementRecup<Commande>(PUT, "commandedocument", "id=" + idCommande + "&champs=" + json);
                return result != null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Échec mise à jour du statut commande : {0}", idCommande);
                return false;
            }
        }

        /// <summary>
        /// Supprime une commande en fonction de son identifiant.
        /// </summary>
        /// <param name="idCommande">L'identifiant de la commande à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon false.</returns>
        public bool SupprimerCommande(string idCommande)
        {
            string json = convertToJson("id", idCommande);
            try
            {
                var result = TraitementRecup<Commande>(DELETE, "commande/" + json, null);
                return result != null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Échec suppression commande {0}", idCommande);
                return false;
            }
        }

        /// <summary>
        /// Retourne tous les abonnements à partir de la base de données.
        /// </summary>
        /// <returns>Liste d'objets Abonnement</returns>
        public List<Abonnement> GetAllAbonnements()
        {
            List<Abonnement> lesAbonnements = TraitementRecup<Abonnement>(GET, "abonnement", null);
            return lesAbonnements;
        }

        /// <summary>
        /// Crée un nouvel abonnement dans la base de données.
        /// </summary>
        /// <param name="abonnement">L'objet Abonnement à créer.</param>
        /// <returns>True si l'abonnement a été créé avec succès, sinon false.</returns>
        public bool CreerAbonnement(Abonnement abonnement)
        {
            var donnees = new
            {
                // idCommande est géré par l'API.
                idRevue = abonnement.IdRevue,
                dateCommande = abonnement.DateCommande.ToString("yyyy-MM-dd"),
                dateFinAbonnement = abonnement.DateFinAbonnement.ToString("yyyy-MM-dd"),
                montant = abonnement.Montant,
                nbExemplaire = 1,
            };

            string json = JsonConvert.SerializeObject(donnees, new CustomDateTimeConverter());
            try
            {
                var result = TraitementRecup<Abonnement>(POST, "abonnement", "champs=" + json);
                return (result != null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Échec création abonnement pour la revue : {0}", abonnement.IdRevue);
                return false;
            }
        }

        /// <summary>
        /// Supprime un abonnement en fonction de son identifiant.
        /// </summary>
        /// <param name="idAbonnement">L'identifiant de l'abonnement à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon false.</returns>
        public bool SupprimerAbonnement(string idAbonnement)
        {
            string json = convertToJson("id", idAbonnement);
            try
            {
                var result = TraitementRecup<Abonnement>(DELETE, "abonnement/" + json, null);
                return (result != null);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Échec suppression abonnement : {0}", idAbonnement);
                return false;
            }
        }
    }
}