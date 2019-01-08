
using System.Collections.Generic;
using System.ComponentModel;

namespace BE
{
    public static class Enums
    {
        public enum Vehicle
        {
            [Description("רכב פרטי")] privateCar = 1,
            [Description("אופנוע")] motorcycle,
            [Description("משאית קלה עד 15 טון")] midTrailer,
            [Description("משאית כבדה מעל 15 טון")] maxTrailer
        }

        public enum Gear
        {
            [Description("ידני")] manual = 1,
            [Description("אוטומט")] auto
        }

        public enum Gender
        {
            [Description("זכר")] male,
            [Description("נקבה")] female
        }

        public enum User
        {
            administrator = 1,
            tester,
            trainee,
            exit
        }

        public static string VTToHebrew(Vehicle myVehicle)
        {
            if (myVehicle == Vehicle.privateCar)
                return ("רכב פרטי");
            if (myVehicle == Vehicle.motorcycle)
                return ("אופנוע");
            if (myVehicle == Vehicle.midTrailer)
                return ("משאית קלה");
            if (myVehicle == Vehicle.maxTrailer)
                return ("משאית כבדה");
            return "";
        }
    }

}