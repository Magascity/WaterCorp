using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class UpdateMetering : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FillMeterNo();


                if (Session["Row"] != null)
                {
                    //Fetch the GridView Row from Session.
                    GridViewRow row = Session["Row"] as GridViewRow;

                    //Fetch and display the Cell values.
                    // HFEmployeeID.Value = row.Cells[0].Text;
                    // txtRecordID.Text = row.Cells[1].Text;
                    //txtLastname.Text = row.Cells[2].Text;
                    HFRecordID.Value = row.Cells[0].Text;
                    //chkMetered.Checked = (bool)row.Cells[12].Text;
                    // Session["EmployeeID"] = txtEmployeeID.Text;
                   LoadCustomers(Int32.Parse(HFRecordID.Value));
                }

                //chkContract.Checked = (bool)reader["contract"];
            }

        }
        private void LoadCustomers(Int64 RecordID)
        {



            // Define ADO.NET objects.
            string selectSQL;
            selectSQL = "SELECT [RecordID] ,[LastName] ,[Firstname] ,[Email],[Phonenumber],[CustomerAddress],[DistrictCode], [ZoneCode],[Subzone],[Round] ,[Foliono],[Metered],[MeterNo],[MeterCharge],[FlateRateCategory],[Tarrif],[Consumption],[CustReference] FROM [_tblCustomers3]";
            selectSQL += " WHERE RecordID = " + RecordID;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                txtRecordID.Text = reader["RecordID"].ToString();
                txtLastname.Text = reader["LastName"].ToString();
                txtFirstname.Text = reader["Firstname"].ToString();
                txtEmail.Text = reader["Email"].ToString();
                txtMobile.Text = reader["Phonenumber"].ToString();
                txtCustomerAddress.Text = reader["CustomerAddress"].ToString();
                txtDpcNo.Text = reader["CustReference"].ToString();
                chkMetered.Checked = (bool)reader["Metered"];
                if(chkMetered.Checked == true)
                {
                    chkMetered.Text = " Metered";
                    //lblMetered.Text = "Metered";
                }
                else
                {
                    chkMetered.Text = " Un-Metered";
                   // lblMetered.Text = "Un-Metered";
                }
                txtMeterNo.Text = reader["MeterNo"].ToString();


                reader.Close();
                lblMessage.Text = "";


            }
            catch (Exception err)
            {
                lblMessage.Text = "Error Tax Payer ";
                lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }

        }


        private void FillMeterNo()
        {
            ddlMeterNo.Items.Clear();
            ddlMeterNo.Items.Insert(0, new ListItem("--Select Meter--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [MeterID], [MeterName] ,[MeterModel],[Manufacturer],[MeterNo] FROM [tblMeterRecords] where Isssued = 0 ";

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
                    newItem.Text = reader["MeterName"] + " : " + reader["MeterModel"] + " : " + reader["Meterno"] + " : " + reader["Manufacturer"].ToString();
                    newItem.Value = reader["Meterno"].ToString();
                    ddlMeterNo.Items.Add(newItem);
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


        private void GetMeterCharge()
        {

            string selectSQL;
            selectSQL = "SELECT [MeterCharge] FROM [_tblMeterCharges] where Active = 1 ";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read();

                // Fill the controls.


                HFCharges.Value = reader["MeterCharge"].ToString();


                reader.Close();
                //lblResults.Text = "";

            }
            catch (Exception err)
            {
                //  lblResults.Text = "Error getting author. ";
                // lblResults.Text += err.Message;
            }
            finally
            {
                con.Close();
            }



        }
        protected void ddlMeterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMeterCharges.Text = "0.0";
            // Hide all panels by default
            PnlMeters.Visible = false;
           // pnlCharges.Visible = false;

            // Check the selected item and show the corresponding panel
            if (ddlMeterStatus.SelectedValue == "Metered")
            {
                PnlMeters.Visible = true;
               // pnlCharges.Visible = true;
                GetMeterCharge();
                txtMeterCharges.Text = HFCharges.Value;
                FillMeterNo();

            }
            else if (ddlMeterStatus.SelectedValue == "Un-Metered")
            {
                //pnlCharges.Visible = true;
                PnlMeters.Visible = false;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}