using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet renvoyé en cas de succès sur une requête demandant une unité légale
    /// </summary>
    public class ReponseUniteLegale : ReponseBase
    {
        /// <summary>
        /// Informations sur l'unité légale correspondant à la requête
        /// </summary>
        [JsonPropertyName("uniteLegale")]
        public UniteLegale UniteLegale { get; set; }

        /// <summary>
        /// Création d'une réponse Unité Légale
        /// </summary>
        public ReponseUniteLegale() : base()
        {
        }
        /// <summary>
        /// Création d'une réponse Unité Légale
        /// </summary>
        /// <param name="ex">Exception (en cas d'erreur)</param>
        public ReponseUniteLegale(Exception ex) : base(ex)
        {
        }
    }
}