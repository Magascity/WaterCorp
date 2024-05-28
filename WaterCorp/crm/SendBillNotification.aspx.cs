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
using System.Threading.Tasks;

namespace WaterCorp.crm
{
    public partial class SendBillNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the page is being loaded for the first time
            if (!IsPostBack)
            {
                // Call a method to send water bill emails
               
            }
        }

        protected async void SendWaterBillEmailsToCustomersAsync()
        {
            // Retrieve connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // SQL query to retrieve water bill data
            string query = "SELECT * FROM WaterBills"; // Modify the query as needed

            try
            {
                // Create SQL connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute SQL command asynchronously
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Loop through each row in the result set asynchronously
                            while (await reader.ReadAsync())
                            {
                                // Retrieve data for each customer
                                string customerName = reader["CustomerName"].ToString();
                                string customerEmail = reader["Email"].ToString();
                                decimal totalDue = Convert.ToDecimal(reader["TotalDue"]);

                                // Compose email
                                string subject = "Monthly Water Bill Notification";
                                string body = $"Dear {customerName},<br><br>"
                                            + $"Your monthly water bill amount is: ${totalDue}<br><br>"
                                            + "Thank you for your prompt payment.<br><br>"
                                            + "Sincerely,<br>"
                                            + "Your Water Utility Company";

                                // Send email asynchronously
                                await SendEmailAsync(customerEmail, subject, body);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // You might want to log the error or display a message to the user
                // For simplicity, let's just display the error message in the console
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        protected async Task SendEmailAsync(string to, string subject, string body)
        {
            // Retrieve SMTP settings from Web.config
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            string username = ConfigurationManager.AppSettings["SmtpUsername"];
            string password = ConfigurationManager.AppSettings["SmtpPassword"];

            // Create a new MailMessage object
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(username);
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            // Create a new SmtpClient object and send the email asynchronously
            using (SmtpClient client = new SmtpClient(smtpServer, port))
            {
                client.Credentials = new NetworkCredential(username, password);
                client.EnableSsl = true;

                try
                {
                    await client.SendMailAsync(mail);
                    Console.WriteLine("Email sent successfully to: " + to);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // You might want to log the error or display a message to the user
                    // For simplicity, let's just display the error message in the console
                    Console.WriteLine("Error sending email to " + to + ": " + ex.Message);
                }
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            SendWaterBillEmailsToCustomersAsync(); 
        }
    }
}