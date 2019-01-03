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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TesterMenu.xaml
    /// </summary>
    public partial class TesterMenu : Window
    {
        private static Tester existTester = null;
        private static Window pWindow = null;
        private IBL bl;

        public TesterMenu(Window parent, Tester tester)
        {
            InitializeComponent();
            bl = Bl_imp.GetBl();
            pWindow = parent;
            existTester = tester;
            header_textBlock.Text = string.Format("שלום, " + existTester.FirstName);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            
        }

        private void AddOrUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddOrUpdateTester addOrUpdateTester = new AddOrUpdateTester(this, existTester);
            addOrUpdateTester.ShowDialog();
            //this.Close();
        }

        private void BackToMainWindow_clickButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Exit_clickButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
            //this.Close();
        }
    }
}
