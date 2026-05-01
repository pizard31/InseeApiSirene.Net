namespace InseeApiSirene.Test
{
    /// <summary>
    /// Numéro SIREN
    /// </summary>
    [TestClass]
    public sealed class NumeroSiren
    {
        /// <summary>
        /// Validation d'un SIREN
        /// </summary>
        [TestMethod]
        [DataRow("326 094 47[1]", true, DisplayName = "SIREN valide : 326 094 471")]
        [DataRow("326 094 47[0]", false, DisplayName = "SIREN invalide : 326 094 470")]
        [DataRow("32609447A", false, DisplayName = "SIREN invalide : 326 094 47A")]
        [DataRow("32609447", false, DisplayName = "SIREN invalide : 326 094 47")]
        [DataRow("32609447100001", false, DisplayName = "SIREN invalide : 326 094 471 00001")]
        public void Tester(String siren, Boolean isValid)
        {
            Boolean bolIsValide = Siren.Tester(Siren.Nettoyer(siren));
            Assert.AreEqual(isValid, bolIsValide, "SIREN validé");
        }

        /// <summary>
        /// Formatage d'un SIREN
        /// </summary>
        [TestMethod]
        [DataRow("326 094 47[1]", "326 094 471", DisplayName = "SIREN formaté : 326 094 47[1]")]
        [DataRow("326 094 47[0]", "326 094 470", DisplayName = "SIREN formaté : 326 094 47[0]")]
        [DataRow("32609447A", "", DisplayName = "SIREN formaté : 326 094 47A")]
        [DataRow("32609447", "", DisplayName = "SIREN formaté : 326 094 47")]
        [DataRow("32609447100001", "", DisplayName = "SIREN formaté : 326 094 471 00001")]
        public void Formater(String siren, String sirenFormate)
        {
            Assert.AreEqual(Siren.Formater(siren), sirenFormate, "SIREN formaté");
        }
    }
}
