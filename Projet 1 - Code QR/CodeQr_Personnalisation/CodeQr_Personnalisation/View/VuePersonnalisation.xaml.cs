using CodeQr_Personnalisation.ViewModel;
using CodeQr_Personnalisation.View;
using CodeQr_Personnalisation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CodeQr_Personnalisation.View
{
    /// <summary>
    /// Logique d'interaction pour PersonnalisationCodeQr.xaml
    /// </summary>
    public partial class PersonnalisationCodeQr : Window
    {
        public PersonnalisationCodeQr()
        {
            InitializeComponent();
        }
        private void Click_Enregistrer(object sender, RoutedEventArgs e)
        {

        }
        private void Click_Annnuler(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is PersonnalisateurCodeQr_VM)
            {
                ((PersonnalisateurCodeQr_VM)this.DataContext).Annuler.Execute(null);
                this.Close();
            }
        }
        private void TextBox_SelectionContenu(object sender, RoutedEventArgs e)
        {
            Comportements.TextBox_SelectionContenu(sender, e);
        }
    }
}
