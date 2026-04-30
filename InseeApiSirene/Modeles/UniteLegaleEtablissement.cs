using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet représentant les valeurs courantes de l'unité légale de l'établissement
    /// </summary>
    public class UniteLegaleEtablissement : UniteLegaleBase
    {
        /// <summary>
        /// État de l'entreprise pendant la période
        /// </summary>
        [JsonPropertyName("etatAdministratifUniteLegale")]
        public EtatAdministratifUniteLegaleEnum? EtatAdministratifUniteLegale { get; set; }

        /// <summary>
        /// Nom de naissance pour les personnes physiques pour la période (null pour les personnes morales)
        /// </summary>
        [JsonPropertyName("nomUniteLegale")]
        public String NomUniteLegale { get; set; }

        /// <summary>
        /// Raison sociale (personnes morales) (null pour les personnes morales)
        /// </summary>
        [JsonPropertyName("denominationUniteLegale")]
        public String DenominationUniteLegale { get; set; }

        /// <summary>
        /// Premier nom sous lequel l’entreprise est connue du public
        /// </summary>
        [JsonPropertyName("denominationUsuelle1UniteLegale")]
        public String DenominationUsuelle1UniteLegale { get; set; }

        /// <summary>
        /// Deuxième nom sous lequel l’entreprise est connue du public
        /// </summary>
        [JsonPropertyName("denominationUsuelle2UniteLegale")]
        public String DenominationUsuelle2UniteLegale { get; set; }

        /// <summary>
        /// Troisième nom sous lequel l’entreprise est connue du public
        /// </summary>
        [JsonPropertyName("denominationUsuelle3UniteLegale")]
        public String DenominationUsuelle3UniteLegale { get; set; }

        /// <summary>
        /// Activité principale de l'entreprise pendant la période (l'APE est codifiée selon la nomenclature d'Activités Française (NAF))
        /// Nomenclature sur https://www.insee.fr/fr/information/2406147
        /// </summary>
        [JsonPropertyName("activitePrincipaleUniteLegale")]
        public String ActivitePrincipaleUniteLegale { get; set; }

        /// <summary>
        /// Catégorie juridique de l'entreprise (variable Null pour les personnes physiques.
        /// Nomenclature sur https://www.insee.fr/fr/information/2028129
        /// </summary>
        [JsonPropertyName("categorieJuridiqueUniteLegale"), JsonConverter(typeof(OutilsJson.Int32NullableStringConverter))]
        public Int32? CategorieJuridiqueUniteLegale { get; set; }

        /// <summary>
        /// Identifiant du siège pour la période
        /// </summary>
        /// <remarks>Le Siret du siège est obtenu en concaténant le numéro Siren et le Nic</remarks>
        [JsonPropertyName("nicSiegeUniteLegale")]
        public String NicSiegeUniteLegale { get; set; }

        /// <summary>
        /// Nomenclature de l'activité, permet de savoir à partir de quelle nomenclature est codifiée ActivitePrincipale
        /// </summary>
        [JsonPropertyName("nomenclatureActivitePrincipaleUniteLegale")]
        public NomenclatureActiviteEnum? NomenclatureActivitePrincipaleUniteLegale { get; set; }

        /// <summary>
        /// Nom d'usage pour les personnes physiques si celui-ci existe, null pour les personnes morales
        /// </summary>
        [JsonPropertyName("nomUsageUniteLegale")]
        public String NomUsageUniteLegale { get; set; }

        /// <summary>
        /// Appartenance de l'unité légale au champ de l'économie sociale et solidaire (ESS)
        /// </summary>
        [JsonPropertyName("economieSocialeSolidaireUniteLegale"), JsonConverter(typeof(OutilsJson.BooleanOorNConverter))]
        public Boolean EconomieSocialeSolidaireUniteLegale { get; set; }

        /// <summary>
        /// Appartenance de l'unité légale au champ société à mission (SM)
        /// </summary>
        [JsonPropertyName("societeMissionUniteLegale"), JsonConverter(typeof(OutilsJson.BooleanNullableOorNConverter))]
        public Boolean? SocieteMissionUniteLegale { get; set; }

        /// <summary>
        /// Caractère employeur de l'unité légale
        /// </summary>
        [JsonPropertyName("caractereEmployeurUniteLegale")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String CaractereEmployeurUniteLegale { get; set; }
    }
}
