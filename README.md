# MediatekDocuments

Suite bureautique moderne pour gérer les documents d’une médiathèque – avec suivi des commandes/abonnements et contrôle d’accès par rôles.

Cette application permet de gérer les documents (livres, DVD, revues) d'une médiathèque.  

Elle a été initialement codée en C# sous Visual Studio 2019, **mais une migration vers .NET 8 et Visual Studio 2022 a été réalisée** afin de garantir la pérennité du projet.

En effet, Visual Studio 2019 est désormais obsolète et difficilement installable, ce qui posait un problème de compatibilité pour les environnements modernes.  
Cette migration permet donc :
- de **travailler avec les dernières versions d'outils (Visual Studio 2022 / .NET 8)**,
- de **profiter des performances améliorées** et du support à long terme du framework .NET 8,
- d'**assurer la compatibilité sur les postes actuels**.
- et d'**utiliser des dépendances et bibliothèques à jour**, contrairement à la version initiale qui s'appuyait sur des packages obsolètes.

C'est une application de bureau, prévue d'être installée sur plusieurs postes accédant à la même base de données.

L'application exploite une API REST pour accéder à la BDD MySQL.

## Présentation de l'application

### Authentification

Le module d’**Authentification** gère la sécurité d’accès à l’application :

- Une page de **login sécurisé** où l’utilisateur doit saisir ses identifiants pour se connecter. Les mots de passe sont traités de manière sécurisée directement depuis l'API (hashage + sel) afin de protéger les comptes.
- Une gestion des **rôles utilisateurs** (par ex. administrateur, administratif) qui détermine les droits d’accès en fonction des services de la médiathèque. Après connexion, l’interface et les actions disponibles s’adaptent en fonction du rôle (certaines fonctionnalités n’étant accessibles qu’aux administrateurs, par exemple).

### Accueil

L’onglet **Accueil** sert de tableau de bord et de point d’entrée vers les modules de l’application. Il offre :

- Une navigation centralisée permettant d’accéder rapidement aux sections _Commandes_, _Abonnements_ et _Documents_.
- L’affichage du **rôle de l’utilisateur** actuellement connecté (par ex. _administrateur_, _bibliothécaire_), afin de contextualiser ses permissions.
- Un **message d’alerte** automatique listant les abonnements aux revues arrivant à expiration prochainement, pour ne manquer aucun renouvellement important (si le rôle de la session le permet).

### Gestion des commandes

Ce module permet de gérer l’ensemble des commandes de livres et DVD pour la médiathèque. Il est organisé en plusieurs onglets pour faciliter le suivi :

- **Toutes les commandes :** vue globale listant l’intégralité des commandes, qu’elles concernent des livres ou des DVD. Cet onglet permet de consulter rapidement le statut de chaque commande et d’effectuer des recherches ou tris sur l’ensemble des commandes.
- **Commandes livres :** section dédiée aux commandes de livres uniquement. On y trouve une liste des livres disponibles à la commande, incluant la possibilité de filtrer ou rechercher certains articles. On peut commander un livre en le sélectionnant, en indiquant le prix d'achat et le nombre d'exemplaire commandé.
- **Commandes DVD :** section similaire à celle des livres, mais spécifique aux commandes de DVD.

### Gestion des abonnements

Ce module prend en charge le suivi des abonnements de la médiathèque à des revues et périodiques. Il comporte deux onglets complémentaires :

- **Abonnements :** vue d’ensemble de tous les abonnements en cours ou expirés. Cet écran liste chaque abonnement à une revue avec ses informations clés (titre de la revue, date de début et fin d’abonnement, coût, etc.). Les utilisateurs habilités peuvent prolongé la souscription d'un abonnement en le renouvellant directement.
- **Revues :** catalogue des titres de revues gérés par la médiathèque. Cet onglet présente la liste des périodiques (journaux, magazines, etc.) auxquels la médiathèque est abonnée ou susceptibles de l’être. Il permet de créer un nouvel abonnement à une revue.

### Gestion des documents

