using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TestDateTime
    {
        public TestDateTime(DayOfWeek day, int hour)
        {
            Day = day;
            Hour = hour;
        }

        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
    }
}
