namespace InseeApiSirene.Test
{
    /// <summary>
    /// Service informations
    /// </summary>
    [TestClass]
    public sealed class Informations
    {
        /// <summary>
        /// Lecture des informations (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        public async Task ServiceInformationsAsync()
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                MsTestConfig.RespecterRateLimit();
                var oInformations = await oSireneApi.InformationsAsync();
                Assert.IsEmpty(oInformations.Message, oInformations.Message);
                Assert.IsNotNull(oInformations, oInformations.Message);
                Assert.AreEqual(200, oInformations.Header.Statut, "Informations sur le service");
                Assert.AreEqual(EtatEnum.Disponible, oInformations.EtatService, "Informations sur le service");
            }
        }

        /// <summary>
        /// Lecture des informations
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        public void ServiceInformations()
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                MsTestConfig.RespecterRateLimit();
                var oInformations = oSireneApi.Informations();
                Assert.IsEmpty(oInformations.Message, oInformations.Message);
                Assert.IsNotNull(oInformations, oInformations.Message);
                Assert.AreEqual(200, oInformations.Header.Statut, "Informations sur le service");
                Assert.AreEqual(EtatEnum.Disponible, oInformations.EtatService, "Informations sur le service");
            }
        }
    }
}
