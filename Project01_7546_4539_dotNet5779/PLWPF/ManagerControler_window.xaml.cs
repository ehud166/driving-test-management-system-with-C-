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

                //this window sign for events from user controls to make him refresh his data
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
            try
            {
                this.TestersDataGrid.ItemsSource = bl.GetTestersList(); //refresh
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
        }


        private void TestEditUserControl_TestEdited(object sender, EventArgs e)
        {
            try
            {
                this.TestsDataGrid.ItemsSource = bl.GetTestsList();  //refresh

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
        }

        private void TraineeEditUserControl_TraineeEdited(object sender, EventArgs e)
        {
            try
            {
                this.TraineesDataGrid.ItemsSource = bl.GetTraineeList();  //refresh

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// making the matching user control to present the specific test details on the user control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                  if (TestsDataGrid.SelectedItem != null)
                  {
                   Test test = TestsDataGrid.SelectedItem as Test;
                   this.TestEditUserControl.DataContext = test;
                  }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
        }



        private void TraineesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TraineesDataGrid.SelectedItem != null)
                {
                Trainee trainee = TraineesDataGrid.SelectedItem as Trainee;
                this.TraineeEditUserControl.DataContext = trainee;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
        }



        private void TestersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TestersDataGrid.SelectedItem != null)
                {
                Tester tester = TestersDataGrid.SelectedItem as Tester;
                this.TesterEditUserControl.DataContext = tester;
                this.TesterEditUserControl.mySchedule.Build = tester.Schedule;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
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

        /// <summary>
        /// group trainees by parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupTrainees_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.TraineesDataGrid.ItemsSource = null;
                this.TraineesDataGrid.Items.Clear();
                //List<Trainee> list = new List<Trainee>();
                //TraineesDataGrid.ItemsSource = list;

                switch (GroupTrainees.SelectedIndex)
                {
                    case 0:
                     
                        foreach (var item in bl.GroupTraineesByTeacher())
                        {
                            foreach (var byTeacher in item)
                            {
                                TraineesDataGrid.Items.Add(byTeacher);
                            }

                            var split = new GridSplitter();
                        }

                        break;
                    case 1:

                        foreach (var item in bl.GroupTraineesBySchool())
                        {
                            foreach (var bySchool in item)
                            {
                                TraineesDataGrid.Items.Add(bySchool);
                            }

                            var split = new GridSplitter();
                        }

                        break;

                    case 2:

                        foreach (var item in bl.GroupTraineesByGender())
                        {
                            foreach (var byGender in item)
                            {
                                TraineesDataGrid.Items.Add(byGender);
                            }

                            var split = new GridSplitter();
                        }

                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// making the test data grid refresh after we set a date for new test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.TestsDataGrid.ItemsSource = bl.GetTestsList();  }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }



}

