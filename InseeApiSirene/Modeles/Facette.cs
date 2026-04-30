using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet représentant une facette (un ensemble de comptages selon un champ, une requête ou une série d'intervalles)
    /// </summary>
    public class Facette
    {
        /// <summary>
        /// Nom de la facette
        /// </summary>
        [JsonPropertyName("nom")]
        public String Nom { get; set; }

        /// <summary>
        /// Nombre d'éléments pour lesquels la facette vaut null
        /// </summary>
        [JsonPropertyName("manquants")]
        public Int32 Manquants { get; set; }

        /// <summary>
        /// Nombre total d'éléments ayant une valeur non nulle pour la facette
        /// </summary>
        [JsonPropertyName("total")]
        public Int64 Total { get; set; }

        /// <summary>
        /// Nombre de valeurs distinctes pour la facette
        /// </summary>
        [JsonPropertyName("modalites")]
        public Int32 Modalites { get; set; }

        /// <summary>
        /// Nombre d'éléments dont la valeur est inférieure au premier intervalle, uniquement pour les facettes de type intervalle
        /// </summary>
        [JsonPropertyName("avant")]
        public Int32 Avant { get; set; }

        /// <summary>
        /// Nombre d'éléments dont la valeur est supérieure au dernier intervalle, uniquement pour les facettes de type intervalle
        /// </summary>
        [JsonPropertyName("apres")]
        public Int32 Apres { get; set; }

        /// <summary>
        /// Nombre d'élements compris entre la borne inférieure du premier intervalle et la borne supérieure du dernier intervalle, uniquement pour les facettes de type intervalle
        /// </summary>
        [JsonPropertyName("entre")]
        public Int32 Entre { get; set; }

        /// <summary>
        /// Liste des comptages associés à la facette
        /// </summary>
        [JsonPropertyName("comptages")]
        public List<Comptage> Comptages { get; set; }
    }
}
