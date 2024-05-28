using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KadswacOnline.Customer
{
    public partial class ViewBills : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected List<UnpaidBill> UnpaidBills = new List<UnpaidBill>();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            //string Dpcno = Session["UserDpcNo"].ToString();

            //HFDpcNo.Value = Session["UserDpcNo"].ToString();
            if (!Page.IsPostBack)
            {
                UnpaidBills = new List<UnpaidBill>();


                if (Session["Row"] != null)
                {
                    //Fetch the GridView Row from Session.
                    GridViewRow row = Session["Row"] as GridViewRow;

                    //Fetch and display the Cell values.
                    // HFEmployeeID.Value = row.Cells[0].Text;
                    // txtRecordID.Text = row.Cells[1].Text;
                    //txtLastname.Text = row.Cells[2].Text;
                    HFDpcNo.Value = row.Cells[16].Text;
                    //HFRecordID.Value = row.Cells[0].Text;
                    //chkMetered.Checked = (bool)row.Cells[12].Text;
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                   // LoadCustomers(Int32.Parse(HFRecordID.Value));

                    LoadCustomers();

                    txtDPCNo.Text = HFDpcNo.Value;
                   // HFDpcNo.Value = Session["UserDpcNo"].ToString();

                }


                //LoadCustomers();

                //txtDPCNo.Text = Session["UserDpcNo"].ToString();
                //HFDpcNo.Value = Session["UserDpcNo"].ToString();
                CalculateAndDisplayTotalAmountDue();

              
                btnPayAll.Visible = true;
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
        public string dpcNo;

        protected void CalculateAndDisplayTotalAmountDue()
        {
            // Get the DPCNo from the textbox
           // dpcNo = txtDPCNo.Text;

           

            //dpcNo = "01-87-45-89-40";

            // Ensure the DPCNo is not empty
            if (!string.IsNullOrEmpty(HFDpcNo.Value))
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
                    //command.Parameters.AddWithValue("@DPCNo", Server.HtmlEncode(txtDPCNo.Text));
                    command.Parameters.AddWithValue("@DPCNo", HFDpcNo.Value);

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




        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string DpcNo = txtDPCNo.Text;
        //    LoadCustomers(DpcNo);
        //}

        private void LoadCustomers()
        {


            // Define ADO.NET objects.
            string selectSQL;
            //selectSQL = "SELECT [LastName] ,[Firstname], Othername ,[Email],[Mobile],[CustomerAddress],[DPCno] FROM [_tblCustomers3]";
            selectSQL = " SELECT [LastName] ,[Firstname], Othername ,[Email],[PhoneNumber],[CustomerAddress],CustReference as [DPCno] FROM [_tblCustomers3]";

            selectSQL += " WHERE CustReference = '" + HFDpcNo.Value.ToString() + "'";
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
                    txtCustomerName.Text = reader["LastName"] + " " + reader["firstname"] + " " + reader["Othername"].ToString();

                    txtCustomerEmail.Text = reader["Email"].ToString();
                    txtCustomerPhone.Text = reader["PhoneNumber"].ToString();
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