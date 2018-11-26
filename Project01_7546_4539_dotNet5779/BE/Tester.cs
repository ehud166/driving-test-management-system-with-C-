using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester
    {
        int tester_id;
        string tester_Lname;
        string tester_Fname;
        DateTime tester_birthday;
        string tester_gender;
        int tester_phone;
        string tester_adress;
        int tester_seniority;
        int tester_max_tests_for_week;
        string tester_vehicle_type;
        bool[,] tester_scedule = new bool[5, 6];
        double tester_max_distance;
        public override string ToString()
        {
            return "tester: " +
                "\nid: " + tester_id.ToString() +
                "\nlast name: " + tester_Lname +
                "\nfirst name: " + tester_Fname +
                "\ndate of birth" + tester_birthday.ToString() +
                "\ngender: " + tester_gender +
                "\nphone: " + tester_phone.ToString() +
                "\nadress: " + tester_adress +
                "\nseniority: " + tester_seniority.ToString() +
                "\nmax tests for week: " + tester_max_tests_for_week.ToString() +
                "\nvehicle type: " + tester_vehicle_type +
                "\n scedule: " + tester_scedule.ToString() +//need checking
                "\nmax distance: " + tester_max_distance.ToString();
        }
    }
}
