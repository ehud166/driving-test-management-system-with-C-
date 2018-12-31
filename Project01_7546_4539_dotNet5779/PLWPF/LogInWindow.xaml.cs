using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL;
using BE;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            DataContext = this;
            InitializeComponent();
        }
        IBL bl = Bl_imp.GetBl();
        private string id;
        private DateTime birth;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                id = idTextBox.Text;
                Trainee a = bl.GetTraineeById(id);
                birth = birthdayDatePicker.DisplayDate;
                // MessageBox.Show(id + "\n" + birth.ToString("d"));
                if (id != null && String.Empty != id && bl.CheckIdValidation(id))
                {
                    if (a != null)
                    {
                        this.Hide();
                        TraneeMenu traneeMenu = new TraneeMenu(a);
                        traneeMenu.ShowDialog();
                    }
                    else
                    {
                        this.Hide();
                        AddOrUpdateTrainee addOrUpdateTrainee = new AddOrUpdateTrainee(id);
                        addOrUpdateTrainee.ShowDialog();
                        this.Close();
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,"Error");
            }
        }

        

        private void idTextBox_keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OkButton_Click(sender,e);
            }
            if ((e.Key< Key.D0|| e.Key > Key.D9)&& (e.Key < Key.NumPad0|| e.Key > Key.NumPad9)&& e.Key !=Key.Tab)//to handled
            {
                idTextBox.BorderBrush = Brushes.Red;
                e.Handled = true;

            }
            idTextBox.BorderBrush = Brushes.Gray;

        }

        private void LogInWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            idTextBox.Focus();
        }
    }

}
