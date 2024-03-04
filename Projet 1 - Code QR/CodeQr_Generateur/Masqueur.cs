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

        //Il ya certaines formules de masque qui sont à discuter selon moi
        private List<bool?[,]> AppliquerLesMasques(bool?[,] tableauFinal, bool?[,] tableauExemple)
        {
            List<bool?[,]> lesTableauxMasques = new List<bool?[,]>();
            bool?[,] tableauFinalOriginal = tableauFinal;

            // Mask 0
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && (x + y) % 2 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no zero


            tableauFinal = tableauFinalOriginal;    //Je recupère le forme originale, pour pouvoir masquer une nouvelle fois
            //Mask 1
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && y % 2 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no un


            //tableauFinal = tableauFinalOriginal;    //Je recupère le forme originale, pour pouvoir masquer une nouvelle fois
            //Mask 2
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && x % 3 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no deux


            tableauFinal = tableauFinalOriginal;    //Je recupère le forme originale, pour pouvoir masquer une nouvelle fois
            //Mask 3
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && (x + y) % 3 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no trois



            tableauFinal = tableauFinalOriginal;    //Je recupère le forme originale, pour pouvoir masquer une nouvelle fois
            //Mask 4    //La formule de masque  ici est à revoir
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && (Math.Floor((double)(y / 2)) + Math.Floor((double)x / 3)) % 2 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no quatre


            tableauFinal = tableauFinalOriginal;    //Je recupère le forme originale, pour pouvoir masquer une nouvelle fois
            //Mask 5  
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && (x + y) % 2 + (x + y) % 3 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no cinq


            tableauFinal = tableauFinalOriginal;    //Je recupère le forme originale, pour pouvoir masquer une nouvelle fois
            //Mask 6   pareil ici
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && ((x + y) % 2 + (x + y) % 3) % 2 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no six


            tableauFinal = tableauFinalOriginal;    //Je recupère le forme originale, pour pouvoir masquer une nouvelle fois
            //Mask 7  pareil ici
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && ((x + y) % 2 + (x + y) % 3) % 2 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }
            lesTableauxMasques.Add(tableauFinal);   //Le tableau masqué no sept


            return lesTableauxMasques;
        }

        public bool?[,] RenvoyerLeMeilleurTableauMasque(bool?[,] tableauFinal, bool?[,] tableauExemple, out int maskPattern)
        {
            List<bool?[,]> lesTableauxMasques = AppliquerLesMasques(tableauFinal, tableauExemple);
            
            int penaliteMin = GetPenaliteMasque(lesTableauxMasques[0]);
            maskPattern = 0;
            bool?[,] meilleurTableau = lesTableauxMasques[0];

            //Pour chaque tableau, on calcule le score de pénalité,  et on choisit celui qui a le score le plus bas

            //TODO Cette boucle est à revoir
            for (int i = 0; i < lesTableauxMasques.Count; i++)
            {
                if (GetPenaliteMasque(lesTableauxMasques[i]) <= penaliteMin)
                {
                    penaliteMin = GetPenaliteMasque(lesTableauxMasques[i]);
                    meilleurTableau = lesTableauxMasques[i];
                    maskPattern = i;
                }
            }
            return meilleurTableau;

        }

        private int GetPenaliteMasque(bool?[,] tableauMasque)
        {
            int penalite = 0;

            //Pénalité #1
            //total horizontal
            for (int row = 0; row < tableauMasque.GetLength(1) - 1; row++)
            {
                int moduleConsecutifCount = 1;
                for (int col = 0; col < tableauMasque.GetLength(0) - 1; col++)
                {
                    if (tableauMasque[col, row] == tableauMasque[col + 1, row])
                    {
                        moduleConsecutifCount++;
                        if (moduleConsecutifCount == 5)
                        {
                            penalite += 3;
                        }
                    }
                    else
                    {
                        moduleConsecutifCount = 1;
                    }

                    if (moduleConsecutifCount > 5)
                    {
                        penalite += 1;
                    }
                }
            }

            //total vertical
            for (int col = 0; col < tableauMasque.GetLength(0) - 1; col++)
            {
                int moduleConsecutifCount = 1;
                for (int row = 0; row < tableauMasque.GetLength(1) - 1; row++)
                {
                    if (tableauMasque[col, row] == tableauMasque[col, row + 1])
                    {
                        moduleConsecutifCount++;
                        if (moduleConsecutifCount == 5)
                        {
                            penalite += 3;
                        }
                    }
                    else
                    {
                        moduleConsecutifCount = 1;
                    }

                    if (moduleConsecutifCount > 5)
                    {
                        penalite += 1;
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
                        penalite += 3;
                    }
                }
            }

            //penalite #3
            for (int col = 0; col < tableauMasque.GetLength(0) - 1; col++)
            {
                for (int row = 0; row < tableauMasque.GetLength(1) - 6; row++)
                {
                    if (tableauMasque[col, row] == false &&
                        tableauMasque[col, row + 1] == true &&
                        tableauMasque[col, row + 2] == false &&
                        tableauMasque[col, row + 3] == false &&
                        tableauMasque[col, row + 4] == false &&
                        tableauMasque[col, row + 5] == true &&
                        tableauMasque[col, row + 6] == false)
                    {
                        if (row >= 4 && tableauMasque[col, row - 1] == true &&
                            tableauMasque[col, row - 2] == true &&
                            tableauMasque[col, row - 3] == true &&
                            tableauMasque[col, row - 4] == true)
                        {
                            penalite += 40;

                        }

                        if (row <= 11 && tableauMasque[col, row + 7] == true &&
                            tableauMasque[col, row + 8] == true &&
                            tableauMasque[col, row + 9] == true &&
                            tableauMasque[col, row + 10] == true)
                        {
                            penalite += 40;

                        }

                    }

                }
            }
            for (int row = 0; row < tableauMasque.GetLength(1) - 1; row++)
            {
                for (int col = 0; col < tableauMasque.GetLength(0) - 6; col++)
                {
                    if (tableauMasque[col, row] == false &&
                        tableauMasque[col + 1, row] == true &&
                        tableauMasque[col + 2, row] == false &&
                        tableauMasque[col + 3, row] == false &&
                        tableauMasque[col + 4, row] == false &&
                        tableauMasque[col + 5, row] == true &&
                        tableauMasque[col + 6, row] == false)
                    {
                        if (col >= 4 && tableauMasque[col - 1, row] == true &&
                            tableauMasque[col - 2, row] == true &&
                            tableauMasque[col - 3, row] == true &&
                            tableauMasque[col - 4, row] == true)
                        {
                            penalite += 40;

                        }

                        if (col <= 11 && tableauMasque[col + 7, row] == true &&
                            tableauMasque[col + 8, row] == true &&
                            tableauMasque[col + 9, row] == true &&
                            tableauMasque[col + 10, row] == true)
                        {
                            penalite += 40;

                        }
                    }
                }
            }

            //Penalite #4
            int totalModules = tableauMasque.GetLength(0) * tableauMasque.GetLength(1);
            int moduleSombre = 0;

            //compter combien de module sombre
            foreach (bool module in tableauMasque)
            {
                if (module == false)
                {
                    moduleSombre++;
                }
            }

            //calculer pourcentage de module sombre
            double percentDark = ((double)moduleSombre / totalModules) * 100;

            //Determiner les multiple avant et après
            int MultipleAvant = ((int)percentDark / 5) * 5;
            int MultipleApres = MultipleAvant + 5;

            //soustraire 50 et prendre valeur absolue, trouver le plus petit des deux et la multiplier par 10 
            penalite += Math.Min(Math.Abs(MultipleAvant - 50), Math.Abs(MultipleApres - 50)) / 5 * 10;

            return penalite;
        }
    }
}
