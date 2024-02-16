using STH1123.ReedSolomon;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SkiaSharp;
using Generateur_Code_QR;
using System.Reflection.Metadata;

namespace matrice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // crate a surface
            string ChaineDebut = "HELLO WORLD";
            string mode = "alphanum";
            CodeQr codeQr = new CodeQr();
            Bloc bloc = new Bloc();
            int nbTotalMotCode = 13, ECcodeword = 13;


            bool?[,] tableauFinal = new bool?[21, 21];
            bool?[,] tableauExemple = new bool?[21, 21];
            int[] tableauMessage = bloc.FormerBloc(codeQr.PreparationCW(ChaineDebut, mode, nbTotalMotCode), nbTotalMotCode, ECcodeword);

            

            //tableauFinal = AjouterModelesDeRecherche(tableauFinal);    //Je remplis les modèles de fonction
            //tableauFinal = AjouterSeparateurs(tableauFinal);   //j'ajoute les séparateurs
            //tableauFinal = AjouterModeleAlignement(tableauFinal);   //j'ajoute les modèles d'alignement
            //tableauFinal = AjouterModeleSyncronisation(tableauFinal);   //Ajouter modèle de syncronisation
            //tableauFinal = AjouterModuleSombre(tableauFinal);  //Ajout du module sombre

            //for (int i = 0; i < tableauFinal.GetLength(0); i++)
            //{
            //    for (int j = 0; j < tableauFinal.GetLength(1); j++)
            //    {
            //        tableauExemple[i, j] = tableauFinal[i, j];
            //    }
            //}

            //tableauFinal = PlacerMessage(tableauFinal, tableauMessage);//Ajout du message dans la matrice
            ////tableauFinal = AppliquerMasque(tableauFinal, tableauExemple);//Ajout du masque No2
            //tableauFinal = ReserverZoneFormat(tableauFinal);




        }
        //static void RemplirMatrice(bool?[,] tableauFinal)
        //{
        //    tableauFinal = new bool?[21, 21];
        //    var info = new SKImageInfo(21, 21);  //Crée un nouveau SKImageInfo avec la largeur et la hauteur spécifiées.

        //    using var surface = SKSurface.Create(info); //Crée une nouvelle surface dont le contenu sera dessiné vers une cible de rendu hors écran, allouée par la surface.
        //    var canvas = surface.Canvas;

        //    // make sure the canvas is blank
        //    canvas.Clear(SKColors.Gray);    //Le fond gris est pour bien voir les modules qui doivent etre blanc

        //    //Je dessine sur ma surface en suivant la logique de remplissage de mon tableau
        //    for (int i = 0; i < tableauFinal.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < tableauFinal.GetLength(1); j++)
        //        {
        //            if (tableauFinal[i, j] == true) // zéro
        //                canvas.DrawPoint(i, j, SKColors.White);
        //            if (tableauFinal[i, j] == false) // un
        //                canvas.DrawPoint(i, j, SKColors.Black);
        //            //if (tableauFinal[i, j] == null)  //Cette condition sera très utile pour le masque
        //            //    canvas.DrawPoint(i, j, SKColors.White);
        //        }
        //    }

        //    // save the file
        //    using var image = surface.Snapshot();
        //    using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        //    using var stream = File.OpenWrite("output.png");

        //    data.SaveTo(stream);

        //}

        //static bool?[,] AjouterModelesDeRecherche(bool?[,] tableau)
        //{
        //    //Le modèle de recherche en haut à droite

        //    for (int j = 0; j <= 6; j++)
        //    {
        //        tableau[0, j] = false;
        //        tableau[6, j] = false;
        //    }

        //    for (int i = 0; i <= 6; i++)
        //    {
        //        tableau[i, 0] = false;
        //        tableau[i, 6] = false;
        //    }

        //    for (int j = 1; j <= 5; j++)
        //    {
        //        tableau[1, j] = true;
        //        tableau[5, j] = true;
        //    }

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        tableau[i, 1] = true;
        //        tableau[i, 5] = true;
        //    }

        //    //le carré au milieu
        //    for (int i = 2; i <= 4; i++)
        //    {
        //        for (int j = 2; j <= 4; j++)
        //        {
        //            tableau[i, j] = false;
        //        }
        //    }

        //    //Le modèle de recherche en bas à gauche
        //    for (int j = 0; j <= 6; j++)
        //    {
        //        tableau[0, j + 21 - 7] = false;
        //        tableau[6, j + 21 - 7] = false;
        //    }
        //    for (int i = 0; i <= 6; i++)
        //    {
        //        tableau[i, 21 - 7] = false;
        //        tableau[i, 20] = false;
        //    }

        //    for (int j = 1; j <= 5; j++)
        //    {
        //        tableau[1, j + 21 - 7] = true;
        //        tableau[5, j + 21 - 7] = true;
        //    }

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        tableau[i, 21 - 7 + 1] = true;
        //        tableau[i, 19] = true;
        //    }

        //    //le carré au milieu
        //    for (int i = 2; i <= 4; i++)
        //    {
        //        for (int j = 2; j <= 4; j++)
        //        {
        //            tableau[i, j + 21 - 7] = false;

        //        }
        //    }


        //    ////Carrée en haut à droite
        //    for (int i = 0; i <= 6; i++)
        //    {
        //        tableau[21 - 7 + i, 0] = false;
        //        tableau[21 - 7 + i, 6] = false;
        //    }
        //    for (int j = 0; j <= 6; j++)
        //    {
        //        tableau[21 - 7, j] = false;
        //        tableau[20, j] = false;
        //    }

        //    for (int j = 1; j <= 5; j++)
        //    {
        //        tableau[21 - 7 + 1, j] = true;
        //        tableau[19, j] = true;
        //    }
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        tableau[i + 21 - 7, 1] = true;
        //        tableau[i + 21 - 7, 5] = true;
        //    }
        //    ///le carré au milieu
        //    for (int i = 2; i <= 4; i++)
        //    {
        //        for (int j = 2; j <= 4; j++)
        //        {
        //            tableau[i + 20 - 7 + 1, j] = false;
        //        }
        //    }

        //    return tableau;
        //}

        //static bool?[,] AjouterSeparateurs(bool?[,] tableau)
        //{
        //    //Les séparateurs qui entourent le module de recherche situé en haut à gauche
        //    for (int j = 0; j <= 7; j++)
        //    {
        //        tableau[7, j] = true;
        //    }
        //    for (int i = 0; i <= 7; i++)
        //    {
        //        tableau[i, 7] = true;
        //    }

        //    //Les séparateurs qui entourent le module de recherche situé en haut à droite
        //    for (int j = 0; j <= 7; j++)
        //    {
        //        tableau[21 - 7 - 1, j] = true;
        //    }
        //    for (int i = 0; i <= 7; i++)
        //    {
        //        tableau[i + 21 - 7 - 1, 7] = true;
        //    }

        //    //Les séparateurs qui entourent le module de recherche situé en bas à gauche
        //    for (int j = 0; j <= 7; j++)
        //    {
        //        tableau[7, j + 21 - 7 - 1] = true;
        //    }
        //    for (int i = 0; i <= 7; i++)
        //    {
        //        tableau[i, 21 - 7 - 1] = true;
        //    }

        //    return tableau;
        //}

        //static bool?[,] AjouterModeleAlignement(bool?[,] tableau)
        //{
        //    //Dans notre cas rien ne se passe car nous sommes en version 1 pour le moment

        //    return tableau;
        //}

        //static bool?[,] AjouterModeleSyncronisation(bool?[,] tableau)
        //{
        //    bool couleur = false;

        //    //pour la ligne horizontale
        //    for (int i = 7 + 1; i <= 21 - 7 - 1 - 1; i++)
        //    {
        //        tableau[i, 6] = couleur;
        //        couleur = !couleur;
        //    }

        //    couleur = false; //ON le remet à false pour pouvoir boucler de nouveau
        //    //pour la ligne verticale
        //    for (int j = 8; j <= 12; j++)
        //    {
        //        tableau[6, j] = couleur;
        //        couleur = !couleur;
        //    }

        //    return tableau;

        //}
        //static bool?[,] AjouterModuleSombre(bool?[,] tableau)
        //{
        //    //Nous sommes en version 1 

        //    //tableau[13, 8] = false;
        //    tableau[8, 13] = false;

        //    return tableau;

        //}

        //static bool?[,] ReserverZoneFormat(bool?[,] tableau)
        //{
        //    //ECC level = Q && Mask Pattern = 2
        //    string information = "011111100110001";


        //    for (int x = 0; x <= 8; x++)
        //    {

        //        if (x < 6)
        //            tableau[x, 8] = information[x] == '0' ? true : false;
        //        else if (x == 6)
        //            continue;
        //        else
        //            tableau[x, 8] = information[x - 1] == '0' ? true : false;

        //    }

        //    int i = 14;
        //    for (int y = 0; y <= 7; y++)
        //    {
        //        if (y < 6)
        //            tableau[8, y] = information[i] == '0' ? true : false;
        //        else if (y == 6)
        //            continue;
        //        else
        //            tableau[8, 7] = information[8] == '0' ? true : false;
        //        i--;
        //    }

        //    i = 7;
        //    for (int x = 13; x <= 20; x++)
        //    {
        //        tableau[x, 8] = information[i] == '0' ? true : false;
        //        i++;

        //    }

        //    i = 0;
        //    for (int y = 20; y >= 14; y--)
        //    {

        //        tableau[8, y] = information[i] == '0' ? true : false;
        //        i++;
        //    }

        //    return tableau;
        //}
        //static bool?[,] PlacerMessage(bool?[,] tableau, int[] message)
        //{
        //    bool couleur = false;
        //    Groupe groupe = new Groupe();
        //    int versionCode = 1;
        //    string messageAPlacer = groupe.StructureMessageFinale(message, versionCode);

        //    int row = tableau.GetLength(0) - 1;
        //    int col = tableau.GetLength(1) - 1;
        //    char currentChar;


        //    //FIX Bleu (True, False, Null, Bleu!)
        //    //Les zones qui entourent les séparateurs situés en haut à gauche
        //    for (int j = 0; j <= 8; j++)
        //    {
        //        if (j != 6)

        //            tableau[8, j] = true;
        //    }
        //    for (int k = 0; k <= 8; k++)
        //    {
        //        if (k != 6)
        //            tableau[k, 8] = true;

        //    }

        //    //La zone qui entoure le module de recherche situé en haut à droite
        //    for (int k = 0; k <= 7; k++)
        //    {
        //        tableau[k + 13, 8] = true;

        //    }

        //    //La zone qui entoure le module de recherche situé en bas à gauche
        //    for (int j = 0; j <= 7; j++)
        //    {
        //        if (j != 0)
        //            tableau[8, j + 13] = true;

        //    }



        //    int i = 0;
        //    while (col >= 1)
        //    {

        //        while (row >= 0)
        //        {
        //            if (tableau[col, row] == null)
        //            {
        //                currentChar = messageAPlacer[i];
        //                couleur = (currentChar == '0') ? true : false;

        //                tableau[col, row] = couleur;
        //                i++;
        //            }
        //            col--;

        //            if (tableau[col, row] == null)
        //            {
        //                currentChar = messageAPlacer[i];
        //                couleur = (currentChar == '0') ? true : false;

        //                tableau[col, row] = couleur;
        //                i++;
        //            }
        //            col++;
        //            row--;

        //        }
        //        row++;
        //        col -= 2;

        //        if (col == 6)
        //        {
        //            col--;
        //        }


        //        while (row <= tableau.GetLength(0) - 1)
        //        {

        //            if (tableau[col, row] == null)
        //            {
        //                currentChar = messageAPlacer[i];
        //                couleur = (currentChar == '0') ? true : false;

        //                tableau[col, row] = couleur;
        //                i++;
        //            }

        //            col--;

        //            if (tableau[col, row] == null)
        //            {

        //                currentChar = messageAPlacer[i];
        //                couleur = (currentChar == '0') ? true : false;

        //                tableau[col, row] = couleur;
        //                i++;
        //            }
        //            col++;
        //            row++;

        //        }
        //        row--;
        //        col -= 2;

        //        if (col == 6)
        //        {
        //            col--;
        //        }


        //    }

        //    return tableau;

        //}

        //static int DeterminerMasque(bool?[,] tableau)
        //{
        //int masqueSelectionne;
        //    return masqueSelectionne;
        //}
        //static bool?[,] AppliquerMasque(bool?[,] tableauFinal, bool?[,] tableauExemple)
        //{

        //    //Mask 2
        //    for (int x = 0; x < tableauFinal.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < tableauFinal.GetLength(1); y++)
        //        {
        //            if (tableauExemple[x, y] == null && x % 3 == 0)
        //            {
        //                tableauFinal[x, y] = !tableauFinal[x, y];
        //            }
        //        }

        //    }

        //    return tableauFinal;
        //}
    }
}