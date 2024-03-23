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
using System.Reflection;
using Microsoft.Win32;

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

            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            //charger l'image et l'afficher
            string pathImage = Environment.CurrentDirectory + "\\output.png";

            var uriSouirce = new Uri(pathImage, UriKind.Absolute);
            img_CodeQr.Source = new BitmapImage(uriSouirce);

            img_CodeQr.Stretch = Stretch.Uniform;   //Pour l'étendre sur tout son contenant

        }
        private void Click_Visualisation(object sender, RoutedEventArgs e)
        {
            Enregistrement_VM enregistrement_VM = new Enregistrement_VM();
            VueEnregistrement vueEnregistrement = new VueEnregistrement();  
            vueEnregistrement.Owner = this; 
            vueEnregistrement.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            vueEnregistrement.DataContext = enregistrement_VM;
            vueEnregistrement.ShowDialog();
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

        //ClickExplorateur_Fichier
        private void ClickExplorateur_Module(object sender, RoutedEventArgs e)
        {
            PersonnalisateurCodeQr_VM personnalisateurCodeQr_VM = new PersonnalisateurCodeQr_VM();

           OpenFileDialog openFileDialog = new OpenFileDialog();
            string pathImage = "";

            if (openFileDialog.ShowDialog() == true)
            {
                pathImage = openFileDialog.FileName;

                //on affiche le chemin du fichier image sélectionné
                pathModule.Text = pathImage;

                //on affiche aussi un apercu du module
                var uriSouirce = new Uri(pathImage, UriKind.Absolute);
                img_Module.Source = new BitmapImage(uriSouirce);

                img_Module.Stretch = Stretch.Uniform;   //Pour l'étendre sur tout son contenant
            }
        }


        private void ClickExplorateur_Logo(object sender, RoutedEventArgs e)
        {
            PersonnalisateurCodeQr_VM personnalisateurCodeQr_VM = new PersonnalisateurCodeQr_VM();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            string pathImage = "";

            if (openFileDialog.ShowDialog() == true)
            {
                pathImage = openFileDialog.FileName;

                //on affiche le chemin du fichier image sélectionné
                pathLogo.Text = pathImage;

                //on affiche aussi un apercu du Logo
                var uriSouirce = new Uri(pathImage, UriKind.Absolute);
                img_Logo.Source = new BitmapImage(uriSouirce);

                img_Logo.Stretch = Stretch.Uniform;   //Pour l'étendre sur tout son contenant
            }
        }
    }
}
