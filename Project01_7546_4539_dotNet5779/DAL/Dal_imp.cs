using BE;
using static BE.Configuration;
using static DS.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Dal_imp : idal
    {
        public void AddTest(Test my_test)
        {
           // my_test.Test_id = courent_test_id++;
            Tests.Add(my_test);
        }

        public void AddTester(Tester my_tester)
        {
            throw new NotImplementedException();
        }

        public void AddTrainee(Trainee my_trainee)
        {
            throw new NotImplementedException();
        }

        public string GetTestersList()
        {
            throw new NotImplementedException();
        }

        public string GetTestsList()
        {
            throw new NotImplementedException();
        }

        public string GetTraineeList()
        {
            throw new NotImplementedException();
        }

        public void RemoveTester(Tester my_tester)
        {
            throw new NotImplementedException();
        }

        public void RemoveTrainee(Trainee my_trainee)
        {
            throw new NotImplementedException();
        }

        public void UpdateTest(Test my_test)
        {
            throw new NotImplementedException();
        }

        public void UpdateTester(Tester my_tester)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrainee(Trainee my_trainee)
        {
            throw new NotImplementedException();
        }
    }
}
