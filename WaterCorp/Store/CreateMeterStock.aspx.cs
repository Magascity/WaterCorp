using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Store
{
    public partial class CreateMeterStock : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUnit();
                FillStockCategory();
                FillSubStockCategory();
            }
        }

        private void FillStockCategory()
        {
            ddlItemCategory.Items.Clear();
            ddlItemCategory.Items.Insert(0, new ListItem("--Select Item Category--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [CategoryID],[Category],[Description] FROM [tblStockCategory]";

            // Define the ADO.NET objects.
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();


                // list box text, and store the unique ID in the Value property.
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["Category"].ToString();
                    newItem.Value = reader["CategoryID"].ToString();
                    ddlItemCategory.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                //lblMessage.Text = "Error reading Faculty";
                //lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        private void FillSubStockCategory()
        {
            ddlItemSubCategory.Items.Clear();
            ddlItemSubCategory.Items.Insert(0, new ListItem("--Select Item Sub-Category--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [ID] ,[SubCategory] FROM [tblStockSubCategory]";

            // Define the ADO.NET objects.
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();


                // list box text, and store the unique ID in the Value property.
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["SubCategory"].ToString();
                    newItem.Value = reader["ID"].ToString();
                    ddlItemSubCategory.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                //lblMessage.Text = "Error reading Faculty";
                //lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        private void FillUnit()
        {
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("--Select Unit--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [UnitID] ,[Unit] FROM [tblStockUnits]";

            // Define the ADO.NET objects.
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();


                // list box text, and store the unique ID in the Value property.
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["Unit"].ToString();
                    newItem.Value = reader["UnitID"].ToString();
                    ddlUnit.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                //lblMessage.Text = "Error reading Faculty";
                //lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        
    }
}