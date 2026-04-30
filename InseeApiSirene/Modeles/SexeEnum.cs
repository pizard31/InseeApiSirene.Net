using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des sexes
    /// </summary>
    [JsonConverter(typeof(OutilsJson.EnumNullableStringConverter<SexeEnum>))]
    public enum SexeEnum
    {
        /// <summary>
        /// Masculin
        /// </summary>
        [JsonStringEnumMemberName("M")]
        Masculin,
        /// <summary>
        /// Féminin
        /// </summary>
        [JsonStringEnumMemberName("F")]
        Feminin,
        /// <summary>
        /// Non déterminé
        /// </summary>
        [JsonStringEnumMemberName("[ND]")]
        NonDetermine,
    }
}
