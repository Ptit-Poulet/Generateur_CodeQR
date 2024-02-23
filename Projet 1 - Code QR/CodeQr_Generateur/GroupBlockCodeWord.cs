using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    internal struct GroupBlockCodewordHelper
    {
        public int TotalDataCodeWords { get { return NbBlocksInGroup1 * NbCodeWordsInGroup1Blocks + NbBlocksInGroup2 * NbCodeWordsInGroup2Blocks; } }
        public int HowManyCorrectionCodewords;
        public int NbBlocksInGroup1;
        public int NbCodeWordsInGroup1Blocks;
        public int NbBlocksInGroup2;
        public int NbCodeWordsInGroup2Blocks;

        public GroupBlockCodewordHelper(int hmcw, int nbbg1, int nbcwg1b, int nbbg2, int nbcwg2b)
        {
            HowManyCorrectionCodewords = hmcw; ;
            NbBlocksInGroup1 = nbbg1;
            NbCodeWordsInGroup1Blocks = nbcwg1b;
            NbBlocksInGroup2 = nbbg2;
            NbCodeWordsInGroup2Blocks = nbcwg2b;
        }
    }

    internal class GroupBlockCodewordSplit
    {
        static Dictionary<ECLevel, Dictionary<int, GroupBlockCodewordHelper>> GroupBlockCodewords = new Dictionary<ECLevel, Dictionary<int, GroupBlockCodewordHelper>>()
    {
        {ECLevel.L, new Dictionary<int, GroupBlockCodewordHelper>()
        {
            {1,new GroupBlockCodewordHelper(7,1,19,0,0)}, {2,new GroupBlockCodewordHelper(10,1,34,0,0)}, {3,new GroupBlockCodewordHelper(15,1,55,0,0)}, {4,new GroupBlockCodewordHelper(20,1,80,0,0)}, {5,new GroupBlockCodewordHelper(26,1,108,0,0)},
            {6,new GroupBlockCodewordHelper(18,2,68,0,0)}, {7,new GroupBlockCodewordHelper(20,2,78,0,0)}, {8,new GroupBlockCodewordHelper(24,2,97,0,0)}, {9,new GroupBlockCodewordHelper(30,2,116,0,0)}, {10,new GroupBlockCodewordHelper(18,2,68,2,69)},
            {11,new GroupBlockCodewordHelper(20,4,81,0,0)}, {12,new GroupBlockCodewordHelper(24,2,92,2,93)}, {13,new GroupBlockCodewordHelper(26,4,107,0,0)}, {14,new GroupBlockCodewordHelper(30,3,115,1,116)}, {15,new GroupBlockCodewordHelper(22,5,87,1,88)},
            {16,new GroupBlockCodewordHelper(24,5,98,1,99)}, {17,new GroupBlockCodewordHelper(28,1,107,5,108)}, {18,new GroupBlockCodewordHelper(30,5,120,1,121)}, {19,new GroupBlockCodewordHelper(28,3,113,4,114)}, {20,new GroupBlockCodewordHelper(28,3,107,5,108)},
            {21,new GroupBlockCodewordHelper(28,4,116,4,117)}, {22,new GroupBlockCodewordHelper(28,2,111,7,112)}, {23,new GroupBlockCodewordHelper(30,4,121,5,122)}, {24,new GroupBlockCodewordHelper(30,6,117,4,118)}, {25,new GroupBlockCodewordHelper(26,8,106,4,107)},
            {26,new GroupBlockCodewordHelper(28,10,114,2,115)}, {27,new GroupBlockCodewordHelper(30,8,122,4,123)}, {28,new GroupBlockCodewordHelper(30,3,117,10,118)}, {29,new GroupBlockCodewordHelper(30,7,116,7,117)}, {30,new GroupBlockCodewordHelper(30,5,115,10,116)},
            {31,new GroupBlockCodewordHelper(30,13,115,3,116)}, {32,new GroupBlockCodewordHelper(30,17,115,0,0)}, {33,new GroupBlockCodewordHelper(30,17,115,1,116)}, {34,new GroupBlockCodewordHelper(30,13,115,6,116)}, {35,new GroupBlockCodewordHelper(30,12,121,7,122)},
            {36,new GroupBlockCodewordHelper(30,6,121,14,122)}, {37,new GroupBlockCodewordHelper(30,17,122,4,123)}, {38,new GroupBlockCodewordHelper(30,4,122,18,123)}, {39,new GroupBlockCodewordHelper(30,20,117,4,118)}, {40,new GroupBlockCodewordHelper(30,19,118,6,119)}
        }
        },

        {ECLevel.M, new Dictionary<int, GroupBlockCodewordHelper>()
        {
            {1,new GroupBlockCodewordHelper(10,1,16,0,0)}, {2,new GroupBlockCodewordHelper(16,1,28,0,0)},{3,new GroupBlockCodewordHelper(26,1,44,0,0)},{4,new GroupBlockCodewordHelper(18,2,32,0,0)},{5,new GroupBlockCodewordHelper(24,2,43,0,0)},
            {6,new GroupBlockCodewordHelper(16,4,27,0,0)},{7,new GroupBlockCodewordHelper(18,4,31,0,0)},{8,new GroupBlockCodewordHelper(22,2,38,2,39)},{9,new GroupBlockCodewordHelper(22,3,36,2,37)},{10,new GroupBlockCodewordHelper(26,4,43,1,44)},
            {11,new GroupBlockCodewordHelper(30,1,50,4,51)},{12,new GroupBlockCodewordHelper(22,6,36,2,37)},{13,new GroupBlockCodewordHelper(22,8,37,1,38)},{14,new GroupBlockCodewordHelper(24,4,40,5,41)},{15,new GroupBlockCodewordHelper(24,5,41,5,42)},
            {16,new GroupBlockCodewordHelper(28,7,45,3,46)},{17,new GroupBlockCodewordHelper(28,10,46,1,47)},{18,new GroupBlockCodewordHelper(26,9,43,4,44)},{19,new GroupBlockCodewordHelper(26,3,44,11,45)},{20,new GroupBlockCodewordHelper(26,3,41,13,42)},
            {21,new GroupBlockCodewordHelper(26,17,42,0,0)},{22,new GroupBlockCodewordHelper(28,17,46,0,0)},{23,new GroupBlockCodewordHelper(28,4,47,14,48)},{24,new GroupBlockCodewordHelper(28,6,45,14,46)},{25,new GroupBlockCodewordHelper(28,8,47,13,48)},
            {26,new GroupBlockCodewordHelper(28,19,46,4,47)},{27,new GroupBlockCodewordHelper(28,22,45,3,46)},{28,new GroupBlockCodewordHelper(28,3,45,23,46)},{29,new GroupBlockCodewordHelper(28,21,45,7,46)},{30,new GroupBlockCodewordHelper(28,19,47,10,48)},
            {31,new GroupBlockCodewordHelper(28,2,46,29,47)},{32,new GroupBlockCodewordHelper(28,10,46,23,47)},{33,new GroupBlockCodewordHelper(28,14,46,21,47)},{34,new GroupBlockCodewordHelper(28,14,46,23,47)},{35,new GroupBlockCodewordHelper(28,12,47,26,48)},
            {36,new GroupBlockCodewordHelper(28,6,47,34,48)},{37,new GroupBlockCodewordHelper(28,29,46,14,47)},{38,new GroupBlockCodewordHelper(28,13,46,32,47)},{39,new GroupBlockCodewordHelper(28,40,47,7,48)},{40,new GroupBlockCodewordHelper(28,18,47,31,48)}
        }
        },

        {ECLevel.Q, new Dictionary<int, GroupBlockCodewordHelper>()
        {
            {1,new GroupBlockCodewordHelper(13,1,13,0,0)},{2,new GroupBlockCodewordHelper(22,1,22,0,0)},{3,new GroupBlockCodewordHelper(18,2,17,0,0)},{4,new GroupBlockCodewordHelper(26,2,24,0,0)},{5,new GroupBlockCodewordHelper(18,2,15,2,16)},
            {6,new GroupBlockCodewordHelper(24,4,19,0,0)},{7,new GroupBlockCodewordHelper(18,2,14,4,15)},{8,new GroupBlockCodewordHelper(22,4,18,2,19)},{9,new GroupBlockCodewordHelper(20,4,16,4,17)},{10,new GroupBlockCodewordHelper(24,6,19,2,20)},
            {11,new GroupBlockCodewordHelper(28,4,22,4,23)},{12,new GroupBlockCodewordHelper(26,4,20,6,21)},{13,new GroupBlockCodewordHelper(24,8,20,4,21)},{14,new GroupBlockCodewordHelper(20,11,16,5,17)},{15,new GroupBlockCodewordHelper(30,5,24,7,25)},
            {16,new GroupBlockCodewordHelper(24,15,19,2,20)},{17,new GroupBlockCodewordHelper(28,1,22,15,23)},{18,new GroupBlockCodewordHelper(28,17,22,1,23)},{19,new GroupBlockCodewordHelper(26,17,21,4,22)},{20,new GroupBlockCodewordHelper(30,15,24,5,25)},
            {21,new GroupBlockCodewordHelper(28,17,22,6,23)},{22,new GroupBlockCodewordHelper(30,7,24,16,25)},{23,new GroupBlockCodewordHelper(30,11,24,14,25)},{24,new GroupBlockCodewordHelper(30,11,24,16,25)},{25,new GroupBlockCodewordHelper(30,7,24,22,25)},
            {26,new GroupBlockCodewordHelper(28,28,22,6,23)},{27,new GroupBlockCodewordHelper(30,8,23,26,24)},{28,new GroupBlockCodewordHelper(30,4,24,31,25)},{29,new GroupBlockCodewordHelper(30,1,23,37,24)},{30,new GroupBlockCodewordHelper(30,15,24,25,25)},
            {31,new GroupBlockCodewordHelper(30,42,24,1,25)},{32,new GroupBlockCodewordHelper(30,10,24,35,25)},{33,new GroupBlockCodewordHelper(30,29,24,19,25)},{34,new GroupBlockCodewordHelper(30,44,24,7,25)},{35,new GroupBlockCodewordHelper(30,39,24,14,25)},
            {36,new GroupBlockCodewordHelper(30,46,24,10,25)},{37,new GroupBlockCodewordHelper(30,49,24,10,25)},{38,new GroupBlockCodewordHelper(30,48,24,14,25)},{39,new GroupBlockCodewordHelper(30,43,24,22,25)},{40,new GroupBlockCodewordHelper(30,34,24,34,25)}
        }
        },

        {ECLevel.H, new Dictionary<int, GroupBlockCodewordHelper>()
        {
            {1,new GroupBlockCodewordHelper(17,1,9,0,0)},{2,new GroupBlockCodewordHelper(28,1,16,0,0)},{3,new GroupBlockCodewordHelper(22,2,13,0,0)},{4,new GroupBlockCodewordHelper(16,4,9,0,0)},{5,new GroupBlockCodewordHelper(22,2,11,2,12)},
            {6,new GroupBlockCodewordHelper(28,4,15,0,0)},{7,new GroupBlockCodewordHelper(26,4,13,1,14)},{8,new GroupBlockCodewordHelper(26,4,14,2,15)},{9,new GroupBlockCodewordHelper(24,4,12,4,13)},{10,new GroupBlockCodewordHelper(28,6,15,2,16)},
            {11,new GroupBlockCodewordHelper(24,3,12,8,13)},{12,new GroupBlockCodewordHelper(28,7,14,4,15)},{13,new GroupBlockCodewordHelper(22,12,11,4,12)},{14,new GroupBlockCodewordHelper(24,11,12,5,13)},{15,new GroupBlockCodewordHelper(24,11,12,7,13)},
            {16,new GroupBlockCodewordHelper(30,3,15,13,16)},{17,new GroupBlockCodewordHelper(28,2,14,17,15)},{18,new GroupBlockCodewordHelper(28,2,14,19,15)},{19,new GroupBlockCodewordHelper(26,9,13,16,14)},{20,new GroupBlockCodewordHelper(28,15,15,10,16)},
            {21,new GroupBlockCodewordHelper(30,19,16,6,17)},{22,new GroupBlockCodewordHelper(24,34,13,0,0)},{23,new GroupBlockCodewordHelper(30,16,15,14,16)},{24,new GroupBlockCodewordHelper(30,30,16,2,17)},{25,new GroupBlockCodewordHelper(30,22,15,13,16)},
            {26,new GroupBlockCodewordHelper(30,33,16,4,17)},{27,new GroupBlockCodewordHelper(30,12,15,28,16)},{28,new GroupBlockCodewordHelper(30,11,15,31,16)},{29,new GroupBlockCodewordHelper(30,19,15,26,16)},{30,new GroupBlockCodewordHelper(30,23,15,25,16)},
            {31,new GroupBlockCodewordHelper(30,23,15,28,16)},{32,new GroupBlockCodewordHelper(30,19,15,35,16)},{33,new GroupBlockCodewordHelper(30,11,15,46,16)},{34,new GroupBlockCodewordHelper(30,59,16,1,17)},{35,new GroupBlockCodewordHelper(30,22,15,41,16)},
            {36,new GroupBlockCodewordHelper(30,2,15,64,16)},{37,new GroupBlockCodewordHelper(30,24,15,46,16)},{38,new GroupBlockCodewordHelper(30,42,15,32,16)},{39,new GroupBlockCodewordHelper(30,10,15,67,16)},{40,new GroupBlockCodewordHelper(30,20,15,61,16)}
        }
        }
    };

        static public GroupBlockCodewordHelper getVersionGroupBlockCodewordInfo(ECLevel ec, int version)
        {
            return GroupBlockCodewords[ec][version];
        }

    }
}
