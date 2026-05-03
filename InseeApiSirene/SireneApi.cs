using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InseeApiSirene
{
    /// <summary>
    /// Classe de l'API Sirene (Système Informatique pour le Répertoire des ENtreprises et des Etablissements)
    /// </summary>
    public class SireneApi : IDisposable
    {
        #region Constantes

        /// <summary>
        /// URL de base des l'API Insee
        /// </summary>
        public const String API_URL = "api.insee.fr";

        /// <summary>
        /// NOM de l'API Sirene
        /// </summary>
        /// <remarks>Utilisé dans l'URL de l'API</remarks>
        public const String API_NAME = "api-sirene";

        /// <summary>
        /// Version de l'API Sirence
        /// </summary>
        /// <remarks>Utilisé dans l'URL de l'API</remarks>
        public const String API_VERSION = "3.11";

        /// <summary>
        /// Format de date par défaut
        /// </summary>
        internal const String FORMAT_DATE = "yyyy-MM-dd";

        /// <summary>
        /// Nombre d'appels maximum à l'API en 1 minute (rate-limit)
        /// </summary>
        public const Int32 LIMITE_NB_APPEL_PAR_MINUTE = 30;

        #endregion

        #region Accesseurs

        /// <summary>
        /// Clé d'intégration de l'API Insee
        /// </summary>
        private readonly String ApiKey = null;

        /// <summary>
        /// Utiliser la compression GZip ?
        /// </summary>
        public Boolean CompressionGzip { get; set; } = false;

        /// <summary>
        /// Timeout des appels à l'API Insee (en secondes)
        /// </summary>
        public Int32 Timeout { get; set; } = 30;

        /// <summary>
        /// Masquer les valeurs nulles dans les réponses de l'API (true par défaut) ;
        /// si false, les propriétés avec des valeurs nulles seront présentes dans les réponses JSON, sinon elles seront omises.
        /// Note : ce paramètre n'a d'effet que sur les propriétés de type référence (ex: String, DateTime?, etc.) et pas sur les types valeur (ex: Int32, Boolean, etc.) qui ont une valeur par défaut (ex: 0 pour Int32, false pour Boolean, etc.) et ne peuvent pas être nulles. Par conséquent, les propriétés de type valeur seront toujours présentes dans les réponses JSON même si elles ont une valeur par défaut.
        /// </summary>
        public Boolean MasquerValeurNulles { get; set; } = true;

        #endregion

        #region Constructeur

        /// <summary>
        /// Création d'un objet SireneApi
        /// </summary>
        /// <param name="apiKey">Clé d'authentification de l'API</param>
        /// <param name="masquerValeurNulles">Masque les valeurs nulles dans les réponses de l'API</param>
        /// <param name="compressionGzip">Utiliser la compression GZip pour les réponses de l'API</param>
        /// <param name="timeout">Timeout des appels à l'API (en secondes)</param>
        public SireneApi(String apiKey, Boolean masquerValeurNulles = true, Boolean compressionGzip = false, Int32 timeout = 30)
        {
            this.ApiKey = apiKey;
            this.CompressionGzip = compressionGzip;
            this.Timeout = timeout;
            this.MasquerValeurNulles = masquerValeurNulles;
        }

        /// <summary>
        /// Implémentation de IDisposable
        /// </summary>
        public void Dispose()
        {
            try
            {
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                throw new SireneApiException(ex.Message, ex);
            }
        }

        #endregion

        #region Requête API

        /// <summary>
        /// Appel de l'API Insee
        /// </summary>
        /// <typeparam name="T">Type de réponse attendue</typeparam>
        /// <param name="route">Route de l'API appelée</param>
        /// <param name="query">Pramètres de requête (null pour ignorer)</param>
        /// <param name="method">Méthode d'appel (GET par défaut)</param>
        /// <returns>Réponse en cas de succès, Exception en cas d'erreur</returns>
        private async Task<T> CallApiAsync<T>(String route, String query = null, String method = "GET")
        {
            try
            {
                // Vérification des paramètres d'appel
                if (String.IsNullOrWhiteSpace(this.ApiKey))
                {
                    throw new SireneApiException("Préciser votre clé d'intégration de l'API Insee");
                }
                if (String.IsNullOrWhiteSpace(route))
                {
                    throw new SireneApiException("Préciser la route de l'API à utiliser");
                }

                // Construction de l'URL
                String url = $"https://{API_URL}/{API_NAME}/{API_VERSION}/{route}";
                var oHttpMethod = new HttpMethod(method);
                Boolean isCsv = typeof(T) == typeof(ReponseCSV);

                // Arguments (GET)
                if (oHttpMethod != HttpMethod.Post && !String.IsNullOrWhiteSpace(query))
                {
                    if (query.StartsWith("&"))
                    {
                        query = $"?{query.TrimStart('&')}";
                    }
                    else if (!query.StartsWith("?"))
                    {
                        query = $"?{query}";
                    }
                    url += query;
                }
                Logger.Info($"[{method}] {url}");

                // Construction de la requête avec la clé API
                using (var oHttpClient = new HttpClient())
                {
                    oHttpClient.Timeout = TimeSpan.FromSeconds(this.Timeout);
                    using (var oHttpRequest = new HttpRequestMessage(oHttpMethod, url))
                    {
                        oHttpRequest.Headers.Add("X-INSEE-Api-Key-Integration", this.ApiKey);
                        if (isCsv)
                        {
                            oHttpRequest.Headers.Add("Accept", "text/csv");
                        }
                        else
                        {
                            oHttpRequest.Headers.Add("Accept", "application/json");
                        }
                        if (this.CompressionGzip)
                        {
                            oHttpRequest.Headers.Add("Accept-Encoding", "gzip");
                        }

                        // Contenu de la requête (POST)
                        if (oHttpMethod == HttpMethod.Post && !String.IsNullOrWhiteSpace(query))
                        {
                            oHttpRequest.Content = new StringContent(query, Encoding.UTF8, "application/x-www-form-urlencoded");
                            if (Logger.IsDebugEnabled())
                            {
                                Logger.Debug("Content: " + query);
                            }
                        }

                        // Appel de l'API (asynchrone)
                        using (var oHttpResponse = await oHttpClient.SendAsync(oHttpRequest, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
                        {
                            //TODO: Gestion avancée des "429-Too Many Requests : vous avez dépassé votre quota d'interrogations"
                            if (Logger.IsDebugEnabled())
                            {
                                // Headers
                                foreach (var oHttpResponseHeader in oHttpResponse.Headers)
                                {
                                    // X-Rate-Limit-Limit
                                    // X-Rate-Limit-Reset
                                    // X-Rate-Limit-Remaining
                                    if (oHttpResponseHeader.Key == "X-Rate-Limit-Remaining")
                                    {
                                        if (Int32.TryParse(String.Join("", oHttpResponseHeader.Value), out Int32 iNbRestant))
                                        {
                                            Logger.Debug($"[Header/X-Rate-Limit-Remaining] = {iNbRestant}");
                                        }
                                    }
                                    if (oHttpResponseHeader.Key == "X-Rate-Limit-Reset")
                                    {
                                        if (Int64.TryParse(String.Join("", oHttpResponseHeader.Value), out Int64 tsReset))
                                        {
                                            Logger.Debug($"[Header/X-Rate-Limit-Reset] = {DateTimeOffset.FromUnixTimeMilliseconds(tsReset).LocalDateTime}");
                                        }
                                    }
                                }
                            }

                            // Gestion de la réponse (succès ou erreur)
                            String strResponse = String.Empty;
                            if (this.CompressionGzip)
                            {
                                strResponse = SireneApi.DecompresserGZip(await oHttpResponse.Content.ReadAsByteArrayAsync().ConfigureAwait(false));
                            }
                            else
                            {
                                strResponse = (await oHttpResponse.Content.ReadAsStringAsync().ConfigureAwait(false)).Trim(); // En UTF-8 par défaut
                            }

                            if (isCsv)
                            {
                                // Réponses au format CSV
                                var oReturn = new ReponseCSV(oHttpResponse);
                                if (oHttpResponse.IsSuccessStatusCode)
                                {
                                    oReturn.CSV = strResponse;
                                }
                                else
                                {
                                    oReturn.Message = strResponse;
                                }
                                return (T)Convert.ChangeType(oReturn, typeof(T));
                            }
                            else
                            {
                                // Réponses au format JSON
                                if (!String.IsNullOrWhiteSpace(strResponse))
                                {
                                    try
                                    {
                                        T oReturn = JsonSerializer.Deserialize<T>(strResponse, OutilsJson.SerializerOptions);
                                        if (Logger.IsDebugEnabled())
                                        {
                                            Logger.Debug($"[{oHttpResponse.StatusCode.GetHashCode()}/{oHttpResponse.StatusCode}] => {Environment.NewLine}{JsonSerializer.Serialize(JsonDocument.Parse(strResponse), OutilsJson.SerializerOptions)}");
                                        }
                                        return oReturn;
                                    }
                                    catch (JsonException jex)
                                    {
                                        ReponseBase oError = new ReponseBase(oHttpResponse)
                                        {
                                            Message = jex.Message,
                                            Exception = jex,
                                        };
                                        throw new SireneApiException(oError);
                                    }
                                }
                                else
                                {
                                    ReponseBase oError = new ReponseBase(oHttpResponse)
                                    {
                                        Message = oHttpResponse.ReasonPhrase,
                                    };
                                    throw new SireneApiException(oError);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReponseBase oError = new ReponseBase(ex);
                throw new SireneApiException(oError);
            }
        }

        /// <summary>
        /// Décompression d'une donnée compressée au format GZip
        /// </summary>
        /// <param name="oGzip">Données compressées au format GZip</param>
        /// <returns>Chaîne de caractères décompressée</returns>
        internal static String DecompresserGZip(Byte[] oGzip)
        {
            try
            {
                if (oGzip == null || oGzip.Length == 0)
                {
                    return String.Empty;
                }
                using (var oInputStream = new MemoryStream(oGzip))
                {
                    using (var oGZipStream = new GZipStream(oInputStream, CompressionMode.Decompress))
                    {
                        using (var oResultStream = new MemoryStream())
                        {
                            oGZipStream.CopyTo(oResultStream);
                            return Encoding.UTF8.GetString(oResultStream.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SireneApiException("Erreur lors de la décompression GZip : " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Formater une valeur pour URL
        /// </summary>
        /// <param name="value">Valeur à formater</param>
        /// <returns>Valeur formaté pour URL</returns>
        internal static String UrlFormat(String value)
        {
            return Uri.EscapeDataString(value.Trim());
        }
        /// <summary>
        /// Formater une valeur pour URL
        /// </summary>
        /// <param name="value">Valeur à formater</param>
        /// <returns>Formated Decimal</returns>
        /// <remarks>0.###</remarks>
        internal static String UrlFormat(Decimal value)
        {
            return value.ToString("0.###").Replace(",", ".");
        }
        /// <summary>
        /// Formater une valeur pour URL
        /// </summary>
        /// <param name="value">Valeur à formater</param>
        /// <param name="dateFormat">Format de date</param>
        /// <returns>Formated DateTime</returns>
        internal static String UrlFormat(DateTime value, String dateFormat)
        {
            return SireneApi.UrlFormat(value.ToString(dateFormat));
        }
        
        #endregion

        #region APIs des unités légales (SIREN)

        /// <summary>
        /// Recherche d'une unité légale par son numéro Siren (9 chiffres)
        /// </summary>
        /// <param name="siren">Identifiant de l'unité légale (9 chiffres)</param>
        /// <param name="date">Date à laquelle on veut obtenir les valeurs des données historisées</param>
        /// <param name="champs">Liste des champs demandés, séparés par des virgules, <see cref="RequeteMultiCriteres.IdentificationSimplifieeUniteLegaleEnum"/> et <see cref="RequeteMultiCriteres.IdentificationStandardUniteLegaleEnum"/></param>
        /// <returns>Unité légale trouvée</returns>
        /// <remarks>[GET] /siren/{siren}</remarks>
        public ReponseUniteLegale UniteLegale(String siren, DateTime? date = null, String champs = null)
        {
            try
            {
                String sQuery = String.Empty;
                if (date.HasValue)
                {
                    sQuery += $"&date={SireneApi.UrlFormat(date.Value, SireneApi.FORMAT_DATE)}";
                }
                if (!String.IsNullOrWhiteSpace(champs))
                {
                    sQuery += $"&champs={SireneApi.UrlFormat(champs)}";
                }
                if (!this.MasquerValeurNulles)
                {
                    sQuery += $"&masquerValeursNulles={this.MasquerValeurNulles}";
                }
                if (sQuery.StartsWith("&"))
                {
                    sQuery = "?" + sQuery.Substring(1);
                }

                ReponseUniteLegale oReturn = Task.Run(async () => await this.CallApiAsync<ReponseUniteLegale>($"siren/{SireneApi.UrlFormat(Siren.Nettoyer(siren))}", sQuery).ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.UniteLegale.Siren} : {oReturn.UniteLegale.NombrePeriodesUniteLegale} période(s) trouvée(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseUniteLegale(ex);
            }
        }
        /// <summary>
        /// Recherche d'une unité légale par son numéro Siren (9 chiffres)
        /// </summary>
        /// <param name="siren">Identifiant de l'unité légale (9 chiffres)</param>
        /// <param name="date">Date à laquelle on veut obtenir les valeurs des données historisées</param>
        /// <param name="champs">Liste des champs demandés, séparés par des virgules, <see cref="RequeteMultiCriteres.IdentificationSimplifieeUniteLegaleEnum"/> et <see cref="RequeteMultiCriteres.IdentificationStandardUniteLegaleEnum"/></param>
        /// <returns>Unité légale trouvée, Message d'erreur</returns>
        /// <remarks>[GET] /siren/{siren}</remarks>
        public async Task<ReponseUniteLegale>  UniteLegaleAsync(String siren, DateTime? date = null, String champs = null)
        {
            try
            {
                String sQuery = String.Empty;
                if (date.HasValue)
                {
                    sQuery += $"&date={SireneApi.UrlFormat(date.Value, SireneApi.FORMAT_DATE)}";
                }
                if (!String.IsNullOrWhiteSpace(champs))
                {
                    sQuery += $"&champs={SireneApi.UrlFormat(champs)}";
                }
                if (!this.MasquerValeurNulles)
                {
                    sQuery += $"&masquerValeursNulles={this.MasquerValeurNulles}";
                }
                if (sQuery.StartsWith("&"))
                {
                    sQuery = "?" + sQuery.Substring(1);
                }

                var oReturn = await this.CallApiAsync<ReponseUniteLegale>($"siren/{SireneApi.UrlFormat(Siren.Nettoyer(siren))}", sQuery).ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.UniteLegale.Siren} : {oReturn.UniteLegale.NombrePeriodesUniteLegale} période(s) trouvée(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseUniteLegale(ex);
            }
        }

        /// <summary>
        /// Recherche simplifié d'unités légales à partir de la raison sociale
        /// </summary>
        /// <param name="raisonSociale">Raison sociale recherchée</param>
        /// <returns>Liste des unités légales trouvées</returns>
        /// <remarks>[POST] /siren?q=raisonSociale</remarks>
        public ReponseUnitesLegales UnitesLegales(String raisonSociale)
        {
            try
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres("raisonSociale:" + raisonSociale, null, "siren,denominationUniteLegale,sigleUniteLegale,denominationUsuelle1UniteLegale,denominationUsuelle2UniteLegale,denominationUsuelle3UniteLegale");
                return this.UnitesLegales(oRequeteMultiCriteres);
            }
            catch (Exception ex)
            {
                return new ReponseUnitesLegales(ex);
            }
        }
        /// <summary>
        /// Recherche simplifié d'unités légales à partir de la raison sociale
        /// </summary>
        /// <param name="raisonSociale">Raison sociale recherchée</param>
        /// <returns>Liste des unités légales trouvées</returns>
        /// <remarks>[POST] /siren?q=raisonSociale</remarks>
        public async Task<ReponseUnitesLegales> UnitesLegalesAsync(String raisonSociale)
        {
            try
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres("raisonSociale:" + raisonSociale, null, "siren,denominationUniteLegale,sigleUniteLegale,denominationUsuelle1UniteLegale,denominationUsuelle2UniteLegale,denominationUsuelle3UniteLegale");
                return await this.UnitesLegalesAsync(oRequeteMultiCriteres).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new ReponseUnitesLegales(ex);
            }
        }

        /// <summary>
        /// Recherche d'unités légales multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des unités légales trouvées</returns>
        /// <remarks>[POST] /siren</remarks>
        public ReponseUnitesLegales UnitesLegales(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseUnitesLegales>("siren", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.UnitesLegales.Count} unité(s) légale(s) trouvée(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseUnitesLegales(ex);
            }
        }
        /// <summary>
        /// Recherche d'unités légales multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des unités légales trouvées, Message d'erreur</returns>
        /// <remarks>[POST] /siren</remarks>
        public async Task<ReponseUnitesLegales> UnitesLegalesAsync(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = await this.CallApiAsync<ReponseUnitesLegales>($"siren", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.UnitesLegales.Count} unité(s) légale(s) trouvée(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseUnitesLegales(ex);
            }
        }
        /// <summary>
        /// Recherche d'unités légales multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des unités légales trouvées au format CSV</returns>
        /// <remarks>[POST] /siren</remarks>
        public ReponseCSV UnitesLegalesCsv(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseCSV>("siren", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Fichier CSV");
                    Logger.Debug(oReturn.CSV);
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseCSV(ex);
            }
        }
        /// <summary>
        /// Recherche d'unités légales multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des unités légales trouvées au format CSV, Message d'erreur</returns>
        /// <remarks>[POST] /siren</remarks>
        public async Task<ReponseCSV> UnitesLegalesCsvAsync(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = await this.CallApiAsync<ReponseCSV>($"siren", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Fichier CSV");
                    Logger.Debug(oReturn.CSV);
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseCSV(ex);
            }
        }

        #endregion

        #region APIs des établissements (SIRET)

        /// <summary>
        /// Recherche d'un établissement par son numéro Siret (14 chiffres)
        /// </summary>
        /// <param name="siret">Identifiant de l'établissement (14 chiffres)</param>
        /// <param name="date">Date à laquelle on veut obtenir les valeurs des données historisées</param>
        /// <param name="champs">Liste des champs demandés, séparés par des virgules, <see cref="RequeteMultiCriteres.IdentificationSimplifieeEtablissementEnum"/> et <see cref="RequeteMultiCriteres.IdentificationStandardEtablissementEnum"/></param>
        /// <returns>Établissement trouvé</returns>
        /// <remarks>[GET] /siret/{siret}</remarks>
        public ReponseEtablissement Etablissement(String siret, DateTime? date = null, String champs = null)
        {
            try
            {
                String sQuery = String.Empty;
                if (date.HasValue)
                {
                    sQuery += $"&date={SireneApi.UrlFormat(date.Value, SireneApi.FORMAT_DATE)}";
                }
                if (!String.IsNullOrWhiteSpace(champs))
                {
                    sQuery += $"&champs={SireneApi.UrlFormat(champs)}";
                }
                if (!this.MasquerValeurNulles)
                {
                    sQuery += $"&masquerValeursNulles={this.MasquerValeurNulles}";
                }
                if (sQuery.StartsWith("&"))
                {
                    sQuery = "?" + sQuery.Substring(1);
                }

                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseEtablissement>($"siret/{SireneApi.UrlFormat(siret)}", sQuery).ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.Etablissement.Siret}");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseEtablissement(ex);
            }
        }
        /// <summary>
        /// Recherche d'un établissement par son numéro Siret (14 chiffres)
        /// </summary>
        /// <param name="siret">Identifiant de l'établissement (14 chiffres)</param>
        /// <param name="date">Date à laquelle on veut obtenir les valeurs des données historisées</param>
        /// <param name="champs">Liste des champs demandés, séparés par des virgules, <see cref="RequeteMultiCriteres.IdentificationSimplifieeEtablissementEnum"/> et <see cref="RequeteMultiCriteres.IdentificationStandardEtablissementEnum"/></param>
        /// <returns>Établissement trouvé, Message d'erreur</returns>
        /// <remarks>[GET] /siret/{siret}</remarks>
        public async Task<ReponseEtablissement> EtablissementAsync(String siret, DateTime? date = null, String champs = null)
        {
            try
            {
                String sQuery = String.Empty;
                if (date.HasValue)
                {
                    sQuery += $"&date={SireneApi.UrlFormat(date.Value, SireneApi.FORMAT_DATE)}";
                }
                if (!String.IsNullOrWhiteSpace(champs))
                {
                    sQuery += $"&champs={SireneApi.UrlFormat(champs)}";
                }
                if (!this.MasquerValeurNulles)
                {
                    sQuery += $"&masquerValeursNulles={this.MasquerValeurNulles}";
                }
                if (sQuery.StartsWith("&"))
                {
                    sQuery = "?" + sQuery.Substring(1);
                }

                var oReturn = await this.CallApiAsync<ReponseEtablissement>($"siret/{SireneApi.UrlFormat(siret)}", sQuery).ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.Etablissement.Siret}");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseEtablissement(ex);
            }
        }

        /// <summary>
        /// Recherche simplifié d'établissements à partir de la raison sociale
        /// </summary>
        /// <param name="raisonSociale">Raison sociale recherchée</param>
        /// <returns>Liste des unités légales trouvées</returns>
        /// <remarks>[POST] /siret?q=raisonSociale</remarks>
        public ReponseEtablissements Etablissements(String raisonSociale)
        {
            try
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres("raisonSociale:" + raisonSociale, null, "siret,siren,denominationUniteLegale,sigleUniteLegale,denominationUsuelleEtablissement,enseigne1Etablissement,enseigne2Etablissement,enseigne3Etablissement,denominationUsuelle1UniteLegale,denominationUsuelle2UniteLegale,denominationUsuelle3UniteLegale");
                return this.Etablissements(oRequeteMultiCriteres);
            }
            catch (Exception ex)
            {
                return new ReponseEtablissements(ex);
            }
        }
        /// <summary>
        /// Recherche simplifié d'établissements à partir de la raison sociale
        /// </summary>
        /// <param name="raisonSociale">Raison sociale recherchée</param>
        /// <returns>Liste des établissements trouvés</returns>
        /// <remarks>[POST] /siret?q=raisonSociale</remarks>
        public async Task<ReponseEtablissements> EtablissementsAsync(String raisonSociale)
        {
            try
            {
                var oRequeteMultiCriteres = new RequeteMultiCriteres("raisonSociale:" + raisonSociale, null, "siret,siren,denominationUniteLegale,sigleUniteLegale,denominationUsuelleEtablissement,enseigne1Etablissement,enseigne2Etablissement,enseigne3Etablissement,denominationUsuelle1UniteLegale,denominationUsuelle2UniteLegale,denominationUsuelle3UniteLegale");
                return await this.EtablissementsAsync(oRequeteMultiCriteres).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new ReponseEtablissements(ex);
            }
        }

        /// <summary>
        /// Recherche d'établissements multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des établissements trouvés</returns>
        /// <remarks>[POST] /siret</remarks>
        public ReponseEtablissements Etablissements(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseEtablissements>("siret", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.Etablissements.Count} établissement(s) trouvé(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseEtablissements(ex);
            }
        }
        /// <summary>
        /// Recherche d'établissements multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des établissements trouvés, Message d'erreur</returns>
        /// <remarks>[POST] /siret</remarks>
        public async Task<ReponseEtablissements> EtablissementsAsync(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = await this.CallApiAsync<ReponseEtablissements>($"siret", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.Etablissements.Count} établissement(s) trouvé(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseEtablissements(ex);
            }
        }
        /// <summary>
        /// Recherche d'établissements multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des établissements trouvés au format CSV</returns>
        /// <remarks>[POST] /siret</remarks>
        public ReponseCSV EtablissementsCsv(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseCSV>("siret", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Fichier CSV");
                    Logger.Debug(oReturn.CSV);
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseCSV(ex);
            }
        }
        /// <summary>
        /// Recherche d'établissements multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des établissements trouvés au format CSV, Message d'erreur</returns>
        /// <remarks>[POST] /siret</remarks>
        public async Task<ReponseCSV> EtablissementsCsvAsync(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = await this.CallApiAsync<ReponseCSV>($"siret", requeteMultiCriteres.ToContent(this.MasquerValeurNulles), "POST").ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Fichier CSV");
                    Logger.Debug(oReturn.CSV);
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseCSV(ex);
            }
        }

        /// <summary>
        /// Recherche liens de succession multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des liens de successions trouvés</returns>
        /// <remarks>[GET] /siret/liensSuccession</remarks>
        public ReponseLienSuccession LiensSuccession(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseLienSuccession>("siret/liensSuccession", requeteMultiCriteres.ToUrlQuery(this.MasquerValeurNulles)).ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ {oReturn.LiensSuccession.Count} lien(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseLienSuccession(ex);
            }
        }
        /// <summary>
        /// Recherche liens de succession multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des liens de successions trouvés</returns>
        /// <remarks>[GET] /siret/liensSuccession</remarks>
        public async Task<ReponseLienSuccession> LiensSuccessionAsync(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = await this.CallApiAsync<ReponseLienSuccession>("siret/liensSuccession", requeteMultiCriteres.ToUrlQuery(this.MasquerValeurNulles)).ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                { 
                    Logger.Info($"✅ {oReturn.LiensSuccession.Count} lien(s)");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseLienSuccession(ex);
            }
        }
        /// <summary>
        /// Recherche liens de succession multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des liens de successions trouvés au format CSV</returns>
        /// <remarks>[GET] /siret/liensSuccession</remarks>
        public ReponseCSV LiensSuccessionCsv(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseCSV>("siret/liensSuccession", requeteMultiCriteres.ToUrlQuery(this.MasquerValeurNulles)).ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Fichier CSV");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseCSV(ex);
            }
        }
        /// <summary>
        /// Recherche liens de succession multicritères
        /// </summary>
        /// <param name="requeteMultiCriteres">Paramètres de la requête multicritères</param>
        /// <returns>Liste des liens de successions trouvés au format CSV</returns>
        /// <remarks>[GET] /siret/liensSuccession</remarks>
        public async Task<ReponseCSV> LiensSuccessionCsvAsync(RequeteMultiCriteres requeteMultiCriteres)
        {
            try
            {
                var oReturn = await this.CallApiAsync<ReponseCSV>("siret/liensSuccession", requeteMultiCriteres.ToUrlQuery(this.MasquerValeurNulles)).ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Fichier CSV");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseCSV(ex);
            }
        }

        #endregion

        #region APIs des informations (état des données)

        /// <summary>
        /// APIs des informations (état des données)
        /// </summary>
        /// <returns>Informations</returns>
        /// <remarks>[GET] /informations</remarks>
        public ReponseInformations Informations()
        {
            try
            {
                var oReturn = Task.Run(async () => await this.CallApiAsync<ReponseInformations>("informations").ConfigureAwait(false)).GetAwaiter().GetResult();
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Service {oReturn.EtatService}");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseInformations(ex);
            }
        }
        /// <summary>
        /// APIs des informations (état des données)
        /// </summary>
        /// <returns>Informations, Message d'erreur</returns>
        /// <remarks>[GET] /informations</remarks>
        public async Task<ReponseInformations> InformationsAsync()
        {
            try
            {
                var oReturn = await this.CallApiAsync<ReponseInformations>("informations").ConfigureAwait(false);
                if (!oReturn.AvecErreur())
                {
                    Logger.Info($"✅ Service {oReturn.EtatService}");
                }
                return oReturn;
            }
            catch (Exception ex)
            {
                return new ReponseInformations(ex);
            }
        }

        #endregion
    }
}
