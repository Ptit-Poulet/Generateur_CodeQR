namespace CodeQr_Generateur
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Donné pas l'utilisateur
            string ChaineDebut = "HELLO WORLD";
            ECLevel niveauCorrection = ECLevel.Q;

            CodeQr codeQr = new CodeQr();
            GenerateurCodeQr generateur = new GenerateurCodeQr();


            ChEncoding mode = codeQr.ChoisirLeMode(ChaineDebut); //on choisit d'abord le mode d'encodage des données
            string codeWord = codeQr.PreparationCW(ChaineDebut, mode, niveauCorrection, out int version);

            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int ECcodeword = group.HowManyCorrectionCodewords;

            generateur.CreerCodeQR(codeWord, ECcodeword, niveauCorrection, version);
        }
    }
}