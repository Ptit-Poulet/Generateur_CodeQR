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


        //private void Click_AfficherImage(object sender, RoutedEventArgs e)
        //{
        //    GenerateurCodeQr_VM generateurCodeQr_VM = new GenerateurCodeQr_VM();

        //    //string filename = @"";

        //    //ProcessStartInfo startInfo = new ProcessStartInfo(filename);
        //    //startInfo.UseShellExecute = false;
        //    //Process proc = System.Diagnostics.Process.Start(startInfo);
        //    //proc.WaitForExit();
        //    ////TxtResult.Text = proc.ExitCode.ToString();


        //    //charger l'image et l'afficher
        //    string assemblyPath = Assembly.GetExecutingAssembly().Location;
        //    //charger l'image et l'afficher
        //    string pathImage = "C:\\Users\\2130331\\Downloads\\Projet 1 - Code QR 4\\Projet 1 - Code QR\\CodeQr_Generateur\\bin\\output.png";
        //    var uriSouirce = new Uri(pathImage, UriKind.Absolute);
        //    img_CodeQr.Source = new BitmapImage(uriSouirce);

        //    img_CodeQr.Stretch = Stretch.Uniform;   //Pour l'étendre sur tout son contenant

        //}

        private void Click_AfficherImage(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\QRGen\\CodeQr_Generateur.exe";
            

            string args = ChaineDebut.Text + " " + comboBox_EcLevel.SelectedItem;

            //avant de lancer le processus, on vérifie si un ancien fichier existe déja et dans ce s on le supprime d'abord:
            string pathImage = Environment.CurrentDirectory + "\\output.png";
            if (File.Exists(pathImage))
            {
                File.Delete(pathImage);
            }

            ProcessStartInfo startInfo = new ProcessStartInfo(path, args);
            startInfo.UseShellExecute = false;
            Process proc = System.Diagnostics.Process.Start(startInfo);
            proc.WaitForExit();


            //TxtResult.Text = proc.ExitCode.ToString();

            //charger l'image et l'afficher
            var uriSouirce = new Uri(pathImage, UriKind.Absolute);
               img_CodeQr.Source = new BitmapImage(uriSouirce);

             img_CodeQr.Stretch = Stretch.Uniform;   //Pour l'étendre sur tout son contenant
        }
    }
}
