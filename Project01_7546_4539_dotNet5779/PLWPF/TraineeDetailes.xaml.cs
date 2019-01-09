using BL;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TraineeDetailes.xaml
    /// </summary>
    public partial class TraineeDetailes : Window
    {
        private static Trainee existTrainee;
        private static Window pWindow = null;
       // static bool exist = false;
        private IBL bl;


        //public TraineeDetailes(Window parent,Trainee newTrainee)
        //{
        //    try
        //    {
        //        InitializeComponent();
        //        bl = Bl_imp.GetBl();
        //        existTrainee = bl.GetTraineeById(newTrainee.ID);
        //        pWindow = parent;
        //        this.DataContext = existTrainee;
        //        TestsDataGrid.DataContext = bl.GetTestsByTraineeId(existTrainee.ID);

        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        public TraineeDetailes(Window parent, Trainee newTrainee)
        {
            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                existTrainee = bl.GetTraineeById(newTrainee.ID);
                pWindow = parent;
                this.DataContext = existTrainee;
                this.TestsDataGrid.ItemsSource = bl.GetTestsByTraineeId(existTrainee.ID);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void MyTests_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LicenseTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(GearComboBox.SelectedValue.ToString() + VehcileTypeComboBox.SelectedValue.ToString());
            LicenseType some = existTrainee.LicenseType.Find(x =>
                x.Gear.ToString() == GearComboBox.SelectedValue?.ToString() && x.VehicleType.ToString() == VehcileTypeComboBox.SelectedValue?.ToString());
            LessonNumTextBox.Content = some?.LessonNum.ToString();
        }

        private void TraineeDetailes_OnClosed(object sender, EventArgs e)
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

        private void OkButtun_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
