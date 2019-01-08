using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ScheduleUserControl.xaml
    /// </summary>
    public partial class ScheduleUserControl : UserControl
    {
        public Tester existTester { get; set; }
        public Schedule Build;



        public ScheduleUserControl()
        {
            existTester = this.DataContext as BE.Tester;  
            try
            {

                InitializeComponent();
                Build = new Schedule();
                this.DataContext = existTester;
                for (var i = 0; i < ScheduleToggenGrid.Children.Count; i++)
                {
                    var s = ScheduleToggenGrid.Children[i] as ToggleButton;
                    s.IsChecked = Build[(DayOfWeek)Grid.GetColumn(s)][9 + Grid.GetRow(s)];
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScheduleToggenGrid_Checked(object sender, RoutedEventArgs e)
        {

            ToggleButton btn = sender as ToggleButton;
            int x = (int)btn.GetValue(Grid.RowProperty) + 9;
            int y = 4 - (int)btn.GetValue(Grid.ColumnProperty);
            Build[(DayOfWeek)y][x] = false;
          //  MessageBox.Show("row" + x.ToString() + "column" + y.ToString()+"checked");
          //  MessageBox.Show(Build[(DayOfWeek)y][x].ToString());
            
        }

        private void ScheduleToggenGrid_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;
            int x = (int)btn.GetValue(Grid.RowProperty)+9;
            int y = 5-(int)btn.GetValue(Grid.ColumnProperty);
          //  MessageBox.Show("row" + x.ToString() + "column" + y.ToString()+ "unchecked");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 6; j++)
            //    {
            //        if (Build[(DayOfWeek)i][j])
            //        {
            //            this.SetValue
            //        }
            //    }
            //}
        }
    }
}
