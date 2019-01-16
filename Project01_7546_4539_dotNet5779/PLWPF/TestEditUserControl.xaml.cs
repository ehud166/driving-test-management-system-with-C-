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
using static BE.Enums;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestEditUserControl.xaml
    /// </summary>
    public partial class TestEditUserControl : UserControl
    {
        private IBL bl;
        private Test existTest = new Test();
        public event EventHandler<EventArgs> TestEdited;
        
        public TestEditUserControl()
        {
            

            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                this.DataContext = existTest;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var x = sender as ToggleButton;
           // x.Foreground = Brushes.Green;
            x.Content = "עבר";
        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var x = sender as ToggleButton;
          //  x.Foreground = Brushes.Red;
            x.Content = "נכשל";
        }

        private void UpdateTest_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                existTest = this.DataContext as Test;
                bl.UpdateTest(existTest);
                MessageBox.Show(testDistance.Content.ToString());
                TestEdited?.Invoke(this, new EventArgs());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        public void TestEditUserControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
