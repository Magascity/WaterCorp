using Irony;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Interop;

namespace WaterCorp.crm
{
    public partial class AssignStafftoCustomers : System.Web.UI.Page
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
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("--Select Business Unit--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT  [Id] ,[DistrictName] ,[Code] FROM [_tblSbus]";

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
                    newItem.Text = reader["DistrictName"].ToString();
                    newItem.Value = reader["Code"].ToString();
                    ddlDistrict.Items.Add(newItem);
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

        private void FillStaffInfo()
        {
            ddlStaff.Items.Clear();
            ddlStaff.Items.Insert(0, new ListItem("--Select Staff--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT S.RecordID, S.Lastname, S.Firstname, (Select Code from _tblSbus where Id= S.SBU) SBU FROM [tblStaffInfo] S where SBU = '" + ddlDistrict.SelectedItem.Value + "'";

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
                    newItem.Text = reader["Lastname"] + " " + reader["Firstname"].ToString();
                    newItem.Value = reader["SBU"].ToString();
                    ddlStaff.Items.Add(newItem);
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


        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStaffInfo();
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            string str = string.Empty;

            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                if (chk != null & chk.Checked)
                {
                    //  str += "<b>RecordID :- </b>" + gvrow.Cells[1].Text + ", ";
                    // str += "<b>Customer Name :- </b>" + gvrow.Cells[2].Text + ", ";
                    //str += "<b>Company :- </b>" + gvrow.Cells[3].Text + ", ";
                    //str += "<b>Addess :- </b>" + gvrow.Cells[4].Text; Email
                    //str += "<br />";
                    //message = "Dear " + gvrow.Cells[2].Text + ", This is wishing you a fabulous Birthday Celebration.";
                    UpdateAssignedTo(gvrow.Cells[1].Text, ddlStaff.SelectedItem.Text);
                }
            }
           // lblMessage.Text = "<b>Selected EmpDetails: </b>" + str + " : ";
            lblMessage.Text = ddlStaff.SelectedItem.Text + " Assigned Successfully !";
        }

        private void UpdateAssignedTo(string recordID, string assignedTo)
        {
            string updateSQL = "UPDATE _tblCustomers3 SET assignedTo = @AssignedTo WHERE RecordID = @RecordID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateSQL, con))
                {
                    cmd.Parameters.AddWithValue("@AssignedTo", assignedTo);
                    cmd.Parameters.AddWithValue("@RecordID", recordID);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error updating assignedTo field: " + ex.Message;
                    }
                }
            }

        }
    }
}