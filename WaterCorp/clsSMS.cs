using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WaterCorp
{
    public class clsSMS
    {

        public string Message { get; set; }
        public string Phone { get; set; }
        public void SendSMS(string Message, string Phone)
        {
            string url;
            url = "https://api.ebulksms.com:443/sendsms?username=aysystems@yahoo.com&apikey=d8ae396be649eddc66b0240e41b1a5b9bf9d0a47&sender=KADSWAC&messagetext=" + Message + "&flash=0&recipients=234" + Phone.ToString();
            // url = "https://api.ebulksms.com:8080/sendsms?username=abrahamlanrealabi@gmail.com&apikey=903c03b44a8af767cf25ac4620cea9b4fbb59a1b&sender=Pinnacle&messagetext=" + message + "&flash=0&recipients=234" + formatedphoneno.ToString();

                                                                                            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            new WebClient().DownloadData(url);

        }
    }
}