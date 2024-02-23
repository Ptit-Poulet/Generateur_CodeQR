
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

        //Attributs
        string _chaineDebut;
        ECLevel _niveauCorrection;


        public CodeQr(string chaineDebut, ECLevel niveauCorrection)
        {
            _chaineDebut = chaineDebut;
            _niveauCorrection = niveauCorrection;
        }


        /// <summary>
        /// Choisir le mode d'encodage en analysant la chaine de début
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns>ChEncoding choisie</returns>
        public ChEncoding ChoisirLeMode(string chaineDebut)
        {
            bool testNumerique = false;
            bool testAlphaNumerique = false;
            bool testByte = false;
            bool testKanji = false;

            List<char> lettre = new List<char> {'0','1','2','3','4','5','6','7','8', '9','A','B','C','D','E',
                                                'F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
                                                'U','V','W','X','Y','Z',' ','$','%','*','+','-','.','/',':'};

            //Si la chaîne d'entrée se compose uniquement de chiffres décimaux (0 à 9), utilisez le mode numérique .
            testNumerique = int.TryParse(chaineDebut, out int valeurNumerique);
            /*Si le mode numérique n'est pas applicable et si tous les caractères de la chaîne d'entrée se trouvent
            *dans la colonne de gauche du tableau alphanumérique , utilisez le mode alphanumérique
            */

            foreach (char c in chaineDebut)
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
        /// <param name="chaineDebut"></param>
        /// <param name="mode"></param>
        /// <param name="niveauCorrection"></param>
        /// <param name="version"></param>
        /// <returns>la chaine finale encodée en binaire </returns>
        public string EncoderChaine(string chaineDebut, ChEncoding mode, ECLevel niveauCorrection, out int version)
        {


            version = TrouverVersion(chaineDebut, mode, niveauCorrection);

            string indicateurMode = TrouverIndicateurMode(mode);
            string chaineDonneEncode = indicateurMode;

            string indicateurNbCaractere = TrouverIndicateurNbCaract(chaineDebut, mode, version);
            chaineDonneEncode = chaineDonneEncode + " " + indicateurNbCaractere;

            string donneEnBits = EncoderSelonLeMode(chaineDebut, mode);
            chaineDonneEncode = chaineDonneEncode + donneEnBits;
            chaineDonneEncode = chaineDonneEncode.Replace(" ", String.Empty);

            chaineDonneEncode = AjouterOctetsPad(chaineDonneEncode, niveauCorrection, version);

            return chaineDonneEncode;

        }

        /// <summary>
        /// Trouve l'indicateur selon le mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns>L'indicateur de mode</returns>
        public string TrouverIndicateurMode(ChEncoding mode)
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
       /// Trouver la version
       /// </summary>
       /// <param name="chainDebut"></param>
       /// <param name="mode"></param>
       /// <param name="niveauCorrection"></param>
       /// <returns>La version</returns>
        public int TrouverVersion(string chainDebut, ChEncoding mode, ECLevel niveauCorrection)
        {

            int version = QRVersionHelper.GetQRVersionFromInput(chainDebut, mode, niveauCorrection);

            return version;
        }

        /// <summary>
        /// Trouver l'indicateur de nombrede caractères 
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <param name="mode"></param>
        /// <param name="version"></param>
        /// <returns>une chaine qui représente l'indicateur de nombre de caractères</returns>
        public string TrouverIndicateurNbCaract(string chaineDebut, ChEncoding mode, int version)
        {

            int bitsNecessaire = nbCaractByMode.GetNbCaract(mode, version);

            //Mettre nb caractère en binaire 
            int Binaire = int.Parse(Convert.ToString(chaineDebut.Length, 2));

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
       /// Encoder la chaine selon le mode choisie
       /// </summary>
       /// <param name="chaineDebut"></param>
       /// <param name="modeSelectionne"></param>
       /// <returns>la chaine encodée, sous forme binaire</returns>
        public string EncoderSelonLeMode(string chaineDebut, ChEncoding modeSelectionne)
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
       /// Ajouter des octects à la suite de la chaine reçue
       /// </summary>
       /// <param name="chaineDebut"></param>
       /// <param name="niveauCorrection"></param>
       /// <param name="version"></param>
       /// <returns>La chaine de bits, finale</returns>
        public string AjouterOctetsPad(string chaineDebut, ECLevel niveauCorrection, int version)
        {
            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int nbTotalMotCode = group.TotalDataCodeWords;

            string result = "";

            int nbBitsRequis = nbTotalMotCode * 8;

            //Terminateur
            if (chaineDebut.Length < nbBitsRequis - 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    chaineDebut += "0";

                }
                int length = chaineDebut.Length;

            }
            else if (chaineDebut.Length > nbBitsRequis - 4 && chaineDebut.Length < nbBitsRequis)
            {
                for (int i = 0; i < nbBitsRequis; i++)
                {
                    chaineDebut += '0';
                }
            }
            string DonneCodeMultiple = chaineDebut;

            //Ajout d'octets de pad
            if (chaineDebut.Length % 8 != 0)
            {

                int remainder = chaineDebut.Length % 8;

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

