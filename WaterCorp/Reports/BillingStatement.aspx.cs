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
    public partial class BillingStatement : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CustomerBillingStatement.rdlc");

            // DSConfirmation dsCustomers = GetData("Select D.TIN, (D.Lastname + ' ' + D.Othernames) Fullname, D.Mobile1 as Mobile, D.emailaddress as Email, T.AmountPaid,T.PaymentType, T.TransactionType, T.Transref from _tblIndividual D join _tblTransactions T on D.TIN = T.TIN where T.PaymentReference = '482817245'");// + txtTransRef.Text + "'");
            DsBillingStatement dsCustomers = GetData("SELECT [WaterBillID] ,[DPCNo],[CustomerName] ,[Address] ,Mobile  ,Email ,[MeterNo] ,[CustomerID] ,[BillingPeriod]  ,[PresentReading] ,[PreviousReading] ,[Consumption] ,[CurrentCharge] ,[OutstandingCharges] ,[LastPaymentDate]  ,[LastPaymentAmount] ,[TotalDue] ,[PaymentDate] ,[PaymentAmount]  ,Paid , (SELECT ISNULL(SUM(CurrentCharge), 0)    FROM WaterBills AS wb \r\n     WHERE wb.DPCNo = WaterBills.DPCNo AND wb.Paid = 0) AS TotalAmountDue FROM [WaterBills] WHERE DPCNo  = '" + Server.HtmlEncode(txtDPCNO.Text) + "'" );
            //DsBillings dsCustomers = GetData("SELECT [WaterBillID] ,[DPCNo] ,[CustomerName] ,[Address] ,[Mobile] ,[Email] ,[MeterNo] ,[MeterCharge],[CustomerID] ,[FlatRateCategory] ,[BillingPeriod]  ,[PresentReading]  ,[PreviousReading] ,[Consumption] ,[CurrentCharge] ,[OutstandingCharges] ,[LastPaymentDate] ,[LastPaymentAmount] ,[TotalDue] ,[PaymentDate],[PaymentAmount] FROM [WaterBills] WHERE LEFT([DPCNo], 2) = '" + ddlDistrictCodes.SelectedItem.Value + "'");
            // dspayslip dsCustomers = GetData("SELECT E.[EmployeeID],(E.[FirstName] + ' ' +E.[Initials] + ' ' + E.[LastName]) Fullname,(Select JobTitle from _tblJobTitle where TitleID = E.[JobTitle]) JobTitle ,(Select Location from _tblLocation where LocationID = E.[Location]) Location  ,(Select BankName from _tblbanklist where ID = E.[BankName]) Bank  ,E.[BankAccount] , (Select Category from _tblCategories where ID = P.[Category]) Category,P.Year ,P.Month  ,P.PayItem ,P.PayAmount FROM [_tblEmployees] E  Right outer join _tblPayrole P on E.EmployeeID = P.EmployeeID  Where P.Month  = '" + ddlMonth.Text + "' and  P.Year = '" + ddlPayYear.Text + "' and E.EmployeeID = " + Convert.ToInt32(txtEmployeeID.Text));
            ReportDataSource datasource = new ReportDataSource("BillingStatement", dsCustomers.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        private DsBillingStatement GetData(string query)
        {
            // string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    using (DsBillingStatement dsCustomers = new DsBillingStatement())
                    {
                        sda.Fill(dsCustomers, "DataTable1");
                        return dsCustomers;
                    }
                }
            }
        }
    }
}