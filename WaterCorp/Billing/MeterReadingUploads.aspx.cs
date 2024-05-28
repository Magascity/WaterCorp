
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic.FileIO;
using System.Web.Configuration;


namespace WaterCorp.Billing
{
    public partial class MeterReadingUploads : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (fupFile.HasFile && Path.GetExtension(fupFile.FileName) == ".csv")
            {
                string filePath = Server.MapPath("~/BulkFolder/" + fupFile.FileName);
                fupFile.SaveAs(filePath);

                // Process the uploaded CSV file and save to SQL Server
                DataTable dataTable = ReadCsvFile(filePath);
                SaveToSqlServer(dataTable);

                // Optionally, you can delete the file after processing
                File.Delete(filePath);

                // Provide feedback to the user

                lblMessage.Text = "CSV file imported successfully!";
                Response.Write("CSV file imported successfully!");
            }
            else
            {
                lblMessage.Text = "Please upload a valid CSV file.";
                Response.Write("Please upload a valid CSV file.");
            }
        }

        private DataTable ReadCsvFile(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Assume the first row contains column headers
                string[] headers = parser.ReadFields();

                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    // Ensure the number of fields matches the number of columns
                    if (fields.Length == dataTable.Columns.Count)
                    {
                        DataRow row = dataTable.NewRow();
                        row.ItemArray = fields;
                        dataTable.Rows.Add(row);
                    }
                    else
                    {
                        // Handle error or skip invalid rows
                    }
                }
            }

            return dataTable;
        }

        private void SaveToSqlServer(DataTable sourceDataTable)
        {
            //string connectionString = "YourSqlConnectionString";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "MeterReadings";

                    // Create a new DataTable with necessary columns
                    DataTable destinationDataTable = new DataTable();
                    destinationDataTable.Columns.Add("Dpcno", typeof(string));
                    destinationDataTable.Columns.Add("MeterReading", typeof(decimal));
                    destinationDataTable.Columns.Add("BillingPeriod", typeof(DateTime));
                    destinationDataTable.Columns.Add("CreatedBy", typeof(string));
                    destinationDataTable.Columns.Add("DateCreated", typeof(DateTime));

                    // Set default values for additional columns
                    DateTime billingPeriod = Convert.ToDateTime(txtBillingPeriod.Text);
                    string createdBy = User.Identity.Name;
                    DateTime dateCreated = DateTime.Now;

                    foreach (DataRow sourceRow in sourceDataTable.Rows)
                    {
                        // Create a new row for the destination DataTable
                        DataRow destinationRow = destinationDataTable.NewRow();

                        // Map existing columns from the source DataTable
                        destinationRow["Dpcno"] = sourceRow["Dpcno"];

                        // Convert "MeterReading" to decimal explicitly
                        if (decimal.TryParse(sourceRow["MeterReading"].ToString(), out decimal meterReading))
                        {
                            destinationRow["MeterReading"] = meterReading;
                        }
                        else
                        {
                            // Handle the case where the conversion fails
                            // You might want to log an error or take appropriate action
                            Response.Write($"Error converting MeterReading value: {sourceRow["MeterReading"]}");
                        }

                        // Set default values for additional columns
                        destinationRow["BillingPeriod"] = billingPeriod;
                        destinationRow["CreatedBy"] = createdBy;
                        destinationRow["DateCreated"] = dateCreated;

                        // Add the row to the destination DataTable
                        destinationDataTable.Rows.Add(destinationRow);
                    }


                    try
                    {
                        // Write the DataTable to the SQL Server table
                        bulkCopy.WriteToServer(destinationDataTable);
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception or log an error message
                        Response.Write($"Error writing DataTable to SQL Server: {ex.Message}");
                    }
                }
            }
        }

    }
}