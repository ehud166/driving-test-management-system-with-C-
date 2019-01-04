

namespace BE
{
    public enum Vehicle
    {
        privateCar = 1, motorcycle, midTrailer, maxTrailer
    }
    public enum Gear
    {
        manual = 1, auto
    }
    public enum Gender
    {
        male, female
    }

    public enum User
    {
        administrator = 1,tester,trainee, exit
    }
    //public class EnumTools
    //{
    //    public static string GetDescription(Enum en)
    //    {
    //        Type type = en.GetType();
    //        MemberInfo[] memInfo = type.GetMember(en.ToString());
    //        if (memInfo != null && memInfo.Length > 0)
    //        {
    //            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
    //            if (attrs != null && attrs.Length > 0)
    //                return ((DescriptionAttribute)attrs[0]).Description;
    //        }
    //        return en.ToString();
    //    }
    //}
}