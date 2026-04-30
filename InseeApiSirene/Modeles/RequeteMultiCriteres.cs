using System;

namespace InseeApiSirene
{
    /// <summary>
    /// Requête pour la recherche multicritères, utilisée dans les méthodes de recherche avancée
    /// </summary>
    public class RequeteMultiCriteres
    {
        private Int32 P_Nombre = 20;
        private Int32 P_Debut = 0;

        /// <summary>
        /// Contenu de la requête multicritères, voir la documentation pour plus de précisions
        /// </summary>
        public String Query { get; set; } = String.Empty;
        /// <summary>
        /// Date à laquelle on veut obtenir les valeurs des données historisées
        /// </summary>
        public DateTime? Date { get; set; } = null;

        /// <summary>
        /// Liste des champs demandés, séparés par des virgules
        /// </summary>
        public String Champs { get; set; } = String.Empty;
        /// <summary>
        /// Nombre d'éléments demandés dans la réponse, défaut 20
        /// </summary>
        public Int32 Nombre
        {
            get => P_Nombre;
            set
            {
                if (value < 0) throw new ArgumentException("Le nombre minimum de résultats retournés est de 0", nameof(value));
                if (value > 1000) throw new ArgumentException("Le nombre maximum de résultats retournés est de 1000", nameof(value));
                P_Nombre = value;
            }
        }

        /// <summary>
        /// Rang du premier élément demandé dans la réponse, défaut 0
        /// </summary>
        public Int32 Debut
        {
            get => P_Debut;
            set
            {
                if (value < 0) throw new ArgumentException("Le début minimum est de 0", nameof(value));
                if (value > 1000) throw new ArgumentException("Le début maximum est de 1000", nameof(value));
                P_Debut = value;
            }
        }

        /// <summary>
        /// Masque (true) ou affiche (false, par défaut) les attributs qui n'ont pas de valeur, ou null pour utiliser le réglage général
        /// </summary>
        internal Boolean? MasquerValeursNulles { get; set; } = null;

        /// <summary>
        /// Champs sur lesquels des tris seront effectués, séparés par des virgules
        /// </summary>
        public String Tri { get; set; } = String.Empty;

        /// <summary>
        /// Paramètre utilisé pour la pagination profonde, voir la documentation
        /// </summary>
        public String Curseur { get; set; } = String.Empty;

        /// <summary>
        /// Liste des champs sur lesquels des comptages seront effectué, séparés par des virgules
        /// </summary>
        public String FacetteChamp { get; set; } = String.Empty;

        /// <summary>
        /// Création d'une requête multicritères
        /// </summary>
        /// <param name="query">Contenu de la requête multicritères, voir la documentation pour plus de précisions</param>
        /// <param name="date">Date à laquelle on veut obtenir les valeurs des données historisées</param>
        /// <param name="champs">Liste des champs demandés, séparés par des virgules</param>
        /// <param name="tri">Champs sur lesquels des tris seront effectués, séparés par des virgules</param>
        /// <param name="nombre"></param>
        /// <param name="debut">Rang du premier élément demandé dans la réponse, défaut 0</param>
        /// <param name="curseur">Paramètre utilisé pour la pagination profonde, voir la documentation</param>
        /// <param name="facetteChamp">Liste des champs sur lesquels des comptages seront effectué, séparés par des virgules</param>
        /// <remarks>Les valeurs seront formatés automatiquement</remarks>
        public RequeteMultiCriteres(String query, DateTime? date = null, String champs = "", String tri = "", Int32 nombre = 20, Int32 debut = 0, String curseur = "", String facetteChamp = "")
        {
            this.Query = query;
            this.Date = date;
            this.Champs = champs;
            this.Tri = tri;
            this.Nombre = nombre;
            this.Debut = debut;
            this.Curseur = curseur;
            this.FacetteChamp = facetteChamp;
        }

