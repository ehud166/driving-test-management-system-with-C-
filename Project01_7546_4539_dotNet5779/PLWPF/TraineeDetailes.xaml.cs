using BL;
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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TraineeDetailes.xaml
    /// </summary>
    public partial class TraineeDetailes : Window
    {
        private static Trainee existTrainee;
        private static Window pWindow = null;
       // static bool exist = false;
        private IBL bl;


        public TraineeDetailes(Window parent,Trainee newTrainee)
        {
            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                existTrainee = newTrainee;
                pWindow = parent;
                this.DataContext = existTrainee;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void MyTests_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DataContext = bl.GetTestsById(existTrainee.ID);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
