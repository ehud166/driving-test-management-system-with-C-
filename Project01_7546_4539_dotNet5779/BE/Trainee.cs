using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee : Person
    {
        public Trainee(string id, string lastName, string firstName, DateTime birthday, Gender gender, string phone, Address address, 
                        Vehicle vehicleType, Gear gear, string drivingSchool, string teacherName, int lessonNum)
                        : base(id, lastName, firstName, birthday, gender, phone, address, vehicleType)
        {
            Gear = gear;
            DrivingSchool = drivingSchool;
            TeacherName = teacherName;
            LessonNum = lessonNum;
        }

        //properties
        public Gear Gear { get; set; }
        public string DrivingSchool { get; set; }
        public string TeacherName { get; set; }
        public int LessonNum { get; set; }
        //ToString
        public override string ToString()
        {

            return base.ToString() + string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}\n{6}: {7}\n", ToSentence("Gear"), Gear.ToString(), ToSentence("DrivingSchool"), DrivingSchool, ToSentence("TeacherName"), TeacherName, ToSentence("LessonNum"), LessonNum.ToString());
        }
    }
}
