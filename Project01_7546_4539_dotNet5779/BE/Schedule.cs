using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Schedule
    {
        public Dictionary<DayOfWeek, HoursPerDay> WeekDays { get; set; }

        //c-tor
        public Schedule()
        {
             WeekDays = new Dictionary<DayOfWeek, HoursPerDay>();
            //initialize false`s array to each day
            for (int i = 0; i < 5; i++)
            {
                WeekDays.Add((DayOfWeek)i, new HoursPerDay());
            }
        }

        public HoursPerDay this[DayOfWeek index]//indexer
        {
            get {return WeekDays[index];}
            set { WeekDays[index] = value; }
        }

    }
}
