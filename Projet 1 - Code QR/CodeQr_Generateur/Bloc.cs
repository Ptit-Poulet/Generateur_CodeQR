
﻿using STH1123.ReedSolomon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class Bloc
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public Bloc() { }

        //Méthodes
        /// <summary>
        /// Utilisation de la bibliotheque ReedSolomon
        /// </summary>
        /// <param name="toEncode"></param>
        /// <param name="ecBytes"></param>
        /// <returns>Codage des erreurs de corrections</returns>
        public ReedSolomonEncoder ReedSolomon(int[] toEncode, int ecBytes)
        {
            ReedSolomonEncoder rse = new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256);
            rse.Encode(toEncode, ecBytes);

            return rse;
        }
    
        /// <summary>
        /// Former un bloc 
        /// </summary>
        /// <param name="codeWord"></param>
        /// <param name="Nbdata"></param>
        /// <param name="ECcodeword"></param>
        /// <returns>Le bloc encode</returns>
        public int[] FormerBloc(string codeWord,ECLevel niveauCorrection, int version)
        {

            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int currentIndex = 0;

            //Donné par le prof 
            //for(int i=0;i<group.NbBlocksInGroup1;i++)
            //{
            //    Bloc leBloc = new Bloc(codeWord.Substring(currentIndex*8, group.NbCodeWordsInGroup1Blocks*8));
            //    group.NbCodeWordsInGroup1Blocks;
            //}
            //for(int i=0;i<group.NbBlocksInGroup2;i++)
            //{

            //}

            int nbTotalMotCode = group.TotalDataCodeWords;
            int ECcodeword = group.HowManyCorrectionCodewords;


            string[] tblCW = codeWord.Split(' ');

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
