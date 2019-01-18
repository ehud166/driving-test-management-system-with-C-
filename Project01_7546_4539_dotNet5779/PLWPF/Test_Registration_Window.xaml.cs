using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static BE.Configuration;
using static BE.Enums;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Test_Registration_Window.xaml
    /// </summary>
    public partial class Test_Registar_Window : Window
    {
        private static Window pWindow;
        private static Trainee existTrainee;
        private static IBL bl = Bl_imp.GetBl();
        private static List<Tester> myRelevantTesters = new List<Tester>();
        private static Test myTest = new Test();


        public Test_Registar_Window(Window parent, Trainee newTrainee)
        {
            existTrainee = new Trainee();
            bl = Bl_imp.GetBl();
            myRelevantTesters = new List<Tester>();
            myTest = new Test();
            InitializeComponent();
            pWindow = parent;
            existTrainee = newTrainee;
            this.DataContext = existTrainee;
            myTest.TraineeId = newTrainee.ID;
            myTest.TestAddress.StreetName = existTrainee.Address.StreetName;
            VehicleTypeComboBox.Items.Clear();
        }


        private void Test_Registar_Window_OnClosed(object sender, EventArgs e)
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
        private void BackToMainWindow_clickButton(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_clickButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void VehicleTypeComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            try
            {
                VehicleTypeComboBox.Items.Clear();
            bool[] vehicleAvailable = new bool[4] { false, false, false, false };
            foreach (var licenseType in existTrainee.LicenseType)
            {

                if (licenseType.LessonNum >= MinLessons && bl.TraineeConditionsForTest(existTrainee.ID, licenseType.VehicleType, licenseType.Gear))
                {
                    vehicleAvailable[(int)licenseType.VehicleType] = true;
                }
            }
            for (int i = 0; i < vehicleAvailable.Length; i++)
            {
                if (vehicleAvailable[i])
                {
                    VehicleTypeComboBox.Items.Add(VT2Hebrew((Vehicle)i));
                }
            }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void VehicleTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bool[] gearAvailable = new bool[2] { false, false};
            if (Hebrew2VT(VehicleTypeComboBox.SelectedValue.ToString()) !=null)
            {
                myTest.VehicleType = Hebrew2VT(VehicleTypeComboBox.SelectedValue.ToString());
            }
            GearComboBox.Items.Clear();
            foreach (var licenseType in existTrainee.LicenseType)
            {
                if (licenseType.LessonNum >= MinLessons && licenseType.VehicleType == myTest.VehicleType && bl.TraineeConditionsForTest(existTrainee.ID, licenseType.VehicleType, licenseType.Gear))
                {
                    gearAvailable[(int)licenseType.Gear] = true;
                }
            }
            for (int i = 0; i < gearAvailable.Length; i++)
            {
                if (gearAvailable[i])
                {
                    GearComboBox.Items.Add(Gear2Hebrew((Gear)i));
                    GearComboBox.IsEnabled = true;
                }
            }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void setBlackOutDates(DateTime start, DateTime end)
        {
            try
            {
                testDatePicker.BlackoutDates.Clear();
            CalendarDateRange XForTheFirst2Days = new CalendarDateRange();
            XForTheFirst2Days.Start = DateTime.Today;
            XForTheFirst2Days.End = DateTime.Today.AddDays(1);
            testDatePicker.BlackoutDates.Add(XForTheFirst2Days);
            
            DateTime i = start.AddDays(2);
            while (i <= end)
            {
                if (i.DayOfWeek == DayOfWeek.Friday || i.DayOfWeek == DayOfWeek.Saturday)
                    testDatePicker.BlackoutDates.Add(new CalendarDateRange(i));
                else
                {
                    myTest.TestDateAndTime = i;
                    if (bl.NotExistTestInMinDaysBetweenTestsDays(myTest))//check every iteration maybe on the next day the trainee will fill the condition
                    {
                        myRelevantTesters = bl.RelevantTesters(myTest);
                        Thread mapQuestThread = new Thread(() =>
                        {
                            int ERRORS = 0;
                            while (ERRORS++<=3)//3 trying
                            {
                                try
                                {
                                    myRelevantTesters = bl.GetListOfTestersAtTraineeArea(myRelevantTesters,
                                        myTest.TestAddress.StreetName);
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message);
                                    Thread.Sleep(2000); //Sleep for 2 sec
                                }
                                Action act = () =>
                                {
                                    if (myRelevantTesters.Count() == 0)
                                        testDatePicker.BlackoutDates.Add(new CalendarDateRange(i));
                                };
                                Dispatcher.BeginInvoke(act);
                            }
                        });
                        //mapQuestThread.Start();
                    }
                }
                i = i.AddDays(1);
            }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DatePickerOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                myTest.TestAddress.StreetName = testAddressComboBox.Text.ToString();
                testDatePicker.SelectedDate=null;
                myTest.Gear = Hebrew2GT(GearComboBox.SelectedValue.ToString());
                testDatePicker.DisplayDateStart = DateTime.Today;
                testDatePicker.DisplayDateEnd = DateTime.Today.AddDays(32);
                setBlackOutDates(DateTime.Today, DateTime.Today.AddDays(32));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void testDatePicker_selectedChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                myTest.TestDateAndTime = testDatePicker.SelectedDate.Value;
            myRelevantTesters = bl.RelevantTesters(myTest);
            testHourComboBox.IsEnabled = true;
            bool[] hours = new bool[WorkHours];
            DateTime dateTime = new DateTime();
            dateTime = myTest.TestDateAndTime;
            testHourComboBox.Items.Clear();
            foreach (var item in myRelevantTesters)
            {
                for (int i = 0; i < WorkHours; i++)
                {
                    if (bl.FreeTester(item, myTest.TestDateAndTime.AddHours(i + 9)))
                    {
                        hours[i] = true;
                    }
                }
            } //after filter the relevant tester we are filter the relevant hours

            for (int i = 0; i < WorkHours; i++)
            {
                if (hours[i] == true)
                {
                    testHourComboBox.Items.Add(string.Format("{0}:00", i + StartHour));
                }
            }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            } 

        }



        private void testAddressComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                myTest.TestAddress.StreetName = testAddressComboBox.Text.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DateTime newDateTime = DateTime.Parse(testDatePicker.Text).AddHours(testHourComboBox.SelectedIndex + 9);
            Test newTest = new Test(existTrainee.ID,newDateTime,myTest.TestAddress,myTest.VehicleType,myTest.Gear);
            Tester newTester = myRelevantTesters.Find(x => bl.FreeTester(x, newDateTime) == true);
            Address a = new Address(testAddressComboBox.Text);
            newTest.TestAddress = a;
            newTest.TesterId = newTester.ID;
            newTest.TestComment = "!!!בהצלחה";
            bl.AddTest(newTest);
            Test newTestAdded = bl.GetTestsList().Find(x => x.Equals(newTest));
            newTester.TesterTests.Add(newTestAdded);
            bl.UpdateTester(newTester);

            //MessageBox.Show(newTestAdded.ToString());
            //MessageBox.Show(bl.GetTesterById(newTestAdded.TesterId).TesterTests.Find(x=>x.Equals(newTest)).ToString());
            this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        
    }
}
