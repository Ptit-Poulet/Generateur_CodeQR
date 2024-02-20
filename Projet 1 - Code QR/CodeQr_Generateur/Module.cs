using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class Module
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public Module() { }

        //Méthodes
        /// <summary>
        /// Création et remplissage d'une matrice pour ensuite l'utiliser
        /// </summary>
        /// <param name="tableauFinal"></param>
        public void RemplirMatrice(bool?[,] tableauFinal)
        {

            var info = new SKImageInfo(21, 21); 

            using var surface = SKSurface.Create(info); 
            var canvas = surface.Canvas;

           
            canvas.Clear(SKColors.Gray);    

           
            for (int i = 0; i < tableauFinal.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFinal.GetLength(1); j++)
                {
                    if (tableauFinal[i, j] == true) 
                        canvas.DrawPoint(i, j, SKColors.White);
                    if (tableauFinal[i, j] == false)
                        canvas.DrawPoint(i, j, SKColors.Black);
                   
                }
            }

          
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite("output.png");

            data.SaveTo(stream);

        }

        /// <summary>
        /// Ajout des module de recherche
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns>Matrice avec les modules de rechercher présent </returns>
        public bool?[,] AjouterModelesDeRecherche(bool?[,] tableau)
        {
            //Le modèle de recherche en haut à droite

            for (int j = 0; j <= 6; j++)
            {
                tableau[0, j] = false;
                tableau[6, j] = false;
            }

            for (int i = 0; i <= 6; i++)
            {
                tableau[i, 0] = false;
                tableau[i, 6] = false;
            }

            for (int j = 1; j <= 5; j++)
            {
                tableau[1, j] = true;
                tableau[5, j] = true;
            }

            for (int i = 1; i <= 5; i++)
            {
                tableau[i, 1] = true;
                tableau[i, 5] = true;
            }

            //le carré au milieu
            for (int i = 2; i <= 4; i++)
            {
                for (int j = 2; j <= 4; j++)
                {
                    tableau[i, j] = false;
                }
            }

            //Le modèle de recherche en bas à gauche
            for (int j = 0; j <= 6; j++)
            {
                tableau[0, j + 21 - 7] = false;
                tableau[6, j + 21 - 7] = false;
            }
            for (int i = 0; i <= 6; i++)
            {
                tableau[i, 21 - 7] = false;
                tableau[i, 20] = false;
            }

            for (int j = 1; j <= 5; j++)
            {
                tableau[1, j + 21 - 7] = true;
                tableau[5, j + 21 - 7] = true;
            }

            for (int i = 1; i <= 5; i++)
            {
                tableau[i, 21 - 7 + 1] = true;
                tableau[i, 19] = true;
            }

            //le carré au milieu
            for (int i = 2; i <= 4; i++)
            {
                for (int j = 2; j <= 4; j++)
                {
                    tableau[i, j + 21 - 7] = false;

                }
            }


            ////Carrée en haut à droite
            for (int i = 0; i <= 6; i++)
            {
                tableau[21 - 7 + i, 0] = false;
                tableau[21 - 7 + i, 6] = false;
            }
            for (int j = 0; j <= 6; j++)
            {
                tableau[21 - 7, j] = false;
                tableau[20, j] = false;
            }

            for (int j = 1; j <= 5; j++)
            {
                tableau[21 - 7 + 1, j] = true;
                tableau[19, j] = true;
            }
            for (int i = 1; i <= 5; i++)
            {
                tableau[i + 21 - 7, 1] = true;
                tableau[i + 21 - 7, 5] = true;
            }
            ///le carré au milieu
            for (int i = 2; i <= 4; i++)
            {
                for (int j = 2; j <= 4; j++)
                {
                    tableau[i + 20 - 7 + 1, j] = false;
                }
            }

            return tableau;
        }

        /// <summary>
        /// AJout des séparateurs
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns>Matrice avec ds séparateur</returns>
        public bool?[,] AjouterSeparateurs(bool?[,] tableau)
        {
           
            for (int j = 0; j <= 7; j++)
            {
                tableau[7, j] = true;
            }
            for (int i = 0; i <= 7; i++)
            {
                tableau[i, 7] = true;
            }

            for (int j = 0; j <= 7; j++)
            {
                tableau[21 - 7 - 1, j] = true;
            }
            for (int i = 0; i <= 7; i++)
            {
                tableau[i + 21 - 7 - 1, 7] = true;
            }

            for (int j = 0; j <= 7; j++)
            {
                tableau[7, j + 21 - 7 - 1] = true;
            }
            for (int i = 0; i <= 7; i++)
            {
                tableau[i, 21 - 7 - 1] = true;
            }

            return tableau;
        }

        /// <summary>
        /// Ajout d'un module d'alignement dans la matrice
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns></returns>
        public bool?[,] AjouterModeleAlignement(bool?[,] tableau)
        {
            //Dans notre cas rien ne se passe car nous sommes en version 1 pour le moment

            return tableau;
        }

        /// <summary>
        /// Ajout des module de syncronisation
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns></returns>
        public bool?[,] AjouterModeleSyncronisation(bool?[,] tableau)
        {
            bool couleur = false;

            //pour la ligne horizontale
            for (int i = 7 + 1; i <= 21 - 7 - 1 - 1; i++)
            {
                tableau[i, 6] = couleur;
                couleur = !couleur;
            }

            couleur = false;
            for (int j = 8; j <= 12; j++)
            {
                tableau[6, j] = couleur;
                couleur = !couleur;
            }

            return tableau;

        }

        /// <summary>
        /// AJout du modulse sombre
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns>Matrice avec le modulse sombre</returns>
        public bool?[,] AjouterModuleSombre(bool?[,] tableau)
        {
            //Nous sommes en version 1 

            tableau[8, 13] = false;

            return tableau;

        }

        /// <summary>
        /// Modifie la zone de format avec des information prédéfini
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns>Matrice avec la zone d'information remplis</returns>
        public bool?[,] ReserverZoneFormat(bool?[,] tableau)
        {
            //ECC level = Q && Mask Pattern = 2
            string information = "011111100110001";


            for (int x = 0; x <= 8; x++)
            {

                if (x < 6)
                    tableau[x, 8] = information[x] == '0' ? true : false;
                else if (x == 6)
                    continue;
                else
                    tableau[x, 8] = information[x - 1] == '0' ? true : false;

            }

            int i = 14;
            for (int y = 0; y <= 7; y++)
            {
                if (y < 6)
                    tableau[8, y] = information[i] == '0' ? true : false;
                else if (y == 6)
                    continue;
                else
                    tableau[8, 7] = information[8] == '0' ? true : false;
                i--;
            }

            i = 7;
            for (int x = 13; x <= 20; x++)
            {
                tableau[x, 8] = information[i] == '0' ? true : false;
                i++;

            }

            i = 0;
            for (int y = 20; y >= 14; y--)
            {

                tableau[8, y] = information[i] == '0' ? true : false;
                i++;
            }

            return tableau;
        }

        /// <summary>
        /// Défini des module noir et blanc selon le CW
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="message"></param>
        /// <returns>Matrice avec le message dedans</returns>
        public bool?[,] PlacerMessage(bool?[,] tableau, int[] message)
        {
            bool couleur = false;
            Groupe groupe = new Groupe();
            int versionCode = 1;
            string messageAPlacer = groupe.StructureMessageFinale(message, versionCode);

            int row = tableau.GetLength(0) - 1;
            int col = tableau.GetLength(1) - 1;
            char currentChar;



            for (int j = 0; j <= 8; j++)
            {
                if (j != 6)

                    tableau[8, j] = true;
            }
            for (int k = 0; k <= 8; k++)
            {
                if (k != 6)
                    tableau[k, 8] = true;

            }


            for (int k = 0; k <= 7; k++)
            {
                tableau[k + 13, 8] = true;

            }


            for (int j = 0; j <= 7; j++)
            {
                if (j != 0)
                    tableau[8, j + 13] = true;

            }



            int i = 0;
            while (col >= 1)
            {

                while (row >= 0)
                {
                    if (tableau[col, row] == null)
                    {
                        currentChar = messageAPlacer[i];
                        couleur = (currentChar == '0') ? true : false;

                        tableau[col, row] = couleur;
                        i++;
                    }
                    col--;

                    if (tableau[col, row] == null)
                    {
                        currentChar = messageAPlacer[i];
                        couleur = (currentChar == '0') ? true : false;

                        tableau[col, row] = couleur;
                        i++;
                    }
                    col++;
                    row--;

                }
                row++;
                col -= 2;

                if (col == 6)
                {
                    col--;
                }


                while (row <= tableau.GetLength(0) - 1)
                {

                    if (tableau[col, row] == null)
                    {
                        currentChar = messageAPlacer[i];
                        couleur = (currentChar == '0') ? true : false;

                        tableau[col, row] = couleur;
                        i++;
                    }

                    col--;

                    if (tableau[col, row] == null)
                    {

                        currentChar = messageAPlacer[i];
                        couleur = (currentChar == '0') ? true : false;

                        tableau[col, row] = couleur;
                        i++;
                    }
                    col++;
                    row++;

                }
                row--;
                col -= 2;

                if (col == 6)
                {
                    col--;
                }


            }

            return tableau;

        }

    }
}
