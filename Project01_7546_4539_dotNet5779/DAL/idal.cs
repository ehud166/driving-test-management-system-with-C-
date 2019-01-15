using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
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
        void UpdateTestInfo(Test my_test);

        /// <summary>
        /// remove test from dal.tests list if test are filling the conditions
        /// </summary>
        /// <param name="my_test"></param>
        void RemoveTest(Test my_test);

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

   

    }
}