        /// <summary>
        /// Formatage de la requête multicritères
        /// </summary>
        /// <returns>Requête multicritères formaté</returns>
        internal String ToContent(Boolean masquerValeurNullesDefault = true)
        {
            String sContent = String.Empty;
            if (!String.IsNullOrWhiteSpace(this.Query))
            {
                sContent += $"&q={SireneApi.UrlFormat(this.Query)}";
            }
            if (this.Date.HasValue)
            {
                sContent += $"&date={SireneApi.UrlFormat(this.Date.Value, SireneApi.FORMAT_DATE)}";
            }
            if (!String.IsNullOrWhiteSpace(this.Champs))
            {
                sContent += $"&champs={this.Champs}";
            }
            if (!String.IsNullOrWhiteSpace(this.Tri))
            {
                sContent += $"&tri={SireneApi.UrlFormat(this.Tri)}";
            }
            if (this.Nombre != 20)
            {
                sContent += $"&nombre={SireneApi.UrlFormat(this.Nombre)}";
            }
            if (this.Debut != 0)
            {
                sContent += $"&debut={SireneApi.UrlFormat(this.Debut)}";
            }
            if (!String.IsNullOrWhiteSpace(this.Curseur))
            {
                sContent += $"&curseur={SireneApi.UrlFormat(this.Curseur)}";
            }
            if (!String.IsNullOrWhiteSpace(this.FacetteChamp))
            {
                sContent += $"&facette.champ={SireneApi.UrlFormat(this.FacetteChamp)}";
            }
            if (this.MasquerValeursNulles.GetValueOrDefault(masquerValeurNullesDefault))
            {
                sContent += $"&masquerValeursNulles={this.MasquerValeursNulles.GetValueOrDefault(masquerValeurNullesDefault)}";
            }
            if (sContent.StartsWith("&"))
            {
                sContent = sContent.Substring(1);
            }
            return sContent;
        }

        /// <summary>
        /// Formatage de la requête multicritères
        /// </summary>
        /// <returns>Requête multicritères formaté</returns>
        internal String ToUrlQuery(Boolean masquerValeurNullesDefault = true)
        {
            return "?" + this.ToContent(masquerValeurNullesDefault);
        }

        #region Groupes de champs prédéfinis

        /// <summary>
        /// Champs groupés simplifiés pour les unités légales
        /// </summary>
        public enum IdentificationSimplifieeUniteLegaleEnum
        {
            /// <summary>
            /// Numéro Siren de l'entreprise
            /// </summary>
            siren,
            /// <summary>
            /// Date de création de l'unité légale
            /// </summary>
            dateCreationUniteLegale,
            /// <summary>
            /// Raison sociale (personnes morales)
            /// </summary>
            denominationUniteLegale,
            /// <summary>
            /// Nom de naissance pour les personnes physiques pour la période
            /// </summary>
            nomUniteLegale,
            /// <summary>
            /// Premier prénom déclaré pour une personne physique
            /// </summary>
            prenom1UniteLegale,
            /// <summary>
            /// Sexe pour les personnes physiques
            /// </summary>
            sexeUniteLegale,
            /// <summary>
            /// Catégorie juridique de l'entreprise
            /// </summary>
            categorieJuridiqueUniteLegale,
            /// <summary>
            /// Activité principale de l'entreprise
            /// </summary>
            activitePrincipaleUniteLegale,
            /// <summary>
            /// État de l'entreprise
            /// </summary>
            etatAdministratifUniteLegale,
            /// <summary>
            /// Caractère employeur de l'unité légale
            /// </summary>
            caractereEmployeurUniteLegale,
            /// <summary>
            /// Date de la dernière mise à jour effectuée au répertoire Sirene sur le Siren concerné
            /// </summary>
            dateDernierTraitementUniteLegale,
        }

        /// <summary>
        /// Champs groupés standard pour les unités légales
        /// </summary>
        public enum IdentificationStandardUniteLegaleEnum
        {
            /// <summary>
            /// Numéro Siren de l'entreprise
            /// </summary>
            siren,
            /// <summary>
            /// Date de création de l'unité légale
            /// </summary>
            dateCreationUniteLegale,
            /// <summary>
            /// Numéro NIC du siège de l'unité légale
            /// </summary>
            nicSiegeUniteLegale,
            /// <summary>
            /// Raison sociale (personnes morales)
            /// </summary>
            denominationUniteLegale,
            /// <summary>
            /// Premier nom sous lequel l'entreprise est connue du public
            /// </summary>
            denominationUsuelle1UniteLegale,
            /// <summary>
            /// Nom de naissance pour les personnes physiques pour la période
            /// </summary>
            nomUniteLegale,
            /// <summary>
            /// Nom d'usage pour les personnes physiques
            /// </summary>
            nomUsageUniteLegale,
            /// <summary>
            /// Premier prénom déclaré pour une personne physique
            /// </summary>
            prenom1UniteLegale,
            /// <summary>
            /// Sexe pour les personnes physiques
            /// </summary>
            sexeUniteLegale,
            /// <summary>
            /// Catégorie juridique de l'entreprise
            /// </summary>
            categorieJuridiqueUniteLegale,
            /// <summary>
            /// Catégorie à laquelle appartient l'entreprise
            /// </summary>
            categorieEntreprise,
            /// <summary>
            /// Activité principale de l'entreprise
            /// </summary>
            activitePrincipaleUniteLegale,
            /// <summary>
            /// État de l'entreprise
            /// </summary>
            etatAdministratifUniteLegale,
            /// <summary>
            /// Tranche d'effectif salarié de l'unité légale
            /// </summary>
            trancheEffectifsUniteLegale,
            /// <summary>
            /// Date de la dernière mise à jour effectuée au répertoire Sirene sur le Siren concerné
            /// </summary>
            dateDernierTraitementUniteLegale,
        }

