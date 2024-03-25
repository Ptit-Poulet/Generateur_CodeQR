using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CodeQr_Personnalisation.Model;

namespace CodeQr_Personnalisation.ViewModel
{
    internal class GenerateurCodeQr_VM : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateurCodeQr_VM"/> class.
        /// </summary>
        public GenerateurCodeQr_VM()
        {

        }

        public enum ECLevel
        {
            L,
            M,
            Q,
            H
        }

        private List<ECLevel> _listeEClevel = new List<ECLevel> { ECLevel.L, ECLevel.M, ECLevel.Q, ECLevel.H};

        public List<ECLevel> ListeEClevel { 
            get { return _listeEClevel; }
            set
            {
                OnPropertyChanged(nameof(ListeEClevel));
            }
        }

        private ECLevel _eCLevelSelectionne;
        public ECLevel EcLevelSelectionne
        {
            get { return _eCLevelSelectionne; }
            set
            {
                _eCLevelSelectionne = value;
                OnPropertyChanged(nameof(EcLevelSelectionne));
            }
        }
        private List<string> _listeSKColor = new List<string> { "red", "blue", "green", "orange", "black"};

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
    }
}
