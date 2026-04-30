using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Informations générales sur le résultat de la requête
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Égal au status de la réponse HTTP
        /// </summary>
        [JsonPropertyName("statut")]
        public Int32 Statut { get; set; }

        /// <summary>
        /// En cas d'erreur, message indiquant la cause de l'erreur. OK sinon
        /// </summary>
        [JsonPropertyName("message")]
        public String Message { get; set; }

        /// <summary>
        /// Nombre total des éléments répondant à la requête
        /// </summary>
        [JsonPropertyName("total")]
        public Int64 Total { get; set; }

        /// <summary>
        /// Numéro du premier élément fourni, défaut à 0, commence à 0
        /// </summary>
        [JsonPropertyName("debut")]
        public Int64 Debut { get; set; }

        /// <summary>
        /// Nombre d'éléments fournis, défaut à 20
        /// </summary>
        [JsonPropertyName("nombre")]
        public Int64 Nombre { get; set; }

        /// <summary>
        /// Curseur passé en argument dans la requête de l'utilisateur, utilisé pour la pagination profonde
        /// </summary>
        [JsonPropertyName("curseur")]
        public String Curseur { get; set; }

        /// <summary>
        /// Curseur suivant pour accéder à la suite des résultat lorsqu'on utilise la pagination profonde
        /// </summary>
        [JsonPropertyName("curseurSuivant")]
        public String CurseurSuivant { get; set; }

        /// <summary>
        /// Détermine si la requête a été effectuée avec une pagination profonde et s'il reste des résultats à récupérer
        /// </summary>
        [JsonIgnore]
        public Boolean CurseurResultatRestant => this.Curseur != this.CurseurSuivant;
    }
}