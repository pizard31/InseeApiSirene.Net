using System;
using System.Text.RegularExpressions;

namespace InseeApiSirene
{
    /// <summary>
    /// Système d'Identification du Répertoire des ENtreprises
    /// </summary>
    /// <remarks>9 chiffres, composé de l'identifiant unique de l'entreprise (8 premiers chiffres) + clé de contrôle (9ème chiffre, calculée selon l'algorithme de Luhn)</remarks>
    public abstract class Siren
    {
        /// <summary>
        /// Nettoyer un numéro SIREN en supprimant les espaces, tirets, etc. et ne garder que les chiffres
        /// </summary>
        /// <param name="siren">Numéro SIREN à nettoyer</param>
        /// <returns>Numéro SIREN nettoyé</returns>
        public static String Nettoyer(String siren)
        {
            if (String.IsNullOrEmpty(siren)) return String.Empty;
            return Regex.Replace(siren, "[^0-9]", "");
        }

        /// <summary>
        /// Formatage d'un numéro SIREN avec espaces pour une meilleure lisibilité (ex: "326 094 471")
        /// </summary>
        /// <param name="siren">Numéro SIREN à formater</param>
        /// <returns>Le numéro SIREN formaté sous forme de chaîne de caractères</returns>
        public static String Formater(String siren)
        {
            try
            {
                siren = Siren.Nettoyer(siren);
                if (!Siren.Tester(siren, false)) return String.Empty;
                return Regex.Replace(siren, ".{3}", "$0 ").Trim();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                throw new ApplicationException(ex.Message.ToString(), ex);
            }
        }

        /// <summary>
        /// Tester la validité d'un numéro SIREN
        /// </summary>
        /// <param name="siren">Numéro SIREN à valider, penser à le <see cref="Siren.Nettoyer"/> avant</param>
        /// <param name="avecVerificationCle">Indique si la clé de contrôle doit être vérifiée (Algorithme de Luhn)</param>
        /// <returns>Vrai si le SIREN est valide, Faux sinon</returns>
        public static Boolean Tester(String siren, Boolean avecVerificationCle = true)
        {
            try
            {
                // Non vide
                if (String.IsNullOrWhiteSpace(siren)) return false;

                // Composé de 9 chiffres
                if (!Regex.IsMatch(siren, @"^\d{9}$")) return false;

                // Algorithme de Luhn pour la clé de contrôle
                if (!avecVerificationCle) return true;
                Int32 sum = 0;
                Boolean alternate = false;
                // 1.Multiplier chaque chiffre par 2, de droite à gauche, en commençant par le 2ème chiffre
                for (Int32 i = siren.Length - 1; i >= 0; i--)
                {
                    Int32 digit = siren[i] - '0';
                    if (alternate)
                    {
                        digit *= 2;
                        // 2.Si le résultat > 9, soustraire 9
                        if (digit > 9) digit -= 9;
                    }
                    // 3.Somme tous les chiffres obtenus
                    sum += digit;
                    alternate = !alternate;
                }
                // 4.Vérifier que le reste de la division par 10 est égal à 0.
                return (sum % 10) == 0;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                throw new ApplicationException(ex.Message.ToString(), ex);
            }
        }
    }
}
