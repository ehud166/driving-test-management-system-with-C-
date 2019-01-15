
using System.Collections.Generic;
using System.ComponentModel;

namespace BE
{
    public static class Enums
    {
        public enum Vehicle
        {
            [Description("רכב פרטי")] privateCar,
            [Description("אופנוע")] motorcycle,
            [Description("משאית קלה עד 15 טון")] midTrailer,
            [Description("משאית כבדה מעל 15 טון")] maxTrailer
        }

        public enum Gear
        {
            [Description("ידני")] manual,
            [Description("אוטומט")] auto
        }

        public enum Gender
        {
            [Description("זכר")] male,
            [Description("נקבה")] female,
            [Description("אחר")] other
        }

        public enum User
        {
            administrator = 1,
            tester,
            trainee,
            exit
        }

        public enum Result
        {
            [Description("אין ציון")] noGrade,
            [Description("עבר")] pass,
            [Description("נכשל")] failed
        }

        public static string VT2Hebrew(Vehicle myVehicle)
        {
            if (myVehicle == Vehicle.privateCar)
                return ("רכב פרטי");
            if (myVehicle == Vehicle.motorcycle)
                return ("אופנוע");
            if (myVehicle == Vehicle.midTrailer)
                return ("משאית קלה עד 15 טון");
            if (myVehicle == Vehicle.maxTrailer)
                return ("משאית כבדה מעל 15 טון");
            return "";
        }

        public static Vehicle? Hebrew2VT(string myVehicle = null)
        {
            Vehicle? vehicle = null;
            switch (myVehicle)
            {
                case "רכב פרטי":
                    vehicle = Vehicle.privateCar;
                    break;
                case "אופנוע":
                    vehicle = Vehicle.motorcycle;
                    break;
                case "משאית קלה עד 15 טון":
                    vehicle = Vehicle.midTrailer;
                    break;
                case "משאית כבדה מעל 15 טון":
                    vehicle = Vehicle.maxTrailer;
                    break;
                default:
                    break;
            }
            return vehicle;
        }
        public static Gear? Hebrew2GT(string myGear = null)
        {
            Gear? gear = null;
            switch (myGear)
            {
                case "ידני":
                    gear = Gear.manual;
                    break;
                case "אוטומט":
                    gear = Gear.auto;
                    break;
                default:
                    break;

            }
            return gear;
        }

        public static string Gear2Hebrew(Gear myGear)
        {
            if (myGear == Gear.auto)
                return ("אוטומט");
            if (myGear == Gear.manual)
                return ("ידני");
            return "";
        }
    }
}