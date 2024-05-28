using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Reports
{
    public partial class InactiveCustomersList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Data binding or other initial setup
                GridView1.DataBind();
            }
        }


        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            // Calculate and display the total number of customers in the FooterRow
            int totalCustomers = GridView1.Rows.Count;
            GridViewRow footerRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal);
            TableCell cell = new TableCell();
            cell.ColumnSpan = GridView1.Columns.Count - 1; // Adjust the column span based on your actual columns
            cell.HorizontalAlign = HorizontalAlign.Right;
            cell.Text = "Total Items: " + totalCustomers.ToString();
            footerRow.Cells.Add(cell);

            // Add the FooterRow to the GridView
            GridView1.Controls[0].Controls.Add(footerRow);
        }
        private void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // Hide the DPCno column if you don't want to export it
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;

                GridView1.RenderControl(hw);

                // Show the DPCno column again after rendering
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // This is required for the export to Excel to work properly
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }

}
