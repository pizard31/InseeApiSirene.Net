using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Ensemble des variables historisées de l'établissement entre dateDebut et dateFin
    /// </summary>
    public class PeriodeEtablissement
    {
        /// <summary>
        /// Date de fin de la période, null pour la dernière période
        /// </summary>
        [JsonPropertyName("dateFin")]
        public DateTime? DateFin { get; set; }

        /// <summary>
        /// Date de début de la période
        /// </summary>
        [JsonPropertyName("dateDebut")]
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// État administratif de l'établissement pendant la période
        /// </summary>
        [JsonPropertyName("etatAdministratifEtablissement")]
        public EtatAdministratifEtablissementEnum? EtatAdministratifEtablissement { get; set; }

        /// <summary>
        /// Indicatrice de changement de l'état administratif de l'établissement par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementEtatAdministratifEtablissement")]
        public Boolean ChangementEtatAdministratifEtablissement { get; set; }

        /// <summary>
        /// Première ligne d'enseigne
        /// </summary>
        [JsonPropertyName("enseigne1Etablissement")]
        public String Enseigne1Etablissement { get; set; }

        /// <summary>
        /// Deuxième ligne d'enseigne
        /// </summary>
        [JsonPropertyName("enseigne2Etablissement")]
        public String Enseigne2Etablissement { get; set; }

        /// <summary>
        /// Troisième ligne d'enseigne
        /// </summary>
        [JsonPropertyName("enseigne3Etablissement")]
        public String Enseigne3Etablissement { get; set; }

        /// <summary>
        /// Indicatrice de changement de l'enseigne de l'établissement par rapport à la période précédente (un seul indicateur pour les trois variables Enseigne1, Enseigne2 et Enseigne3). Un seul indicateur pour les trois variables enseigne
        /// </summary>
        [JsonPropertyName("changementEnseigneEtablissement")]
        public Boolean ChangementEnseigneEtablissement { get; set; }

        /// <summary>
        /// Nom sous lequel l'activité de l'établissement est connu du public
        /// </summary>
        [JsonPropertyName("denominationUsuelleEtablissement")]
        public String DenominationUsuelleEtablissement { get; set; }

        /// <summary>
        /// Indicatrice de changement de la dénomination usuelle de l'établissement par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementDenominationUsuelleEtablissement")]
        public Boolean ChangementDenominationUsuelleEtablissement { get; set; }

        /// <summary>
        /// Activité principale de l'établissement pendant la période
        /// L'APE est codifiée selon la nomenclature d'Activités Française (NAF) : https://www.insee.fr/fr/information/2406147
        /// </summary>
        [JsonPropertyName("activitePrincipaleEtablissement")]
        public String ActivitePrincipaleEtablissement { get; set; }

        /// <summary>
        /// Nomenclature de l'activité, permet de savoir à partir de quelle nomenclature est codifiée ActivitePrincipaleEtablissement
        /// </summary>
        [JsonPropertyName("nomenclatureActivitePrincipaleEtablissement")]
        public NomenclatureActiviteEnum? NomenclatureActivitePrincipaleEtablissement { get; set; }

        /// <summary>
        /// Indicatrice de changement de l'activité principale de l'établissement par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementActivitePrincipaleEtablissement")]
        public Boolean ChangementActivitePrincipaleEtablissement { get; set; }

        /// <summary>
        /// Caractère employeur de l'établissement
        /// </summary>
        /// <remarks>O=oui, N=non, null=sans objet</remarks>
        [JsonPropertyName("caractereEmployeurEtablissement"), JsonConverter(typeof(OutilsJson.BooleanNullableOorNConverter))]
        public Boolean? CaractereEmployeurEtablissement { get; set; }

        /// <summary>
        /// Indicatrice de changement du caractère employeur de l'établissement par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementCaractereEmployeurEtablissement")]
        public Boolean ChangementCaractereEmployeurEtablissement { get; set; }
    }
}