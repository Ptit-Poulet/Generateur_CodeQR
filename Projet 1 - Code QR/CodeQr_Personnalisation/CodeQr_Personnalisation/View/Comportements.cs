using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeQr_Personnalisation.View
{
    static class Comportements
    {
        /// <summary>
        /// Sélectionne tout le contenu d'un textbox de manière asynchrone de manière a s'assurer que ce soit la dernière action exécutée.
        /// </summary>
        /// <param name="sender">Composant de l'interface qui a déclenché l'événement.</param>
        /// <param name="e">Objet contenant les arguments du changement de contenu du composant.</param>
        static public async void TextBox_SelectionContenu(object sender, RoutedEventArgs e)
        {

            await Task.Delay(100); // Délai pour simuler l'opération asynchrone

            // L'exécution étant asynchrone, elle s'exécute dans un thread séparé de l'interface,
            // il faut donc manipuler les composants de l'interface sur son thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (sender is TextBox)
                {
                    // Sélectionne le contenu
                    ((TextBox)sender).SelectAll();

                    // Indique que l'événement a été traité
                    e.Handled = true;
                }
            });


        }
    }
}
