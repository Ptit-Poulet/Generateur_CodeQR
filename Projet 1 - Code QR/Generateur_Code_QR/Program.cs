using STH1123.ReedSolomon;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Generateur_Code_QR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ChaineDebut = "HELLO WORLD";
            string mode = "alphanum";
            CodeQr codeQr = new CodeQr();
            int nbTotalMotCode = 16;

            string resultat = codeQr.PreparationCW(ChaineDebut, mode, nbTotalMotCode);
            string resultatAttendu = "00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01000000 11101100 00010001 11101100 00010001 11101100 00010001";
           // Console.WriteLine(resultat);
            if (resultat == resultatAttendu)
            {
                Console.WriteLine("Le code est : est le même");

            }

            //// //Exemple Bibliotheque Reed - Solomon
            //int[] G1B1 = new int[26] { Convert.ToInt32("00100000", 2), Convert.ToInt32("01011011", 2), Convert.ToInt32("00001011", 2), Convert.ToInt32("01111000", 2), Convert.ToInt32("11010001", 2), 
            //Convert.ToInt32("01110010", 2),Convert.ToInt32("11011100", 2), Convert.ToInt32("01001101", 2), Convert.ToInt32("01000011", 2), Convert.ToInt32("01000000", 2), Convert.ToInt32("11101100", 2),
            //Convert.ToInt32("00010001", 2),Convert.ToInt32("11101100", 2), Convert.ToInt32("00010001", 2), Convert.ToInt32("11101100", 2), Convert.ToInt32("00010001", 2), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            //rse.Encode(G1B1, 10);

            //for (int i = 16; i < G1B1.Length; i++)
            //{
            //    Console.WriteLine(G1B1[i]);
            //}

            //pour créer un code 5-Q. Il y'a 18 mots de codes de correction d'erreurs pour chaque bloc
            int[] MG1B1 = new int[33] { Convert.ToInt32("01000011", 2), Convert.ToInt32("01010101", 2), Convert.ToInt32("01000110", 2),
                                        Convert.ToInt32("10000110", 2),Convert.ToInt32("01010111", 2), Convert.ToInt32("00100110", 2),
                                        Convert.ToInt32("01010101", 2), Convert.ToInt32("11000010", 2),Convert.ToInt32("01110111", 2),
                                        Convert.ToInt32("00110010", 2), Convert.ToInt32("00000110", 2), Convert.ToInt32("00010010", 2),
                                        Convert.ToInt32("00000110", 2), Convert.ToInt32("01100111", 2), Convert.ToInt32("00100110", 2),
                                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

            int[] MG1B2 = new int[33] { Convert.ToInt32("11110110", 2), Convert.ToInt32("11110110", 2), Convert.ToInt32("01000010", 2),
                                        Convert.ToInt32("00000111", 2), Convert.ToInt32("01110110", 2), Convert.ToInt32("10000110", 2),
                                        Convert.ToInt32("11110010", 2), Convert.ToInt32("00000111", 2), Convert.ToInt32("00100110", 2),
                                        Convert.ToInt32("01010110", 2), Convert.ToInt32("00010110", 2), Convert.ToInt32("11000110", 2),
                                        Convert.ToInt32("11000111", 2), Convert.ToInt32("10010010", 2), Convert.ToInt32("00000110", 2),
                                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
           

            int[] MG2B1 = new int[34] { Convert.ToInt32("10110110", 2), Convert.ToInt32("11100110", 2), Convert.ToInt32("11110111", 2),Convert.ToInt32("01110111", 2),
                                        Convert.ToInt32("00110010", 2), Convert.ToInt32("00000111", 2), Convert.ToInt32("01110110", 2), Convert.ToInt32("10000110", 2),
                                        Convert.ToInt32("01010111", 2), Convert.ToInt32("00100110", 2), Convert.ToInt32("01010010", 2), Convert.ToInt32("00000110", 2),
                                        Convert.ToInt32("10000110", 2), Convert.ToInt32("10010111", 2), Convert.ToInt32("00110010", 2), Convert.ToInt32("00000111", 2),
                                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

            int[] MG2B2 = new int[34] { Convert.ToInt32("01000110", 2), Convert.ToInt32("11110111", 2), Convert.ToInt32("01110110", 2),Convert.ToInt32("01010110", 2),
                                          Convert.ToInt32("11000010", 2), Convert.ToInt32("00000110", 2), Convert.ToInt32("10010111", 2), Convert.ToInt32("00110010", 2),
                                          Convert.ToInt32("00010000", 2), Convert.ToInt32("11101100", 2), Convert.ToInt32("00010001", 2), Convert.ToInt32("11101100", 2),
                                          Convert.ToInt32("00010001", 2), Convert.ToInt32("11101100", 2), Convert.ToInt32("00010001", 2), Convert.ToInt32("11101100", 2),
                                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

            //int[] MG2B2 = new int[34] { Convert.ToInt32("01000110", 2), Convert.ToInt32("11110111", 2), Convert.ToInt32("01110110", 2),Convert.ToInt32("01010110", 2),
            //                            Convert.ToInt32("11000010", 2), Convert.ToInt32("00000110", 2), Convert.ToInt32("10010111", 2), Convert.ToInt32("00110010", 2),
            //                           Convert.ToInt32("11100000", 2), Convert.ToInt32("11101100", 2), Convert.ToInt32("00010001", 2), Convert.ToInt32("11101100", 2),
            //                          Convert.ToInt32("00010001", 2), Convert.ToInt32("11101100", 2), Convert.ToInt32("00010001", 2), Convert.ToInt32("11101100", 2),
            //                          0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};    //Il y'a une erreur au niveau du 16, car la valeur correspondante ici est 224
           
            ReedSolomonEncoder rse = new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256);

            rse.Encode(MG1B1, 18);
            rse.Encode(MG1B2, 18);
            rse.Encode(MG2B1, 18);
            rse.Encode(MG2B2, 18);

            //for (int i = 16; i <= MG1B1.Length; i++)
            //{
            //    Console.WriteLine(MG2B2[i]);
            //}




            int[,] motDecodesDeDonneesEntrelaces = new int[16, 4];

            int[,] motDecodesDeCorrectionEntrelaces = new int[18, 4];


            //Entrelaçons les mots de codes de données
            for (int i = 0; i < motDecodesDeDonneesEntrelaces.GetLength(0); i++)
            {
                motDecodesDeDonneesEntrelaces[i, 0] = MG1B1[i];
                motDecodesDeDonneesEntrelaces[i, 1] = MG1B2[i];
                motDecodesDeDonneesEntrelaces[i, 2] = MG2B1[i];
                motDecodesDeDonneesEntrelaces[i, 3] = MG2B2[i];

                if(i == motDecodesDeDonneesEntrelaces.GetLength(0) - 1)     //À cause que les deux premiers blocs du groupe 1 ont une taille inférieure de 1 à ceux du groupe 2
                {
                    motDecodesDeDonneesEntrelaces[i, 0] = 0;
                    motDecodesDeDonneesEntrelaces[i, 1] = 0;
                    motDecodesDeDonneesEntrelaces[i, 2] = MG2B1[i];
                    motDecodesDeDonneesEntrelaces[i, 3] = MG2B2[i];
                }
            }


            string concatenes = "";

            //je le remplis
            for (int i = 0; i < motDecodesDeDonneesEntrelaces.GetLength(0); i++)
            {
               for(int j = 0; j < motDecodesDeDonneesEntrelaces.GetLength(1); j++)
                {
                    //Console.WriteLine(motDecodesDeDonneesEntrelaces[i, j]);
                    
                    if(motDecodesDeDonneesEntrelaces[i, j] == 0)
                        continue;
                    else
                        concatenes += motDecodesDeDonneesEntrelaces[i, j] + ",";

                    // Console.WriteLine(concatenes);
                }
            }

           // Console.WriteLine(concatenes.Length + "\r\n");

           // Console.WriteLine(concatenes + "\r\n");
            

            //Entrelaçons les mots de codes de corresction d'erreurs
            for (int i = 0; i < motDecodesDeCorrectionEntrelaces.GetLength(0); i++)
            {
                motDecodesDeCorrectionEntrelaces[i, 0] = MG1B1[i +15];
                motDecodesDeCorrectionEntrelaces[i, 1] = MG1B1[i + 15];
                motDecodesDeCorrectionEntrelaces[i, 2] = MG2B1[i + 16];
                motDecodesDeCorrectionEntrelaces[i, 3] = MG2B2[i + 16];

            }

            string concatenes2 = "";

            //je le remplis
            for (int i = 0; i < motDecodesDeCorrectionEntrelaces.GetLength(0); i++)
            {
                for (int j = 0; j < motDecodesDeCorrectionEntrelaces.GetLength(1); j++)
                {
                    if (motDecodesDeCorrectionEntrelaces[i, j] == 0)
                        continue;
                    else
                        concatenes2 += motDecodesDeCorrectionEntrelaces[i, j] + ",";

                    // Console.WriteLine(concatenes);
                }
            }

            //Console.WriteLine(concatenes2 + "\r\n" + "\r\n");

            string messageFinale = concatenes + concatenes2;

            string[] tableauMessageFinal = messageFinale.Split(',');
            //Console.WriteLine(messageFinale + "\r\n");

            for (int i = 0; i < tableauMessageFinal.Length; i++)
            {

                //int valeurNumerique = int.Parse(Convert.ToString(tableauMessageFinal[i]));
                //string elementBinaire = Convert.ToString(valeurNumerique, 2);
                //string elementOctet = elementBinaire.PadLeft(8 - elementBinaire.Length , '0');

                //Console.WriteLine(tableauMessageFinal[i] + elementOctet);

                if (tableauMessageFinal[i] == "")
                    continue;
                else
                {
                    int valeurNumerique = int.Parse(tableauMessageFinal[i]);
                    string elementBinaire = Convert.ToString(valeurNumerique, 2);
                    //Console.WriteLine(elementBinaire + "  " + elementBinaire.Length);
                    string elementOctet = elementBinaire.PadLeft(8 - elementBinaire.Length, '0');   //Ceci ne marche pas !
                    Console.WriteLine(elementOctet);
                }


            }


            //Console.WriteLine(int.Parse("67"));
        }

    }

}