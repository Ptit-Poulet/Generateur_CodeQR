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
            /*---------------------------------------------------*/
            Bloc bloc = new Bloc();
            //TEST foNCTION BLOC FORMER BLOC
            //string resultatAttendu = "00100000 01011011 00001011 01111000 11010001 01110010 11011100 01001101 01000011 01000000 11101100 00010001 11101100";
            //int[] lol = bloc.FomerBloc(resultatAttendu);

            //for (int i = 0; i < lol.Length; i++)
            //{
            //    Console.WriteLine(lol[i]);
            //}
            //ReedSolomon est dans la class Bloc
            bloc.ReedSolomon(MG1B1, 18);
            bloc.ReedSolomon(MG1B2, 18);
            bloc.ReedSolomon(MG2B1, 18);
            bloc.ReedSolomon(MG2B2, 18);

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
            }
            else
            {
                Console.WriteLine("TEST MESSAGE FINAL: Échoué");

            }


        }
    }
}