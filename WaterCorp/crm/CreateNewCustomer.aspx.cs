using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WaterCorp.crm
{
    public partial class CreateNewCustomer : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Int32 ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //FillBusinessUnits();
                FillTarrifRates();
                FillDistrictCodes();
                
            }

        }

        private void FillMeterNo()
        {
            ddlMeterNo.Items.Clear();
            ddlMeterNo.Items.Insert(0, new ListItem("--Select Meter--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [MeterID], [MeterName] ,[MeterModel],[Manufacturer],[MeterNo] FROM [tblMeterRecords] where Isssued = 0 ";

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
                    newItem.Text = reader["MeterName"] + " : " + reader["MeterModel"] + " : "  + reader["Meterno"] + " : " + reader["Manufacturer"].ToString();
                    newItem.Value = reader["Meterno"].ToString();
                    ddlMeterNo.Items.Add(newItem);
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

        private void FillDistrictCodes()
        {
            ddlDistrictCode.Items.Clear();
            ddlDistrictCode.Items.Insert(0, new ListItem("--Select Business Unit--", "0"));


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
                    ddlDistrictCode.Items.Add(newItem);
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


        private void FillTarrifRates()
        {
            ddlTarrifCategory.Items.Clear();
            ddlTarrifCategory.Items.Insert(0, new ListItem("--Select Tarrif Category--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [TarrifID] ,[TarrifType] FROM [_tblTarrif]";

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
                    newItem.Text = reader["TarrifType"].ToString();
                    newItem.Value = reader["TarrifID"].ToString();
                    ddlTarrifCategory.Items.Add(newItem);
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

            Guid uniqueGuid = Guid.NewGuid();

            // Convert to string if needed
            string uniqueGuidString = uniqueGuid.ToString();




            string insertSQL = "spInsertCustomerInfo";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            // cmd.Parameters.AddWithValue("@CustomerID", uniqueGuidString);

            string CustomerName = txtLastname.Text + " " + txtFirstname.Text + " " + txtMiddlename.Text;
            cmd.Parameters.AddWithValue("@CustomerName", CustomerName.ToString());
            cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
            cmd.Parameters.AddWithValue("@Othername", txtMiddlename.Text);
            cmd.Parameters.AddWithValue("@Lastname", txtLastname.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Phonenumber", txtMobile.Text);
            cmd.Parameters.AddWithValue("@CustomerAddress", txtCustomerAddress.Text);

          
            cmd.Parameters.AddWithValue("@DistrictCode", ddlDistrictCode.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@ZoneCode", txtZoneCode.Text);
            cmd.Parameters.AddWithValue("@Subzone", txtSubzone.Text);

            cmd.Parameters.AddWithValue("@Round", txtRound.Text);
            cmd.Parameters.AddWithValue("@Foliono", txtFoliono.Text);

            if (ddlMeterStatus.SelectedValue == "Metered")
            {
                cmd.Parameters.AddWithValue("@Metered", true);
                cmd.Parameters.AddWithValue("@MeterNo", ddlMeterNo.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Consumption", Convert.ToDecimal(txtConsumption.Text));
                cmd.Parameters.AddWithValue("@MeterCharge", txtMeterCharges.Text);
                cmd.Parameters.AddWithValue("@FlateRateCategory", Int32.Parse(ddlTarrifCategory.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@Tarrif", Convert.ToDecimal(HFTarrif.Value));

            }
            else
            {
                cmd.Parameters.AddWithValue("@Metered", 0);
                cmd.Parameters.AddWithValue("@MeterCharge", 0);
                cmd.Parameters.AddWithValue("@FlateRateCategory", Int32.Parse(ddlTarrifCategory.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@MeterNo", DBNull.Value);
                cmd.Parameters.AddWithValue("@Tarrif", Convert.ToDecimal(HFTarrif.Value));
                cmd.Parameters.AddWithValue("@Consumption", Convert.ToDecimal(txtConsumption.Text));
            }


            string dpcno = ddlDistrictCode.SelectedItem.Value+ "-" + txtZoneCode.Text + "-" + txtSubzone.Text + "-" + txtRound.Text + "-" + txtFoliono.Text;
            cmd.Parameters.AddWithValue("@DPCno", dpcno.ToString());

            cmd.Parameters.AddWithValue("@SMSNotice", chkSMS.Checked);

            cmd.Parameters.AddWithValue("@EmailNotice", chkEmail.Checked);

            cmd.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);




            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();



                string selectStatement = "Select IDENT_CURRENT('_tblCustomers3') FROM _tblCustomers3";
                SqlCommand selectCommand = new SqlCommand(selectStatement, con);

                ID = Convert.ToInt32(selectCommand.ExecuteScalar());
                


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
                WaterCorp.Logic.ExceptionUtility.LogException(sqlEx, "Save New Customer");


            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {

                clsSMS sms = new clsSMS();
                string message = "Dear, " + txtFirstname.Text + " Welcome to KADSWAC your DPCNo is  : " + dpcno.ToString();
                string phoneno = txtMobile.Text;
                // phoneno = "08080788332";

                //format phone No so that it becomes 2348035982461 and not 23408035982461
                string formatedphoneno = phoneno.Substring(1, 10);
                sms.SendSMS(message, formatedphoneno);

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "_tblCustomers3"; // Replace with the actual table name
                int recordId = ID; // Replace with the actual record identifier
                string action = "Create Customer"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("ViewUnverifiedCustomers.aspx");


            }
        }

        private void GetMeterCharge()
        {

            string selectSQL;
            selectSQL = "SELECT [MeterCharge] FROM [_tblMeterCharges] where Active = 1 ";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                // Fill the controls.


                HFCharges.Value = reader["MeterCharge"].ToString();


                reader.Close();
                //lblResults.Text = "";

            }
            catch (Exception err)
            {
                //  lblResults.Text = "Error getting author. ";
                // lblResults.Text += err.Message;
            }
            finally
            {
                con.Close();
            }



        }
        protected void ddlMeterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMeterCharges.Text = "0.0";
            // Hide all panels by default
            PnlMeters.Visible = false;
            pnlCharges.Visible = false;

            // Check the selected item and show the corresponding panel
            if (ddlMeterStatus.SelectedValue == "Metered")
            {
                PnlMeters.Visible = true;
                pnlCharges.Visible = true;
                GetMeterCharge();
                txtMeterCharges.Text = HFCharges.Value;
                FillMeterNo();

            }
            else if (ddlMeterStatus.SelectedValue == "Un-Metered")
            {
                pnlCharges.Visible = true;
                PnlMeters.Visible = false;
            }

        }

        protected void ddlTarrifCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT [TarrifID] ,[TarrifType],[Tarrif],[Consumption],CB_Account_Number, JV_Account_Number  FROM [_tblTarrif] ";
            selectSQL += "WHERE TarrifID = " + Convert.ToInt32(ddlTarrifCategory.SelectedItem.Value);
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                // Fill the controls.


                HFConsumption.Value = reader["Consumption"].ToString();
                HFTarrif.Value = reader["Tarrif"].ToString();
                HFCBAccount.Value = reader["CB_Account_Number"].ToString();
                HFJVAccount.Value = reader["JV_Account_Number"].ToString();
                txtTarrif.Text = HFTarrif.Value;
                txtConsumption.Text = HFConsumption.Value;

                reader.Close();
                //lblResults.Text = "";

            }
            catch (Exception err)
            {
                //  lblResults.Text = "Error getting author. ";
                // lblResults.Text += err.Message;
            }
            finally
            {
                con.Close();
            }


        }


        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            // Handle specific exception.
            if (exc is HttpUnhandledException)
            {
                lblMessage.Text = "An error occurred on this page. Please verify your " +
                "information to resolve the issue.";
            }
            // Clear the error from the server.
            Server.ClearError();
        }

       
    }
}