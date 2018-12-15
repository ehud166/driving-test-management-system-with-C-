using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee : Person
    {
        public Trainee(string id, string lastName, string firstName, DateTime birthday, Gender gender, string phone, Adress adress, 
                        Vehicle vehicleType, Gear gear, string drivingSchool, string teacherName, int lessonNum, int numOfTests = 0 )
                        : base(id, lastName, firstName, birthday, gender, phone, adress, vehicleType)
        {
            Gear = gear;
            DrivingSchool = drivingSchool;
            TeacherName = teacherName;
            LessonNum = lessonNum;
            NumOfTests = numOfTests;
        }

        //properties
        public Gear Gear { get; set; }
        public string DrivingSchool { get; set; }
        public string TeacherName { get; set; }
        public int LessonNum { get; set; }
        public int NumOfTests { get; set; }
        //ToString
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
