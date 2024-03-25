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
 
        private List<string> _listeSKColor = new List<string> { "red", "blue", "green", "orange" };

        public List<string> ListeColor
        {
            get { return _listeSKColor; }
            set
            {
                OnPropertyChanged(nameof(ListeColor));
            }
        }

        private string _ColorSelectionne;
        public string ColorSelectionne
        {
            get { return _ColorSelectionne; }
            set
            {
                _ColorSelectionne = value;
                OnPropertyChanged(nameof(ColorSelectionne));
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
