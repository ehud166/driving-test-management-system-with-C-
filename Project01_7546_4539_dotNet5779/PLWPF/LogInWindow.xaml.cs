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
        IBL bl = Bl_imp.GetBl();
        private static string id;
        private static Window pWindow;
        private static string user;

        public LogInWindow(Window parent,string  s)
        {
            InitializeComponent();
            user = s;
            pWindow = parent;
            

        }
        

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                id = idTextBox.Text;
                if (bl.CheckIdValidation(id)) //if id is correct
                {
                    this.Hide();
                    pWindow.Hide();
                    switch (user) //checks who is the user is the user 
                    {
                        case "manager":
                            Manager managerDetails = bl.GetManagerById(id);
                            if (managerDetails != null && PasswordBox.Password == managerDetails.Password)
                            {
                                ManagerControler_window managerControler_window = new ManagerControler_window(pWindow);
                                managerControler_window.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("!!!אין לך הרשאת מנהל");
                                pWindow?.Show();
                                this.Close();
                            }
                            break;
                        case "tester":
                            Tester testerDetailes = bl.GetTesterById(id);

                            if (testerDetailes != null /*&& testerDetailes.Password == PasswordBox.Password*/)
                            {

                                TesterMenu testerMenu = new TesterMenu(pWindow, testerDetailes);
                                testerMenu.ShowDialog();
                            }

                            else
                            {
                                // if (pWindow==MainWindow.Trainee_Click)
                                AddOrUpdateTester addOrUpdateTester = new AddOrUpdateTester(pWindow, new Tester(id));
                                addOrUpdateTester.ShowDialog();
                            }
                            break;
                        case "trainee":
                            Trainee traineeDetailes = bl.GetTraineeById(id);
                            if (traineeDetailes != null)
                            {
                                //this.DataContext = traineeDetailes;
                                TraineeMenu_window traineeMenuWindow = new TraineeMenu_window(pWindow, traineeDetailes);
                                traineeMenuWindow.ShowDialog();
                            }
                            else
                            {
                                // if (pWindow==MainWindow.Trainee_Click)
                                AddOrUpdateTrainee addOrUpdateTrainee = new AddOrUpdateTrainee(pWindow, new Trainee(id));
                                addOrUpdateTrainee.ShowDialog();
                            }
                            break;
                        default:
                            break;
                    }
                    this.Close();

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        

        private void idTextBox_keyDown(object sender, KeyEventArgs e)
        {
            try
            {


                if (e.Key == Key.Enter)
                {
                    OkButton_Click(sender, e);
                }

                if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9) &&  //not numbers
                    e.Key != Key.Tab) //to handled
                {
                    idTextBox.BorderBrush = Brushes.Red;
                    e.Handled = true;

                }

                idTextBox.BorderBrush = Brushes.Gray;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void LogInWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                idTextBox.Focus();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                if (idTextBox.Text.Length == 9)
            {
                if (user == "manager" || (user == "trainee" && bl.GetTraineeById(idTextBox.Text) != null))
                {
                    PasswordBox.Visibility = Visibility.Visible;
                    passwordLabel.Visibility = Visibility.Visible;
                }
                if (user == "manager" || (user == "tester" && bl.GetTesterById(idTextBox.Text) != null))
                {
                    PasswordBox.Visibility = Visibility.Visible;
                    passwordLabel.Visibility = Visibility.Visible;
                }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
          
    }

}
