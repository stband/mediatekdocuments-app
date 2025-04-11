# Configuration de l'authentification à l'API

> Externalisation des identifiants API via un fichier `appsettings.json` ou des **variables d’environnement**, pour éviter de stocker des mots de passe en dur dans le code.

---

## Deux méthodes pour fournir les identifiants

Vous avez **deux façons de configurer l'authentification à l'API** :

> Note : l’authentification est actuellement basique (`admin:adminpwd`) pour faciliter le développement local. Mais les deux méthodes suivantes, vous permettent de **remplacer facilement ce mot de passe plus tard**, sans modifier le code. Vous pourrez connecter l’application à votre propre instance de l’API avec des identifiants personnalisés.

Pour commencer, vous devez vérifier que vous avez bien installé les packages NuGet nécessaires :

```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
```

*Vous pouvez copier et coller ces commandes dans un terminal à la racine du projet pour les installer ou vérifier l'installation.*

### Méthode 1 : Fichier `appsettings.json`

C’est la méthode la plus simple en développement.

Un fichier **modèle** (`appsettings.json.template`) est présent dans le projet. Il contient uniquement la structure et des valeurs neutres :

   ```json
   {
     "ApiSettings": {
       "UriApi": "http://localhost/rest_mediatekdocuments/",
       "Authentication": ""
     }
   }
   ```

2. Copiez ce fichier et renommez-le :

```powershell
cp MediaTekDocuments/appsettings.json.template MediaTekDocuments/appsettings.json
```

3. Remplissez vos identifiants dans le fichier `appsettings.json` :

```json
{
  "ApiSettings": {
    "UriApi": "http://localhost/rest_mediatekdocuments/",
    "Authentication": "admin:adminpwd"
  }
}
```

4. Ce fichier est **ignoré par Git** (`.gitignore`), donc vous ne risquez pas de pousser vos credentials accidentellement.

### Méthode 2 : Variables d’environnement (recommandée pour la production)

1. Au lieu d’écrire dans un fichier, définissez les variables d’environnement suivantes :

Sous Linux ou macOS :

```bash
export ApiSettings__UriApi="https://api.monsite.com/"
export ApiSettings__Authentication="monServiceUser:monMotDePasse"
```

Sous Windows (PowerShell pas CMD) :

```powershell
$env:ApiSettings__UriApi = "https://api.monsite.com/"
$env:ApiSettings__Authentication = "monServiceUser:monMotDePasse"
```

Ces variables remplacent automatiquement les valeurs du fichier `appsettings.json` si celui-ci est présent. Donc elles sont prioritaires.

### Quelle méthode utiliser ?

- La méthode 1 est idéale dans en environnement local, en développement.
- La méthode 2 est idéale dans un environnement plus sécurisé, en production, tests ou automatisation.
  - C'est aussi recommandé si vous êtes plusieurs à travailler sur le même système, et que vous avez plusieurs session différentes, car il est possible de créer des variables d'environnements locale à une session, ce qui est utile.

