using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Enums;

namespace BE
{
    public class Trainee : Person
    {
        public Trainee(string id, string firstName, string lastName, DateTime birthday, Gender gender, string phoneAreaCode, string phoneNumber, Address address,string email, string password,
            List<LicenseType> licenseType, string drivingSchool, string teacherName)
                        : base(id, firstName, lastName, birthday, gender, phoneAreaCode, phoneNumber, address, email, password)
        {
            DrivingSchool = drivingSchool;
            TeacherName = teacherName;
            LicenseType = licenseType;
        }

        public Trainee(string id, string firstName, string lastName, DateTime birthday, Gender gender, string phoneAreaCode, string phoneNumber, Address address, string email, string password,
             string drivingSchool, string teacherName)
            : base(id, firstName, lastName, birthday, gender, phoneAreaCode, phoneNumber, address, email, password)
        {
            DrivingSchool = drivingSchool;
            TeacherName = teacherName;
            LicenseTypeInitialize();
        }
        public Trainee() : base()
        {

        }

        public Trainee(string id) : base(id)
        {
            LicenseTypeInitialize();
        }

        private static void LicenseTypeInitialize()
        {
            List<LicenseType> LicenseType = new List<LicenseType>();
            LicenseType.Add(new LicenseType(Vehicle.maxTrailer, Gear.auto));
            LicenseType.Add(new LicenseType(Vehicle.maxTrailer, Gear.manual));
            LicenseType.Add(new LicenseType(Vehicle.midTrailer, Gear.auto));
            LicenseType.Add(new LicenseType(Vehicle.midTrailer, Gear.manual));
            LicenseType.Add(new LicenseType(Vehicle.motorcycle, Gear.auto));
            LicenseType.Add(new LicenseType(Vehicle.motorcycle, Gear.manual));
            LicenseType.Add(new LicenseType(Vehicle.privateCar, Gear.auto));
            LicenseType.Add(new LicenseType(Vehicle.privateCar, Gear.manual));
        }

        //properties
        public string DrivingSchool { get; set; }
        public string TeacherName { get; set; }
        public List<LicenseType> LicenseType { get; set; }

        //ToString
        public override string ToString()
        {
            string str = "";
            foreach (var type in LicenseType)
            {
                str += string.Format("vehicle type: {0}  gear: {1}  lesson number: {2}", type.VehicleType, type.Gear,
                    type.LessonNum);
            }
            return base.ToString() + string.Format("{0}\n {1}: {2}\n{3}: {4}\n", str, ToSentence("DrivingSchool"), DrivingSchool, ToSentence("TeacherName"), TeacherName);
        }
    }
}
