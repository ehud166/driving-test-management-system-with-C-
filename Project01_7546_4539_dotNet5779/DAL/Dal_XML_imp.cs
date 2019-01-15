using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
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

        public Dal_XML_imp()
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

        public void UpdateTestInfo(Test my_test)
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


        public void UpdateTraineeInfo(Trainee my_trainee)
        {
            var v = traineesList.FirstOrDefault(x => x.ID == my_trainee.ID);
            if (v == null)//if Trainee with the same id not found
                throw new Exception("Trainee with the same id not found...");
            traineesList.Remove(v);//removing old data
            traineesList.Add(my_trainee);//add the updated data
            updateTraineeXML();
        }


        #endregion

        #region Tester

        public void AddTester(Tester my_tester)
        {
            XElement testerToAdd;
            try
            {
                testerToAdd = (from p in testerRoot.Elements()
                    where p.Element("id").Value == my_tester.ID
                    select p).FirstOrDefault();
            }
            catch (Exception e)
            {
                testerToAdd = null;
            }
            if (testerToAdd != null) //duplicate id's are not permitted
                throw new Exception("child  with the same ID already exists...");
            XElement id = new XElement("id", my_tester.ID);
            XElement firstName = new XElement("firstName", my_tester.FirstName);
            XElement lastName = new XElement("lastName", my_tester.LastName);
            XElement birthday = new XElement("lastName", my_tester.LastName);
            XElement gender = new XElement("lastName", my_tester.LastName);
            XElement phoneAreaCode = new XElement("lastName", my_tester.LastName);
            XElement phoneNumber = new XElement("lastName", my_tester.LastName);
            XElement address = new XElement("lastName", my_tester.LastName);
            XElement email = new XElement("lastName", my_tester.LastName);
            XElement password = new XElement("lastName", my_tester.LastName);
            XElement vehicleType = new XElement("lastName", my_tester.LastName);
            XElement seniority = new XElement("lastName", my_tester.LastName);
            XElement maxTestsForWeek = new XElement("lastName", my_tester.LastName);
            XElement maxDistance = new XElement("lastName", my_tester.LastName);
            XElement schedule = new XElement("lastName", my_tester.LastName);
            //XElement testerTests = new XElement();
            
            //foreach (var x in my_tester.TesterTests)

            //{
            //    testerTests.Add(x.);
            //}

            //XElement newTester = new XElement("tester",id, firstName, lastName, birthday, gender, phoneAreaCode, phoneNumber, address, email, password,
            //vehicleType, seniority, maxTestsForWeek, maxDistance, schedule, List<Test> testerTests)

            //List<Tester> copyTesters = new List<Tester>();
            //copyTesters = Testers.Select(x => new Tester(x.ID, x.FirstName, x.LastName, x.Birthday, x.Gender,
            //        x.PhoneAreaCode, x.PhoneNumber,
            //        new Address(x.Address.StreetName, x.Address.BuildingNumber, x.Address.City), x.Email, x.Password,
            //        x.VehicleType,
            //        x.Seniority, x.MaxTestsForWeek, x.MaxDistance, x.Schedule, new List<Test>(x?.TesterTests.Select(y =>
            //            new Test(y.TraineeId, y.TestDateAndTime,
            //                new Address(y.TestAddress.StreetName, y.TestAddress.BuildingNumber, y.TestAddress.City),
            //                y.VehicleType,
            //                y.Gear, y.TestComment, y.TestDistance,
            //                y.TestReverseParking, y.TestMirrors, y.TestMerge, y.TestVinker, y.TestResult, y.TesterId,
            //                y.ID)).ToList())))
            //    .ToList();
            //return copyTesters;
            //testerRoot.Add(newTester);
            //testerRoot.Save(testerPath);




        }


        public void RemoveTester(string id)
        {
            throw new NotImplementedException();
        }


        public void UpdateTester(Tester my_tester)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void UpdateTrainee(Trainee my_trainee)
        {
            throw new NotImplementedException();
        }



        #endregion





















    }
}
