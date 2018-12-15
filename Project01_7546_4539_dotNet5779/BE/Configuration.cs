using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        static int min_lesson_num;
        static int max_tester_age;
        static int min_student_age;
        static int min_tests_distance;
        static int courent_test_id = 00000001;

        //properties
        public static int Min_lesson_num { get => min_lesson_num; set => min_lesson_num = value; }
        public static int Max_tester_age { get => max_tester_age; set => max_tester_age = value; }
        public static int Min_student_age { get => min_student_age; set => min_student_age = value; }
        public static int Min_tests_distance { get => min_tests_distance; set => min_tests_distance = value; }
        public static int Courent_test_id { get => courent_test_id; set => courent_test_id = value; }
    }
}