        /// <summary>
        /// Champs groupés simplifiés pour les établissements
        /// </summary>
        public enum IdentificationSimplifieeEtablissementEnum
        {
            /// <summary>
            /// Numéro Siren de l'entreprise à laquelle appartient l'établissement
            /// </summary>
            siren,
            /// <summary>
            /// Numéro Siret de l'établissement
            /// </summary>
            siret,
            /// <summary>
            /// Date de création de l'établissement
            /// </summary>
            dateCreationEtablissement,
            /// <summary>
            /// Date de la dernière mise à jour effectuée au répertoire Sirene
            /// </summary>
            dateDernierTraitementEtablissement,
            /// <summary>
            /// État de l'entreprise pendant la période
            /// </summary>
            etatAdministratifUniteLegale,
            /// <summary>
            /// Raison sociale (personnes morales)
            /// </summary>
            denominationUniteLegale,
            /// <summary>
            /// Nom de naissance pour les personnes physiques
            /// </summary>
            nomUniteLegale,
            /// <summary>
            /// Complément d'adresse de l'établissement
            /// </summary>
            complementAdresseEtablissement,
            /// <summary>
            /// Numéro dans la voie
            /// </summary>
            numeroVoieEtablissement,
            /// <summary>
            /// Indice de répétition dans la voie
            /// </summary>
            indiceRepetitionEtablissement,
            /// <summary>
            /// Numéro de la dernière adresse dans la voie
            /// </summary>
            dernierNumeroVoieEtablissement,
            /// <summary>
            /// Indice de répétition de la dernière adresse dans la voie
            /// </summary>
            indiceRepetitionDernierNumeroVoieEtablissement,
            /// <summary>
            /// Type de la voie
            /// </summary>
            typeVoieEtablissement,
            /// <summary>
            /// Libellé de la voie
            /// </summary>
            libelleVoieEtablissement,
            /// <summary>
            /// Code postal
            /// </summary>
            codePostalEtablissement,
            /// <summary>
            /// Libellé de la commune pour les adresses en France
            /// </summary>
            libelleCommuneEtablissement,
            /// <summary>
            /// Libellé complémentaire pour une adresse à l'étranger
            /// </summary>
            libelleCommuneEtrangerEtablissement,
            /// <summary>
            /// Distribution spéciale (BP par ex)
            /// </summary>
            distributionSpecialeEtablissement,
            /// <summary>
            /// Code commune de localisation de l'établissement hors établissements situés à l'étranger
            /// </summary>
            codeCommuneEtablissement,
            /// <summary>
            /// Numéro de Cedex
            /// </summary>
            codeCedexEtablissement,
            /// <summary>
            /// Libellé correspondant au numéro de Cedex (variable codeCedexEtablissement)
            /// </summary>
            libelleCedexEtablissement,
            /// <summary>
            /// Code pays pour les établissements situés à l'étranger
            /// </summary>
            codePaysEtrangerEtablissement,
            /// <summary>
            /// Libellé du pays pour les adresses à l'étranger
            /// </summary>
            libellePaysEtrangerEtablissement,
            /// <summary>
            /// IdentifiantAdresseEtablissement
            /// </summary>
            identifiantAdresseEtablissement,
            /// <summary>
            /// Coordonnée Lambert abscisse de l'établissement
            /// </summary>
            coordonneeLambertAbscisseEtablissement,
            /// <summary>
            /// Coordonnée Lambert ordonnée de l'établissement
            /// </summary>
            coordonneeLambertOrdonneeEtablissement,
            /// <summary>
            /// État administratif de l'établissement pendant la période
            /// </summary>
            etatAdministratifEtablissement,
            /// <summary>
            /// Activité principale de l'établissement pendant la période
            /// </summary>
            activitePrincipaleEtablissement,
            /// <summary>
            /// Caractère employeur de l'établissement
            /// </summary>
            caractereEmployeurEtablissement,
        }

