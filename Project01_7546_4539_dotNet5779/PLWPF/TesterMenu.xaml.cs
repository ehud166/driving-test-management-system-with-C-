using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                pWindow = parent;
                existTester = tester;
                header_textBlock.Text = string.Format("שלום, " + existTester.FirstName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        

        private void AddOrUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                AddOrUpdateTester addOrUpdateTester = new AddOrUpdateTester(this, existTester);
                addOrUpdateTester.ShowDialog();
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
            this.Close();
            pWindow?.Close();
        }

        private void TesterMenu_OnClosed(object sender, EventArgs e)
        {
            pWindow?.Show();
        }

        private void TesterMenu_OnClosing(object sender, CancelEventArgs e)
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

        private void Update_Test_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                UpdateTest_window updateTest_Window = new UpdateTest_window(this, existTester);
                updateTest_Window.ShowDialog();
                //this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveTester(existTester.ID);
                MessageBox.Show("הוסרת מהמערכת בהצלחה");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddOrUpdateTester addOrUpdateTester = new AddOrUpdateTester(this, existTester);
            #region changesForInfo
            addOrUpdateTester.AddOrUpdateButtonClick.Visibility = Visibility.Collapsed;
            addOrUpdateTester.passwordBox.Visibility = Visibility.Collapsed;
            addOrUpdateTester.passwordBox_label.Visibility = Visibility.Collapsed;
            addOrUpdateTester.validationPasswordBox.Visibility = Visibility.Collapsed;
            addOrUpdateTester.validationPasswordBox_label.Visibility = Visibility.Collapsed;
            addOrUpdateTester.idTextBox.IsEnabled = false;
            addOrUpdateTester.lastNameTextBox.IsEnabled = false;
            addOrUpdateTester.firstNameTextBox.IsEnabled = false;
            addOrUpdateTester.birthdayDatePicker.IsEnabled = false;
            addOrUpdateTester.genderComboBox.IsEnabled = false;
            addOrUpdateTester.addressComboBox.IsEnabled = false;
            addOrUpdateTester.prefixPhoneComboBox.IsEnabled = false;
            addOrUpdateTester.phoneNumbersTextBox.IsEnabled = false;
            addOrUpdateTester.EmailTextBox.IsEnabled = false;
            addOrUpdateTester.VehcileTypeComboBox.IsEnabled = false;
            addOrUpdateTester.maxDistanceTextBox.IsEnabled = false;
            addOrUpdateTester.maxTestsForWeekTextBox.IsEnabled = false;
            addOrUpdateTester.SeniorityTextBox.IsEnabled = false;
            addOrUpdateTester.mySchedule.Visibility = Visibility.Collapsed;
            #endregion
            addOrUpdateTester.ShowDialog();
        }
    }
}
