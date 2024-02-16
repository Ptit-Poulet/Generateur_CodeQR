using System;
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
        public CodeQr() { }
        //Méthodes
        /// <summary>
        /// Regroupeent des fonctions de la calsse pour former le codeword
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <param name="mode"></param>
        /// <param name="nbTotalMotCode"></param>
        /// <returns>Codeword prêt à être placé</returns>
        public string PreparationCW(string ChaineDebut, ChEncoding mode, ECLevel niveauCorrection)
        {


           int  version =  TrouverVersion(ChaineDebut, mode, niveauCorrection);

            string indicateurMode = TrouverIndicateurMode(ChaineDebut,mode);
            string ChaineDonneEncode = indicateurMode;

            string indicateurNbCaractere = TrouverIndicateurNbCaract(ChaineDebut, version);
            ChaineDonneEncode = ChaineDonneEncode + " " + indicateurNbCaractere;

            string donneEnBits = CodageAlphanum(ChaineDebut);
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
        public string TrouverIndicateurNbCaract(string ChaineDebut,int version)
        {
            //TODO 
            //Mettre nb caractère en binaire 
            int Binaire = int.Parse(Convert.ToString(ChaineDebut.Length, 2));

            // les zéro qui seront en plus pour faire l'indicateur de nb caractère 9 bits de long
            int Bits = Binaire.ToString().Length;
            int NbCaractere = 0;
            int longueurBinaire = Bits;//Savoir la longueur de l'indicateur

            //*****9 bits de log a cause de la version****

            if (longueurBinaire < 9)//Savoir si la longueur de l'indicateur est 9 bits de long 
            {
                Bits = 9 - longueurBinaire;
                NbCaractere = Binaire.ToString("D").Length + Bits;

            }
            string indicateurNbCaractere = Binaire.ToString("D" + NbCaractere.ToString());

            return indicateurNbCaractere;
        }

        /// <summary>
        /// Mettre données en bits selon le mode
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returns>DOnnées en bits</returns>
        public string CodageAlphanum(string ChaineDebut)
        {
            string CaractereEncode = "";
            string binaire = "";
            string indicateurCaractere = "";
            int valeurNumerique = 0;

            List<string> lettre = new List<string> { "H", "E", "L", "O", " ", "W", "R", "D" };
            List<string> chiffre = new List<string> { "17", "14", "21", "24", "36", "32", "27", "13" };

            List<List<string>> alphaNumValue = new List<List<string>>();
            List<string> caractereEnBinaire = new List<string>();

            //Créer une liste avec les valeurs alphanumerique
            for (int i = 0; i < 8; i++)
            {
                List<string> value = new List<string>();
                value.Add(lettre[i]);
                value.Add(chiffre[i]);
                alphaNumValue.Add(value);
            }

            // Conversion en binaire
            foreach (char c in ChaineDebut)
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
        /// Ajoute des octets de pad
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returns></returns>
        public string AjouterOctetsPad(string ChaineDebut, ECLevel niveauCorrection, int version)
        {
            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int nbTotalMotCode = group.TotalDataCodeWords;

            string result = "";

            //Sachant que c'est la verison 1 avec un code d'erreur de Q
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
