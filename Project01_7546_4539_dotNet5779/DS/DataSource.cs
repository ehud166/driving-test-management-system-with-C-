using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    static public class DataSource
    {
        public static List<Test> Tests { get; private set; } = new List<Test>();
        public static List<Tester> Testers { get; private set; } = new List<Tester>();
        public static List<Trainee> Trainees { get; private set; } = new List<Trainee>();


        //Person human = new Person("314784539", "ehud", "gershony", DateTime.Parse("30/07/1956"), Gender.male, "0530010100", new Address("kolombia", 6, "jerusalem"), Vehicle.motorcycle);
        //Console.WriteLine(human);
        //Tester checkForTester = new Tester(human.ID, human.FirstName, human.LastName, human.Birthday, human.Gender, human.Phone, human.Address, human.VehicleType, 11, 25, 100);
        //Testers.Add(checkForTester);
        //Trainee checkForTrainee = new Trainee("", "yishay", "badichi", DateTime.Parse("30/07/1996"), Gender.male, "053823117", new Address("kolombia", 7, "jerusalem"), Vehicle.privateCar, Gear.manual, "or-yarok", checkForTester.FirstName + "\n" + checkForTester.LastName, 21);
        //Trainees.Add(checkForTrainee);
       
    }
    
}
