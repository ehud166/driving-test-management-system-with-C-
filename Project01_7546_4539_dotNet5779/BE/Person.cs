using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace BE
{
    public class Person
    {

        public Person(string id, string firstName, string lastName, DateTime birthday,
   string gender, string phoneAreaCode, string phoneNumber, Address address, Vehicle vehicleType)
        {
            ID = id;
            LastName = lastName;
            FirstName = firstName;
            Birthday = birthday;
            Gender = gender;
            PhoneAreaCode = phoneAreaCode;
            PhoneNumber = phoneNumber;
            Address = address;
            VehicleType = vehicleType;
        }

        public Person()
        {

        }
        public Person(string id)
        {
            ID = id;
        }

        public string ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public Address Address { get; set; }
        public Vehicle VehicleType { get; set; }
        //property for store split phoneNumber
        public string PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        //gender property
        public string Gender { get => _gender == BE.Gender.male ? "זכר" : "נקבה"; set => _gender = ToGender(value); }
        //gender private field
        private Gender _gender;

        //helper to convert hebrew string to enum
        private static Gender ToGender(string value)
        {
            if (value == "זכר") return BE.Gender.male;
               else if (value == "נקבה") return BE.Gender.female;
            return 0;
        }

        public static string ToSentence(string title)
        {
            return string.Concat(title.Select(x => char.IsUpper(x) ? " " + char.ToLower(x) : x.ToString())).TrimStart(' ');
        }

        public override string ToString()
        {
            return string.Format("   {0}: {1}\n   {2}: {3}\n   {4}: {5}\n   {6}: {7}\n   {8}: {9}\n   {10}: {11}\n   {12}: {13}\n   {14}: {15}\n", ToSentence("Id"), ID.ToString(), ToSentence("FirstName"), FirstName, ToSentence("LastName"), LastName, ToSentence("Birthday"), Birthday.ToString("MM/dd/yyyy"), ToSentence("Gender"), Gender.ToString(), ToSentence("Phone"), PhoneAreaCode + " - " + PhoneNumber, ToSentence("Address"), Address.ToString(), ToSentence("VehicleType"), VehicleType.ToString());
        }
    }
}
