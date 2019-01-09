using BE;
using static BE.Configuration;
using static DS.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using static BE.Enums;

namespace DAL
{

    public class Dal_imp : IDAL
    {
        #region Singleton
        protected static Dal_imp instance = null;
        public static IDAL GetDal()
        {
             if (instance == null)
                instance = new Dal_imp();
             return instance;
        }
        protected Dal_imp()
        {
            #region help to insert

            List<LicenseType> vt = new List<LicenseType>();
             vt.Add(new LicenseType(Vehicle.motorcycle,Gear.manual,10));
             vt.Add(new LicenseType(Vehicle.motorcycle,Gear.auto,10));
             vt.Add(new LicenseType(Vehicle.privateCar, Gear.manual, 32));
             vt.Add(new LicenseType(Vehicle.privateCar, Gear.auto, 33));
             vt.Add(new LicenseType(Vehicle.midTrailer, Gear.manual, 34));
             vt.Add(new LicenseType(Vehicle.midTrailer, Gear.auto, 35));
             vt.Add(new LicenseType(Vehicle.maxTrailer, Gear.manual, 25));
             vt.Add(new LicenseType(Vehicle.maxTrailer, Gear.auto, 6));
            Tester tester1 = new Tester("314784539", "אהוד", "גרשוני", DateTime.Parse("13/02/1970"), Gender.male, "053","0010199", new Address("shakhal", 8, "jerusalem"),"ehud@g.jct.ac.il","1234", Vehicle.privateCar, 13, 30, 100);
            //Console.WriteLine(tester1);
            Testers.Add(tester1);
            Tester tester2 = new Tester("000002121", "דודו", "כהן", DateTime.Parse("30/07/1956"), Gender.male, "053","0010100", new Address("kolombia", 6, "jerusalem"), "dudu@g.jct.ac.il", "1234", Vehicle.midTrailer, 11, 30, 100);
            //Console.WriteLine(tester1);
            Testers.Add(tester2);
            Tester tester3 = new Tester("201057858", "רועי דוד", "מרגלית", DateTime.Parse("06/01/1948"), Gender.male, "053", "1010199", new Address("shakhal", 8, "גלעד"), "roi@global.com", "1234", Vehicle.motorcycle, 13, 30, 100);
            //Console.WriteLine(tester1);
            Testers.Add(tester3);

            Trainee trainee1 = new Trainee("032577546", "ישי", "בדיחי", DateTime.Parse("30/07/1996"), Gender.male, "052","6608111", new Address("kolombia", 7, "jerusalem"), "yishay@g.jct.ac.il", "1234",vt, "or-yarok", tester1.FirstName + " " + tester1.LastName);
            Trainees.Add(trainee1);
            //Console.WriteLine(trainee1);
            Trainee trainee2 = new Trainee("206026858", "הדס", "גרשוני", DateTime.Parse("17/03/1996"), Gender.female, "058","6114147", new Address("kolombia", 7, "jerusalem"), "hadas@g.jct.ac.il", "1234", vt, "or-yarok", tester1.FirstName + " " + tester1.LastName);
            Trainees.Add(trainee2);

            Test checkTest = new Test("032577546", DateTime.Parse("23/12/18 9:0"), new Address("f", 4, "a"), Vehicle.privateCar, Gear.manual);
            Tests.Add(checkTest);

            checkTest.TestDistance = false;
            checkTest.TestReverseParking = false;
            checkTest.TestMirrors = true;
            checkTest.TestVinker = true;
            checkTest.TestMerge = true;
            checkTest.TestResult = false;
            UpdateTest(checkTest);
            checkTest = new Test("206026858", DateTime.Parse("23/12/18 10:0"), new Address("f", 4, "a"), Vehicle.privateCar, Gear.manual);
            Tests.Add(checkTest);
            checkTest.TestDistance = false;
            checkTest.TestReverseParking = false;
            checkTest.TestMirrors = false;
            checkTest.TestVinker = true;
            checkTest.TestMerge = true;
            checkTest.TestResult = false;
            checkTest.TestComment = " ";
            UpdateTest(checkTest);
            #endregion
            //default c-tor
        }
        #endregion


