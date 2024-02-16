using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generateur_Code_QR
{
    public class Masqueur
    {
        public Masqueur() { }
        /// <summary>
        /// Le Masque choisi est appliqué sur le tableau
        /// </summary>
        /// <param name="tableauFinal"></param>
        /// <param name="tableauExemple"></param>
        /// <returns>Matrice avec un masque appliqué</returns>
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
