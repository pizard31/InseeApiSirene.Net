namespace InseeApiSirene.Test
{
    /// <summary>
    /// Recherche établissements (SIRET) sur l'API Sirene
    /// </summary>
    [TestClass]
    public sealed class RechercheEtablissement
    {
        private const String SIRET_TEST = "32929709700035";

        /// <summary>
        /// Recherche unitaire par SIRET (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow(SIRET_TEST, null, DisplayName = "SIRET : 329 297 097 00035")]
        [DataRow(SIRET_TEST, "01/09/1990", DisplayName = "SIRET : 329 297 097 00035 au 1er septembre 1990")]
        [DataRow(SIRET_TEST, "31/12/2999", DisplayName = "SIRET : 329 297 097 00035 sur la situation courante")]
        public async Task UnitaireParSiretAsync(String siret, String date)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oReponseEtablissement = await oSireneApi.EtablissementAsync(new Siret(siret), String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date));
                Assert.IsEmpty(oReponseEtablissement.Message, oReponseEtablissement.Message);
                Assert.IsNotNull(oReponseEtablissement, oReponseEtablissement.Message);
                Assert.AreEqual(200, oReponseEtablissement.Header.Statut, "Établissement trouvé");
                Assert.AreEqual(siret, oReponseEtablissement.Etablissement.Siret, "Mauvais SIRET de l'établissement");
            }
        }

        /// <summary>
        /// Recherche unitaire par SIRET
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow(SIRET_TEST, null, DisplayName = "SIRET : 329 297 097 00035")]
        [DataRow(SIRET_TEST, "01/09/1990", DisplayName = "SIRET : 329 297 097 00035 au 1er septembre 1990")]
        [DataRow(SIRET_TEST, "31/12/2999", DisplayName = "SIRET : 329 297 097 00035 sur la situation courante")]
        public void UnitaireParSiret(String siret, String date)
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oReponseEtablissement = oSireneApi.Etablissement(new Siret(siret), String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date));
                Assert.IsEmpty(oReponseEtablissement.Message, oReponseEtablissement.Message);
                Assert.IsNotNull(oReponseEtablissement, oReponseEtablissement.Message);
                Assert.AreEqual(200, oReponseEtablissement.Header.Statut, "Établissement trouvé");
                Assert.AreEqual(siret, oReponseEtablissement.Etablissement.Siret, "SIRET de l'établissement");
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
                var oReponseUnitesLegales = await oSireneApi.UnitesLegalesAsync("YAC'HUS");
                Assert.IsEmpty(oReponseUnitesLegales.Message, oReponseUnitesLegales.Message);
                Assert.IsNotNull(oReponseUnitesLegales, oReponseUnitesLegales.Message);
                Assert.AreEqual(200, oReponseUnitesLegales.Header.Statut, "Unité(s) légale(s) trouvée(s)");
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
                var oReponseUnitesLegales = oSireneApi.UnitesLegales("YAC'HUS");
                Assert.IsEmpty(oReponseUnitesLegales.Message, oReponseUnitesLegales.Message);
                Assert.IsNotNull(oReponseUnitesLegales, oReponseUnitesLegales.Message);
                Assert.AreEqual(200, oReponseUnitesLegales.Header.Statut, "Unité(s) légale(s) trouvée(s)");
            }
        }

        /// <summary>
        /// Recherche multicritère (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("siren:3*", "", "siret,denominationUniteLegale", "", 20, 0, "*", "", DisplayName = "Recherche de tous les établissement dont le siren commence par 3")]
        [DataRow("siren:775672272", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements du Siren 775672272")]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("codeCommuneEtablissement:92046", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de la commune de Malakoff (code commune=92046)")]
        [DataRow("periode(activitePrincipaleEtablissement:33.01)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont le code de l'activité principale a été (ou est*) 33.01")]
        [DataRow("-categorieJuridiqueUniteLegale:1000", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale est considérée comme une personne morale")]
        [DataRow("-periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui n'ont jamais été fermés")]
        [DataRow("categorieJuridiqueUniteLegale:5510 OR categorieJuridiqueUniteLegale:5520", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements relevant des catégories juridiques 5510 et 5520")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z AND etatAdministratifEtablissement:A)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont au moins une période où leur état est « actif » et leur activité principale est 84.23Z")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z) AND -periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont moins une période dont l'activitePrincipaleEtablissement est 84.23Z et qui n'ont jamais été fermés")]
        [DataRow("codeCommuneEtablissement:92046 AND categorieJuridiqueUniteLegale:9220", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de Malakoff dont la dernière catégorie juridique est 9220")]
        [DataRow("dateDernierTraitementEtablissement:2026-02", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements mis à jour au mois de février 2026 et non mis à jour depuis")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, y compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:{DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, non compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, Y compris DUPONT et non compris DURAND")]
        [DataRow("categorieJuridiqueUniteLegale:1000 AND activitePrincipaleUniteLegale:86.21Z AND nombrePeriodesEtablissement:[12 TO 20]", "", "siret,nombrePeriodesEtablissement", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de médecins généralistes dont le nombre de périodes va de 12 à 20 (inclus)")]
        [DataRow("activitePrincipaleUniteLegale:8*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont l'activité principale commence par 8")]
        [DataRow("-sigleUniteLegale:*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont le sigle n'est pas rempli")]
        [DataRow("denominationUniteLegale:lami*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont la dénomination commence par \"LAMI\"")]
        [DataRow("sigleUniteLegale:???", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle sur 3 positions")]
        [DataRow("sigleUniteLegale:FC?", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle qui commence par FC et est sur 3 positions exactement")]
        [DataRow("prenom1UniteLegale:MICKAEL~ AND -prenom1UniteLegale:MICKAEL", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a comme prenom1UniteLegale MICKAEL à deux caractères près, mais pas MICKAEL exactement")]
        [DataRow("sigleUniteLegale:PAUL~1", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a pour sigle PAUL à une erreur près")]
        [DataRow("denominationUniteLegale:\"bleu le\"", "", "denominationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche exacte)")]
        [DataRow("denominationUniteLegale:\"bleu le\"~2", "", "denominationUniteLegale", "", 100, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche approximative)")]
        [DataRow("denominationUniteLegale:yst~ AND denominationUniteLegale:anotwer~ AND denominationUniteLegale:copany~", "", "denominationUniteLegale", "", 1000, 0, "", "", DisplayName = "Recherche approximative sur plusieurs termes")]
        public async Task MultiCritereAsync(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseEtablissements = await oSireneApi.EtablissementsAsync(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseEtablissements.Message, oReponseEtablissements.Message);
                Assert.IsNotNull(oReponseEtablissements, oReponseEtablissements.Message);
                Assert.AreEqual(200, oReponseEtablissements.Header.Statut, "Établissements trouvés");
            }
        }

        /// <summary>
        /// Recherche multicritère en CSV (ASYNC)
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("siren:3*", "", "siret,denominationUniteLegale", "", 20, 0, "*", "", DisplayName = "Recherche de tous les établissement dont le siren commence par 3")]
        [DataRow("siren:775672272", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements du Siren 775672272")]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("codeCommuneEtablissement:92046", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de la commune de Malakoff (code commune=92046)")]
        [DataRow("periode(activitePrincipaleEtablissement:33.01)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont le code de l'activité principale a été (ou est*) 33.01")]
        [DataRow("-categorieJuridiqueUniteLegale:1000", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale est considérée comme une personne morale")]
        [DataRow("-periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui n'ont jamais été fermés")]
        [DataRow("categorieJuridiqueUniteLegale:5510 OR categorieJuridiqueUniteLegale:5520", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements relevant des catégories juridiques 5510 et 5520")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z AND etatAdministratifEtablissement:A)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont au moins une période où leur état est « actif » et leur activité principale est 84.23Z")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z) AND -periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont moins une période dont l'activitePrincipaleEtablissement est 84.23Z et qui n'ont jamais été fermés")]
        [DataRow("codeCommuneEtablissement:92046 AND categorieJuridiqueUniteLegale:9220", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de Malakoff dont la dernière catégorie juridique est 9220")]
        [DataRow("dateDernierTraitementEtablissement:2026-02", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements mis à jour au mois de février 2026 et non mis à jour depuis")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, y compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:{DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, non compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, Y compris DUPONT et non compris DURAND")]
        [DataRow("categorieJuridiqueUniteLegale:1000 AND activitePrincipaleUniteLegale:86.21Z AND nombrePeriodesEtablissement:[12 TO 20]", "", "siret,nombrePeriodesEtablissement", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de médecins généralistes dont le nombre de périodes va de 12 à 20 (inclus)")]
        [DataRow("activitePrincipaleUniteLegale:8*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont l'activité principale commence par 8")]
        [DataRow("-sigleUniteLegale:*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont le sigle n'est pas rempli")]
        [DataRow("denominationUniteLegale:lami*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont la dénomination commence par \"LAMI\"")]
        [DataRow("sigleUniteLegale:???", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle sur 3 positions")]
        [DataRow("sigleUniteLegale:FC?", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle qui commence par FC et est sur 3 positions exactement")]
        [DataRow("prenom1UniteLegale:MICKAEL~ AND -prenom1UniteLegale:MICKAEL", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a comme prenom1UniteLegale MICKAEL à deux caractères près, mais pas MICKAEL exactement")]
        [DataRow("sigleUniteLegale:PAUL~1", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a pour sigle PAUL à une erreur près")]
        [DataRow("denominationUniteLegale:\"bleu le\"", "", "denominationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche exacte)")]
        [DataRow("denominationUniteLegale:\"bleu le\"~2", "", "denominationUniteLegale", "", 100, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche approximative)")]
        [DataRow("denominationUniteLegale:yst~ AND denominationUniteLegale:anotwer~ AND denominationUniteLegale:copany~", "", "denominationUniteLegale", "", 1000, 0, "", "", DisplayName = "Recherche approximative sur plusieurs termes")]
        public async Task MultiCritereCsvAsync(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseCSV = await oSireneApi.EtablissementsCsvAsync(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseCSV.Message, oReponseCSV.Message);
                Assert.IsNotNull(oReponseCSV, oReponseCSV.Message);
                Assert.IsNotEmpty(oReponseCSV.CSV, "Données CSV");
            }
        }

        /// <summary>
        /// Recherche multicritère
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("siren:3*", "", "siret,denominationUniteLegale", "", 20, 0, "*", "", DisplayName = "Recherche de tous les établissement dont le siren commence par 3")]
        [DataRow("siren:775672272", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements du Siren 775672272")]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("codeCommuneEtablissement:92046", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de la commune de Malakoff (code commune=92046)")]
        [DataRow("periode(activitePrincipaleEtablissement:33.01)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont le code de l'activité principale a été (ou est*) 33.01")]
        [DataRow("-categorieJuridiqueUniteLegale:1000", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale est considérée comme une personne morale")]
        [DataRow("-periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui n'ont jamais été fermés")]
        [DataRow("categorieJuridiqueUniteLegale:5510 OR categorieJuridiqueUniteLegale:5520", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements relevant des catégories juridiques 5510 et 5520")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z AND etatAdministratifEtablissement:A)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont au moins une période où leur état est « actif » et leur activité principale est 84.23Z")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z) AND -periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont moins une période dont l'activitePrincipaleEtablissement est 84.23Z et qui n'ont jamais été fermés")]
        [DataRow("codeCommuneEtablissement:92046 AND categorieJuridiqueUniteLegale:9220", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de Malakoff dont la dernière catégorie juridique est 9220")]
        [DataRow("dateDernierTraitementEtablissement:2026-02", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements mis à jour au mois de février 2026 et non mis à jour depuis")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, y compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:{DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, non compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, Y compris DUPONT et non compris DURAND")]
        [DataRow("categorieJuridiqueUniteLegale:1000 AND activitePrincipaleUniteLegale:86.21Z AND nombrePeriodesEtablissement:[12 TO 20]", "", "siret,nombrePeriodesEtablissement", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de médecins généralistes dont le nombre de périodes va de 12 à 20 (inclus)")]
        [DataRow("activitePrincipaleUniteLegale:8*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont l'activité principale commence par 8")]
        [DataRow("-sigleUniteLegale:*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont le sigle n'est pas rempli")]
        [DataRow("denominationUniteLegale:lami*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont la dénomination commence par \"LAMI\"")]
        [DataRow("sigleUniteLegale:???", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle sur 3 positions")]
        [DataRow("sigleUniteLegale:FC?", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle qui commence par FC et est sur 3 positions exactement")]
        [DataRow("prenom1UniteLegale:MICKAEL~ AND -prenom1UniteLegale:MICKAEL", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a comme prenom1UniteLegale MICKAEL à deux caractères près, mais pas MICKAEL exactement")]
        [DataRow("sigleUniteLegale:PAUL~1", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a pour sigle PAUL à une erreur près")]
        [DataRow("denominationUniteLegale:\"bleu le\"", "", "denominationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche exacte)")]
        [DataRow("denominationUniteLegale:\"bleu le\"~2", "", "denominationUniteLegale", "", 100, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche approximative)")]
        [DataRow("denominationUniteLegale:yst~ AND denominationUniteLegale:anotwer~ AND denominationUniteLegale:copany~", "", "denominationUniteLegale", "", 1000, 0, "", "", DisplayName = "Recherche approximative sur plusieurs termes")]
        public void MultiCritere(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseEtablissements = oSireneApi.Etablissements(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseEtablissements.Message, oReponseEtablissements.Message);
                Assert.IsNotNull(oReponseEtablissements, oReponseEtablissements.Message);
                Assert.AreEqual(200, oReponseEtablissements.Header.Statut, "Établissements trouvés");
            }
        }

        /// <summary>
        /// Recherche multicritère en CSV
        /// </summary>
        [TestMethod]
        [Timeout(2000, CooperativeCancellation = true)]
        [DataRow("siren:3*", "", "siret,denominationUniteLegale", "", 20, 0, "*", "", DisplayName = "Recherche de tous les établissement dont le siren commence par 3")]
        [DataRow("siren:775672272", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements du Siren 775672272")]
        [DataRow("unitePurgeeUniteLegale:true", "", "", "", 20, 0, "", "", DisplayName = "Recherche de toutes les unités purgées")]
        [DataRow("codeCommuneEtablissement:92046", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de la commune de Malakoff (code commune=92046)")]
        [DataRow("periode(activitePrincipaleEtablissement:33.01)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont le code de l'activité principale a été (ou est*) 33.01")]
        [DataRow("-categorieJuridiqueUniteLegale:1000", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale est considérée comme une personne morale")]
        [DataRow("-periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui n'ont jamais été fermés")]
        [DataRow("categorieJuridiqueUniteLegale:5510 OR categorieJuridiqueUniteLegale:5520", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements relevant des catégories juridiques 5510 et 5520")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z AND etatAdministratifEtablissement:A)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont au moins une période où leur état est « actif » et leur activité principale est 84.23Z")]
        [DataRow("periode(activitePrincipaleEtablissement:84.23Z) AND -periode(etatAdministratifEtablissement:F)", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements qui ont moins une période dont l'activitePrincipaleEtablissement est 84.23Z et qui n'ont jamais été fermés")]
        [DataRow("codeCommuneEtablissement:92046 AND categorieJuridiqueUniteLegale:9220", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de Malakoff dont la dernière catégorie juridique est 9220")]
        [DataRow("dateDernierTraitementEtablissement:2026-02", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements mis à jour au mois de février 2026 et non mis à jour depuis")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND]", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, y compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:{DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, non compris DUPONT et DURAND")]
        [DataRow("nomUsageUniteLegale:[DUPONT TO DURAND}", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les etablissements d'UL dont le nom d'usage va de DUPONT à DURAND, Y compris DUPONT et non compris DURAND")]
        [DataRow("categorieJuridiqueUniteLegale:1000 AND activitePrincipaleUniteLegale:86.21Z AND nombrePeriodesEtablissement:[12 TO 20]", "", "siret,nombrePeriodesEtablissement", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements de médecins généralistes dont le nombre de périodes va de 12 à 20 (inclus)")]
        [DataRow("activitePrincipaleUniteLegale:8*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont l'activité principale commence par 8")]
        [DataRow("-sigleUniteLegale:*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont le sigle n'est pas rempli")]
        [DataRow("denominationUniteLegale:lami*", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements des unités légales dont la dénomination commence par \"LAMI\"")]
        [DataRow("sigleUniteLegale:???", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle sur 3 positions")]
        [DataRow("sigleUniteLegale:FC?", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a un sigle qui commence par FC et est sur 3 positions exactement")]
        [DataRow("prenom1UniteLegale:MICKAEL~ AND -prenom1UniteLegale:MICKAEL", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a comme prenom1UniteLegale MICKAEL à deux caractères près, mais pas MICKAEL exactement")]
        [DataRow("sigleUniteLegale:PAUL~1", "", "", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a pour sigle PAUL à une erreur près")]
        [DataRow("denominationUniteLegale:\"bleu le\"", "", "denominationUniteLegale", "", 20, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche exacte)")]
        [DataRow("denominationUniteLegale:\"bleu le\"~2", "", "denominationUniteLegale", "", 100, 0, "", "", DisplayName = "Recherche de tous les établissements dont l'unité légale a une dénomination sociale comprenant \"BLEU LE\" (recherche approximative)")]
        [DataRow("denominationUniteLegale:yst~ AND denominationUniteLegale:anotwer~ AND denominationUniteLegale:copany~", "", "denominationUniteLegale", "", 1000, 0, "", "", DisplayName = "Recherche approximative sur plusieurs termes")]
        public void MultiCritereCsv(String query, String date = "", String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            using (var oSireneApi = new SireneApi(MsTestConfig.GetApiKey()))
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres(query, String.IsNullOrWhiteSpace(date) ? null : Convert.ToDateTime(date), champs, tri, nombre, debut, curseur, facetteChamp);
                var oReponseCSV = oSireneApi.EtablissementsCsv(oRequeteMultiCriteres);
                Assert.IsEmpty(oReponseCSV.Message, oReponseCSV.Message);
                Assert.IsNotNull(oReponseCSV, oReponseCSV.Message);
                Assert.IsNotEmpty(oReponseCSV.CSV, "Données CSV");
            }
        }
    }
}
