using DAL;
using BE;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class bl_imp : IBL
    {
        IDAL dal = Dal_imp.getDal();

        protected static bl_imp instance = null;
        public static IBL getBl()
        {
            if (instance == null)
                instance = new bl_imp();
            return instance;
        }

        protected bl_imp()
        {
            //default c-tor
        }
        //tester 
        public void AddTester(Tester my_tester)
        {
            DateTime duration = my_tester.Birthday.AddYears(40);//to calculate the tester`s age i added 40 years and compare to Now
            if (duration >=DateTime.Now)
            {
                dal.AddTester(my_tester);
            }
            else
            {
                throw new Exception("ERROR:\n" +
                                    "This Tester are too young\n");
            }
        }
        public void RemoveTester(string id)
        {
            dal.RemoveTester(id);
        }

        public void UpdateTester(Tester my_tester)
        {
            dal.UpdateTester(my_tester);
        }

        //trainee
        public void AddTrainee(Trainee my_trainee)
        {
            DateTime duration = my_trainee.Birthday.AddYears(18);//to calculate the trainee`s age i added 18 years and compare to Now
            if (duration >= DateTime.Now)
            {
                dal.AddTrainee(my_trainee);
            }
            else
            {
                throw new Exception("ERROR:\n" +
                                    "This Trainee are too young\n");
            }
            dal.AddTrainee(my_trainee);
        }

        public void RemoveTrainee(string id)
        {
            dal.RemoveTrainee(id);
        }

        public void UpdateTrainee(Trainee my_trainee)
        {
            dal.UpdateTrainee(my_trainee);
        }

        //test
        public void AddTest(Test my_test)
        {
            if (TraineeHave20Lessons(my_test)&&NotExistTestIn7Days(my_test)&& !PassedForThisType(my_test))
            {//now the trainee past the conditions and we need to find tester
                //List<Tester> relevantTesters = new List<Tester>();
                var relevantTester = (from tester in dal.GetTestersList()
                                     let conditions = getListOfNotGetToMaxTests(getListOfTestersAtTraineeArea(getListByVehicleType(my_test),
                                     my_test.TestAdress))
                                     from item in conditions
                                     where FreeTester(item, my_test)
                                     select item
                                     ).FirstOrDefault();
                if (relevantTester!=null)
                {
                    my_test.TesterId = relevantTester.ID;
                    relevantTester.schedule[my_test.TestDateAndTime.Day][my_test.TestDateAndTime.Hour] =
                        false;
                }
                else
                {
                    Console.WriteLine("we didn`t find available tester at your date request, here a new date offer\n");
                    var conditions =
                        getListOfNotGetToMaxTests(getListOfTestersAtTraineeArea(getListByVehicleType(my_test),
                        my_test.TestAdress));
                    if (!getFreeTesterAndTime(conditions, my_test))
                    {
                        throw new Exception("we didn`t find any tester at your area, please change your test adress request ");
                    }
                }
                dal.AddTest(my_test);
            }
        }

        public bool FreeTester(Tester t, Test myTest) => t.schedule[myTest.TestDateAndTime.Day][myTest.TestDateAndTime.Hour];

        public bool getFreeTesterAndTime(List<Tester> testersCondition, Test myTest)
        {
            foreach (var tester in testersCondition)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (tester.schedule[(DayOfWeek)i][j])
                        {
                            Console.WriteLine("would ypu like to set at {0} on {1}? ", (DayOfWeek)i, j);
                            bool answer = Convert.ToBoolean(Console.ReadKey());
                            if (answer)
                            {
                                myTest.TestDateAndTime.Day = (DayOfWeek) i;
                                myTest.TestDateAndTime.Hour = j;
                                tester.schedule[(DayOfWeek) i][j] = false;
                                myTest.TesterId = tester.ID;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public List<Tester> getListOfNotGetToMaxTests(List<Tester> testers)
        {
            List<Test> testList = GetTestsList();
            foreach (var tester in testers)
            {
                var v = (from item in testList
                    where item.TesterId == tester.ID
                    select item).Count();
                if (v >= tester.MaxTestsForWeek)
                {
                    testers.Remove(tester);
                }
            }
            //after removing the unrelevant tester
            return testers;
        }
        public List<Tester> getListOfTestersAtTraineeArea(List<Tester> copyTesters, Adress my_adress)
        {
            Random testerCoardinate = new Random(200);
            Random traineeCoardinate = new Random(200);
            var distance = (UInt32) (testerCoardinate.Next() - traineeCoardinate.Next());
            return copyTesters.Where(item => distance <= item.MaxDistance).ToList();
        }

        public List<Tester> getListByVehicleType(Test my_test)
        {
            return GetTestersList().Where(item => item.VehicleType == my_test.VehicleType).ToList();
        }



        public bool TraineeHave20Lessons(Test my_test)
        {
            var v = (from item in GetTraineeList()
                                  where item.ID == my_test.TraineeId && item.LessonNum >= 20
                                  select item).FirstOrDefault();
            return v != null ? true: throw new Exception("ERROR:\n" +
                                    "This trainee have not study 20 lesson yet\n");
            
        }

        public bool NotExistTestIn7Days(Test my_test)
        {
            var v = (from item in GetTestsList()
                    where item.TraineeId == my_test.TraineeId && DateTime.Now.DayOfWeek - item.TestDateAndTime.Day < 7
                    select item).FirstOrDefault();
            return v==null? true : throw new Exception("ERROR:\n" +
                                             "This Test are too early (haven't pass 7 days from this trainee last test\n");
        }

        public void UpdateTest(Test my_test)
        {
            //פונקציה הבודקת אם עבר 4/5 אם כן חייב לקבל עובר ואם לא, חייב לקבל נכשל.
            //need to update the field num of test and func to check if tester fill all
            if(ResultMeetingCriteria(my_test) && TheTesterFillAll(my_test))
               dal.UpdateTest(my_test);
        }

        public bool TheTesterFillAll(Test my_test)
        {
            foreach (var property in my_test.GetType().GetProperties())
            {
                if (property.GetValue(my_test,null) == null) throw new Exception("ERROR: you need to fill all fields of test");
            }

            return true;
        }

        public bool ResultMeetingCriteria(Test my_test)
        {
            int count = 0;
            if (my_test.TestDistance == true) count++;
            if (my_test.TestMirrors == true) count++;
            if (my_test.TestVinker == true) count++;
            if (my_test.TestReverseParking == true) count++;
            if (count < 4)
            {
                if (my_test.TestResult == true)
                {
                    throw new Exception("WARNING: the trainee failed in more then one condition, please consider your result\n");
                }
            }
            else
            {
                if (my_test.TestResult == false)
                {
                    throw new Exception("WARNING: the trainee succeed in 4/5 condition, please consider your result\n");
                }
            }

            return true;
        }



        //string getters list
        public List<Tester> GetTestersList()
        {
            return dal.GetTestersList();
        }

        public List<Trainee> GetTraineeList()
        {
            return dal.GetTraineeList();
        }

        public List<Test>  GetTestsList()
        {
            return dal.GetTestsList();
        }

        public IEnumerable<IGrouping<Vehicle, Tester>> groupTestersByVehicle(bool toSort = false)
        {
            IEnumerable<IGrouping<Vehicle, Tester>> result =
                from tester in dal.GetTestersList()
                group tester by tester.VehicleType;
            //to sort condition by vehicle
            if (toSort)
            {
                result.OrderBy(x => x.Key);
            }
            return result;
        }
        public IEnumerable<IGrouping<string, Trainee>> groupTraineesBySchool(bool toSort = false)
        {
            IEnumerable<IGrouping<string, Trainee>> result =
                from trainee in dal.GetTraineeList()
                group trainee by trainee.DrivingSchool;
            //to sort condition by school name
            if (toSort)
            {
                result.OrderBy(x => x.Key);
            }
            return result;
        }

        public IEnumerable<IGrouping<string, Trainee>> groupTraineesByTester(bool toSort = false)
        {
            IEnumerable<IGrouping<string, Trainee>> result =
                from trainee in dal.GetTraineeList()
                group trainee by trainee.TeacherName;
            //to sort condition by school name
            if (toSort)
            {
                result.OrderBy(x => x.Key);
            }
            return result;
        }

        public IEnumerable<IGrouping<int, Trainee>> groupTraineesByNumOfTests(bool toSort = false)
        {

            IEnumerable < IGrouping<int, Trainee>> result =
                from trainee in dal.GetTraineeList()
                group trainee by trainee.NumOfTests;
            //to sort condition by school name
            if (toSort)
            {
                result.OrderBy(x => x.Key);
            }
            return result;
        }

               public delegate bool listOfDelegate(Test myTest);
        public List<Test> MyList(listOfDelegate some)
        {
            List<Test> newList = new List<Test>();
            foreach (var itemTest in dal.GetTestsList())
            {
                if (some(itemTest))
                {
                    newList.Add(itemTest);
                }
            }
            return newList;
        }

        public bool TraineePass(Test myTest)
        {
            if (myTest.TestResult == true)
            {
                return true;
            }
            return false;
        }
        public bool Motorcycle(Test myTest)
        {
            if (myTest.VehicleType == Vehicle.motorcycle)
            {
                return true;
            }
            return false;
        }
        public bool successInParking(Test myTest)
        {
            if (myTest.TestReverseParking == true)
            {
                return true;
            }
            return false;
        }

        public bool PassedForThisType(Test myTest)
        {
            var conditionCheck = (from test in dal.GetTestsList()
                where test.TraineeId == myTest.TraineeId && //same id
                     (( test.VehicleType == myTest.VehicleType &&  //case a: same vehicle and (same gear or manual gear)
                      (test.Gear == myTest.Gear || test.Gear == Gear.manual ))|| 
                       ((test.VehicleType == Vehicle.maxTrailer || test.VehicleType == Vehicle.midTrailer) && myTest.VehicleType == Vehicle.privateCar)) //case b: he has track license and want to test for private
                       && test.TestResult == true//pass the test
                                  select test).FirstOrDefault();
            return conditionCheck != null ? true : throw new Exception("dear trainee: you have already driver license for this type of test\n");
        }

        public int NumberOfTest(Trainee myTrainee) => dal.GetTestsList().Count(item => item.TraineeId == myTrainee.ID);

        public bool DeservingToLicense(Trainee myTrainee)
        {var  v = dal.GetTestsList().Where(item => item.TraineeId == myTrainee.ID && item.TestResult == true).FirstOrDefault();
            return v != null? true: false;
        }
        public List<Test> GetListForExpectedDay(DayOfWeek expectedDay) => dal.GetTestsList().Where(test => test.TestDateAndTime.Day == expectedDay).ToList();

        public Test GetTestById(string id) => dal.GetTestsList().Where(test => test.ID == id).FirstOrDefault();

    }

}
