using BE;
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
using static BE.Enums;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Test_Registration_Window.xaml
    /// </summary>
    public partial class Test_Registar_Window : Window
    {
        private static Window pWindow = null;
        private static Trainee existTrainee;
        private static IBL bl = Bl_imp.GetBl();
        private static List<Tester> myRelevantTesters = new List<Tester>();
        private static Test myTest = new Test();


        public Test_Registar_Window(Window parent, Trainee newTrainee)
        {
            InitializeComponent();
            pWindow = parent;
            existTrainee = newTrainee;
            this.DataContext = existTrainee;
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
                pWindow.Close();
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

        private void Test_Registar_Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            //VehcileTypeComboBox.Text =  existTrainee.LicenseType.FindAll(x => x?.VehicleType == Vehicle.maxTrailer ).ToString();
            //List<Vehicle> a = new List<Vehicle>();
            //a.Add(Vehicle.maxTrailer);
            //a.Add(Vehicle.midTrailer);
            //MessageBox.Show(VehcileTypeComboBox.SelectedIndex.ToString());
            //String s = (this.VehcileTypeComboBox.SelectedValue).ToString();



            if (existTrainee.LicenseType.Any(x => x?.VehicleType == Vehicle.maxTrailer && x.LessonNum >= 20))
            {
                VehicleTypeComboBox.Items.Add("משאית כבדה");
            }
            if (existTrainee.LicenseType.Any(x => x?.VehicleType == Vehicle.midTrailer && x.LessonNum >= 20))
            {
                VehicleTypeComboBox.Items.Add("משאית קלה");
            }
            if (existTrainee.LicenseType.Any(x => x?.VehicleType == Vehicle.privateCar && x.LessonNum >= 20))
            {
                VehicleTypeComboBox.Items.Add("רכב פרטי");
            }
            if (existTrainee.LicenseType.Any(x => x?.VehicleType == Vehicle.motorcycle && x.LessonNum >= 20))
            {
                VehicleTypeComboBox.Items.Add("אופנוע");
            }



        }

        private void VehicleTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            myTest.VehicleType = Hebrew2VT(VehicleTypeComboBox.SelectedValue.ToString());
            GearComboBox.Items.Clear();
            if (existTrainee.LicenseType.Any(x =>
                VTToHebrew(x.VehicleType) == VehicleTypeComboBox.SelectedValue.ToString() && x.LessonNum >= 20 && x.Gear == Gear.auto))
            {
                if (!GearComboBox.Items.Contains("אוטומט"))
                {
                    GearComboBox.Items.Add("אוטומט");
                    GearComboBox.IsEnabled = true;
                }
               
            }

            if (existTrainee.LicenseType.Any(x =>
               VTToHebrew(x.VehicleType) == VehicleTypeComboBox.SelectedValue.ToString() && x.LessonNum >= 20 && x.Gear == Gear.manual))
            {
                if (!GearComboBox.Items.Contains("ידני"))
                {
                    GearComboBox.Items.Add("ידני");
                    GearComboBox.IsEnabled = true;
                }
            }
        }


        private void setBlackOutDates(DateTime start, DateTime end)
        {
            testDatePicker.BlackoutDates.Clear();
            CalendarDateRange XForTheFirst2Days = new CalendarDateRange();
            XForTheFirst2Days.Start = DateTime.Today;
            XForTheFirst2Days.End = DateTime.Today.AddDays(1);
            testDatePicker.BlackoutDates.Add(XForTheFirst2Days);
            // myTest.TestAddress = new Address("tr", 45, "ff", 4);
            // myTest.VehicleType = Vehicle.maxTrailer;
            //testAdressTextBox.Text = existTrainee.Address.City;
            // myTest.TestAddress.City = testAdressTextBox.Text;
            DateTime i = start.AddDays(2);
            while (i <= end)
            {
                if (i.DayOfWeek == DayOfWeek.Friday || i.DayOfWeek == DayOfWeek.Saturday)
                    testDatePicker.BlackoutDates.Add(new CalendarDateRange(i));
                else
                {
                    myTest.TestDateAndTime = i;
                    myRelevantTesters = bl.RelevantTesters(myTest);
                    if (myRelevantTesters.Count() == 0)
                        testDatePicker.BlackoutDates.Add(new CalendarDateRange(i));
                }
                i = i.AddDays(1);
            }
        }

        private void DatePickerOpened(object sender, RoutedEventArgs e)
        {

            testDatePicker.SelectedDate=null;
            myTest.Gear = Hebrew2GT(GearComboBox.SelectedValue.ToString());
            testDatePicker.DisplayDateStart = DateTime.Today;
            testDatePicker.DisplayDateEnd = DateTime.Today.AddDays(32);
            setBlackOutDates(DateTime.Today, DateTime.Today.AddDays(32));
        }

        private void testDatePicker_selectedChanged(object sender, SelectionChangedEventArgs e)
        {
            myTest.TestDateAndTime = testDatePicker.SelectedDate.Value;
            myRelevantTesters = bl.RelevantTesters(myTest);
            bool[] hours = new bool[6];
            DateTime dateTime = new DateTime();
            dateTime = myTest.TestDateAndTime;
            testHourComboBox.Items.Clear();
            foreach (var item in myRelevantTesters)
            {
                for (int i = 0; i < 6; i++)
                {
                    if(bl.FreeTester(item, myTest.TestDateAndTime.AddHours(i + 9)))
                    {
                            hours[i] = true;
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                    if (hours[i] == true)
                    {
                        testHourComboBox.Items.Add(string.Format("{0}:00", i + 9));
                    }
            }
        }

       
      
        private void testAddressComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            myTest.TestAddress.City = testAddressComboBox.Text.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(testHourComboBox.SelectedIndex.ToString());
            //MessageBox.Show(testDatePicker.Text.ToString());
            
            DateTime newDateTime = DateTime.Parse(testDatePicker.Text).AddHours(testHourComboBox.SelectedIndex + 9);
            Test newTest = new Test(existTrainee.ID,newDateTime,myTest.TestAddress,myTest.VehicleType,myTest.Gear);
            Tester newTester = myRelevantTesters.Find(x => bl.FreeTester(x, newDateTime) == true);
            Address a = new Address(testAddressComboBox.Text,0,"",50);
            newTest.TestAddress = a;
            newTest.TesterId = newTester.ID;
            bl.AddTest(newTest);
            Test newTestAdded = bl.GetTestsList().Find(x => x.Equals(newTest));
            newTester.TesterTests.Add(newTestAdded);
            bl.UpdateTester(newTester);
            //MessageBox.Show(newTestAdded.ToString());
            //MessageBox.Show(bl.GetTesterById(newTestAdded.TesterId).TesterTests.Find(x=>x.Equals(newTest)).ToString());

        }
    }
}
