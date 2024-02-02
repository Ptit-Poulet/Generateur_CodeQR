namespace Test_Genetareur_QR
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string input = "HELLO WORLD";

            string p1 = input.Substring(0, 2);  //HE

            string p2 = input.Substring(2, 2);  //LL

            string p3 = input.Substring(4, 2);  //O
            string p4 = input.Substring(6, 2);  //WO
            string p5 = input.Substring(8, 2);  //RL
            string p6 = input.Substring(10, 1); //D

            string binaire11Bits = "";
            //List<string> lettre = new List<string> { "H", "E", "L", "O", " ", "W", "R", "D" };
            //List<string> chiffre = new List<string> { "14", "21", "24", "36", "36", "32", "27", "13" };
            List<List<string>> alphaNumValue = new List<List<string>>();
          
            List<string> caractereEnBinaire = new List<string>();




           

            // Conversion en binaire
            foreach (char c in input)
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

            Console.WriteLine(binaire11Bits);
        }

        public static string Conversion(string c)
        {

            string[,] correspondances = { { "H", "14" }, { "E", "21" }, { "L", "24" }, { "0", "36" } };

            string correspondance = "";

            for (int i = 0; i < correspondances.GetLength(0); i++)
            {
                bool resultat = c == correspondances[i, 0];

                if (resultat)
                {
                    correspondance = correspondances[i, 1];
                }
                
            }

            return correspondance;
        }

    }
}