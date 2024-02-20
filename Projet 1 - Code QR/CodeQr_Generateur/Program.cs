namespace CodeQr_Generateur
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string ChaineDebut = "HELLO WORLD";
            ECLevel niveauCorrection = ECLevel.Q;

            CodeQr codeQr = new CodeQr();
            GenerateurCodeQr generateur = new GenerateurCodeQr();

            ChEncoding mode = codeQr.ChoisirLeMode(ChaineDebut); 
            string codeWord = codeQr.PreparationCW(ChaineDebut, mode, niveauCorrection, out int version);
            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int ECcodeword = group.HowManyCorrectionCodewords;
            int nbTotalMotCode = group.TotalDataCodeWords;
            generateur.CreerCodeQR(codeWord, ECcodeword, niveauCorrection, version);

        }
    }
}