using static BE.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester : Person
    {
        //c-tor
        public Tester(string id, string lastName, string firstName, DateTime birthday, Gender gender, string phone, Address address,
            Vehicle vehicleType, int seniority, int maxTestsForWeek, double maxDistance, List<Test> TestersTests = null)
            : base(id, lastName, firstName, birthday, gender, phone, address, vehicleType)
        {
            Seniority = seniority;
            MaxTestsForWeek = maxTestsForWeek;
            MaxDistance = maxDistance;
            Schedule = new Schedule();
        }

        //properties
        public int Seniority { get; set; }
        public int MaxTestsForWeek { get; set; }
        public double MaxDistance { get; set; }
        public Schedule Schedule { get; set; }// at c-tor: need to fill FALSE in the place he couldn`t work
        public List<Test> TesterTests { get; set; }

        public override string ToString()
        {
            return base.ToString() + string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}\n", ToSentence("Seniority"), Seniority.ToString(), ToSentence("MaxTestsForWeek"), MaxTestsForWeek.ToString(), ToSentence("MaxDistance"), MaxDistance.ToString());
        }
    }
}
