using System;
using System.ComponentModel;
using System.Linq;
using BE;
using BL;
using static  BE.User;

namespace PL
{
    public class Program
    {

        static void Main(string[] args)
        {
            IBL bl = Bl_imp.GetBl();

            #region help to insert


            

            Tester tester1 = new Tester("314784539", "ehud", "gershony", DateTime.Parse("13/02/1970"), Gender.male, "0530010199", new Address("shakhal", 8, "jerusalem"), Vehicle.privateCar, 13, 30, 100);
            //Console.WriteLine(tester1);
            bl.AddTester(tester1);
            Tester tester2 = new Tester("000002121", "dudu", "cohen", DateTime.Parse("30/07/1956"), Gender.male, "0530010100", new Address("kolombia", 6, "jerusalem"), Vehicle.privateCar, 11, 30, 100);
            //Console.WriteLine(tester1);
            bl.AddTester(tester2);

            Trainee trainee1 = new Trainee("032577546", "yishay", "badichi", DateTime.Parse("30/07/1996"), Gender.male, "053823117", new Address("kolombia", 7, "jerusalem"), Vehicle.privateCar, Gear.manual, "or-yarok", tester1.FirstName + " " + tester1.LastName,50);
            bl.AddTrainee(trainee1);
            //Console.WriteLine(trainee1);
            Trainee trainee2 = new Trainee("206026858", "hadas", "gershony", DateTime.Parse("17/03/1996"), Gender.male, "053823117", new Address("kolombia", 7, "jerusalem"), Vehicle.privateCar, Gear.manual, "or-yarok", tester1.FirstName + " " + tester1.LastName, 50);
            bl.AddTrainee(trainee2);

            Test checkTest = new Test("032577546", DateTime.Parse("23/12/18 9:0"), new Address("f", 4, "a"), Vehicle.privateCar, Gear.manual);
            bl.AddTest(checkTest);
            checkTest.TestDistance = true;
            checkTest.TestReverseParking = true;
            checkTest.TestMirrors = true;
            checkTest.TestVinker = true;
            checkTest.TestMerge = true;
            checkTest.TestResult = true;
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



            #region decleration to input

            string id, lName, fName, birthday, gender, phone, drivingSchool, teacherName, streetName, city, traineeId;
            int lessonNum, seniority, maxTestsForWeek, maxDistance, building;
            Vehicle vehicleType;
            Address address;
            Gear gear;
            DateTime testDate;
            User tryUser;
            #endregion


            Console.WriteLine("\nwelcome to computer data bank of transport ministery");

            do
            {
                try
                {
                    Console.WriteLine("choose your section\n" +
                                      "1:administrator\n" +
                                      "2: tester\n" +
                                      "3: trainee\n" +
                                      "4: exit");
                    string user = Console.ReadLine();
                    
                    if (Enum.TryParse<User>(user, out tryUser))
                        switch (tryUser)
                    {
                        case administrator:
                            Console.WriteLine("print:\n   1:all tests list" +
                                              "\n   2:all testers list" +
                                              "\n   3:all trainees list" +
                                              "\n   4:group testers by vehicle" +
                                              "\n   5:group trainees by school" +
                                              "\n   6:group test by result");

                        switch (Convert.ToInt32(Console.ReadLine()))
                            {
                                case 1:
                                    foreach (var test in bl.GetTestsList())
                                    {
                                        Console.WriteLine(test+ "\n");
                                    }
                                    break;
                                case 2:
                                    foreach (var test in bl.GetTestersList())
                                    {
                                        Console.WriteLine(test + "\n");
                                    }
                                        break;
                                case 3:
                                    foreach (var test in bl.GetTraineeList())
                                    {
                                        Console.WriteLine(test + "\n");
                                    }
                                        break;
                               
                                case 4:
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
                                case 5:
                                    Console.WriteLine("print Trainees by school");
                                    foreach (var courentKey in bl.GroupTraineesBySchool())
                                    {
                                        Console.WriteLine("key: {0}\n", courentKey.Key);
                                        foreach (var i in courentKey)
                                        {
                                            Console.WriteLine("{0}\n", i);
                                        }
                                    }

                                    break;
                                case 6:
                                    Console.WriteLine("print tests by result");
                                    foreach (var courentKey in bl.GroupTestByResult())
                                    {
                                        Console.WriteLine("key: {0}\n", courentKey.Key);
                                        foreach (var i in courentKey)
                                        {
                                            Console.WriteLine("{0}\n", i);
                                        }
                                    }

                                    break;
                                default:
                                    break;

                            }

                            break;
                        case tester:
                            Console.WriteLine("1: new tester\n" +
                                              "2: exist tester\n");
                            switch (Convert.ToInt32(Console.ReadLine()))
                            {
                                case 1:
                                    while (true)
                                    {
                                        Console.WriteLine("put the next fields:\n" +
                                                          "   ID,first name, last name, birthday(with the form: DD/MM/YYYY, gender, phone number, address, vehicle, seniority, maxTestsForWeek, maxDistance");
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
                                            Console.WriteLine("1: privateCar ,2: motorcycle,3: midTrailer, 4: maxTrailer\n");
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
                                    Tester newTester = new Tester(id, lName, fName, DateTime.Parse(birthday),
                                        (Gender) (gender == "male" ? 0 : 1), phone, address, vehicleType, seniority,
                                        maxTestsForWeek, maxDistance);
                                    //add to list
                                    bl.AddTester(newTester);
                                    break;
                                case 2:
                                    Console.WriteLine("1:update test\n" +
                                                      "2:quit from this job\n");
                                    switch (Convert.ToInt32(Console.ReadLine()))
                                    {
                                        case 1:
                                                #region InputResultTest

                                            Console.WriteLine("put the test ID\n");
                                            string testId = Console.ReadLine();
                                                Test tempTest = bl.GetTestsList().Find(item => item.ID == testId);
                                            Console.WriteLine(
                                                "put v/x: distance, reverse, mirrors,vinker, merge, result,comment\n");
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
                        case trainee:
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
                                            vehicleType = (Vehicle) (Convert.ToInt32(Console.ReadLine()));
                                            gear = (Gear) (Convert.ToInt32(Console.ReadLine()));
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
                                    Trainee newTrainee = new Trainee(id, lName, fName, DateTime.Parse(birthday),
                                        (Gender) (gender == "male" ? 0 : 1), phone, address, vehicleType, gear,
                                        drivingSchool, teacherName, lessonNum);
                                    //adding to list
                                    bl.AddTrainee(newTrainee);

                                    Console.WriteLine("do you want to sign up for test? Y/N");
                                    string decision = Console.ReadLine();
                                    if (decision == "y" || decision == "Y")
                                        AddNewTest(bl, out streetName, out city, out building, out address,
                                            out traineeId, out testDate, out Vehicle vehicle, out gear);
                                    break;
                                case 2:
                                    Console.WriteLine("1:new test\n" +
                                                      "2:check if you passed the test" +
                                                      "3: edit date time\n");
                                    switch (Convert.ToInt32(Console.ReadLine()))
                                    {
                                        case 1:
                                            AddNewTest(bl, out streetName, out city, out building, out address,
                                                out traineeId, out testDate, out Vehicle vehicle, out gear);
                                            break;
                                        case 2:
                                            Console.WriteLine("put your ID");
                                            traineeId = Console.ReadLine();
                                            Console.WriteLine("choose vehicle type\n 1: privateCar ,2: motorcycle,3: midTrailer, 4: maxTrailer");
                                            vehicleType = (Vehicle) (Convert.ToInt32(Console.ReadLine()));
                                            Test temp = bl.GetTestsList().Where(item =>
                                                    item.TraineeId == traineeId && item.VehicleType == vehicleType && item.TestResult == false)
                                                .LastOrDefault();
                                            if (temp == null)
                                            {
                                                Console.WriteLine("passed");
                                            }
                                            else
                                            {
                                                Console.WriteLine(" NOT passed");
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
                            case exit:
                                return;
                            default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
