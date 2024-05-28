using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class UpdateCustomer : System.Web.UI.Page
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
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                   LoadCustomers(Int32.Parse(HFRecordID.Value));
                }

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
                    newItem.Text = reader["MeterName"] + " : " + reader["MeterModel"] + " : " + reader["Meterno"] + " : " + reader["Manufacturer"].ToString();
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

        private void LoadCustomers(Int64 RecordID)
        {



            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT [RecordID] ,[LastName] ,[Firstname], Othername ,[Email],[Phonenumber],[CustomerAddress],[DistrictCode], [ZoneCode],[Subzone],[Round] ,[Foliono],[Metered],[MeterNo],[MeterCharge],[FlateRateCategory],[Tarrif],[Consumption],[DPCno],Active FROM [_tblCustomers3]";
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

                txtLastname.Text = reader["LastName"].ToString();

                txtFirstname.Text = reader["Firstname"].ToString();


                txtEmail.Text = reader["Email"].ToString();
                txtMobile.Text = reader["Phonenumber"].ToString();
                txtCustomerAddress.Text = reader["CustomerAddress"].ToString();
                txtFoliono.Text = reader["Foliono"].ToString();
                txtZoneCode.Text = reader["ZoneCode"].ToString();
                txtRound.Text = reader["Round"].ToString();
                txtTarrif.Text = reader["Tarrif"].ToString();
                txtConsumption.Text = reader["Consumption"].ToString();
                txtSubzone.Text = reader["Subzone"].ToString();

                ddlDistrictCode.SelectedValue = reader["DistrictCode"].ToString();
                ddlTarrifCategory.SelectedValue = reader["FlateRateCategory"].ToString();

                txtDpcNo.Text = reader["DPCno"].ToString();
                chkMetered.Checked = (bool)reader["Metered"];
                chkStatus.Checked = (bool)reader["Active"];

                if (chkStatus.Checked == true)
                {
                    chkStatus.Text = "Active (Connected)";
                    //lblMetered.Text = "Metered";
                }
                else
                {
                    chkStatus.Text = "In-Active (Disconnected)";
                    // lblMetered.Text = "Un-Metered";
                }

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
                txtExistingMeterCharges.Text = Convert.ToDouble(reader["MeterCharge"]).ToString();
                txtExistingTarrif.Text = Convert.ToDouble(reader["Tarrif"]).ToString();
                txtExistingConsumption.Text = Convert.ToDouble(reader["Consumption"]).ToString();
                HFTarrif.Value = Convert.ToDouble(reader["Tarrif"]).ToString();
                HFConsumption.Value = Convert.ToDouble(reader["Consumption"]).ToString();

                reader.Close();
                lblMessage.Text = "";


            }
            catch (Exception err)
            {
                lblMessage.Text = "Error Customers! ";
                lblMessage.Text += err.Message;
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
              //  pnlCharges.Visible = true;
                GetMeterCharge();
                //txtMeterCharges.Text = HFCharges.Value;
                txtExistingMeterCharges.Text = HFCharges.Value;
                FillMeterNo();

            }
            else if (ddlMeterStatus.SelectedValue == "Un-Metered")
            {
                txtExistingMeterCharges.Text = "0";

                //pnlCharges.Visible = true;
              //  PnlMeters.Visible = false;
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


            // Check that a file is actually being submitted.
            if (fupUploadFile.PostedFile.FileName == "")
            {
                lblMessage.Text = "No file specified.";
            }
            else
            {
                // Check the extension.
                string extension = Path.GetExtension(fupUploadFile.PostedFile.FileName);
                switch (extension.ToLower())
                {
                    case ".png":
                    case ".gif":
                    case ".jpg":
                    case ".jpeg":
                    case ".pdf":
                        break;
                    default:
                        lblMessage.Text = "This file type is not allowed only (png,jpeg,gif,jpg,pdf).";
                        return;
                }
                // Using this code, the saved file will retain its original
                // file name, but be stored in the current server
                // application directory.


                string FileName = Path.GetFileName(fupUploadFile.PostedFile.FileName);
                //string fullUploadPath = Path.Combine(uploadDirectory,
                //  serverFileName);

                string newFileName = "";
                newFileName = Guid.NewGuid().ToString() + FileName;




                string insertSQL = "spUpdateCustomer";



                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(insertSQL, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the parameters.
                // cmd.Parameters.AddWithValue("@CustomerID", uniqueGuidString);
                cmd.Parameters.AddWithValue("@LastName", txtLastname.Text);
                cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
                cmd.Parameters.AddWithValue("@Middlename", txtMiddlename.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@CustomerAddress", txtCustomerAddress.Text);


                cmd.Parameters.AddWithValue("@DistrictCode", ddlDistrictCode.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@ZoneCode", txtZoneCode.Text);
                cmd.Parameters.AddWithValue("@Subzone", txtSubzone.Text);

                cmd.Parameters.AddWithValue("@Round", txtRound.Text);
                cmd.Parameters.AddWithValue("@Foliono", txtFoliono.Text);
                cmd.Parameters.AddWithValue("@Active", chkStatus.Checked);

                if (txtMeterNo.Text != " ")
                {

                    cmd.Parameters.AddWithValue("@Metered", true);
                    cmd.Parameters.AddWithValue("@MeterNo", txtMeterNo.Text);
                    cmd.Parameters.AddWithValue("@Consumption", Convert.ToDecimal(txtConsumption.Text));
                    cmd.Parameters.AddWithValue("@MeterCharge", txtExistingMeterCharges.Text);
                    cmd.Parameters.AddWithValue("@FlateRateCategory", Int32.Parse(ddlTarrifCategory.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@Tarrif", Convert.ToDecimal(HFTarrif.Value));


                }

                else if (ddlMeterStatus.SelectedValue == "Metered")
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
                    cmd.Parameters.AddWithValue("@FlateRateCategory", 0);
                    cmd.Parameters.AddWithValue("@MeterNo", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tarrif", Convert.ToDecimal(HFTarrif.Value));
                    cmd.Parameters.AddWithValue("@Consumption", Convert.ToDecimal(txtConsumption.Text));
                }




                string dpcno = ddlDistrictCode.SelectedItem.Value + "-" + txtZoneCode.Text + "-" + txtSubzone.Text + "-" + txtRound.Text + "-" + txtFoliono.Text;
                cmd.Parameters.AddWithValue("@DPCno", dpcno.ToString());
                cmd.Parameters.AddWithValue("@ReasonforUpdates", txtReason.Text);


                cmd.Parameters.AddWithValue("@UpdatedBy", User.Identity.Name);
                cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                cmd.Parameters.AddWithValue("@RecordID", Int32.Parse(HFRecordID.Value));

                //,@FilePath varchar(300)
                //           ,@FileData


                int added = 0;
                long ID;
                try
                {

                    Stream fs = fupUploadFile.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    cmd.Parameters.AddWithValue("@FileData", bytes);
                    fupUploadFile.PostedFile.SaveAs(Server.MapPath("~/crm/FileUploads/") + newFileName);

                    cmd.Parameters.AddWithValue("@FilePath", "~/crm/FileUploads/" + newFileName);



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
                    WaterCorp.Logic.ExceptionUtility.LogException(sqlEx, "Update Customer");


                }
                finally
                {
                    con.Close();
                }

                if (added > 0)
                {

                    //clsSMS sms = new clsSMS();
                    //string message = "Dear, " + txtLastname.Text + "  to KADSWAC your DPCNo is  : " + dpcno.ToString();
                    //string phoneno = txtMobile.Text;
                    //// phoneno = "08080788332";

                    ////format phone No so that it becomes 2348035982461 and not 23408035982461
                    //string formatedphoneno = phoneno.Substring(1, 10);
                    //sms.SendSMS(message, formatedphoneno);

                    string userId = User.Identity.Name; // Replace with the actual user identifier
                    string tableName = "_tblCustomers3"; // Replace with the actual table name
                    int recordId = Int32.Parse(HFRecordID.Value); // Replace with the actual record identifier
                    string action = "Update Customer"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                    AuditTrailManager auditTrailManager = new AuditTrailManager();
                    auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                    // InsertBillingStatus();

                    //btnPayroll.Visible = true;
                    Response.Redirect("SearchCustomers.aspx");


                }
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


        protected void btnContinue_Click(object sender, EventArgs e)
        {

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


        //protected void ddlMeterStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtMeterCharges.Text = "0.0";
        //    // Hide all panels by default
        //    PnlMeters.Visible = false;
        //    pnlCharges.Visible = false;

        //    // Check the selected item and show the corresponding panel
        //    if (ddlMeterStatus.SelectedValue == "Metered")
        //    {
        //        PnlMeters.Visible = true;
        //        pnlCharges.Visible = true;
        //        GetMeterCharge();
        //        txtMeterCharges.Text = HFCharges.Value;
        //        FillMeterNo();

        //    }
        //    else if (ddlMeterStatus.SelectedValue == "Un-Metered")
        //    {
        //        pnlCharges.Visible = true;
        //        PnlMeters.Visible = false;
        //    }

        //}

        //protected void ddlTarrifCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Define ADO.NET objects.
        //    string selectSQL;
        //    selectSQL = "SELECT [TarrifID] ,[TarrifType],[Tarrif],[Consumption] FROM [_tblTarrif] ";
        //    selectSQL += "WHERE TarrifID = " + Convert.ToInt32(ddlTarrifCategory.SelectedItem.Value);
        //    SqlConnection con = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand(selectSQL, con);
        //    SqlDataReader reader;

        //    // Try to open database and read information.
        //    try
        //    {
        //        con.Open();
        //        reader = cmd.ExecuteReader();
        //        reader.Read();

        //        // Fill the controls.


        //        HFConsumption.Value = reader["Consumption"].ToString();
        //        HFTarrif.Value = reader["Tarrif"].ToString();

        //        txtTarrif.Text = HFTarrif.Value;
        //        txtConsumption.Text = HFConsumption.Value;

        //        reader.Close();
        //        //lblResults.Text = "";

        //    }
        //    catch (Exception err)
        //    {
        //        //  lblResults.Text = "Error getting author. ";
        //        // lblResults.Text += err.Message;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }


        //}
    }
}
    
