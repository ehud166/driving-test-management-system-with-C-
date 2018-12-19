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
        public void AddTest(Test my_test)
        {
            my_test.ID = Courent_test_id.ToString().PadLeft(8 - Courent_test_id.ToString().Length, '0');
            Courent_test_id++;
            Tests.Add(my_test);
        }

        public void AddTester(Tester my_tester)
        {
            var v = (from item in Testers
                where item.ID == my_tester.ID
                select item).FirstOrDefault();
            if (v == null)
            {
                Testers.Add(my_tester);
            }
            else
            {
                throw new Exception("ERROR:\n" +
                                    "This Tester already exist\n");
            }
        }

        

        public void AddTrainee(Trainee my_trainee)
        {
            if (Trainees.Where(x=>x.ID == my_trainee.ID).Count() > 0)
                {
                    throw new Exception("ERROR:\n" +
                                        "This trainee already exist\n");
                }
            Trainees.Add(my_trainee);
        }

        public List<Tester> GetTestersList()
        {
            List<Tester> copyTesters = new List<Tester>();
            copyTesters = Testers.Select(x => new Tester(x.ID, x.LastName, x.FirstName, x.Birthday, x.Gender, x.Phone, new Address(x.Address.StreetName, x.Address.BuildingNumber, x.Address.City), x.VehicleType, x.Seniority, x.MaxTestsForWeek, x.MaxDistance))
                .ToList();
            return copyTesters;
        }

        public List<Test> GetTestsList()
        {
            List<Test> copyTests = new List<Test>();
                copyTests = Tests.Select(x => new Test(x.TraineeId, x.TestDateAndTime, new Address(x.TestAddress.StreetName, x.TestAddress.BuildingNumber, x.TestAddress.City),x.VehicleType, x.Gear, x.TestDistance,
                x.TestReverseParking, x.TestMirrors, x.TestMerge, x.TestVinker, x.TestResult, x.TesterId, x.TestComment,
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

        public void UpdateTest(Test my_test)
        {
            foreach (var itemTest in Tests)
            {
                if (my_test.ID == itemTest.ID)
                {
                    Tests.Remove(itemTest);
                    Tests.Add(my_test);
                }
            }
            throw new Exception("ERROR:\n" +
                                "This test does NOT exist\n");
        }

        public void UpdateTester(Tester my_tester)
        {
            foreach (var itemTester in Testers)
            {
                if (my_tester.ID == itemTester.ID)
                {
                    Testers.Remove(itemTester);
                    Testers.Add(my_tester);
                }
            }
            throw new Exception("ERROR:\n" +
                                "This tester does NOT exist\n");
        }

        public void UpdateTrainee(Trainee my_trainee)
        {
            foreach (var itemTrainee in Trainees)
            {
                if (my_trainee.ID == itemTrainee.ID)
                {
                    Trainees.Remove(itemTrainee);
                    Trainees.Add(my_trainee);
                }
            }
            throw new Exception("ERROR:\n" +
                                "This Trainee does NOT exist\n");
        }

    }
}
