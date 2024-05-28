using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Billing
{
    public partial class ViewBills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            Response.Redirect("PayBills.aspx");
            // Response.Redirect("Da.aspx");

        }        

        protected void btnExport_Click(object sender, EventArgs e)
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("SelectedDate", DbType.Date, txtBillingPeriod.Text);

            string selectCommand = "SELECT [WaterBillID], [DPCNo], [CustomerName], [Address], Mobile, Email, [MeterNo], [CustomerID], [BillingPeriod], [PresentReading], [PreviousReading], [Consumption], [CurrentCharge], [OutstandingCharges], [LastPaymentDate], [LastPaymentAmount], [TotalDue], [CreatedBy], [DateCreated], [PaymentDate], [PaymentAmount] FROM [dbo].[WaterBills] WHERE [BillingPeriod] = @SelectedDate";

            if (!string.IsNullOrEmpty(txtDPCNo.Text))
            {
                selectCommand += " AND [DPCNo] = @DPCNo";
                SqlDataSource1.SelectParameters.Add("DPCNo", DbType.String, txtDPCNo.Text);
            }

            if (!string.IsNullOrEmpty(txtMobileNo.Text))
            {
                selectCommand += " AND [MobileNo] = @MobileNo";
                SqlDataSource1.SelectParameters.Add("MobileNo", DbType.String, txtMobileNo.Text);
            }

            SqlDataSource1.SelectCommand = selectCommand;
            GridView1.DataBind();
        }
    }
}