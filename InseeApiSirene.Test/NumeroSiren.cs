using System.Diagnostics;

namespace InseeApiSirene.Test
{
    /// <summary>
    /// Numéro de SIREN
    /// </summary>
    [TestClass]
    public sealed class NumeroSiren
    {
        /// <summary>
        /// Création d'un SIREN
        /// </summary>
        [TestMethod]
        public void Siren326094471()
        {
            var oSiren = new Siren("326 094 47[1]");
            Assert.IsNotNull(oSiren, "Constructeur objet SIREN");
            Assert.IsTrue(oSiren.IsValide(), "Validité SIREN");
            Trace.WriteLine("var oSiren = new Siren(\"326 094 47[1]\");");
            Trace.WriteLine("oSiren.ToString(); // " + oSiren.ToString());
            Trace.WriteLine("oSiren.AfficherAvecEspaces(); // " + oSiren.AfficherAvecEspaces());
            Trace.WriteLine("oSiren.IsValide(); // " + oSiren.IsValide());
        }

        /// <summary>
        /// Validité d'un SIREN
        /// </summary>
        [TestMethod]
        [DataRow("326 094 47[1]", true, DisplayName = "SIREN valide : 326 094 471")]
        [DataRow("326 094 47[0]", false, DisplayName = "SIREN invalide : 326 094 470")]
        public void SirenValide(String siren, Boolean isValid)
        {
            Boolean bolIsValide = Siren.IsValide(siren);
            Assert.AreEqual(isValid, bolIsValide, "Validité SIREN");
        }
    }
}
