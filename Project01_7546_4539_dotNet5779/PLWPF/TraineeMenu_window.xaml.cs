using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TraineeMenu_window.xaml
    /// </summary>
    public partial class TraineeMenu_window : Window
    {
        private static Trainee existTrainee = null;
        private static Window pWindow = null;

        public TraineeMenu_window(Window parent, Trainee trainee)
        {
            InitializeComponent();
            pWindow = parent;
            existTrainee = trainee;
            header_textBlock.Text = string.Format("שלום " + existTrainee.FirstName);
        }


        private void AddOrUpdate_ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddOrUpdateTrainee addOrUpdateTrainee = new AddOrUpdateTrainee(this,existTrainee);
            addOrUpdateTrainee.ShowDialog();
            //this.Close();
        }

        private void BackToMainWindow_clickButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Exit_clickButton(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
            //this.Close();
        }

        private void MyDetailes_ButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(existTrainee.ToString());
        }

        private void TraineeMenu_window_OnClosed(object sender, EventArgs e)
        {
            pWindow?.Show();
        }
    }
}
