using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des états administratifs d'une Unité légale
    /// </summary>
    [JsonConverter(typeof(OutilsJson.EnumNullableStringConverter<EtatAdministratifUniteLegaleEnum>))]
    public enum EtatAdministratifUniteLegaleEnum
    {
        /// <summary>
        /// Entreprise active
        /// </summary>
        [JsonStringEnumMemberName("A")]
        Active,
        /// <summary>
        /// Entreprise cessée
        /// </summary>
        [JsonStringEnumMemberName("C")]
        Cessee,
    }
}
