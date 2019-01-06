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
        IBL bl = Bl_imp.GetBl();
        private static string id;
        private static string password;
        private static Window pWindow;
        private static bool isTester;

        public LogInWindow(Window parent,bool flag)
        {
            InitializeComponent();
            isTester = flag;
            pWindow = parent;
            

        }
        

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = idTextBox.Text;
                //password = LogIn_ShowPassword.;
                // MessageBox.Show(id + "\n" + birth.ToString("d"));
                if (bl.CheckIdValidation(id))
                {
                    this.Hide();
                    pWindow.Hide();

                    if (isTester)
                    {
                        Tester testerDetailes = bl.GetTesterById(id);
                        if (testerDetailes != null)
                        {
                            //this.DataContext = testerDetailes;
                            TesterMenu testerMenu = new TesterMenu(pWindow, testerDetailes);
                            testerMenu.ShowDialog();
                            this.Close();
                        }

                        else
                        {
                            // if (pWindow==MainWindow.Trainee_Click)
                            AddOrUpdateTester addOrUpdateTester = new AddOrUpdateTester(pWindow, new Tester(id));
                            addOrUpdateTester.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        Trainee traineeDetailes = bl.GetTraineeById(id);
                        if (traineeDetailes != null)
                        {
                            //this.DataContext = traineeDetailes;
                            TraineeMenu_window traineeMenuWindow = new TraineeMenu_window(pWindow, traineeDetailes);
                            traineeMenuWindow.ShowDialog();
                            this.Close();
                        }

                        else
                        {
                            // if (pWindow==MainWindow.Trainee_Click)
                            AddOrUpdateTrainee addOrUpdateTrainee = new AddOrUpdateTrainee(pWindow, new Trainee(id));
                            addOrUpdateTrainee.ShowDialog();
                            this.Close();
                        }
                    }
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

                if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9) &&
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


        
    }

}
