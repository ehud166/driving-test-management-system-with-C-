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
        //tester 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="my_tester"></param>
        void AddTester(Tester my_tester);
        void RemoveTester(string id);
        void UpdateTester(Tester my_tester);

        //trainee
        void AddTrainee(Trainee my_trainee);
        void RemoveTrainee(string id);
        void UpdateTrainee(Trainee my_trainee);

        //test
        void AddTest(Test my_test);
        void UpdateTest(Test my_test);

        //string getters list

        List<Tester> GetTestersList();
        List<Trainee> GetTraineeList();
        List<Test> GetTestsList();

    }
}
