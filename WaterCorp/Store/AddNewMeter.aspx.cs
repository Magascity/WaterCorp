using Irony;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Store
{
    public partial class AddNewMeter : System.Web.UI.Page
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           


            string insertSQL = "spCreateMeters";



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add the parameters.
            cmd.Parameters.AddWithValue("@MeterName", txtMeterName.Text);
            cmd.Parameters.AddWithValue("@MeterModel", txtModel.Text);
            cmd.Parameters.AddWithValue("@Manufacturer", txtManufacturer.Text);
            cmd.Parameters.AddWithValue("@MeterNo", txtMeterNo.Text);            
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


                Response.Redirect("ViewMeterStock");


            }
        }
    }
}