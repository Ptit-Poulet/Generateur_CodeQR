namespace CodeQr_Generateur
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            string ChaineDebut = "HELLO WORLD";
            ChEncoding mode = ChEncoding.AlphaNum;
            int ECcodeword = 13;
            ECLevel niveauCorrection = ECLevel.Q;
            //int nbTotalMotCode = 13;
            CodeQr codeQr = new CodeQr();
            Bloc bloc = new Bloc();
            GenerateurCodeQr generateur = new GenerateurCodeQr();

            string codeWord = codeQr.PreparationCW(ChaineDebut, mode, niveauCorrection);
            generateur.CreerCodeQR(codeWord, nbTotalMotCode, ECcodeword);
        }
    }
}