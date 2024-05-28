using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Interop;

namespace WaterCorp.Billing
{
    public partial class ViewOustandings : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                CalculateAndDisplayTotalAmountDue();
            }
        }


        protected void CalculateAndDisplayTotalAmountDue()
        {
            // Get the DPCNo from the textbox
            string dpcNo = txtDPCNo.Text;

            // Ensure the DPCNo is not empty
            if (!string.IsNullOrEmpty(dpcNo))
            {
                // Create a SQL query to sum the CurrentCharge for unpaid bills
                string query = "SELECT ISNULL(SUM(CurrentCharge), 0) AS TotalAmountDue " +
                               "FROM WaterBills " +
                               "WHERE DPCNo = @DPCNo AND Paid = 0";

                // Use a SqlConnection and SqlCommand to execute the query
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@DPCNo", Server.HtmlEncode(txtDPCNo.Text));

                    // Open the connection and execute the query
                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    // Check the result and update the label
                    if (result != null && result != DBNull.Value)
                    {
                        decimal totalAmountDue = Convert.ToDecimal(result);
                        lblTotalAmount.Text = $"Total Amount Due: {totalAmountDue:F2}";
                    }
                    else
                    {
                        lblTotalAmount.Text = "Total Amount Due: 0.00";
                    }
                }
            }
            else
            {
                lblTotalAmount.Text = "Total Amount Due: 0.00";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                            
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

                    if (chkSelect != null)
                    {
                        chkSelect.Attributes["onclick"] = "calculateTotalAmountDue();";
                    }
               

                // Retrieve the "Paid" status directly from the DataBinder
                bool isPaid = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Paid"));

                TableCell paidCell = e.Row.Cells[GridView1.Columns.IndexOf(GridView1.Columns.OfType<BoundField>().Single(c => c.DataField == "Paid"))];

                if (isPaid)
                {
                    paidCell.BackColor = System.Drawing.Color.Green;
                    paidCell.Text = "Paid";
                }
                else
                {
                    paidCell.BackColor = System.Drawing.Color.Red;
                    paidCell.Text = "Not Paid";


                }

             }
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string DpcNo = txtDPCNo.Text;
            LoadCustomers(DpcNo);
        }

        private void LoadCustomers(string DPCno)
        {


            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT [LastName] ,[Firstname],Middlename ,[Email],[Mobile],[CustomerAddress],[DPCno] FROM [_tblCustomers3]";
            selectSQL += " WHERE DPCno = '" + DPCno + "'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    PnlDetails.Visible = true;
                    pnlPayment.Visible = true;
                    //  txtRecordID.Text = reader["RecordID"].ToString();
                    txtCustomerName.Text = reader["LastName"] + " " + reader["firstname"] + " " + reader["Middlename"].ToString();

                    txtCustomerEmail.Text = reader["Email"].ToString();
                    txtCustomerPhone.Text = reader["Mobile"].ToString();
                    txtAddress.Text = reader["CustomerAddress"].ToString();

                    CalculateAndDisplayTotalAmountDue();
                }
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            string script = "$('#mymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
        }




        protected void btnPayAll_Click(object sender, EventArgs e)
        {

            bool atLeastOneRowSelected = false;
            decimal totalPayable = 0;
            
            

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    atLeastOneRowSelected = true;

                    // Assuming CurrentCharge is in the 8th cell (index 7)
                    decimal currentCharge;
                    if (decimal.TryParse(row.Cells[8].Text, out currentCharge))
                    {
                        totalPayable += currentCharge;
                    }
                }
            }

            if (!atLeastOneRowSelected)
            {
                // Display an alert or perform any action for no rows selected
                ScriptManager.RegisterStartupScript(this, GetType(), "alertScript",
                    "alert('Please select at least one row.');", true);
                return;
            }

            // Display the TotalPayable
            lblTotalAmount.Text = "Total Amount: " + totalPayable.ToString("0.00");




            Page.ClientScript.RegisterStartupScript(this.GetType(), "calculateTotalPayableScript",
                 "calculateTotalPayable();", true);


            pnlPaymentMode.Visible = true;



        }

        protected void ddlModeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlRef.Visible = false;
            pnlButton.Visible = true;
            if (ddlModeOfPayment.SelectedItem.Value == "POS")
            {
                PnlRef.Visible = true;
                pnlButton.Visible = true;
            }

            if (ddlModeOfPayment.SelectedItem.Value == "Transfer")
            {
                PnlRef.Visible = true;
                pnlButton.Visible = true;
            }
        }


        protected void LoadAndSetSequenceNumber()
        {
            // Connection string to your database
          //  string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

            // SQL query to retrieve the last sequence number from your table
            string sqlQuery = "SELECT TOP 1 SequenceNumber FROM PaymentTransactions ORDER BY SequenceNumber DESC";

            // Create a new SqlConnection and SqlCommand
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    connection.Open();

                    // Execute the query and retrieve the last sequence number
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        // Increment the last sequence number by 1
                        int lastSequenceNumber = Convert.ToInt32(result) + 1;

                        // Set the value to the hidden field
                        HFSequenceNumber.Value = lastSequenceNumber.ToString();
                    }
                    else
                    {
                        // If no records are present, start with 1
                        HFSequenceNumber.Value = "1";
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadAndSetSequenceNumber();

            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                if (chk != null && chk.Checked)
                {
                    // Assuming the columns are in the order: DPCNo, BillingPeriod, PaymentAmount, PaymentMethod, PayRef, CreatedBy, DateCreated
                    string dpcNo = gvrow.Cells[2].Text; // Assuming DPCNo is in the 2nd cell (index 1)
                    DateTime billingPeriod = Convert.ToDateTime(gvrow.Cells[4].Text); // Assuming BillingPeriod is in the 4th cell (index 3)
                    decimal paymentAmount = Convert.ToDecimal(gvrow.Cells[8].Text); // Assuming PaymentAmount is in the 9th cell (index 8)
                    string paymentMethod = ddlModeOfPayment.SelectedItem.Value; // You need to provide the actual payment method
                    string payRef = txtTransRef.Text; // You need to provide the actual payment reference
                    string createdBy = User.Identity.Name; // You need to provide the actual creator
                    DateTime dateCreated = DateTime.Now; // Assuming current date and time
                    long BillID = long.Parse(gvrow.Cells[1].Text);
                    // Execute the stored procedure
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("spRecordPayment", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the stored procedure
                            command.Parameters.AddWithValue("@DPCNo", dpcNo);
                            command.Parameters.AddWithValue("@BillingPeriod", billingPeriod);
                            command.Parameters.AddWithValue("@PaymentDate", dateCreated); // Assuming payment date is the current date and time
                            command.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                            command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                            command.Parameters.AddWithValue("@PayRef", payRef);
                            command.Parameters.AddWithValue("@CreatedBy", createdBy);
                            command.Parameters.AddWithValue("@DateCreated", dateCreated);
                            command.Parameters.AddWithValue("@SequenceNo", Int32.Parse(HFSequenceNumber.Value));
                            command.Parameters.AddWithValue("@BillID", BillID);


                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

           // HFSequenceNumber.Value = lastSequenceNumber.ToString();
            Session["SequenceNumber"] = HFSequenceNumber.Value;

            Response.Redirect("PaymentReceipt.aspx");
        }
    }

}