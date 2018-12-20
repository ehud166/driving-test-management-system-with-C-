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
    public class Bl_imp : IBL
    {
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
        }

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
                var q = CheckIdValidation(my_test.TraineeId) && TraineeHave20Lessons(my_test) &&
                        NotExistTestIn7Days(my_test) && !PassedForThisType(my_test) &&
                        !TraineeTryingToSignTwice(my_test.TraineeId, my_test.VehicleType, my_test.Gear);
                if (q)
                {//now the trainee past the conditions and we need to find tester (including check id for trainee but not necessary to check tester because it checked on AddTester)
                    var relevantTester = (from tester in dal.GetTestersList()
                            let listOfRelevantTesters = GetListOfTestersAtTraineeArea(
                                GetListByVehicleType(my_test), my_test.TestAddress) //get a list of tester match the condition to test 
                            from item in listOfRelevantTesters
                            where FreeTester(item, my_test.TestDateAndTime) && FreeTesterAtThisWeek(item,my_test.TestDateAndTime) //now find free tester
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

                        OfferFreeTesterAndTime(listOfRelevantTesters);
                        
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

       
        //--------------------------------------------------------------
        //help functions for check tester condition
        private static bool CheckTesterAge(Tester my_tester)
        {
            DateTime tempAge = new DateTime();
            tempAge = my_tester.Birthday;//to calculate the tester`s age i added 40 years and compare to Now
            tempAge.AddYears(40);
            if (tempAge <= DateTime.Now)
                return true;
            else
                throw new Exception("ERROR:\n" +
                                    "This Tester are too young\n");
        }
        //--------------------------------------------------------------
        //help functions for check trainee condition
        private bool CheckTraineeAge(Trainee my_trainee)
        {
            DateTime tempAge = new DateTime();
            tempAge = my_trainee.Birthday;
            tempAge.AddYears(18);//to calculate the trainee`s age i added 18 years and compare to Now
            if (tempAge <= DateTime.Now)
                return true;
            else
                throw new Exception("ERROR:\n" +
                                    "This Trainee are too young\n");
        }
        //--------------------------------------------------------------
        //help functions for check test condition
        public void OfferFreeTesterAndTime(List<Tester> testersCondition)
        {
            DateTime restartDate = DateTime.Today.AddDays(3).AddHours(9);//DateTime is a value type
            //restart hour for the next round hour
            while (restartDate.Hour < 9 || restartDate.Hour > 14 || restartDate <= DateTime.Now)
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
                dal.UpdateTest(my_test);
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

        private bool DatesAreInTheSameWeek(DateTime date1, DateTime date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

            return d1 == d2;
        }


        private bool FreeTesterAtThisWeek(Tester optionalTester, DateTime t)
        {
            var x = (from test in optionalTester.TesterTests
                where DatesAreInTheSameWeek(test.TestDateAndTime, t)
                select test).Count();
            return x < optionalTester.MaxTestsForWeek;
        }

        private List<Tester> GetListOfTestersAtTraineeArea(List<Tester> copyTesters, Address myAddress)
        {
            return copyTesters.Where(item => Math.Abs(item.Address.TemporaryCoordinate-myAddress.TemporaryCoordinate) <= item.MaxDistance).ToList();
        }
        private List<Tester> GetListByVehicleType(Test my_test)
        {
            return GetTestersList().Where(item => item.VehicleType == my_test.VehicleType).ToList();
        }



        private bool TraineeHave20Lessons(Test my_test)
        {
            var v = (from item in GetTraineeList()
                                  where item.ID == my_test.TraineeId && item.LessonNum >= 20 && item.VehicleType == my_test.VehicleType
                                  select item).FirstOrDefault();
            return v != null ? true: throw new Exception("ERROR:\n" +
                                    "This trainee have not study 20 lesson yet for this vehicle type\n");
            
        }

        private bool NotExistTestIn7Days(Test my_test)
        {
            var v = (from item in GetTestsList()
                    where item.TraineeId == my_test.TraineeId && (item.TestDateAndTime.AddMinutes(1) - DateTime.Now).Days < 7// adding minute to round the lossing miliseconds at running time
                    select item).FirstOrDefault();
            return v==null? true : throw new Exception("ERROR:\n" +
                                             "This Test are too early (haven't pass 7 days from this trainee last test\n");
        }



        private bool TheTesterFillAll(Test my_test)
        {
            foreach (var property in my_test.GetType().GetProperties())
            {
                if (property.GetValue(my_test,null) == null) throw new Exception("ERROR: you need to fill all fields of test");
            }

            return true;
        }

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

        private bool PassedForThisType(Test myTest)
        {
            var conditionCheck = (from test in dal.GetTestsList()
                where test.TraineeId == myTest.TraineeId && //same id
                     (( test.VehicleType == myTest.VehicleType &&  //case a: same vehicle and (same gear or manual gear)
                      (test.Gear == myTest.Gear || test.Gear == Gear.manual ))|| 
                       ((test.VehicleType == Vehicle.maxTrailer || test.VehicleType == Vehicle.midTrailer) && myTest.VehicleType == Vehicle.privateCar)) //case b: he has track license and want to test for private
                       && test.TestResult == true//pass the test
                                  select test).Count();
            return conditionCheck == 0 ? false : throw new Exception("dear trainee: you have already driver license for this type of test\n");
        }

        private int NumberOfTest(Trainee myTrainee) => dal.GetTestsList().Count(item => item.TraineeId == myTrainee.ID);

        private bool DeservingToLicense(Trainee myTrainee)
        {var  v = dal.GetTestsList().Where(item => item.TraineeId == myTrainee.ID && item.TestResult == true).FirstOrDefault();
            return v != null? true: false;
        }
        // לטיפול       private List<Test> GetListForExpectedDay(DayOfWeek expectedDay) => dal.GetTestsList().Where(test => test.TestDateAndTime.Day == expectedDay).ToList();

        private Test GetTestById(string id) => dal.GetTestsList().Where(test => test.ID == id).FirstOrDefault();

        private int GetNumOfTraineeTests(string id, Vehicle vType, Gear gType) => dal.GetTestsList()
            .Where(x => x.TraineeId == id && x.VehicleType == vType && x.Gear == gType).Count();
        private bool TraineeTryingToSignTwice(string id, Vehicle vType, Gear gType) => dal.GetTestsList()
            .Where(x => x.TraineeId == id && x.VehicleType == vType && x.Gear == gType && x.TestResult == null).Count() > 0;
        private bool CheckIdValidation(string id)
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
    }

}
