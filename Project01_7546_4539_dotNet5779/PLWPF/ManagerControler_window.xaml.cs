using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
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
                pWindow = parent;
                this.TestsDataGrid.ItemsSource = bl.GetTestsList();
                this.TraineesDataGrid.ItemsSource = bl.GetTraineeList();
                this.TestersDataGrid.ItemsSource = bl.GetTestersList();
                // this.TraineesDataGrid.DataContextChanged += 
                TraineeEditUserControl.TraineeDeleted += TraineeEditUserControl_TraineeDeleted;
                TesterEditUserControl.TesterDeleted += TesterEditUserControl_TraineeDeleted;
                TraineeEditUserControl.TraineeEdited += TraineeEditUserControl_TraineeEdited;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TraineeEditUserControl_TraineeDeleted(object sender, EventArgs e)
        {
            this.TraineesDataGrid.ItemsSource = bl.GetTraineeList();
        }
        private void TesterEditUserControl_TraineeDeleted(object sender, EventArgs e)
        {
            this.TestersDataGrid.ItemsSource = bl.GetTestersList();
        }
        private void TraineeEditUserControl_TraineeEdited(object sender, EventArgs e)
        {
            this.TraineesDataGrid.ItemsSource = bl.GetTraineeList();

        }


        private void TestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.TesterEditUserControl.Visibility = Visibility.Collapsed;
            //this.TraineeEditUserControl.Visibility = Visibility.Collapsed;
            //this.TestEditUserControl.Visibility = Visibility.Visible;
            Test test = TestsDataGrid.SelectedItem as Test;
            this.TestEditUserControl.DataContext = test;


        }

        private void TraineesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // this.TestEditUserControl.Visibility = Visibility.Collapsed;
           //this.TesterEditUserControl.Visibility = Visibility.Collapsed;
           // this.TraineeEditUserControl.Visibility = Visibility.Visible;
            Trainee trainee = TraineesDataGrid.SelectedItem as Trainee;
            this.TraineeEditUserControl.DataContext = trainee;
        }

       

        private void TestersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.TraineeEditUserControl.Visibility = Visibility.Collapsed;
            //this.TestEditUserControl.Visibility = Visibility.Collapsed;
            //this.TesterEditUserControl.Visibility = Visibility.Visible;
            Tester tester = TestersDataGrid.SelectedItem as Tester;
            if (tester != null) { 
           this.TesterEditUserControl.DataContext = tester;
            }
        }


        private void ManagerControler_window_OnClosing(object sender, CancelEventArgs e)
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

