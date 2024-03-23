using CodeQr_Personnalisation.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour VueEnregistrement.xaml
    /// </summary>
    public partial class VueEnregistrement : Window
    {
        public VueEnregistrement()
        {
            InitializeComponent();
            DataContext = new Enregistrement_VM();

            string pathImage = "C:\\Users\\2130331\\Downloads\\Projet 1 - Code QR 4\\Projet 1 - Code QR\\CodeQr_Generateur\\bin\\output.png";
            var uriSouirce = new Uri(pathImage, UriKind.Absolute);
            img_CodeQr.Source = new BitmapImage(uriSouirce);

            img_CodeQr.Stretch = Stretch.Uniform;   //Pour l'étendre sur tout son contenant
        }

        private void Click_Enregistrer(object sender, RoutedEventArgs e)
        {
           SaveFileDialog saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == true)
            {
                saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                //saveFileDialog.Title = "CodeQRPerso";

                if (saveFileDialog.FileName != "")
                {
                    //fichier par le quel sera enregistrer l'image finale
                   FileStream fileStream = (System.IO.FileStream)saveFileDialog.OpenFile();

                    //on écrit(dessine l'image dans ce fichier)

                    fileStream.Close();
                }
            }
            
        }


        private void Click_Annuler(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Enregistrement_VM)
            {
                ((Enregistrement_VM)this.DataContext).Annuler.Execute(null);
                this.Close();
            }
        }
        private void TextBox_SelectionContenu(object sender, RoutedEventArgs e)
        {
            Comportements.TextBox_SelectionContenu(sender, e);
        }
    }
}
