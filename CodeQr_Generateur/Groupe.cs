using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class Groupe
    {
        //Attributs
        List<Bloc> _lesBlocs = new List<Bloc>();

        /// <summary>
        /// Constructeur de groupe
        /// </summary>
        public Groupe(string[] octetsBlocs, int nbCodeWordsParBloc, int nbBlocs, int nbCodeWordsEC) 
        {
            int curseur = 0;    

            for(int i=0;i<nbBlocs;i++)
            {
                //former la sous-section utile pour le bloc
                string[] sousOctetsBloc = new ArraySegment<string>(octetsBlocs, curseur, nbCodeWordsParBloc).ToArray();
                Bloc leBloc = new Bloc(sousOctetsBloc, nbCodeWordsEC);

                _lesBlocs.Add(leBloc);
                curseur += nbCodeWordsParBloc;
            }
        }

        /// <summary>
        /// Retourne les blocs
        /// </summary>
        /// <returns></returns>
        public List<Bloc> GetBlocs() { return  _lesBlocs; }


    }
}
