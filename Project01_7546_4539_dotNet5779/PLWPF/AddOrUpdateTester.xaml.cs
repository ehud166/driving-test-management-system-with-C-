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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddOrUpdateTester.xaml
    /// </summary>
    public partial class AddOrUpdateTester : Window
    {
        private static Tester existTester;
        private static Window pWindow = null;
        static bool exist = false;
        private IBL bl;

        public AddOrUpdateTester(Window parent, Tester newTester)
        {
            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                existTester = newTester;
                pWindow = parent;
                this.DataContext = existTester;
                idTextBox.IsEnabled = false;
                SeniorityScrollBar.Maximum = 50;
                SeniorityScrollBar.SmallChange = 1;

                if (parent.GetType().ToString() == "PLWPF.TesterMenu")
                {
                    mySchedule.Build = existTester.Schedule;
                    exist = true;
                    AddOrUpdateButtonClick.Content = "עדכן";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                existTester.Schedule = mySchedule.Build;
                if (exist)
                {
                    bl.UpdateTester(existTester);
                    MessageBox.Show(existTester.FirstName + " עודכן בהצלחה");
                }
                else
                {
                    bl.AddTester(existTester);
                    MessageBox.Show(existTester.FirstName + " נרשם בהצלחה");
                    pWindow = new TesterMenu(pWindow, existTester);
                }

                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


            }
        }

       


        private void AddOrUpdateTester_OnClosing(object sender, CancelEventArgs e)
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

        private void SeniorityScrollBar_OnValueChangedScrollBar(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SeniorityTextBox.Text = SeniorityScrollBar.Value.ToString();
            SeniorityScrollBar.Minimum = double.Parse(SeniorityTextBox.Text);

        }
    }
    
}
