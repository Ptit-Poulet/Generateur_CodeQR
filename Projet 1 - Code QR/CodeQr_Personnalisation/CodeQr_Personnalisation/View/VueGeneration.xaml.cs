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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeQr_Personnalisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GenerateurCodeQr_VM();
        }
        private void Click_PersonnaliserCodeQR(object sender, RoutedEventArgs e)
        {
            PersonnalisateurCodeQr_VM vmPersonnalisation = new PersonnalisateurCodeQr_VM();
            PersonnalisationCodeQr vuePersonnalisation  = new PersonnalisationCodeQr();

            vuePersonnalisation.Owner = this;
            vuePersonnalisation.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            vuePersonnalisation.DataContext = vmPersonnalisation;
            vuePersonnalisation.ShowDialog();

        }
        private void TextBox_SelectionContenu(object sender, RoutedEventArgs e)
        {
            Comportements.TextBox_SelectionContenu(sender, e);
        }
    }
}
