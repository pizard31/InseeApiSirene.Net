using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet renvoyé en cas de succès sur une requête demandant des établissements
    /// </summary>
    public class ReponseEtablissements : ReponseBase
    {
        /// <summary>
        /// Liste des établissements correspondant à la requête
        /// </summary>
        [JsonPropertyName("etablissements")]
        public List<Etablissement> Etablissements { get; set; }

        /// <summary>
        /// Liste des facettes correspondant à la requête
        /// </summary>
        [JsonPropertyName("facettes")]
        public List<Facette> Facettes { get; set; }

        /// <summary>
        /// Création d'une réponse Etablissements
        /// </summary>
        public ReponseEtablissements() : base()
        {
        }
        /// <summary>
        /// Création d'une réponse Etablissements
        /// </summary>
        /// <param name="ex">Exception (en cas d'erreur)</param>
        public ReponseEtablissements(Exception ex) : base(ex)
        {
        }
    }
}