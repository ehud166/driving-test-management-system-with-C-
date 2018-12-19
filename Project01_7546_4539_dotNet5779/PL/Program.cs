using System;
using System.ComponentModel;
using System.Linq;
using BE;
using BL;

namespace PL
{
    public class Program
    {

        static void Main(string[] args)
        {
           

            IBL bl = Bl_imp.GetBl();


            Person human = new Person("314784539", "ehud", "gershony", DateTime.Parse("30/07/1956"), Gender.male, "0530010100", new Address("kolombia", 6, "jerusalem"), Vehicle.motorcycle);
            Tester checkForTester = new Tester(human.ID, human.FirstName, human.LastName, human.Birthday, human.Gender, human.Phone, human.Address, human.VehicleType, 11, 25, 100);
            bl.AddTester(checkForTester);
            Trainee checkForTrainee = new Trainee("032577546", "yishay", "badichi", DateTime.Parse("30/07/1996"), Gender.male, "053823117", new Address("kolombia", 7, "jerusalem"), Vehicle.privateCar, Gear.manual, "or-yarok", checkForTester.FirstName + "\n" + checkForTester.LastName,50);
            bl.AddTrainee(checkForTrainee);
            checkForTrainee = new Trainee("206026858", "hadas", "gershony", DateTime.Parse("30/10/1996"), Gender.male, "053823117", new Address("kolombia", 7, "jerusalem"), Vehicle.privateCar, Gear.manual, "or-yarok", checkForTester.FirstName + "\n" + checkForTester.LastName, 50);
            bl.AddTrainee(checkForTrainee);

            Test checkTest = new Test("032577546", DateTime.Parse("19/12/18 9:0"), new Address("f", 4, "a"), Vehicle.privateCar, Gear.manual);
            bl.AddTest(checkTest);
            checkTest.TestDistance = true;
            checkTest.TestReverseParking = true;
            checkTest.TestMirrors = true;
            checkTest.TestVinker = true;
            checkTest.TestMerge = true;
            checkTest.TestResult = true;
            bl.UpdateTest(checkTest);
            checkTest = new Test("206026858", DateTime.Parse("19/12/18 9:0"), new Address("f", 4, "a"), Vehicle.privateCar, Gear.manual);
            bl.AddTest(checkTest);
            bl.AddTest(checkTest);
            checkTest.TestDistance = true;
            checkTest.TestReverseParking = true;
            checkTest.TestMirrors = true;
            checkTest.TestVinker = true;
            checkTest.TestMerge = true;
            checkTest.TestResult = true;
            checkTest.TestComment = " ";
            bl.UpdateTest(checkTest);


            //Console.WriteLine("some person");
            //Console.WriteLine(human);
            //Console.WriteLine("\ndetails for tester");
            //Console.WriteLine(checkForTester);
            //Console.WriteLine("\ndetails for trainee");
            //Console.WriteLine(checkForTrainee);
            Console.WriteLine("\nwelcome to computer data bank of transport ministery");
            string id, lName, fName, birthday, gender, phone, drivingSchool, teacherName;
            int lessonNum, seniority, maxTestsForWeek, maxDistance;
            string streetName, city, traineeId;
            Vehicle vehicleType;
            int building;
            Address address;
            Gear gear;
            DateTime testDate;
            do
            {
                Console.WriteLine("1:administrator\n" +
                                  "2: tester\n" +
                                  "3: trainee\n");
                User user = (User)Convert.ToInt32(Console.ReadLine());
                switch (user)
                {
                    case User.administrator:
                        Console.WriteLine("print:\n   1:all tests list" +
                                          "\n   2:all testers list" +
                                          "\n   3:all trainees list" +
                                          "\n   4:list for tests in specific day" +
                                          "\n   5:list for passed trainees" +
                                          "\n   6:list for tests in specific vehicle" +
                                          "\n   7:list for tests in specific tester" +
                                          "\n   8:list for tests in specific trainee\n" +
                                          "\n   9:group by license\n" +
                                          "\n   8:list for tests in specific trainee\n" +
                                          "\n   8:list for tests in specific trainee\n");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                               




                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                            case 7:
                                break;
                            case 8:
                                break;
                            case 9:
                                Console.WriteLine("print by vehicles\n");
                                foreach (var courentKey in bl.GroupTestersByVehicle())
                                {
                                    Console.WriteLine("key: {0}\n", courentKey.Key);
                                    foreach (var i in courentKey)
                                    {
                                        Console.WriteLine("{0}\n", i);
                                    }
                                }
                                break;
                            case 10:
                                Console.WriteLine("print Trainees by school");
                                foreach (var courentKey in bl.GroupTraineesBySchool())
                                {
                                    Console.WriteLine("key: {0}\n", courentKey.Key);
                                    foreach (var i in courentKey)
                                    {
                                        Console.WriteLine("\t{0}\n", i);
                                    }
                                }
                                break;
                            case 11:
                                Console.WriteLine("print tests by result");
                                foreach (var courentKey in bl.GroupTestByResult())
                                {
                                    Console.WriteLine("key: {0}\n", courentKey.Key);
                                    foreach (var i in courentKey)
                                    {
                                        Console.WriteLine("\t{0}\n", i);
                                    }
                                }
                                break;
                            default:
                                break;
                           
                        }

                        break;
                    case User.tester:
                        Console.WriteLine("1: new tester\n" +
                                          "2: exist tester\n");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                Console.WriteLine("put the next fields:\n" +
                                                  "   ID,first name, last name, birthday(with the form: DD/MM/YYYY, gender, phone number, address, vehicle, seniority, maxTestsForWeek, maxDistance");
                                //(string id, string lastName, string firstName, DateTime birthday, Gender gender, string phone, Address address,
                                //    Vehicle vehicleType, int seniority, int maxTestsForWeek, double maxDistance)
                                while (true)
                                {
                                    try
                                    {
                                        #region InputTester

                                        id = Console.ReadLine();
                                        fName = Console.ReadLine();
                                        lName = Console.ReadLine();
                                        birthday = Console.ReadLine();
                                        gender = Console.ReadLine();
                                        phone = Console.ReadLine();
                                        //string street_name, int building_number, string city
                                        Console.WriteLine("street name, building, city\n");
                                        streetName = Console.ReadLine();
                                        building = Convert.ToInt32(Console.ReadLine());
                                        city = Console.ReadLine();
                                        address = new Address(streetName, building, city);
                                        Console.WriteLine(
                                            "1: privateCar ,2: motorcycle,3: midTrailer, 4: maxTrailer\n");
                                        vehicleType = (Vehicle) (Convert.ToInt32(Console.ReadLine()));
                                        seniority = Convert.ToInt32(Console.ReadLine());
                                        maxTestsForWeek = Convert.ToInt32(Console.ReadLine());
                                        maxDistance = Convert.ToInt32(Console.ReadLine());

                                        #endregion
                                        
                                        break;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("invalid input\n {0}", e.Message);
                                    }
                                }
                                //tester c-tor 
                                Tester newTester = new Tester(id, lName, fName, DateTime.Parse(birthday), (Gender)(gender == "male" ? 0 : 1), phone, address, vehicleType, seniority, maxTestsForWeek, maxDistance);
                                bl.AddTester(newTester);
                                break;
                            case 2:
                                Console.WriteLine("1:update test\n" +
                                                  "2:quit from this job\n");
                                switch (Convert.ToInt32(Console.ReadLine()))
                                {
                                    case 1:
                                        Console.WriteLine("put the test ID\n");
                                        string testId = Console.ReadLine();

                                        #region InputResultTest

                                        Test tempTest = bl.GetTestsList().Find(item => item.ID == testId);
                                        Console.WriteLine("put v/x: distance, reverse, mirrors,vinker, merge, result,comment\n");
                                        bool distance, reverse, mirrors, vinker, merge, result;
                                        string comment;
                                        distance = Convert.ToBoolean(Console.ReadLine());
                                        reverse = Convert.ToBoolean(Console.ReadLine());
                                        mirrors = Convert.ToBoolean(Console.ReadLine());
                                        vinker = Convert.ToBoolean(Console.ReadLine());
                                        merge = Convert.ToBoolean(Console.ReadLine());
                                        result = Convert.ToBoolean(Console.ReadLine());
                                        comment = Console.ReadLine();

                                        tempTest.TestDistance = distance;
                                        tempTest.TestReverseParking = reverse;
                                        tempTest.TestMirrors = mirrors;
                                        tempTest.TestVinker = vinker;
                                        tempTest.TestMerge = merge;
                                        tempTest.TestResult = result;
                                        tempTest.TestComment = comment;

                                        #endregion
                                        //fill all the results detailes and now send to update
                                        bl.UpdateTest(tempTest);
                                        break;
                                    case 2:
                                        Console.WriteLine("sorry for your leaving\n");
                                        Console.WriteLine("put your ID\n");
                                        string testerId = Console.ReadLine();
                                        //send to remove test
                                        bl.RemoveTester(testerId);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }

                        break;
                    case User.trainee:
                        Console.WriteLine("1: new trainee\n" +
                                          "2: exist trainee\n");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("put the next fields:\n" +
                                                          "   ID,first name, last name, birthday(with the form: DD/MM/YYYY, gender, phone number, address, vehicleType, gear, drivingSchool, teacherName, lessonNum");

                                        #region InputTrainee

                                        id = Console.ReadLine();
                                        lName = Console.ReadLine();
                                        fName = Console.ReadLine();
                                        birthday = Console.ReadLine();
                                        gender = Console.ReadLine();
                                        phone = Console.ReadLine();
                                        //string street_name, int building_number, string city
                                        Console.WriteLine("street name, building, city\n");
                                        streetName = Console.ReadLine();
                                        building = Convert.ToInt32(Console.ReadLine());
                                        city = Console.ReadLine();
                                        address = new Address(streetName, building, city);
                                        Console.WriteLine(
                                            "1: privateCar ,2: motorcycle,3: midTrailer, 4: maxTrailer\n");
                                        vehicleType = (Vehicle)(Convert.ToInt32(Console.ReadLine()));
                                        gear = (Gear)(Convert.ToInt32(Console.ReadLine()));
                                        drivingSchool = Console.ReadLine();
                                        teacherName = Console.ReadLine();
                                        lessonNum = Convert.ToInt32(Console.ReadLine());

                                        #endregion
                                        
                                        break;

                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("invalid input\n {0}", e.Message);
                                    }
                                }
                                //tester c-tor 
                                Trainee newTrainee = new Trainee(id, lName, fName, DateTime.Parse(birthday), (Gender)(gender == "male" ? 0 : 1), phone, address, vehicleType, gear, drivingSchool, teacherName, lessonNum);
                                bl.AddTrainee(newTrainee);

