using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class Masqueur
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public Masqueur() { }

        //Méthodes
        /// <summary>
        /// applique le masque choisi sur le code généré
        /// </summary>
        /// <param name="tableauFinal"></param>
        /// <param name="tableauExemple"></param>
        /// <param name="masqueChoisi"></param>
        /// <returns>CodeQr avec son masque</returns>
        public bool?[,] AppliquerMasque(bool?[,] tableauFinal, bool?[,] tableauExemple, int masqueChoisi)
        {

            //Mask 2
            for (int x = 0; x < tableauFinal.GetLength(0); x++)
            {
                for (int y = 0; y < tableauFinal.GetLength(1); y++)
                {
                    if (tableauExemple[x, y] == null && x % 3 == 0)
                    {
                        tableauFinal[x, y] = !tableauFinal[x, y];
                    }
                }

            }

            return tableauFinal;
        }
    }
}
