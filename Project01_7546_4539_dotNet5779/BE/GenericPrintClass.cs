using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    static class GenericPrintClass
    {
        

        public static string ToStringProperty<T>(this T t, string ch = null)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                if (item.GetType().BaseType != t.GetType())
                {
                    //Console.WriteLine("item.GetType().BaseType");
                    str += "\n" + ch + item.Name + ": " + item.GetValue(t, null);
                }

            return str;
        }
       
    }
}
