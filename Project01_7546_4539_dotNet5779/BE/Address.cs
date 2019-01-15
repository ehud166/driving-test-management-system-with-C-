using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Address
    {


        public Address(string streetName, int buildingNumber = 0, string city = "")
        {
            StreetName = streetName;
            BuildingNumber = buildingNumber;
            City = city;
            
        }
        public Address()
        {
            StreetName = "";
            BuildingNumber = 0;
            City = "";
        }

        //properties
        public string StreetName { get; set; }
        public int BuildingNumber { get; set; }
        public string City { get; set; }


        public override string ToString()
        {
            return string.Format("   {0}: {1}\n   {2}: {3}\n   {4}: {5}\n", "Street name", StreetName, "Building number", BuildingNumber.ToString(), "city", City);
        }


    }
}
