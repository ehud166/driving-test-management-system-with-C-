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
                LessonNumScrollBar.Maximum = 100; //maximum 100 lessons per vehicle type for student
                LessonNumScrollBar.SmallChange = 1; // lessons raise up by 1 (int)
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void ValidationPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passwordBox.Password != validationPasswordBox.Password && passwordBox.Password.Length == validationPasswordBox.Password.Length)
            {
                MessageBox.Show("הסיסמאות אינן תואמות אחת לשניה, נסה שוב");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void LessonNumScrollBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                int a = (int)LessonNumScrollBar.Value;
            LessonNumTextBox.Text = a.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// making te LessonNumTextBox present the right lesson num for every car and gear types
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicenseTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                some = existTrainee.LicenseType.Find(x =>
                x.Gear.ToString() == GearComboBox.SelectedValue?.ToString() && x.VehicleType.ToString() == VehcileTypeComboBox.SelectedValue?.ToString());
            if (some != null)
                LessonNumTextBox.Text = some.LessonNum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void LessonNumTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var a = LessonNumTextBox.Text == null ? 0 : int.Parse(LessonNumTextBox.Text);
            some.LessonNum = a;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+");
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
                phoneNumbersTextBox.Clear();
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
                if (!bl.IsValidEmailAddress(EmailTextBox?.Text))
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

        private void DeleteTraineeButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveTrainee(this.idTextBox.Text);
                MessageBox.Show(this.firstNameTextBox.Text+" "+ this.lastNameTextBox.Text+" "+"הוסר/ה מהמערכת בהצלחה");
                Trainee trainee = new Trainee();
                this.DataContext = trainee;
                TraineeEdited?.Invoke(this, new EventArgs());
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
                existTrainee = this.DataContext as Trainee;
            bl.UpdateTrainee(existTrainee);
            MessageBox.Show(existTrainee.FirstName + " עודכן בהצלחה");
            TraineeEdited?.Invoke(this, new EventArgs());
         }
         catch (Exception exception)
         {
             MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
         }
        }



        private void TraineeeTestButtonClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.TraineeHaveMinLessons(existTrainee.ID))
                {
                   
                    Test_Registar_Window test_Registar_Window = new Test_Registar_Window(null, existTrainee);
                    test_Registar_Window.ShowDialog();
                    //this.Close();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void TraineeEditUserControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                existTrainee = this.DataContext as Trainee;
            }


            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    
}

