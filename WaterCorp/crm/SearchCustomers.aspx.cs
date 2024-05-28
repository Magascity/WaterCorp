using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class SearchCustomers : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Assess(object sender, EventArgs e)
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
            Response.Redirect("UpdateCustomer.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void UpdateMeter(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnMeter = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnMeter.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;
            //Redirect to other Page.
            Response.Redirect("UpdateMetering.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void CustomerBill(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnViewBill = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnViewBill.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;
            //Redirect to other Page.
            Response.Redirect("ViewBills.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void GeoInfo(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnGeoInfo = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnGeoInfo.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;
            //Redirect to other Page.
            Response.Redirect("UpdateSpatialData.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void LogComplaint(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnLogComplaint = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnLogComplaint.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;
            //Redirect to other Page.
            Response.Redirect("LogComplaint.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void SendMessage(object sender, EventArgs e)
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
            Response.Redirect("SendMessage.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();

            //string query = "SELECT [TIN],[Lastname],[Othernames],[Firstname],[Mobile1],[Mobile2] ,[emailaddress],OrganizationTIN,Taxoffice FROM [_tblIndividualRegistration] " +
            //    "WHERE CAST(TIN AS NVARCHAR) LIKE '%' + @SearchValue + '%' OR " +
            //    "lastname LIKE '%' + @SearchValue + '%' OR " +
            //    "Mobile1 LIKE '%' + @SearchValue + '%' OR " +
            //    "Mobile2 LIKE '%' + @SearchValue + '%' OR " +
            //    "emailAddress LIKE '%' + @SearchValue + '%'";

            //string query = "SELECT [TIN],[Lastname],[Othernames],[Firstname],[Mobile1],[Mobile2] ,[emailaddress],[OrganizationTIN],[TaxOffice] FROM [_tblIndividual] " +
            string query = "SELECT C.[RecordID],C.CustomerName, C.[Email]" +
      ",C.PhoneNumber ,C.[CustomerAddress] ,C.[DistrictCode] ,C.[ZoneCode] " +
      ",C.[Subzone] ,C.[Round] ,C.[Foliono] ,C.[Metered] ,C.[MeterNo]  ,C.[MeterCharge] " +
      ", (SELECT TarrifType FROM [_tblTarrif] where [TarrifID] = C.[FlateRateCategory]) As Category " +
      ",C.[Tarrif]  ,C.[Consumption] ,C.CustReference,C.Latitude,C.Longitude ,C.[DateCreated] FROM [_tblCustomers3] C  Where C.Verified = 1 and (" +
       "[RecordID] LIKE '%' + @SearchValue + '%' OR " +
 "CustomerName LIKE '%' + @SearchValue + '%' OR " +
 //"[Othernames] LIKE '%' + @SearchValue + '%' OR " +
 "[CustReference] LIKE '%' + @SearchValue + '%' OR " +
 "[Email] LIKE '%' + @SearchValue + '%' OR " +
 "[MeterNo] LIKE '%' + @SearchValue + '%')"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchValue", searchValue);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }



        }
    }
}