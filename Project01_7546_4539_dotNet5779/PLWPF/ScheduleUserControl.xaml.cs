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
        private Schedule _build;

        public Tester existTester { get; set; }
        public Schedule Build
        {
            get => _build;

            set
            {
                _build = value;
                for (var i = 0; i < ScheduleToggenGrid.Children.Count; i++)
                {
                    var s = ScheduleToggenGrid.Children[i] as ToggleButton;
                    //MessageBox.Show("row" + s.ToString() + "column" + s.ToString() + "checked");
                    s.IsChecked = _build[(DayOfWeek)(4 - Grid.GetColumn(s))][9 + Grid.GetRow(s)];
                }
            }
        }



        public ScheduleUserControl()
        {
            try
            {

                InitializeComponent();
               

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScheduleToggenGrid_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                ToggleButton btn = sender as ToggleButton;
            int x = (int)btn.GetValue(Grid.RowProperty) + 9;
            int y = 4 - (int)btn.GetValue(Grid.ColumnProperty);
            Build[(DayOfWeek)y][x] = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ScheduleToggenGrid_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton btn = sender as ToggleButton;
            int x = (int)btn.GetValue(Grid.RowProperty) + 9;
            int y = 4 - (int)btn.GetValue(Grid.ColumnProperty);
            Build[(DayOfWeek)y][x] = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
      
        }


    }
}
