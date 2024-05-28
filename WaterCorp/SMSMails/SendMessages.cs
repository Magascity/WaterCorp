using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace WaterCorp.SMSMails
{
    public class SendMessages
    {
        public string Message { get; set; }
        public string Phone { get; set; }
        public void SendSMS(string Message, string Phone)
        {
            string url;
            url = "https://api.ebulksms.com:8080/sendsms?username=aysystems@yahoo.com&apikey=d8ae396be649eddc66b0240e41b1a5b9bf9d0a47&sender=WhiteField&messagetext=" + Message + "&flash=0&recipients=234" + Phone.ToString();
            // url = "https://api.ebulksms.com:8080/sendsms?username=abrahamlanrealabi@gmail.com&apikey=903c03b44a8af767cf25ac4620cea9b4fbb59a1b&sender=Pinnacle&messagetext=" + message + "&flash=0&recipients=234" + formatedphoneno.ToString();


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            new WebClient().DownloadData(url);

        }

        public void SendEmail(string Message, string email)
        {
            MailMessage objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress("noreply@Kadwac.com");
            objMailMessage.To.Add(new MailAddress(email.ToString()));
            objMailMessage.Subject = "Service Notification";
            objMailMessage.Body = Message.ToString();
            objMailMessage.IsBodyHtml = true;
            // Send the email message.

            try
            {
                SmtpClient objSmtpClient = new SmtpClient();
                objSmtpClient.Send(objMailMessage);
                objSmtpClient.Dispose();
            }
            catch (Exception ex)
            {
                // Set the message.
                //  lblMessage.ForeColor = System.Drawing.Color.Green;
                //  lblMessage.Text = ex.Message + " :" + ex.InnerException;

            }

        }
    }
}