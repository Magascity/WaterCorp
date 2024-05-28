using ClosedXML.Excel;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Billing
{
    public partial class MeterReadingUpload : System.Web.UI.Page
    {
        //Latest Uploads

        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fupUploads.HasFile)
            {
                string extension = Path.GetExtension(fupUploads.FileName);
                if (extension == ".xlsx" || extension == ".csv")
                {
                    // Specify your database connection string
                   // string connectionString = "YourConnectionStringHere";
                    int totalRecords = 0;
                    int validRecords = 0;
                    int invalidRecords = 0;

                    try
                    {
                        using (var stream = fupUploads.PostedFile.InputStream)
                        {
                            if (extension == ".xlsx")
                            {
                                // Excel file
                                using (var workBook = new XLWorkbook(stream))
                                {
                                    var ws = workBook.Worksheets.First();
                                    foreach (var row in ws.RowsUsed())
                                    {
                                        totalRecords++;

                                        // Validate data types and non-empty values here
                                        // For simplicity, let's assume columns A and B need validation
                                        if (IsValidData(row.Cell(1).Value, typeof(int)) &&
                                            IsValidData(row.Cell(2).Value, typeof(string)))
                                        {
                                            // Save to the database
                                            using (SqlConnection connection = new SqlConnection(connectionString))
                                            {
                                                connection.Open();
                                                SqlCommand cmd = new SqlCommand("INSERT INTO YourTable (Column1, Column2) VALUES (@Value1, @Value2)", connection);
                                                cmd.Parameters.AddWithValue("@Value1", row.Cell(1).Value);
                                                cmd.Parameters.AddWithValue("@Value2", row.Cell(2).Value);
                                                cmd.ExecuteNonQuery();
                                            }

                                            validRecords++;
                                        }
                                        else
                                        {
                                            invalidRecords++;
                                        }
                                    }
                                }
                            }
                            else if (extension == ".csv")
                            {
                                //using (var reader = new StreamReader("path\\to\\file.csv"))
                                //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))

                                // CSV file
                                using (var reader = new StreamReader(stream))
                                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                                {
                                    var records = csv.GetRecords<dynamic>().ToList();
                                    foreach (var record in records)
                                    {
                                        totalRecords++;

                                        // Validate data types and non-empty values here
                                        // For simplicity, let's assume columns A and B need validation
                                        if (IsValidData(record.Column1, typeof(int)) &&
                                            IsValidData(record.Column2, typeof(string)))
                                        {
                                            // Save to the database
                                            using (SqlConnection connection = new SqlConnection(connectionString))
                                            {
                                                connection.Open();
                                                SqlCommand cmd = new SqlCommand("INSERT INTO YourTable (Column1, Column2) VALUES (@Value1, @Value2)", connection);
                                                cmd.Parameters.AddWithValue("@Value1", record.Column1);
                                                cmd.Parameters.AddWithValue("@Value2", record.Column2);
                                                cmd.ExecuteNonQuery();
                                            }

                                            validRecords++;
                                        }
                                        else
                                        {
                                            invalidRecords++;
                                        }
                                    }
                                }
                            }
                        }

                        lblResults.Text = $"Total Records: {totalRecords}<br/>Valid Records: {validRecords}<br/>Invalid Records: {invalidRecords}";
                    }
                    catch (Exception ex)
                    {
                        lblResults.Text = $"Error: {ex.Message}";
                    }
                }
                else
                {
                    lblResults.Text = "Invalid file format. Please upload a valid Excel (.xlsx) or CSV (.csv) file.";
                }
            }
            else
            {
                lblResults.Text = "Please select a file to upload.";
            }
        }

        private bool IsValidData(object value, Type targetType)
        {
            // Implement your data validation logic here based on the targetType
            // For simplicity, let's assume non-empty values are valid for all types
            return value != null && !string.IsNullOrEmpty(value.ToString());
        }
    }
}
