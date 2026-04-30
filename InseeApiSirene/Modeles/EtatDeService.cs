using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Etat de service
    /// </summary>
    public class EtatDeService
    {
        /// <summary>
        /// Collection
        /// </summary>
        [JsonPropertyName("Collection")]
        public CollectionEnum Collection { get; set; }

        /// <summary>
        /// Etat du service
        /// </summary>
        [JsonPropertyName("etatCollection")]
        public EtatEnum EtatCollection { get; set; }
    }
}