        //adders
        //-------------------------------------------------
        public void AddTest(Test my_test)
        {
            try
            {
                my_test.ID = Courent_test_id.ToString().PadLeft(9 - Courent_test_id.ToString().Length, '0');
                Courent_test_id++;
                Tests.Add(my_test);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void AddTester(Tester my_tester)
        {
            try
            {
                var v = (from item in Testers
                    where item.ID == my_tester.ID
                    select item).Count();
                if (v == 0)
                {
                    Testers.Add(my_tester);
                }
                else
                {
                    throw new Exception("ERROR:\n" +
                                        "This Tester already exist\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void AddTrainee(Trainee my_trainee)
        {
            try
            {
                if (Trainees.Where(x => x.ID == my_trainee.ID).Count() > 0)
                {
                    throw new Exception("ERROR:\n" +
                                        "This trainee already exist\n");
                }
                Trainees.Add(my_trainee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        //geters list by dippinng copying lists
        //-------------------------------------------------
        public List<Tester> GetTestersList()
        {
            List<Tester> copyTesters = new List<Tester>();
            copyTesters = Testers.Select(x => new Tester(x.ID, x.FirstName, x.LastName, x.Birthday, x.Gender,
                    x.PhoneAreaCode, x.PhoneNumber,
                    new Address(x.Address.StreetName, x.Address.BuildingNumber, x.Address.City), x.Email, x.Password,
                    x.VehicleType,
                    x.Seniority, x.MaxTestsForWeek, x.MaxDistance, x.Schedule, new List<Test>(x?.TesterTests.Select(y =>
                        new Test(y.TraineeId, y.TestDateAndTime,
                            new Address(y.TestAddress.StreetName, y.TestAddress.BuildingNumber, y.TestAddress.City),
                            y.VehicleType,
                            y.Gear, y.TestComment, y.TestDistance,
                            y.TestReverseParking, y.TestMirrors, y.TestMerge, y.TestVinker, y.TestResult, y.TesterId,
                            y.ID)).ToList())))
                .ToList();
            return copyTesters;
        }
        public List<Test> GetTestsList()
        {
            List<Test> copyTests = new List<Test>();
            copyTests = Tests.Select(x => new Test(x.TraineeId, x.TestDateAndTime,
                new Address(x.TestAddress.StreetName, x.TestAddress.BuildingNumber, x.TestAddress.City), x.VehicleType,
                x.Gear, x.TestComment, x.TestDistance,
                x.TestReverseParking, x.TestMirrors, x.TestMerge, x.TestVinker, x.TestResult, x.TesterId,
                x.ID)).ToList();
            return copyTests;
        }
        public List<Trainee> GetTraineeList()
        {
            List<Trainee> copyTrainees = new List<Trainee>();
            copyTrainees = Trainees.Select(x => new Trainee(x.ID, x.FirstName, x.LastName, x.Birthday, x.Gender, x.PhoneAreaCode, x.PhoneNumber,
                new Address(x.Address.StreetName, x.Address.BuildingNumber, x.Address.City), x.Email, x.Password, new List<LicenseType>(x?.LicenseType.Select(y => new LicenseType(y.VehicleType, y.Gear, y.LessonNum)).ToList()), 
                x.DrivingSchool, x.TeacherName)).ToList();
            return copyTrainees;
        }
        //


        //remove
        //-------------------------------------------------
        public void RemoveTester(string id)
        {
            try
            {
                Testers.Remove(Testers.Where(x => x.ID == id).First());
            }
            catch
            {
                throw new Exception("ERROR:\n" +
                                        "This tester does NOT exist\n");
            }
        }
        public void RemoveTrainee(string id)
        {
            try
            {
                Trainees.Remove(Trainees.Where(x => x.ID == id).First());
            }
            catch
            {
                throw new Exception("ERROR:\n" +
                                    "This trainee does NOT exist\n");
            }
        }


        //update
        //-------------------------------------------------
        public void UpdateTest(Test my_test)
        {
            try
            {
                var updateTest = Tests.Where(itemTest => itemTest.ID == my_test.ID).FirstOrDefault();
                if (updateTest == null)
                    throw new Exception("ERROR:\n" +
                                        "This test does NOT exist\n");
                
                Tests.Remove(updateTest);
                updateTest = new Test(my_test.TraineeId, my_test.TestDateAndTime, new Address(my_test.TestAddress.StreetName, my_test.TestAddress.BuildingNumber,
                        my_test.TestAddress.City, my_test.TestAddress.TemporaryCoordinate), my_test.VehicleType,
                    my_test.Gear, my_test.TestComment, my_test.TestDistance, my_test.TestReverseParking,
                    my_test.TestMirrors, my_test.TestMerge, my_test.TestVinker, my_test.TestResult, my_test.TesterId,
                    my_test.TestComment);
                Tests.Add(updateTest);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void UpdateTester(Tester my_tester)
        {
            try
            {
                var updateTester = Testers.Where(itemTester => itemTester.ID == my_tester.ID).FirstOrDefault();
                
                if (updateTester == null)
                    throw new Exception("ERROR:\n" +
                                        "This tester does NOT exist\n");

                Testers.Remove(updateTester);
                updateTester = new Tester(my_tester.ID, my_tester.FirstName, my_tester.LastName, my_tester.Birthday,
                    my_tester.Gender, my_tester.PhoneAreaCode, my_tester.PhoneNumber, new Address(my_tester.Address.StreetName, my_tester.Address.BuildingNumber, my_tester.Address.City), my_tester.Email,my_tester.Password, my_tester.VehicleType,
                    my_tester.Seniority, my_tester.MaxTestsForWeek, my_tester.MaxDistance, my_tester.Schedule,
                    new List<Test>(my_tester?.TesterTests.Select(y =>
                        new Test(y.TraineeId, y.TestDateAndTime,
                            new Address(y.TestAddress.StreetName, y.TestAddress.BuildingNumber, y.TestAddress.City),
                            y.VehicleType,
                            y.Gear, y.TestComment, y.TestDistance,
                            y.TestReverseParking, y.TestMirrors, y.TestMerge, y.TestVinker, y.TestResult, y.TesterId,
                            y.ID))));
                Testers.Add(updateTester);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void UpdateTrainee(Trainee my_trainee)
        {
            try
            {
                var v = Trainees.Where(itemTrainee => itemTrainee.ID == my_trainee.ID).FirstOrDefault();

                if (v == null)
                    throw new Exception("ERROR:\n" +
                                        "This Trainee does NOT exist\n");

                Trainees.Remove(v);
                v = new Trainee(my_trainee.ID, my_trainee.FirstName, my_trainee.LastName, my_trainee.Birthday,
                    my_trainee.Gender, my_trainee.PhoneAreaCode, my_trainee.PhoneNumber,
                    new Address(my_trainee.Address.StreetName, my_trainee.Address.BuildingNumber,
                        my_trainee.Address.City), my_trainee.Email, my_trainee.Password,
                    new List<LicenseType>(my_trainee.LicenseType
                        .Select(y => new LicenseType(y.VehicleType, y.Gear, y.LessonNum)).ToList()),
                    my_trainee.DrivingSchool, my_trainee.TeacherName);
                Trainees.Add(v);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //-------------------------------------------------

    }
}
