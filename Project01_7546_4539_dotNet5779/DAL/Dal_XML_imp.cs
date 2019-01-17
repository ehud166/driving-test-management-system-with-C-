using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using static BE.Enums;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        protected static Dal_XML_imp instance = null;
        public static IDAL GetDal()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;
        }


        public static int Courent_test_id { get; set; }


        //list Path
        string testPath = @"testXml.xml";
        string testerPath = @"testerXml.xml";
        string traineePath = @"traineeXml.xml";
        string configPath = @"configXml.xml";


        private XElement testerRoot;
        private XElement configRoot;

        List<Trainee> traineesList = new List<Trainee>();
        List<Test> testsList = new List<Test>();

        XmlSerializer TraineesSerializer = new XmlSerializer(typeof(List<Trainee>));
        XmlSerializer TestsSerializer = new XmlSerializer(typeof(List<Test>));

        protected Dal_XML_imp()
        {
           
            try
            {
                if (!File.Exists(testPath) || !File.Exists(testerPath) || !File.Exists(traineePath) || !File.Exists(configPath))
                    CreateFiles();
                if (File.Exists(testPath) || File.Exists(testerPath) || File.Exists(traineePath) || File.Exists(configPath))
                    LoadData();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void CreateFiles()//if a file does not exist the function will create it
        {
            if (!File.Exists(configPath))
            {
                configRoot = new XElement("config");
                configRoot.Add(new XElement("testNumber", 00000001)); // we will save the first contract number to the file, ready for the first contract...
                configRoot.Save(configPath);
            }
            if (!File.Exists(testerPath))
            {
                testerRoot = new XElement("tester"); // create a children file
                testerRoot.Save(testerPath);
            }


            if ((!File.Exists(testPath) && testsList != null) || File.Exists(testPath))
            {
                File.WriteAllText(testPath, "");
                using (StreamWriter writer = File.AppendText(testPath))
                {
                    TestsSerializer.Serialize(writer, testsList);
                }
            }
            if ((!File.Exists(traineePath) && traineesList != null) || File.Exists(traineePath))
            {
                File.WriteAllText(traineePath, "");
                using (StreamWriter writer = File.AppendText(traineePath))
                {
                    TraineesSerializer.Serialize(writer, traineesList);
                }
            }
        }

        private void LoadData()//load xml file to lists
        {
            try
            {
                if (File.Exists(configPath))
                {
                    configRoot = XElement.Load(configPath);

                    Courent_test_id = int.Parse(configRoot.Element("testNumber").Value);
                }
                if (File.Exists(testerPath))
                {
                    testerRoot = XElement.Load(testerPath);
                }

                if (File.Exists(traineePath))
                {
                    using (StreamReader reader = new StreamReader(traineePath))
                    {
                        traineesList= (List<Trainee>)TraineesSerializer.Deserialize(reader);
                    }
                }
                if (File.Exists(testPath))
                {
                    using (StreamReader reader = new StreamReader(testPath))
                    {
                        testsList = (List<Test>)TestsSerializer.Deserialize(reader);
                    }
                }
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }


        private void updateConfigXML()
        {
            configRoot = new XElement("config");
            configRoot.Add(new XElement("testNumber", Courent_test_id));
            configRoot.Save(configPath);
        }


        #region Test

        

        private void updateTestXML()
        {
            File.WriteAllText(testPath, "");
            using (StreamWriter writer = File.AppendText(testPath))
            {
                TestsSerializer.Serialize(writer, testsList);
            }
        }


        public void AddTest(Test my_test)
        {
            if ((testsList.FirstOrDefault(m => m.ID == my_test.ID)) != null)  //if Test with the same id already exists
                throw new Exception("test with the same id already exists...");
            my_test.ID = Courent_test_id.ToString().PadLeft(9 - Courent_test_id.ToString().Length, '0');
            Courent_test_id++;
            updateConfigXML();//update to running number in xml config file
            testsList.Add(my_test);
            updateTestXML();
        }

        public void UpdateTest(Test my_test)
        {
            Test s = testsList.FirstOrDefault(x => x.ID == my_test.ID);
            if (s == null)//if Test with the same id not found
                throw new Exception("test with the same id not found...");
            testsList.Remove(s);//removing old data
            testsList.Add(my_test);//add the updated data
            updateTestXML();
        }

        public void RemoveTest(Test my_test)
        {
            if ((testsList.FirstOrDefault(s => s.ID == my_test.ID)) == null)//if Test with the same id not found
                throw new Exception("mother with the same id not found...");
            testsList.Remove(my_test);
            updateTestXML();
        }

        #endregion

        #region Trainee

        private void updateTraineeXML()
        {
            File.WriteAllText(traineePath, "");
            using (StreamWriter writer = File.AppendText(traineePath))
            {
                TraineesSerializer.Serialize(writer, traineesList);
            }
        }

       

        public void AddTrainee(Trainee my_trainee)
        {
            if ((traineesList.FirstOrDefault(x => x.ID == my_trainee.ID)) != null)//if Trainee with the same id already exists 
                throw new Exception("Trainee with the same id already exists...");
            traineesList.Add(my_trainee);
            updateTraineeXML();
        }

        public void RemoveTrainee(string id)
        {
            var v = (traineesList.FirstOrDefault(x => x.ID == id));
            if (v == null)//if Trainee with the same id not found
                throw new Exception("Trainee with the same id not found...");
            traineesList.Remove(v);
            updateTraineeXML();
        }


        public void UpdateTrainee(Trainee my_trainee)
        {
            var v = traineesList.FirstOrDefault(x => x.ID == my_trainee.ID);
            if (v == null)//if Trainee with the same id not found
                throw new Exception("Trainee with the same id not found...");
            traineesList.Remove(v);//removing old data
            traineesList.Add(my_trainee);//add the updated data
            updateTraineeXML();
        }


        #endregion

        #region help2XML
        private Dictionary<DayOfWeek, HoursPerDay> XML2Schedule( string schedule)
        {
            Dictionary<DayOfWeek, HoursPerDay> scheduleFromXML = new Dictionary<DayOfWeek, HoursPerDay>();
            if (schedule != null && schedule.Length > 0)
            {
                string[] values = schedule.Split(',');
                int sizeA = int.Parse(values[0]);
                int sizeB = int.Parse(values[1]);
                int index = 2;
                for (int i = 0; i < sizeA; i++)
                    for (int j = 0; j < sizeB; j++)
                        scheduleFromXML[(DayOfWeek)i][j] = bool.Parse(values[index++]);
            }
            return scheduleFromXML;
        }
        private Address XML2Address( XElement address)
        {
            Address fromXML;
            try
            {
                fromXML = new Address()
                {
                    StreetName = address.Element("StreetName").Value,
                    BuildingNumber = int.Parse(address.Element("BuildingNumber").Value),
                    City = address.Element("StreetName").Value

                };
            }
            catch
            {
                throw new Exception("בעיה בפענוח המסמך");
                //fromXML = null;
            }
            return fromXML;
        }
        private Test XML2Test( XElement test)
        {
            Test fromXML;
            try
            {
                fromXML = new Test()
                {
                    ID = test.Element("ID").Value,
                    TesterId = test.Element("TesterId").Value,
                    TraineeId = test.Element("TraineeId").Value,
                    TestDateAndTime = Convert.ToDateTime(test.Element("TestDateAndTime").Value),
                    TestAddress = new Address(test.Element("Address").Element("StreetName").Value,
                        int.Parse(test.Element("Address").Element("BuildingNumber").Value),
                        test.Element("Address").Element("City").Value),
                    VehicleType = (from type in (Vehicle[]) Enum.GetValues(typeof(Vehicle))
                        where type.ToString() == test.Element("Vehicle").Value
                        select type).FirstOrDefault(),
                    Gear = (from type in (Gear[]) Enum.GetValues(typeof(Gear))
                        where type.ToString() == test.Element("Gear").Value
                        select type).FirstOrDefault(),
                    TestDistance = (from type in (Result[]) Enum.GetValues(typeof(Result))
                        where type.ToString() == test.Element("Result").Value
                        select type).FirstOrDefault(),
                    TestReverseParking = (from type in (Result[]) Enum.GetValues(typeof(Result))
                        where type.ToString() == test.Element("Result").Value
                        select type).FirstOrDefault(),
                    TestMirrors = (from type in (Result[]) Enum.GetValues(typeof(Result))
                        where type.ToString() == test.Element("Result").Value
                        select type).FirstOrDefault(),
                    TestVinker = (from type in (Result[]) Enum.GetValues(typeof(Result))
                        where type.ToString() == test.Element("Result").Value
                        select type).FirstOrDefault(),
                    TestMerge = (from type in (Result[]) Enum.GetValues(typeof(Result))
                        where type.ToString() == test.Element("Result").Value
                        select type).FirstOrDefault(),
                    TestResult = (from type in (Result[]) Enum.GetValues(typeof(Result))
                        where type.ToString() == test.Element("Result").Value
                        select type).FirstOrDefault(),
                    TestComment = test.Element("TestComment").Value
                };

            }
            catch
            {
                throw new Exception("בעיה בפענוח המסמך טסט");
                //fromXML = null;
            }
            return fromXML;
        

        }
        private List<Test> XML2TestList(XElement testList)
        {
            List<Test> fromXML = new List<Test>();
            try
            {
                foreach (var test in testList.Elements())
                {
                    fromXML.Add(XML2Test(test));
                }
            }
            catch
            {
                throw new Exception("בעיה בפענוח הרשימ טסטים של טסטר");
                //fromXML = null;
            }

            return fromXML;
        }


        private string Schedule2XML( Dictionary<DayOfWeek, HoursPerDay> schedule)
        {
            if (schedule == null)
                return null;
            string result = "";
            if (schedule != null)
            {
                int sizeA = 5;
                int sizeB = 6;
                result += "" + sizeA + "," + sizeB;
                for (int i = 0; i < sizeA; i++)
                for (int j = 0; j < sizeB; j++)
                    result += "," + schedule[(DayOfWeek)i][j];
            }
            return result;
        }
        private XElement Test2XML( Test test)
        {
            XElement traineeId = new XElement("TraineeId", test.TraineeId);
            XElement TestDateAndTime = new XElement("TestDateAndTime", test.TestDateAndTime);
            XElement Address = new XElement("Address", Address2XML(test.TestAddress));
            XElement VehicleType = new XElement("Vehicle", test.VehicleType);
            XElement Gear = new XElement("Gear", test.Gear);
            XElement TestComment = new XElement("TestComment", test.TestComment);
            XElement TestDistance = new XElement("TestDistance", test.TestDistance);
            XElement TestReverseParking = new XElement("TestReverseParking", test.TestReverseParking);
            XElement TestMirrors = new XElement("TestMirrors", test.TestMirrors);
            XElement TestMerge = new XElement("TestMerge", test.TestMerge);
            XElement TestVinker = new XElement("TestVinker", test.TestVinker);
            XElement TestResult = new XElement("TestResult", test.TestResult);
            XElement TesterId = new XElement("TesterId", test.TesterId);
            XElement ID = new XElement("ID", test.ID);
            return new XElement("Test", traineeId, TestDateAndTime, Address, VehicleType, Gear, TestComment,
                TestDistance, TestReverseParking, TestMirrors, TestMerge, TestVinker, TestResult, TesterId, ID);

        }
        private XElement Address2XML( Address address)
        {
            XElement streetName = new XElement("StreetName", address.StreetName);
            XElement buildingNumber = new XElement("BuildingNumber", address.BuildingNumber);
            XElement city = new XElement("City", address.City);
            return new XElement("Address", streetName, buildingNumber, city);


        }
        private XElement TestList2XML(List<Test> testList)
        {
            XElement listToAdd = new XElement("TesterTests");
            foreach (var test in testList)
            {
                listToAdd.Add(Test2XML(test));
            }

            return listToAdd;
        }
        #endregion

        //private void Schedule
        #region Tester

        public void AddTester(Tester my_tester)
        {
            XElement testerToAdd;
            try
            {
                testerToAdd = (from p in testerRoot.Elements()
                    where p.Element("Id").Value == my_tester.ID
                    select p).FirstOrDefault();
            }
            catch (Exception e)
            {
                testerToAdd = null;
            }
            if (testerToAdd != null) //duplicate id's are not permitted
                throw new Exception("טסטר קיים");
            XElement id = new XElement("Id", my_tester.ID);
            XElement firstName = new XElement("FirstName", my_tester.FirstName);
            XElement lastName = new XElement("LastName", my_tester.LastName);
            XElement birthday = new XElement("Birthday", my_tester.Birthday);
            XElement gender = new XElement("Gender", my_tester.Gender);
            XElement phoneAreaCode = new XElement("PhoneAreaCode", my_tester.PhoneAreaCode);
            XElement phoneNumber = new XElement("PhoneNumber", my_tester.PhoneNumber);
            XElement address = new XElement("Address", Address2XML(my_tester.Address));
            XElement email = new XElement("Email", my_tester.Email);
            XElement password = new XElement("Password", my_tester.Password);
            XElement vehicleType = new XElement("VehicleType", my_tester.VehicleType);
            XElement seniority = new XElement("Seniority", my_tester.Seniority);
            XElement maxTestsForWeek = new XElement("MaxTestsForWeek", my_tester.MaxTestsForWeek);
            XElement maxDistance = new XElement("MaxDistance", my_tester.MaxDistance);
            XElement schedule = new XElement("Schedule", Schedule2XML(my_tester.Schedule.WeekDays));
            XElement testerTests = new XElement("TesterTests", TestList2XML(my_tester.TesterTests));

            XElement newTester = new XElement("Tester",id,firstName,lastName,birthday,gender,phoneAreaCode,phoneNumber,address,email,password,vehicleType,seniority,
            maxTestsForWeek, maxDistance, schedule, testerTests);
            testerRoot.Add(newTester);
            testerRoot.Save(testerPath);
        }
        public void RemoveTester(string id)
        {
            var toRemove = (from tester in testerRoot.Elements()
                where tester.Element("id").Value == id
                select tester).FirstOrDefault();
            toRemove.Remove();
            testerRoot.Save(testerPath);
        }
        public void UpdateTester(Tester my_tester)
        {
            XElement testerToUpdate;
            try
            {
                testerToUpdate = (from p in testerRoot.Elements()
                               where p.Element("Id").Value == my_tester.ID
                               select p).FirstOrDefault();
            }
            catch (Exception e)
            {
                testerToUpdate = null;
            }
            if (testerToUpdate != null) //duplicate id's are not permitted
                throw new Exception("טסטר קיים");
            XElement id = new XElement("Id", my_tester.ID);
            XElement firstName = new XElement("FirstName", my_tester.FirstName);
            XElement lastName = new XElement("LastName", my_tester.LastName);
            XElement birthday = new XElement("Birthday", my_tester.Birthday);
            XElement gender = new XElement("Gender", my_tester.Gender);
            XElement phoneAreaCode = new XElement("PhoneAreaCode", my_tester.PhoneAreaCode);
            XElement phoneNumber = new XElement("PhoneNumber", my_tester.PhoneNumber);
            XElement address = new XElement("Address", Address2XML(my_tester.Address));
            XElement email = new XElement("Email", my_tester.Email);
            XElement password = new XElement("Password", my_tester.Password);
            XElement vehicleType = new XElement("VehicleType", my_tester.VehicleType);
            XElement seniority = new XElement("Seniority", my_tester.Seniority);
            XElement maxTestsForWeek = new XElement("MaxTestsForWeek", my_tester.MaxTestsForWeek);
            XElement maxDistance = new XElement("MaxDistance", my_tester.MaxDistance);
            XElement schedule = new XElement("Schedule", Schedule2XML(my_tester.Schedule.WeekDays));
            XElement testerTests = new XElement("TesterTests", TestList2XML(my_tester.TesterTests));

            XElement newTester = new XElement("Tester", id, firstName, lastName, birthday, gender, phoneAreaCode, phoneNumber, address, email, password, vehicleType, seniority,
            maxTestsForWeek, maxDistance, schedule, testerTests);
            testerRoot.Save(testerPath);
        }
        #endregion


        #region geterrs
        public List<Test> GetTestsList()
        {
            return testsList;
        }
        public List<Trainee> GetTraineeList()
        {
            return traineesList;
        }
        public List<Tester> GetTestersList()
        {
            List<Tester> testerList = new List<Tester>();
            LoadData();
            try
            {
                foreach (XElement my_tester in testerRoot.Elements())
                {
                    Tester t = new Tester();

                    t.ID = my_tester.Element("ID").Value;
                    t.FirstName = my_tester.Element("FirstName").Value;
                    t.LastName = my_tester.Element("LastName").Value;
                    t.Birthday = DateTime.Parse(my_tester.Element("Birthday").Value);
                    t.Gender = (from type in (Gender[]) Enum.GetValues(typeof(Gender))
                        where type.ToString() == my_tester.Element("Gender").Value
                        select type).FirstOrDefault();
                    t.PhoneAreaCode = my_tester.Element("PhoneAreaCode").Value;
                    t.PhoneNumber = my_tester.Element("PhoneNumber").Value;
                    t.Address = new Address(my_tester.Element("Address").Element("StreetName").Value,
                        int.Parse(my_tester.Element("Address").Element("BuildingNumber").Value),
                        my_tester.Element("Address").Element("City").Value);
                    t.Email = my_tester.Element("Email").Value;
                    t.Password = my_tester.Element("Password").Value;
                    t.VehicleType = (from type in (Vehicle[]) Enum.GetValues(typeof(Vehicle))
                        where type.ToString() == my_tester.Element("Vehicle").Value
                        select type).FirstOrDefault();
                    t.Seniority = int.Parse(my_tester.Element("Seniority").Value);
                    t.MaxTestsForWeek = int.Parse(my_tester.Element("MaxTestsForWeek").Value);
                    t.MaxDistance = int.Parse(my_tester.Element("MaxDistance").Value);
                    t.Schedule = new Schedule();
                    t.Schedule.WeekDays = XML2Schedule(my_tester.Element("Schedule").Value);
                    t.TesterTests = new List<Test>();
                    t.TesterTests = XML2TestList(my_tester.Element("TesterTests"));
                    testerList.Add(t);
                }
            }
            catch
            {
                testerList = null;
            }
            return testerList;
        }
        #endregion





















    }
}
