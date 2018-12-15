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
        public Person(string id, string lastName, string firstName, DateTime birthday, 
                        Gender gender, string phone, Adress adress, Vehicle vehicleType)
        {
            ID = id;
            LastName = lastName;
            FirstName = firstName;
            Birthday = birthday;
            Gender = gender;
            Phone = phone;
            Adress = adress;
            VehicleType = vehicleType;
        }

        public string ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Adress Adress { get; set; }
        public Vehicle VehicleType { get; set; }

        


        public override string ToString()
        {
            //var sb = new StringBuilder();
            //string print = "";
            //foreach (object obj in this)
            //{
            //    print += ToSentence(obj) + obj.ToString();
            //}
            return this.ToStringProperty();
            //return string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}\n{6}: {7}\n{8}: {9}\n{10}: {11}\n{12}: {13}\n{14}: {15}\n", ToSentence(ID), ID.ToString(), ToSentence(LastName), LastName, ToSentence(FirstName), FirstName, ToSentence(Birthday), Birthday.ToString(), ToSentence(Gender), Gender.ToString(), ToSentence(Phone), Phone.ToString(), ToSentence(Adress), Adress.ToString(), ToSentence(VehicleType), VehicleType.ToString());
        }
    }
}
