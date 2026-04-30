using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des status de diffusion
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatutDiffusionEnum
    {
        /// <summary>
        /// Diffusible
        /// </summary>
        [JsonStringEnumMemberName("O")]
        Diffusible,
        /// <summary>
        /// Unité en diffusion partielle
        /// </summary>
        [JsonStringEnumMemberName("P")]
        DiffusionPartielle,
    }
}
