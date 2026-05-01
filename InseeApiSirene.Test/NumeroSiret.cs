using System.Diagnostics;

namespace InseeApiSirene.Test
{
    /// <summary>
    /// Numéro SIRET
    /// </summary>
    [TestClass]
    public sealed class NumeroSiret
    {
        /// <summary>
        /// Validation d'un SIRET
        /// </summary>
        [TestMethod]
        [DataRow("329 297 09[7] 00035", true, DisplayName = "SIRET valide : 329 297 097 00035")]
        [DataRow("329 297 09[6] 00035", false, DisplayName = "SIRET invalide : 329 297 096 00035")]
        [DataRow("32929709A00035", false, DisplayName = "SIRET invalide : 329 297 09A 00035")]
        [DataRow("3292970900035", false, DisplayName = "SIRET invalide : 329 297 09 00035")]
        [DataRow("32929709000351", false, DisplayName = "SIRET invalide : 329 297 09 000351")]
        public void Tester(String siret, Boolean isValid)
        {
            Boolean bolIsValide = Siret.Tester(Siret.Nettoyer(siret));
            Assert.AreEqual(isValid, bolIsValide, "SIRET validé");
        }

        /// <summary>
        /// Formatage d'un SIRET
        /// </summary>
        [TestMethod]
        [DataRow("329 297 09[7] 00035", "329 297 097 00035", DisplayName = "SIRET formaté : 329 297 097 00035")]
        [DataRow("329 297 09[6] 00035", "329 297 096 00035", DisplayName = "SIRET formaté : 329 297 096 00035")]
        [DataRow("32929709A00035", "", DisplayName = "SIRET formaté : 329 297 09A 00035")]
        [DataRow("3292970900035", "", DisplayName = "SIRET formaté : 329 297 09 00035")]
        [DataRow("32929709000351", "329 297 090 00351", DisplayName = "SIRET formaté : 329 297 090 000351")]
        public void Formater(String siret, String siretFormate)
        {
            Assert.AreEqual(Siret.Formater(siret), siretFormate, "SIRET formaté");
        }

        /// <summary>
        /// Extraction d'un SIREN à partir d'un SIRET
        /// </summary>
        [TestMethod]
        [DataRow("329 297 09[7] 00035", "329297097", "00035", DisplayName = "Extraction SIREN : 329 297 097 00035")]
        [DataRow("329 297 09[6] 00035", "329297096", "00035", DisplayName = "Extraction SIREN : 329 297 096 00035")]
        [DataRow("32929709A00035", "", "", DisplayName = "Extraction SIREN : 329 297 09A 00035")]
        [DataRow("3292970900035", "", "", DisplayName = "Extraction SIREN : 329 297 09 00035")]
        [DataRow("32929709000351", "329297090", "00351", DisplayName = "Extraction SIREN : 329 297 090 00351")]
        public void ExtraireSirenEtNic(String siret, String sirenExtrait, String nicExtrait)
        {
            String siren = Siret.ExtraireSiren(siret, out String nic);
            Assert.AreEqual(siren, sirenExtrait, "SIREN extrait");
            Assert.AreEqual(nic, nicExtrait, "NIC extrait");
        }
    }
}
