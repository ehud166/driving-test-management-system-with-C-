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
                TraineeEditUserControl.TraineeEdited += TraineeEditUserControl_TraineeEdited;
                TesterEditUserControl.TesterEdited += TesterEditUserControl_TesterEdited;
                TestEditUserControl.TestEdited += TestEditUserControl_TestEdited;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void TesterEditUserControl_TesterEdited(object sender, EventArgs e)
        {
            this.TraineesDataGrid.ItemsSource = bl.GetTraineeList();
        }


        private void TestEditUserControl_TestEdited(object sender, EventArgs e)
        {
            this.TestersDataGrid.ItemsSource = bl.GetTraineeList();
        }

        private void TraineeEditUserControl_TraineeEdited(object sender, EventArgs e)
        {
            this.TestsDataGrid.ItemsSource = bl.GetTraineeList();
        }


        private void TestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            Test test = TestsDataGrid.SelectedItem as Test;
            this.TestEditUserControl.DataContext = test;
        }

        private void TraineesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trainee trainee = TraineesDataGrid.SelectedItem as Trainee;
            this.TraineeEditUserControl.DataContext = trainee;
        }



        private void TestersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            Tester tester = TestersDataGrid.SelectedItem as Tester;
            if (tester != null) { 
           this.TesterEditUserControl.DataContext = tester;
           this.TesterEditUserControl.mySchedule.Build = tester.Schedule;

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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void (object sender, SelectionChangedEventArgs e)
        //{
        //    foreach (var courentKey in bl.GroupTestersByVehicle())
        //    {
        //        grouping.Items.Add(courentKey.Key.ToString());

        //    }
        //}
    }



}

