using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;

namespace WaterCorp.crm
{
    public partial class SendBulkEmail : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected async void btnSendBulkEmails_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve customer data
                List<Customer> customerList = GetCustomerData();

                // Provide a templateId (replace "YourTemplateId" with the actual template ID)
                string templateId = "YourTemplateId";

                // Use Task.Run for background processing
                await Task.Run(() => SendBulkEmails(customerList, templateId));

                // You can add a success message or redirect the user to another page
                lblStatus.Text = "Bulk emails sent successfully!";
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, display error to the user, etc.)
                lblStatus.Text = "Error: " + ex.Message;
            }
        }


        public List<Customer> GetCustomerData()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Email, FirstName, LastName FROM _tblCustomer3";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming you have a Customer class with properties Email, FirstName, LastName
                                Customer customer = new Customer
                                {
                                    Email = reader["Email"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                };

                                customers.Add(customer);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (log, throw, etc.)
                        Console.WriteLine($"Error retrieving customer data: {ex.Message}");
                        throw;
                    }
                }
            }

            return customers;
        }

        private string GetEmailTemplateFromDatabase(string templateId)
        {
           // string connectionString = ConfigurationManager.ConnectionStrings["YourDatabaseConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Replace "YourTemplateTable" with the actual table name where email templates are stored
                string query = "SELECT Body FROM YourTemplateTable WHERE TemplateId = @TemplateId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TemplateId", templateId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assuming the email template body is stored in the "Body" column
                            return reader["Body"].ToString();
                        }
                    }
                }
            }

            // If the template is not found or any error occurs, return a default message
            return "Email template not found or an error occurred.";
        }


        private void SendBulkEmails(List<Customer> customers, string templateId)
        {
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            string smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
            string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

            using (SmtpClient client = new SmtpClient(smtpServer))
            {
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                foreach (var customer in customers)
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("your-email@example.com");
                    mail.To.Add(customer.Email);

                    // Retrieve the email template from the database
                    string emailTemplate = GetEmailTemplateFromDatabase(templateId);

                    // Replace placeholders in the template with customer-specific information
                    emailTemplate = ReplacePlaceholders(emailTemplate, customer);

                    mail.Body = emailTemplate;
                    mail.IsBodyHtml = true;

                    // Send the email
                    client.Send(mail);
                }
            }
        }

        private string ReplacePlaceholders(string template, Customer customer)
        {
            // Replace placeholders with customer-specific information
            template = template.Replace("{FirstName}", customer.FirstName);
            template = template.Replace("{LastName}", customer.LastName);

            return template;
        }


    }


    public class Customer
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}