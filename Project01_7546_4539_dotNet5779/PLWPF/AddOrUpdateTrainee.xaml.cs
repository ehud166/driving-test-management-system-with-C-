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
        private static Window pWindow = null;
        static  bool exist = false;
        private IBL bl;

        public AddOrUpdateTrainee(Window parent, Trainee newTrainee)
        {
            try
            {

                InitializeComponent();
                bl = Bl_imp.GetBl();
                existTrainee = newTrainee;
                pWindow = parent;
                this.DataContext = existTrainee;
                idTextBox.IsEnabled = false;
                MessageBox.Show(parent.ToString());
                if (parent.GetType().ToString() == "PLWPF.TraineeMenu_window")
                {
                    exist = true;
                    AddOrUpdateButtonClick.Content = "עדכן";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

       

        private void AddOrUpdateButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (exist)
                {
                    bl.UpdateTrainee(existTrainee);
                    MessageBox.Show(existTrainee.FirstName + " עודכן בהצלחה");
                }
                else
                {
                    bl.AddTrainee(existTrainee);
                    MessageBox.Show(existTrainee.FirstName + " נרשם בהצלחה");
                    pWindow = new TraineeMenu_window(pWindow, existTrainee);
                }
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
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
                MessageBox.Show(exception.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
