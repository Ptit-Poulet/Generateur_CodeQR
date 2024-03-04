namespace CodeQr_Generateur
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //if(args.Length == 0) { return -1; } s'il n'y a aucun argument
            //Donné pas l'utilisateur
            //string chaineDebut = "HELLO WORLD HELLO HELLO HELLO HELLO YOYOYOYOYOYOYOOYOYOYOYOYOYOOYYOOYOYOYOYOYOYOYOYOOYYOYOYOYOOYOYOYYOHELLOHELLOHELLOHELLOH";
            string chaineDebut = "HELLO WORLD";
            int J = chaineDebut.Length;
            ECLevel niveauCorrection = ECLevel.Q;
            //string levelEC = "";
            //args.TakeLast<>();
            //switch (levelEC.ToUpper())
            //{
            //    case "L":
            //        niveauCorrection = ECLevel.L;
            //        break;

            //    case "Q":
            //        niveauCorrection = ECLevel.Q;
            //        break;

            //    case "H":
            //        niveauCorrection = ECLevel.H;
            //        break;

            //    case "M":
            //        niveauCorrection = ECLevel.M;
            //        break;

            //    default:
            //        break;
            //}

            Encodage encodage = new Encodage(chaineDebut, niveauCorrection);
            
            ChEncoding mode = encodage.ChoisirLeMode(chaineDebut); //on choisit d'abord le mode d'encodage des données
            string codeWord = encodage.EncoderChaine(mode, out int version);

            //int version = encodage.TrouverVersion(chaineDebut, mode, niveauCorrection);
            //string[] chaineOctets = codeWord.Split(" ");

            //string[] octetsTest = new string[]
            //{
            //    "01000011", "01010101", "01000110", "10000110", "01010111", "00100110", "01010101", "11000010",
            //    "01110111", "00110010", "00000110", "00010010", "00000110", "01100111", "00100110",
            //    "11110110", "11110110", "01000010", "00000111", "01110110", "10000110", "11110010", "00000111",
            //    "00100110", "01010110", "00010110", "11000110", "11000111", "10010010", "00000110",
            //    "10110110", "11100110", "11110111", "01110111", "00110010", "00000111", "01110110", "10000110",
            //    "01010111", "00100110", "01010010", "00000110", "10000110", "10010111", "00110010", "00000111",
            //    "01000110", "11110111", "01110110", "01010110", "11000010", "00000110", "10010111", "00110010",
            //    "00010000", "11101100", "00010001", "11101100", "00010001", "11101100", "00010001", "11101100"
            //};

            //GenerateurCodeQr generateur = new GenerateurCodeQr(octetsTest, niveauCorrection, 5);
            GenerateurMessage generateur = new GenerateurMessage(codeWord.Split(" "), niveauCorrection, version);

            string messageFinale = generateur.GenererMessageFinal();

            GenerateurCodeQR codeQr = new GenerateurCodeQR(version, niveauCorrection);
            codeQr.CreerCodeQr(messageFinale);

            //GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            //int ECcodeword = group.HowManyCorrectionCodewords;

            //generateur.CreerCodeQR(codeWord, ECcodeword, niveauCorrection, version);
        }
    }
}