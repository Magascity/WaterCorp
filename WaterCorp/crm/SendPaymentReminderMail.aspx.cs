using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WaterCorp.Billing;
using WebGrease;
using log4net;
using System.Web.Configuration;

namespace WaterCorp.crm
{
    public partial class SendPaymentReminderMail : System.Web.UI.Page
    {
        // private static readonly ILog log = LogManager.GetLogger(typeof(SendEmails));
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SendEmail));
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call method to send emails
                SendBillingEmails();
            }
        }

        protected void SendBillingEmails()
        {
           // string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
            string query = "SELECT CustomerName, Email, BillingAmount FROM Customers WHERE BillingStatus = 'Unpaid'";

            int emailsSent = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string customerName = reader["CustomerName"].ToString();
                            string email = reader["Email"].ToString();
                            decimal billingAmount = Convert.ToDecimal(reader["BillingAmount"]);

                            // Call method to send email
                            if (SendEmail(customerName, email, billingAmount))
                            {
                                emailsSent++;
                            }
                        }

                        // Display a message indicating the number of emails sent
                        if (emailsSent > 0)
                        {
                            lblMessage.Text = $"{emailsSent} email(s) sent successfully.";
                            lblMessage.Visible = true;
                        }
                        else
                        {
                            lblMessage.Text = "No emails to send.";
                            lblMessage.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        log.Error("An error occurred while sending billing emails.", ex);
                    }
                }
            }
        }

        protected bool SendEmail(string customerName, string email, decimal billingAmount)
        {
            // Get SMTP settings from web.config
            string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            string smtpUserName = ConfigurationManager.AppSettings["SmtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

            // Configure SMTP settings
            SmtpClient smtpClient = new SmtpClient(smtpHost);
            smtpClient.Port = smtpPort;
            smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
            smtpClient.EnableSsl = true;

            // Create the email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUserName);
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Billing Notification";

            // HTML email template
            string body = $@"
            <html>
            <body>
                <p>Dear {customerName},</p>
                <p>This is to notify you that you have an unpaid bill of ${billingAmount}. Please settle the payment at your earliest convenience.</p>
                <p>Regards,<br/>Your Company Name</p>
            </body>
            </html>";

            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                // Update billing status in the database if needed

                return true; // Email sent successfully
            }
            catch (Exception ex)
            {
                // Log the exception
                log.Error($"An error occurred while sending email to {email}.", ex);
                return false; // Failed to send email
            }
        }
    }
}