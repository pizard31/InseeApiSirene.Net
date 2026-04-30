using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Ensemble des variables d'adresse complémentaire d'un établissement
    /// </summary>
    public class AdresseComplementaire
    {
        /// <summary>
        /// Complément d'adresse de l'établissement
        /// </summary>
        [JsonPropertyName("complementAdresse2Etablissement")]
        public String ComplementAdresse2Etablissement { get; set; }

        /// <summary>
        /// Numéro dans la voie
        /// </summary>
        [JsonPropertyName("numeroVoie2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String NumeroVoie2Etablissement { get; set; }

        /// <summary>
        /// Indice de répétition dans la voie
        /// </summary>
        [JsonPropertyName("indiceRepetition2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String IndiceRepetition2Etablissement { get; set; }

        /// <summary>
        /// Type de la voie
        /// </summary>
        [JsonPropertyName("typeVoie2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String TypeVoie2Etablissement { get; set; }

        /// <summary>
        /// Libellé de la voie
        /// </summary>
        [JsonPropertyName("libelleVoie2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String LibelleVoie2Etablissement { get; set; }

        /// <summary>
        /// Code postal
        /// </summary>
        [JsonPropertyName("codePostal2Etablissement")]
        public String CodePostal2Etablissement { get; set; }

        /// <summary>
        /// Libellé de la commune pour les adresses en France
        /// </summary>
        [JsonPropertyName("libelleCommune2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String LibelleCommune2Etablissement { get; set; }

        /// <summary>
        /// Libellé complémentaire pour une adresse à l'étranger
        /// </summary>
        [JsonPropertyName("libelleCommuneEtranger2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String LibelleCommuneEtranger2Etablissement { get; set; }

        /// <summary>
        /// Distribution spéciale (BP par ex)
        /// </summary>
        [JsonPropertyName("distributionSpeciale2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String DistributionSpeciale2Etablissement { get; set; }

        /// <summary>
        /// Code commune de localisation de l'établissement hors établissements situés à l'étranger
        /// Le code commune est défini dans le code officiel géographique (COG) : https://www.insee.fr/fr/information/2028028
        /// </summary>
        [JsonPropertyName("codeCommune2Etablissement")]
        public String CodeCommune2Etablissement { get; set; }

        /// <summary>
        /// Numéro de Cedex
        /// </summary>
        [JsonPropertyName("codeCedex2Etablissement")]
        public String CodeCedex2Etablissement { get; set; }

        /// <summary>
        /// Libellé correspondant au numéro de Cedex (variable codeCedexEtablissement)
        /// </summary>
        [JsonPropertyName("libelleCedex2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String LibelleCedex2Etablissement { get; set; }

        /// <summary>
        /// Code pays pour les établissements situés à l'étranger
        /// </summary>
        [JsonPropertyName("codePaysEtranger2Etablissement")]
        public String CodePaysEtranger2Etablissement { get; set; }

        /// <summary>
        /// Libellé du pays pour les adresses à l'étranger
        /// </summary>
        [JsonPropertyName("libellePaysEtranger2Etablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String LibellePaysEtranger2Etablissement { get; set; }
    }
}