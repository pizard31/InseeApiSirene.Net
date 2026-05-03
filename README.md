# Wrapper .Net de l'API Sirene de l'Insee

[![NuGet Version](https://img.shields.io/nuget/v/InseeApiSirene)](https://www.nuget.org/packages/InseeApiSirene/) [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Wrapper de l'API Sirene de l'[Insee](https://portail-api.insee.fr/) pour les développements sur plateforme .Net.

L'API Sirene donne accès aux informations concernant les entreprises et les établissements enregistrés au répertoire interadministratif Sirene depuis sa création en 1973, y compris les unités fermées.

La recherche peut être unitaire, multicritère, phonétique et porter sur les données courantes et historisées.
- Les services actuellement disponibles interrogent :
    - les **Unités légales** (SIREN)
    - les **Établissements** (SIRET)
- Le service **Informations** permet de connaître les dates de dernières mises à jour.
- Le service **Liens de succession** informe sur les prédécesseurs et les successeurs des établissements.

Pour plus d'infos, consulter les [Informations générales](https://portail-api.insee.fr/catalog/api/2ba0e549-5587-3ef1-9082-99cd865de66f)
et la [Documentation technique](https://portail-api.insee.fr/catalog/api/2ba0e549-5587-3ef1-9082-99cd865de66f/doc/).

## Version de l'API

Utilisation de l'Api Sirene **3.11** qui est la version de référence depuis le **30 avril 2024**.

Le répertoire Sirene est en version **4**.

Pour plus d'infos, consulter les [Évolutions](https://portail-api.insee.fr/catalog/api/2ba0e549-5587-3ef1-9082-99cd865de66f/doc?page=41bc4d65-fca4-436d-bc4d-65fca4136dcd#en-bref)

## Support

La bibliothèque InseeApiSirene.Net est compatible avec les environnements suivants :

- .NET Framework 4.6.1+
- .NET Core 2.0+ 
- .NET 5.0+
- Mono 5.4+
- Xamarin.iOS 10.14+
- Xamarin.Android 8.0+
- UWP 10.0.16299+

## Installation

Le package Insee.Net est disponible sur [NuGet.org](https://www.nuget.org/packages/InseeNet/).

```sh
dotnet add package InseeApiSirene
```

## Clé d'intégration API

Un compte est nécéssaire pour avoir accès à une **clé d'API** (*X-INSEE-Api-Key-Integration*) pour se connecter à l'API Sirene.

Pour obtenir cette clé, vous devrez créer un compte (gratuit) sur le portail API de l'Insee,
puis créer une application (en mode "simple")
et enfin souscrire à l'API Sirene pour l'application.

Pour plus d'infos, consulter le [guide pas à pas](https://static.insee.fr/api-sirene/Insee_API_publique_modalites_connexion.pdf)
ou la [documentation](https://portail-api.insee.fr/catalog/api/2ba0e549-5587-3ef1-9082-99cd865de66f/doc?page=85c5657d-b1a1-4466-8565-7db1a194667b).

## Limites d'utilisation

L'usage de l'API Sirene est soumis à une limite de **30 interrogations par minute**.

*L'Insee se réserve le droit de changer cette limite en cas de nécessité.*

Pour plus d'infos, consulter les [Conditions générales d'utilisation de l'API Sirene](https://portail-api.insee.fr/catalog/api/2ba0e549-5587-3ef1-9082-99cd865de66f/doc?page=3e63ae9c-5611-4e55-a3ae-9c5611de55f9)
et la [Charte d'utilisations du catalogue des API de l'Insee](https://portail-api.insee.fr/).

## Utilisation

### Configuration de l'instance

```csharp
using (var oSireneApi = new SireneApi("01234567-89ab-cdef-0123-456789abcdef")) // A remplacer par votre propre clé d'API
{
    oSireneApi.Timeout = 30; // Timeout en secondes des appels à l'API
    oSireneApi.CompressionGzip = true; // Active la compression Gzip lors des appels à l'API (réduction de la taille des données échangées), la décompression sera éffectuée localement (augmentation des ressources).

    var oReponseSynchrone = oSireneApi.Fonction();
    var oReponseSynchroneEnCsv = oSireneApi.FonctionCsv();
    var oReponseAsynchrone = await oSireneApi.FonctionAsync();
    var oReponseAsynchroneEnCsv = await oSireneApi.FonctionCsvAsync();
}
```
### Paramètres globaux de l'instance

Initialisable à l'instanciation de l'API ou à tout moment via les propriétés de l'instance.
- MasquerValeurNulles : Détermine si l'API doit renvoyer les champs dont les données sont nulles.
- Timeout : Timeout en secondes des appels à l'API.
- CompressionGzip = Active la compression Gzip lors des appels à l'API (réduction de la taille des données échangées), la décompression sera éffectuée localement (augmentation des ressources).

## Fonctions proposées

Chaque fonction avec appel à l'API est disponible en version Asynchrone (await/async) ou Synchrone (classique).

### Manipulation des numéros SIREN et SIRET

Fonctions bonus de validation et formatage des numéros SIRET et SIREN.
```csharp
// Numéro SIREN
Siren.Nettoyer("326 094 47[1]"); // 326094471
Siren.Tester("326 094 47[1]"); // True
Siren.Formater("326 094 47[1]"); // 326 094 471

// Numéro SIRET
Siret.Tester("326 094 47[1] 00035"); // True
Siret.Nettoyer("326 094 47[1] 00035"); // 32609447100035
Siret.Formater("326 094 47[1] 00035"); // 326 094 471 00035
Siret.ExtraireSiren("326 094 47[1] 00035", out String nic); // 326094471 (et nic = 00035)
```

### Recherches unitaires

Trouver les informations d'une unité légale à partir de son numéro SIREN :
```csharp
using (var oSireneApi = new SireneApi("01234567-89ab-cdef-0123-456789abcdef")) // A remplacer par votre propre clé d'API
{
    var oUniteLegale = oSireneApi.UniteLegale("123 456 789");
}
```

Trouver les informations d'un établissement à partir de son numéro SIRET :
```csharp
using (var oSireneApi = new SireneApi("01234567-89ab-cdef-0123-456789abcdef")) // A remplacer par votre propre clé d'API
{
    var oEtablissement = oSireneApi.Etablissement("123 456 789 00001");
}
```

### Recherches simplifiées

Trouver les unités légales ou établissements à partir de la raison sociale (et ses historiques) :
```csharp
using (var oSireneApi = new SireneApi("01234567-89ab-cdef-0123-456789abcdef")) // A remplacer par votre propre clé d'API
{
    // Recherche des unités légales
    var oUnitesLegales = oSireneApi.UnitesLegales("Raison Sociale");

    // Recherche des établissements
    var oEtablissements = oSireneApi.Etablissements("Raison Sociale");
}
```

### Recherches multicritères

Possibilité de construire une requête complexe via l'objet 'RequeteMultiCriteres' pour des établissements ou des unités légales.
- en combinant plusieurs critères de recherche (voir la [documentation](https://portail-api.insee.fr/catalog/api/2ba0e549-5587-3ef1-9082-99cd865de66f/doc?page=0ec89386-318b-4dc6-8893-86318bbdc64a#pr%C3%A9sentation) la syntaxe détaillée).
- en définissant les élèments à retourner dans le résultat
- en définissant un ordre de tri
- en gérant la pagination des résultats

Il est possible d'obtenir les données structurées (Objets) ou brutes (CSV).

## Stack Technique

- .NET Standard 2.0
- C# 7.3
- Visual Studio 2026

## Logs

Par défaut les logs sortent sur "System.Diagnostics.Debug".

Possibilité d'ajouter des traces des opérations via un Logger personnalisé

```csharp
// Configuration des logs
InseeApiSirene.Logger = new ConsoleLogger();

/// <summary>
/// Classe de log pour une sortie sur la Console
/// </summary>
public class ConsoleLogger : ILogger
{
    public void Debug(String message, Exception exception = null) => Console.WriteLine($"[DEBUG] {message}");
    public void Info(String message, Exception exception = null) => Console.WriteLine($"[INFO] {message}");
    public void Warning(String message, Exception exception = null) => Console.WriteLine($"[WARNING] {message}");
    public void Error(String message, Exception exception = null) => Console.WriteLine($"[ERROR] {message}");
    public void Fatal(String message, Exception exception = null) => Console.WriteLine($"[FATAL] {message}");

    public Boolean IsDebugEnabled() => true;
    public Boolean IsInfoEnabled() => true;
    public Boolean IsWarningEnabled() => true;
    public Boolean IsErrorEnabled() => true;
    public Boolean IsFatalEnabled() => true;
}
```

## Test unitaires

Tests unitaires avec MSTest (projet InseeApiSirene.Tests).

Unitialiser votre clé API pour les tests unitaires (à faire une seule fois dans le dossier *InseeApiSirene.Test*) :
```powershell
dotnet user-secrets init
dotnet user-secrets set "InseeApiKey" "votre_clé_api_personnelle"
```

## License

InseeNet est distribué sous [licence MIT](LICENSE).