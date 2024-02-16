﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STH1123.ReedSolomon;

namespace Generateur_Code_QR
{
    public class Bloc
    {
      
        /// <summary>
        /// Constructeur
        /// </summary>
        public Bloc() { }

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
        //1-Q
        /// <summary>
        /// FOrmer un bloc avec un seul groupe et bloc
        /// </summary>
        /// <param name="codeWord"></param>
        /// <param name="Nbdata"></param>
        /// <param name="ECcodeword"></param>
        /// <returns>Le bloc encode</returns>
        public int[] FormerBloc(string codeWord, int Nbdata, int ECcodeword)
        {
            ////Spécifique à 1-Q
            //int ECcodeword = 13;

            string[] tblCW = codeWord.Split(' ');

            int[] bloc = new int[Nbdata + ECcodeword];


            //Convertir en Décimal et mettre le mot de code dans un tableau
            for (int i = 0; i < tblCW.Length; i++)
            {
                bloc[i] = Convert.ToInt32(tblCW[i], 2);
            }

            //Mettre les mots de codes d'erreurs
            for(int i = 0; i < ECcodeword; i++)
            {
                bloc[i + Nbdata] = '0';
            }

            ReedSolomon(bloc, ECcodeword);

            return bloc;
        }

      

    }
}