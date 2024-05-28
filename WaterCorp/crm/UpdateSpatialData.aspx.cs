using Irony;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class UpdateSpatialData : System.Web.UI.Page
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Int32 ID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                if (Session["Row"] != null)
                {
                    //Fetch the GridView Row from Session.
                    GridViewRow row = Session["Row"] as GridViewRow;


                    HFRecordID.Value = row.Cells[0].Text;
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                   
                }


            }
        }
        protected void SaveLocationButton_Click(object sender, EventArgs e)
        {
            decimal latitude = Convert.ToDecimal(LatitudeHiddenField.Value);
            decimal longitude = Convert.ToDecimal(LongitudeHiddenField.Value);


            


    

            string insertSQL = "spUpdateGeoLocations";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            cmd.Parameters.AddWithValue("@RecordID", HFRecordID.Value);
            cmd.Parameters.AddWithValue("@Latitude", latitude);
            cmd.Parameters.AddWithValue("@Longitude", longitude);
            



            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();



                string selectStatement = "Select IDENT_CURRENT('_tblCustomers3') FROM _tblCustomers3";
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
                WaterCorp.Logic.ExceptionUtility.LogException(sqlEx, "Update Customer Info");


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
                string action = "Update Staff"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("SearchCustomers.aspx");


            }




        }
    }

    
}