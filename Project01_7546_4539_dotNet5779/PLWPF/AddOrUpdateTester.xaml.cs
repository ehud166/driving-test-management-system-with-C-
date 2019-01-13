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
using System.Text.RegularExpressions;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddOrUpdateTester.xaml
    /// </summary>
    public partial class AddOrUpdateTester : Window
    {
        private static Tester existTester;
        private static Window pWindow = null;
        static bool exist = false;
        private IBL bl;

        public AddOrUpdateTester(Window parent, Tester newTester)
        {
            try
            {
                existTester = newTester;
                pWindow = parent;
                this.DataContext = existTester;
                InitializeComponent();
                //tester can't set his own seniority,only manager can
                this.SeniorityScrollBar.Visibility = Visibility.Hidden;
                this.SeniorityLabel.Visibility = Visibility.Hidden;
                this.SeniorityTextBox.Visibility = Visibility.Hidden;


                bl = Bl_imp.GetBl();
                mySchedule.Build = existTester.Schedule;
                idTextBox.IsEnabled = false;
                SeniorityScrollBar.Maximum = 50;
                SeniorityScrollBar.SmallChange = 1;

                if (parent.GetType().ToString() == "PLWPF.TesterMenu")
                {
                    mySchedule.Build = existTester.Schedule;
                    exist = true;
                    AddOrUpdateButtonClick.Content = "עדכן";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                existTester.Schedule = mySchedule.Build;
                if (exist)
                {
                    bl.UpdateTester(existTester);
                    MessageBox.Show(existTester.FirstName + " עודכן בהצלחה");
                }
                else
                {
                    bl.AddTester(existTester);
                    MessageBox.Show(existTester.FirstName + " נרשם בהצלחה");
                    pWindow = new TesterMenu(pWindow, existTester);
                }

                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
        }

       


        private void AddOrUpdateTester_OnClosing(object sender, CancelEventArgs e)
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

        private void SeniorityScrollBar_OnValueChangedScrollBar(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SeniorityTextBox.Text = SeniorityScrollBar.Value.ToString();
            SeniorityScrollBar.Minimum = double.Parse(SeniorityTextBox.Text);

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void phoneNumbersTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneNumbersTextBox.Text.Length < 7)
            {
                phoneNumbersTextBox.Clear();
            }
        }
        private void EmailTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            bool result = bl.IsValidEmailAddress(EmailTextBox?.Text);

            if (!result)
            {
                EmailTextBox.Clear();
                EmailTextBox.BorderBrush = Brushes.Red;
            }
            else
                EmailTextBox.BorderBrush = Brushes.LightBlue;
        }

        private void maxTestsForWeekScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            maxTestsForWeekTextBox.Text = maxTestsForWeekScrollBar.Value.ToString();
        }

        private void maxDistanceScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            maxDistanceTextBox.Text = (maxDistanceScrollBar.Value).ToString();
        }
    }
    
}
