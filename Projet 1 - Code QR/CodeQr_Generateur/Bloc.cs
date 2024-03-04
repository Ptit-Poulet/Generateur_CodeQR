
﻿using STH1123.ReedSolomon;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class Bloc
    {
        //Attributs
        int[] _codeWords;
        int _nbECCCodewords;


        /// <summary>
        /// Constructeur
        /// </summary>
        public Bloc(string[] codeWords, int nbCodeWordsCorrectionErreur) 
        {
            _nbECCCodewords = nbCodeWordsCorrectionErreur;
            _codeWords = new int[codeWords.Length + nbCodeWordsCorrectionErreur];
            int i = 0;
            foreach(string s in codeWords)
            {
                _codeWords[i] = Convert.ToInt32(s,2);
                i++;
            }
            for(;i<_codeWords.Length;i++)
            {
                _codeWords[i] = 0;
            }

            AppliquerReedSolomon();
            //string[] chaineOctects = _chaineEnBinaire.Split(' ');  
            
        }

       

        private void AppliquerReedSolomon()
        {
            ReedSolomonEncoder rse = new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256);
            rse.Encode(_codeWords, _nbECCCodewords);
        }

        public string[] GetEncodedCodeWords()
        {
            string[] result = new string[_codeWords.Length];
            for(int i=0;i<result.Length;i++)
            {
                result[i] = Convert.ToString(_codeWords[i], 2).PadLeft(8,'0');
            }
            return result;
        }
    
        

    }
}
