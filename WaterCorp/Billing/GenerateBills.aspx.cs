using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Threading.Tasks;
using System.Configuration;

namespace WaterCorp.Billing
{
    public partial class GenerateBills : System.Web.UI.Page
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Black;
            lblMessage.Font.Bold = false;

            // Get the billing period from your input, assuming it's in a TextBox named BillingPeriodTextBox
            DateTime billingPeriod;
            if (!DateTime.TryParse(txtBillingPeriod.Text, out billingPeriod))
            {
                // Handle invalid date format
                return;
            }

            // Check if the billing period already exists asynchronously
            if (await BillingPeriodExistsAsync(billingPeriod))
            {
                // Display a message to the user that the bill has already been generated
                lblMessage.Text = "The bill for the selected period has already been generated.";
                lblMessage.ForeColor = System.Drawing.Color.CadetBlue;
                lblMessage.Font.Bold = true;
            }
            else
            {
                // Execute the code asynchronously in another method (you can replace this with your actual method call)
                await ExecuteWaterBillGenerationAsync(billingPeriod);
            }
        }

        private async Task<bool> BillingPeriodExistsAsync(DateTime billingPeriod)
        {
            // Connection string (replace with your actual connection string)
            //string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to check if the billing period exists
                string query = "SELECT COUNT(*) FROM WaterBills WHERE BillingPeriod = @BillingPeriod";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BillingPeriod", billingPeriod);

                    await connection.OpenAsync();

                    // Execute the query asynchronously
                    int count = (int)await command.ExecuteScalarAsync();

                    // If count is greater than 0, the billing period already exists
                    return count > 0;
                }
            }
        }

        private async Task ExecuteWaterBillGenerationAsync(DateTime billingPeriod)
        {
            try
            {
                // Call the stored procedure to generate the water bill
                await GenerateMonthlyWaterBillAsync(billingPeriod);

                // After processing is complete, update the UI or perform any additional tasks
                lblMessage.Text = "Water bill generated successfully!";
                lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
                lblMessage.Font.Bold = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                lblMessage.Text = $"Error generating water bill: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Font.Bold = true;
            }
        }

        private async Task GenerateMonthlyWaterBillAsync(DateTime billingPeriod)
        {
            // Connection string (replace with your actual connection string)
          //  string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //GenerateMonthlyWaterBill7
                using (SqlCommand command = new SqlCommand("GenerateMonthlyMeteredWaterBill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BillingPeriod", billingPeriod);
                    command.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);

                    await connection.OpenAsync();

                    // Execute the stored procedure asynchronously
                    await command.ExecuteNonQueryAsync();
                }
            }
        }



        //private async Task<int> ExecuteNonQueryAsync(SqlCommand cmd)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        await con.OpenAsync();
        //        cmd.Connection = con;
        //        return await cmd.ExecuteNonQueryAsync();
        //    }
        //}

        //protected async void btnSave_Click(object sender, EventArgs e)
        //{




        //    string insertSQL = "GenerateMonthlyWaterBill";

        //    using (SqlCommand cmd = new SqlCommand(insertSQL))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@BillingPeriod", Convert.ToDateTime(txtBillingPeriod.Text));

        //        try
        //        {
        //            int added = await ExecuteNonQueryAsync(cmd);

        //            // Optionally, update UI or show a success message
        //            lblMessage.Text = $"{added} records generated successfully!";
        //        }
        //        catch (Exception err)
        //        {
        //            lblMessage.Text = $"Error: {err.Message}";
        //        }
        //    }




        //}



        //protected void btnSave_Click(object sender, EventArgs e)
        //{


        //    string insertSQL = "spGenerateBill";



        //    SqlConnection con = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand(insertSQL, con);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    // Add the parameters.
        //    // cmd.Parameters.AddWithValue("@CustomerID", uniqueGuidString);
        //    cmd.Parameters.AddWithValue("@BillingDate", Convert.ToDateTime(txtBillingPeriod.Text));





        //    int added = 0;

        //    try
        //    {


        //        con.Open();
        //        added = cmd.ExecuteNonQuery();






        //    }
        //    catch (Exception err)
        //    {
        //        lblMessage.Text = err.Message;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
    }
}