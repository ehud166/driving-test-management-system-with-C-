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
            InitializeComponent();
            bl = Bl_imp.GetBl();
            existTrainee = newTrainee;
            pWindow = parent;
            this.DataContext = existTrainee;
            idTextBox.IsEnabled = false;

            if (parent.GetType().ToString() == "PLWPF.TraineeMenu_window")
            {
                exist = true;
                AddOrUpdateButtonClick.Content = "עדכן";
                pWindow = new TraineeMenu_window(pWindow, existTrainee);
            }
        }

       

        private void AddOrUpdateButtonClick_OnClick(object sender, RoutedEventArgs e)
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
            }

            this.Close();
        }

        private void AddOrUpdateTrainee_OnClosed(object sender, EventArgs e)
        {
            pWindow?.ShowDialog();
        }
    }
}
