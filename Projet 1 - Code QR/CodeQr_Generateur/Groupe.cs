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
        List<Bloc> _lesBlocs = new List<Bloc>();

        /// <summary>
        /// Constructeur
        /// </summary>
        public Groupe(string[] octetsBlocs, int nbCodeWordsParBloc, int nbBlocs, int nbCodeWordsEC) 
        {
            //TODO: séparer octetsBlocs selon le nombre de blocs
            int curseur = 0;    //commence à zéro pour le 1er groupe  

            for(int i=0;i<nbBlocs;i++)
            {
                //former la sous-section utile pour le bloc

                string[] sousOctetsBloc = new ArraySegment<string>(octetsBlocs, curseur, nbCodeWordsParBloc).ToArray();
                Bloc leBloc = new Bloc(sousOctetsBloc, nbCodeWordsEC);

                _lesBlocs.Add(leBloc);
                curseur += nbCodeWordsParBloc;  //Je déplace le curseur pour le prochain bloc
            }
        }

        public List<Bloc> GetBlocs() { return  _lesBlocs; }


    }
}
