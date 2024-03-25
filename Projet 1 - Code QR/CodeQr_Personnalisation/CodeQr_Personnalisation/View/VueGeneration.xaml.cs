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
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;

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
            PersonnalisationCodeQr vuePersonnalisation = new PersonnalisationCodeQr();

            vuePersonnalisation.Owner = this;
            vuePersonnalisation.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            vuePersonnalisation.DataContext = vmPersonnalisation;
            vuePersonnalisation.ShowDialog();

        }
        private void Click_PasImplémenté(object sender, RoutedEventArgs e)
        {
           
            string message = $"Cette fonction n'est pas encore disponible.";

          MessageBox.Show(message," ", MessageBoxButton.OK, MessageBoxImage.Warning);
           
        }
        private void TextBox_SelectionContenu(object sender, RoutedEventArgs e)
        {
            Comportements.TextBox_SelectionContenu(sender, e);
        }

        private void Click_AfficherImage(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\QRGen\\CodeQr_Generateur.exe";

            Modifiable.Chaine = ChaineDebut.Text;
            Modifiable.Eclevel = comboBox_EcLevel.SelectedItem?.ToString() ?? "Q";
            Modifiable.Couleur = comboBox_Couleur.SelectedItem?.ToString() ?? "black";
            string args = ($"\"{Modifiable.Chaine}\" {Modifiable.Eclevel} {Modifiable.Couleur}");

            
            string pathImage = Environment.CurrentDirectory + "\\output.png";
            if (File.Exists(pathImage))
            {
                File.Delete(pathImage);
            }

            GenerateQRCode(path, args, pathImage);

            if (!string.IsNullOrEmpty(ChaineDebut.Text))
            {
                img_CodeQr.Source = LoadImage(pathImage);
            }

            img_CodeQr.Stretch = Stretch.Uniform;
        }

        private void GenerateQRCode(string filename, string args, string outputPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(filename, args);
            startInfo.UseShellExecute = false;
            Process proc = System.Diagnostics.Process.Start(startInfo);
            proc.WaitForExit();
        }

        private BitmapImage LoadImage(string url)
        {
            BitmapImage returnVal = null;
            if (url != null)
            {
                BitmapImage image = new BitmapImage();
                using (FileStream stream = File.OpenRead(url))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                returnVal = image;
            }
            return returnVal;
        }
    }
}