                                Console.WriteLine("do you want to sign up for test? Y/N");
                                string decision = Console.ReadLine();
                                if (decision == "y" || decision == "Y") AddNewTest(bl, out streetName, out city, out building, out address, out traineeId, out testDate, out Vehicle vehicle, out gear);
                                break;
                            case 2:
                                Console.WriteLine("1:new test\n" +
                                                  "2:check if you passed the test" +
                                                  "3: edit date time\n");
                                switch (Convert.ToInt32(Console.ReadLine()))
                                {
                                    case 1:
                                        AddNewTest(bl, out streetName, out city, out building, out address, out traineeId, out testDate, out Vehicle vehicle, out gear);
                                        break;
                                    case 2:
                                        Console.WriteLine("put your ID and vehicle type\n");
                                        traineeId = Console.ReadLine();
                                        vehicleType = (Vehicle)(Convert.ToInt32(Console.ReadLine()));
                                        Test temp = bl.GetTestsList().Where(item => item.TraineeId == traineeId && item.VehicleType == vehicleType).LastOrDefault();
                                        if (temp == null)
                                        {
                                            Console.WriteLine("passed\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine(" NOT passed\n");
                                        }
                                        //find the excepted test
                                        //check if trainee passed the test
                                        break;
                                    case 3:
                                        //NOT finished this case...
                                        Console.WriteLine("put the test ID\n");
                                        string testId = Console.ReadLine();
                                        Test tempTest = bl.GetTestsList().Find(item => item.ID == testId);
                                        Console.WriteLine("put a new date\n");
                                        DateTime newDate = DateTime.Parse(Console.ReadLine());
                                        //check if there is free tester


                                        //find the expected test
                                        // edit date time
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

            } while (true);

        }

        

        private static void AddNewTest(IBL bl, out string streetName, out string city, out int building, out Address address, out string traineeId,out DateTime testDate, out Vehicle vehicle, out Gear gear)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("put the next fields:\n" +
                              "trainee id, test date, test address, gear");
                    traineeId = Console.ReadLine();
                    Console.WriteLine("which date? at format DD/MM/YY");
                    testDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("hour between 9-14");
                    testDate.AddHours(Convert.ToInt32(Console.ReadLine()));


                    Console.WriteLine("add street name, building, city\n");

            streetName = Console.ReadLine();
            building = Convert.ToInt32(Console.ReadLine());
            city = Console.ReadLine();
            address = new Address(streetName, building, city);
                    Console.WriteLine("vehicle, gear");
            vehicle = (Vehicle)(Convert.ToInt32(Console.ReadLine()));
            gear = (Gear)(Convert.ToInt32(Console.ReadLine()));
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("invalid input\n {0}", e.Message);
                }
            }
            //creating new test and adding to list
            Test newTest = new Test(traineeId, testDate, address, vehicle, gear);
            bl.AddTest(newTest);
        }
    }

}
