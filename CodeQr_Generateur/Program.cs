using SkiaSharp;
using System.Drawing;

namespace CodeQr_Generateur
{
    internal class Program
    {
        static int  Main(string[] args)
        {

            //Pour modifier les console arguments pour le débogage, allez dans "Déboguer -> Propriétés de débogage de DemoArgumentsConsole" et mettez les valeurs désirées
            if (args.Length == 0) { return 0; }

            else
            {
                string chaineDebut = args[0];
                int J = chaineDebut.Length;
                ECLevel niveauCorrection = ECLevel.Q;   //par défaut
                SKColor couleur;

                //Détermination du niveau de correction d'erreur en fonction du second argument
                switch (args[1])
                {
                    case "Q":
                        niveauCorrection = ECLevel.Q;
                        break;
                    case "H":
                        niveauCorrection = ECLevel.H;
                        break;
                    case "L":
                        niveauCorrection = ECLevel.L;
                        break;
                    case "M":
                        niveauCorrection = ECLevel.M;
                        break;
                }
                //switch (args[2].ToLower())
                switch("orange")
                {
                    
                    case "red":
                        couleur =  SKColors.Red;
                        break;
                    case "green":
                        couleur = SKColors.Green;
                        break;
                    case "blue":
                        couleur = SKColors.Blue;
                        break;
                    case "orange":
                        couleur = SKColors.Orange;
                        break;
                    case "black":
                        couleur = SKColors.Black;
                        break;
                    default:
                        couleur = SKColors.Black;
                        break;
                }

                Encodage encodage = new Encodage(chaineDebut, niveauCorrection);

                ChEncoding mode = encodage.ChoisirLeMode(chaineDebut); //on choisit d'abord le mode d'encodage des données
                string codeWord = encodage.EncoderChaine(mode, out int version);


                GenerateurMessage generateur = new GenerateurMessage(codeWord.Split(" "), niveauCorrection, version);

                string messageFinale = generateur.GenererMessageFinal();

                GenerateurCodeQR codeQr = new GenerateurCodeQR(version, niveauCorrection);
                codeQr.CreerCodeQr(messageFinale, couleur);

                string pathImage = Environment.CurrentDirectory + "\\bin\\output.png";

                return 1;
            }
        }
    }
}