using Irony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class SendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Build the email message.
            string fullname = string.Empty;

            fullname = txtLastname.Text + " " + txtOthernames.Text;


            MailMessage objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress("noreply@kadwac.com.ng");
            objMailMessage.To.Add(new MailAddress(txtEmail.Text));
            objMailMessage.Subject = "Password Retrieval";
            objMailMessage.Body = "Dear " + fullname + "," + "<br /><br />Please retain the following password as you will need it to login to the system.<br /><br />Password: <i>" + " " + "</i><br /><br />Thank you.";
            objMailMessage.IsBodyHtml = true;
            // Send the email message.
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Send(objMailMessage);
            objSmtpClient.Dispose();
            // Set the message.
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Your email address was found, and your password was sent. Please check your email.";

        }
    }
}