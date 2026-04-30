using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des états administratifs d'un établissement
    /// </summary>
    [JsonConverter(typeof(OutilsJson.EnumNullableStringConverter<EtatAdministratifEtablissementEnum>))]
    public enum EtatAdministratifEtablissementEnum
    {
        /// <summary>
        /// Etablissement actif
        /// </summary>
        [JsonStringEnumMemberName("A")]
        Actif,
        /// <summary>
        /// Etablissement fermé
        /// </summary>
        [JsonStringEnumMemberName("F")]
        Ferme,
    }
}
