using Microsoft.Extensions.Configuration;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)] // Execution des tests en parallèle (plus rapide)

namespace InseeApiSirene.Test
{
    /// <summary>
    /// Classe d'aide pour les tests unitaires, notamment pour la récupération de la clé d'API dans les secrets utilisateur
    /// </summary>
    /// <remarks>Utilisation des User Secrets
    /// Pour installer votre clé :
    /// > dotnet user-secrets init
    /// > dotnet user-secrets set "InseeApiKey" "votre_clé_api_personnelle"
    /// </remarks>
    public static class MsTestConfig
    {
        public static String GetApiKey()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Informations>()
                .Build();
            String? apiKey = config["InseeApiKey"];
            return apiKey ?? throw new InvalidOperationException("Vous devez configurer votre clé API dans les secrets utilisateur (voir documentation pour plus d'infos).");
        }
    }
}
