namespace InseeApiSirene.Test
{
    /// <summary>
    /// Recherche unités légales (SIREN) sur l'API Sirene
    /// </summary>
    [TestClass]
    public sealed class RechercheUniteLegale
    {
        private const String SIREN_TEST = "326094471";

        /// <summary>
        /// Recherche unitaire par SIREN (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow(SIREN_TEST, null, DisplayName = "SIREN : 326 094 471")]
        [DataRow(SIREN_TEST, "14/03/2000", DisplayName = "SIREN : 326 094 471 au 14 mars 2000")]
        [DataRow(SIREN_TEST, "31/12/2999", DisplayName = "SIREN : 326 094 471 sur la situation courante")]
        public async Task UnitaireParSirenAsync(String siren, String date)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oReponseUniteLegale = await oSireneApi.UniteLegaleAsync(new Siren(siren), String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date));
                Assert.IsEmpty(oReponseUniteLegale.Message, oReponseUniteLegale.Message);
                Assert.IsNotNull(oReponseUniteLegale);
                Assert.AreEqual(200, oReponseUniteLegale.Header.Statut, "Unité légale trouvée");
                Assert.AreEqual(siren, oReponseUniteLegale.UniteLegale.Siren, "SIREN de l'unité légale");
            }
        }

        /// <summary>
        /// Recherche unitaire par SIREN
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow(SIREN_TEST, null, DisplayName = "SIREN : 326 094 471")]
        [DataRow(SIREN_TEST, "14/03/2000", DisplayName = "SIREN : 326 094 471 au 14 mars 2000")]
        [DataRow(SIREN_TEST, "31/12/2999", DisplayName = "SIREN : 326 094 471 sur la situation courante")]
        public void UnitaireParSiren(String siren, String date)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oReponseUniteLegale = oSireneApi.UniteLegale(new Siren(siren), String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date));
                Assert.IsEmpty(oReponseUniteLegale.Message, oReponseUniteLegale.Message);
                Assert.IsNotNull(oReponseUniteLegale);
                Assert.AreEqual(200, oReponseUniteLegale.Header.Statut, "Unité légale trouvée");
                Assert.AreEqual(siren, oReponseUniteLegale.UniteLegale.Siren, "SIREN de l'unité légale");
            }
        }

        /// <summary>
        /// Recherche simplifiée par Raison Sociale (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        public async Task SimplifieeParRaisonSocialeAsync()
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oReponseEtablissements = await oSireneApi.EtablissementsAsync("YAC'HUS");
                Assert.IsEmpty(oReponseEtablissements.Message, oReponseEtablissements.Message);
                Assert.IsNotNull(oReponseEtablissements, oReponseEtablissements.Message);
                Assert.AreEqual(200, oReponseEtablissements.Header.Statut, "Établissement(s) trouvé(s)");
            }
        }

        /// <summary>
        /// Recherche simplifiée par Raison Sociale
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        public void SimplifieeParRaisonSociale()
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oReponseEtablissements = oSireneApi.Etablissements("YAC'HUS");
                Assert.IsEmpty(oReponseEtablissements.Message, oReponseEtablissements.Message);
                Assert.IsNotNull(oReponseEtablissements, oReponseEtablissements.Message);
                Assert.AreEqual(200, oReponseEtablissements.Header.Statut, "Établissement(s) trouvé(s)");
            }
        }

        /// <summary>
        /// Recherche multicritère (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("periode(denominationUniteLegale:GAZ)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la dénomination contient ou a contenu le mot GAZ")]
        [DataRow("periode(etatAdministratifUniteLegale:C)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont été cessées")]
        [DataRow("periode(activitePrincipaleUniteLegale:84.23Z OR activitePrincipaleUniteLegale:86.21Z)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises dont l'activité principale est 84.23Z ou 86.21Z, ou l'a été par le passé")]
        [DataRow("periode(activitePrincipaleUniteLegale:68.10Z) AND categorieEntreprise:PME", "31/12/2030", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises exerçant l'activité « marchand de biens » et appartenant à la catégorie PME (Cf. supra : combinaison de variables historisées et non-historisées, paramètre date)")]
        [DataRow("dateCreationUniteLegale:2014-01-01", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la date de création est au 01/01/2014")]
        [DataRow("dateCreationUniteLegale:[1980 TO 2003]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont l'année de création est entre 1980 et 2003")]
        [DataRow("periode(changementDenominationUniteLegale:true AND dateDebut:2017)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont eu un changement de dénomination l'année 2017")]
        [DataRow("periode(denominationUniteLegale:\"LE TIMBRE\")", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la dénomination contient exactement le terme \"LE TIMBRE\"")]
        [DataRow("-siren:1* AND -siren:2*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont le siren ne commence ni par 1 ni par 2 (Etat et collectivités)")]
        [DataRow("dateCreationUniteLegale:*", "", "siren,dateCreationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création est renseignée")]
        [DataRow("-dateCreationUniteLegale:*", "", "siren", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création n'est pas renseignée")]
        public async Task MultiCritereAsync(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseUniteLegale = await oSireneApi.UnitesLegalesAsync(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseUniteLegale.Message, oReponseUniteLegale.Message);
                Assert.IsNotNull(oReponseUniteLegale, oReponseUniteLegale.Message);
                Assert.AreEqual(200, oReponseUniteLegale.Header.Statut, "Unités légales trouvées");
            }
        }

        /// <summary>
        /// Recherche multicritère en CSV (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("periode(denominationUniteLegale:GAZ)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la dénomination contient ou a contenu le mot GAZ")]
        [DataRow("periode(etatAdministratifUniteLegale:C)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont été cessées")]
        [DataRow("periode(activitePrincipaleUniteLegale:84.23Z OR activitePrincipaleUniteLegale:86.21Z)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises dont l'activité principale est 84.23Z ou 86.21Z, ou l'a été par le passé")]
        [DataRow("periode(activitePrincipaleUniteLegale:68.10Z) AND categorieEntreprise:PME", "31/12/2030", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises exerçant l'activité « marchand de biens » et appartenant à la catégorie PME (Cf. supra : combinaison de variables historisées et non-historisées, paramètre date)")]
        [DataRow("dateCreationUniteLegale:2014-01-01", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la date de création est au 01/01/2014")]
        [DataRow("dateCreationUniteLegale:[1980 TO 2003]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont l'année de création est entre 1980 et 2003")]
        [DataRow("periode(changementDenominationUniteLegale:true AND dateDebut:2017)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont eu un changement de dénomination l'année 2017")]
        [DataRow("periode(denominationUniteLegale:\"LE TIMBRE\")", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la dénomination contient exactement le terme \"LE TIMBRE\"")]
        [DataRow("-siren:1* AND -siren:2*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont le siren ne commence ni par 1 ni par 2 (Etat et collectivités)")]
        [DataRow("dateCreationUniteLegale:*", "", "siren,dateCreationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création est renseignée")]
        [DataRow("-dateCreationUniteLegale:*", "", "siren", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création n'est pas renseignée")]
        public async Task MultiCritereCsvAsync(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseUniteLegale = await oSireneApi.UnitesLegalesCsvAsync(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseUniteLegale.Message, oReponseUniteLegale.Message);
                Assert.IsNotNull(oReponseUniteLegale, oReponseUniteLegale.Message);
                Assert.AreEqual(200, oReponseUniteLegale.Statut, "Unités légales trouvées");
            }
        }

        /// <summary>
        /// Recherche multicritère
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("periode(denominationUniteLegale:GAZ)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la dénomination contient ou a contenu le mot GAZ")]
        [DataRow("periode(etatAdministratifUniteLegale:C)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont été cessées")]
        [DataRow("periode(activitePrincipaleUniteLegale:84.23Z OR activitePrincipaleUniteLegale:86.21Z)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises dont l'activité principale est 84.23Z ou 86.21Z, ou l'a été par le passé")]
        [DataRow("periode(activitePrincipaleUniteLegale:68.10Z) AND categorieEntreprise:PME", "31/12/2030", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises exerçant l'activité « marchand de biens » et appartenant à la catégorie PME (Cf. supra : combinaison de variables historisées et non-historisées, paramètre date)")]
        [DataRow("dateCreationUniteLegale:2014-01-01", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la date de création est au 01/01/2014")]
        [DataRow("dateCreationUniteLegale:[1980 TO 2003]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont l'année de création est entre 1980 et 2003")]
        [DataRow("periode(changementDenominationUniteLegale:true AND dateDebut:2017)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont eu un changement de dénomination l'année 2017")]
        [DataRow("periode(denominationUniteLegale:\"LE TIMBRE\")", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la dénomination contient exactement le terme \"LE TIMBRE\"")]
        [DataRow("-siren:1* AND -siren:2*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont le siren ne commence ni par 1 ni par 2 (Etat et collectivités)")]
        [DataRow("dateCreationUniteLegale:*", "", "siren,dateCreationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création est renseignée")]
        [DataRow("-dateCreationUniteLegale:*", "", "siren", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création n'est pas renseignée")]
        public void MultiCritere(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseUniteLegale = oSireneApi.UnitesLegales(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseUniteLegale.Message, oReponseUniteLegale.Message);
                Assert.IsNotNull(oReponseUniteLegale, oReponseUniteLegale.Message);
                Assert.AreEqual(200, oReponseUniteLegale.Header.Statut, "Unités légales trouvées");
            }   
        }

        /// <summary>
        /// Recherche multicritère en CSV
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("periode(denominationUniteLegale:GAZ)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la dénomination contient ou a contenu le mot GAZ")]
        [DataRow("periode(etatAdministratifUniteLegale:C)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont été cessées")]
        [DataRow("periode(activitePrincipaleUniteLegale:84.23Z OR activitePrincipaleUniteLegale:86.21Z)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises dont l'activité principale est 84.23Z ou 86.21Z, ou l'a été par le passé")]
        [DataRow("periode(activitePrincipaleUniteLegale:68.10Z) AND categorieEntreprise:PME", "31/12/2030", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les entreprises exerçant l'activité « marchand de biens » et appartenant à la catégorie PME (Cf. supra : combinaison de variables historisées et non-historisées, paramètre date)")]
        [DataRow("dateCreationUniteLegale:2014-01-01", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont la date de création est au 01/01/2014")]
        [DataRow("dateCreationUniteLegale:[1980 TO 2003]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL dont l'année de création est entre 1980 et 2003")]
        [DataRow("periode(changementDenominationUniteLegale:true AND dateDebut:2017)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les UL qui ont eu un changement de dénomination l'année 2017")]
        [DataRow("periode(denominationUniteLegale:\"LE TIMBRE\")", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la dénomination contient exactement le terme \"LE TIMBRE\"")]
        [DataRow("-siren:1* AND -siren:2*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont le siren ne commence ni par 1 ni par 2 (Etat et collectivités)")]
        [DataRow("dateCreationUniteLegale:*", "", "siren,dateCreationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création est renseignée")]
        [DataRow("-dateCreationUniteLegale:*", "", "siren", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités légales dont la date de création n'est pas renseignée")]
        public void MultiCritereCsv(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseUniteLegale = oSireneApi.UnitesLegalesCsv(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseUniteLegale.Message, oReponseUniteLegale.Message);
                Assert.IsNotNull(oReponseUniteLegale, oReponseUniteLegale.Message);
                Assert.AreEqual(200, oReponseUniteLegale.Statut, "Unités légales trouvées");
            }
        }
    }
}
