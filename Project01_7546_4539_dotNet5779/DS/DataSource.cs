using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    static public class DataSource
    {
        public static List<Test> Tests { get; private set; } = new List<Test>();
        public static List<Tester> Testers { get; private set; } = new List<Tester>();
        public static List<Trainee> Trainees { get; private set; } = new List<Trainee>();
    }
    
}
