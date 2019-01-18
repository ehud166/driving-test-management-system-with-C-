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
using System.Text.RegularExpressions;
using BL;
using BE;
using static BE.Enums;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestEditUserControl.xaml
    /// </summary>
    public partial class TestEditUserControl : UserControl
    {
        private IBL bl;
        private Test existTest = new Test();
        public event EventHandler<EventArgs> TestEdited;
        
        public TestEditUserControl()
        {
            

            try
            {
                InitializeComponent();
                bl = Bl_imp.GetBl();
                this.DataContext = existTest;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// test results toggleButton event, if checked so the trainee succseed in this parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = sender as ToggleButton;
                x.Content = "עבר";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = sender as ToggleButton;
            x.Content = "נכשל";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTest_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //existTest = this.DataContext as Test;

                #region test update
                existTest.TestDistance = string2result(testDistance.Content.ToString());
                existTest.TestMerge = string2result(testMerge.Content.ToString());
                existTest.TestMirrors = string2result(testMirrors.Content.ToString());
                existTest.TestReverseParking = string2result(testReverseParking.Content.ToString());
                existTest.TestVinker = string2result(testVinker.Content.ToString());
                existTest.TestResult = string2result(testResult.Content.ToString());
                #endregion

                MessageBox.Show("הציון עודכן בהצלחה");
                bl.UpdateTest(existTest);
                TestEdited?.Invoke(this, new EventArgs()); //if update test so we need to tell the hosting window to refresh his tests list
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        /// <summary>
        /// every click on test results make them change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TestEditUserControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                existTest = this.DataContext as Test;
                testDistance.IsChecked = Result2Bool(existTest.TestDistance); //set the test togglebutton results by selected test results
                testMerge.IsChecked = Result2Bool(existTest.TestMerge);
                testMirrors.IsChecked = Result2Bool(existTest.TestMirrors);
                testReverseParking.IsChecked = Result2Bool(existTest.TestReverseParking);
                testVinker.IsChecked = Result2Bool(existTest.TestVinker);
                testResult.IsChecked = Result2Bool(existTest.TestResult);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }
    }
}
