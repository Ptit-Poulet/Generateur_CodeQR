using STH1123.ReedSolomon;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Generateur_Code_QR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ChaineDebut = "HELLO WORLD";
            string mode = "alphanum";
            CodeQr codeQr = new CodeQr();
            int nbTotalMotCode = 13;

<<<<<<< HEAD
            //string resultat = codeQr.PreparationCW(ChaineDebut, mode, nbTotalMotCode);
            //string resultatAttendu = "00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01000000 11101100 00010001 11101100 00010001 11101100 00010001";
            //// Console.WriteLine(resultat);
            //if (resultat == resultatAttendu)
            //{
            //    Console.WriteLine("Le code est est le même");

            //}
=======
            /*TEST  FONCTION PREPARATION CW*/
            string resultat = codeQr.PreparationCW(ChaineDebut, mode, nbTotalMotCode);
            string resultatAttendu = "00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01000000 11101100 00010001 11101100";
            if (resultat == resultatAttendu)
            {
                Console.WriteLine("TEST PREPARATION cw: Réussi");
            }
            else
            {
                Console.WriteLine("TEST PREPARATION cw: Échoué");

            }
            /*TEST  FONCTION PREPARATION CW*/
>>>>>>> b5ec5e3542d471f45786832f4d0a896ceb686153


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
<<<<<<< HEAD

            ReedSolomonEncoder rse = new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256);
=======
            /*---------------------------------------------------*/
            Bloc bloc = new Bloc();
            //TEST foNCTION BLOC FORMER BLOC
            int[]message = bloc.FormerBloc(resultat, 13, 13);
>>>>>>> b5ec5e3542d471f45786832f4d0a896ceb686153

            //for (int i = 0; i < message.Length; i++)
            //{
            //    Console.WriteLine(message[i]);
            //}
            //ReedSolomon est dans la class Bloc
            bloc.ReedSolomon(MG1B1, 18);
            bloc.ReedSolomon(MG1B2, 18);
            bloc.ReedSolomon(MG2B1, 18);
            bloc.ReedSolomon(MG2B2, 18);

<<<<<<< HEAD
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
=======
            /*TEST FONCTIONs POUR MESSAGE FINAL*/
            Groupe groupe = new Groupe();
            int NbData = 16;
            int ECcodeword = 18;
            int versionCode = 5;
            string data = groupe.EntrelacerData(NbData, MG1B1, MG1B2, MG2B1, MG2B2);
            string codeErreur = groupe.EntrelacerEC(ECcodeword, NbData, MG1B1, MG1B2, MG2B1, MG2B2);
            string messageEnBinanire = groupe.StructurerMessageFinal(data, codeErreur, versionCode);

            string resultatAttendu2 = "01000011111101101011011001000110010101011111011011100110111101110100011001000010111101110111011010000110000001110111011101010110010101110111011000110010110000100010011010000110000001110000011001010101111100100111011010010111110000100000011110000110001100100111011100100110010101110001000000110010010101100010011011101100000001100001011001010010000100010001001011000110000001101110110000000110110001111000011000010001011001111001001010010111111011000010011000000110001100100001000100000111111011001101010101010111100101001110101111000111110011000111010010011111000010110110000010110001000001010010110100111100110101001010110101110011110010100100110000011000111101111011011010000101100100111111000101111100010010110011101111011111100111011111001000100001111001011100100011101110011010101111100010000110010011000010100010011010000110111100001111111111011101011000000111100110101011001001101011010001101111010101001001101111000100010000101000000010010101101010001101101100100000111010000110100011111100000010000001101111011110001100000010110010001001111000010110001101111011000000000";
            if (messageEnBinanire == resultatAttendu2)
            {
                Console.WriteLine("TEST MESSAGE FINAL: Réussi");
>>>>>>> b5ec5e3542d471f45786832f4d0a896ceb686153
            }
            else
            {
<<<<<<< HEAD
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
=======
                Console.WriteLine("TEST MESSAGE FINAL: Échoué");
>>>>>>> b5ec5e3542d471f45786832f4d0a896ceb686153

            }

            Console.WriteLine(resultatAttendu2.Length);

            string messaFinaleBinaire = groupe.StructureMessageFinale(message, 1);
            Console.WriteLine("HELLO WORLD :"+ messaFinaleBinaire);

<<<<<<< HEAD

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
=======
            int[] chaineFormatDECinq = new int[15] { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            ReedSolomonEncoder rse = new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256);
             rse.Encode(chaineFormatDECinq, 10);

            //foreach(int i in chaineFormatDECinq) { Console.WriteLine(i); }

>>>>>>> b5ec5e3542d471f45786832f4d0a896ceb686153
        }
    }
}