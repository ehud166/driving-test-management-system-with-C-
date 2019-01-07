using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Interaction logic for TraineeMenu_window.xaml
    /// </summary>
    public partial class TraineeMenu_window : Window
    {
        private static Trainee existTrainee = null;
        private static Window pWindow = null;
        private IBL bl;

        public TraineeMenu_window(Window parent, Trainee trainee)
        {
            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                pWindow = parent;
                existTrainee = trainee;
                header_textBlock.Text = string.Format("שלום, " + existTrainee.FirstName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddOrUpdate_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                AddOrUpdateTrainee addOrUpdateTrainee = new AddOrUpdateTrainee(this, existTrainee);
                addOrUpdateTrainee.ShowDialog();
                //this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToMainWindow_clickButton(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_clickButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
            //this.Close();
        }

        private void MyDetailes_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                 this.Hide();
                TraineeDetailes traineeDetailes = new TraineeDetailes(this, existTrainee);
                traineeDetailes.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TraineeMenu_window_OnClosed(object sender, EventArgs e)
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

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveTrainee(existTrainee.ID);
                MessageBox.Show("הוסרת מהמערכת בהצלחה");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void TestRegistar_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.TraineeHave20Lessons(existTrainee.ID))
                {
                    this.Hide();
                    Test_Registar_Window test_Registar_Window = new Test_Registar_Window();
                    test_Registar_Window.ShowDialog();
                    //this.Close();
                }
           
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
