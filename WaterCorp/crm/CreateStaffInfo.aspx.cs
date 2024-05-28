
using Irony;
using Org.BouncyCastle.Asn1.X509;
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

namespace WaterCorp.crm
{
    public partial class CreateStaffInfo : System.Web.UI.Page
    {
       
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Int32 ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDistrictCodes();

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
                    newItem.Value = reader["ID"].ToString();
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // 



            

            lblMessage.Text = "";

            Guid uniqueGuid = Guid.NewGuid();

            // Convert to string if needed
            string uniqueGuidString = uniqueGuid.ToString();




            string insertSQL = "spInsertStaffInfo";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
             cmd.Parameters.AddWithValue("@PersonnelID", txtPersonnelID.Text);           
            cmd.Parameters.AddWithValue("@Firstname", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@Othername", txtOtherName.Text);
            cmd.Parameters.AddWithValue("@Lastname", txtLastName.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
            cmd.Parameters.AddWithValue("@SBU", ddlDistrictCode.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);




            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();



                string selectStatement = "Select IDENT_CURRENT('tblStaffInfo') FROM tblStaffInfo";
                SqlCommand selectCommand = new SqlCommand(selectStatement, con);

                ID = Convert.ToInt32(selectCommand.ExecuteScalar());
                //ContractID = Convert.ToInt32(selectCommand.ExecuteScalar());
                //////lblMessage.Text = added.ToString() + " record inserted.";

                //  lblMessage.Text = " Record Created.";


                // Session["EmployeeID"] = added.ToString();


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
                WaterCorp.Logic.ExceptionUtility.LogException(sqlEx, "Save New StaffInfo");


            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {

                //clsSMS sms = new clsSMS();
                //string message = "Dear, " + txtFirstname.Text + " Welcome to KADSWAC your DPCNo is  : " + dpcno.ToString();
                //string phoneno = txtMobile.Text;
                //// phoneno = "08080788332";

                ////format phone No so that it becomes 2348035982461 and not 23408035982461
                //string formatedphoneno = phoneno.Substring(1, 10);
                //sms.SendSMS(message,formatedphoneno);

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "tblStaffInfo"; // Replace with the actual table name
                int recordId = ID; // Replace with the actual record identifier
                string action = "Create Staff"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("ViewStaffInfo.aspx");


            }


        }
    }
}