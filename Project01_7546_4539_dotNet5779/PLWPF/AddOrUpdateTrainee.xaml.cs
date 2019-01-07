using System;
using System.Windows;
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
                //MessageBox.Show(parent.ToString());
                if (parent.GetType().ToString() == "PLWPF.TraineeMenu_window")
                {
                    exist = true;
                    AddOrUpdateButtonClick.Content = "עדכן";
                }

                LessonNumScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(LessonNumScrollBar_OnValueChanged);
                LessonNumScrollBar.Minimum = 0;
                LessonNumScrollBar.Maximum = 100;
                LessonNumScrollBar.SmallChange = 1;
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


        private void LessonNumScrollBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LessonNumTextBox.Text = LessonNumScrollBar.Value.ToString();
        }

    }
}
