using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeQr_Personnalisation.ViewModel
{
    internal class PersonnalisateurCodeQr_VM : ObservableObject
    {
        public bool EstAnnule { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonnalisateurCodeQr_VM"/> class.
        /// </summary>
        public PersonnalisateurCodeQr_VM()
        {
            Enregistrer = new RelayCommand(Enregistrer_Execute);
            Annuler = new RelayCommand(Annuler_Execute);
        }
        public enum SKColor
        {
            red = 'r',
            blue = 'b',
            green = 'g',
            orange = 'o'
        }

        private List<SKColor> _listeSKColor = new List<SKColor> { SKColor.red, SKColor.blue, SKColor.green, SKColor.orange };

        public List<SKColor> ListeSKColor
        {
            get { return _listeSKColor; }
            set
            {
                OnPropertyChanged(nameof(ListeSKColor));
            }
        }

        private SKColor _SKColorSelectionne;
        public SKColor SKColorSelectionne
        {
            get { return _SKColorSelectionne; }
            set
            {
                _SKColorSelectionne = value;
                OnPropertyChanged(nameof(SKColorSelectionne));
            }
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
