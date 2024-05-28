using Irony;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class VerifyCustomersInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UpdateInfo(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnPayroll = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnPayroll.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;
            //Redirect to other Page.
            Response.Redirect("CustomerUpdates.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void VerifyInfo(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnPayroll = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnPayroll.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;


            int recordIDIndex = GetColumnIndexByName(GridView1, "RecordID");

            // Get the RecordID from the GridView Row using the index.
            string recordID = row.Cells[recordIDIndex].Text;

            // Call the stored procedure to update the data in the database.
            UpdateCustomerInfo(recordID);
        }

        private int GetColumnIndexByName(GridView grid, string columnName)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i] is BoundField && ((BoundField)grid.Columns[i]).DataField.Equals(columnName))
                {
                    return i;
                }
            }
            return -1;
        }

        private void UpdateCustomerInfo(string recordID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spVerifyCustomers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecordID", Int32.Parse(recordID));
                        cmd.Parameters.AddWithValue("@Verified", true);
                        cmd.Parameters.AddWithValue("@VerifiedBy", User.Identity.Name);
                        cmd.Parameters.AddWithValue("@DateVerified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Active", 1);

                        // Add other parameters as needed...

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Rebind the GridView after the update.
                GridView1.DataBind();

                // Display success message.
                lblMessage.Text = "Verification successful!";
                lblMessage.ForeColor = System.Drawing.Color.Green; // Set color to green or any other color.
            }
            catch (Exception ex)
            {
                // Handle exceptions, display error message, log, etc.
                lblMessage.Text = "Error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red; // Set color to red or any other color.
            }
        }


    }
}