using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class SendMessage : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["Row"] != null)
                {
                    //Fetch the GridView Row from Session.
                    GridViewRow row = Session["Row"] as GridViewRow;

                    //Fetch and display the Cell values.
                    // HFEmployeeID.Value = row.Cells[0].Text;
                    // txtRecordID.Text = row.Cells[1].Text;
                    //txtLastname.Text = row.Cells[2].Text;
                    HFCustomerID.Value = row.Cells[0].Text;
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                    LoadCustomers(Int32.Parse(HFCustomerID.Value));
                }

            }



        }

        private void LoadCustomers(Int64 RecordID)
        {



            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT [RecordID] ,[LastName] , Firstname, Othername ,[Email],[PhoneNumber],[CustomerAddress],[DistrictCode], [ZoneCode],[Subzone],[Round] ,[Foliono],[Metered],[MeterNo],[MeterCharge],[FlateRateCategory],[Tarrif],[Consumption],[CustReference] FROM [_tblCustomers3]";
            selectSQL += " WHERE RecordID = " + RecordID;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                //txtRecordID.Text = reader["RecordID"].ToString();
                txtLastname.Text = reader["LastName"].ToString();

                txtOthernames.Text = reader["Firstname"].ToString();

                txtEmail.Text = reader["Email"].ToString();
                txtCustomerAddress.Text = reader["CustomerAddress"].ToString();

                txtDpcno.Text = reader["CustReference"].ToString();

                txtMobile.Text = reader["PhoneNumber"].ToString();

                reader.Close();
                lblMessage.Text = "";


            }
            catch (Exception err)
            {
                lblMessage.Text = "Error Retrieving Customer";
                lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userEmail = txtEmail.Text;
            string userPhone = txtMobile.Text;
            string messageText = txtDescription.Text;
            string firstName = txtOthernames.Text;

            bool isEmailChecked = chkEmail.Checked;
            bool isSmsChecked = chkSMS.Checked;

            // Check if at least one checkbox is checked
            if (!isEmailChecked && !isSmsChecked)
            {
                lblMessage.Text = "Please select at least one notification method (Email or SMS).";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Prepare the message
            string message = $"Dear {firstName}, {messageText}";

            // Send Email if checkbox is checked
            if (isEmailChecked)
            {
                SendEmail(userEmail, message);
            }

            // Send SMS if checkbox is checked
            if (isSmsChecked)
            {
                SendSms(userPhone, message);
            }

            lblMessage.Text = "Messages sent successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        // Function to send email
        private void SendEmail(string toEmail, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();

                SmtpServer.Host = "mail.whitefieldhotels.org";
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("noreply@whitefieldhotels.org", "uh0a8@1Y3");
                SmtpServer.EnableSsl = false;

                mail.From = new MailAddress("noreply@whitefieldhotels.org");
                mail.To.Add(toEmail);
                mail.Subject = "Notification";
                mail.Body = message;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed to send email. Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Function to send SMS
        private void SendSms(string phoneNumber, string message)
        {
            try
            {
                clsSMS msg = new clsSMS();
                string formattedPhoneNumber = phoneNumber.Substring(1, 10); // Adjust based on your phone number format
                msg.SendSMS(message, formattedPhoneNumber);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed to send SMS. Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}