        /// <summary>
        /// Champs groupés standard pour les établissements
        /// </summary>
        public enum IdentificationStandardEtablissementEnum
        {
            /// <summary>
            /// Numéro Siren de l'entreprise à laquelle appartient l'établissement
            /// </summary>
            siren,
            /// <summary>
            /// Numéro Siret de l'établissement
            /// </summary>
            siret,
            /// <summary>
            /// Date de création de l'établissement
            /// </summary>
            dateCreationEtablissement,
            /// <summary>
            /// Tranche d'effectif salarié de l'établissement
            /// </summary>
            trancheEffectifsEtablissement,
            /// <summary>
            /// Date de la dernière mise à jour effectuée au répertoire Sirene
            /// </summary>
            dateDernierTraitementEtablissement,
            /// <summary>
            /// Indicatrice précisant si le Siret est celui de l'établissement siège ou non
            /// </summary>
            etablissementSiege,
            /// <summary>
            /// État de l'entreprise pendant la période
            /// </summary>
            etatAdministratifUniteLegale,
            /// <summary>
            /// Catégorie juridique de l'entreprise
            /// </summary>
            categorieJuridiqueUniteLegale,
            /// <summary>
            /// Raison sociale (personnes morales)
            /// </summary>
            denominationUniteLegale,
            /// <summary>
            /// Sexe pour les personnes physiques
            /// </summary>
            sexeUniteLegale,
            /// <summary>
            /// Nom de naissance pour les personnes physiques
            /// </summary>
            nomUniteLegale,
            /// <summary>
            /// Premier prénom déclaré pour une personne physique
            /// </summary>
            prenom1UniteLegale,
            /// <summary>
            /// Catégorie à laquelle appartient l'entreprise
            /// </summary>
            categorieEntreprise,
            /// <summary>
            /// Complément d'adresse de l'établissement
            /// </summary>
            complementAdresseEtablissement,
            /// <summary>
            /// Numéro dans la voie
            /// </summary>
            numeroVoieEtablissement,
            /// <summary>
            /// Indice de répétition dans la voie
            /// </summary>
            indiceRepetitionEtablissement,
            /// <summary>
            /// Numéro de la dernière adresse dans la voie
            /// </summary>
            dernierNumeroVoieEtablissement,
            /// <summary>
            /// Indice de répétition de la dernière adresse dans la voie
            /// </summary>
            indiceRepetitionDernierNumeroVoieEtablissement,
            /// <summary>
            /// Type de la voie
            /// </summary>
            typeVoieEtablissement,
            /// <summary>
            /// Libellé de la voie
            /// </summary>
            libelleVoieEtablissement,
            /// <summary>
            /// Code postal
            /// </summary>
            codePostalEtablissement,
            /// <summary>
            /// Libellé de la commune pour les adresses en France
            /// </summary>
            libelleCommuneEtablissement,
            /// <summary>
            /// Libellé complémentaire pour une adresse à l'étranger
            /// </summary>
            libelleCommuneEtrangerEtablissement,
            /// <summary>
            /// Distribution spéciale (BP par ex)
            /// </summary>
            distributionSpecialeEtablissement,
            /// <summary>
            /// Code commune de localisation de l'établissement hors établissements situés à l'étranger
            /// </summary>
            codeCommuneEtablissement,
            /// <summary>
            /// Numéro de Cedex
            /// </summary>
            codeCedexEtablissement,
            /// <summary>
            /// Libellé correspondant au numéro de Cedex (variable codeCedexEtablissement)
            /// </summary>
            libelleCedexEtablissement,
            /// <summary>
            /// Code pays pour les établissements situés à l'étranger
            /// </summary>
            codePaysEtrangerEtablissement,
            /// <summary>
            /// Libellé du pays pour les adresses à l'étranger
            /// </summary>
            libellePaysEtrangerEtablissement,
            /// <summary>
            /// IdentifiantAdresseEtablissement
            /// </summary>
            identifiantAdresseEtablissement,
            /// <summary>
            /// Coordonnée Lambert abscisse de l'établissement
            /// </summary>
            coordonneeLambertAbscisseEtablissement,
            /// <summary>
            /// Coordonnée Lambert ordonnée de l'établissement
            /// </summary>
            coordonneeLambertOrdonneeEtablissement,
            /// <summary>
            /// État administratif de l'établissement pendant la période
            /// </summary>
            etatAdministratifEtablissement,
            /// <summary>
            /// Première ligne d'enseigne
            /// </summary>
            enseigne1Etablissement,
            /// <summary>
            /// Nom sous lequel l'activité de l'établissement est connu du public
            /// </summary>
            denominationUsuelleEtablissement,
            /// <summary>
            /// Activité principale de l'établissement pendant la période
            /// </summary>
            activitePrincipaleEtablissement,
        }

        #endregion
    }
}
