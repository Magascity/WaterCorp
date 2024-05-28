using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Billing
{
    public partial class GenerateMeterBilling : System.Web.UI.Page
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDistrictCodes();
            }
        }

        private void FillDistrictCodes()
        {
            ddlDistrictCode.Items.Clear();
            ddlDistrictCode.Items.Insert(0, new ListItem("--Select Business Unit--", "0"));

            string selectSQL = "SELECT [Id], [DistrictName], [Code] FROM [_tblSbus]";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["DistrictName"].ToString();
                    newItem.Value = reader["Code"].ToString();
                    ddlDistrictCode.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                lblMessage.Text = "Error reading Faculty";
                lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        [WebMethod]
        public static void GenerateMonthlyUnmeteredWaterBill(DateTime billingPeriod, int districtCode, string createdBy)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GenerateMonthlyUnmeteredWaterBill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillingPeriod", billingPeriod);
                    cmd.Parameters.AddWithValue("@DistrictCode", districtCode);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            DateTime billingPeriod;
            if (DateTime.TryParse(txtBillingPeriod.Text, out billingPeriod))
            {
                GenerateMonthlyUnmeteredWaterBill(billingPeriod, int.Parse(ddlDistrictCode.SelectedValue), User.Identity.Name);
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid billing period format.";
            }
        }
    }
}