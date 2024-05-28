using DocumentFormat.OpenXml.Math;

using Irony;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Interop;

namespace WaterCorp.Billing
{
    public partial class ViewBillbyAccount : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected List<UnpaidBill> UnpaidBills = new List<UnpaidBill>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnPayAll.Visible = false;
                UnpaidBills = new List<UnpaidBill>();
                CalculateAndDisplayTotalAmountDue();
            }
        }


        public class UnpaidBill
        {
            public string DPCNo { get; set; }
            public DateTime BillingPeriod { get; set; }
            public decimal CurrentCharge { get; set; }
        }

       
        protected void Assess(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnPayBill = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnPayBill.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;
            //Redirect to other Page.
            Session["CustomerName"] = txtCustomerName.Text;
            Session["Email"] = txtCustomerEmail.Text;
            Session["Mobile"] = txtCustomerPhone.Text;
            Response.Redirect("PayBills.aspx");
            // Response.Redirect("Da.aspx");

           


        }



        //private void CalculateAndDisplayTotalAmountDue()
        //{
        //    decimal totalAmountDue = 0;

        //    foreach (GridViewRow row in GridView1.Rows)
        //    {
        //        bool isPaid = Convert.ToBoolean(DataBinder.Eval(row.DataItem, "Paid"));

        //        if (!isPaid)
        //        {
        //            // Retrieve the CurrentCharge for unpaid bills
        //            decimal currentCharge = Convert.ToDecimal(DataBinder.Eval(row.DataItem, "CurrentCharge"));

        //            // Add the CurrentCharge to the total amount due
        //            totalAmountDue += currentCharge;
        //        }
        //    }

        //    lblTotalAmount.Text = $"Total Amount Due: ${totalAmountDue:F2}";
        //}


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

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        // Find the cell containing the "Paid" field
        //        TableCell paidCell = e.Row.Cells[GridView1.Columns.IndexOf(GridView1.Columns.OfType<BoundField>().Single(c => c.DataField == "Paid"))];

        //        // Check the value of the "Paid" field
        //        bool isPaid = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Paid"));

        //        // Set the color of the cell based on the "Paid" status
        //        paidCell.ForeColor = isPaid ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        //    }
        //}

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        //DataRowView dataItem = (DataRowView)e.Row.DataItem;

        //        //// Check if the customer is metered based on the presence of MeterNo
        //        //bool isMetered = !string.IsNullOrEmpty(dataItem["MeterNo"].ToString());

        //        //// Hide or show the columns based on whether the customer is metered or not
        //        //e.Row.Cells[GridView1.Columns.IndexOf(GridView1.Columns.OfType<BoundField>().Single(c => c.DataField == "PreviousReading"))].Visible = isMetered;
        //        //e.Row.Cells[GridView1.Columns.IndexOf(GridView1.Columns.OfType<BoundField>().Single(c => c.DataField == "PresentReading"))].Visible = isMetered;

        //        // Find the cell containing the "Paid" field
        //        TableCell paidCell = e.Row.Cells[GridView1.Columns.IndexOf(GridView1.Columns.OfType<BoundField>().Single(c => c.DataField == "Paid"))];

        //        // Check the value of the "Paid" field
        //        bool isPaid = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Paid"));

        //        // Set the color and text of the cell based on the "Paid" status
        //        if (isPaid)
        //        {
        //            paidCell.BackColor = System.Drawing.Color.Green;
        //            paidCell.Text = "Paid";
        //        }
        //        else
        //        {
        //            paidCell.BackColor = System.Drawing.Color.Red;
        //            paidCell.Text = "Not Paid";
        //        }

        //        // Find the "Paid" field value
        //        //bool isPaid = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Paid"));

        //        // Find the "Button1" control
        //        Button btnPayBill = (Button)e.Row.FindControl("Button1");

        //        // Disable the button if the bill is already paid
        //        btnPayBill.Enabled = !isPaid;

        //        // Optionally, you can change the appearance based on the paid status
        //        if (isPaid)
        //        {
        //            btnPayBill.CssClass = "btn btn-success disabled"; // Change to a suitable class for disabled and paid button
        //        }


        //        // Assuming you have a class named UnpaidBill to represent bill details
        //        UnpaidBill unpaidBill = new UnpaidBill();
        //        unpaidBill.DPCNo = DataBinder.Eval(e.Row.DataItem, "DPCNo").ToString();
        //        unpaidBill.BillingPeriod = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "BillingPeriod"));
        //        unpaidBill.CurrentCharge = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CurrentCharge"));
        //        // Add other properties as needed

        //        // Check if the bill is unpaid
        //        if (DataBinder.Eval(e.Row.DataItem, "Paid").ToString() == "False")
        //        {
        //            UnpaidBills.Add(unpaidBill);
        //        }

        //    }
        //}


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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

                // Find the "Button1" control
                Button btnPayBill = (Button)e.Row.FindControl("Button1");

                // Disable the button if the bill is already paid
                btnPayBill.Enabled = !isPaid;

                // Optionally, you can change the appearance based on the paid status
                if (isPaid)
                {
                    btnPayBill.CssClass = "btn btn-success disabled"; // Change to a suitable class for disabled and paid button
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


        // Method to retrieve unpaid bills from the database
        private List<UnpaidBill> GetUnpaidBills(string dpcNo)
        {
            List<UnpaidBill> unpaidBills = new List<UnpaidBill>();

            // Assuming you have a database connection and command setup
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT 
                [DPCNo],
                [BillingPeriod],
                [CurrentCharge]
            FROM 
                [WaterBills] 
            WHERE 
                DPCNo = @DPCNo
                AND Paid = 0
                AND (PaymentAmount IS NULL OR PaymentDate IS NULL)
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DPCNo", dpcNo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UnpaidBill unpaidBill = new UnpaidBill
                            {
                                DPCNo = reader["DPCNo"].ToString(),
                                BillingPeriod = Convert.ToDateTime(reader["BillingPeriod"]),
                                CurrentCharge = Convert.ToDecimal(reader["CurrentCharge"]),
                                // Add other properties as needed
                            };

                            unpaidBills.Add(unpaidBill);
                        }
                    }
                }
            }

            return unpaidBills;
        }


        protected void btnPayAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                // Assuming PaymentAmount TextBox is in the 10th cell (index 9)
                TableCell paymentAmountCell = row.Cells[9];
                TextBox txtPaymentAmount = paymentAmountCell.Controls.OfType<TextBox>().FirstOrDefault();

                if (txtPaymentAmount != null)
                {
                    string paymentAmount = txtPaymentAmount.Text.Trim();

                    // Check if PaymentAmount is 0 or null or empty
                    if (paymentAmount == "0" || string.IsNullOrEmpty(paymentAmount) || paymentAmount.ToLower() == "null")
                    {
                        // Display an alert using JavaScript
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertScript",
                            $"alert('Payment Amount is 0 or empty for row {row.RowIndex + 1}');", true);
                    }
                }
            }
        }





        private void RecordBatchPayment(DataTable payments, string createdBy, DateTime dateCreated)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Generate a single SequenceNumber for the entire batch
                int batchSequenceNumber;
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(SequenceNumber), 0) + 1 FROM PaymentTransactions", connection))
                {
                    batchSequenceNumber = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Pass the batchSequenceNumber to spRecordPayment stored procedure
                using (SqlCommand cmd = new SqlCommand("spRecordBatchPayment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Payments", payments);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                    cmd.Parameters.AddWithValue("@DateCreated", dateCreated);
                    cmd.Parameters.AddWithValue("@BatchSequenceNumber", batchSequenceNumber);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        


    }
}