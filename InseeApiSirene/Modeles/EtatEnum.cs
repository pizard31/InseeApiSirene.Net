using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des états
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EtatEnum
    {
        /// <summary>
        /// Disponible
        /// </summary>
        [JsonStringEnumMemberName("UP")]
        Disponible,
        /// <summary>
        /// Indisponible
        /// </summary>
        [JsonStringEnumMemberName("DOWN")]
        Indisponible,
    }
}
