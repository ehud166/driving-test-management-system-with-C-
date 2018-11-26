using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface idal
    {
        //tester 
        void AddTester(Tester my_tester);
        void RemoveTester(Tester my_tester);
        void UpdateTester(Tester my_tester);

        //trainee
        void AddTrainee(Trainee my_trainee);
        void RemoveTrainee(Trainee my_trainee);
        void UpdateTrainee(Trainee my_trainee);

        //test
        void AddTest(Test my_test);
        void UpdateTest(Test my_test);

        //string getters list
        string GetTestersList();
        string GetTraineeList();
        string GetTestsList();

    }
}
