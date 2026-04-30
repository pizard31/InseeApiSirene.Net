using System;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet représentant un lien de succession entre deux établissements.
    /// </summary>
    public class LienSuccession
    {
        /// <summary>
        /// Numéro Siret de l'établissement prédécesseur
        /// </summary>
        [JsonPropertyName("siretEtablissementPredecesseur")]
        public String SiretEtablissementPredecesseur { get; set; }

        /// <summary>
        /// Numéro Siret de l'établissement successeur
        /// </summary>
        [JsonPropertyName("siretEtablissementSuccesseur")]
        public String SiretEtablissementSuccesseur { get; set; }

        /// <summary>
        /// Date d'effet du lien de succession, au format AAAA-MM-JJ
        /// </summary>
        [JsonPropertyName("dateLienSuccession")]
        public DateTime DateLienSuccession { get; set; }

        /// <summary>
        /// Indicatrice de transfert de siège
        /// </summary>
        [JsonPropertyName("transfertSiege")]
        public Boolean TransfertSiege { get; set; }

        /// <summary>
        /// Indicatrice de continuité économique entre les deux établissements
        /// </summary>
        [JsonPropertyName("continuiteEconomique")]
        public Boolean ContinuiteEconomique { get; set; }

        /// <summary>
        /// Date de traitement du lien de succession
        /// </summary>
        [JsonPropertyName("dateDernierTraitementLienSuccession")]
        public DateTime DateDernierTraitementLienSuccession { get; set; }
    }
}