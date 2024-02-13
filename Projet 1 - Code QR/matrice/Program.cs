using STH1123.ReedSolomon;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SkiaSharp;

namespace matrice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // crate a surface
            var info = new SKImageInfo(21, 21);  //Crée un nouveau SKImageInfo avec la largeur et la hauteur spécifiées.

            using var surface = SKSurface.Create(info); //Crée une nouvelle surface dont le contenu sera dessiné vers une cible de rendu hors écran, allouée par la surface.

            bool?[,] tableauFinal = new bool?[21, 21];

            tableauFinal = AjouterModelesDeRecherche(tableauFinal);    //Je remplis les modèles de fonction
            tableauFinal = AjouterSeparateurs(tableauFinal);   //j'ajoute les séparateurs
            tableauFinal = AjouterModeleAlignement(tableauFinal);   //j'ajoute les modèles d'alignement
            tableauFinal = AjouterModeleSyncronisation(tableauFinal);   //Ajouter modèle de syncronisation

            var canvas = surface.Canvas;

            // make sure the canvas is blank
            canvas.Clear(SKColors.Gray);    //Le fond gris est pour bien voir les modules qui doivent etre blanc



            //Je dessine sur ma surface en suivant la logique de remplissage de mon tableau
            for (int i = 0; i < tableauFinal.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFinal.GetLength(1); j++)
                {
                    if (tableauFinal[i, j] == true)
                        canvas.DrawPoint(i, j, SKColors.White);
                    if (tableauFinal[i, j] == false)
                        canvas.DrawPoint(i, j, SKColors.Black);
                    //if (tableauFinal[i, j] == null)  //Cette condition sera très utile pour le masque
                    //    canvas.DrawPoint(i, j, SKColors.White);
                }
            }





            // save the file
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite("output.png");

            data.SaveTo(stream);
        }



        static bool?[,] AjouterModelesDeRecherche(bool?[,] tableau)
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

        static bool?[,] AjouterSeparateurs(bool?[,] tableau)
        {
            //Les séparateurs qui entourent le module de recherche situé en haut à gauche
            for (int j = 0; j <= 7; j++)
            {
                tableau[7, j] = true;
            }
            for (int i = 0; i <= 7; i++)
            {
                tableau[i, 7] = true;
            }

            //Les séparateurs qui entourent le module de recherche situé en haut à droite
            for (int j = 0; j <= 7; j++)
            {
                tableau[21 - 7 - 1, j] = true;
            }
            for (int i = 0; i <= 7; i++)
            {
                tableau[i + 21 - 7 - 1, 7] = true;
            }

            //Les séparateurs qui entourent le module de recherche situé en bas à gauche
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

        static bool?[,] AjouterModeleAlignement(bool?[,] tableau)
        {
            //Dans notre cas rien ne se passe car nous sommes en version 1 pour le moment

            return tableau;
        }

        static bool?[,] AjouterModeleSyncronisation(bool?[,] tableau)
        {
            bool couleur = false;

            //pour la ligne horizontale
            for(int i = 7 + 1; i <= 21 - 7 - 1 - 1; i++)
            {
                tableau[i, 5] = couleur;
                couleur = !couleur;
            }

            return tableau;

        }

    }
}