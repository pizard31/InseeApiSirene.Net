using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des collections de données disponibles dans l'API Sirene
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CollectionEnum
    {
        /// <summary>
        /// Unités légales
        /// </summary>
        [JsonStringEnumMemberName("Unités Légales")]
        UnitesLegales,
        /// <summary>
        /// Établissements
        /// </summary>
        [JsonStringEnumMemberName("Établissements")]
        Etablissements,
        /// <summary>
        /// Liens de succession
        /// </summary>
        [JsonStringEnumMemberName("Liens de succession")]
        LiensDeSuccession,
    }
}
