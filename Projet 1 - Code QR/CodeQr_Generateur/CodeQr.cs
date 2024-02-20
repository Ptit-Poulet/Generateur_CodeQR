using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
        public CodeQr() {
           

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

             testNumerique = int.TryParse(ChaineDebut, out valeurNumerique);
           
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
            else if(testKanji)      
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


            version =  TrouverVersion(ChaineDebut, mode, niveauCorrection);

            string indicateurMode = TrouverIndicateurMode(mode);
            string ChaineDonneEncode = indicateurMode;

            string indicateurNbCaractere = TrouverIndicateurNbCaract(ChaineDebut,mode, version);
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
        /// Trouver La version
        /// </summary>
        /// <param name="ChainDebut"></param>
        /// <returns>La version</returns>
        public int TrouverVersion(string ChainDebut,ChEncoding mode, ECLevel niveauCorrection)
        {
            
            int version = QRVersionHelper.GetQRVersionFromInput(ChainDebut, niveauCorrection, mode);
           
            return version;
        }

        /// <summary>
        /// trouver l'indicateur du nombre de caractère
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returnsL'indicateur nb de caracteres></returns>
        public string TrouverIndicateurNbCaract(string ChaineDebut,ChEncoding mode,int version)
        {
           
            int bitsNecessaire = nbCaractByMode.GetNbCaract(mode,version); 
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
        /// Encodage de la chaine selon le mode
        /// </summary>
        /// <param name="modeSelectionne"></param>
        /// <param name="chaineDebut"></param>
        /// <param name="version"></param>
        /// <returns>Chaine encodé</returns>
        public string EncoderSelonLeMode(ChEncoding modeSelectionne, string chaineDebut, int version)
        {
            string chaineEncodee = "";

            switch(modeSelectionne)
            {
                case ChEncoding.AlphaNum:
                    chaineEncodee = CodageAlphanum(chaineDebut);
                    break;
                case ChEncoding.Num:
                    chaineEncodee = codageNumerique(chaineDebut);
                    break;
                case ChEncoding.Byte:
                    chaineEncodee = CodageModeOctet(chaineDebut, version);
                    break;
                case ChEncoding.Kanji:
                    chaineEncodee = CodageModeKanji(chaineDebut, version);
                    break;
            }
            return chaineEncodee;
        }

        /// <summary>
        /// Mettre données en bits selon le mode Alphanumerique
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returns>DOnnées en bits</returns>
        public string CodageAlphanum(string ChaineDebut)
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

            foreach (char c in ChaineDebut)
            {
               
                var matchingItem = alphaNumValue.FirstOrDefault(item => item.Contains(c.ToString()));

                
                if (matchingItem != null)
                {
                    caractereEnBinaire.Add(matchingItem[1]);
                }

            }


           
            for (int i = 0; i < caractereEnBinaire.Count; i += 2)
            {
               
                if (i == caractereEnBinaire.Count - 1)
                {
                    valeurNumerique = int.Parse(caractereEnBinaire[i]);
                }
                else
                {
                    valeurNumerique = int.Parse(caractereEnBinaire[i]) * 45 + int.Parse(caractereEnBinaire[i + 1]);
                }

                
                int longueurPadding = (i == caractereEnBinaire.Count - 1) ? 6 : 11;

                binaire = Convert.ToString(valeurNumerique, 2).PadLeft(longueurPadding, '0');

                CaractereEncode = CaractereEncode + " " + binaire;

                
                if (i == caractereEnBinaire.Count - 1)
                {
                    break;
                }
            }

            return CaractereEncode;
        }

        /// <summary>
        /// Encoder la chaine de debut en mode numerique
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        public string codageNumerique(string chaineDebut)
        {
            string chaineEncodee = "";
            List<string> lesGroupesDeTrois = new List<string>();

            //Divisez la chaîne en groupes de trois
            for(int i = 0; i < chaineDebut.Length; i+=3)
            {
                if(i < chaineDebut.Length - 2)
                    lesGroupesDeTrois.Add(chaineDebut.Substring(i, 3));
                else if(i == chaineDebut.Length - 2)
                    lesGroupesDeTrois.Add(chaineDebut.Substring(i, 2));
                else
                    lesGroupesDeTrois.Add(chaineDebut.Substring(i, 1));
            }

            //Convertir chaque groupe en binaire
            foreach(string groupe in lesGroupesDeTrois)
            {
                if(groupe.Length == 3)  
                {
                    if (groupe[0] == '0')  
                    {
                        if (groupe[1] == '0')  
                        {
                            
                            chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
                        }
                        else    
                            chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(7, '0');
                    }
                    else   
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(10, '0');

                }
                else if(groupe.Length == 2) 
                {
                    if (groupe[0] == '0') 
                    {
                       
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
                    }
                    else  
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(7, '0');
                }
                else   
                    chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
            }


            return chaineEncodee;
        }

        /// <summary>
        /// Codage en mode octet
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string CodageModeOctet(string chaineDebut, int version)
        {
            string chaineEncodee = "";

            foreach(char c in chaineDebut)
            {
               chaineEncodee += Convert.ToString(c, 2).PadLeft(8, '0');
            }

            return chaineEncodee;   
        }

        /// <summary>
        /// Codage en mode Kanji
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string CodageModeKanji(string chaineDebut, int version)
        {
            string chaineEncodee = "";

           //TODO: à faire

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

            //Ajout d'octets de pad
            if (ChaineDebut.Length % 8 != 0)
            {

                string DonneCodeMultiple = ChaineDebut;
                int remainder = ChaineDebut.Length % 8;

                if (remainder != 0)
                {
                    DonneCodeMultiple += new string('0', 8 - remainder);
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

            }


            return result.Trim();
        }
    }
}
