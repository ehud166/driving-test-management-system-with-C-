using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using BL;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddOrUpdateTrainee.xaml
    /// </summary>
    public partial class AddOrUpdateTrainee : Window
    {
        private static Trainee existTrainee = new Trainee();
        private static Window pWindow = null;
        LicenseType some = new LicenseType();
        static bool exist = false;
        private IBL bl;

        public AddOrUpdateTrainee(Window parent, Trainee newTrainee)
        {
            try
            {

                InitializeComponent();

                // trainee can't update this details, only manager can
                this.VehicleLabel.Visibility = Visibility.Hidden;
                this.VehcileTypeComboBox.Visibility = Visibility.Hidden;
                this.LessonNumLabel.Visibility = Visibility.Hidden;
                this.LessonNumStackPanel.Visibility = Visibility.Hidden;
                this.GearLabel.Visibility = Visibility.Hidden;
                this.GearComboBox.Visibility = Visibility.Hidden;



                bl = Bl_imp.GetBl();
                existTrainee = newTrainee;
                pWindow = parent;
                this.DataContext = existTrainee;
                idTextBox.IsEnabled = false;
                //MessageBox.Show(parent.ToString());
                if (parent.GetType().ToString() == "PLWPF.TraineeMenu_window")
                {
                    exist = true;
                    AddOrUpdateButtonClick.Content = "עדכן";
                }

                //LessonNumScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(LessonNumScrollBar_OnValueChanged);
                //LessonNumScrollBar.Minimum = 0;
                LessonNumScrollBar.Maximum = 100;
                LessonNumScrollBar.SmallChange = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void AddOrUpdateButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                some.LessonNum = int.Parse(LessonNumTextBox.Text);
                existTrainee.LicenseType.Find(x => x.VehicleType == some.VehicleType && x.Gear == some.Gear).LessonNum =
                    some.LessonNum;
                if (exist)
                {
                    bl.UpdateTrainee(existTrainee);
                    MessageBox.Show(existTrainee.FirstName + " עודכן בהצלחה");
                }
                else
                {
                    //LicenseType a = new LicenseType(Enums.Vehicle.motorcycle, Enums.Gear.manual, 30);
                    //existTrainee.LicenseType = new List<LicenseType>();
                    //existTrainee.LicenseType.Add(a);
                    //Address b = new Address("yu", 5, "gerusalem");
                    //existTrainee.Address = b;
                    bl.AddTrainee(existTrainee);
                    MessageBox.Show(existTrainee.FirstName + " נרשם בהצלחה");
                    pWindow = new TraineeMenu_window(pWindow, existTrainee);
                }
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddOrUpdateTrainee_OnClosed(object sender, EventArgs e)
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


        private void LessonNumScrollBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LessonNumTextBox.Text = LessonNumScrollBar.Value.ToString();
        }



        private void LicenseTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(GearComboBox.SelectedValue.ToString() + VehcileTypeComboBox.SelectedValue.ToString());
            some = existTrainee.LicenseType.Find(x =>
                 x.Gear.ToString() == GearComboBox.SelectedValue?.ToString() && x.VehicleType.ToString() == VehcileTypeComboBox.SelectedValue?.ToString());
            LessonNumTextBox.Text = some?.LessonNum.ToString();


        }


        private void ValidationPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password != validationPasswordBox.Password)
            {
                MessageBox.Show("הסיסמאות אינן תואמות אחת לשניה, נסה שוב");
            }
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
    }
}
