using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Ensemble des variables d'adresse d'un établissement
    /// </summary>
    public class Adresse
    {
        /// <summary>
        /// Complément d'adresse de l'établissement
        /// </summary>
        [JsonPropertyName("complementAdresseEtablissement")]
        public String ComplementAdresseEtablissement { get; set; }

        /// <summary>
        /// Numéro dans la voie
        /// </summary>
        [JsonPropertyName("numeroVoieEtablissement")]
        public String NumeroVoieEtablissement { get; set; }

        /// <summary>
        /// Indice de répétition dans la voie
        /// </summary>
        [JsonPropertyName("indiceRepetitionEtablissement")]
        public String IndiceRepetitionEtablissement { get; set; }

        /// <summary>
        /// Numéro de la dernière adresse dans la voie
        /// </summary>
        [JsonPropertyName("dernierNumeroVoieEtablissement")]
        public String DernierNumeroVoieEtablissement { get; set; }

        /// <summary>
        /// Indice de répétition de la dernière adresse dans la voie
        /// </summary>
        [JsonPropertyName("indiceRepetitionDernierNumeroVoieEtablissement")]
        public String IndiceRepetitionDernierNumeroVoieEtablissement { get; set; }

        /// <summary>
        /// Type de la voie
        /// </summary>
        [JsonPropertyName("typeVoieEtablissement")]
        public String TypeVoieEtablissement { get; set; }

        /// <summary>
        /// Libellé de la voie
        /// </summary>
        [JsonPropertyName("libelleVoieEtablissement")]
        public String LibelleVoieEtablissement { get; set; }

        /// <summary>
        /// Code postal
        /// </summary>
        [JsonPropertyName("codePostalEtablissement")]
        public String CodePostalEtablissement { get; set; }

        /// <summary>
        /// Libellé de la commune pour les adresses en France
        /// </summary>
        [JsonPropertyName("libelleCommuneEtablissement")]
        public String LibelleCommuneEtablissement { get; set; }

        /// <summary>
        /// Libellé complémentaire pour une adresse à l'étranger
        /// </summary>
        [JsonPropertyName("libelleCommuneEtrangerEtablissement")]
        public String LibelleCommuneEtrangerEtablissement { get; set; }

        /// <summary>
        /// Distribution spéciale (BP par ex)
        /// </summary>
        [JsonPropertyName("distributionSpecialeEtablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String DistributionSpecialeEtablissement { get; set; }

        /// <summary>
        /// Code commune de localisation de l'établissement hors établissements situés à l'étranger
        /// Le code commune est défini dans le code officiel géographique (COG) : https://www.insee.fr/fr/information/2028028
        /// </summary>
        [JsonPropertyName("codeCommuneEtablissement")]
        public String CodeCommuneEtablissement { get; set; }

        /// <summary>
        /// Numéro de Cedex
        /// </summary>
        [JsonPropertyName("codeCedexEtablissement")]
        public String CodeCedexEtablissement { get; set; }

        /// <summary>
        /// Libellé correspondant au numéro de Cedex (variable codeCedexEtablissement)
        /// </summary>
        [JsonPropertyName("libelleCedexEtablissement")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String LibelleCedexEtablissement { get; set; }

        /// <summary>
        /// Code pays pour les établissements situés à l'étranger
        /// </summary>
        [JsonPropertyName("codePaysEtrangerEtablissement")]
        public String CodePaysEtrangerEtablissement { get; set; }

        /// <summary>
        /// Libellé du pays pour les adresses à l'étranger
        /// </summary>
        [JsonPropertyName("libellePaysEtrangerEtablissement")]
        public String LibellePaysEtrangerEtablissement { get; set; }

        /// <summary>
        /// IdentifiantAdresseEtablissement
        /// </summary>
        [JsonPropertyName("identifiantAdresseEtablissement")]
        public String IdentifiantAdresseEtablissement { get; set; }

        /// <summary>
        /// Coordonnée Lambert abscisse de l'établissement
        /// </summary>
        [JsonPropertyName("coordonneeLambertAbscisseEtablissement")]
        public String CoordonneeLambertAbscisseEtablissement { get; set; }

        /// <summary>
        /// Coordonnée Lambert ordonnée de l'établissement
        /// </summary>
        [JsonPropertyName("coordonneeLambertOrdonneeEtablissement")]
        public String CoordonneeLambertOrdonneeEtablissement { get; set; }

        /// <summary>
        /// Adresse provenant de la Base Adresse Nationale (BAN) ?
        /// </summary>
        /// <remarks>IdentifiantAdresseEtablissement = xxxxxxxxx_B</remarks>
        [JsonIgnore]
        public Boolean IsProvenanceBAN
        {
            get
            {
                return this.IdentifiantAdresseEtablissement?.ToString().EndsWith("_B") ?? false;
            }
        }
        /// <summary>
        /// Adresse provenant du cadastre ?
        /// </summary>
        /// <remarks>IdentifiantAdresseEtablissement = xxxxxxxxx_C</remarks>
        [JsonIgnore]
        public Boolean IsProvenanceCadastre
        {
            get
            {
                return this.IdentifiantAdresseEtablissement?.ToString().EndsWith("_C") ?? false;
            }
        }
    }
}