using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ManagerControler_window.xaml
    /// </summary>
    public partial class ManagerControler_window : Window
    {
        private static Window pWindow = null;
        private IBL bl;

        public ManagerControler_window(Window parent)
        {
            try
            {

                InitializeComponent();
                bl = Bl_imp.GetBl();
                this.TestsDataGrid.ItemsSource = bl.GetTestsList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItems as DataRowView;
            if (row_selected != null)
            {
                Trainee trainee = new Trainee();
                MessageBox.Show(row_selected["TraineeId"].ToString());
                trainee= bl.GetTraineeById(row_selected["TraineeId"].ToString());
            }
        }
    }
}
