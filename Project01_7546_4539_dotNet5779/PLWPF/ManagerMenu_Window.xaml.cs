using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ManagerMenu_Window.xaml
    /// </summary>
    public partial class ManagerMenu_Window : Window
    {
        private static Window pWindow = null;
        public ManagerMenu_Window(Window parent, Manager newManager)
        {
            InitializeComponent();
            pWindow = parent;
        }

        private void ManagerMenu_Window_OnClosing(object sender, CancelEventArgs e)
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

        private void Trainee_Click(object sender, RoutedEventArgs e)
        {
            ManagerControler_window managerControler_Window = new ManagerControler_window(this);
            managerControler_Window.ShowDialog();
        }
    }
}
