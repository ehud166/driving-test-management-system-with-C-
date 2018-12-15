using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DataSource
    {
        
        private static List<Test> tests;
        private static List<Tester> testers;
        private static List<Trainee> trainees;

        public static List<Test> Tests { get => tests; private set => tests = value; }
        public static List<Tester> Testers { get => testers; private set => testers = value; }
        public static List<Trainee> Trainees { get => trainees; private set => trainees = value; }
    }
}
