using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des nomenclatures d'activité
    /// </summary>
    [JsonConverter(typeof(OutilsJson.EnumNullableStringConverter<NomenclatureActiviteEnum>))]
    public enum NomenclatureActiviteEnum
    {
        /// <summary>
        /// NAF rév. 2, 2008
        /// Depuis le 1er janvier 2008
        /// </summary>
        /// <remarks>https://www.insee.fr/fr/information/2120875</remarks>
        [JsonStringEnumMemberName("NAFRev2")]
        NAF_Rev2,
        /// <summary>
        /// NAF rév. 1, 2003
        /// Du 1er janvier 2003 au 31 décembre 2007
        /// </summary>
        /// <remarks>https://www.insee.fr/fr/information/2408180</remarks>
        [JsonStringEnumMemberName("NAFRev1")]
        NAF_Rev1,
        /// <summary>
        /// NAF 1993
        /// Du 1er janvier 1993 au 31 décembre 2002
        /// </summary>
        /// <remarks>https://www.insee.fr/fr/information/2408178</remarks>
        [JsonStringEnumMemberName("NAF1993")]
        NAF_1993,
        /// <summary>
        /// NAP
        /// Du 1er janvier 1973 au 31 décembre 1992
        /// </summary>
        [JsonStringEnumMemberName("NAP")]
        NAP,
    }
}
