using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    internal static class nbCaractByMode
    {

        static Dictionary<ChEncoding, List<int>> dictio = new Dictionary<ChEncoding, List<int>>()
        {
                {ChEncoding.Num, new List<int> {10, 12, 14} },
                {ChEncoding.AlphaNum, new List<int> {9,11,13} },
                {ChEncoding.Byte, new List<int> {8,16,16} },
                {ChEncoding.Kanji,  new List<int> {8,10,12} },
        };


        public static int GetNbCaract(ChEncoding mode, int version)
        {

            if (1 <= version && version <= 9)
                return dictio[mode][0];
            else if (version <= 26)
                return dictio[mode][1];
            else if (version <= 40)
                return dictio[mode][2];

            return -1;

        }
    }


}
