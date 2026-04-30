using System.Diagnostics;

namespace InseeApiSirene.Test
{
    /// <summary>
    /// Numéro de SIRET
    /// </summary>
    [TestClass]
    public sealed class NumerosSiret
    {
        /// <summary>
        /// Création d'un SIRET
        /// </summary>
        [TestMethod]
        public void Siret32929709700035()
        {
            var oSiret = new Siret("329 297 09[7] 00035");
            Assert.IsNotNull(oSiret, "Constructeur objet SIRET");
            Assert.IsTrue(oSiret.IsValide(), "Validité SIRET");
            Trace.WriteLine("var oSiret = new Siret(\"329 297 09[7] 00035\");");
            Trace.WriteLine("oSiret.ToString(); // " + oSiret.ToString());
            Trace.WriteLine("oSiret.Siren; // " + oSiret.Siren);
            Trace.WriteLine("oSiret.NIC; // " + oSiret.NIC);
            Trace.WriteLine("oSiret.AfficherAvecEspaces(); // " + oSiret.AfficherAvecEspaces());
            Trace.WriteLine("oSiret.IsValide(); // " + oSiret.IsValide());
        }
    }
}
