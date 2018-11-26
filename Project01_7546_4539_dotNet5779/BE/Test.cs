using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        int test_id;
        int test_tester_id;
        int test_trainee_id;
        DateTime test_date;
        DateTime test_date_and_time;
        string test_adress;
        //bool kriterions
        bool test_distance;
        bool test_reverse_parking;
        bool test_mirrors;
        bool test_vinker;
        // */
        bool test_result;
        string test_comment;

        public int Test_id { get => test_id; set => test_id = value; }
        public int Test_tester_id { get => test_tester_id; set => test_tester_id = value; }
        public int Test_trainee_id { get => test_trainee_id; set => test_trainee_id = value; }
        public DateTime Test_date { get => test_date; set => test_date = value; }
        public DateTime Test_date_and_time { get => test_date_and_time; set => test_date_and_time = value; }
        public string Test_adress { get => test_adress; set => test_adress = value; }
        public bool Test_distance { get => test_distance; set => test_distance = value; }
        public bool Test_reverse_parking { get => test_reverse_parking; set => test_reverse_parking = value; }
        public bool Test_mirrors { get => test_mirrors; set => test_mirrors = value; }
        public bool Test_vinker { get => test_vinker; set => test_vinker = value; }
        public bool Test_result { get => test_result; set => test_result = value; }
        public string Test_comment { get => test_comment; set => test_comment = value; }
        //ToString








    }
}
