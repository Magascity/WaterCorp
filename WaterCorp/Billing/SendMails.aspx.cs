using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Web.Configuration;

namespace WaterCorp.Billing
{
    public partial class SendMails : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private byte[] CreatePdf(SqlDataReader reader)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                // Create a new PDF document
                using (PdfWriter writer = new PdfWriter(stream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        // Add content to the PDF based on your data
                        // For example:
                        document.Add(new Paragraph($"Customer Name: {reader["CustomerName"]}"));
                        document.Add(new Paragraph($"Billing Period: {reader["BillingPeriod"]}"));
                        // Add other relevant details...

                        // Save the document
                        document.Close();
                    }
                }

                return stream.ToArray();
            }
        }

        private void SendEmail(string recipientEmail, byte[] attachment)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("ayok@email.com");
            mail.To.Add(recipientEmail);
            mail.Subject = "Water Bill";
            mail.Body = "Please find attached you bill!";

            // Attach the PDF
            MemoryStream ms = new MemoryStream(attachment);
            mail.Attachments.Add(new Attachment(ms, "Bill.pdf", "application/pdf"));

            // Setup SMTP client
            SmtpClient smtp = new SmtpClient("localhost"); ;
            //smtp.Port = 587;
            //smtp.Credentials = new NetworkCredential("your@email.com", "yourPassword");
            //smtp.EnableSsl = true;

            // Send the email
            smtp.Send(mail);
        }

        protected void btnMails_Click(object sender, EventArgs e)
        {
            // Retrieve customer data from the database
            //string connectionString = "YourConnectionString";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM WaterBills";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a PDF for each customer bill
                            byte[] pdfBytes = CreatePdf(reader);

                            // Send email with PDF attachment
                            SendEmail(reader["Email"].ToString(), pdfBytes);
                        }
                    }
                }
            }
        }
    }
}