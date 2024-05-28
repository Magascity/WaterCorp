
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WaterCorp.crm
{
    public partial class CustomerUpdates : System.Web.UI.Page
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillBusinessUnits();
                FillTarrifRates();
                FillDistrictCodes();



                if (Session["Row"] != null)
                {
                    //Fetch the GridView Row from Session.
                    GridViewRow row = Session["Row"] as GridViewRow;

                    //Fetch and display the Cell values.
                    // HFEmployeeID.Value = row.Cells[0].Text;
                    // txtRecordID.Text = row.Cells[1].Text;
                    //txtLastname.Text = row.Cells[2].Text;
                    HFRecordID.Value = row.Cells[0].Text;
                    //chkMetered.Checked = (bool)row.Cells[12].Text;
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                    LoadCustomers(Int32.Parse(HFRecordID.Value));
                }

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


            string insertSQL = "spVerifyUpdated";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            // cmd.Parameters.AddWithValue("@CustomerID", uniqueGuidString);
            cmd.Parameters.AddWithValue("@RecordID", Int32.Parse(HFRecordID.Value));
            cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            //cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
            //cmd.Parameters.AddWithValue("@Middlename", txtMiddlename.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
            cmd.Parameters.AddWithValue("@CustomerAddress", txtCustomerAddress.Text);
            cmd.Parameters.AddWithValue("@DistrictCode", ddlDistrictCode.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@ZoneCode", txtZoneCode.Text);
            cmd.Parameters.AddWithValue("@Subzone", txtSubzone.Text);
            
            cmd.Parameters.AddWithValue("@FlateRateCategory", ddlTarrifCategory.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Tarrif", Convert.ToDecimal(txtTarrif.Text));
            cmd.Parameters.AddWithValue("@Consumption", Convert.ToDecimal(txtConsumption.Text));
            cmd.Parameters.AddWithValue("@Round", txtRound.Text);
            cmd.Parameters.AddWithValue("@Foliono", txtFoliono.Text);

            string dpcno = ddlDistrictCode.SelectedItem.Value + "-" + txtZoneCode.Text + "-" + txtSubzone.Text + "-" + txtRound.Text + "-" + txtFoliono.Text;
            cmd.Parameters.AddWithValue("@DPCno", dpcno.ToString());
            cmd.Parameters.AddWithValue("@UpdatedBy", User.Identity.Name);
            cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);



      

            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();


                lblMessage.Text = "Record Updated Successfully !";
               


            }
            catch (Exception err)
            {
                lblMessage.Text = err.Message;



                // Log the exception.
                WaterCorp.Logic.ExceptionUtility.LogException(err, "Update New Customer");


            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {


                
                btnVerify.Visible = true;

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "_tblCustomers3"; // Replace with the actual table name
                int recordId = Int32.Parse(HFRecordID.Value); // Replace with the actual record identifier
                string action = "Update Customer"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
              //  Response.Redirect("ViewCustomers.aspx");


            }



        }

        private void LoadCustomers(Int64 RecordID)
        {



            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT [RecordID] , Customername,[LastName] ,[Firstname] ,[Email],[PhoneNumber],[CustomerAddress],[DistrictCode], [ZoneCode],[Subzone],[Round] ,[Foliono],[Metered],[MeterNo],[MeterCharge],[FlateRateCategory],[Tarrif],[Consumption],[CustReference] FROM [_tblCustomers3]";
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

              //  txtRecordID.Text = reader["RecordID"].ToString();
                txtCustomerName.Text = reader["CustomerName"].ToString();

              //  txtFirstname.Text = reader["Firstname"].ToString();


                txtEmail.Text = reader["Email"].ToString();
                txtMobile.Text = reader["PhoneNumber"].ToString();
                txtCustomerAddress.Text = reader["CustomerAddress"].ToString();
                txtFoliono.Text = reader["Foliono"].ToString();
                txtZoneCode.Text = reader["ZoneCode"].ToString();
                txtRound.Text = reader["Round"].ToString();
                txtTarrif.Text = reader["Tarrif"].ToString();
                txtConsumption.Text = reader["Consumption"].ToString();
                txtSubzone.Text = reader["Subzone"].ToString();

                ddlDistrictCode.SelectedValue = reader["DistrictCode"].ToString();
                ddlTarrifCategory.SelectedValue = reader["FlateRateCategory"].ToString();

                txtDPCNo.Text = reader["CustReference"].ToString();
                chkMetered.Checked = (bool)reader["Metered"];
                if (chkMetered.Checked == true)
                {
                    chkMetered.Text = " Metered";
                    //lblMetered.Text = "Metered";
                }
                else
                {
                    chkMetered.Text = " Un-Metered";
                    // lblMetered.Text = "Un-Metered";
                }
                txtMeterNo.Text = reader["MeterNo"].ToString();


                reader.Close();
                lblMessage.Text = "";


            }
            catch (Exception err)
            {
                lblMessage.Text = "Error Customers ";
                lblMessage.Text += err.Message;
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

        protected void ddlTarrifCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT [TarrifID] ,[TarrifType],[Tarrif],[Consumption] FROM [_tblTarrif] ";
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

        protected void btnVerify_Click(object sender, EventArgs e)
        {



            string insertSQL = "spVerifyCustomers";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            // cmd.Parameters.AddWithValue("@CustomerID", uniqueGuidString);
            cmd.Parameters.AddWithValue("@RecordID", Int32.Parse(HFRecordID.Value));
            cmd.Parameters.AddWithValue("@Verified", true);           
            cmd.Parameters.AddWithValue("@VerifiedBy", User.Identity.Name);
            cmd.Parameters.AddWithValue("@DateVerified", DateTime.Now);
            cmd.Parameters.AddWithValue("@Active", 1);




            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();






            }
            catch (Exception err)
            {
                lblMessage.Text = err.Message;



                // Log the exception.
                WaterCorp.Logic.ExceptionUtility.LogException(err, "Update New Customer");


            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {



                btnVerify.Visible = true;

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "_tblCustomers3"; // Replace with the actual table name
                int recordId = Int32.Parse(HFRecordID.Value); // Replace with the actual record identifier
                string action = "Verify Customer"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("VerifyCustomersInfo.aspx");


            }

        }
    }
}