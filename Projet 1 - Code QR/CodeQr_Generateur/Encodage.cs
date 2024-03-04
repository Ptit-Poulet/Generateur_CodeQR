
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{

    public class Encodage
    {

        //Attributs
        string _chaineDebut;
        ECLevel _niveauCorrection;
        Dictionary<ChEncoding, List<int>> _dictio;
        Dictionary<ECLevel, Dictionary<ChEncoding, List<int>>> VersionByECAndCharType;


        /// <summary>
        /// Initializes a new instance of the <see cref="Encodage" /> class.
        /// </summary>
        /// <param name="chaineDebut">The chaine debut.</param>
        /// <param name="niveauCorrection">The niveau correction.</param>
        public Encodage(string chaineDebut, ECLevel niveauCorrection)
        {
            _chaineDebut = chaineDebut;
            _niveauCorrection = niveauCorrection;
            InitiliserDictio();
        }


        private void InitiliserDictio()
        {
            _dictio = new Dictionary<ChEncoding, List<int>>()
            {
                {ChEncoding.Num, new List<int> {10, 12, 14} },
                {ChEncoding.AlphaNum, new List<int> {9,11,13} },
                {ChEncoding.Byte, new List<int> {8,16,16} },
                {ChEncoding.Kanji,  new List<int> {8,10,12} },
            };

            VersionByECAndCharType = new Dictionary<ECLevel, Dictionary<ChEncoding, List<int>>>()
            {

              {ECLevel.L, new Dictionary<ChEncoding,List<int>>()
                {
                    {ChEncoding.Num, new List<int>(40) { 41,77,127,187,255,322,370,461,552,652,772,883,1022,1101,1250,1408,1548,1725,1903,2061,2232,2409,2620,2812,3057,3283,3517,3669,3909,4158,4417,4686,4965,5253,5529,5836,6153,6479,6743,7089} },
                    {ChEncoding.AlphaNum, new List<int>(40)  { 25,47,77,114,154,195,224,279,335,395,468,535,619,667,758,854,938,1046,1153,1249,1352,1460,1588,1704,1853,1990,2132,2223,2369,2520,2677,2840,3009,3183,3351,3537,3729,3927,4087,4296} },
                    {ChEncoding.Byte, new List<int>(40) { 17,32,53,78,106,134,154,192,230,271,321,367,425,458,520,586,644,718,792,858,929,1003,1091,1171,1273,1367,1465,1528,1628,1732,1840,1952,2068,2188,2303,2431,2563,2699,2809,2953} },
                    {ChEncoding.Kanji, new List<int>(40) { 10,20,32,48,65,82,95,118,141,167,198,226,262,282,320,361,397,442,488,528,572,618,672,721,784,842,902,940,1002,1066,1132,1201,1273,1347,1417,1496,1577,1661,1729,1817} }
              }},

              {ECLevel.M, new Dictionary<ChEncoding, List<int>>()
                {
                    {ChEncoding.Num, new List<int>(40) { 34, 63, 101, 149, 202, 255, 293, 365, 432, 513, 604, 691, 796, 871, 991, 1082, 1212, 1346, 1500, 1600, 1708, 1872, 2059, 2188, 2395, 2544, 2701, 2857, 3035, 3289, 3486, 3693, 3909, 4134, 4343, 4588, 4775, 5039, 5313, 5596 } },
                    {ChEncoding.AlphaNum, new List<int>(40) { 20, 38, 61, 90, 122, 154, 178, 221, 262, 311, 366, 419, 483, 528, 600, 656, 734, 816, 909, 970, 1035, 1134, 1248, 1326, 1451, 1542, 1637, 1732, 1839, 1994, 2113, 2238, 2369, 2506, 2632, 2780, 2894, 3054, 3220, 3391 } },
                    {ChEncoding.Byte, new List<int>(40) { 14, 26, 42, 62, 84, 106, 122, 152, 180, 213, 251, 287, 331, 362, 412, 450, 504, 560, 624, 666, 711, 779, 857, 911, 997, 1059, 1125, 1190, 1264, 1370, 1452, 1538, 1628, 1722, 1809, 1911, 1989, 2099, 2213, 2331} },
                    {ChEncoding.Kanji, new List<int>(40) { 8, 16, 26, 38, 52, 65, 75, 93, 111, 131, 155, 177, 204, 223, 254, 277, 310, 345, 384, 410, 438, 480, 528, 561, 614, 652, 692, 732, 778, 843, 894, 947, 1002, 1060, 1113, 1176, 1224, 1292, 1362, 1435} }
                } },

              {ECLevel.Q, new Dictionary<ChEncoding, List<int>>()
                {
                    {ChEncoding.Num, new List<int>(40) { 27, 48, 77, 111, 144, 178, 207, 259, 312, 364, 427, 489, 580, 621, 703, 775, 876, 948, 1063, 1159, 1224, 1358, 1468, 1588, 1718, 1804, 1933, 2085, 2181, 2358, 2473, 2670, 2805, 2949, 3081, 3244, 3417, 3599, 3791, 3993} },
                    {ChEncoding.AlphaNum, new List<int>(40) {16,29,47,67,87,108,125,157,189,221,259,296,352,376,426,470,531,574,644,702,742,823,890,963,1041,1094,1172,1263,1322,1429,1499,1618,1700,1787,1867,1966,2071,2181,2298,2420} },
                    {ChEncoding.Byte, new List<int>(40) { 11,20,32,46,60,74,86,108,130,151,177,203,241,258,292,322,364,394,442,482,509,565,611,661,715,751,805,868,908,982,1030,1112,1168,1228,1283,1351,1423,1499,1579,1663} },
                    {ChEncoding.Kanji, new List<int>(40) { 7,12,20,28,37,45,53,66,80,93,109,125,149,159,180,198,224,243,272,297,314,348,376,407,440,462,496,534,559,604,634,684,719,756,790,832,876,923,972,1024} }
                } },

              {ECLevel.H, new Dictionary<ChEncoding, List<int>>()
                {
                    {ChEncoding.Num, new List<int>(40) { 17,34,58,82,106,139,154,202,235,288,331,374,427,468,530,602,674,746,813,919,969,1056,1108,1228,1286,1425,1501,1581,1677,1782,1897,2022,2157,2301,2361,2524,2625,2735,2927,3057} },
                    {ChEncoding.AlphaNum, new List<int>(40) { 10,20,35,50,64,84,93,122,143,174,200,227,259,283,321,365,408,452,493,557,587,640,672,744,779,864,910,958,1016,1080,1150,1226,1307,1394,1431,1530,1591,1658,1774,1852} },
                    {ChEncoding.Byte, new List<int>(40) { 7,14,24,34,44,58,64,84,98,119,137,155,177,194,220,250,280,310,338,382,403,439,461,511,535,593,625,658,698,742,790,842,898,958,983,1051,1093,1139,1219,1273} },
                    {ChEncoding.Kanji, new List<int>(40) { 4,8,15,21,27,36,39,52,60,74,85,96,109,120,136,154,173,191,208,235,248,270,284,315,330,365,385,405,430,457,486,518,553,590,605,647,673,701,750,784} }
                } }


            };
        }

        private int GetNbCaract(ChEncoding mode, int version)
        {

            if (1 <= version && version <= 9)
                return _dictio[mode][0];
            else if (version <= 26)
                return _dictio[mode][1];
            else if (version <= 40)
                return _dictio[mode][2];

            return -1;

        }

        private int GetQRVersionFromInput(string input, ChEncoding chartype, ECLevel ec)
        {
            int inputlength = input.Length;

            List<int> theListIActuallyWant = VersionByECAndCharType[ec][chartype];

            for (int i = 0; i < 40; i++)
            {
                if (theListIActuallyWant[i] >= inputlength)
                {
                    return i + 1;
                }
            }
            return -1;
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
            int i = 0;
            foreach (char c in chaineDebut)
            {
                if (!lettre.Contains(c))
                {

                    testAlphaNumerique = false;
                    break;
                }
                else
                    testAlphaNumerique = true;
                i++;
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
        public string EncoderChaine(ChEncoding mode, out int version)
        {
            version = GetQRVersionFromInput(_chaineDebut, mode, _niveauCorrection);

            string indicateurMode = TrouverIndicateurMode(mode);
            string chaineDonneEncode = indicateurMode;

            string indicateurNbCaractere = TrouverIndicateurNbCaract(mode, version);
            chaineDonneEncode = chaineDonneEncode + " " + indicateurNbCaractere;

            string donneEnBits = EncoderSelonLeMode(mode);
            chaineDonneEncode = chaineDonneEncode + donneEnBits;
            chaineDonneEncode = chaineDonneEncode.Replace(" ", String.Empty);

            chaineDonneEncode = AjouterOctetsPad(chaineDonneEncode, version);

            return chaineDonneEncode;

        }

        /// <summary>
        /// Trouve l'indicateur selon le mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns>L'indicateur de mode</returns>
        private string TrouverIndicateurMode(ChEncoding mode)
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
        /// Encoder la chaine de debut en mode numerique
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        private string CodageNumerique()
        {
            string chaineEncodee = "";
            List<string> lesGroupesDeTrois = new List<string>();

            for (int i = 0; i < _chaineDebut.Length; i += 3)
            {
                if (i < _chaineDebut.Length - 2)
                    lesGroupesDeTrois.Add(_chaineDebut.Substring(i, 3));
                else if (i == _chaineDebut.Length - 2)
                    lesGroupesDeTrois.Add(_chaineDebut.Substring(i, 2));
                else
                    lesGroupesDeTrois.Add(_chaineDebut.Substring(i, 1));
            }

            //Convertir chaque groupe en binaire
            foreach (string groupe in lesGroupesDeTrois)
            {
                if (groupe.Length == 3)
                {
                    if (groupe[0] == '0')
                    {
                        if (groupe[1] == '0')
                        {
                            //il doit etre interprété comme un nombre à 1 chiffre
                            chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
                        }
                        else    //il doit etre interprêté comme un nombre à deux chiffres
                            chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(7, '0');
                    }
                    else    //Si effectivement il s'agit d'un avec le premier chifrre différent de zero
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(10, '0');

                }
                else if (groupe.Length == 2)
                {
                    if (groupe[0] == '0')
                    {
                        //Dans ce cas il doit etre interprêté comme un nombre à 1 chiffre
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
                    }
                    else    //il doit effectivement etre interprêté comme un nombre à deux chiffres
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(7, '0');
                }
                else    //si le groupe final ne comprend qu'un seul chiffre
                    chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
            }


            return chaineEncodee;
        }
        /// <summary>
        /// Encoder la chaine de debut en mode alphanum
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        private string CodageAlphaNum()
        {
            string CaractereEncode = "";
            string binaire = "";
            string indicateurCaractere = "";
            int valeurNumerique = 0;

            List<string> lettre = new List<string> {"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E",
                            "F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"," ","$","%","*","+","-",".","/",":"};

            List<string> chiffre = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35","36","37","38","39","40","41","42","43","44"};

            List<List<string>> alphaNumValue = new List<List<string>>();
            List<string> caractereEnBinaire = new List<string>();

            //Créer une liste avec les valeurs alphanumerique
            for (int i = 0; i < 44; i++)
            {
                List<string> value = new List<string>();
                value.Add(lettre[i]);
                value.Add(chiffre[i]);
                alphaNumValue.Add(value);
            }

            // Conversion en binaire
            foreach (char c in _chaineDebut)
            {
                // Recherche de la correspondance dans la liste alphaNumValue
                var matchingItem = alphaNumValue.FirstOrDefault(item => item.Contains(c.ToString()));

                // Si une correspondance est trouvée, ajoutez la représentation binaire
                if (matchingItem != null)
                {
                    caractereEnBinaire.Add(matchingItem[1]);
                }

            }


            //Formule pour mettre en binaire
            for (int i = 0; i < caractereEnBinaire.Count; i += 2)
            {
                //Si le caractère est seul
                if (i == caractereEnBinaire.Count - 1)
                {
                    valeurNumerique = int.Parse(caractereEnBinaire[i]);
                }
                else
                {
                    valeurNumerique = int.Parse(caractereEnBinaire[i]) * 45 + int.Parse(caractereEnBinaire[i + 1]);
                }

                //Regarder si fini sur un caractère seul
                int longueurPadding = (i == caractereEnBinaire.Count - 1) ? 6 : 11;

                binaire = Convert.ToString(valeurNumerique, 2).PadLeft(longueurPadding, '0');

                CaractereEncode = CaractereEncode + " " + binaire;

                //Break la boucle si c'Est le dernier caractère
                if (i == caractereEnBinaire.Count - 1)
                {
                    break;
                }
            }
            return CaractereEncode;
        }
        /// <summary>
        /// Encoder la chaine de debut en mode Byte
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        private string CodageByte()
        {
            //TODO
            string chaineEncodee = "";
            List<string> caractereEnHexa = new List<string>();

            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(_chaineDebut);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            string msg = iso.GetString(isoBytes);

            foreach (char c in _chaineDebut)
            {
                chaineEncodee += Convert.ToString(c, 2).PadLeft(8, '0');
            }


            return chaineEncodee;
            //throw new Exception();
        }
        /// <summary>
        /// Encoder la chaine de debut en mode kanji
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        private static string CodageKanji()
        {
            //TODO

            throw new Exception();
        }


        /// <summary>
        /// Trouver l'indicateur de nombrede caractères 
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <param name="mode"></param>
        /// <param name="version"></param>
        /// <returns>une chaine qui représente l'indicateur de nombre de caractères</returns>
        private string TrouverIndicateurNbCaract(ChEncoding mode, int version)
        {

            int bitsNecessaire = GetNbCaract(mode, version);

            //Mettre nb caractère en binaire 
            int Binaire = int.Parse(Convert.ToString(_chaineDebut.Length, 2));

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
        private string EncoderSelonLeMode(ChEncoding modeSelectionne)
        {
            string chaineEncodee = "";

            switch (modeSelectionne)
            {
                case ChEncoding.Num:
                    chaineEncodee = CodageNumerique();
                    break;
                case ChEncoding.AlphaNum:
                    chaineEncodee = CodageAlphaNum();
                    break;
                case ChEncoding.Byte:
                    chaineEncodee = CodageByte();
                    break;
                case ChEncoding.Kanji:
                    chaineEncodee = CodageKanji();
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
        private string AjouterOctetsPad(string chaineEncodee, int version)
        {
            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(_niveauCorrection, version);
            int nbTotalMotCode = group.TotalDataCodeWords;

            string result = "";

            int nbBitsRequis = nbTotalMotCode * 8;

            //Terminateur
            if (chaineEncodee.Length < nbBitsRequis - 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    chaineEncodee += "0";

                }
                int length = chaineEncodee.Length;

            }
            else if (chaineEncodee.Length > nbBitsRequis - 4 && chaineEncodee.Length < nbBitsRequis)
            {
                for (int i = 0; i < nbBitsRequis; i++)
                {
                    chaineEncodee += '0';
                }
            }
            string DonneCodeMultiple = chaineEncodee;

            //Ajout d'octets de pad
            if (chaineEncodee.Length % 8 != 0)
            {

                int remainder = chaineEncodee.Length % 8;

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

