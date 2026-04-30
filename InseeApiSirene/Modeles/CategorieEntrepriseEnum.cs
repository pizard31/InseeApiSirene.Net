using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des catégories d'entreprises
    /// </summary>
    [JsonConverter(typeof(OutilsJson.EnumNullableStringConverter<CategorieEntrepriseEnum>))]
    public enum CategorieEntrepriseEnum
    {
        /// <summary>
        /// Petite ou moyenne entreprise
        /// </summary>
        [JsonStringEnumMemberName("PME")]
        PME,
        /// <summary>
        /// Entreprise de taille intermédiaire
        /// </summary>
        [JsonStringEnumMemberName("ETI")]
        ETI,
        /// <summary>
        /// Grande entreprise
        /// </summary>
        [JsonStringEnumMemberName("GE")]
        GE,
    }
}
