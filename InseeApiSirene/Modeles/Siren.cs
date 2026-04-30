using System;
using System.Text.RegularExpressions;

namespace InseeApiSirene
{
    /// <summary>
    /// Système d'Identification du Répertoire des ENtreprises
    /// </summary>
    /// <remarks>9 chiffres, composé de l'identifiant unique de l'entreprise (8 premiers chiffres) + clé de contrôle (9ème chiffre, calculée selon l'algorithme de Luhn)</remarks>
    public class Siren
    {
        /// <summary>
        /// Numéro SIREN
        /// </summary>
        private String NumeroSiren { get; set; }

        /// <summary>
        /// Numéro SIREN
        /// </summary>
        /// <param name="siren">Numéro SIREN</param>
        public Siren(String siren)
        {
            if (String.IsNullOrWhiteSpace(siren))
            {
                throw new ArgumentException("Le SIREN ne peut pas être vide");
            }
            // Nettoyage : suppression des espaces, tirets, etc.
            if (siren.Length != 9)
            {
                siren = Regex.Replace(siren, "[^0-9]", "");
                if (siren.Length != 9)
                {
                    throw new ArgumentException("Un SIREN est composé de 9 chiffres");
                }
            }
            if (!Siren.IsValide(siren))
            {
                throw new ArgumentException($"Le SIREN '{siren}' est invalide");
            }
            this.NumeroSiren = siren;
        }

        /// <summary>
        /// Affichage du SIREN
        /// </summary>
        /// <returns>Le numéro SIREN sous forme de chaîne de caractères</returns>
        public override String ToString()
        {
            return this.NumeroSiren;
        }

        /// <summary>
        /// Validation d'un numéro SIREN
        /// </summary>
        /// <remarks>Algorithme de Luhn</remarks>
        /// <returns>Vrai si le SIREN est valide, Faux sinon</returns>
        public Boolean IsValide()
        {
            return Siren.IsValide(this.ToString());
        }

        /// <summary>
        /// Affichage du SIREN avec espaces pour une meilleure lisibilité (ex: "326 094 471")
        /// </summary>
        /// <returns>Le numéro SIREN sous forme de chaîne de caractères</returns>
        public string AfficherAvecEspaces()
        {
            return Regex.Replace(this.NumeroSiren, ".{3}", "$0 ").Trim();
        }

        /// <summary>
        /// Validation d'un numéro SIREN
        /// </summary>
        /// <remarks>Algorithme de Luhn</remarks>
        /// <param name="siren">Numéro SIREN à valider</param>
        /// <returns>Vrai si le SIREN est valide, Faux sinon</returns>
        public static Boolean IsValide(String siren)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(siren))
                {
                    return false;
                }

                // Nettoyage (ne garder que les chiffres)
                siren = Regex.Replace(siren, "[^0-9]", "");
                if (siren.Length != 9)
                {
                    return false;
                }

                // Algorithme de Luhn pour la clé de contrôle
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
