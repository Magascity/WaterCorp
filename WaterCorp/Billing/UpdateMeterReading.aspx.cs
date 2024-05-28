using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace WaterCorp.Billing
{
    public partial class UpdateMeterReading : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Int32 ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillBusinessUnits();
                //FillTarrifRates();
                //FillDistrictCodes();




                if (Session["Row"] != null)
                {
                    //Fetch the GridView Row from Session.
                    GridViewRow row = Session["Row"] as GridViewRow;

                    //Fetch and display the Cell values.
                    // HFEmployeeID.Value = row.Cells[0].Text;
                    // txtRecordID.Text = row.Cells[1].Text;
                    //txtLastname.Text = row.Cells[2].Text;
                    txtDpcNo.Text = row.Cells[17].Text;
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                    
                }

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // 

            //  varchar(50)
            //, date
            //,  decimal(18, 2)
            //,@CreatedBy nvarchar(256)
            //,@DateCreated datetime

            
            string insertSQL = "spInsertMeterReadings";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            // cmd.Parameters.AddWithValue("@CustomerID", uniqueGuidString);
            cmd.Parameters.AddWithValue("@DPCno", txtDpcNo.Text);
            cmd.Parameters.AddWithValue("@BillingPeriod", Convert.ToDateTime(txtBillingPeriod.Text));
            cmd.Parameters.AddWithValue("@MeterReading", Convert.ToDecimal(txtMeterReading.Text));
            cmd.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);




            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();



                string selectStatement = "Select IDENT_CURRENT('MeterReadings') FROM MeterReadings";
                SqlCommand selectCommand = new SqlCommand(selectStatement, con);

                ID = Convert.ToInt32(selectCommand.ExecuteScalar());
               


            }
            catch (Exception err)
            {
                lblMessage.Text = err.Message;
            }
            finally
            {
                con.Close();
            }

            if (added > 0)
            {

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "_tblCustomers3"; // Replace with the actual table name
                Int32 recordId = ID; // Replace with the actual record identifier
                string action = "Create Customer"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("ViewMeterReading.aspx");


            }
        }
    }
}