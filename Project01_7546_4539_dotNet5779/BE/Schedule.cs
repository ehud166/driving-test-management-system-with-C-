using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// helper class for represent tester schedule
    /// </summary>
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

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="x">get the day of week </param>
        /// <returns>the schedule for this day</returns>
        public HoursPerDay this[DayOfWeek index]//indexer
        {
            get {return WeekDays[index];}
            set { WeekDays[index] = value; }
        }

    }
}
