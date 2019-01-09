using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Enums;

namespace BE
{
    public class Test
    {
        public Test( string traineeId, DateTime testDateAndTime, Address testAddress, Vehicle? vehicleType, Gear? gear , string testComment = "", bool ? testDistance = null, bool? testReverseParking = null, bool? testMirrors = null, bool? testMerge = null, bool? testVinker = null, bool? testResult = null, string testerId = null, string iD = null)
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

        public Test()
        {

        }

        public string ID { get; set; }
        public string TesterId { get; set; }
        public string TraineeId { get; set; }
        public DateTime TestDateAndTime { get; set; }
        public Address TestAddress { get; set; }
        public Vehicle? VehicleType { get; set; }
        public Gear? Gear { get; set; }
        //test results
        public bool? TestDistance { get; set; }
        public bool? TestReverseParking { get; set; }
        public bool? TestMirrors { get; set; }
        public bool? TestVinker { get; set; }
        public bool? TestMerge { get; set; }
        public bool? TestResult { get; set; }
        public string TestComment { get; set; }

        //ToString
        //public override string ToString()
        //{
        //    return this.ToStringProperty();
        //}

        public override string ToString()
        {
            return string.Format("   {0}: {1}\n   {2}: {3}\n   {4}: {5}\n   {6}: {7}\n   {8}: {9}\n   {10}: {11}\n   {12}: {13}\n   {14}: {15}\n   {16}: {17}\n   {18}: {19}\n   {20}: {21}\n   {22}: {23}\n   {24}: {25}\n   {26}: {27}\n",
         "Id", ID, "tester Id", TesterId, "trainee Id", TraineeId,  "test date and dime" , TestDateAndTime.ToString("MM/dd/yyyy hh:mm"),
         "test address", TestAddress.ToString(), "vehicle type",  VehicleType.ToString(), "gear",   Gear.ToString(),
         "test distance",  TestDistance.ToString(), "test reverse parking", TestReverseParking.ToString(), "test mirrors",
         TestMirrors.ToString(), "test vinker",  TestVinker.ToString(),
         "test merge",  TestMerge.ToString(), "test result",  TestResult.ToString(), "test comment",  TestComment.ToString());
        }






    }
}
