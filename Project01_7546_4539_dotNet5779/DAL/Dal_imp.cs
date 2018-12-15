using BE;
using static BE.Configuration;
using static DS.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class Dal_imp : IDAL
    {
        protected static Dal_imp instance = null;
        public static IDAL getDal()
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
            foreach (var itemTrainee in Trainees)
            {
                if (my_trainee.ID == itemTrainee.ID)
                {
                    throw new Exception("ERROR:\n" +
                                        "This trainee already exist\n");
                }
            }
            Trainees.Add(my_trainee);
        }

        public List<Tester> GetTestersList()
        {
            List < Tester > copyTesters = new List<Tester>(Testers);
            return copyTesters;
        }

        public List<Test> GetTestsList()
        {
            List<Test> copyTests = new List<Test>(Tests);
            return copyTests;
        }

        public List<Trainee> GetTraineeList()
        {
            List<Trainee> copyTrainees = new List<Trainee>(Trainees);
            return copyTrainees;
        }

        public void RemoveTester(string id)
        {
            try
            {
                Testers.Remove((from s in Testers
                        where s.ID == id
                        select s
                    ).First());
                
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
                Trainees.Remove((from s in Trainees
                        where s.ID == id
                        select s
                    ).First());

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
