using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Generateur_Code_QR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            //Avec le mode alphanumérique, niveau Q et version 1
            string ChaineEnBinaire ="";
            string mode = "alphanum";
                //Error Correction Level Q
            string ChaineCaractere = "HELLO WORLD";

            //Trouver le mode
            switch (mode)
            {
                //case "numeric":
                    //ChaineEnBinaire += 0001;
                //    break;

                case "alphanum":
                    ChaineEnBinaire += "0010";
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
            ChaineEnBinaire += " "+ indicateurNbCaractere;
           
            //string codageAlphaNum = program.CodageAlphaNum(ChaineCaractere);
            //ChaineEnBinaire += " " + codageAlphaNum;

            //Console.WriteLine(ChaineEnBinaire);
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
        /// Codage alphanumerique des caractères
        /// </summary>
        /// <param name="Caractere">les caractères</param>
        /// <returns>Chaine binaire</returns>
        public string CodageAlphaNum(string Caractere)
        {
            string binaire11Bits = "";
            List<string> lettre = new List<string> { "H", "E", "L", "O"," ","W", "R", "D"};
            List<string> chiffre = new List<string>{ "14", "21", "24", "36", "36", "32", "27", "13" };
            List<List<string>> alphaNumValue = new List<List<string>>();
            for(int i = 0; i < 8; i++)
            {
                List<string> value = new List<string>();
                value.Add(lettre[i]);
                value.Add(chiffre[i]);
                alphaNumValue.Add(value);
            }
            List<string> caractereEnBinaire = new List<string>();

            //int nbCaractere = Caractere.Length;
            //for(int i = 0; i < nbCaractere; i++)
            //{
            //    for(int j = 0; j < nbCaractere; j++)
            //    if (alphaNumValue.Contains(Caractere[i].ToString()))
            //    {
            //            caractereEnBinaire[i] += alphaNumValue[j];
            //    }

            //}
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

            for (int i = 0; i < caractereEnBinaire.Count - 1; i += 2)
            {
                int valeurNumerique = int.Parse(caractereEnBinaire[i]) * 45;
                binaire11Bits = Convert.ToString(valeurNumerique, 2).PadLeft(11, '0');

            }

            return binaire11Bits;

        }


    }

}