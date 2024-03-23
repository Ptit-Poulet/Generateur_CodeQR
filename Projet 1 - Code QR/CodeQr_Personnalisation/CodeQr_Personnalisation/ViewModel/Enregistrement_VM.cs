using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeQr_Personnalisation.ViewModel
{
    class Enregistrement_VM
    {
        public bool EstAnnule { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enregistrement_VM"/> class.
        /// </summary>
        public Enregistrement_VM()
        {
            Enregistrer = new RelayCommand(Enregistrer_Execute);
            Annuler = new RelayCommand(Annuler_Execute);
        }

            public ICommand Enregistrer { get; }
        /// <summary>
        /// Retourne les valeurs saisies à l'appelant et indique que l'action n'est pas annulée.
        /// </summary>
        protected virtual void Enregistrer_Execute()
        {

            EstAnnule = false;
        }
        public ICommand Annuler { get; }
        /// <summary>
        /// Indique que l'action est annulée.
        /// </summary>
        protected private void Annuler_Execute() { EstAnnule = true; }
    }
    
}
