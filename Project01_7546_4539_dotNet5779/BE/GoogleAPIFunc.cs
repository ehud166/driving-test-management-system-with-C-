using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml;

namespace BE
{
    public class GoogleAPIFunc
    {
        public GoogleAPIFunc()
        {
            string origin = "pisga 45 st. jerusalem"; //or "תקווה פתח 100 העם אחד "etc.
            string destination = "gilgal 78 st. ramat-gan"; //or "גן רמת 10 בוטינסקי'ז "etc.
            //build the url that includes the 2 addresses
            string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" +
                         origin + "&destinations=" + destination +
                         "&mode=driving&sensor=false&language=en-EN&units=imperial";
            //request from google service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            //we have an answer
            {
                if (xmldoc.GetElementsByTagName("status")[1].ChildNodes[0].InnerText == "NOT_FOUND")
                    //one of the addresses is not found
                    MessageBox.Show("one of the adrresses is not found");
                else
                {
                    //2 of the addresses are OK
                    //display the returned distance
                    XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                    double dist = Convert.ToDouble(distance[0].ChildNodes[1].InnerText.Replace(" mi",
                        ""));
                    Console.WriteLine(dist * 1.609344); //each mile is 1.609344 kilometer
                    //display the returned duration
                    XmlNodeList duration = xmldoc.GetElementsByTagName("duration");
                    string dur = duration[0].ChildNodes[1].InnerText;
                    MessageBox.Show(dur);
                }
            }
            else
            //we have no answer, the web is busy, the waiting time for answer is limited
            //(QUERY_OVER_LIMIT), we should try again(at least 2 seconds between 2 requests)

            {
                MessageBox.Show("We have'nt got an answer, maybe the net is busy...");
            }
        }
    }
}


