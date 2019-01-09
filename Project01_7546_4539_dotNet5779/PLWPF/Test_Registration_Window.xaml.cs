using BE;
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
        public Test_Registar_Window(Window parent, Trainee newTrainee)
        {
            InitializeComponent();
            pWindow = parent;
            existTrainee = newTrainee;
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

        private void Exit_clickButton(object sender, RoutedEventArgs e)
        {
            this.Close();
            pWindow?.Close();
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

            setBlackOutDates(DateTime.Now, DateTime.Now.AddDays(32));


        }

        private void VehicleTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(VehicleTypeComboBox.SelectedValue.ToString());
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
            testDatePicker.DisplayDateStart = DateTime.Now;
            testDatePicker.DisplayDateEnd = DateTime.Now.AddDays(32);

            CalendarDateRange XForTheFirst2Days = new CalendarDateRange();
            XForTheFirst2Days.Start = DateTime.Now;
            XForTheFirst2Days.End = DateTime.Now.AddDays(1);
            testDatePicker.BlackoutDates.Add(XForTheFirst2Days);
            string a = existTrainee.Address.City;
            DateTime i = start;
            while (i <= end)
            {
                if (i.DayOfWeek == DayOfWeek.Friday || i.DayOfWeek == DayOfWeek.Saturday)
                    testDatePicker.BlackoutDates.Add(new CalendarDateRange(i));
                else
                {
                    //var v = myBL.Available_testers_by_day_nearby(i, a, Tools.ToCarType(car_typeComboBox.SelectedItem), Tools.ToGearType(gear_typeComboBox.SelectedItem));
                    //if (v.Count() == 0)
                    //    Test_datePicker.BlackoutDates.Add(new CalendarDateRange(i));
                }
                i = i.AddDays(1);
            }
        }
    }
}