Ce module concerne le catalogue interne des documents (livres, DVD et autres médias) disponibles à la médiathèque. Ses principales fonctionnalités incluent :

- **Affichage** de l’ensemble des documents de la médiathèque sous forme de liste ou de grille, avec pour chaque document ses informations bibliographiques essentielles (titre, auteur/réalisateur, année, etc.).
- **Tri** dynamique des documents selon différents critères (ordre alphabétique, catégorie, etc.) afin de faciliter l’organisation et la consultation du catalogue.
- **Recherche** avancée permettant de filtrer les documents par mots-clés ou par champs spécifiques (par exemple rechercher un titre, un auteur, un genre). Cette fonctionnalité aide les utilisateurs à trouver rapidement un document particulier au sein de la collection.

Chaque document peut être consulté en détail via cette interface.

Le 4ème onglet de ce module permet d'ajouter des parutions à une revue, permettant ainsi d'indiquer qu'elles sont les parutions en stock, ajoutant un numéro spécifique pour chacune d'entre elles.

## L'API REST

L'accès à la BDD se fait à travers une API REST protégée par une authentification basique.<br>
Le code de l'API se trouve ici :

https://github.com/stband/mediatekdocuments-rest-api

avec toutes les explications pour l'utiliser (dans le README).

## Installation de l’application

### Option 1 — Installation rapide via exécutable

> Pour les utilisateurs finaux qui veulent simplement lancer l’appli.

1. Récupérez **`installers/MediatekDocuments.exe`** directement dans le dossier `installers/` du dépôt.

2. Double-cliquez et suivez l’assistant.  
   L’application est copiée dans `%ProgramFiles%\MediatekDocuments`.

3. Ouvrez le fichier `%ProgramData%\MediatekDocuments\appsettings.json` et renseignez :
   ```json
   {
     "Api": {
       "BaseUrl": "http://mon.serveur/mediatek-api/public/",
       "Username": "monUtilisateur",
       "Password": "monMotDePasse"
     }
   }
   ```
> Il est aussi possible d'utiliser directement des variables d'environnements pour plus de sécurité.

Les instructions pour installer et lancer l’API REST sont disponibles ici : https://github.com/stband/mediatekdocuments-rest-api

4. Lancez MediatekDocuments depuis le menu Démarrer et c’est prêt !

### Option 2 — Installation développeur (compilation depuis la source)

### Prérequis

- .NET SDK 8.0 (LTS)  
- Visual Studio 2022 (Community ou Enterprise)
- WAMP/XAMPP avec MySQL en service (ou autre solution d'hébergement pour API)
- Git

### Cloner le dépôt

Cloner le dépôt depuis le répertoire de destination
```bash
git clone https://github.com/stband/mediatekdocuments-app.git
cd mediatekdocuments-app
```
### Installer et configurer l'API REST

- L'API REST est disponible ici : (https://github.com/stband/mediatekdocuments-rest-api).
- Elle contient aussi la base de données et les explications pour la configurer (dans le README correspondant).

### Configurer l'authentification à l'API REST depuis l'application

Voir [la configuration de l'authentification à l’API](docs/configuration-auth-api.md) pour plus d'infos.

### Configurer et lancer l'application

Dans le CLI (powershell/cmd)

1. Restaurer tous les packages NuGet

```bash
dotnet restore
```

2. Compiler le projet
dotnet build

```bash
dotnet build
```

3. Lancer l’application WinForms

```bash
dotnet run --project MediaTekDocuments
```

## Origines du projet

Cette application est un **fork et une évolution** du dépôt pédagogique  
[CNED-SLAM / MediaTekDocuments](https://github.com/CNED-SLAM/MediaTekDocuments),  
initialement développé pour illustrer les notions de gestion documentaire au sein du
BTS SIO option SLAM.  
La présente version étend ces bases (installeur Windows, API REST PHP et hébergement, nouvelles fonctionnalités, mise à
jour .NET 8, etc.) tout en concervant la base du projet.

Réalisés et maintenus par **<stband>** ([@stband](https://github.com/stband).

## Licence

Ce projet est distribué sous la licence [MIT](LICENSE).

