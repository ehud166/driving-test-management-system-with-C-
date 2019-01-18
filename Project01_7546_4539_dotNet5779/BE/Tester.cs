using static BE.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Enums;

namespace BE
{
    public class Tester : Person
    {
        //c-tor
        public Tester(string id, string firstName, string lastName, DateTime birthday, Gender gender, string phoneAreaCode, string phoneNumber, Address address, string email, string password,
            Vehicle vehicleType, int seniority, int maxTestsForWeek, double maxDistance, Schedule schedule, List<Test> testerTests)
            : base(id, firstName, lastName, birthday, gender, phoneAreaCode, phoneNumber, address, email, password)
        {
            VehicleType = vehicleType;
            Seniority = seniority;
            MaxTestsForWeek = maxTestsForWeek;
            MaxDistance = maxDistance;
            Schedule = schedule;
            TesterTests = testerTests;
        }
        public Tester(string id, string firstName, string lastName, DateTime birthday, Gender gender, string phoneAreaCode, string phoneNumber, Address address, string email, string password,
            Vehicle vehicleType, int seniority, int maxTestsForWeek, double maxDistance)
            : base(id, firstName, lastName, birthday, gender, phoneAreaCode, phoneNumber, address, email, password)
        {
            VehicleType = vehicleType;
            Seniority = seniority;
            MaxTestsForWeek = maxTestsForWeek;
            MaxDistance = maxDistance;
            Schedule = new Schedule();
            TesterTests = new List<Test>();
        }

        public Tester()
        {
            Address = new Address();
            Schedule = new Schedule();
            TesterTests = new List<Test>();
        }

        public Tester(string id) : base(id)
        {
            Address = new Address();
            Schedule = new Schedule();
            TesterTests = new List<Test>();
        }

        //properties
        public Vehicle VehicleType { get; set; }
        public int Seniority { get; set; }
        public int MaxTestsForWeek { get; set; }
        public double MaxDistance { get; set; }
        public Schedule Schedule { get; set; }// at c-tor: need to fill FALSE in the place he couldn`t work
        public List<Test> TesterTests { get; set; }

        public override string ToString()
        {
            string str = "          sun   mon   thu   wed   tue\n   ";
            for (int i = 9; i < 15; i++)
            {
                
                str +=i + ":00  ";
                for (int j = 0; j < 5; j++)
                {
                    if (Schedule[(DayOfWeek)j][i])
                    {
                        str += "  v   ";
                    }
                    else
                    {
                        str += "  x   ";
                    }

                }
                str += "\n  "  ;

            }
            return  base.ToString() + string.Format("   {0}: {1}\n   {2}: {3}\n   {4}: {5}\n   {6}:\n{7}\n ", ToSentence("VehicleType"), VehicleType, ToSentence("Seniority"), Seniority.ToString(), ToSentence("MaxTestsForWeek"), MaxTestsForWeek.ToString(), ToSentence("MaxDistance"), MaxDistance.ToString(),"Schedule", str);
        }
    }
}
