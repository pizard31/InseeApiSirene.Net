using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Ensemble des variables historisées de l'unité légale entre dateDebut et dateFin.
    /// </summary>
    public class PeriodeUniteLegale
    {
        /// <summary>
        /// Date de fin de la période, null pour la dernière période
        /// </summary>
        /// <remarks>Null pour la période contenant les valeurs courantes de l'entreprise pour toutes les variables</remarks>
        [JsonPropertyName("dateFin")]
        public DateTime? DateFin { get; set; }

        /// <summary>
        /// Date de début de la période
        /// </summary>
        [JsonPropertyName("dateDebut")]
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// État de l'entreprise pendant la période
        /// </summary>
        [JsonPropertyName("etatAdministratifUniteLegale")]
        public EtatAdministratifUniteLegaleEnum? EtatAdministratifUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement d'état par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementEtatAdministratifUniteLegale")]
        public Boolean ChangementEtatAdministratifUniteLegale { get; set; }

        /// <summary>
        /// Nom de naissance pour les personnes physiques pour la période, null pour les personnes morales
        /// </summary>
        [JsonPropertyName("nomUniteLegale")]
        public String NomUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement du nom par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementNomUniteLegale")]
        public Boolean ChangementNomUniteLegale { get; set; }

        /// <summary>
        /// Nom d'usage pour les personnes physiques si celui-ci existe, null pour les personnes morales
        /// </summary>
        [JsonPropertyName("nomUsageUniteLegale")]
        public String NomUsageUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement du nom d'usage par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementNomUsageUniteLegale")]
        public Boolean ChangementNomUsageUniteLegale { get; set; }

        /// <summary>
        /// Raison sociale (personnes morales), null pour les personnes physiques
        /// </summary>
        [JsonPropertyName("denominationUniteLegale")]
        public String DenominationUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement de la dénomination par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementDenominationUniteLegale")]
        public Boolean ChangementDenominationUniteLegale { get; set; }

        /// <summary>
        /// Premier nom sous lequel l'entreprise est connue du public
        /// </summary>
        [JsonPropertyName("denominationUsuelle1UniteLegale")]
        public String DenominationUsuelle1UniteLegale { get; set; }

        /// <summary>
        /// Deuxième nom sous lequel l'entreprise est connue du public
        /// </summary>
        [JsonPropertyName("denominationUsuelle2UniteLegale")]
        public String DenominationUsuelle2UniteLegale { get; set; }

        /// <summary>
        /// Troisième nom sous lequel l'entreprise est connue du public
        /// </summary>
        [JsonPropertyName("denominationUsuelle3UniteLegale")]
        public String DenominationUsuelle3UniteLegale { get; set; }

        /// <summary>
        /// Catégorie juridique de l'entreprise (variable Null pour les personnes physiques.
        /// Nomenclature sur https://www.insee.fr/fr/information/2028129
        /// </summary>
        [JsonPropertyName("categorieJuridiqueUniteLegale"), JsonConverter(typeof(OutilsJson.Int32NullableStringConverter))]
        public Int32? CategorieJuridiqueUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement de la catégorie juridique par rapport à la période précédente
        /// </summary>
        /// <remarks>Toujours à false pour les personnes physiques sauf pour la première période à true par défaut</remarks>
        [JsonPropertyName("changementCategorieJuridiqueUniteLegale")]
        public Boolean ChangementCategorieJuridiqueUniteLegale { get; set; }

        /// <summary>
        /// Activité principale de l'entreprise pendant la période (l'APE est codifiée selon la nomenclature d'Activités Française (NAF))
        /// Nomenclature sur https://www.insee.fr/fr/information/2406147
        /// </summary>
        [JsonPropertyName("activitePrincipaleUniteLegale")]
        public String ActivitePrincipaleUniteLegale { get; set; }

        /// <summary>
        /// Nomenclature de l'activité, permet de savoir à partir de quelle nomenclature est codifiée ActivitePrincipale
        /// </summary>
        [JsonPropertyName("nomenclatureActivitePrincipaleUniteLegale")]
        public NomenclatureActiviteEnum? NomenclatureActivitePrincipaleUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement de l'activité principale par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementActivitePrincipaleUniteLegale")]
        public Boolean ChangementActivitePrincipaleUniteLegale { get; set; }

        /// <summary>
        /// Identifiant du siège pour la période
        /// </summary>
        /// <remarks>Le Siret du siège est obtenu en concaténant le numéro Siren et le Nic</remarks>
        [JsonPropertyName("nicSiegeUniteLegale")]
        public String NicSiegeUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement du NIC du siège par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementNicSiegeUniteLegale")]
        public Boolean ChangementNicSiegeUniteLegale { get; set; }

        /// <summary>
        /// Appartenance de l'unité légale au champ de l'économie sociale et solidaire (ESS)
        /// </summary>
        [JsonPropertyName("economieSocialeSolidaireUniteLegale"), JsonConverter(typeof(OutilsJson.BooleanOorNConverter))]
        public Boolean EconomieSocialeSolidaireUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement de l'ESS par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementEconomieSocialeSolidaireUniteLegale")]
        public Boolean ChangementEconomieSocialeSolidaireUniteLegale { get; set; }

        /// <summary>
        /// Appartenance de l'unité légale au champ société à mission (SM)
        /// </summary>
        [JsonPropertyName("societeMissionUniteLegale"), JsonConverter(typeof(OutilsJson.BooleanNullableOorNConverter))]
        public Boolean? SocieteMissionUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement du champ société à mission par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementSocieteMissionUniteLegale")]
        public Boolean ChangementSocieteMissionUniteLegale { get; set; }

        /// <summary>
        /// Caractère employeur de l'unité légale
        /// </summary>
        [JsonPropertyName("caractereEmployeurUniteLegale")]
        [Obsolete("Plus géré, toujours null", true)]
        internal String CaractereEmployeurUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement du caractère employeur par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementCaractereEmployeurUniteLegale")]
        public Boolean ChangementCaractereEmployeurUniteLegale { get; set; }

        /// <summary>
        /// Indicatrice de changement de la dénomination par rapport à la période précédente
        /// </summary>
        [JsonPropertyName("changementDenominationUsuelleUniteLegale")]
        public Boolean ChangementDenominationUsuelleUniteLegale { get; set; }

        /// <summary>
        /// Numéro Siret de l'établissement
        /// </summary>
        /// <remarks>Le Siret du siège est obtenu en concaténant le numéro Siren et le Nic</remarks>
        public String Siret(String siren)
        {
            return $"{siren}{this.NicSiegeUniteLegale}";
        }

        /// <summary>
        /// Raison sociale (personnes morales et physiques)
        /// </summary>
        /// <returns></returns>
        public String RaisonSociale()
        {
            if (!String.IsNullOrWhiteSpace(this.DenominationUniteLegale))
            {
                // Personne Morale
                return this.DenominationUniteLegale;
            }
            else
            {
                // Personne Physique
                if (!String.IsNullOrWhiteSpace(this.NomUsageUniteLegale))
                {
                    // avec nom d'usage
                    return this.NomUsageUniteLegale;
                }
                else
                {
                    // sans nom d'usage
                    return this.NomUniteLegale;
                }
            }
        }
    }
}
