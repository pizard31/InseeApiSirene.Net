using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Enumération des états administratifs
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TrancheEffectifsEnum
    {
        /// <summary>
        /// Unité non-employeuse ou présumée non-employeuse (faute de déclaration reçue)
        /// </summary>
        [JsonStringEnumMemberName("NN")]
        NN,
        /// <summary>
        /// 0 salarié (n'ayant pas d'effectif au 31/12 mais ayant employé des salariés au cours de l'année N)
        /// </summary>
        [JsonStringEnumMemberName("00")]
        S0,
        /// <summary>
        /// 1 ou 2 salariés
        /// </summary>
        [JsonStringEnumMemberName("01")]
        S1ou2,
        /// <summary>
        /// 3 à 5 salariés
        /// </summary>
        [JsonStringEnumMemberName("02")]
        S3a5,
        /// <summary>
        /// 6 à 9 salariés
        /// </summary>
        [JsonStringEnumMemberName("03")]
        S6a9,
        /// <summary>
        /// 10 à 19 salariés
        /// </summary>
        [JsonStringEnumMemberName("11")]
        S10a19,
        /// <summary>
        /// 20 à 49 salariés
        /// </summary>
        [JsonStringEnumMemberName("12")]
        S20a49,
        /// <summary>
        /// 50 à 99 salariés
        /// </summary>
        [JsonStringEnumMemberName("21")]
        S50a99,
        /// <summary>
        /// 100 à 199 salariés
        /// </summary>
        [JsonStringEnumMemberName("22")]
        S100a199,
        /// <summary>
        /// 200 à 249 salariés
        /// </summary>
        [JsonStringEnumMemberName("31")]
        S200a249,
        /// <summary>
        /// 250 à 499 salariés
        /// </summary>
        [JsonStringEnumMemberName("32")]
        S250a499,
        /// <summary>
        /// 500 à 999 salariés
        /// </summary>
        [JsonStringEnumMemberName("41")]
        S500a999,
        /// <summary>
        /// 1 000 à 1 999 salariés
        /// </summary>
        [JsonStringEnumMemberName("42")]
        S1000a1999,
        /// <summary>
        /// 2 000 à 4 999 salariés
        /// </summary>
        [JsonStringEnumMemberName("51")]
        S2000a4999,
        /// <summary>
        /// 5 000 à 9 999 salariés
        /// </summary>
        [JsonStringEnumMemberName("52")]
        S5000a9999,
        /// <summary>
        /// 10 000 salariés et plus
        /// </summary>
        [JsonStringEnumMemberName("53")]
        S10000etPlus,
    }
}
