using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet représentant un comptage particulier à l'intérieur d'une facette
    /// </summary>
    public class Comptage
    {
        /// <summary>
        /// Modalité comptée
        /// </summary>
        [JsonPropertyName("valeur")]
        public String Valeur { get; set; }

        /// <summary>
        /// Nombre d'éléments correspondant à la modalité comptée
        /// </summary>
        [JsonPropertyName("nombre")]
        public Int64 Nombre { get; set; }
    }
}
