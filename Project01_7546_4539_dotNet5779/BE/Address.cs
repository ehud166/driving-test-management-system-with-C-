using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Address
    {
        public Address(string streetName, int buildingNumber, string city)
        {
            StreetName = streetName;
            BuildingNumber = buildingNumber;
            City = city;
        }

        //properties
        public string StreetName { get; set; }
        public int BuildingNumber { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty("\t");
        }

        
    }
}
