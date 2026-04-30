using System;

namespace InseeApiSirene
{
    /// <summary>
    /// Exception de l'API Sirene
    /// </summary>
    public class SireneApiException : Exception
    {
        ReponseBase Reponse { get; set; } = null;

        /// <summary>
        /// Exception SireneApi
        /// </summary>
        public SireneApiException()
        {
        }

        /// <summary>
        /// Exception SireneApi avec objet ReponseBase
        /// </summary>
        /// <param name="oReponse">Réponse de base</param>
        public SireneApiException(ReponseBase oReponse) : base(oReponse.Message, oReponse.Exception)
        {
            this.Reponse = oReponse;
        }

        /// <summary>
        /// Exception SireneApi avec Message
        /// </summary>
        /// <param name="message">Message</param>
        public SireneApiException(String message) : base(message)
        {
        }

        /// <summary>
        /// Exception SireneApi avec Message et Exception sous-jacente
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="inner">Exception sous-jacente</param>
        public SireneApiException(String message, Exception inner) : base(message, inner)
        {
        }
    }
}