using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {

        //properties
        public static int Min_lesson_num { get; set; }
        public static int Max_tester_age { get; set; }
        public static int Min_student_age { get; set; }
        public static int Min_tests_distance { get; set; }
        public static int Courent_test_id { get; set; } = 00000001;

   
        // public static string MasterPassword = "1234";
            public static string TesterPassword = "123";
            public const int WorkDays = 5;
            public const int WorkHours = 6;
            public const int StartHour = 9;
            public const int EndHour = 14;
            public const int MinLessons = 20;
            public const int TraineeMinAge = 18;
            public const int TesterMinAge = 40;
            public const int MinDaysBetweenTests = 7;
            public static int TestId = 10000000;

        /*-----------------------------------------------------
        detailes:
        manager: ID:000000000 password:1234
        -----------------------------------------------------*/
    }
}

