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
            //switch(ChaineCaractere.Length)
            //{
            //    //Version 1
            //    case <16:

            //        break;

            //        //Version 2
            //    //case < 29:
            //    //    break;
            //    //...

            //}

            //Indicateur de nombre de caractère
            string indicateurNbCaractere = program.IndicateurNbCaractere(ChaineCaractere);

            ChaineEnBinaire = ChaineEnBinaire + " "+ indicateurNbCaractere;

            //bits de données
            string codageAlphaNum = program.CodageAlphaNum(ChaineCaractere);

            ChaineEnBinaire = ChaineEnBinaire + codageAlphaNum;

            Console.WriteLine(ChaineEnBinaire);
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
        /// <returns>Chaine binaire</returns>
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


    }

}