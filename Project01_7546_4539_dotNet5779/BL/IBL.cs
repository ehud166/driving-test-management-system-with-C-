using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public interface IBL
    {
        //tester 
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

        IEnumerable<IGrouping<Vehicle, Tester>> GroupTestersByVehicle(bool toSort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupTraineesBySchool(bool toSort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupTraineesByTester(bool toSort = false);
        IEnumerable<IGrouping<int, Trainee>> GroupTraineesByNumOfTests(bool toSort = false);
        IEnumerable<IGrouping<bool?, Test>> GroupTestByResult(bool toSort = false);

    }
}
