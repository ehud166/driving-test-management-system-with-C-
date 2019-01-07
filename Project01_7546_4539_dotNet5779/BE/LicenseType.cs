using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Enums;

namespace BE
{
    public class LicenseType
    {
        public Vehicle VehicleType { get; set; }
        public Gear Gear { get; set; }
        public int LessonNum { get; set; }

        public LicenseType(Vehicle vehicleType, Gear gear, int lessonNum=0)
        {
            VehicleType = vehicleType;
            Gear = gear;
            LessonNum = lessonNum;
        }

        
            
        public LicenseType() { }
    }
}
