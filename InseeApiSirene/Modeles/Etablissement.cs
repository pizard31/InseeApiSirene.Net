using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet représentant un établissement et son historique
    /// </summary>
    public class Etablissement
    {
        /// <summary>
        /// Score de l'élément parmi l'ensemble des éléments répondant à la requête, plus le score est élevé, plus l'élément est haut placé
        /// Le score n'a pas de signification en dehors de la requête et n'est pas comparable aux score d'autres requêtes
        /// </summary>
        [JsonPropertyName("score")]
        public Decimal Score { get; set; }

        /// <summary>
        /// Numéro Siren de l'entreprise à laquelle appartient l'établissement
        /// </summary>
        [JsonPropertyName("siren")]
        public String Siren { get; set; }

        /// <summary>
        /// Numéro interne de classement de l'établissement
        /// </summary>
        [JsonPropertyName("nic")]
        public String Nic { get; set; }

        /// <summary>
        /// Numéro Siret de l'établissement
        /// </summary>
        /// <remarks>Toujours renseigné</remarks>
        [JsonPropertyName("siret")]
        public String Siret { get; set; }

        /// <summary>
        /// Statut de diffusion de l'établissement
        /// </summary>
        [JsonPropertyName("statutDiffusionEtablissement")]
        public StatutDiffusionEnum StatutDiffusionEtablissement { get; set; }

        /// <summary>
        /// Date de création de l'établissement
        /// </summary>
        [JsonPropertyName("dateCreationEtablissement")]
        public DateTime? DateCreationEtablissement { get; set; }

        /// <summary>
        /// Tranche d'effectif salarié de l'établissement, valorisée uniquement si l'année correspondante est supérieure ou égale à l'année d'interrogation -3 (sinon, NN)
        /// </summary>
        [JsonPropertyName("trancheEffectifsEtablissement")]
        public TrancheEffectifsEnum TrancheEffectifsEtablissement { get; set; }

        /// <summary>
        /// Année de la tranche d'effectif salarié de l'établissement, valorisée uniquement si l'année est supérieure ou égale à l'année d'interrogation -3 (sinon, null)
        /// </summary>
        [JsonPropertyName("anneeEffectifsEtablissement"), JsonConverter(typeof(OutilsJson.Int32NullableStringConverter))]
        public Int32? AnneeEffectifsEtablissement { get; set; }

        /// <summary>
        /// Code de l'activité exercée par l'artisan inscrit au registre des métiers.
        /// L'APRM est codifiée selon la nomenclature d'Activités Française de l'Artisanat (NAFA)
        /// </summary>
        [JsonPropertyName("activitePrincipaleRegistreMetiersEtablissement")]
        public String ActivitePrincipaleRegistreMetiersEtablissement { get; set; }

        /// <summary>
        /// Date de la dernière mise à jour effectuée au répertoire Sirene sur le Siret concerné
        /// </summary>
        [JsonPropertyName("dateDernierTraitementEtablissement")]
        public DateTime? DateDernierTraitementEtablissement { get; set; }

        /// <summary>
        /// Indicatrice précisant si le Siret est celui de l'établissement siège ou non
        /// </summary>
        [JsonPropertyName("etablissementSiege")]
        public Boolean EtablissementSiege { get; set; }

        /// <summary>
        /// Nombre de périodes dans la vie de l'établissement
        /// </summary>
        [JsonPropertyName("nombrePeriodesEtablissement")]
        public Int32 NombrePeriodesEtablissement { get; set; }

        /// <summary>
        /// Code APE en nomenclature NAF25
        /// </summary>
        [JsonPropertyName("activitePrincipaleNAF25Etablissement")]
        public String ActivitePrincipaleNAF25Etablissement { get; set; }

        /// <summary>
        /// Objet représentant les valeurs courante de l'unité légale de l'établissement
        /// </summary>
        [JsonPropertyName("uniteLegale")]
        public UniteLegaleEtablissement UniteLegale { get; set; }

        /// <summary>
        /// Ensemble des variables d'adresse d'un établissement
        /// </summary>
        [JsonPropertyName("adresseEtablissement")]
        public Adresse AdresseEtablissement { get; set; }

        /// <summary>
        /// Ensemble des variables d'adresse complémentaire d'un établissement
        /// </summary>
        [JsonPropertyName("adresse2Etablissement")]
        public AdresseComplementaire Adresse2Etablissement { get; set; }

        /// <summary>
        /// Ensemble des variables historisées de l'établissement entre dateDebut et dateFin
        /// </summary>
        [JsonPropertyName("periodesEtablissement")]
        public List<PeriodeEtablissement> PeriodesEtablissement { get; set; }
    }
}