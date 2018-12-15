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
        public Tester(string id, string lastName, string firstName, DateTime birthday, Gender gender, string phone, Adress adress,
            Vehicle vehicleType, int seniority, int maxTestsForWeek, double maxDistance)
            : base(id, lastName, firstName, birthday, gender, phone, adress, vehicleType)
        {
            Seniority = seniority;
            MaxTestsForWeek = maxTestsForWeek;
            MaxDistance = maxDistance;
            Dictionary<DayOfWeek, HoursPerDay> schedule = new Dictionary<DayOfWeek, HoursPerDay>();
        }

        //properties
        public int Seniority { get; set; }
        public int MaxTestsForWeek { get; set; }
        public double MaxDistance { get; set; }
        public Dictionary<DayOfWeek, HoursPerDay> schedule { get; set; }// at c-tor: need to fill FALSE in the place he couldn`t work

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
