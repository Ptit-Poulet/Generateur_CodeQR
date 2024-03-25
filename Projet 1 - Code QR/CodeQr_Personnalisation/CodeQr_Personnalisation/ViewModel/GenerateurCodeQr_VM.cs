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
        private string _ChaineDebut;
        public string ChaineDebut
        {
            get { return _ChaineDebut; }
            set
            {
                _ChaineDebut = value;
                OnPropertyChanged(nameof(ChaineDebut));
                UpdateCodeCreer();
            }
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
                UpdateCodeCreer();
            }
        }
        private void UpdateCodeCreer()
        {
            CodeCreer = $"{ChaineDebut} {EcLevelSelectionne}";
        }
        private string _CodeCreer;
        public string CodeCreer
        {
            get { return _CodeCreer; }
            set
            {
                _CodeCreer = value;
                OnPropertyChanged(nameof(CodeCreer));
            }
        }
    }
}
