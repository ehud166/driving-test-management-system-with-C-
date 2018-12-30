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
    /// Interaction logic for TraneeMenu.xaml
    /// </summary>
    public partial class TraneeMenu : Window
    {
        private Trainee existTrainee;
        public TraneeMenu(Trainee a)
        {
            InitializeComponent();
            existTrainee = a;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateTrainee addOrUpdateTrainee = new AddOrUpdateTrainee(existTrainee);
            addOrUpdateTrainee.Show();
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
