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
    /// Interaction logic for UpdateTest_window.xaml
    /// </summary>
    public partial class UpdateTest_window : Window
    {
        private static Window pWindow = null;
        private IBL bl;
        private static Tester existTester = new Tester();
        public UpdateTest_window(Window parent,Tester newTester)
        {
            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                existTester = newTester;
                pWindow = parent;
                this.TestsDataGrid.ItemsSource = existTester.TesterTests;

            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Test test = TestsDataGrid.SelectedItem as Test;
                this.TestEditUserControl.DataContext = test;
                this.TestEditUserControl.TestEditUserControl_OnDataContextChanged(sender,new DependencyPropertyChangedEventArgs());
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

    }
}
