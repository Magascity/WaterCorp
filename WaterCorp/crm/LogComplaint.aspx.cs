
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using static iText.IO.Codec.TiffWriter;

namespace WaterCorp.crm
{
    public partial class LogComplaint : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillBusinessUnits();
                FillService();
                //   FillSubService();

                

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
                lblMessage.Text = "Error reading Services";
                lblMessage.Text += err.Message;
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
            string selectSQL = "SELECT [SubserviceID] ,[SubServiceName],[ServiceID] FROM [SubServices] where ServiceID = " + Int32.Parse(ddlService.SelectedItem.Value);

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

            // Check that a file is actually being submitted.
            if (fupUpload.PostedFile.FileName == "")
            {
                //lblMessage.Text = "No file specified.";

                string insertSQL = "spInsertComplaint";



                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(insertSQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the parameters.
                cmd.Parameters.AddWithValue("@CustomerID", HFCustomerID.Value);
                cmd.Parameters.AddWithValue("@DpcNo", txtDpcno.Text);
                cmd.Parameters.AddWithValue("@ServiceID", Int32.Parse(ddlService.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@SubServiceID ", Int32.Parse(ddlSubService.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                cmd.Parameters.AddWithValue("@Status", "Open");

                cmd.Parameters.AddWithValue("@FileData", DBNull.Value);


                cmd.Parameters.Add("@FileImage", SqlDbType.VarBinary, -1).Value = DBNull.Value;

                cmd.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
                cmd.Parameters.AddWithValue("@DateLogged", DateTime.Now);


           

                int added = 0;
                int ID = 0;
                try
                {



                    con.Open();
                    added = cmd.ExecuteNonQuery();



                    string selectStatement = "Select IDENT_CURRENT('Complaints') FROM Complaints";
                    SqlCommand selectCommand = new SqlCommand(selectStatement, con);

                    ID = Convert.ToInt32(selectCommand.ExecuteScalar());



                }
                catch (Exception err)
                {
                    lblMessage.Text = err.Message;


                    // Log the exception.
                      WaterCorp.Logic.ExceptionUtility.LogException(err, "Create a New Complaint");


                }
                finally
                {
                    con.Close();
                }

                if (added > 0)
                {



                    string userId = User.Identity.Name; // Replace with the actual user identifier
                    string tableName = "Complaints"; // Replace with the actual table name
                    int recordId = ID; // Replace with the actual record identifier
                    string action = "Customer Complaint"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                    AuditTrailManager auditTrailManager = new AuditTrailManager();
                    auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                    // InsertBillingStatus();

                    //btnPayroll.Visible = true;
                    Response.Redirect("ViewComplaints.aspx");


                }



            }
            else
            {
                // Check the extension.
                string extension = Path.GetExtension(fupUpload.PostedFile.FileName);
                switch (extension.ToLower())
                {
                    case ".png":
                    case ".gif":
                    case ".jpg":

                        break;
                    default:
                        lblMessage.Text = "This file type is not allowed only (png,jpeg,gif).";
                        return;
                }
                // Using this code, the saved file will retain its original
                // file name, but be stored in the current server
                // application directory.


                string FileName = Path.GetFileName(fupUpload.PostedFile.FileName);
                //string fullUploadPath = Path.Combine(uploadDirectory,
                //  serverFileName);

                string newFileName = "";
                newFileName = Guid.NewGuid().ToString() + FileName;


                //@CustomerID int
                //,  int
                //, int
                //,  varchar(100)
                //,@FileImage varbinary(max)
                //, varchar(20)
                //,@DateLogged datetime


                string insertSQL = "spInsertComplaint";



                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(insertSQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the parameters.
                cmd.Parameters.AddWithValue("@CustomerID", HFCustomerID.Value);
               
                cmd.Parameters.AddWithValue("@ServiceID", Int32.Parse(ddlService.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@SubServiceID ", Int32.Parse(ddlSubService.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Status", "Open");

                cmd.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
                cmd.Parameters.AddWithValue("@DateLogged", DateTime.Now);




                int added = 0;
                int ID = 0;
                try
                {

                    Stream fs = fupUpload.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    cmd.Parameters.AddWithValue("@FileData", bytes);
                    fupUpload.PostedFile.SaveAs(Server.MapPath("~/crm/FileUploads/") + newFileName);

                    cmd.Parameters.AddWithValue("@FileImage", "~/crm/FileUploads/" + newFileName);


                    con.Open();
                    added = cmd.ExecuteNonQuery();



                    string selectStatement = "Select IDENT_CURRENT('Complaints') FROM Complaints";
                    SqlCommand selectCommand = new SqlCommand(selectStatement, con);

                    ID = Convert.ToInt32(selectCommand.ExecuteScalar());
                    


                }
                catch (Exception err)
                {
                    lblMessage.Text = err.Message;


                    // Log the exception.
                    WaterCorp.Logic.ExceptionUtility.LogException(err, "Create a New Complaint");


                }
                finally
                {
                    con.Close();
                }

                if (added > 0)
                {





                    string userId = User.Identity.Name; // Replace with the actual user identifier
                    string tableName = "Complaints"; // Replace with the actual table name
                    int recordId = ID; // Replace with the actual record identifier
                    string action = "Customer Complaint"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                    AuditTrailManager auditTrailManager = new AuditTrailManager();
                    auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                    // InsertBillingStatus();

                    //btnPayroll.Visible = true;
                    Response.Redirect("ViewComplaints.aspx");


                }
            }

        }

        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubService();
        }
    }
}