using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet renvoyé en cas de succès sur une requête demandant un établissement
    /// </summary>
    public class ReponseEtablissement : ReponseBase
    {
        /// <summary>
        /// Informations sur l'établissement correspondant à la requête
        /// </summary>
        [JsonPropertyName("etablissement")]
        public Etablissement Etablissement { get; set; }

        /// <summary>
        /// Création d'une réponse Etablissement
        /// </summary>
        public ReponseEtablissement() : base()
        {
        }
        /// <summary>
        /// Création d'une réponse Etablissement
        /// </summary>
        /// <param name="ex">Exception (en cas d'erreur)</param>
        public ReponseEtablissement(Exception ex) : base(ex)
        {
        }
    }
}