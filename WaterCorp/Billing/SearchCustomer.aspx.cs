using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Billing
{
    public partial class SearchCustomer : System.Web.UI.Page
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
            Response.Redirect("UpdateMeterReading.aspx");
            // Response.Redirect("Da.aspx");

        }

        protected void PayBill(object sender, EventArgs e)
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
            Response.Redirect("UpdateMeterReading.aspx");
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
            string query = "SELECT C.[RecordID],C.[LastName],C.[Firstname] ,C.[Email]" +
      ",C.[Mobile] ,C.[CustomerAddress] ,C.[DistrictCode] ,C.[ZoneCode] " +
      ",C.[Subzone] ,C.[Round] ,C.[Foliono] ,C.[Metered] ,C.[MeterNo]  ,C.[MeterCharge] " +
      ", (SELECT TarrifType FROM [_tblTarrif] where [TarrifID] = C.[FlateRateCategory]) As Category " +
      ",C.[Tarrif]  ,C.[Consumption] ,C.[DPCno] ,C.[DateCreated] FROM [_tblCustomers3] C  Where" +
       "[RecordID] LIKE '%' + @SearchValue + '%' OR " +
 "[LastName] LIKE '%' + @SearchValue + '%' OR " +
 "[Firstname] LIKE '%' + @SearchValue + '%' OR " +
 "[Mobile] LIKE '%' + @SearchValue + '%' OR " +
 "[Email] LIKE '%' + @SearchValue + '%' OR " +
 "[MeterNo] LIKE '%' + @SearchValue + '%'";

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