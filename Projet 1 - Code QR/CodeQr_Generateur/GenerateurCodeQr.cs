using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class GenerateurCodeQr
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public GenerateurCodeQr() { }

        /// <summary>
        /// Regroupement des fonctions pour créer le code QR
        /// </summary>
        /// <param name="codeWord"></param>
        /// <param name="nbTotalMotCode"></param>
        /// <param name="ECcodeword"></param>
        public void CreerCodeQR(string codeWord, int nbTotalMotCode, int ECcodeword)
        {
            bool?[,] tableauFinal = new bool?[21, 21];
            bool?[,] tableauExemple = new bool?[21, 21];
            int masqueChoisi = 0;//Sera déterminé ultérieurement
            Module module = new Module();
            Masqueur masqueur = new Masqueur();
            Bloc bloc = new Bloc();
            CodeQr codeQr = new CodeQr();

            int[] message = bloc.FormerBloc(codeWord, nbTotalMotCode, ECcodeword);

            tableauFinal = module.AjouterModelesDeRecherche(tableauFinal);
            tableauFinal = module.AjouterSeparateurs(tableauFinal);
            tableauFinal = module.AjouterModeleAlignement(tableauFinal);
            tableauFinal = module.AjouterModeleSyncronisation(tableauFinal);
            tableauFinal = module.AjouterModuleSombre(tableauFinal);

            for (int i = 0; i < tableauFinal.GetLength(0); i++)
            {
                for (int j = 0; j < tableauFinal.GetLength(1); j++)
                {
                    tableauExemple[i, j] = tableauFinal[i, j];
                }
            }

            tableauFinal = module.PlacerMessage(tableauFinal, message);
            tableauFinal = masqueur.AppliquerMasque(tableauFinal, tableauExemple, masqueChoisi);
            tableauFinal = module.ReserverZoneFormat(tableauFinal);

            module.RemplirMatrice(tableauFinal);
        }
    }
}
