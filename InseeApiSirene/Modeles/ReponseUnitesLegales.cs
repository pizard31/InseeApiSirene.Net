using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet renvoyé en cas de succès sur une requête demandant des unités légales
    /// </summary>
    public class ReponseUnitesLegales : ReponseBase
    {
        /// <summary>
        /// Liste des unités légales correspondant à la requête
        /// </summary>
        [JsonPropertyName("unitesLegales")]
        public List<UniteLegale> UnitesLegales { get; set; }

        /// <summary>
        /// Liste des facettes correspondant à la requête
        /// </summary>
        [JsonPropertyName("facettes")]
        public List<Facette> Facettes { get; set; }

        /// <summary>
        /// Création d'une réponse Unités Légales
        /// </summary>
        public ReponseUnitesLegales() : base()
        {
        }
        /// <summary>
        /// Création d'une réponse Unités Légales
        /// </summary>
        /// <param name="ex">Exception (en cas d'erreur)</param>
        public ReponseUnitesLegales(Exception ex) : base(ex)
        {
        }
    }
}