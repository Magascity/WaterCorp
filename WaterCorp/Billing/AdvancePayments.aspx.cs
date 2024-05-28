using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Billing
{
    public partial class AdvancePayments : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Your code to make a payment using the stored procedure
            string dpcNo = txtDpcNo.Text;
            DateTime paymentDate = DateTime.Now;
            decimal paymentAmount;

            //if (DateTime.TryParse(txtBillingPeriod.Text, out paymentDate) && decimal.TryParse(txtAmountDue.Text, out paymentAmount))
            if (decimal.TryParse(txtAmountDue.Text, out paymentAmount))
            {
                MakePayment(dpcNo, paymentDate, paymentAmount);
            }
            else
            {
                // Handle invalid date or amount format
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            // Your code to search for outstanding bills based on DPCNo
            string dpcNo = txtDpcNo.Text;
            DataTable outstandingBills = GetOutstandingBills(dpcNo);

            // Bind the result to the GridView
            GridView1.DataSource = outstandingBills;
            GridView1.DataBind();

            // Display the total outstanding amount in the payment textbox
            decimal totalOutstanding = CalculateTotalOutstanding(outstandingBills);
            txtAmountDue.Text = totalOutstanding.ToString("N2");
        }

        private DataTable GetOutstandingBills(string dpcNo)
        {
            // Your code to retrieve outstanding bills from the database
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT BillingPeriod, OutstandingCharges FROM WaterBills WHERE DPCNo = @DPCNo AND OutstandingCharges > 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DPCNo", dpcNo);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        private decimal CalculateTotalOutstanding(DataTable outstandingBills)
        {
            // Your code to calculate the total outstanding amount
            decimal totalOutstanding = 0;

            foreach (DataRow row in outstandingBills.Rows)
            {
                totalOutstanding += Convert.ToDecimal(row["OutstandingCharges"]);
            }

            return totalOutstanding;
        }

        private void MakePayment(string dpcNo, DateTime paymentDate, decimal paymentAmount)
        {
            // Your code to call the stored procedure and make a payment
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spUpdatePaymentInformation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DPCNo", dpcNo);
                    command.Parameters.AddWithValue("@PaymentDate", paymentDate);
                    command.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                    // Add other parameters as needed

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Refresh the outstanding bills after the payment
            btnSearch_Click(null, EventArgs.Empty);
        }

        private void LoadCustomerOutstandingBills()
        {
            // Retrieve customer's outstanding bills and bind to GridView
            // Use your connection string
            //string connectionString = "YourConnectionString";
            string dpcNo = txtDpcNo.Text; // Replace with the actual DPCNo

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Calculate the total due for the customer
                string totalDueQuery = "SELECT SUM(CurrentCharge + OutstandingCharges) AS TotalDue FROM WaterBills WHERE DPCNo = @DPCNo AND OutstandingCharges > 0";
                using (SqlCommand totalDueCommand = new SqlCommand(totalDueQuery, connection))
                {
                    totalDueCommand.Parameters.AddWithValue("@DPCNo", dpcNo);

                    object totalDue = totalDueCommand.ExecuteScalar();
                    

                    // Display the total due amount in the TextBox or Label, wherever you want to show it
                    txtAmountDue.Text = totalDue != DBNull.Value ? totalDue.ToString() : "0";
                }

                // Now, retrieve and bind the detailed billing information to the GridView
                string detailedBillsQuery = "SELECT WaterBillID, BillingPeriod, CurrentCharge, OutstandingCharges FROM WaterBills WHERE DPCNo = @DPCNo AND OutstandingCharges > 0";
                using (SqlCommand detailedBillsCommand = new SqlCommand(detailedBillsQuery, connection))
                {
                    detailedBillsCommand.Parameters.AddWithValue("@DPCNo", dpcNo);

                    SqlDataAdapter adapter = new SqlDataAdapter(detailedBillsCommand);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void ddlModeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddlModeOfPayment.SelectedItem.Value == "Cash")
            {
                txtTransRef.Text = "N/A";
            }

            

        }
    }
}