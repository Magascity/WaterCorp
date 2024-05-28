using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Billing
{
    public partial class CreateBusinessDistricts : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        long id;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            

            string insertSQL = "spInsertSbu";

            
           
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            cmd.Parameters.AddWithValue("@DistrictName", txtDistrictName.Text);
            cmd.Parameters.AddWithValue("@Code", txtDistrictCode.Text);
            
            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();



                string selectStatement = "Select IDENT_CURRENT('_tblSbus') FROM _tblSbus";
                SqlCommand selectCommand = new SqlCommand(selectStatement, con);

                id = Convert.ToInt32(selectCommand.ExecuteScalar());
                


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
                WaterCorp.Logic.ExceptionUtility.LogException(sqlEx, "Create SBU");


            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {

                
                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "[_tblSbus"; // Replace with the actual table name
                long recordId = id; // Replace with the actual record identifier
                string action = "Create SBU"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("ViewBusinessDistricts.aspx");


            }


        }
    }
}