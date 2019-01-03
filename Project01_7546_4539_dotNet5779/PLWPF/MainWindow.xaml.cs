using BL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IBL bl = Bl_imp.GetBl();
            
            InitializeComponent();

        }
        
        private void Trainee_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            bool a=false;
            LogInWindow logInWindow = new LogInWindow(this,a);
            logInWindow.ShowDialog();
          //  this.Close();
        }

        private void Tester_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            bool a = true;
            LogInWindow logInWindow = new LogInWindow(this,a);
            logInWindow.ShowDialog();
            //  this.Close();
        }
    }
}
