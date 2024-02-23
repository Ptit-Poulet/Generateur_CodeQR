
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{

    public class CodeQr
    {

        /// <summary>
        /// Constructeur
        /// </summary>
        public CodeQr()
        {


        }

        /// <summary>
        /// Choisir le mode d'encodage en analysant la chaine de début
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returns></returns>
        public ChEncoding ChoisirLeMode(string ChaineDebut)
        {
            bool testNumerique = false;
            bool testAlphaNumerique = false;
            bool testByte = false;
            bool testKanji = false;

            int valeurNumerique = 0;
            List<char> lettre = new List<char> {'0','1','2','3','4','5','6','7','8', '9','A','B','C','D','E',
                                                'F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
                                                'U','V','W','X','Y','Z',' ','$','%','*','+','-','.','/',':'};

            //Si la chaîne d'entrée se compose uniquement de chiffres décimaux (0 à 9), utilisez le mode numérique .
            testNumerique = int.TryParse(ChaineDebut, out valeurNumerique);
            /*Si le mode numérique n'est pas applicable et si tous les caractères de la chaîne d'entrée se trouvent
            *dans la colonne de gauche du tableau alphanumérique , utilisez le mode alphanumérique
            */

            foreach (char c in ChaineDebut)
            {
                if (!lettre.Contains(c))
                {
                    testAlphaNumerique = false;
                    break;
                }
                else
                    testAlphaNumerique = true;
            }

            //valeur de retour
            if (testNumerique)
                return ChEncoding.Num;
            else if (testAlphaNumerique)
                return ChEncoding.AlphaNum;
            else if (!testAlphaNumerique)
                return ChEncoding.Byte;
            else if (testKanji)      //la valeur de "testKanji" est à réevaluer
                return ChEncoding.Kanji;


            return ChEncoding.AlphaNum; //valeur de retour par défaut !
        }



        //Méthodes
        /// <summary>
        /// Regroupeent des fonctions de la calsse pour former le codeword
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <param name="mode"></param>
        /// <param name="nbTotalMotCode"></param>
        /// <returns>Codeword prêt à être placé</returns>
        public string PreparationCW(string ChaineDebut, ChEncoding mode, ECLevel niveauCorrection, out int version)
        {


            version = TrouverVersion(ChaineDebut, mode, niveauCorrection);

            string indicateurMode = TrouverIndicateurMode(ChaineDebut, mode);
            string ChaineDonneEncode = indicateurMode;

            string indicateurNbCaractere = TrouverIndicateurNbCaract(ChaineDebut, mode, version);
            ChaineDonneEncode = ChaineDonneEncode + " " + indicateurNbCaractere;

            string donneEnBits = EncoderSelonLeMode(mode, ChaineDebut, version);
            ChaineDonneEncode = ChaineDonneEncode + donneEnBits;
            string Trim = ChaineDonneEncode.Replace(" ", String.Empty);

            string BitsdeDonne = AjouterOctetsPad(Trim, niveauCorrection, version);

            return BitsdeDonne;

        }

        /// <summary>
        /// Trouve l'indicateur selon le mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns>L'indicateur</returns>
        public string TrouverIndicateurMode(string chaine, ChEncoding mode)
        {
            string indicateurMode = "";

            //Trouver le mode
            switch (mode)
            {
                case ChEncoding.Num:
                    indicateurMode += "0001";
                    break;

                case ChEncoding.AlphaNum:
                    indicateurMode += "0010";
                    break;

                case ChEncoding.Byte:
                    indicateurMode += "0100";
                    break;
                case ChEncoding.Kanji:
                    indicateurMode += "1000";
                    break;


            }

            return indicateurMode;
        }

        /// <summary>
        /// Trouver La version
        /// </summary>
        /// <param name="ChainDebut"></param>
        /// <returns>La version</returns>
        public int TrouverVersion(string ChainDebut, ChEncoding mode, ECLevel niveauCorrection)
        {

            int version = QRVersionHelper.GetQRVersionFromInput(ChainDebut, niveauCorrection, mode);

            return version;
        }

        /// <summary>
        /// trouver l'indicateur du nombre de caractère
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returnsL'indicateur nb de caracteres></returns>
        public string TrouverIndicateurNbCaract(string ChaineDebut, ChEncoding mode, int version)
        {

            int bitsNecessaire = nbCaractByMode.GetNbCaract(mode, version);

            //Mettre nb caractère en binaire 
            int Binaire = int.Parse(Convert.ToString(ChaineDebut.Length, 2));

            int Bits = Binaire.ToString().Length;
            int NbCaractere = 0;
            int longueurBinaire = Bits;

            if (longueurBinaire < bitsNecessaire)
            {
                Bits = bitsNecessaire - longueurBinaire;
                NbCaractere = Binaire.ToString("D").Length + Bits;

            }
            string indicateurNbCaractere = Binaire.ToString("D" + NbCaractere.ToString());

            return indicateurNbCaractere;
        }

        /// <summary>
        /// Encode la chaine selon le mode d'encodage
        /// </summary>
        /// <param name="modeSelectionne"></param>
        /// <param name="chaineDebut"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string EncoderSelonLeMode(ChEncoding modeSelectionne, string chaineDebut, int version)
        {
            string chaineEncodee = "";

            switch (modeSelectionne)
            {
                case ChEncoding.Num:
                    chaineEncodee = Encodage.CodageNumerique(chaineDebut);
                    break;
                case ChEncoding.AlphaNum:
                    chaineEncodee = Encodage.CodageAlphaNum(chaineDebut);
                    break;
                case ChEncoding.Byte:
                    chaineEncodee = Encodage.CodageByte(chaineDebut);
                    break;
                case ChEncoding.Kanji:
                    chaineEncodee = Encodage.CodageKanji(chaineDebut);
                    break;
            }
            return chaineEncodee;
        }


        /// <summary>
        /// Ajoute des octets de pad
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returns></returns>
        public string AjouterOctetsPad(string ChaineDebut, ECLevel niveauCorrection, int version)
        {
            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int nbTotalMotCode = group.TotalDataCodeWords;

            string result = "";

            int nbBitsRequis = nbTotalMotCode * 8;

            //Terminateur
            if (ChaineDebut.Length < nbBitsRequis - 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    ChaineDebut += "0";

                }
                int length = ChaineDebut.Length;

            }
            else if (ChaineDebut.Length > nbBitsRequis - 4 && ChaineDebut.Length < nbBitsRequis)
            {
                for (int i = 0; i < nbBitsRequis; i++)
                {
                    ChaineDebut += '0';
                }
            }
            string DonneCodeMultiple = ChaineDebut;

            //Ajout d'octets de pad
            if (ChaineDebut.Length % 8 != 0)
            {

                int remainder = ChaineDebut.Length % 8;

                if (remainder != 0)
                {
                    DonneCodeMultiple += new string('0', 8 - remainder);
                }


            }
            do
            {

                DonneCodeMultiple += "11101100";

                if (DonneCodeMultiple.Length != nbBitsRequis)
                {
                    DonneCodeMultiple += "00010001";
                }


            }
            while (DonneCodeMultiple.Length != nbBitsRequis);

            //Mettre des espaces après chaque 8bits
            for (int i = 0; i < DonneCodeMultiple.Length; i += 8)
            {

                result += DonneCodeMultiple.Substring(i, Math.Min(8, DonneCodeMultiple.Length - i)) + " ";


            }


            return result.Trim();
        }
    }
}

