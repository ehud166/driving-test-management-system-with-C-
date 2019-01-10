using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Address
    {
        Random r = new Random();
        public Address(string streetName, int buildingNumber, string city, int temporaryCoordinate = 0 )
        {
            StreetName = streetName;
            BuildingNumber = buildingNumber;
            City = city;
            TemporaryCoordinate = r.Next(100);
        }
        public Address()
        {
            StreetName = "";
            BuildingNumber = 0;
            City = "";
            TemporaryCoordinate = r.Next(100);
        }

        //properties
        public string StreetName { get; set; }
        public int BuildingNumber { get; set; }
        public string City { get; set; }
        public int TemporaryCoordinate { get; set; }


        public override string ToString()
        {
            return string.Format("   {0}: {1}\n   {2}: {3}\n   {4}: {5}\n   {6}: {7}\n", "Street name", StreetName, "Building number", BuildingNumber.ToString(), "city", City, "Temporary coordinate", TemporaryCoordinate.ToString());
        }


    }
}
