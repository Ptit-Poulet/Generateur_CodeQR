using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STH1123.ReedSolomon;

namespace Generateur_Code_QR
{
    internal class Bloc
    {

        public ReedSolomonEncoder CorrectionErreur()
        {

            //Exemple Bibliotheque Reed - Solomon
            int[] G1B1 = new int[33] { Convert.ToInt32("01000011", 2), Convert.ToInt32("01010101", 2), Convert.ToInt32("01000110", 2), Convert.ToInt32("10000110", 2), Convert.ToInt32("01010111", 2), Convert.ToInt32("00100110", 2), Convert.ToInt32("01010101", 2), Convert.ToInt32("11000010", 2), Convert.ToInt32("01110111", 2), Convert.ToInt32("00110010", 2), Convert.ToInt32("00000110", 2), Convert.ToInt32("00010010", 2), Convert.ToInt32("00000110", 2), Convert.ToInt32("01100111", 2), Convert.ToInt32("00100110", 2), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            ReedSolomonEncoder rse = new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256);
            rse.Encode(G1B1, 18);
            
            return rse;
        }

    }
}
