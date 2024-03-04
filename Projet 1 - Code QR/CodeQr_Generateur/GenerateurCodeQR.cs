using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CodeQr_Generateur
{
    internal class GenerateurCodeQR
    {
        //Atributs possibles de cette classe
        ECLevel _niveauCorrection;
        int _version;
        bool?[,] _tableauInitial;
        Dictionary<int, List<int>> _dictioAlignement;
        Dictionary<ECLevel, Dictionary<int, string>> BitsZoneFormat;
        Dictionary<int, string> BitsZoneVersion;

        public GenerateurCodeQR(int version, ECLevel niveauCortrection)
        {
            _version = version;
            _tableauInitial = new bool?[(_version - 1) * 4 + 21, (_version - 1) * 4 + 21];

            _tableauInitial = InitialiserTableau();
            InitialiserDictio();
            _niveauCorrection = niveauCortrection;
        }

        private bool?[,] InitialiserTableau()
        {
            bool?[,] tableauARemplir;

            tableauARemplir = AjouterModelesDeRecherche();
            tableauARemplir = AjouterSeparateurs(tableauARemplir);

            tableauARemplir = AjouterModeleSyncronisation(tableauARemplir);
            tableauARemplir[8, 4 * _version + 9] = false;//Module sombre
            tableauARemplir = ReserverZoneFormat(tableauARemplir, _version);
            if (_version >= 7)
                tableauARemplir = ReserverZoneVersion(tableauARemplir);

            return tableauARemplir;

        }

        private void InitialiserDictio()
        {
            _dictioAlignement = new Dictionary<int, List<int>>
            {
                {2, new List<int> {6, 18} }, {3, new List < int > { 6, 22 }}, {4, new List < int > { 6, 26 }},
                {5 , new List < int > { 6, 30 }}, {6, new List < int > { 6, 34 }}, {7, new List<int> {6, 22, 38} },
                {8, new List<int> {6, 24, 42} }, {9, new List<int> {6, 26, 46} }, {10, new List<int> {6,28, 50} },
                {11, new List<int> {6, 30, 54}}, {12, new List<int> { 6, 32, 58 } }, {13, new List<int> {6, 34, 62 }},
                {14, new List<int> {6, 26, 46, 66}}, {15, new List<int> {6, 26, 48, 70}}, {16, new List<int> {6, 26, 50, 74}},
                {17, new List<int> {6, 30, 54, 78}}, {18, new List<int> {6, 30, 56, 82}}, {19, new List<int> {6, 30, 58, 86}},
                {20, new List<int> {6, 34, 62, 90}}, {21, new List<int> {6, 28, 50, 72, 94}}, {22, new List<int> {6, 26, 50, 74, 98}},
                {23, new List<int> {6, 30, 54, 78, 102}}, {24, new List<int> {6, 28, 54, 80, 106}}, {25, new List<int> {6, 32, 58, 84, 110}},
                {26, new List<int> {6, 30, 58, 86, 114} }, {27, new List<int> {6, 34, 62, 90, 118} },
                {28, new List<int> {6, 26, 50, 74, 98, 122}}, {29, new List<int> {6, 30, 54, 78, 102, 126}},
                {30, new List<int> {6, 26, 52, 78, 104, 130}}, {31, new List<int> {6, 30, 56, 82, 108, 134}},
                {32, new List<int> {6, 34, 60, 86, 112, 138}}, {33, new List<int> {6, 30, 58, 86, 114, 142}},
                {34, new List<int> {6, 34, 62, 90, 118, 146}},
                {35, new List<int> {6, 30, 54, 78, 102, 126, 150}}, {36, new List<int> {6, 24, 50, 76, 102, 128, 154} },
                {37, new List<int> {6, 28, 54, 80, 106, 132, 158}}, {38, new List<int> {6, 32, 58, 84, 110, 136, 162}},
                {39, new List<int> {6, 26, 54, 82, 110, 138, 166}}, {40, new List<int> {6, 30, 58, 86, 114, 142, 170}},
            };

            BitsZoneFormat = new Dictionary<ECLevel, Dictionary<int, string>>
            {
                {ECLevel.L, new Dictionary<int, string>
                    {
                        {0, "111011111000100" },{1, "111001011110011" },{2, "111110110101010" },{3, "111100010011101" },
                        {4, "110011000101111" },{5, "110001100011000" },{6, "110110001000001" },{7, "110100101110110" },
                    }
                },
                {ECLevel.M, new Dictionary<int, string>
                    {
                        {0, "101010000010010" },{1, "101000100100101" },{2, "101111001111100" },{3, "101101101001011" },
                        {4, "100010111111001" },{5, "100000011001110" },{6, "100111110010111" },{7, "100101010100000" },
                    }
                },
                {ECLevel.Q, new Dictionary<int, string>
                    {
                        {0, "011010101011111" },{1, "011000001101000" },{2, "011111100110001" },{3, "011101000000110" },
                        {4, "010010010110100" },{5, "010000110000011" },{6, "010111011011010" },{7, "010101111101101" },
                    }
                },
                {ECLevel.H, new Dictionary<int, string>
                    {
                        {0, "001011010001001" },{1, "001001110111110" },{2, "001110011100111" },{3, "001100111010000" },
                        {4, "000011101100010" },{5, "000001001010101" },{6, "000110100001100" },{7, "000100000111011" },
                     }
                },

            };

            BitsZoneVersion = new Dictionary<int, string>
            {
                {7, "000111110010010100" }, {8, "001000010110111100" },{9, "001001101010011001" }, {10, "001010010011010011" }, {11, "001011101111110110" },
                {12, "001100011101100010" }, {13, "001101100001000111" }, {14, "001110011000001101" }, {15, "001111100100101000" }, {16, "010000101101111000" },
                {17, "010001010001011101" },{18, "010010101000010111" },{19, "010011010100110010" },{20, "010100100110100110" },{21, "010101011010000011" },
                {22, "010110100011001001" },{23, "010111011111101100" },{24, "011000111011000100" },{25, "011001000111100001" },{26, "011010111110101011" },
                {27, "011011000010001110" },{28, "011100110000011010" },{29, "011101001100111111" },{30, "011110110101110101" },{31, "011111001001010000" },
                {32, "100000100111010101" },{33, "100001011011110000" },{34, "100010100010111010" },{35, "100011011110011111" },{36, "100100101100001011" },
                {37, "100101010000101110" },{38, "100110101001100100" },{39, "100111010101000001" },{40, "101000110001101001" }
            };
        }

        private List<int> GetListPosition()
        {
            return _dictioAlignement[_version];
        }

        private string GetBitsZoneFormat(int maskPattern)
        {

            return BitsZoneFormat[_niveauCorrection][maskPattern];
        }

        private string GetBitsZoneVersion()
        {
            return BitsZoneVersion[_version];
        }


        public void CreerCodeQr(string message)
        {
            //int[] tableauBits = new int[message.Length];
            //for(int i = 0;i < message.Length;i++)
            //{
            //    tableauBits[i] = int.Parse(Convert.ToString(message[i]));
            //}

            bool?[,] tableauFinal = _tableauInitial;
            bool?[,] tableauExemple = new bool?[(_version - 1) * 4 + 21, (_version - 1) * 4 + 21];

            Masqueur masqueur = new Masqueur();

            if (_version >= 2)
            {
                List<int> PositionsPossibles = GetListPosition();
                foreach (int position1 in PositionsPossibles)
                {
                    foreach (int position2 in PositionsPossibles)
                    {
                        tableauFinal = AjouterModeleAlignement(position1, position2, tableauFinal);
                    }
                }
            }

            for (int i = 0; i < tableauFinal.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFinal.GetLength(1); j++)
                {
                    tableauExemple[i, j] = tableauFinal[i, j];
                }
            }


            tableauFinal = PlacerMessage(tableauFinal, message); //À revoir surement

            tableauFinal = masqueur.RenvoyerLeMeilleurTableauMasque(tableauFinal, tableauExemple, out int maskPattern);

            tableauFinal = RemplirZoneFormat(tableauFinal, 0);

            if (_version >= 7)
                tableauFinal = RemplirZoneVersion(tableauFinal);

            RemplirMatrice(tableauFinal);

            /*Infos Importantes pour le prof
             * Les classes Modules et Masque n'ont pas été utilisées
             * et la classe "FonctionsUtiles" comporte plus de 12 focntions
             */

        }

        /// <summary>
        /// Création et remplissage d'une matrice pour ensuite l'utiliser
        /// </summary>
        /// <param name="tableauFinal"></param>
        private void RemplirMatrice(bool?[,] tableauFinal)
        {

            var info = new SKImageInfo(tableauFinal.GetLength(0), tableauFinal.GetLength(1));

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
            using var stream = File.OpenWrite("../../output.png");

            data.SaveTo(stream);

        }
        /// <summary>
        /// Ajout des module de recherche
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns>Matrice avec les modules de rechercher présent </returns>
        public bool?[,] AjouterModelesDeRecherche()
        {
            //On definit d'abord le tableau sur lequel on va travailler
            bool?[,] tableau = new bool?[((_version - 1) * 4 + 21), ((_version - 1) * 4 + 21)];

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
                tableau[0, j + tableau.GetLength(1) - 7] = false;
                tableau[6, j + tableau.GetLength(1) - 7] = false;
            }
            for (int i = 0; i <= 6; i++)
            {
                tableau[i, tableau.GetLength(1) - 7] = false;
                tableau[i, tableau.GetLength(1) - 1] = false;
            }

            for (int j = 1; j <= 5; j++)
            {
                tableau[1, j + tableau.GetLength(1) - 7] = true;
                tableau[5, j + tableau.GetLength(1) - 7] = true;
            }

            for (int i = 1; i <= 5; i++)
            {
                tableau[i, tableau.GetLength(1) - 7 + 1] = true;
                tableau[i, tableau.GetLength(1) - 2] = true;
            }

            //le carré au milieu
            for (int i = 2; i <= 4; i++)
            {
                for (int j = 2; j <= 4; j++)
                {
                    tableau[i, j + tableau.GetLength(1) - 7] = false;

                }
            }


            ////Carrée en haut à droite
            for (int i = 0; i <= 6; i++)
            {
                tableau[tableau.GetLength(0) - 7 + i, 0] = false;
                tableau[tableau.GetLength(0) - 7 + i, 6] = false;
            }
            for (int j = 0; j <= 6; j++)
            {
                tableau[tableau.GetLength(0) - 7, j] = false;
                tableau[tableau.GetLength(0) - 1, j] = false;
            }

            for (int j = 1; j <= 5; j++)
            {
                tableau[tableau.GetLength(0) - 7 + 1, j] = true;
                tableau[tableau.GetLength(0) - 2, j] = true;
            }
            for (int i = 1; i <= 5; i++)
            {
                tableau[i + tableau.GetLength(0) - 7, 1] = true;
                tableau[i + tableau.GetLength(0) - 7, 5] = true;
            }
            ///le carré au milieu
            for (int i = 2; i <= 4; i++)
            {
                for (int j = 2; j <= 4; j++)
                {
                    tableau[i + tableau.GetLength(0) - 7, j] = false;
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
                tableau[tableau.GetLength(0) - 7 - 1, j] = true;
            }
            for (int i = 0; i <= 7; i++)
            {
                tableau[i + tableau.GetLength(0) - 7 - 1, 7] = true;
            }

            for (int j = 0; j <= 7; j++)
            {
                tableau[7, j + tableau.GetLength(1) - 7 - 1] = true;
            }
            for (int i = 0; i <= 7; i++)
            {
                tableau[i, tableau.GetLength(1) - 7 - 1] = true;
            }

            return tableau;
        }

        /// <summary>
        /// Ajout d'un module d'alignement dans la matrice
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns></returns>
        public bool?[,] AjouterModeleAlignement(int row, int col, bool?[,] tableau)
        {
            bool?[,] tableauExemple = AjouterModelesDeRecherche();

            //Une sorte de test
            bool test = true;

            if (tableauExemple[row, col] == null)
            {
                tableau[row, col] = false;

                for (int i = 0; i <= 4; i++)
                {
                    if (tableauExemple[i + row - 2, col - 2] == null)
                        tableau[i + row - 2, col - 2] = false;
                    else
                        break;

                    if (tableauExemple[i + row - 2, col + 2] == null)
                        tableau[i + row - 2, col + 2] = false;
                    else
                        break;
                }
                for (int j = 1; j <= 4; j++)
                {
                    if (tableauExemple[row - 2, j + col - 2] == null)
                        tableau[row - 2, j + col - 2] = false;
                    else
                        break;

                    if (tableauExemple[row + 2, j + col - 2] == null)
                        tableau[row + 2, j + col - 2] = false;
                    else
                        break;
                }

                for (int i = 0; i <= 2; i++)
                {
                    if (tableauExemple[i + row - 1, col - 1] == null)
                        tableau[i + row - 1, col - 1] = true;
                    else
                        break;

                    if (tableauExemple[i + row - 1, col + 1] == null)
                        tableau[i + row - 1, col + 1] = true;
                    else
                        break;
                }

                if (tableauExemple[row - 1, col] == null)
                    tableau[row - 1, col] = true;


                if (tableauExemple[row + 1, col] == null)
                    tableau[row + 1, col] = true;
            }

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
            for (int i = 7 + 1; i <= tableau.GetLength(0) - 7 - 1 - 1; i++)
            {
                tableau[i, 6] = couleur;
                couleur = !couleur;
            }

            couleur = false;
            for (int j = 8; j <= tableau.GetLength(1) - 8 - 1; j++)
            {
                tableau[6, j] = couleur;
                couleur = !couleur;
            }

            return tableau;

        }

        /// <summary>
        /// Reserver la la zone d'informations sur le format
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool?[,] ReserverZoneFormat(bool?[,] tableau, int version)
        {
            //Près du motif de recherche en haut à gauche
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

            //Près du motif de recherche en haut à droite
            for (int k = 0; k <= 7; k++)
            {
                tableau[k + tableau.GetLength(0) - 8, 8] = true;

            }

            //Près du motif de recherche en bas à gauche 
            for (int j = 0; j <= 7; j++)
            {
                //if (j != 0) // ([(4 * V) + 9], 8)
                //    tableau[8, j + 13] = true;

                if (j + tableau.GetLength(1) - 8 != 4 * version + 9)    //Pour sauter le module sombre
                    tableau[8, j + tableau.GetLength(1) - 8] = true;

            }

            return tableau;
        }

        /// <summary>
        /// Reserver la zone d'information sur le version
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns></returns>
        public bool?[,] ReserverZoneVersion(bool?[,] tableau)
        {
            //Au dessus du motif en bas à gauche
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 2; j++)
                    tableau[i, tableau.GetLength(1) - 11 + j] = true;
            }

            //À gauche du motif de recherche en haut à droite
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 5; j++)
                    tableau[i + tableau.GetLength(0) - 9 - 2, j] = true;
            }
            return tableau;
        }

        /// <summary>
        /// Placer le message dans le matrice à travers les couleurs blanche ou noire
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="message"></param>
        /// <returns>Matrice avec le message dedans</returns>
        public bool?[,] PlacerMessage(bool?[,] tableau, string messageAPlacer)
        {
            bool couleur = false;
            //  Groupe groupe = new Groupe();


            int row = tableau.GetLength(0) - 1;
            int col = tableau.GetLength(1) - 1;
            char currentChar;
            int mes = messageAPlacer.Length;



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

        /// <summary>
        /// Modifie la zone de format avec des information prédéfini
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns>Matrice avec la zone d'information remplis</returns>
        public bool?[,] RemplirZoneFormat(bool?[,] tableau, int maskPattern)
        {
            string information = GetBitsZoneFormat(maskPattern);    //011010101011111

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
            for (int x = tableau.GetLength(0) - 8; x <= tableau.GetLength(0) - 1; x++)
            {
                tableau[x, 8] = information[i] == '0' ? true : false;
                i++;

            }

            i = 0;
            for (int y = tableau.GetLength(1) - 1; y >= tableau.GetLength(1) - 7; y--)
            {

                tableau[8, y] = information[i] == '0' ? true : false;
                i++;
            }
            return tableau;
        }


        public bool?[,] RemplirZoneVersion(bool?[,] tableau)
        {
            string information = GetBitsZoneVersion();  //000111110010010100
            int p = information.Length - 1;

            //Au dessus du motif en bas à gauche
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    tableau[i, tableau.GetLength(1) - 11 + j] = information[p] == '0' ? true : false;
                    p--;
                }
            }

            int k = 17;
            for (int j = 0; j <= 5; j++) //000111110010010100
            {
                for (int i = 0; i <= 2; i++)
                {
                    tableau[i + tableau.GetLength(0) - 9 - 2, j] = information[k] == '0' ? true : false;
                    k--;
                }
            }

            return tableau;
        }
    }
}

