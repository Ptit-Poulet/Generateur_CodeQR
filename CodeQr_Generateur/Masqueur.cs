using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class Masqueur
    {

        /// <summary>
        /// Constructeur
        /// </summary>
        public Masqueur() { }

        //Méthodes
        /// <summary>
        /// Application de tous les masques sur la matrice 
        /// </summary>
        /// <param name="tableauPreMasque"></param>
        /// <param name="tableauZoneModele"></param>
        /// <returns></returns>
        private List<bool?[,]> AppliquerLesMasques(bool?[,] tableauPreMasque, bool?[,] tableauZoneModele)
        {
            List<bool?[,]> lesTableauxMasques = new List<bool?[,]>();

            // Mask 0
            bool?[,] tableauMasque0 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque0.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque0.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && (x + y) % 2 == 0)
                    {
                        tableauMasque0[x, y] = !tableauMasque0[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque0);

            //Mask 1
            bool?[,] tableauMasque1 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque1.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque1.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && ((y % 2) == 0))
                    {
                        tableauMasque1[x, y] = !tableauMasque1[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque1);


            //Mask 2
            bool?[,] tableauMasque2 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque2.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque2.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && x % 3 == 0)
                    {
                        tableauMasque2[x, y] = !tableauMasque2[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque2);


            //Mask 3
            bool?[,] tableauMasque3 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque3.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque3.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && (x + y) % 3 == 0)
                    {
                        tableauMasque3[x, y] = !tableauMasque3[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque3);


            //Mask 4    
            bool?[,] tableauMasque4 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque4.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque4.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && (Math.Floor((double)(y / 2)) + Math.Floor((double)x / 3)) % 2 == 0)
                    {
                        tableauMasque4[x, y] = !tableauMasque4[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque4);


            //Mask 5  
            bool?[,] tableauMasque5 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque5.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque5.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && (x * y) % 2 + (x * y) % 3 == 0)
                    {
                        tableauMasque5[x, y] = !tableauMasque5[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque5);


            //Mask 6   
            bool?[,] tableauMasque6 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque6.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque6.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && (((x * y) % 2) + ((x * y) % 3)) % 2 == 0)
                    {
                        tableauMasque6[x, y] = !tableauMasque6[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque6);



            //Mask 7
            bool?[,] tableauMasque7 = GenerateurCodeQR.ClonerTableau(tableauPreMasque);
            for (int x = 0; x < tableauMasque7.GetLength(0); x++)
            {
                for (int y = 0; y < tableauMasque7.GetLength(1); y++)
                {
                    if (tableauZoneModele[x, y] == null && ((x + y) % 2 + (x * y) % 3) % 2 == 0)
                    {
                        tableauMasque7[x, y] = !tableauMasque7[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauMasque7);

            return lesTableauxMasques;
        }
        /// <summary>
        /// Vérifie quel masque à le score le moins élevé
        /// </summary>
        /// <param name="tableauFinal"></param>
        /// <param name="tableauExemple"></param>
        /// <returns>Le meilleur masque</returns>
        public bool?[,] RenvoyerLeMeilleurTableauMasque(bool?[,] tableauFinal, bool?[,] tableauExemple)
        {
            List<bool?[,]> lesTableauxMasques = AppliquerLesMasques(tableauFinal, tableauExemple);

            lesTableauxMasques[0] = GenerateurCodeQR.RemplirZoneFormat(lesTableauxMasques[0], 0);
            int penaliteMin = GetPenaliteMasque(lesTableauxMasques[0]);

            bool?[,] meilleurTableau = lesTableauxMasques[0];

            for (int i = 0; i < lesTableauxMasques.Count; i++)
            {
                lesTableauxMasques[i] = GenerateurCodeQR.RemplirZoneFormat(lesTableauxMasques[i], i);
                int penaliteCourante = GetPenaliteMasque(lesTableauxMasques[i]);
                if (penaliteCourante <= penaliteMin)
                {
                    penaliteMin = penaliteCourante;
                    meilleurTableau = lesTableauxMasques[i];

                }
            }
            return meilleurTableau;

        }
        /// <summary>
        /// Calcul toutes les pénalitées de chaque masque
        /// </summary>
        /// <param name="tableauMasque"></param>
        /// <returns>le score total des pénalités</returns>
        private int GetPenaliteMasque(bool?[,] tableauMasque)
        {
            //TODO Refaire le calcule des pénalités ( surtout 3)
            int penalite1 = 0;
            int penalite2 = 0;
            int penalite3 = 0;
            int penalite4 = 0;

            //Pénalité #1
            for (int col = 0; col < tableauMasque.GetLength(0); col++)
            {
                int moduleConsecutifCount = 1;
                for (int row = 0; row < tableauMasque.GetLength(1) - 1; row++)
                {
                    if (tableauMasque[col, row] == tableauMasque[col, row + 1])
                    {
                        moduleConsecutifCount++;
                        if (moduleConsecutifCount == 5)
                        {

                            penalite1 += 3;



                        }
                    }
                    else
                    {
                        moduleConsecutifCount = 1;
                    }

                    if (moduleConsecutifCount > 5)
                    {
                        penalite1 += 1;
                    }
                }
            }
            for (int row = 0; row < tableauMasque.GetLength(1); row++)
            {
                int moduleConsecutifCount = 1;
                for (int col = 0; col < tableauMasque.GetLength(0) - 1; col++)
                {
                    if (tableauMasque[col, row] == tableauMasque[col + 1, row])
                    {
                        moduleConsecutifCount++;
                        if (moduleConsecutifCount == 5)
                        {
                            penalite1 += 3;
                        }
                    }
                    else
                    {
                        moduleConsecutifCount = 1;
                    }

                    if (moduleConsecutifCount > 5)
                    {
                        penalite1 += 1;
                    }
                }
            }

            //penalite #2
            for (int col = 0; col < tableauMasque.GetLength(0) - 1; col++)
            {
                for (int row = 0; row < tableauMasque.GetLength(1) - 1; row++)
                {
                    if (tableauMasque[col, row] == tableauMasque[col, row + 1] &&
                        tableauMasque[col, row] == tableauMasque[col + 1, row] &&
                        tableauMasque[col, row] == tableauMasque[col + 1, row + 1])
                    {
                        penalite2 += 3;

                    }
                }
            }

            //penalite #3
            bool[] pattern = new bool[] { false, true, false, false, false, true, false, true, true, true, true };
            bool[] patternInverse = new bool[] { true, true, true, true, false, true, false, false, false, true, false };

            int nombreFois = 0;
            for (int noLigne = 0; noLigne < tableauMasque.GetLength(0); noLigne++)
            {
                //Vérification sur une ligne précise
                bool?[] row = new bool?[tableauMasque.GetLength(1)];
                //Remplissage du tableau à une dimanesion
                for (int j = 0; j < tableauMasque.GetLength(1); j++)
                    row[j] = tableauMasque[noLigne, j];
                bool test = false;

                for (int columnIndex = 0; columnIndex < tableauMasque.GetLength(0) - 11; columnIndex++)
                {
                    for (int k = 0; k < pattern.Length; k++)
                    {
                        if (row[columnIndex + k] == pattern[k])
                            test = true;
                        else
                        {
                            test = false;
                            break;
                        }
                    }
                    if (test == true)
                        nombreFois++;

                    for (int k = 0; k < patternInverse.Length; k++)
                    {
                        if (row[columnIndex + k] == patternInverse[k])
                            test = true;
                        else
                        {
                            test = false;
                            break;
                        }
                    }
                    if (test == true)
                        nombreFois++;
                }


            }
            for (int noColonne = 0; noColonne < tableauMasque.GetLength(1); noColonne++)
            {
                //Vérification sur une ligne précise
                bool?[] col = new bool?[tableauMasque.GetLength(1)];
                //Remplissage du tableau à une dimanesion
                for (int j = 0; j < tableauMasque.GetLength(1); j++)
                    col[j] = tableauMasque[j, noColonne];
                bool test = false;

                for (int ligneIndex = 0; ligneIndex < tableauMasque.GetLength(0) - 11; ligneIndex++)
                {
                    for (int k = 0; k < pattern.Length; k++)
                    {
                        if (col[ligneIndex + k] == pattern[k])
                            test = true;
                        else
                        {
                            test = false;
                            break;
                        }
                    }
                    if (test == true)
                        nombreFois++;

                    for (int k = 0; k < patternInverse.Length; k++)
                    {
                        if (col[ligneIndex + k] == patternInverse[k])
                            test = true;
                        else
                        {
                            test = false;
                            break;
                        }
                    }
                    if (test == true)
                        nombreFois++;
                }

            }

            penalite3 = nombreFois * 40;

            //bool[] pattern = new bool[] { false, true, false, false, false, true, false, true, true, true, true };
            //bool[] patternInverse = new bool[] { true, true, true, true, false, true, false, false, false, true, false };

            ////int correspondance = 0;

            //int i = 0;
            //int j = 0;
            //for (int row = 0; row < tableauMasque.GetLength(1) - 1; row++)
            //{
            //    for (int col = 0; col < tableauMasque.GetLength(0) - 1; col++)
            //    {
            //        if (tableauMasque[col, row] == pattern[i])
            //        {
            //            i++;
            //            if (i == pattern.Length)
            //            {
            //                penalite3 += 40;
            //                i = 0;
            //            }
            //        }
            //        else
            //        {
            //            i = 0;
            //        }

            //        if (tableauMasque[col, row] == patternInverse[j ])
            //        {
            //            j++;
            //            if (j == patternInverse.Length)
            //            {
            //                penalite3 += 40;
            //                j = 0;
            //            }
            //        }
            //        else
            //        {
            //            j = 0;
            //        }

            //    }
            //}
            //for (int col = 0; col < tableauMasque.GetLength(0) - 1; col++)
            //{
            //    for (int row = 0; row < tableauMasque.GetLength(1) - 1; row++)
            //    {
            //        if (tableauMasque[col, row] == pattern[i])
            //        {
            //            i++;
            //            if (i == pattern.Length)
            //            {
            //                penalite3 += 40;
            //                i = 0;
            //            }
            //        }
            //        else
            //        {
            //            i = 0;
            //        }

            //        if (tableauMasque[col, row] == patternInverse[j])
            //        {
            //            j++;
            //            if (j == patternInverse.Length)
            //            {
            //                penalite3 += 40;
            //                j = 0;
            //            }
            //        }
            //        else
            //        {
            //            j = 0;
            //        }
            //    }
            //}

            //if (tableauMasque[col, row] == true && tableauMasque[col, row] == tableauMasque[col, row + 1] &&
            //    tableauMasque[col, row] == tableauMasque[col, row + 2] && tableauMasque[col, row] == tableauMasque[col, row + 3])
            //{
            //    if (row > 3 && row + 10 <= tableauMasque.GetLength(1) - 1)
            //    {
            //        if (tableauMasque[col, row + 4] == false &&
            //    tableauMasque[col, row + 5] == true &&
            //    tableauMasque[col, row + 6] == false &&
            //    tableauMasque[col, row + 7] == false &&
            //    tableauMasque[col, row + 8] == false &&
            //    tableauMasque[col, row + 9] == true &&
            //    tableauMasque[col, row + 10] == false)
            //        {
            //            penalite3 += 40;
            //        }

            //    }
            //    if (row < tableauMasque.GetLength(1) - 3 && row - 10 >= 0)
            //    {

            //    }
            //}
            //if (tableauMasque[col, row] == false &&
            //    tableauMasque[col, row + 1] == true &&
            //    tableauMasque[col, row + 2] == false &&
            //    tableauMasque[col, row + 3] == false &&
            //    tableauMasque[col, row + 4] == false &&
            //    tableauMasque[col, row + 5] == true &&
            //    tableauMasque[col, row + 6] == false)
            //{
            //    if (row >= 4 && tableauMasque[col, row - 1] == true &&
            //        tableauMasque[col, row - 2] == true &&
            //        tableauMasque[col, row - 3] == true &&
            //        tableauMasque[col, row - 4] == true)
            //    {
            //        penalite3 += 40;

            //    }

            //    if (row <= 11 && tableauMasque[col, row + 7] == true &&
            //        tableauMasque[col, row + 8] == true &&
            //        tableauMasque[col, row + 9] == true &&
            //        tableauMasque[col, row + 10] == true)
            //    {
            //        penalite3 += 40;

            //Penalite #4
            int totalModules = tableauMasque.GetLength(0) * tableauMasque.GetLength(1);
            int moduleSombre = 0;


            foreach (bool module in tableauMasque)
            {
                if (!module)
                {
                    moduleSombre++;
                }
            }

            double percentDark = ((double)moduleSombre / totalModules) * 100;

            int MultipleAvant = ((int)percentDark / 5) * 5;
            int MultipleApres = MultipleAvant + 5;

            penalite4 += Math.Min(Math.Abs(MultipleAvant - 50), Math.Abs(MultipleApres - 50)) / 5 * 10;

            return penalite1 + penalite2 + penalite3 + penalite4;
        }
    }
}
