using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace InseeApiSirene
{
    /// <summary>
    /// Objet renvoyé en cas de succès sur une requête au format CSV
    /// </summary>
    public class ReponseCSV : ReponseBase
    {
        /// <summary>
        /// Contenu CSV
        /// </summary>
        [JsonIgnore]
        public String CSV { get; set; }

        /// <summary>
        /// Création d'une réponse CSV
        /// </summary>
        /// <param name="ex">Exception (en cas d'erreur)</param>
        public ReponseCSV(Exception ex) : base(ex)
        {
        }

        /// <summary>
        /// Création d'une réponse CSV
        /// </summary>
        /// <param name="oHttpResponse">Réponse HTTP reçue</param>
        /// <remarks>En UTF-8</remarks>
        public ReponseCSV(HttpResponseMessage oHttpResponse) : base(oHttpResponse)
        {
        }
    }
}