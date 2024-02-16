using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class Groupe
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public Groupe() { }

        //Pour les codewords qui ne donnent qu'un seul group l'entrelacement n'est pas nécessaire

        /// <summary>
        /// Entrelacement des données codeword avec 2groupes de 2 bloc spécifiquement
        /// </summary>
        /// <returns>Données entrelacé</returns>
        public string EntrelacerData(int NbData, int[] MG1B1, int[] MG1B2, int[] MG2B1, int[] MG2B2)
        {
            // pour 5-Q
            //NbData = 16;
            int[,] EntrelaceDonnee = new int[NbData, 4];
            string data = "";

            // Entrelacer les donnes
            for (int i = 0; i < EntrelaceDonnee.GetLength(0); i++)
            {
                EntrelaceDonnee[i, 0] = MG1B1[i];
                EntrelaceDonnee[i, 1] = MG1B2[i];
                EntrelaceDonnee[i, 2] = MG2B1[i];
                EntrelaceDonnee[i, 3] = MG2B2[i];

                if (i == EntrelaceDonnee.GetLength(0) - 1)     //À cause que les deux premiers blocs du groupe 1 ont une taille inférieure de 1 à ceux du groupe 2
                {
                    EntrelaceDonnee[i, 0] = 0;
                    EntrelaceDonnee[i, 1] = 0;

                }
            }
            for (int i = 0; i < EntrelaceDonnee.GetLength(0); i++)
            {
                for (int j = 0; j < EntrelaceDonnee.GetLength(1); j++)
                {
                    if (EntrelaceDonnee[i, j] == 0)
                        continue;
                    else
                        data += EntrelaceDonnee[i, j] + ", ";


                }
            }
            return data;

        }

        /// <summary>
        /// Entrelacement des correction d'erreur codeword avec 2groupes de 2 bloc spécifiquement
        /// </summary>
        /// <param name="ECcodeword"></param>
        /// <param name="NbData"></param>
        /// <param name="MG1B1"></param>
        /// <param name="MG1B2"></param>
        /// <param name="MG2B1"></param>
        /// <param name="MG2B2"></param>
        /// <returns>Correction d'erreur entrelacé</returns>
        public string EntrelacerEC(int ECcodeword, int NbData, int[] MG1B1, int[] MG1B2, int[] MG2B1, int[] MG2B2)
        {
            int[,] EntrelaceCodeErreur = new int[ECcodeword, 4];
            string codeErreur = "";

            //Entrelacer les mots de codes d'erreurs
            for (int i = 0; i < EntrelaceCodeErreur.GetLength(0); i++)
            {
                EntrelaceCodeErreur[i, 0] = MG1B1[i + NbData - 1];
                EntrelaceCodeErreur[i, 1] = MG1B2[i + NbData - 1];
                EntrelaceCodeErreur[i, 2] = MG2B1[i + NbData];
                EntrelaceCodeErreur[i, 3] = MG2B2[i + NbData];
            }

            for (int i = 0; i < EntrelaceCodeErreur.GetLength(0); i++)
            {
                for (int j = 0; j < EntrelaceCodeErreur.GetLength(1); j++)
                {
                    if (EntrelaceCodeErreur[i, j] != 0)
                    {
                        codeErreur += EntrelaceCodeErreur[i, j];

                        // Vérifier si ce n'est pas le dernier élément avant d'ajouter la virgule
                        if (!(i == EntrelaceCodeErreur.GetLength(0) - 1 && j == EntrelaceCodeErreur.GetLength(1) - 1))
                        {
                            codeErreur += ", ";
                        }
                    }

                }
            }
            return codeErreur;
        }

        /// <summary>
        /// Construction du message finale en binaire avec les données et Ec entrelacé
        /// </summary>
        /// <param name="data"></param>
        /// <param name="EC"></param>
        /// <param name="versionCode"></param>
        /// <returns>Binaire à placer dans la matrice</returns>
        public string StructurerMessageFinal(string donnee, string codeErreur, int versionCode)
        {
            string messageFinal = donnee + codeErreur;
            string elementOctet = "";

            string[] tableauMessageFinal = messageFinal.Split(',');

            //Mettre le message en binaire et en 8 bits
            for (int i = 0; i < tableauMessageFinal.Length; i++)
            {

                if (tableauMessageFinal[i] == "")
                    continue;
                else
                {
                    int valeurNumerique = int.Parse(tableauMessageFinal[i]);
                    string elementBinaire = Convert.ToString(valeurNumerique, 2);
                    int remainder = elementBinaire.Length % 8;

                    if (elementBinaire.Length % 8 != 0)
                    {
                        elementOctet += new string('0', 8 - remainder);

                    }
                    elementOctet += elementBinaire;

                }
            }

            List<List<int>> bitsrestantes = VersionRemainderBits(versionCode);

            //Ajout des bits nécessaires
            for (int i = 0; i < bitsrestantes.Count; i++)
            {
                // Récupération de la version et des bits requis 
                int currentVersion = bitsrestantes[i][0];
                int requiredBitsCount = bitsrestantes[i][1];

                // Vérification si la version correspond à versionCode
                if (currentVersion == versionCode)
                {
                    // Ajout des bits nécessaires selon requiredBitsCount
                    for (int j = 0; j < requiredBitsCount; j++)
                    {
                        elementOctet += '0';
                    }

                    break;
                }
            }

            return elementOctet;
        }

        /// <summary>
        /// Construction du message final en binaire sans avoir à entrelacer des données
        /// </summary>
        /// <param name="message"></param>
        /// <param name="versionCode"></param>
        /// <returns>Binaire à placer dans la matrice</returns>
        public string StructureMessageFinale(int[] message, int versionCode)
        {
            string elementOctet = "";

            //Mettre le message en binaire et en 8 bits
            for (int i = 0; i < message.Length; i++)
            {

                if (message[i] == null)
                    continue;
                else
                {
                    int valeurNumerique = message[i];
                    string elementBinaire = Convert.ToString(valeurNumerique, 2);
                    int remainder = elementBinaire.Length % 8;

                    if (elementBinaire.Length % 8 != 0)
                    {
                        elementOctet += new string('0', 8 - remainder);

                    }
                    elementOctet += elementBinaire;

                }
            }

            List<List<int>> bitsrestantes = VersionRemainderBits(versionCode);

            //Ajout des bits nécessaires
            for (int i = 0; i < bitsrestantes.Count; i++)
            {
                // Récupération de la version et des bits requis 
                int currentVersion = bitsrestantes[i][0];
                int requiredBitsCount = bitsrestantes[i][1];

                // Vérification si la version correspond à versionCode
                if (currentVersion == versionCode)
                {
                    // Ajout des bits nécessaires selon requiredBitsCount
                    for (int j = 0; j < requiredBitsCount; j++)
                    {
                        elementOctet += '0';
                    }

                    break;
                }
            }
            return elementOctet;
        }

        /// <summary>
        /// Redonne le nombre de bits nécessaire selon la version
        /// </summary>
        /// <param name="versionCode"></param>
        /// <returns>Nb bits restant pour le placecement dans la matrice</returns>
        public List<List<int>> VersionRemainderBits(int versionCode)
        {
            //Je pense qu'on peut utiliser un dictionnaire pour ça à l'avenir
            List<int> version = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            List<int> requiredBits = new List<int> { 0, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3 };

            List<List<int>> remainderBits = new List<List<int>>();
            //int versionCode = 5;

            //Créer une liste avec les valeurs 
            for (int i = 0; i < 20; i++)
            {
                List<int> value = new List<int>();
                value.Add(version[i]);
                value.Add(requiredBits[i]);
                remainderBits.Add(value);
            }

            return remainderBits;
        }

    }
}
