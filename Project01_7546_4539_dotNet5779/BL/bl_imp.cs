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
using System.Net;
using System.Xml;
using System.Windows;
using System.IO;

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
                dal.AddTest(my_test);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public List<Tester> RelevantTesters(Test my_test)
        {
            DateTime dateTime = new DateTime();
            dateTime = my_test.TestDateAndTime;
            int[] hours = new int[6];
            for (int i = 0; i < 6; i++)
            {

                hours[i] = 9 + i;
            }

            var relevantTesters = (from tester in dal.GetTestersList()
                    let listOfRelevantTesters = GetListByVehicleType(my_test) //get a list of tester match the condition to test 
                    from item in listOfRelevantTesters //testers fills the pre condition
                    from someHour in hours//check for every hour in this day maybe the tester will free at the next hour
                    where FreeTester(item, my_test.TestDateAndTime.AddHours(someHour)) &&
                          FreeTesterAtThisWeek(item, my_test.TestDateAndTime.AddHours(someHour)) //now find free tester
                    select item
                ).ToList();
            return relevantTesters;
        }

        public bool TraineeConditionsForTest(string id, Vehicle vehicle, Gear gear)
        {
            return !PassedForThisType(id, vehicle, gear) &&
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
        /// <returns>true for available</returns>r
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
        public IEnumerable<IGrouping<string, Trainee>> GroupTraineesByTeacher(bool toSort = false)
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
        public IEnumerable<IGrouping<Result?, Test>> GroupTestByResult(bool toSort = false)
        {

            IEnumerable<IGrouping<Result?, Test>> result =
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
        public Test GetTestsById(string id) => dal.GetTestsList().Find(test => test.TraineeId == id);



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
        public bool DeservingToLicense(Trainee myTrainee) => dal.GetTestsList().Any(item => item.TraineeId == myTrainee.ID && item.TestResult == Result.pass);


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
        public bool NotExistTestIn7Days(Test myTest)
        {
            var v = (from item in GetTestsList()
                    where item.TraineeId == myTest.TraineeId &&  item.Gear == myTest.Gear && item.VehicleType == myTest.VehicleType && (myTest.TestDateAndTime - item.TestDateAndTime.AddMinutes(1)).Days < 7// adding minute to round the lossing miliseconds at running time
                    select item).FirstOrDefault();
            return v == null ? true : //false; 
                throw new Exception("ERROR:\n" +
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
                if (property.GetValue(my_test, null) == null) throw new Exception("ERROR: you need to fill all fields of test");
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
            if (my_test.TestDistance == Result.pass) count++;
            if (my_test.TestMirrors == Result.pass) count++;
            if (my_test.TestVinker == Result.pass) count++;
            if (my_test.TestReverseParking == Result.pass) count++;
            if (my_test.TestMerge == Result.pass) count++;
            if (count < 4)
            {
                if (my_test.TestResult == Result.pass)
                {
                    throw new Exception("WARNING: the trainee failed in more then one condition, please consider your result\n");
                }
            }
            else
            {
                if (my_test.TestResult == Result.failed)
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
        private bool TraineeTryingToSignTwice(string id,Vehicle? vehicle,Gear? gear) => dal.GetTestsList()
                    .Any(x => x.TraineeId == id && x.VehicleType == vehicle  && x.Gear == gear && x.TestResult == Result.noGrade);


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
            return Regex.IsMatch(s, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }

        public bool IsValidNumber(string s)
        {
            return Regex.IsMatch(s, "[^0-9]+");
        }
        public bool IsValidAlphabetic(string s)
        {
            return Regex.IsMatch(s, "^[a-zA-Zא-ת ]");
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
            if (myTest.TestResult == Result.pass)
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
            if (myTest.TestReverseParking == Result.pass)
            {
                return true;
            }
            return false;
        }
        private bool PassedForThisType(string id, Vehicle? vehicle, Gear? gear)
        {
            var conditionCheck = (from test in dal.GetTestsList()
                                  let x= GetTraineeById(id)
                                  where test.TraineeId == id && x.LicenseType.Any(Type => //same id
                     (( test.VehicleType == Type.VehicleType &&  //case a: same vehicle and (same gear or manual gear)
                      (test.Gear == Type.Gear || test.Gear == Gear.manual ))|| 
                       ((test.VehicleType == Vehicle.maxTrailer || test.VehicleType == Vehicle.midTrailer) && Type.VehicleType == Vehicle.privateCar)) //case b: he has track license and want to test for private
                       && test.TestResult == Result.pass)//pass the test
                                  select test).Count();
            return conditionCheck == 0 ? false : true;
                //throw new Exception("dear trainee: you have already driver license for this type of test\n");
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





        #region Distance Calculation:

        private int MapQuestAPIFunc(string origin = "גולומב 3 ירושלים", string destination = "ברוך דובדבני 40 ירושלים", string KEY = "8p4iUHOOoJ5CPm6P3G36a3GvbuZyhgfr")
        {
            try
            {
                //MessageBox.Show(origin + "\n" + destination);
                double distInMiles = -1;
                string url = @"https://www.mapquestapi.com/directions/v2/route" +
                             @"?key=" + KEY +
                             @"&from=" + origin +
                             @"&to=" + destination +
                             @"&outFormat=xml" +
                             @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
                             @"&enhancedNarrative=false&avoidTimedConditions=false";
                //request from MapQuest service the distance between the 2 addresses
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();
                //the response is given in an XML format
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(responsereader);
                if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
                    //we have the expected answer
                {
                    //display the returned distance
                    XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                    distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                    //MessageBox.Show("Distance In KM: " + distInMiles * 1.609344);
                    //display the returned driving time
                    XmlNodeList formattedTime = xmldoc.GetElementsByTagName("formattedTime");
                    string fTime = formattedTime[0].ChildNodes[0].InnerText;
                    //MessageBox.Show("Driving Time: " + fTime);
                }
                else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
                    //we have an answer that an error occurred, one of the addresses is not found
                {
                    throw new Exception("an error occurred, one of the addresses is not found. try again.");
                }
                else //busy network or other error...
                {
                    throw new Exception("We have'nt got an answer, maybe the net is busy...");
                }
                MessageBox.Show("Distance In KM: " + distInMiles * 1.609344);
                return distInMiles == -1 ? 100000 : (int) (distInMiles * 1.609344);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// list from tester at trainee area
        /// </summary>
        /// <param name="copyTesters"></param>
        /// <param name="myAddress"></param>
        /// <returns>relvant tester list</returns>
        public List<Tester> GetListOfTestersAtTraineeArea(List<Tester> relevantTesters, string testAddress)//חייב לתקן שיחפש מרחק נכון
        {
            try
            {
                //MessageBox.Show(MapQuestAPIFunc().ToString());
                return relevantTesters.Where(item => MapQuestAPIFunc(item.Address.StreetName, testAddress) <= item.MaxDistance).ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }
       
        #endregion

    }

}
