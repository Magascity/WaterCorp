using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WaterCorp.Reports
{
    public partial class DynamicBillingReport : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

               // FillDistrictCodes();

            }
        }

      

        protected void btnSubmit_Click(object sender, EventArgs e)
        {



            PnlReport.Visible = true;

            PrintReport(txtDistrictCode.Text, txtzoneCode.Text, txtsubzone.Text, txtRound.Text, txtfoliono.Text);

        }


        private void PrintReport(string districtCode, string zoneCode, string subzone, string round, string foliono)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/WaterBill.rdlc");

            DsBillings dsCustomers = GetData("GetWaterBillReport", districtCode, zoneCode, subzone, round, foliono);

            ReportDataSource datasource = new ReportDataSource("DsWaterBill", dsCustomers.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }



        private DsBillings GetData(string query, string districtCode, string zoneCode, string subzone, string round, string foliono)
        {
            {
                // string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand(query);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DistrictCode", txtDistrictCode.Text);
                cmd.Parameters.AddWithValue("@ZoneCode", txtzoneCode.Text);
                cmd.Parameters.AddWithValue("@Subzone", txtsubzone.Text);
                cmd.Parameters.AddWithValue("@Round", txtRound.Text);
                cmd.Parameters.AddWithValue("@Foliono", txtfoliono.Text);




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
}