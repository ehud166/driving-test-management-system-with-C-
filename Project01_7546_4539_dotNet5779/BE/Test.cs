using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        public Test(string testerId, string traineeId, TestDateTime testDateAndTime, Adress testAdress,Gear gear ,bool? testDistance = null, bool? testReverseParking = null, bool? testMirrors = null, bool? testVinker = null, bool? testResult = null, string testComment = null, string iD = null)
        {
            ID = iD;
            TesterId = testerId;
            TraineeId = traineeId;
            TestDateAndTime = testDateAndTime;
            TestAdress = testAdress;
            Gear = gear;
            TestDistance = testDistance;
            TestReverseParking = testReverseParking;
            TestMirrors = testMirrors;
            TestVinker = testVinker;
            TestResult = testResult;
            TestComment = testComment;
        }

        public string ID { get; set; }
        public string TesterId { get; set; }
        public string TraineeId { get; set; }
        public TestDateTime TestDateAndTime { get; set; }
        public Adress TestAdress { get; set; }
        public Vehicle VehicleType { get; set; }
        public Gear Gear { get; set; }
        //test results
        public bool? TestDistance { get; set; }
        public bool? TestReverseParking { get; set; }
        public bool? TestMirrors { get; set; }
        public bool? TestVinker { get; set; }
        public bool? TestResult { get; set; }
        public string TestComment { get; set; }
        //ToString
        public override string ToString()
        {
            return this.ToStringProperty();
        }








    }
}
