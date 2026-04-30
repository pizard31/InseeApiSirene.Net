using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Classe représentant une unité légale
    /// </summary>
    public class UniteLegale : UniteLegaleEtablissement
    {
        /// <summary>
        /// Score de l'élément parmi l'ensemble des éléments répondant à la requête, plus le score est élevé, plus l'élément est haut placé. Le score n'a pas de signification en dehors de la requête et n'est pas comparable aux score d'autres requêtes
        /// </summary>
        [JsonPropertyName("score")]
        public Decimal Score { get; set; }

        /// <summary>
        /// Numéro Siren de l'entreprise
        /// </summary>
        /// <remarks>Toujours renseigné</remarks>
        [JsonPropertyName("siren")]
        public String Siren { get; set; }

        /// <summary>
        /// Nombre de périodes dans la vie de l'unité légale
        /// </summary>
        [JsonPropertyName("nombrePeriodesUniteLegale")]
        public Int32 NombrePeriodesUniteLegale { get; set; }

        /// <summary>
        /// Liste des périodes de l'unité légale
        /// </summary>
        [JsonPropertyName("periodesUniteLegale")]
        public List<PeriodeUniteLegale> PeriodesUniteLegale { get; set; }
    }

    /// <summary>
    /// Classe représentant la base d'une unité légale
    /// </summary>
    public abstract class UniteLegaleBase
    {
        /// <summary>
        /// Statut de diffusion de l'unité légale
        /// </summary>
        [JsonPropertyName("statutDiffusionUniteLegale")]
        public StatutDiffusionEnum StatutDiffusionUniteLegale { get; set; }

        /// <summary>
        /// True si l'unité est une unité purgée
        /// </summary>
        [JsonPropertyName("unitePurgeeUniteLegale")]
        public Boolean UnitePurgeeUniteLegale { get; set; }

        /// <summary>
        /// Date de création de l'unité légale
        /// </summary>
        [JsonPropertyName("dateCreationUniteLegale")]
        public DateTime? DateCreationUniteLegale { get; set; }

        /// <summary>
        /// Date de naissance pour la personne physique sinon null
        /// </summary>
        /// <remarks>L'accès à ces données est soumis à une démarche auprès de la Commission nationale de l'informatique et des libertés.</remarks>
        [JsonPropertyName("dateNaissanceUniteLegale")]
        public DateTime? DateNaissanceUniteLegale { get; set; }

        /// <summary>
        /// Code commune de naissance pour les personnes physiques, null pour les personnes morales et les personnes physiques nées à l'étranger
        /// </summary>
        /// <remarks>L'accès à ces données est soumis à une démarche auprès de la Commission nationale de l'informatique et des libertés.</remarks>
        [JsonPropertyName("codeCommuneNaissanceUniteLegale")]
        public String CodeCommuneNaissanceUniteLegale { get; set; }

        /// <summary>
        /// Code pays de naissance pour les personnes physiques nées à l'étranger, null sinon
        /// </summary>
        /// <remarks>L'accès à ces données est soumis à une démarche auprès de la Commission nationale de l'informatique et des libertés.</remarks>
        [JsonPropertyName("codePaysNaissanceUniteLegale")]
        public String CodePaysNaissanceUniteLegale { get; set; }

        /// <summary>
        /// Nationalité pour les personnes physiques
        /// </summary>
        /// <remarks>L'accès à ces données est soumis à une démarche auprès de la Commission nationale de l'informatique et des libertés.</remarks>
        [JsonPropertyName("libelleNationaliteUniteLegale")]
        public String LibelleNationaliteUniteLegale { get; set; }

        /// <summary>
        /// Numéro au Répertoire National des Associations
        /// </summary>
        [JsonPropertyName("identifiantAssociationUniteLegale")]
        public String IdentifiantAssociationUniteLegale { get; set; }

        /// <summary>
        /// Tranche d'effectif salarié de l'unité légale, valorisé uniquement si l'année correspondante est supérieure ou égale à l'année d'interrogation-3 (sinon, NN)
        /// </summary>
        [JsonPropertyName("trancheEffectifsUniteLegale")]
        public TrancheEffectifsEnum TrancheEffectifsUniteLegale { get; set; }

        /// <summary>
        /// Année de validité de la tranche d'effectif salarié de l'unité légale, valorisée uniquement si l'année est supérieure ou égale à l'année d'interrogation-3 (sinon, null)
        /// </summary>
        [JsonPropertyName("anneeEffectifsUniteLegale"), JsonConverter(typeof(OutilsJson.Int32NullableStringConverter))]
        public Int32? AnneeEffectifsUniteLegale { get; set; }

        /// <summary>
        /// Date de la dernière mise à jour effectuée au répertoire Sirene sur le Siren concerné
        /// </summary>
        [JsonPropertyName("dateDernierTraitementUniteLegale")]
        public DateTime? DateDernierTraitementUniteLegale { get; set; }

        /// <summary>
        /// Catégorie à laquelle appartient l'entreprise : Petite ou moyenne entreprise, Entreprise de taille intermédiaire, Grande entreprise
        /// </summary>
        [JsonPropertyName("categorieEntreprise")]
        public CategorieEntrepriseEnum? CategorieEntreprise { get; set; }

        /// <summary>
        /// Année de validité de la catégorie d'entreprise
        /// </summary>
        [JsonPropertyName("anneeCategorieEntreprise"), JsonConverter(typeof(OutilsJson.Int32NullableStringConverter))]
        public Int32? AnneeCategorieEntreprise { get; set; }

        /// <summary>
        /// Sigle de l'unité légale
        /// </summary>
        [JsonPropertyName("sigleUniteLegale")]
        public String SigleUniteLegale { get; set; }

        /// <summary>
        /// Sexe pour les personnes physiques sinon null
        /// </summary>
        [JsonPropertyName("sexeUniteLegale")]
        public SexeEnum? SexeUniteLegale { get; set; }

        /// <summary>
        /// Premier prénom déclaré pour une personne physique, peut être null dans le cas d'une unité purgée
        /// </summary>
        [JsonPropertyName("prenom1UniteLegale")]
        public String Prenom1UniteLegale { get; set; }

        /// <summary>
        /// Deuxième prénom déclaré pour une personne physique
        /// </summary>
        [JsonPropertyName("prenom2UniteLegale")]
        public String Prenom2UniteLegale { get; set; }

        /// <summary>
        /// Troisième prénom déclaré pour une personne physique
        /// </summary>
        [JsonPropertyName("prenom3UniteLegale")]
        public String Prenom3UniteLegale { get; set; }

        /// <summary>
        /// Quatrième prénom déclaré pour une personne physique
        /// </summary>
        [JsonPropertyName("prenom4UniteLegale")]
        public String Prenom4UniteLegale { get; set; }

        /// <summary>
        /// Prénom usuel pour les personne physiques, correspond généralement au Prenom1
        /// </summary>
        [JsonPropertyName("prenomUsuelUniteLegale")]
        public String PrenomUsuelUniteLegale { get; set; }

        /// <summary>
        /// Pseudonyme pour les personnes physiques
        /// </summary>
        [JsonPropertyName("pseudonymeUniteLegale")]
        public String PseudonymeUniteLegale { get; set; }

        ///// <summary>
        ///// Code APE en nomenclature NAF25
        ///// </summary>
        //[JsonPropertyName("activitePrincipaleNAF25UniteLegale")]
        //public String ActivitePrincipaleNAF25UniteLegale { get; set; }
    }
}