using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet renvoyé en cas de succès sur une requête demandant les prédécesseurs
    /// </summary>
    public class ReponseLienSuccession : ReponseBase
    {
        /// <summary>
        /// Liste des liens de succession correspondant à la requête
        /// </summary>
        [JsonPropertyName("liensSuccession")]
        public List<LienSuccession> LiensSuccession { get; set; }

        /// <summary>
        /// Création d'une réponse Lien de succession
        /// </summary>
        public ReponseLienSuccession() : base()
        {
        }
        /// <summary>
        /// Création d'une réponse Lien de succession
        /// </summary>
        /// <param name="ex">Exception (en cas d'erreur)</param>
        public ReponseLienSuccession(Exception ex) : base(ex)
        {
        }
    }
}