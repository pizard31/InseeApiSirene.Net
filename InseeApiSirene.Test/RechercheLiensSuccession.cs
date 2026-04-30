namespace InseeApiSirene.Test
{
    /// <summary>
    /// Recherche sur les liens de succession
    /// </summary>
    [TestClass]
    public sealed class RechercheLiensSuccession
    {
        /// <summary>
        /// Recherche sur les liens de succession
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("transfertSiege:true", DisplayName = "Recherche de tous les liens de succession correspondant à des transferts de siège")]
        [DataRow("continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec continuité économique")]
        [DataRow("siretEtablissementSuccesseur:53331016500022 AND continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec l'établissement 53331016500022 comme successeur, pour lesquels il y a continuité économique")]
        [DataRow("siretEtablissementSuccesseur:30075733300061 OR siretEtablissementPredecesseur:30075733300061", DisplayName = "Recherche de tous les liens de succession où l'établissement 30075733300061 est soit prédécesseur, soit successeur")]
        [DataRow("siretEtablissementPredecesseur:30077813100023 OR siretEtablissementPredecesseur:30078180400020 OR siretEtablissementPredecesseur:30078425300019 OR siretEtablissementPredecesseur:30078719900037", DisplayName = "Recherche de tous les successeurs des siret 30077813100023, 30078180400020, 30078425300019 et 30078719900037")]
        [DataRow("siretEtablissementPredecesseur:39860733300042", DisplayName = "Recherche de l'établissement successeur du siret 39860733300042")]
        [DataRow("siretEtablissementSuccesseur:70020113000361", DisplayName = "Recherche de l'établissement prédécesseur du siret 70020113000361")]
        [DataRow("siretEtablissementSuccesseur:80309172700013", DisplayName = "Recherche de l'établissement prédécesseur du siret 80309172700013")]
        [DataRow("siretEtablissementPredecesseur:54210765112867", DisplayName = "Recherche de l'établissement successeur du siret 54210765112867")]
        [DataRow("dateLienSuccession:2019-02-02", DisplayName = "Recherche de tous les liens de succession avec une date d'effet au 2 février 2019")]
        [DataRow("dateDernierTraitementLienSuccession:[2025-03-15T00:00:00 TO 2025-03-15T03:59:59]", DisplayName = "Recherche de tous les liens de succession traités le 15 mars 2025 avant 4h du matin")]
        public void LiensSuccession(String query)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query);
                var oReponseLienSuccession = oSireneApi.LiensSuccession(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseLienSuccession.Message, oReponseLienSuccession.Message);
                Assert.IsNotNull(oReponseLienSuccession, oReponseLienSuccession.Message);
                Assert.AreEqual(200, oReponseLienSuccession.Header.Statut, "Lien de succession");
            }
        }

        /// <summary>
        /// Recherche sur les liens de succession avec sortie en CSV
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("transfertSiege:true", DisplayName = "Recherche de tous les liens de succession correspondant à des transferts de siège")]
        [DataRow("continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec continuité économique")]
        [DataRow("siretEtablissementSuccesseur:53331016500022 AND continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec l'établissement 53331016500022 comme successeur, pour lesquels il y a continuité économique")]
        [DataRow("siretEtablissementSuccesseur:30075733300061 OR siretEtablissementPredecesseur:30075733300061", DisplayName = "Recherche de tous les liens de succession où l'établissement 30075733300061 est soit prédécesseur, soit successeur")]
        [DataRow("siretEtablissementPredecesseur:30077813100023 OR siretEtablissementPredecesseur:30078180400020 OR siretEtablissementPredecesseur:30078425300019 OR siretEtablissementPredecesseur:30078719900037", DisplayName = "Recherche de tous les successeurs des siret 30077813100023, 30078180400020, 30078425300019 et 30078719900037")]
        [DataRow("siretEtablissementPredecesseur:39860733300042", DisplayName = "Recherche de l'établissement successeur du siret 39860733300042")]
        [DataRow("siretEtablissementSuccesseur:70020113000361", DisplayName = "Recherche de l'établissement prédécesseur du siret 70020113000361")]
        [DataRow("siretEtablissementSuccesseur:80309172700013", DisplayName = "Recherche de l'établissement prédécesseur du siret 80309172700013")]
        [DataRow("siretEtablissementPredecesseur:54210765112867", DisplayName = "Recherche de l'établissement successeur du siret 54210765112867")]
        [DataRow("dateLienSuccession:2019-02-02", DisplayName = "Recherche de tous les liens de succession avec une date d'effet au 2 février 2019")]
        [DataRow("dateDernierTraitementLienSuccession:[2025-03-15T00:00:00 TO 2025-03-15T03:59:59]", DisplayName = "Recherche de tous les liens de succession traités le 15 mars 2025 avant 4h du matin")]
        public void LiensSuccessionCsv(String query)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query);
                var oReponseLienSuccession = oSireneApi.LiensSuccessionCsv(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseLienSuccession.Message, oReponseLienSuccession.Message);
                Assert.IsNotNull(oReponseLienSuccession);
                Assert.IsNotEmpty(oReponseLienSuccession.CSV, "Lien de succession CSV");
            }
        }

        /// <summary>
        /// Recherche sur les liens de succession
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("transfertSiege:true", DisplayName = "Recherche de tous les liens de succession correspondant à des transferts de siège")]
        [DataRow("continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec continuité économique")]
        [DataRow("siretEtablissementSuccesseur:53331016500022 AND continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec l'établissement 53331016500022 comme successeur, pour lesquels il y a continuité économique")]
        [DataRow("siretEtablissementSuccesseur:30075733300061 OR siretEtablissementPredecesseur:30075733300061", DisplayName = "Recherche de tous les liens de succession où l'établissement 30075733300061 est soit prédécesseur, soit successeur")]
        [DataRow("siretEtablissementPredecesseur:30077813100023 OR siretEtablissementPredecesseur:30078180400020 OR siretEtablissementPredecesseur:30078425300019 OR siretEtablissementPredecesseur:30078719900037", DisplayName = "Recherche de tous les successeurs des siret 30077813100023, 30078180400020, 30078425300019 et 30078719900037")]
        [DataRow("siretEtablissementPredecesseur:39860733300042", DisplayName = "Recherche de l'établissement successeur du siret 39860733300042")]
        [DataRow("siretEtablissementSuccesseur:70020113000361", DisplayName = "Recherche de l'établissement prédécesseur du siret 70020113000361")]
        [DataRow("siretEtablissementSuccesseur:80309172700013", DisplayName = "Recherche de l'établissement prédécesseur du siret 80309172700013")]
        [DataRow("siretEtablissementPredecesseur:54210765112867", DisplayName = "Recherche de l'établissement successeur du siret 54210765112867")]
        [DataRow("dateLienSuccession:2019-02-02", DisplayName = "Recherche de tous les liens de succession avec une date d'effet au 2 février 2019")]
        [DataRow("dateDernierTraitementLienSuccession:[2025-03-15T00:00:00 TO 2025-03-15T03:59:59]", DisplayName = "Recherche de tous les liens de succession traités le 15 mars 2025 avant 4h du matin")]
        public async Task LiensSuccessionAsync(String query)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query);
                var oReponseLienSuccession = await oSireneApi.LiensSuccessionAsync(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseLienSuccession.Message, oReponseLienSuccession.Message);
                Assert.IsNotNull(oReponseLienSuccession, oReponseLienSuccession.Message);
                Assert.AreEqual(200, oReponseLienSuccession.Header.Statut, "Lien de succession");
            }
        }

        /// <summary>
        /// Recherche sur les liens de succession avec sortie en CSV
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("transfertSiege:true", DisplayName = "Recherche de tous les liens de succession correspondant à des transferts de siège")]
        [DataRow("continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec continuité économique")]
        [DataRow("siretEtablissementSuccesseur:53331016500022 AND continuiteEconomique:true", DisplayName = "Recherche de tous les liens de succession avec l'établissement 53331016500022 comme successeur, pour lesquels il y a continuité économique")]
        [DataRow("siretEtablissementSuccesseur:30075733300061 OR siretEtablissementPredecesseur:30075733300061", DisplayName = "Recherche de tous les liens de succession où l'établissement 30075733300061 est soit prédécesseur, soit successeur")]
        [DataRow("siretEtablissementPredecesseur:30077813100023 OR siretEtablissementPredecesseur:30078180400020 OR siretEtablissementPredecesseur:30078425300019 OR siretEtablissementPredecesseur:30078719900037", DisplayName = "Recherche de tous les successeurs des siret 30077813100023, 30078180400020, 30078425300019 et 30078719900037")]
        [DataRow("siretEtablissementPredecesseur:39860733300042", DisplayName = "Recherche de l'établissement successeur du siret 39860733300042")]
        [DataRow("siretEtablissementSuccesseur:70020113000361", DisplayName = "Recherche de l'établissement prédécesseur du siret 70020113000361")]
        [DataRow("siretEtablissementSuccesseur:80309172700013", DisplayName = "Recherche de l'établissement prédécesseur du siret 80309172700013")]
        [DataRow("siretEtablissementPredecesseur:54210765112867", DisplayName = "Recherche de l'établissement successeur du siret 54210765112867")]
        [DataRow("dateLienSuccession:2019-02-02", DisplayName = "Recherche de tous les liens de succession avec une date d'effet au 2 février 2019")]
        [DataRow("dateDernierTraitementLienSuccession:[2025-03-15T00:00:00 TO 2025-03-15T03:59:59]", DisplayName = "Recherche de tous les liens de succession traités le 15 mars 2025 avant 4h du matin")]
        public async Task LiensSuccessionCsvAsync(String query)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query);
                var oReponseLienSuccession = await oSireneApi.LiensSuccessionCsvAsync(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseLienSuccession.Message, oReponseLienSuccession.Message);
                Assert.IsNotNull(oReponseLienSuccession);
                Assert.IsNotEmpty(oReponseLienSuccession.CSV, "Lien de succession CSV");
            }
        }
    }
}
