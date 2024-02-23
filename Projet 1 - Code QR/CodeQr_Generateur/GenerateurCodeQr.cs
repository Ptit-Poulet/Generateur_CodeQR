
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class GenerateurCodeQr
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public GenerateurCodeQr() { }

        /// <summary>
        /// Regroupement des fonctions pour créer le code QR
        /// </summary>
        /// <param name="codeWord"></param>
        /// <param name="nbTotalMotCode"></param>
        /// <param name="ECcodeword"></param>
        public void CreerCodeQR(string codeWord, int ECcodeword, ECLevel niveauCorrection, int version)
        {

            bool?[,] tableauFinal = new bool?[21, 21];
            bool?[,] tableauExemple = new bool?[21, 21];
            int masqueChoisi = 0;//Sera déterminé ultérieurement
            Module module = new Module();
            Masqueur masqueur = new Masqueur();
            Bloc bloc = new Bloc();
            CodeQr codeQr = new CodeQr();

         
            int[] message = bloc.FormerBloc(codeWord,niveauCorrection, version);

            tableauFinal = module.AjouterModelesDeRecherche(tableauFinal);
            tableauFinal = module.AjouterSeparateurs(tableauFinal);
            tableauFinal = module.AjouterModeleAlignement(tableauFinal);
            tableauFinal = module.AjouterModeleSyncronisation(tableauFinal);
            tableauFinal = module.AjouterModuleSombre(tableauFinal);

            for (int i = 0; i < tableauFinal.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFinal.GetLength(1); j++)
                {
                    tableauExemple[i, j] = tableauFinal[i, j];
                }
            }

            tableauFinal = module.PlacerMessage(tableauFinal, message);
            tableauFinal = masqueur.AppliquerMasque(tableauFinal, tableauExemple, masqueChoisi);
            tableauFinal = module.ReserverZoneFormat(tableauFinal);

            module.RemplirMatrice(tableauFinal);
        }


        private List<Groupe> FormerGroupes(string[] chaineOctects, ECLevel niveauCorrection, int version)
        {
            GroupBlockCodewordHelper infoGroupesBlocs = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int currentIndex = 0;

            //If 2 Groupes
            //Creer 2 Groupes
            //else
            //Creer 1 seul groupe

            if()

            //Donné par le prof 
            /*for (int i = 0; i < group.NbBlocksInGroup1; i++)
            {
                Bloc leBloc = new Bloc(codeWord.Substring(currentIndex, group.NbCodeWordsInGroup1Blocks));
                group.NbCodeWordsInGroup1Blocks;
            }
            for (int i = 0; i < group.NbBlocksInGroup2; i++)
            {

            }*/
        }

        /// <summary>
        /// Former un bloc 
        /// </summary>
        /// <param name="codeWord"></param>
        /// <param name="Nbdata"></param>
        /// <param name="ECcodeword"></param>
        /// <returns>Le bloc encode</returns>
        public int[] FormerBloc(string[] chaineOctects, ECLevel niveauCorrection, int version)
        {

            

            string[] B1G1 = new string[group.NbCodeWordsInGroup1Blocks];

            for (int i = 0; i < group.NbCodeWordsInGroup1Blocks; i++)
            {

                B1G1[i] = chaineOctects[i];
            }

            string[] B2G1 = new string[group.NbCodeWordsInGroup1Blocks];

            for (int i = group.NbCodeWordsInGroup1Blocks; i < 2 * group.NbCodeWordsInGroup1Blocks; i++)
            {

                B2G1[i] = chaineOctects[i];
            }






            int nbTotalMotCode = group.TotalDataCodeWords;
            int ECcodeword = group.HowManyCorrectionCodewords;

            int[] bloc = new int[nbTotalMotCode + ECcodeword];


            //Convertir en Décimal et mettre le mot de code dans un tableau
            for (int i = 0; i < tblCW.Length; i++)
            {
                bloc[i] = Convert.ToInt32(tblCW[i], 2);
            }

            //Mettre les mots de codes d'erreurs
            for (int i = 0; i < ECcodeword; i++)
            {
                bloc[i + nbTotalMotCode] = '0';
            }

            ReedSolomon(bloc, ECcodeword);

            return bloc;
        }
    }
}
