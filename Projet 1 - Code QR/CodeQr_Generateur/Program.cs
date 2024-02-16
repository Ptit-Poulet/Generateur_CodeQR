namespace CodeQr_Generateur
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            string ChaineDebut = "HELLO WORLD";
            string mode = "alphanum";
            CodeQr codeQr = new CodeQr();
            int nbTotalMotCode = 13;
            int ECcodeword = 13;
            Bloc bloc = new Bloc();
            GenerateurCodeQr generateur = new GenerateurCodeQr();

            string codeWord = codeQr.PreparationCW(ChaineDebut, mode, nbTotalMotCode);
            generateur.CreerCodeQR(codeWord, nbTotalMotCode, ECcodeword);
        }
    }
}