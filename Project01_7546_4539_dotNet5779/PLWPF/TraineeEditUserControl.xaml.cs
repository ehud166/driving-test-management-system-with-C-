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
    public partial class TraineeEditUserControl : UserControl
    {
        private static Trainee existTrainee = new Trainee();
    //    private static Window pWindow = null;
        LicenseType some = new LicenseType();
        static bool exist = false;
        private IBL bl;
        public event EventHandler<EventArgs> TraineeEdited;

        public TraineeEditUserControl()
        {
            

            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                LessonNumScrollBar.Maximum = 100;
                LessonNumScrollBar.SmallChange = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void ValidationPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password != validationPasswordBox.Password)
            {
                MessageBox.Show("הסיסמאות אינן תואמות אחת לשניה, נסה שוב");
            }
        }
        private void LessonNumScrollBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int a = (int)LessonNumScrollBar.Value;
            LessonNumTextBox.Text = a.ToString();
        }
        



        private void LicenseTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            some = existTrainee.LicenseType.Find(x =>
                 x.Gear.ToString() == GearComboBox.SelectedValue?.ToString() && x.VehicleType.ToString() == VehcileTypeComboBox.SelectedValue?.ToString());
            LessonNumTextBox.Text = some?.LessonNum.ToString();


        }

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EmailTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!bl.IsValidEmailAddress(EmailTextBox?.Text))
            {
                EmailTextBox.Clear();
                EmailTextBox.BorderBrush = Brushes.Red;
            }
            else
                EmailTextBox.BorderBrush = Brushes.LightBlue;
        }

        private void phoneNumbersTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneNumbersTextBox.Text.Length < 7)
                phoneNumbersTextBox.Clear();
        }

        private void AlphabeticValidation_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!bl.IsValidAlphabetic(e?.Text))
            {
                e.Handled = true;
            }
        }

        private void DeleteTraineButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveTrainee(this.idTextBox.Text);
                MessageBox.Show(this.firstNameTextBox.Text+" "+ this.lastNameTextBox.Text+" "+"הוסר/ה מהמערכת בהצלחה");
                //Trainee trainee = new Trainee();
                //this.DataContext = trainee;
                TraineeEdited?.Invoke(this, new EventArgs());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


      
        

        private void UpdateButtonClick_Click(object sender, RoutedEventArgs e)
        {
            existTrainee = this.DataContext as Trainee;
            bl.UpdateTrainee(existTrainee);
            MessageBox.Show(existTrainee.FirstName + " עודכן בהצלחה");
            TraineeEdited?.Invoke(this, new EventArgs());
        }

    }
}

