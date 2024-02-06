﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Generateur_Code_QR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program  program = new Program();

            //Avec le mode alphanumérique, niveau Q et version 1
            string ChaineCaractere = "HELLO WORLD";
            string ChaineEnBinaire ="";
            string mode = "alphanum";
                //Error Correction Level Q

            //Trouver le mode
            switch (mode)
            {
                //case "numeric":
                    //ChaineEnBinaire += 0001;
                //    break;

                case "alphanum":
                    ChaineEnBinaire += "0010"; //indicateur de mode
                    break;

                //case:...

            }

            ////Trouver la version
            switch (ChaineCaractere.Length)
            {
                //Version 1
                case < 16:

                    break;

                    //Version 2
                    //case < 29:
                    //    break;
                    //...

            }

            //Indicateur de nombre de caractère
            string indicateurNbCaractere = program.IndicateurNbCaractere(ChaineCaractere);

            ChaineEnBinaire = ChaineEnBinaire + " "+ indicateurNbCaractere;

            //bits de données
            string codageAlphaNum = program.CodageAlphaNum(ChaineCaractere);

            ChaineEnBinaire = ChaineEnBinaire + codageAlphaNum;
            string trim = ChaineEnBinaire.Replace(" ", String.Empty);   
            string bitsdeDonne = program.AJoutOctetPad(trim);

            Console.WriteLine(bitsdeDonne);
        }
        /// <summary>
        /// Longueur d'un int
        /// </summary>
        /// <param name="number"> le nombre</param>
        /// <returns>sa longueur</returns>
        public int Size(int number)
        {

            int i = (int)Math.Abs(number);
            if (i == 0) return 0;
            return i.ToString().Length;
        }

        /// <summary>
        /// Foonction pour obtenir l'indicateur de nombre de caractères
        /// </summary>
        /// <param name="ChaineCaractere"></param>
        /// <returns>Indicateur de nb caractères</returns>
        public string IndicateurNbCaractere(string ChaineCaractere)
        {
            Program program = new Program();
            //Mettre nb caractère en binaire 
            int numCaractere = ChaineCaractere.Length;
            string Bin = Convert.ToString(numCaractere, 2);//Met les caractère en binaire
            int Binaire = int.Parse(Bin);
            int Bits = 0;// les zéro qui seront en plus pour faire l'indicateur de nb caractère 9 bits de long
            int NbCaractere = 0;
            string indicateurNbCaractere = "";
            int longueurBinaire = program.Size(Binaire);//Savoir la longueur de l'indicateur

            //*****9 bits de log a cause de la version****

            if (longueurBinaire < 9)//Savoir si la longueur de l'indicateur est 9 bits de long 
            {
                Bits = 9 - longueurBinaire;
                NbCaractere = Binaire.ToString("D").Length + Bits;

            }
            indicateurNbCaractere = Binaire.ToString("D" + NbCaractere.ToString());

            return indicateurNbCaractere;
        }

        /// <summary>
        /// Codage alphanumerique des caractères
        /// </summary>
        /// <param name="Caractere">les caractères</param>
        /// <returns>Bits de données</returns>
        public string CodageAlphaNum(string Caractere)
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
            foreach (char c in Caractere)
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

        public string AJoutOctetPad(string Donnecode)
        {
            //faire des enum pour tout les tableau qu'on aura besoin ??
            int nbTotalMotCode = 13;
            string result = "";
            
            //Sachant que c'est la verison 1 avec un code d'erreur de Q
            int nbBitsRequis = nbTotalMotCode * 8;


            if(Donnecode.Length < nbBitsRequis - 4)
            {
                for(int i = 0; i < 4; i++)
                {
                    Donnecode += "0";

                }
                int length = Donnecode.Length;

            }
            else if(Donnecode.Length > nbBitsRequis - 4 && Donnecode.Length < nbBitsRequis)
            {
                for(int i = 0; i < nbBitsRequis; i++)
                {
                    Donnecode.PadLeft(1, '0');
                }
            }

            if(Donnecode.Length % 8 != 0)
            {

                string DonneCodeMultiple = Donnecode.Replace(" ", "");
                int remainder = Donnecode.Length % 8;

                if(remainder != 0)
                {
                    DonneCodeMultiple += new string('0', 8 - remainder);
                }
                
                do
                {

                    DonneCodeMultiple = DonneCodeMultiple + "11101100";
                
                    if(DonneCodeMultiple.Length != nbBitsRequis)
                    {
                        DonneCodeMultiple = DonneCodeMultiple + "00010001";
                    }


                }
                while (DonneCodeMultiple.Length != nbBitsRequis);

                for (int i = 0; i < DonneCodeMultiple.Length; i += 8)
                {
                    result += DonneCodeMultiple.Substring(i, Math.Min(8, DonneCodeMultiple.Length - i)) + " ";
                }

            }

            return result;
        }


    }

}