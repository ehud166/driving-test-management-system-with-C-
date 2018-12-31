using BE;
using static BE.Configuration;
using static DS.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;

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
            copyTesters = Testers.Select(x => new Tester(x.ID, x.LastName, x.FirstName, x.Birthday, x.Gender, x.Phone,
                    new Address(x.Address.StreetName, x.Address.BuildingNumber, x.Address.City), x.VehicleType,
                    x.Seniority, x.MaxTestsForWeek, x.MaxDistance, x.Schedule, new List<Test>(x.TesterTests)))
                .ToList();
            return copyTesters;
        }
        public List<Test> GetTestsList()
        {
            List<Test> copyTests = new List<Test>();
                copyTests = Tests.Select(x => new Test(x.TraineeId, x.TestDateAndTime, new Address(x.TestAddress.StreetName, x.TestAddress.BuildingNumber, x.TestAddress.City),x.VehicleType, x.Gear, x.TestComment, x.TestDistance,
                x.TestReverseParking, x.TestMirrors, x.TestMerge, x.TestVinker, x.TestResult, x.TesterId,
                x.ID)).ToList();
            return copyTests;
        }
        public List<Trainee> GetTraineeList()
        {
            List<Trainee> copyTrainees = new List<Trainee>();
            copyTrainees = Trainees.Select(x => new Trainee(x.ID, x.LastName, x.FirstName, x.Birthday, x.Gender, x.Phone,
                new Address(x.Address.StreetName, x.Address.BuildingNumber, x.Address.City), x.VehicleType, x.Gear,
                x.DrivingSchool, x.TeacherName, x.LessonNum)).ToList();
            return copyTrainees;
        }


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
                var v = Tests.Where(itemTest => itemTest.ID == my_test.ID).Select(itemTest => new Test(itemTest.TraineeId,itemTest.TestDateAndTime,itemTest.TestAddress,itemTest.VehicleType,itemTest.Gear,itemTest.TestComment,itemTest.TestDistance,itemTest.TestReverseParking,itemTest.TestMirrors,itemTest.TestMerge,itemTest.TestVinker,itemTest.TestResult,itemTest.TesterId,itemTest.TestComment)).Count();
                if (v == 0)
                    throw new Exception("ERROR:\n" +
                                        "This test does NOT exist\n");
                if (v > 1)
                    throw new Exception("ERROR:\n" +
                                        "there is two test with the same id\n");
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
                var v = Testers.Where(itemTester => itemTester.ID == my_tester.ID).FirstOrDefault();
                Testers.Remove(v);
                v = new Tester(my_tester.ID, my_tester.LastName, my_tester.FirstName, my_tester.Birthday,
                    my_tester.Gender, my_tester.Phone, my_tester.Address, my_tester.VehicleType,
                    my_tester.Seniority, my_tester.MaxTestsForWeek, my_tester.MaxDistance, my_tester.Schedule,
                    my_tester.TesterTests);
                Testers.Add(v);
                if (v == null)
                    throw new Exception("ERROR:\n" +
                                        "This tester does NOT exist\n");
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
                Trainees.Remove(v);
                v = new Trainee(my_trainee.ID, my_trainee.LastName, my_trainee.FirstName, my_trainee.Birthday,
                    my_trainee.Gender, my_trainee.Phone,
                    new Address(my_trainee.Address.StreetName, my_trainee.Address.BuildingNumber,
                        my_trainee.Address.City),
                    my_trainee.VehicleType, my_trainee.Gear, my_trainee.DrivingSchool, my_trainee.TeacherName,
                    my_trainee.LessonNum);
                Trainees.Add(v);
           
                if (v == null)
                    throw new Exception("ERROR:\n" +
                                        "This Trainee does NOT exist\n");
               
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
