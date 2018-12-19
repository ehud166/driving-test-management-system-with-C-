using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        public Test( string traineeId, DateTime testDateAndTime, Address testAddress, Vehicle vehicleType, Gear gear ,bool? testDistance = null, bool? testReverseParking = null, bool? testMirrors = null, bool? testMerge = null, bool? testVinker = null, bool? testResult = null, string testerId = null, string testComment = null, string iD = null)
        {
            ID = iD;
            TesterId = testerId;
            TraineeId = traineeId;
            TestDateAndTime = testDateAndTime;
            TestAddress = testAddress;
            VehicleType = vehicleType;
            Gear = gear;
            TestDistance = testDistance;
            TestReverseParking = testReverseParking;
            TestMirrors = testMirrors;
            TestVinker = testVinker;
            TestMerge = testMerge;
            TestResult = testResult;
            TestComment = testComment;
        }

        public string ID { get; set; }
        public string TesterId { get; set; }
        public string TraineeId { get; set; }
        public DateTime TestDateAndTime { get; set; }
        public Address TestAddress { get; set; }
        public Vehicle VehicleType { get; set; }
        public Gear Gear { get; set; }
        //test results
        public bool? TestDistance { get; set; }
        public bool? TestReverseParking { get; set; }
        public bool? TestMirrors { get; set; }
        public bool? TestVinker { get; set; }
        public bool? TestMerge { get; set; }
        public bool? TestResult { get; set; }
        public string TestComment { get; set; }
        //ToString
        public override string ToString()
        {
            return this.ToStringProperty();
        }








    }
}
