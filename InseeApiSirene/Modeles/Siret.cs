using System;
using System.Text.RegularExpressions;

namespace InseeApiSirene
{
    /// <summary>
    /// Système d'Identification du Répertoire des ETablissements
    /// Identifie un établissement physique (siège social, succursale, etc.)
    /// </summary>
    /// <remarks>14 chiffres, composé du SIREN (9 chiffres) + NIC (5 chiffres)</remarks>
    public class Siret
    {
        /// <summary>
        /// Numéro SIREN
        /// </summary>
        public Siren Siren { get; set; }
        /// <summary>
        /// NIC
        /// </summary>
        public String NIC { get; private set; }

        /// <summary>
        /// Numéro SIRET
        /// </summary>
        /// <param name="siret">Numéro SIRET</param>
        public Siret(String siret)
        {
            if (String.IsNullOrWhiteSpace(siret))
            {
                throw new ArgumentException("Le SIRET ne peut pas être vide");
            }
            // Nettoyage : suppression des espaces, tirets, etc.
            if (siret.Length != 9)
            {
                siret = Regex.Replace(siret, "[^0-9]", "");
                if (siret.Length != 14)
                {
                    throw new ArgumentException("Un SIRET est composé de 14 chiffres");
                }
            }

            // Décomposition SIRET+NIC
            this.Siren = new Siren(siret.Substring(0, 9));
            this.NIC = siret.Substring(9, 5);
        }

        /// <summary>
        /// Affichage du SIRET
        /// </summary>
        /// <returns>Le numéro SIRET sous forme de chaîne de caractères</returns>
        public override string ToString()
        {
            return this.Siren.ToString() + this.NIC;
        }

        /// <summary>
        /// Validation d'un numéro SIRET
        /// </summary>
        /// <remarks>Algorithme de Luhn</remarks>
        /// <returns>Vrai si le SIRET est valide, Faux sinon</returns>
        public Boolean IsValide()
        {
            return Siret.IsValide(this.ToString());
        }

        /// <summary>
        /// Affichage du SIRET avec espaces pour une meilleure lisibilité (ex: "123 456 789 00000")
        /// </summary>
        /// <returns>Le numéro SIRET sous forme de chaîne de caractères</returns>
        public string AfficherAvecEspaces()
        {
            return $"{this.Siren.AfficherAvecEspaces()} {this.NIC}";
        }

        /// <summary>
        /// Validation d'un numéro SIRET
        /// </summary>
        /// <remarks>Algorithme de Luhn</remarks>
        /// <param name="siret">Numéro SIRETà valider</param>
        /// <returns>Vrai si le SIRET est valide, Faux sinon</returns>
        public static Boolean IsValide(String siret)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(siret))
                {
                    return false;
                }

                // Nettoyage (ne garder que les chiffres)
                siret = Regex.Replace(siret, "[^0-9]", "");
                if (siret.Length != 14)
                {
                    return false;
                }

                // Validation du SIREN (9 premiers chiffres)
                return Siren.IsValide(siret.Substring(0, 9));
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                throw new ApplicationException(ex.Message.ToString(), ex);
            }
        }
    }
}
