using Microsoft.Extensions.Configuration;

[assembly: DoNotParallelize] // Lancer les tests de manière séquentielle pour éviter les problèmes de dépassement de quota (rate-limit) lors des appels à l'API

namespace InseeApiSirene.Test
{
    /// <summary>
    /// Classe d'aide pour les tests unitaires, notamment pour la récupération de la clé d'API
    /// </summary>
    /// <remarks>Utilisation des User-Secrets
    /// Pour installer votre clé :
    /// > dotnet user-secrets init
    /// > dotnet user-secrets set "InseeApiKey" "votre_clé_api_personnelle"
    /// </remarks>
    public static class MsTestConfig
    {
        private static Int32 CompteurAppels = 0;

        /// <summary>
        /// Lecture de la clé d'API, avec gestion d'erreur si la clé n'est pas configurée
        /// </summary>
        /// <returns>Clé API</returns>
        /// <exception cref="InvalidOperationException">Exception levée si la clé est manquante</exception>
        public static String GetApiKey()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Informations>()     // Va lire les "user-secrets" (utilisé en local)
                .AddEnvironmentVariables()          // Ajoute les variables d'environnement (utilisé par GitHub Actions)
                .Build();
            String? apiKey = config["InseeApiKey"];
            return apiKey ?? throw new InvalidOperationException("Vous devez configurer votre clé API dans les 'User-Secrets' (voir documentation pour plus d'infos).");
        }

        /// <summary>
        /// Respecter les limites du nombre d'appels à l'API pour éviter les erreurs de dépassement de quota (rate-limit), en mettant en pause le thread si la limite est dépassée
        /// </summary>
        public static void RespecterRateLimit()
        {
            CompteurAppels++;
            if (CompteurAppels > SireneApi.LIMITE_NB_APPEL_PAR_MINUTE)
            {
                Thread.Sleep(60000); // Attente d'une minute pour réinitialiser le compteur d'appels
                CompteurAppels = 1;
            }
        }
    }
}
