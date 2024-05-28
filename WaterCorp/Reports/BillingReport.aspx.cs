using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Reports
{
    public partial class BillingReport : System.Web.UI.Page
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
            ddlDistrictCodes.Items.Clear();
            ddlDistrictCodes.Items.Insert(0, new ListItem("--Select Business Unit--", "0"));


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
                    ddlDistrictCodes.Items.Add(newItem);
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



            PnlReport.Visible = true;

            PrintReport();

        }


        private void PrintReport()
        {
            ReportViewer1.Visible = true;

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/WaterBill.rdlc");

            // DSConfirmation dsCustomers = GetData("Select D.TIN, (D.Lastname + ' ' + D.Othernames) Fullname, D.Mobile1 as Mobile, D.emailaddress as Email, T.AmountPaid,T.PaymentType, T.TransactionType, T.Transref from _tblIndividual D join _tblTransactions T on D.TIN = T.TIN where T.PaymentReference = '482817245'");// + txtTransRef.Text + "'");
            DsBillings dsCustomers = GetData("SELECT [WaterBillID] ,[DPCNo] ,[CustomerName] ,[Address] ,[Mobile] ,[Email] ,[MeterNo] ,[MeterCharge],[CustomerID] ,[FlatRateCategory] ,[BillingPeriod]  ,[PresentReading]  ,[PreviousReading] ,[Consumption] ,[CurrentCharge] ,[OutstandingCharges] ,[LastPaymentDate] ,[LastPaymentAmount] ,[TotalDue] ,[PaymentDate],[PaymentAmount] FROM [WaterBills] group by [WaterBillID] ,[DPCNo] ,[CustomerName] ,[Address] ,[Mobile] ,[Email] ,[MeterNo] ,[MeterCharge],[CustomerID] ,[FlatRateCategory] ,[BillingPeriod]  ,[PresentReading]  ,[PreviousReading] ,[Consumption] ,[CurrentCharge] ,[OutstandingCharges] ,[LastPaymentDate] ,[LastPaymentAmount] ,[TotalDue] ,[PaymentDate],[PaymentAmount] ");
            //DsBillings dsCustomers = GetData("SELECT [WaterBillID] ,[DPCNo] ,[CustomerName] ,[Address] ,[Mobile] ,[Email] ,[MeterNo] ,[MeterCharge],[CustomerID] ,[FlatRateCategory] ,[BillingPeriod]  ,[PresentReading]  ,[PreviousReading] ,[Consumption] ,[CurrentCharge] ,[OutstandingCharges] ,[LastPaymentDate] ,[LastPaymentAmount] ,[TotalDue] ,[PaymentDate],[PaymentAmount] FROM [WaterBills] WHERE LEFT([DPCNo], 2) = '" + ddlDistrictCodes.SelectedItem.Value + "'");
            // dspayslip dsCustomers = GetData("SELECT E.[EmployeeID],(E.[FirstName] + ' ' +E.[Initials] + ' ' + E.[LastName]) Fullname,(Select JobTitle from _tblJobTitle where TitleID = E.[JobTitle]) JobTitle ,(Select Location from _tblLocation where LocationID = E.[Location]) Location  ,(Select BankName from _tblbanklist where ID = E.[BankName]) Bank  ,E.[BankAccount] , (Select Category from _tblCategories where ID = P.[Category]) Category,P.Year ,P.Month  ,P.PayItem ,P.PayAmount FROM [_tblEmployees] E  Right outer join _tblPayrole P on E.EmployeeID = P.EmployeeID  Where P.Month  = '" + ddlMonth.Text + "' and  P.Year = '" + ddlPayYear.Text + "' and E.EmployeeID = " + Convert.ToInt32(txtEmployeeID.Text));
            ReportDataSource datasource = new ReportDataSource("DsWaterBill", dsCustomers.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        private DsBillings GetData(string query)
        {
            // string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    using (DsBillings dsCustomers = new DsBillings())
                    {
                        sda.Fill(dsCustomers, "DataTable1");
                        return dsCustomers;
                    }
                }
            }
        }
    }
}