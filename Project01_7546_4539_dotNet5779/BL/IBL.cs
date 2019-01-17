using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using static BE.Enums;


namespace BL
{
    public interface IBL
    {
        //-----------------------------------------------------------------------------------------
        //tester options
        //-----------------------------------------------------------------------------------------
        /// <summary>
        ///add tester to dal.testers list if tester are filling the conditions
        /// </summary>
        /// <param name="my_tester"></param>
        void AddTester(Tester my_tester);


        /// <summary>
        /// remove tester from dal.testers list if exist
        /// </summary>
        /// <param name="id"></param>
        void RemoveTester(string id);


        /// <summary>
        /// tester can edit his detailes
        /// </summary>
        /// <param name="my_tester"></param>
        void UpdateTester(Tester my_tester);


        //-----------------------------------------------------------------------------------------
        //trainee options
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// add trainee to dal.trainees list if trainees are filling the conditions
        /// </summary>
        /// <param name="my_trainee"></param>
        void AddTrainee(Trainee my_trainee);


        /// <summary>
        /// remove trainee from dal.trainees list if trainees are filling the conditions
        /// </summary>
        /// <param name="id"></param>
        void RemoveTrainee(string id);


        /// <summary>
        /// trainee can edit his detailes
        /// </summary>
        /// <param name="my_trainee"></param>
        void UpdateTrainee(Trainee my_trainee);

        //-----------------------------------------------------------------------------------------
        //test options
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// add test to dal.tests list if test are filling the conditions andd adding to the relevant tester in tester.testsList
        /// </summary>
        /// <param name="my_test"></param>
        void AddTest(Test my_test);


        /// <summary>
        /// tester can update the test results
        /// </summary>
        /// <param name="my_test"></param>
        void UpdateTest(Test my_test);

        //-----------------------------------------------------------------------------------------
        //string getters list
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// getter for testersList
        /// </summary>
        /// <returns>list of all testers</returns>
        List<Tester> GetTestersList();


        /// <summary>
        /// getter for traineesList
        /// </summary>
        /// <returns>list of all trainees</returns>
        List<Trainee> GetTraineeList();


        /// <summary>
        /// getter for testsList
        /// </summary>
        /// <returns>list of all tests</returns>
        List<Test> GetTestsList();


        //-----------------------------------------------------------------------------------------
        //grouping lists by keys
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Group testers list by vehicle type
        /// </summary>
        /// <param name="toSort">order to sort the group list</param>
        /// <returns>Ienumarable for the group</returns>
        IEnumerable<IGrouping<Vehicle, Tester>> GroupTestersByVehicle(bool toSort = false);


        /// <summary>
        /// Group testers list by vehicle type
        /// </summary>
        /// <param name="toSort">order to sort the group list</param>
        /// <returns>Ienumarable for the group</returns>
        IEnumerable<IGrouping<string, Trainee>> GroupTraineesBySchool(bool toSort = false);


        /// <summary>
        /// Group trainees list  by tester name
        /// </summary>
        /// <param name="toSort">order to sort the group list</param>
        /// <returns>Ienumarable for the group</returns>
        IEnumerable<IGrouping<string, Trainee>> GroupTraineesByTeacher(bool toSort = false);


        /// <summary>
        /// Group trainees list by gender
        /// </summary>
        /// <param name="toSort">order to sort the group list</param>
        /// <returns>Ienumarable for the group</returns>
        IEnumerable<IGrouping<Gender, Trainee>> GroupTraineesByGender(bool toSort = false);


        /// <summary>
        /// Group tests list by result (pass/failed)
        /// </summary>
        /// <param name="toSort">order to sort the group list</param>
        /// <returns>Ienumarable for the group</returns>
        IEnumerable<IGrouping<Result?, Test>> GroupTestByResult(bool toSort = false);


        //adding to IBL to help find existing tester/test/trainee
        //------------------------------------------------------------------------------------
        /// <summary>
        /// find the test id and get the test
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the test with this id</returns>
        Test GetTestsById(string id);

        /// <summary>
        /// find the test Trainee id and get the test
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the test with this trainee id</returns>
        List<Test> GetTestsByTraineeId(string id);

        /// <summary>
        /// find the tester id and get the tester
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the tester with this id</returns>
        Tester GetTesterById(string id);

        bool DeservingToLicense(Trainee myTrainee);


        /// <summary>
        /// find the trainee id and get the trainee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the trainee with this id</returns>
        Trainee GetTraineeById(string id);

        Manager GetManagerById(string id);


        List<Tester> GetListOfTestersAtTraineeArea(List<Tester> relevantTesters, string testAddress);
        bool CheckIdValidation(string id);
        bool IsValidEmailAddress(string s);
        bool IsValidNumber(string s);
        bool IsValidAlphabetic(string s);
        bool TraineeHave20Lessons(String id);
        bool NotExistTestIn7Days(Test myTest);
        bool TraineeConditionsForTest(string id, Vehicle vehicle, Gear gear);
        List<Tester> RelevantTesters(Test my_test);
        bool FreeTester(Tester optionalTester, DateTime checkDateTime);
        //------------------------------------------------------------------------------------
    }
}
