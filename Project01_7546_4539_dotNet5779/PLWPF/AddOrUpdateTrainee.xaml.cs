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
        private static Trainee existTrainee;
        static  bool flag = false;
        private IBL bl;

        public AddOrUpdateTrainee(string id)
        {
            InitializeComponent();
            bl = Bl_imp.GetBl();
            idTextBox.Text = id;
            idTextBox.IsEnabled = false;
            //existTrainee = new Trainee(id);
            //this.DataContext = existTrainee;
        }


        public AddOrUpdateTrainee(Trainee newTrainee)
        {
            InitializeComponent();
            bl = Bl_imp.GetBl();
            existTrainee = newTrainee;
            flag = true;


            this.DataContext = existTrainee;
            AddOrUpdateButtonClick.Content = "עדכן";
            idTextBox.IsEnabled = false;

         
            //prefixPhoneComboBox.SelectedItem = existTrainee.Phone.Substring(0, existTrainee.Phone.Length - 7);
            //phoneNumbersTextBox.Text = existTrainee.Phone.Substring(existTrainee.Phone.Length - 7, existTrainee.Phone.Length);
        }

       

        private void AddOrUpdateButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                bl.UpdateTrainee(existTrainee);
            }
            else
            {
                bl.AddTrainee(existTrainee);
            }

            this.Close();
            MessageBox.Show(existTrainee.ToString());
        }

    }
}
