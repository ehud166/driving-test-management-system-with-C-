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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TesterEditUserControl.xaml
    /// </summary>
    public partial class TesterEditUserControl : UserControl
    {
        private IBL bl;
        private static Tester existTester;
        public event EventHandler<EventArgs> TesterEdited;
        public TesterEditUserControl()
        {
            
            
            try
            {
                
                InitializeComponent();
                bl = Bl_imp.GetBl();
                existTester = new Tester();
                SeniorityScrollBar.Maximum = 50;  //max 50 years
                SeniorityScrollBar.SmallChange = 1;
                maxDistanceScrollBar.Maximum = 300; //max 300 km
                maxDistanceScrollBar.SmallChange = 1; //change by int
                maxTestsForWeekScrollBar.Maximum = 30; //max 5 days * 6 hours
                maxTestsForWeekScrollBar.SmallChange = 1;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
     

        private void SeniorityScrollBar_OnValueChangedScrollBar(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                SeniorityTextBox.Text = SeniorityScrollBar.Value.ToString();
            SeniorityScrollBar.Minimum = double.Parse(SeniorityTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+"); //only numbers for all numbers textBox
            e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void phoneNumbersTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (phoneNumbersTextBox.Text.Length < 7)
                {
                    phoneNumbersTextBox.Clear();//delete the field if is not right
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EmailTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void maxTestsForWeekScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                maxTestsForWeekTextBox.Text = maxTestsForWeekScrollBar.Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void maxDistanceScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                maxDistanceTextBox.Text = (maxDistanceScrollBar.Value).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeletButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveTester(this.idTextBox.Text);
                MessageBox.Show(this.firstNameTextBox.Text + " " + this.lastNameTextBox.Text + " " + "הוסר/ה מהמערכת בהצלחה");
                Tester tester = new Tester();
                this.DataContext = tester;
                TesterEdited?.Invoke(this, new EventArgs());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ValidationPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passwordBox.Password != validationPasswordBox.Password)
                {
                MessageBox.Show("הסיסמאות אינן תואמות אחת לשניה, נסה שוב");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void UpdateButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                existTester = this.DataContext as Tester;
            bl.UpdateTester(existTester);
            MessageBox.Show(existTester.FirstName + " עודכן בהצלחה");
            TesterEdited?.Invoke(this, new EventArgs()); //let the window know that the tester details need to refresh
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      



        private void AlphabeticValidation_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!bl.IsValidAlphabetic(e?.Text))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void TesterEditUserControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {

                existTester = this.DataContext as Tester;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
