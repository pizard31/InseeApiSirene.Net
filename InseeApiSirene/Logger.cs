using System;

namespace InseeApiSirene
{
    /// <summary>
    /// Définit un point d'entrée statique pour la journalisation à l'échelle de l'application à différents niveaux de gravité.
    /// </summary>
    /// <remarks>La classe Logger délègue toutes les opérations de journalisation à l'implémentation actuelle de ILogger définie dans la propriété Instance.
    /// Par défaut, un journaliseur de base est utilisé, mais les consommateurs peuvent attribuer un ILogger personnalisé pour modifier le comportement de journalisation globalement.
    /// Cette classe est thread-safe et destinée à être utilisée dans toute l'application pour garantir une journalisation cohérente.</remarks>
    public static class Logger
    {
        /// <summary>
        /// Instance actuelle de ILogger utilisée pour la journalisation.
        /// Par défaut, une implémentation de base est fournie, mais les consommateurs peuvent attribuer une implémentation personnalisée pour modifier le comportement de journalisation globalement.
        /// </summary>
        public static ILogger Instance { get; set; } = new DefaultLogger();

        /// <summary>
        /// Informations de journalisation de niveau debug à des fins de diagnostic.
        /// </summary>
        /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents pour le débogage de l'exécution de l'application.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        public static void Debug(string message, Exception exception = null) => Instance.Debug(message, exception);

        /// <summary>
        /// Informations de journalisation de niveau informationnel.
        /// </summary>
        /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents sur l'opération en cours.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        public static void Info(string message, Exception exception = null) => Instance.Info(message, exception);

        /// <summary>
        /// Informations de journalisation de niveau avertissement.
        /// </summary>
        /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents sur l'avertissement émis.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        public static void Warning(string message, Exception exception = null) => Instance.Warning(message, exception);

        /// <summary>
        /// Informations de journalisation de niveau erreur.
        /// </summary>
        /// <param name="message">Le message d'erreur à journaliser. Ce message doit fournir un contexte suffisant pour comprendre la nature de l'erreur.</param>
        /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
        public static void Error(string message, Exception exception = null) => Instance.Error(message, exception);

        /// <summary>
        /// Informations de journalisation de niveau fatal.
        /// </summary>
        /// <param name="message">Un message descriptif qui fournit un contexte pour l'erreur.</param>
        /// <param name="exception">L'exception à journaliser. Ne peut pas être null.</param>
        public static void Fatal(string message, Exception exception = null) => Instance.Fatal(message, exception);


        /// <summary>
        /// Détermine si la journalisation de niveau debug est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau debug est activée, sinon faux.</returns>
        public static Boolean IsDebugEnabled() => Instance.IsDebugEnabled();

        /// <summary>
        /// Détermine si la journalisation de niveau informationnel est activée.    
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau informationnel est activée, sinon faux.</returns>
        public static Boolean IsInfoEnabled() => Instance.IsInfoEnabled();

        /// <summary>
        /// Détermine si la journalisation de niveau avertissement est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau avertissement est activée, sinon faux.</returns>
        public static Boolean IsWarningEnabled() => Instance.IsWarningEnabled();

        /// <summary>
        /// Détermine si la journalisation de niveau erreur est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau erreur est activée, sinon faux.</returns>
        public static Boolean IsErrorEnabled() => Instance.IsErrorEnabled();

        /// <summary>
        /// Détermine si la journalisation de niveau fatal est activée.
        /// </summary>
        /// <returns>Vrai si la journalisation de niveau fatal est activée, sinon faux.</returns>
        public static Boolean IsFatalEnabled() => Instance.IsFatalEnabled();

        /// <summary>
        /// Classe de log par défaut utilisant la sortie sur "System.Diagnostics.Debug"
        /// </summary>
        public class DefaultLogger : ILogger
        {
            /// <summary>
            /// Informations de journalisation de niveau debug à des fins de diagnostic.
            /// </summary>
            /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents pour le débogage de l'exécution de l'application.</param>
            /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
            public void Debug(String message, Exception exception = null) => System.Diagnostics.Debug.WriteLine($"[InseeApiSirene:🔍] {message}{(exception == null ? "" : "\n" + exception)}");

            /// <summary>
            /// Informations de journalisation de niveau informationnel.
            /// </summary>
            /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents sur l'opération en cours.</param>
            /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
            public void Info(String message, Exception exception = null) => System.Diagnostics.Debug.WriteLine($"[InseeApiSirene:ℹ] {message}{(exception == null ? "" : "\n" + exception)}");

            /// <summary>
            /// Informations de journalisation de niveau avertissement.
            /// </summary>
            /// <param name="message">Le message à journaliser. Il doit fournir un contexte ou des détails pertinents sur l'avertissement émis.</param>
            /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
            public void Warning(String message, Exception exception = null) => System.Diagnostics.Debug.WriteLine($"[InseeApiSirene:⚠] {message}{(exception == null ? "" : "\n" + exception)}");

            /// <summary>
            /// Informations de journalisation de niveau erreur.
            /// </summary>
            /// <param name="message">Le message d'erreur à journaliser. Ce message doit fournir un contexte suffisant pour comprendre la nature de l'erreur.</param>
            /// <param name="exception">L'exception associée au message de journalisation, le cas échéant.</param>
            public void Error(String message, Exception exception = null) => System.Diagnostics.Debug.WriteLine($"[InseeApiSirene:🚨] {message}{(exception == null ? "" : "\n" + exception)}");

            /// <summary>
            /// Informations de journalisation de niveau fatal.
            /// </summary>
            /// <param name="message">Un message descriptif qui fournit un contexte pour l'erreur.</param>
            /// <param name="exception">L'exception à journaliser. Ne peut pas être null.</param>
            public void Fatal(String message, Exception exception = null) => System.Diagnostics.Debug.WriteLine($"[InseeApiSirene:💀] {message}{(exception == null ? "" : "\n" + exception)}");


            /// <summary>
            /// Détermine si la journalisation de niveau debug est activée.
            /// </summary>
            /// <returns>Vrai si la journalisation de niveau debug est activée, sinon faux.</returns>
#if DEBUG
            public Boolean IsDebugEnabled() => true;
#else
            public Boolean IsDebugEnabled() => false;
#endif

            /// <summary>
            /// Détermine si la journalisation de niveau informationnel est activée.    
            /// </summary>
            /// <returns>Vrai si la journalisation de niveau informationnel est activée, sinon faux.</returns>
#if DEBUG
            public Boolean IsInfoEnabled() => true;
#else
            public Boolean IsInfoEnabled() => false;
#endif

            /// <summary>
            /// Détermine si la journalisation de niveau avertissement est activée.
            /// </summary>
            /// <returns>Vrai si la journalisation de niveau avertissement est activée, sinon faux.</returns>
            public Boolean IsWarningEnabled() => true;

            /// <summary>
            /// Détermine si la journalisation de niveau erreur est activée.
            /// </summary>
            /// <returns>Vrai si la journalisation de niveau erreur est activée, sinon faux.</returns>
            public Boolean IsErrorEnabled() => true;

            /// <summary>
            /// Détermine si la journalisation de niveau fatal est activée.
            /// </summary>
            /// <returns>Vrai si la journalisation de niveau fatal est activée, sinon faux.</returns>
            public Boolean IsFatalEnabled() => true;
        }
    }
}
