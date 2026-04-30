using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Dates des dernières mises à jour pour une collection de données spécifique.
    /// </summary>
    public class DatesDernieresMisesAJourDesDonnee
    {
        /// <summary>
        /// Nom de la collection concernée. Valeurs possibles : "Unités Légales" ou "Établissements".
        /// </summary>
        [JsonPropertyName("collection")]
        public CollectionEnum Collection { get; set; }

        /// <summary>
        /// Date et heure de la dernière mise à disposition des données de la collection.
        /// </summary>
        /// <remarks>Format ISO 8601 : yyyy-MM-ddTHH:mm:ss.SSS</remarks>
        [JsonPropertyName("dateDerniereMiseADisposition")]
        public DateTime DateDerniereMiseADisposition { get; set; }

        /// <summary>
        /// Date correspondant à la date de validité maximale des données consultées.
        /// </summary>
        /// <remarks>Format ISO 8601 : yyyy-MM-ddTHH:mm:ss.SSS</remarks>
        [JsonPropertyName("dateDernierTraitementMaximum")]
        public DateTime DateDernierTraitementMaximum { get; set; }

        /// <summary>
        /// Date du dernier traitement de masse sur la collection.
        /// À cette date, plusieurs centaines de milliers de documents peuvent avoir été mis à jour.
        /// Il est conseillé de traiter cette date de manière spécifique dans les processus métiers.
        /// </summary>
        /// <remarks>Format ISO 8601 : yyyy-MM-ddTHH:mm:ss.SSS</remarks>
        [JsonPropertyName("dateDernierTraitementDeMasse")]
        public DateTime DateDernierTraitementDeMasse { get; set; }
    }
}
