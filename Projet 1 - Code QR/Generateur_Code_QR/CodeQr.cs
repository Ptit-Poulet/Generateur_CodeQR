﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generateur_Code_QR
{
    internal class CodeQr
    {
        //Variables

        //version
        string mode = "alphanum";
        //EC - Error code

        /// <summary>
        /// Constructeur
        /// </summary>
        public CodeQr()
        {

        }

        //Méthodes
        public string PreparationCW(string ChaineDebut, string mode, int nbTotalMotCode)
        {
            
          
            string indicateurMode = TrouverIndicateurMode(mode);
            string ChaineDonneEncode = indicateurMode;

            string indicateurNbCaractere = TrouverIndicateurNbCaract(ChaineDebut);
            ChaineDonneEncode = ChaineDonneEncode +" "+ indicateurNbCaractere;

            string donneEnBits = CodageAlphanum(ChaineDebut);
            ChaineDonneEncode = ChaineDonneEncode + donneEnBits;
            string Trim = ChaineDonneEncode.Replace(" ", String.Empty);

            string BitsdeDonne = AjouterOctetsPad(Trim, nbTotalMotCode);

            return BitsdeDonne;

            //ChaineDonneEncode += TrouverVersion(ChaineDebut);



        }

        /// <summary>
        /// Trouve l'indicateur selon le mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns>L'indicateur</returns>
        public string TrouverIndicateurMode(string mode) 
        {
            string indicateurMode = "";
            //Trouver le mode
            switch (mode)
            {
                //case "numeric":
                //ChaineEnBinaire += 0001;
                //    break;

                case "alphanum":
                    indicateurMode += "0010"; //indicateur de mode
                    break;

                    //case:...

            }

            return indicateurMode;
        }
        
        /// <summary>
        /// Trouver La version
        /// </summary>
        /// <param name="ChainDebut"></param>
        /// <returns>La version</returns>
        public int TrouverVersion(string ChainDebut)
        {
            int version = 0;

            ////Trouver la version
            switch (ChainDebut.Length)
            {
                //Version 1
                case < 16:
                    version = 1;
                    break;

                    //Version 2
                    //case < 29:
                    //    break;
                    //...

            }
            return version;
        }
        /// <summary>
        /// trouver l'indicateur du nombre de caractère
        /// </summary>
        /// <param name="ChaineDebut"></param>
        /// <returnsL'indicateur nb de caracteres></returns>
        public string TrouverIndicateurNbCaract(string ChaineDebut)
        {
            //TODO modifier

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
            //TODO modifier
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
        public string AjouterOctetsPad(string ChaineDebut, int nbTotalMotCode)
        {
            //TODO modifier

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
                    ChaineDebut.PadLeft(1, '0');
                }
            }

            //Ajout d'octets de pad
            if (ChaineDebut.Length % 8 != 0)
            {

                string DonneCodeMultiple = ChaineDebut.Replace(" ", "");
                int remainder = ChaineDebut.Length % 8;

                if (remainder != 0)
                {
                    DonneCodeMultiple += new string('0', 8 - remainder);
                }

                do
                {

                    DonneCodeMultiple = DonneCodeMultiple + "11101100";

                    if (DonneCodeMultiple.Length != nbBitsRequis)
                    {
                        DonneCodeMultiple = DonneCodeMultiple + "00010001";
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