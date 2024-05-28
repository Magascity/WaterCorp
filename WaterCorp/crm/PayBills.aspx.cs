using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WaterCorp;

namespace KadswacOnline.Customer
{
    public partial class PayBills : System.Web.UI.Page
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
                    txtBillID.Text = row.Cells[0].Text;
                    txtDPCNo.Text = row.Cells[1].Text;
                    txtAmountDue.Text = row.Cells[7].Text;
                    //txtCustomerName.Text = row.Cells[2].Text;
                    txtBillingPeriod.Text = row.Cells[3].Text;
                    //txtEmail.Text = row.Cells[4].Text;
                    //txtMobile.Text = row.Cells[3].Text;
                    HFID.Value = row.Cells[0].Text;
                    HFBillingPeriod.Value = row.Cells[3].Text;

                    // Session["EmployeeID"] = txtEmployeeID.Text;

                }

                txtEmail.Text = Session["Email"].ToString();
                txtMobile.Text = Session["Mobile"].ToString();
                txtCustomerName.Text = Session["CustomerName"].ToString();


            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            string insertSQL = "spRecordPayment";
            



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            // cmd.Parameters.AddWithValue("@CustomerID", uniqueGuidString);
            cmd.Parameters.AddWithValue("@DPCNo", txtDPCNo.Text);
            cmd.Parameters.AddWithValue("@BillingPeriod", Convert.ToDateTime(HFBillingPeriod.Value));
            cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@PaymentAmount", Convert.ToDecimal(txtAmountDue.Text));
            cmd.Parameters.AddWithValue("@Paymentmethod", ddlModeOfPayment.SelectedItem.Value);


            if (ddlModeOfPayment.SelectedValue == "Cash")
            {
                cmd.Parameters.AddWithValue("@PayRef", string.Empty);

            }
            else
            {
                cmd.Parameters.AddWithValue("@PayRef", txtTransRef.Text);

            }




            cmd.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);




            int added = 0;

            try
            {


                con.Open();
                added = cmd.ExecuteNonQuery();
                


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

                decimal amount = Convert.ToDecimal(txtAmountDue.Text); // Replace this with the actual amount from your txtAmount.Text

                // Format the amount to Nigerian currency (NGN)
                string formattedAmount = string.Format(new System.Globalization.CultureInfo("ha-Latn-NG"), "{0:C}", amount);


                clsSMS sms = new clsSMS();
                string message = "Dear, " + txtCustomerName.Text + " Your Bill for the period " + txtBillingPeriod.Text + " for the sum of " + formattedAmount + " as being recieved!";
                string phoneno = txtMobile.Text;
                // phoneno = "08080788332";

                //format phone No so that it becomes 2348035982461 and not 23408035982461
                string formatedphoneno = phoneno.Substring(1, 10);
                sms.SendSMS(message, formatedphoneno);

                string userId = User.Identity.Name; // Replace with the actual user identifier
                string tableName = "PaymentTransactions"; // Replace with the actual table name
                int recordId = Int32.Parse(HFID.Value); // Replace with the actual record identifier
                string action = "Customer Bill"; // Replace with the actual action performed (e.g., Insert, Update, Delete)

                AuditTrailManager auditTrailManager = new AuditTrailManager();
                auditTrailManager.LogAuditTrail(userId, tableName, recordId, action);


                // InsertBillingStatus();

                //btnPayroll.Visible = true;
                Response.Redirect("PaymentReceipt.aspx");


            }



        }

        protected void ddlModeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlRef.Visible = false;
            if (ddlModeOfPayment.SelectedItem.Value == "POS")
            {
                PnlRef.Visible = true;
            }

            if (ddlModeOfPayment.SelectedItem.Value == "Transfer")
            {
                PnlRef.Visible = true;
            }




        }
    }
}
