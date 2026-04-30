using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet de réponse de base (avec gestion des erreurs)
    /// </summary>
    public class ReponseBase
    {
        /// <summary>
        /// Informations générales sur le résultat de la requête
        /// </summary>
        [JsonPropertyName("header")]
        public Header Header { get; set; } = null;

        /// <summary>
        /// Message d'erreur détaillé
        /// </summary>
        /// <remarks>Pour certaines erreur spécifiques (Header = null)</remarks>
        [JsonPropertyName("message")]
        public String Message { get; set; } = String.Empty;

        /// <summary>
        /// Code HTTP de l'erreur
        /// </summary>
        /// <remarks>Pour certaines erreur spécifiques (Header = null)</remarks>
        [JsonPropertyName("http_status_code")]
        public Int32? Statut { get; set; } = null;

        /// <summary>
        /// Inner exception
        /// </summary>
        /// <remarks>Pour certaines erreur spécifiques (Header = null)</remarks>
        [JsonIgnore]
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Création d'une réponse de base
        /// </summary>
        public ReponseBase()
        {
        }

        /// <summary>
        /// Création d'une réponse de base
        /// </summary>
        /// <param name="oException">Exception reçue</param>
        /// <param name="oHttpResponse">Réponse HTTP reçue</param>
        public ReponseBase(Exception oException, HttpResponseMessage oHttpResponse = null)
        {
            Logger.Error(oException.Message, oException);
            this.Exception = oException;
            this.Message = oException.Message;
            if (oHttpResponse != null)
            {
                this.Statut = oHttpResponse.StatusCode.GetHashCode();
            }
        }

        /// <summary>
        /// Création d'une réponse de base
        /// </summary>
        /// <param name="oHttpResponse">Réponse HTTP reçue</param>
        public ReponseBase(HttpResponseMessage oHttpResponse)
        {
            this.Statut = oHttpResponse.StatusCode.GetHashCode();
        }

        /// <summary>
        /// Détermine si la requête à des erreurs
        /// </summary>
        /// <returns></returns>
        public Boolean HasError()
        {
            Boolean result = false;
            if (this.Header != null)
            {
                result = this.Header.Statut >= 300; // Pas 2xx
            }
            else if (this.Statut.HasValue)
            {
                result = this.Statut >= 300; // Pas 2xx
            }
            else
            {
                
                result = this.Exception != null;
            }
            if (result)
            {
                Logger.Warning($"⚠ {this.ToString()}");
            }
            return result;
        }

        /// <summary>
        /// Affichage formaté de l'erreur, en fonction des informations disponibles dans l'objet
        /// </summary>
        /// <returns>[Statut] Message</returns>
        public override String ToString()
        {
            if (this.Header != null)
            {
                return $"[{this.Header.Statut}] {this.Header.Message}";
            }
            else if (this.Statut.HasValue)
            {
                return $"[{this.Statut}] {this.Message}";
            }
            else
            {   
                return $"{this.Message}";
            }
        }
    }
}
