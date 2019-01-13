using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestEditUserControl.xaml
    /// </summary>
    public partial class TestEditUserControl : UserControl
    {
        private IBL bl;

        public TestEditUserControl()
        {
            bl = Bl_imp.GetBl();

            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void testDistance_Checked(object sender, RoutedEventArgs e)
        {
            var x = this.DataContext as Test;
            x.TestDistance = true;
        }

        private void testDistance_Unchecked(object sender, RoutedEventArgs e)
        {
            var x = this.DataContext as Test;
            x.TestDistance = false;
        }
    }
}
