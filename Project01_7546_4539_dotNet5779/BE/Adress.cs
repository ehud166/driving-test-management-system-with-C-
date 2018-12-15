using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Adress
    {
        public Adress(string street_name, int building_number, string city)
        {
            Street_name = street_name;
            Building_number = building_number;
            City = city;
        }

        //properties
        public string Street_name { get; set; }
        public int Building_number { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty("\t");
        }
    }
}
