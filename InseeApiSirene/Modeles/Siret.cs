using System;
using System.Text.RegularExpressions;

namespace InseeApiSirene
{
    /// <summary>
    /// Système d'Identification du Répertoire des ETablissements
    /// Identifie un établissement physique (siège social, succursale, etc.)
    /// </summary>
    /// <remarks>14 chiffres, composé du SIREN (9 chiffres) + NIC (5 chiffres)</remarks>
    public abstract class Siret
    {
        /// <summary>
        /// Décomposer un SIRET en SIREN (+ NIC)
        /// </summary>
        /// <param name="siret">Numéro SIRET à décomposer</param>
        /// <param name="nic">NIC ou chaine vide</param>
        /// <returns>Numéro SIREN ou chaine vide</returns>
        public static String ExtraireSiren(String siret, out String nic)
        {
            try
            {
                // Décomposition SIRET+NIC
                siret = Siret.Nettoyer(siret);
                if (Siret.Tester(siret, false))
                {
                    nic = siret.Substring(9, 5);
                    return siret.Substring(0, 9);
                }
                else
                {
                    nic = String.Empty;
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                throw new ApplicationException(ex.Message.ToString(), ex);
            }
        }

        /// <summary>
        /// Nettoyer un numéro SIRET en supprimant les espaces, tirets, etc. et ne garder que les chiffres
        /// </summary>
        /// <param name="siret">Numéro SIRET à nettoyer</param>
        /// <returns>Numéro SIRET nettoyé</returns>
        public static String Nettoyer(String siret)
        {
            if (String.IsNullOrEmpty(siret)) return String.Empty;
            return Regex.Replace(siret, "[^0-9]", "");
        }

        /// <summary>
        /// Formatage d'un numéro SIRET avec espaces pour une meilleure lisibilité (ex: "123 456 789 00000")
        /// </summary>
        /// <param name="siret">Numéro SIRET à formater</param>
        /// <returns>Le numéro SIRET formaté sous forme de chaîne de caractères</returns>
        public static String Formater(String siret)
        {
            try
            {
                siret = Siret.Nettoyer(siret);
                if (!Siret.Tester(siret, false)) return String.Empty;
                String siren = ExtraireSiren(siret, out String nic);
                if (!Siren.Tester(siren, false)) return String.Empty;
                return $"{Siren.Formater(siren)} {nic}";
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                throw new ApplicationException(ex.Message.ToString(), ex);
            }
        }

        /// <summary>
        /// Tester la validité d'un numéro SIRET
        /// </summary>
        /// <param name="siret">Numéro SIRET à valider, penser à le <see cref="Siret.Nettoyer"/> avant</param>
        /// <param name="avecVerificationCle">Indique si la clé de contrôle du SIREN doit être vérifiée (Algorithme de Luhn)</param>
        /// <returns>Vrai si le SIRET est valide, Faux sinon</returns>
        public static Boolean Tester(String siret, Boolean avecVerificationCle = true)
        {
            try
            {
                // Non vide
                if (String.IsNullOrWhiteSpace(siret)) return false;

                // Composé de 14 chiffres
                if (!Regex.IsMatch(siret, @"^\d{14}$")) return false;

                // Algorithme de Luhn pour la clé de contrôle du SIREN
                if (!avecVerificationCle) return true;
                return Siren.Tester(Siret.ExtraireSiren(siret, out String nic), true);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                throw new ApplicationException(ex.Message.ToString(), ex);
            }
        }
    }
}
