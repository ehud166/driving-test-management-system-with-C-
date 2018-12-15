using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Schedule
    {
        Dictionary<DayOfWeek, HoursPerDay> weekDays;
        //c-tor
        public Schedule()
        {
            Dictionary<DayOfWeek, HoursPerDay> weekDays = new Dictionary<DayOfWeek, HoursPerDay>();
            //initialize false`s array to each day
            for (int i = 0; i < 5; i++)
            {
                weekDays.Add((DayOfWeek)i, new HoursPerDay());
            }
        }

        public HoursPerDay this[DayOfWeek index]//indexer
        {
            get {return weekDays[index];}
            set { weekDays[index] = value; }
        }
    }
}
