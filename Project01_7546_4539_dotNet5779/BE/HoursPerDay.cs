using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HoursPerDay
    {
        public bool[] HoursOfWork { get; set; }
        //c-tor
        public HoursPerDay()
        {
            HoursOfWork = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                HoursOfWork[i] = true;
            }
        }

        public bool this[int x]//indexer
        {
            get
            {
                if (x<9||x>14)
                {
                    throw new Exception("ERROR: the hour you set does NOT at hours range of test ");
                }
                return HoursOfWork[x - 9];
            }
            set
            {
                 HoursOfWork[x - 9] = value;
            }
        }
    }
}
