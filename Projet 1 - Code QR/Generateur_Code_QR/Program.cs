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

            //string resultat = codeQr.PreparationCW(ChaineDebut, mode, nbTotalMotCode);
            //string resultatAttendu = "00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01000000 11101100 00010001 11101100 00010001 11101100 00010001";
            //// Console.WriteLine(resultat);
            //if (resultat == resultatAttendu)
            //{
            //    Console.WriteLine("Le code est est le même");

            //}

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

            /*N@55 dans G2B2 égal à 224 et non 16 */

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

                if (i == motDecodesDeDonneesEntrelaces.GetLength(0) - 1)     //À cause que les deux premiers blocs du groupe 1 ont une taille inférieure de 1 à ceux du groupe 2
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
                for (int j = 0; j < motDecodesDeDonneesEntrelaces.GetLength(1); j++)
                {
                    //Console.WriteLine(motDecodesDeDonneesEntrelaces[i, j]);

                    if (motDecodesDeDonneesEntrelaces[i, j] == 0)
                        continue;
                    else
                        concatenes += motDecodesDeDonneesEntrelaces[i, j] + ",";

                    // Console.WriteLine(concatenes);
                }
            }

            // Console.WriteLine(concatenes.Length + "\r\n");

            // Console.WriteLine(concatenes + "\r\n");


            //Entrelaçons les mots de codes de correction d'erreurs
            for (int i = 0; i < motDecodesDeCorrectionEntrelaces.GetLength(0); i++)
            {
                motDecodesDeCorrectionEntrelaces[i, 0] = MG1B1[i + 15];
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


                }
            }

            string messageFinale = concatenes + concatenes2;
            messageFinale = messageFinale.Remove(messageFinale.LastIndexOf(','));

            //Console.WriteLine(messageFinale + "\r\n");

            //Convertir en bianire
            string messageTuto = "67, 246, 182, 70, 85, 246, 230, 247, 70, 66, 247, 118, 134, 7, 119, 86, 87, 118, 50, 194, 38, 134, 7, 6, 85, 242, 118, 151, 194, 7, 134, 50, 119, 38, 87, 16, 50, 86, 38, 236, 6, 22, 82, 17, 18, 198, 6, 236, 6, 199, 134, 17, 103, 146, 151, 236, 38, 6, 50, 17, 7, 236, 213, 87, 148, 235, 199, 204, 116, 159, 11, 96, 177, 5, 45, 60, 212, 173, 115, 202, 76, 24, 247, 182, 133, 147, 241, 124, 75, 59, 223, 157, 242, 33, 229, 200, 238, 106, 248, 134, 76, 40, 154, 27, 195, 255, 117, 129, 230, 172, 154, 209, 189, 82, 111, 17, 10, 2, 86, 163, 108, 131, 161, 163, 240, 32, 111, 120, 192, 178, 39, 133, 141, 236";
            string[] tblmessageTuto = messageTuto.Split(',');
            string resultatAttendu = "0100001111110110101101100100011001010101111101101110011011110111010001100100001011110111011101101000011000000111011101110101011001010111011101100011001011000010001001101000011000000111000001100101010111110010011101101001011111000010000001111000011000110010011101110010011001010111000100000011001001010110001001101110110000000110000101100101001000010001000100101100011000000110111011000000011011000111100001100001000101100111100100101001011111101100001001100000011000110010000100010000011111101100110101010101011110010100111010111100011111001100011101001001111100001011011000001011000100000101001011010011110011010100101011010111001111001010010011000001100011110111101101101000010110010011111100010111110001001011001110111101111110011101111100100010000111100101110010001110111001101010111110001000011001001100001010001001101000011011110000111111111101110101100000011110011010101100100110101101000110111101010100100110111100010001000010100000001001010110101000110110110010000011101000011010001111110000001000000110111101111000110000001011001000100111100001011000110111101100";
            int valNumeriueTuto = 0;
            string enBinaireTuto = "";
            string Bits8 = "";
            string messageBinaireTuto = "";

            for (int i = 0; i < tblmessageTuto.Length - 1; i++)
            {
                valNumeriueTuto = int.Parse(tblmessageTuto[i]);
                Bits8 = Convert.ToString(valNumeriueTuto, 2).PadLeft(8 - enBinaireTuto.Length, '0');

                messageBinaireTuto = messageBinaireTuto + Bits8;


            }
            if (messageBinaireTuto == resultatAttendu)
            {
                Console.WriteLine("go");
            }
            string[] tableauMessageFinal = messageFinale.Split(',');
            int valNumerique = 0;
            string enBinaire = "";
            string messageBinaire = "";


            for (int i = 0; i < tableauMessageFinal.Length ; i++)
            {
                valNumerique = int.Parse(tableauMessageFinal[i]);
                enBinaire = Convert.ToString(valNumerique, 2);
                int remainder = enBinaire.Length % 8;

                if(remainder != 0)
                {
                    messageBinaire += new string('0', 8 - remainder);
                }

            }
            if (messageBinaire == resultatAttendu)
            {
                Console.WriteLine("go");

            }

            //for (int i = 0; i < tableauMessageFinal.Length; i++)
            //{

            //    //int valeurNumerique = int.Parse(Convert.ToString(tableauMessageFinal[i]));
            //    //string elementBinaire = Convert.ToString(valeurNumerique, 2);
            //    //string elementOctet = elementBinaire.PadLeft(8 - elementBinaire.Length , '0');

            //    //Console.WriteLine(tableauMessageFinal[i] + elementOctet);

            //    if (tableauMessageFinal[i] == "")
            //        continue;
            //    else
            //    {
            //        int valeurNumerique = int.Parse(tableauMessageFinal[i]);
            //        string elementBinaire = Convert.ToString(valeurNumerique, 2);
            //        //Console.WriteLine(elementBinaire + "  " + elementBinaire.Length);
            //        string elementOctet = elementBinaire.PadLeft(8 - elementBinaire.Length, '0');   //Ceci ne marche pas !
            //       //Console.WriteLine(elementOctet);
            //    }


            //}


            //Console.WriteLine(int.Parse("67"));
        }

    }

}