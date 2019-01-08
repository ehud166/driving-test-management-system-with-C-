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
using static BE.Enums;
using System.Text.RegularExpressions;

namespace BL
{
    public class Bl_imp : IBL
    {
        private List<Manager> managerList;
        #region singleton
        IDAL dal = Dal_imp.GetDal();
        
        protected static Bl_imp instance = null;
        public static IBL GetBl()
        {
            if (instance == null)
                instance = new Bl_imp();
            return instance;
        }

        protected Bl_imp()
        {
            //default c-tor
            managerList = new List<Manager>();
            managerList.Add(new Manager("314784539","אהוד","1234"));
            managerList.Add(new Manager("032577546","ישי","1234"));
        }
        #endregion

        public void AddTester(Tester my_tester)
        {
            try
            {
                if (CheckTesterAge(my_tester) && CheckIdValidation(my_tester.ID))
                {
                    dal.AddTester(my_tester);
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
                if (CheckTraineeAge(my_trainee) && CheckIdValidation(my_trainee.ID))
                {
                    dal.AddTrainee(my_trainee);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public void AddTest(Test my_test)
        {
            try
            {
                bool q = TraineeConditionsForTest(my_test.TraineeId, my_test.VehicleType, my_test.Gear);
                if (q)
                {//now the trainee past the conditions and we need to find tester (including check id for trainee but not necessary to check tester because it checked on AddTester)
                    var relevantTester = (from tester in dal.GetTestersList()
                                          let listOfRelevantTesters = GetListOfTestersAtTraineeArea(
                                              GetListByVehicleType(my_test), my_test.TestAddress) //get a list of tester match the condition to test 
                                          from item in listOfRelevantTesters
                                          where FreeTester(item, my_test.TestDateAndTime) && FreeTesterAtThisWeek(item, my_test.TestDateAndTime) //now find free tester
                                          select item
                        ).FirstOrDefault();

                    if (relevantTester != null)
                    {
                        my_test.TesterId = relevantTester.ID;
                        relevantTester.TesterTests.Add(my_test);
                        dal.UpdateTester(relevantTester);
                        dal.AddTest(my_test);
                    }
                    else//not found free tester
                    {
                        var listOfRelevantTesters =
                            GetListOfTestersAtTraineeArea(GetListByVehicleType(my_test),
                                my_test.TestAddress);

                        OfferFreeDateForTest(listOfRelevantTesters);

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public bool TraineeConditionsForTest(String id,Vehicle vehicle,Gear gear)
        {
            return CheckIdValidation(id) && TraineeHave20LessonsForSpecificLicense(id,vehicle,gear) &&
                    NotExistTestIn7Days(id, vehicle, gear) && !PassedForThisType(id, vehicle, gear) &&
                    !TraineeTryingToSignTwice(id, vehicle, gear);
        }


        //--------------------------------------------------------------
        /// <summary>
        /// help functions for check tester condition
        /// </summary>
        /// <param name="my_tester">to check age</param>
        /// <returns>true if tester under 40 years old</returns>
        private static bool CheckTesterAge(Tester my_tester)
        {
            DateTime tempAge = new DateTime();
            tempAge = my_tester.Birthday;//to calculate the tester`s age i added 40 years and compare to Now
            tempAge = tempAge.AddYears(40);
            if (tempAge <= DateTime.Now)
                return true;
            else
                throw new Exception("ERROR:\n" +
                                    "This Tester are too young\n");
        }
        //--------------------------------------------------------------



        //--------------------------------------------------------------
        /// <summary>
        ///help functions for check trainee condition
        /// </summary>
        /// <param name="my_trainee">to check age</param>
        /// <returns>true if trainee under 18 years old</returns>
        private bool CheckTraineeAge(Trainee my_trainee)
        {
            DateTime tempAge = new DateTime();
            tempAge = my_trainee.Birthday;
            tempAge = tempAge.AddYears(18);//to calculate the trainee`s age i added 18 years and compare to Now
            if (tempAge <= DateTime.Now)
                return true;
            else
                throw new Exception("ERROR:\n" +
                                    "This Trainee are too young\n");
        }
        //--------------------------------------------------------------





        //--------------------------------------------------------------
        /// <summary>
        ///help functions to offer the trainee free date for test
        /// </summary>
        /// <param name="testersCondition"></param>
        public void OfferFreeDateForTest(List<Tester> testersCondition)
        {
            #region parameter to restart time

            DateTime restartDate = DateTime.Today.AddDays(3).AddHours(9);//DateTime is a value type
            //restart hour for the next round hour
            #endregion

            while (restartDate.Hour < 9 || restartDate.Hour > 14 || restartDate <= DateTime.Now)//if the date earlier from now or not in the hour of testing
            {
                restartDate = restartDate.AddHours(1);
            }
            DateTime courrent = new DateTime();
            foreach (var tester in testersCondition)
            {
                courrent = restartDate;
                while (courrent.Day - DateTime.Now.Day <= 14)//offer newe date for the trainee maximum two weeks from now
                {
                    while (courrent.DayOfWeek == DayOfWeek.Friday || courrent.DayOfWeek == DayOfWeek.Saturday)
                    {
                        courrent = courrent.AddDays(1);
                    }

                    if (FreeTester(tester,courrent) && FreeTesterAtThisWeek(tester,courrent))
                    {
                        throw new Exception(string.Format(
                            "we didn`t find available tester at your date request, here a new date offer\n" +
                            "date: {0}", courrent.ToString("MM/dd/yyyy hh:mm")));
                    }

                    courrent = courrent.AddHours(1);
                }
                //not found tester on next 14 days
            }
            throw new Exception("we didn`t find any tester at your area, please change your test address request ");
        }



        /// <summary>
        /// checking if tester avalible at this specific date and hour
        /// </summary>
        /// <param name="optionalTester">optional tester to test </param>
        /// <param name="checkDateTime">optional date</param>
        /// <returns>true for available</returns>
        public bool FreeTester(Tester optionalTester, DateTime checkDateTime)
        {
            return optionalTester.Schedule[checkDateTime.DayOfWeek][checkDateTime.Hour] && !optionalTester.TesterTests.Any(x => x.TestDateAndTime == checkDateTime);
            //bool func for tree tester in specific date
        }

        //--------------------------------------------------------------




        public void RemoveTester(string id)
        {
            try
            {
                dal.RemoveTester(id);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void RemoveTrainee(string id)
        {
            try
            {
                dal.RemoveTrainee(id);
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
                dal.UpdateTester(my_tester);
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
                dal.UpdateTrainee(my_trainee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void UpdateTest(Test my_test)
        {
            //פונקציה הבודקת אם עבר 4/5 אם כן חייב לקבל עובר ואם לא, חייב לקבל נכשל.
            //need to update the field num of test and func to check if tester fill all
            if (ResultMeetingCriteria(my_test) && TheTesterFillAll(my_test))
            {
                if (my_test.TestResult == true)//if trainee pass the test we can remove him from traineesList
                {
                    dal.RemoveTrainee(my_test.TraineeId);
                }
                dal.UpdateTest(my_test);
            }
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
        public List<Test> GetTestsList()
        {
            return dal.GetTestsList();
        }

        //grouping
        public IEnumerable<IGrouping<Vehicle, Tester>> GroupTestersByVehicle(bool toSort = false)
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
        public IEnumerable<IGrouping<string, Trainee>> GroupTraineesBySchool(bool toSort = false)
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
        public IEnumerable<IGrouping<string, Trainee>> GroupTraineesByTester(bool toSort = false)
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
        public IEnumerable<IGrouping<Gender, Trainee>> GroupTraineesByGender(bool toSort = false)
        {

            IEnumerable<IGrouping<Gender, Trainee>> result =
                from trainee in dal.GetTraineeList()
                group trainee by trainee.Gender;
            //to sort condition by school name
            if (toSort)
            {
                result.OrderBy(x => x.Key);
            }
            return result;
        }
        public IEnumerable<IGrouping<bool?, Test>> GroupTestByResult(bool toSort = false)
        {

            IEnumerable<IGrouping<bool?, Test>> result =
                from test in dal.GetTestsList()
                group test by test.TestResult;
            //to sort condition by school name
            if (toSort)
            {
                result.OrderBy(x => x.Key);
            }
            return result;
        }

        /// <summary>
        /// find the test id and get the test
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the test with this id</returns>
        public List<Test> GetTestsById(string id) => dal.GetTestsList().FindAll(test => test.TraineeId == id);



        /// <summary>
        /// find the trainee id and get the test
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the test with this Trainee id</returns>
        public List<Test> GetTestsByTraineeId(string id) => dal.GetTestsList().FindAll(test => test.TraineeId == id);

        /// <summary>
        /// find the tester id and get the tester
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the tester with this id</returns>
        public Tester GetTesterById(string id) => dal.GetTestersList().Find(tester => tester.ID == id);


        /// <summary>
        /// find the trainee id and get the trainee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the trainee with this id</returns>
        public Trainee GetTraineeById(string id) => dal.GetTraineeList().Find(trainee => trainee.ID == id);
        public Manager GetManagerById(string id) => managerList.Find(manager => manager.ID == id);


        /// <summary>
        /// enonymous function to check if this trainee deserving to license
        /// </summary>
        /// <param name="myTrainee"></param>
        /// <returns>true if he does</returns>
        public bool DeservingToLicense(Trainee myTrainee) => dal.GetTestsList().Any(item => item.TraineeId == myTrainee.ID && item.TestResult == true);


        /// <summary>
        /// check if we still can to add test to this tester at this week 
        /// </summary>
        /// <param name="optionalTester"></param>
        /// <param name="t">test date</param>
        /// <returns>true if this tester can add test this week</returns>
        private bool FreeTesterAtThisWeek(Tester optionalTester, DateTime t)
        {
            var x = (from test in optionalTester.TesterTests
                where DatesAreInTheSameWeek(test.TestDateAndTime, t)
                select test).Count();
            return x < optionalTester.MaxTestsForWeek;
        }



        /// <summary>
        /// helper funcion to check if two test are in the same week (for max tester test for week)
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns>true id at the same week</returns>
        private bool DatesAreInTheSameWeek(DateTime date1, DateTime date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

            return d1 == d2;
        }

        /// <summary>
        /// list from tester at trainee area
        /// temporary
        /// </summary>
        /// <param name="copyTesters"></param>
        /// <param name="myAddress"></param>
        /// <returns>relvant tester list</returns>
        private List<Tester> GetListOfTestersAtTraineeArea(List<Tester> copyTesters, Address myAddress)
        {
            return copyTesters.Where(item => Math.Abs(item.Address.TemporaryCoordinate-myAddress.TemporaryCoordinate) <= item.MaxDistance).ToList();
        }


        /// <summary>
        /// list function for delegat
        /// </summary>
        /// <param name="my_test"></param>
        /// <returns>list</returns>
        private List<Tester> GetListByVehicleType(Test my_test)
        {
            return GetTestersList().Where(item => item.VehicleType == my_test.VehicleType).ToList();
        }


        /// <summary>
        /// list function for delegat
        /// </summary>
        /// <param name="my_test"></param>
        /// <returns>true if trainee filling the condition</returns>
        public bool TraineeHave20Lessons(String id)
        {
            var v = (from item in GetTraineeList()
                where item.ID == id && item.LicenseType.Any(x => x.LessonNum >= 20)
                select item).FirstOrDefault();
            return v != null ? true : throw new Exception("ERROR:\n" +
                                                          "This trainee have not study 20 lesson yet for ANY vehicle type and gear or he does NOT exist\n");

        }

        /// <summary>
        /// list function for delegat
        /// </summary>
        /// <param name="my_test"></param>
        /// <returns>true if trainee filling the condition</returns>
        private bool TraineeHave20LessonsForSpecificLicense(String id,Vehicle vehicle,Gear gear)
        {
            var v = (from item in GetTraineeList()
                where item.ID == id && item.LicenseType.Any(x => vehicle == x.VehicleType && gear == x.Gear && x.LessonNum >= 20)
                select item).FirstOrDefault();
            return v != null ? true : throw new Exception("ERROR:\n" +
                                                          "This trainee have not study 20 lesson yet for this vehicle type and gear or he does NOT exist\n");

        }


        /// <summary>
        /// check if the trainee hasn`t sign or tested in 7 days
        /// </summary>
        /// <param name="my_test"></param>
        /// <returns>true if not</returns>
        private bool NotExistTestIn7Days(String id, Vehicle vehicle, Gear gear)
        {
            var v = (from item in GetTestsList()
                    where item.TraineeId == id &&  item.Gear == gear && item.VehicleType == vehicle && (item.TestDateAndTime.AddMinutes(1) - DateTime.Now).Days < 7// adding minute to round the lossing miliseconds at running time
                    select item).FirstOrDefault();
            return v==null? true : throw new Exception("ERROR:\n" +
                                             "This Test are too early (haven't pass 7 days from this trainee last test\n");
        }


        /// <summary>
        /// check if tester fill all the fields for update test
        /// </summary>
        /// <param name="my_test"></param>
        /// <returns>true if does</returns>
        private bool TheTesterFillAll(Test my_test)
        {
            foreach (var property in my_test.GetType().GetProperties())
            {
                if (property.GetValue(my_test,null) == null) throw new Exception("ERROR: you need to fill all fields of test");
            }

            return true;
        }


        /// <summary>
        /// check if tester filling 4 from 5 condition to pass
        /// </summary>
        /// <param name="my_test"></param>
        /// <returns>true if test result make sense </returns>
        private bool ResultMeetingCriteria(Test my_test)
        {
            int count = 0;
            if (my_test.TestDistance == true) count++;
            if (my_test.TestMirrors == true) count++;
            if (my_test.TestVinker == true) count++;
            if (my_test.TestReverseParking == true) count++;
            if (my_test.TestMerge == true) count++;
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


        /// <summary>
        /// enonymous function to check if the trainee trying to sign twice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vType"></param>
        /// <param name="gType"></param>
        /// <returns>true if he does</returns>
        private bool TraineeTryingToSignTwice(string id,Vehicle vehicle,Gear gear) => dal.GetTestsList()
                    .Any(x => x.TraineeId == id && GetTraineeById(id).LicenseType.Any(y => y.Gear == gear && y.VehicleType == vehicle) && x.TestResult == null);


        /// <summary>
        /// calculating function to calculate the id recieving and checking if id legall
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true for legall id</returns>
        public bool CheckIdValidation(string id)
        {
            char[] digits = id.PadLeft(9, '0').ToCharArray();
            int[] oneTwo = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int[] multiply = new int[9];
            int[] oneDigit = new int[9];
            for (int i = 0; i < 9; i++)
                multiply[i] = Convert.ToInt32(digits[i].ToString()) * oneTwo[i];
            for (int i = 0; i < 9; i++)
                oneDigit[i] = (int)(multiply[i] / 10) + multiply[i] % 10;
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += oneDigit[i];
            if (sum % 10 != 0)
            {
                throw new Exception("illigal ID, try again\n");
            }
            return true;
        }

        public bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }



        #region delegates

        //----------------------------------------------------
        //bool functions that make list from test filling the condition with delegate
        private delegate bool listOfDelegate(Test myTest);
        private List<Test> MyList(listOfDelegate some)
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

        private bool TraineePass(Test myTest)
        {
            if (myTest.TestResult == true)
            {
                return true;
            }
            return false;
        }
        private bool Motorcycle(Test myTest)
        {
            if (myTest.VehicleType == Vehicle.motorcycle)
            {
                return true;
            }
            return false;
        }
        private bool SuccessInParking(Test myTest)
        {
            if (myTest.TestReverseParking == true)
            {
                return true;
            }
            return false;
        }
        private bool PassedForThisType(string id, Vehicle vehicle, Gear gear)
        {
            var conditionCheck = (from test in dal.GetTestsList()
                                  let x= GetTraineeById(id)
                                  where test.TraineeId == id && x.LicenseType.Any(Type => //same id
                     (( test.VehicleType == Type.VehicleType &&  //case a: same vehicle and (same gear or manual gear)
                      (test.Gear == Type.Gear || test.Gear == Gear.manual ))|| 
                       ((test.VehicleType == Vehicle.maxTrailer || test.VehicleType == Vehicle.midTrailer) && Type.VehicleType == Vehicle.privateCar)) //case b: he has track license and want to test for private
                       && test.TestResult == true)//pass the test
                                  select test).Count();
            return conditionCheck == 0 ? false : throw new Exception("dear trainee: you have already driver license for this type of test\n");
        }
        //---------------------------------------------------
        #endregion



        /// <summary>
        /// enonymous function to count the num of test that the trainee did
        /// </summary>
        /// <param name="myTrainee"></param>
        /// <returns>sum of trainee tests</returns>
        private int NumberOfTest(Trainee myTrainee) => dal.GetTestsList().Count(item => item.TraineeId == myTrainee.ID);


       

        /// <summary>
        /// make a list from the tests on the expected day
        /// </summary>
        /// <param name="expectedDay"></param>
        /// <returns>list from all the test on this day</returns>
        private List<Test> GetListForExpectedDay(DayOfWeek expectedDay) => dal.GetTestsList().Where(test => test.TestDateAndTime.DayOfWeek == expectedDay).ToList();


        /// <summary>
        /// get num of trainee test for this vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vType"></param>
        /// <param name="gType"></param>
        /// <returns>sum of all this specific tests</returns>
        private int GetNumOfTraineeTests(string id, Vehicle vType, Gear gType) => dal.GetTestsList()
            .Where(x => x.TraineeId == id && x.VehicleType == vType && x.Gear == gType).Count();
    }

}
