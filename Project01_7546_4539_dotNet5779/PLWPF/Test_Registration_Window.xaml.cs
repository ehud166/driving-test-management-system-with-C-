using BE;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Test_Registration_Window.xaml
    /// </summary>
    public partial class Test_Registar_Window : Window
    {
        private static Window pWindow = null;
        public Test_Registar_Window(Window parent, Trainee newTrainee)
        {
            InitializeComponent();
            pWindow = parent;
        }


        private void Test_Registar_Window_OnClosed(object sender, EventArgs e)
        {
            try
            {
                pWindow?.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BackToMainWindow_clickButton(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_clickButton(object sender, RoutedEventArgs e)
        {
            this.Close();
            pWindow?.Close();
        }
    }
}
