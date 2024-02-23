namespace CodeQr_Generateur
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Donné pas l'utilisateur
            string chaineDebut = "HELLO WORLD";
            ECLevel niveauCorrection = ECLevel.Q;

            CodeQr codeQr = new CodeQr(chaineDebut, niveauCorrection);
            GenerateurCodeQr generateur = new GenerateurCodeQr();


            ChEncoding mode = codeQr.ChoisirLeMode(chaineDebut); //on choisit d'abord le mode d'encodage des données
            string codeWord = codeQr.EncoderChaine(chaineDebut, mode, niveauCorrection, out int version);

            GroupBlockCodewordHelper group = GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(niveauCorrection, version);
            int ECcodeword = group.HowManyCorrectionCodewords;

            generateur.CreerCodeQR(codeWord, ECcodeword, niveauCorrection, version);
        }
    }
}