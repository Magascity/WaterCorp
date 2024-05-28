using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Globalization;

namespace WaterCorp.Billing
{
    public partial class UploadMeterReadings : System.Web.UI.Page
    {


        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        clsMeterReadings finfo = new clsMeterReadings();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ViewState["INSERTEFROWS"] = 0;
            ViewState["FAILEDFROWS"] = 0;
            SaveFileContent();
            showFinalmessage();

            DisplayData();
        }

        public void SaveFileContent()
        {

            string path = fupFile.PostedFile.FileName;
            string extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".xls":
                    SaveDataXLS();
                    break;
                case ".csv":
                    SaveDataCSV();
                    break;
            }
        }

        public void SaveDataXLS()
        {




            if (fupFile.HasFile)
            {

                DataTable dts = new DataTable();

                //upload = Folder Name

                fupFile.SaveAs(Server.MapPath("~/BulkFolder/") + fupFile.FileName.ToString());
                string ExcelFilePathName = Server.MapPath("~/BulkFolder/") + fupFile.FileName;

                DataTable dts1 = new DataTable();
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + ExcelFilePathName + ";Extended Properties ='Excel 8.0; HDR = Yes;'");
                DbCommand command = con.CreateCommand();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", con);
                con.Open();
                DataSet ds = new DataSet();
                da.Fill(dts);
                if (dts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dts.Rows)
                    {


                        if (dr["DPCno"].Equals(null) || dr["DPCno"].Equals(0))
                        {
                            lblMessage.Text = "DPCNo is Missing";
                            lblMessage.Visible = true;
                            break;
                        }
                        else
                        {

                            finfo.DPCno = dr["DPCno"].ToString();
                        }


                        if (dr["MeterReading"].Equals(null) || dr["MeterReading"].Equals(0))
                        {
                            lblMessage.Text = "Meter reading is missing or equal to 0.";
                            lblMessage.Visible = true;
                            break;
                        }

                        else
                        {
                            finfo.MeterReading = Convert.ToDecimal(dr["MeterReading"]);

                        }
                        
                                        





                        InsertedInfo(finfo);
                        lblMessage.Visible = false;
                    }

                }
            }

        }

        public void SaveDataCSV()
        {
            if (fupFile.HasFile)
            {

                //upload = Folder Name

                FileInfo f = new FileInfo(Server.MapPath("~/BulkFolder/") + fupFile.FileName.ToString());
                DataTable dts = new DataTable();
                fupFile.SaveAs(Server.MapPath("~/BulkFolder/") + fupFile.FileName.ToString());
                string[] Lines = File.ReadAllLines(f.FullName);
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });
                int Cols = Fields.GetLength(0);
                DataTable dt = new DataTable();
                //1st row must be column names; force lower case to ensure matching later on.
                for (int i = 0; i < Cols; i++)
                {
                    dt.Columns.Add(Fields[i].ToLower(), typeof(string));
                }
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = dt.NewRow();
                    for (int j = 0; j < Cols; j++)
                        Row[j] = Fields[j];
                    dt.Rows.Add(Row);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["DPCno"].Equals(null) || dr["DPCno"].Equals(0))
                    {
                        lblMessage.Text = "DPCNo is Missing";
                        lblMessage.Visible = true;
                        break;
                    }
                    else
                    {

                        finfo.DPCno = dr["DPCno"].ToString();
                    }


                    if (dr["MeterReading"].Equals(null) || dr["MeterReading"].Equals(0))
                    {
                        lblMessage.Text = "Meter reading is missing or equal to 0.";
                        lblMessage.Visible = true;
                        break;
                    }

                    else
                    {
                        finfo.MeterReading = Convert.ToDecimal(dr["MeterReading"]);

                    }




                    InsertedInfo(finfo);
                    lblMessage.Visible = false;
                }
            }
        }

        void showFinalmessage()
        {
            int ins = Convert.ToInt32(ViewState["INSERTEFROWS"]),
            fail = Convert.ToInt32(ViewState["FAILEDFROWS"]);

            //LtrlFinalMessage.Text = String.Format("Inserted Successfull:<b>{0}</b> " + " <br> Duplicate found:<b>{1}</b><br> Out of Total:<b>{2}</b>", ins.ToString(), fail.ToString(), (ins + fail).ToString());
            LtrlFinalMessage.Text = String.Format("Inserted Successfull:<b>{0}</b> " + " <br> Duplicate found:<b>{1}</b> <br> Out of Total:<b>{2}</b>", ins.ToString(), fail.ToString(), (ins + fail).ToString());
        }

        void InsertedInfo(clsMeterReadings finfo)
        {
            if (AddFileToTable(finfo) > 0)
            {
                ViewState["INSERTEFROWS"] = Convert.ToInt32(ViewState["INSERTEFROWS"]) + 1;
            }
            else
            {
                ViewState["FAILEDFROWS"] = Convert.ToInt32(ViewState["FAILEDFROWS"]) + 1;
            }
        }

        private void DisplayData()
        {

            //string connectionString = "YourSqlConnectionString";

            // Parse the selected month and year from the TextBox
            DateTime selectedBillingPeriod;
            if (DateTime.TryParse(txtBillingPeriod.Text, out selectedBillingPeriod))
            {
                // Use the selected month and year in the SQL query
                string query = @"SELECT [MeterReadingID]
                        ,[DPCno]
                        ,[BillingPeriod]
                        ,[MeterReading]
                        ,[CreatedBy]
                        ,[DateCreated]
                    FROM [WaterCorp].[dbo].[MeterReadings]
                    WHERE MONTH([BillingPeriod]) = MONTH(@SelectedBillingPeriod)
                        AND YEAR([BillingPeriod]) = YEAR(@SelectedBillingPeriod)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@SelectedBillingPeriod", selectedBillingPeriod);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Bind the DataTable to your GridView
                            GridView1.DataSource = dataTable;
                            GridView1.DataBind();
                        }
                    }
                }
            }
            else
            {
                // Handle the case where parsing the TextBox value fails
                Response.Write("Invalid Billing Period format.");
            }



        }

        protected int AddFileToTable(clsMeterReadings finfo)
        {

            string insertSQL = "spInsertMeterReadings";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DPCno", finfo.DPCno);
            cmd.Parameters.AddWithValue("@MeterReading", finfo.MeterReading);
            cmd.Parameters.AddWithValue("@BillingPeriod", Convert.ToDateTime(txtBillingPeriod.Text));
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);

            con.Open();

            cmd.CommandType = CommandType.StoredProcedure;
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }



    }

}

