using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using WaterCorp.SMSMails;

namespace WaterCorp.crm
{
    public partial class UpdateComplaintsStatus : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        SendMessages msg = new SendMessages();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FillService();
                FillSubService();

              
                FillDistrictCodes();

                if (Session["Row"] != null)
                {
                    //Fetch the GridView Row from Session.
                    GridViewRow row = Session["Row"] as GridViewRow;

                    //Fetch and display the Cell values.
                    // HFEmployeeID.Value = row.Cells[0].Text;
                    // txtRecordID.Text = row.Cells[1].Text;
                    //txtLastname.Text = row.Cells[2].Text;
                    HFComplaintID.Value = row.Cells[0].Text;
                    //chkMetered.Checked = (bool)row.Cells[12].Text;
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                    LoadCustomers(Int32.Parse(HFComplaintID.Value));
                }

            }
        }

        private void LoadCustomers(Int64 RecordID)
        {



            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT  C.[ComplaintID],C.[CustomerID], C.[DpcNo], C.[ServiceID], C.[SubServiceID], C.[Description], C.[FileImage],  C.[Status],  C.[DateLogged],  C.[DateResolved], C.AcknowledgeNotes, C.[CreatedBy], C.[FileData],  CT.[LastName],  CT.[Firstname],  CT.[Othername], CT.[Email], CT.[Phonenumber] , CT.[CustomerAddress],CT.[DistrictCode] FROM  [Complaints] C JOIN  [_tblCustomers3] CT ON C.[CustomerID] = CT.[RecordID] ";
            selectSQL += " WHERE C.[ComplaintID] = " + RecordID;
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
                txtComplaintID.Text = reader["ComplaintID"].ToString();
                txtCustomerID.Text = reader["CustomerID"].ToString();
                txtDpcNo.Text = reader["DpcNo"].ToString();
                txtCustomername.Text = reader["Lastname"] + " " + reader["Firstname"] + " " + reader["Othername"].ToString();

                txtEmail.Text = reader["Email"].ToString();
                txtAddress.Text = reader["CustomerAddress"].ToString();
                txtDescription.Text = reader["Description"].ToString();
                txtMobile.Text = reader["Phonenumber"].ToString();

                ddlService.SelectedValue = reader["ServiceID"].ToString();

                ddlSubService.SelectedValue = reader["SubServiceID"].ToString();
                ddlSBU.SelectedValue = reader["DistrictCode"].ToString();

                if (reader["AcknowledgeNotes"].ToString() == "")
                {
                    PnlResolution.Visible = false;
                    PnlAck.Visible = true;
                    btnAck.Visible = true;
                    btnSubmit.Visible = false;
                }
                else
                {
                    PnlResolution.Visible = true;
                    PnlAck.Visible = false;
                    btnAck.Visible = false;
                    btnSubmit.Visible = true;
                }


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

        private void FillDistrictCodes()
        {
            ddlSBU.Items.Clear();
            ddlSBU.Items.Insert(0, new ListItem("--Select Business Unit--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT  [Id] ,[DistrictName] ,[Code] FROM [_tblSbus]";

            // Define the ADO.NET objects.
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();


                // list box text, and store the unique ID in the Value property.
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["DistrictName"].ToString();
                    newItem.Value = reader["Code"].ToString();
                    ddlSBU.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                //lblMessage.Text = "Error reading Faculty";
                //lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }

       


        private void FillService()
        {
            ddlService.Items.Clear();
            ddlService.Items.Insert(0, new ListItem("--Select Service--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [ServiceID] ,[ServiceName] FROM [Services]";

            // Define the ADO.NET objects.
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();


                // list box text, and store the unique ID in the Value property.
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["ServiceName"].ToString();
                    newItem.Value = reader["ServiceID"].ToString();
                    ddlService.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                //lblMessage.Text = "Error reading Faculty";
                //lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        private void FillSubService()
        {
            ddlSubService.Items.Clear();
            ddlSubService.Items.Insert(0, new ListItem("--Select Sub-Service--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [SubserviceID] ,[SubServiceName],[ServiceID] FROM [SubServices]";

            // Define the ADO.NET objects.
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();


                // list box text, and store the unique ID in the Value property.
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["SubServiceName"].ToString();
                    newItem.Value = reader["SubserviceID"].ToString();
                    ddlSubService.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                //lblMessage.Text = "Error reading Faculty";
                //lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           

            lblMessage.Text = "";

            

   
            string insertSQL = "spUpdateComplaintResolution";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            cmd.Parameters.AddWithValue("@ComplaintID", long.Parse(HFComplaintID.Value));
            cmd.Parameters.AddWithValue("@ResolutionNotes", txtResolution.Text);            

            cmd.Parameters.AddWithValue("@ResolvedBy", User.Identity.Name);
            cmd.Parameters.AddWithValue("@DateResolved", DateTime.Now);



            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();





            }
            //catch (Exception err)
            //{
            //    lblMessage.Text = err.Message;

            catch (SqlException sqlEx)
            {
                // Check if the exception is related to a primary key violation
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    // 2627: Violation of PRIMARY KEY constraint.
                    // 2601: Cannot insert duplicate key row in object with unique index.

                    string errorMessage = sqlEx.Message;

                    // Extract the duplicate key value from the error message
                    int startIndex = errorMessage.IndexOf("duplicate key value is (") + "duplicate key value is (".Length;
                    int endIndex = errorMessage.IndexOf(").", startIndex);

                    string duplicateKeyValue = errorMessage.Substring(startIndex, endIndex - startIndex);

                    lblMessage.Text = $"Violation of PRIMARY KEY constraint. Cannot insert duplicate key. The duplicate key value is ({duplicateKeyValue}).";
                }
                else
                {
                    lblMessage.Text = sqlEx.Message;
                }


                // Log the exception.
                WaterCorp.Logic.ExceptionUtility.LogException(sqlEx, "Update Complaint");


            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {

                clsSMS sms = new clsSMS();
                string message = "Dear, " + txtCustomername.Text + " We your complaint has been resolved. Thanks for your Patronage! ";
                string phoneno = txtMobile.Text;
                // phoneno = "08080788332";

                //format phone No so that it becomes 2348035982461 and not 23408035982461
                string formatedphoneno = phoneno.Substring(1, 10);
                sms.SendSMS(message, formatedphoneno);


                msg.SendEmail("Your Complaint has been resolved. Thank you for your Patronage", txtEmail.Text.Trim());

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "Complaint"; // Replace with the actual table name
                int recordId = Int32.Parse(HFComplaintID.Value); // Replace with the actual record identifier
                string action = "Customer Complaint"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("ViewComplaintsStatus.aspx");


            }


        }

        protected void btnAck_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";

            string insertSQL = "spUpdateComplaintAck";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            cmd.Parameters.AddWithValue("@ComplaintID", long.Parse(HFComplaintID.Value));


            cmd.Parameters.AddWithValue("@AcknowledgeNotes", txtAcknowledgement.Text);
            cmd.Parameters.AddWithValue("@AcknowledgeDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@AcknowledgedBy", User.Identity.Name);



            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();





            }
            //catch (Exception err)
            //{
            //    lblMessage.Text = err.Message;

            catch (SqlException sqlEx)
            {
                // Check if the exception is related to a primary key violation
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    // 2627: Violation of PRIMARY KEY constraint.
                    // 2601: Cannot insert duplicate key row in object with unique index.

                    string errorMessage = sqlEx.Message;

                    // Extract the duplicate key value from the error message
                    int startIndex = errorMessage.IndexOf("duplicate key value is (") + "duplicate key value is (".Length;
                    int endIndex = errorMessage.IndexOf(").", startIndex);

                    string duplicateKeyValue = errorMessage.Substring(startIndex, endIndex - startIndex);

                    lblMessage.Text = $"Violation of PRIMARY KEY constraint. Cannot insert duplicate key. The duplicate key value is ({duplicateKeyValue}).";
                }
                else
                {
                    lblMessage.Text = sqlEx.Message;
                }


                // Log the exception.
                WaterCorp.Logic.ExceptionUtility.LogException(sqlEx, "Update Complaint");


            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {

                //clsSMS sms = new clsSMS();
                //string message = "Dear, " + txtCustomername.Text + " We have recieved your complaint and are already working to resolve the Issue. Thanks for your Patronage! ";
                //string phoneno = txtMobile.Text;
                //// phoneno = "08080788332";

                ////format phone No so that it becomes 2348035982461 and not 23408035982461
                //string formatedphoneno = phoneno.Substring(1, 10);
                //sms.SendSMS(message, formatedphoneno);


                //msg.SendEmail("Your Complaint has been recieved and we are already working to resolve it. Thank you for your Patronage", txtEmail.Text.Trim());

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "Complaint"; // Replace with the actual table name
                int recordId = Int32.Parse(HFComplaintID.Value); // Replace with the actual record identifier
                string action = "Customer Complaint ack"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("ViewComplaintsStatus.aspx");


            }
        }
    }
}