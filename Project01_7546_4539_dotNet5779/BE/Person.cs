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
                        Gender gender, string phone, Address address, Vehicle vehicleType)
        {
            ID = id;
            LastName = lastName;
            FirstName = firstName;
            Birthday = birthday;
            Gender = gender;
            Phone = phone;
            Address = address;
            VehicleType = vehicleType;
        }

        public string ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public Vehicle VehicleType { get; set; }



        public static string ToSentence(string title)
        {
            return string.Concat(title.Select(x => char.IsUpper(x) ? " " + char.ToLower(x) : x.ToString())).TrimStart(' ');
        }

        public override string ToString()
        {
            return string.Format("   {0}: {1}\n   {2}: {3}\n   {4}: {5}\n   {6}: {7}\n   {8}: {9}\n   {10}: {11}\n   {12}: {13}\n   {14}: {15}\n", ToSentence("Id"), ID.ToString(), ToSentence("FirstName"), FirstName, ToSentence("LastName"), LastName, ToSentence("Birthday"), Birthday.ToString("MM/dd/yyyy"), ToSentence("Gender"), Gender.ToString(), ToSentence("Phone"), Phone.ToString(), ToSentence("Address"), Address.ToString(), ToSentence("VehicleType"), VehicleType.ToString());
        }
    }
}
