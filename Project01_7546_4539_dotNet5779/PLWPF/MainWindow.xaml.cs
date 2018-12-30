using BL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IBL bl = Bl_imp.GetBl();
            #region help to insert




            Tester tester1 = new Tester("314784539", "ehud", "gershony", DateTime.Parse("13/02/1970"), Gender.male, "0530010199", new Address("shakhal", 8, "jerusalem"), Vehicle.privateCar, 13, 30, 100);
            //Console.WriteLine(tester1);
            bl.AddTester(tester1);
            Tester tester2 = new Tester("000002121", "dudu", "cohen", DateTime.Parse("30/07/1956"), Gender.male, "0530010100", new Address("kolombia", 6, "jerusalem"), Vehicle.privateCar, 11, 30, 100);
            //Console.WriteLine(tester1);
            bl.AddTester(tester2);

            Trainee trainee1 = new Trainee("032577546", "yishay", "badichi", DateTime.Parse("30/07/1996"), Gender.male, "053823117", new Address("kolombia", 7, "jerusalem"), Vehicle.privateCar, Gear.manual, "or-yarok", tester1.FirstName + " " + tester1.LastName, 50);
            bl.AddTrainee(trainee1);
            //Console.WriteLine(trainee1);
            Trainee trainee2 = new Trainee("206026858", "hadas", "gershony", DateTime.Parse("17/03/1996"), Gender.male, "053823117", new Address("kolombia", 7, "jerusalem"), Vehicle.privateCar, Gear.manual, "or-yarok", tester1.FirstName + " " + tester1.LastName, 50);
            bl.AddTrainee(trainee2);

            Test checkTest = new Test("032577546", DateTime.Parse("23/12/18 9:0"), new Address("f", 4, "a"), Vehicle.privateCar, Gear.manual);
            bl.AddTest(checkTest);

            checkTest.TestDistance = false;
            checkTest.TestReverseParking = false;
            checkTest.TestMirrors = true;
            checkTest.TestVinker = true;
            checkTest.TestMerge = true;
            checkTest.TestResult = false;
            bl.UpdateTest(checkTest);
            checkTest = new Test("206026858", DateTime.Parse("23/12/18 10:0"), new Address("f", 4, "a"), Vehicle.privateCar, Gear.manual);
            bl.AddTest(checkTest);
            checkTest.TestDistance = false;
            checkTest.TestReverseParking = false;
            checkTest.TestMirrors = false;
            checkTest.TestVinker = true;
            checkTest.TestMerge = true;
            checkTest.TestResult = false;
            checkTest.TestComment = " ";
            bl.UpdateTest(checkTest);
            #endregion
            InitializeComponent();
        }
        
        private void Trainee_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            new LogInWindow().Show();
            this.Close();
            
        }

 
    }
}
