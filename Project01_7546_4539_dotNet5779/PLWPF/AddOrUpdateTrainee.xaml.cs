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
using System.Windows.Shapes;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddOrUpdateTrainee.xaml
    /// </summary>
    public partial class AddOrUpdateTrainee : Window
    {

        public AddOrUpdateTrainee(string id)
        {
            InitializeComponent();
            IBL bl = Bl_imp.GetBl();


            idTextBox.Text = id;
            idTextBox.IsEnabled = false;
        }
        public AddOrUpdateTrainee(Trainee existTrainee)
        {
            InitializeComponent();
            IBL bl = Bl_imp.GetBl();


            idTextBox.Text = existTrainee.ID;
            idTextBox.IsEnabled = false;
            lastNameTextBox.Text = existTrainee.LastName;
            firstNameTextBox.Text = existTrainee.FirstName;
            birthdayDatePicker.Text = existTrainee.Birthday.ToString("yy-MM-dd");
            GenderComboBox.Text = existTrainee.Gender.ToString();
          //prefixPhoneComboBox.Text = existTrainee.Phone.Substring(0, existTrainee.Phone.Length - 7);
          //phoneNumbersTextBox.Text = existTrainee.Phone.Substring(existTrainee.Phone.Length - 7, existTrainee.Phone.Length);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
