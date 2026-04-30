using System;

namespace InseeApiSirene
{
    /// <summary>
    /// Définis un contrat pour la journalisation de messages à différents niveaux de gravité, y compris les messages de débogage, d'information, d'avertissement et d'erreur.
    /// </summary>
    /// <remarks>Les implémentations de cette interface doivent s'assurer que les messages sont journalisés en fonction de leur gravité.
    /// Cette interface est destinée à être utilisée par des applications ou des composants nécessitant une journalisation structurée.
    /// Les implémenteurs peuvent fournir des fonctionnalités supplémentaires telles que le formatage des messages, le filtrage ou l'intégration avec des systèmes de journalisation externes.</remarks>
    public interface ILogger
    {
        /// <summary>
        /// Écrit un message de journalisation de niveau debug à des fins de diagnostic.
        /// </summary>
        /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents pour le débogage de l'exécution de l'application.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        /// <remarks>Utilisez cette méthode pour enregistrer des informations utiles pendant le développement ou le dépannage.
        /// Assurez-vous que la configuration de la journalisation permet de capturer les messages de niveau debug, car ils peuvent être ignorés dans les environnements de production.</remarks>
        void Debug(String message, Exception exception = null);

        /// <summary>
        /// Écrit un message de journalisation de niveau informationnel.
        /// </summary>
        /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents sur l'opération en cours.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        /// <remarks>Cette méthode est généralement utilisée pour enregistrer des informations générales qui peuvent aider à surveiller le comportement de l'application.
        /// Assurez-vous que le message est concis et pertinent par rapport à l'événement journalisé.</remarks>
        void Info(String message, Exception exception = null);

        /// <summary>
        /// Écrit un message de journalisation de niveau avertissement.
        /// </summary>
        /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents sur l'avertissement émis.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        /// <remarks>Cette méthode est généralement utilisée pour indiquer des problèmes non critiques qui peuvent nécessiter attention.
        /// Assurez-vous que le message est clair et informatif pour faciliter le dépannage.</remarks>
        void Warning(String message, Exception exception = null);
        
        /// <summary>
        /// Écrit un message de journalisation de niveau erreur.
        /// </summary>
        /// <param name="message">Le message d'erreur à journaliser. Ce message doit fournir un contexte suffisant pour comprendre la nature de l'erreur.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        /// <remarks>Assurez-vous que le message est clair et concis, car il sera utilisé pour le débogage et la surveillance.</remarks>
        void Error(String message, Exception exception = null);
        
        /// <summary>
        /// Écrit un message de journalisation de niveau fatal avec l'exception spécifiée à des fins de diagnostic et de dépannage.
        /// </summary>
        /// <param name="message">Un message descriptif qui fournit un contexte pour l'erreur.</param>
        /// <param name="exception">L'exception à journaliser. Ne peut pas être null.</param>
        /// <remarks>Utilisez cette méthode pour enregistrer les détails des erreurs et les informations sur les exceptions dans le système de journalisation de l'application.
        /// Le message doit fournir un contexte suffisant pour aider à identifier et résoudre le problème.</remarks>
        void Fatal(String message, Exception exception = null);

        /// <summary>
        /// Détermine si la journalisation de niveau debug est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau debug est activée, sinon faux.</returns>
        Boolean IsDebugEnabled();
        
        /// <summary>
        /// Détermine si la journalisation de niveau informationnel est activée.    
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau informationnel est activée, sinon faux.</returns>
        Boolean IsInfoEnabled();

        /// <summary>
        /// Détermine si la journalisation de niveau avertissement est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau avertissement est activée, sinon faux.</returns>
        Boolean IsWarningEnabled();

        /// <summary>
        /// Détermine si la journalisation de niveau erreur est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau erreur est activée, sinon faux.</returns>
        Boolean IsErrorEnabled();

        /// <summary>
        /// Détermine si la journalisation de niveau fatal est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau fatal est activée, sinon faux.</returns>
        Boolean IsFatalEnabled();
    }
}