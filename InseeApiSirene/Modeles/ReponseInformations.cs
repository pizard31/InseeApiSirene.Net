using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet renvoyé en cas de requête demandant les informations sur le service
    /// </summary>
    public class ReponseInformations : ReponseBase
    {
        /// <summary>
        /// État actuel du service
        /// </summary>
        [JsonPropertyName("etatService")]
        public EtatEnum EtatService { get; set; }

        /// <summary>
        /// Etats des services
        /// </summary>
        [JsonPropertyName("etatsDesServices")]
        public List<EtatDeService> EtatsDesServices { get; set; }

        /// <summary>
        /// Numéro de la version
        /// </summary>
        [JsonPropertyName("versionService")]
        public String VersionService { get; set; }

        /// <summary>
        /// Historique des versions de l'API Sirene
        /// </summary>
        [JsonPropertyName("journalDesModifications")]
        public String JournalDesModifications { get; set; }

        /// <summary>
        /// Dates des dernières mises à jour de chaque collection de données
        /// </summary>
        [JsonPropertyName("datesDernieresMisesAJourDesDonnees")]
        public List<DatesDernieresMisesAJourDesDonnee> DatesDernieresMisesAJourDesDonnees { get; set; }

        /// <summary>
        /// Création d'une réponse Informations
        /// </summary>
        public ReponseInformations() : base()
        {
        }
        /// <summary>
        /// Création d'une réponse Informations
        /// </summary>
        /// <param name="ex">Exception (en cas d'erreur)</param>
        public ReponseInformations(Exception ex) : base(ex)
        {
        }
    }
}