using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.Reports
{
    public partial class CustomersList : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind the GridView initially
                BindGridView();
            }
        }

        protected void ddlActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Rebind the GridView when the dropdown selection changes
            BindGridView();
        }

        //private void BindGridView()
        //{
        //    // Retrieve the selected value from the DropDownList
        //    string activeStatus = ddlActiveStatus.SelectedValue;

        //    // Call your data access method to fetch data using the modified SQL query
        //    DataTable dt = GetDataFromDatabase(activeStatus);

        //    // Bind the GridView with the retrieved data
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}

        //private void BindGridView()
        //{
        //    // Retrieve the selected value from the DropDownList
        //    string activeStatusString = ddlActiveStatus.SelectedValue;

        //    // Convert the string to a boolean
        //    bool? activeStatus = null;

        //    if (!string.IsNullOrEmpty(activeStatusString))
        //    {
        //        activeStatus = bool.Parse(activeStatusString);
        //    }

        //    // Call your data access method to fetch data using the modified SQL query
        //    DataTable dt = GetDataFromDatabase(activeStatus);

        //    // Bind the GridView with the retrieved data
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}

        //private void BindGridView()
        //{
        //    // Retrieve the selected value from the DropDownList
        //    string activeStatusString = ddlActiveStatus.SelectedValue;

        //    // Convert the string to a boolean or null
        //    bool? activeStatus = null;
        //    if (!string.IsNullOrEmpty(activeStatusString))
        //    {
        //        activeStatus = bool.Parse(activeStatusString);
        //    }

        //    // Fetch data from the database
        //    DataTable dt = GetDataFromDatabase(activeStatus);

        //    // Bind the GridView with the retrieved data
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}

        private void BindGridView()
        {
            // Retrieve the selected value from the DropDownList
            string activeStatusString = ddlActiveStatus.SelectedValue;

            // Convert the string to a boolean or null
            bool? activeStatus = null;
            if (!string.IsNullOrEmpty(activeStatusString))
            {
                if (activeStatusString == "1")
                {
                    activeStatus = true;
                }
                else if (activeStatusString == "0")
                {
                    activeStatus = false;
                }
            }

            // Fetch data from the database
            DataTable dt = GetDataFromDatabase(activeStatus);

            // Bind the GridView with the retrieved data
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }



        //private DataTable GetDataFromDatabase(bool? isActive)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // SQL query
        //        string query = @"
        //    SELECT
        //        C.[RecordID],
        //        C.[LastName],
        //        C.[Firstname],
        //        C.[Middlename],
        //        C.[Email],
        //        C.[Mobile],
        //        C.[CustomerAddress],
        //        (SELECT TOP 1 DistrictName FROM [_tblSbus] WHERE Code = C.[DistrictCode]) AS DistrictName,
        //        C.[DistrictCode],
        //        C.[ZoneCode],
        //        C.[Subzone],
        //        C.[Round],
        //        C.[Foliono],
        //        C.[Metered],
        //        C.[MeterNo],
        //        (SELECT TarrifType FROM [_tblTarrif] WHERE TarrifID = C.[FlateRateCategory]) AS TarrifType,
        //        C.[DPCno],
        //        C.[Active]
        //    FROM
        //        [_tblCustomers3] C
        //    WHERE
        //        (@ActiveStatus IS NULL OR C.[Active] = @ActiveStatus OR (@ActiveStatus = 0 AND C.[Active] IS NULL))
        //";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            // Add parameter for the active status
        //            command.Parameters.AddWithValue("@ActiveStatus", (object)isActive ?? DBNull.Value);

        //            // Execute the query and fill the DataTable
        //            DataTable dt = new DataTable();
        //            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        //            {
        //                adapter.Fill(dt);
        //            }
        //            return dt;
        //        }
        //    }
        //}

        private DataTable GetDataFromDatabase(bool? isActive)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query
                string query = @"
            SELECT
                C.[RecordID],
                C.[LastName],
                C.[Firstname],
                C.[Othername],
                C.[Email],
                C.[Phonenumber],
                C.[CustomerAddress],
                (SELECT TOP 1 DistrictName FROM [_tblSbus] WHERE Code = C.[DistrictCode]) AS DistrictName,
                C.[DistrictCode],
                C.[ZoneCode],
                C.[Subzone],
                C.[Round],
                C.[Foliono],
                C.[Metered],
                C.[MeterNo],
                (SELECT TarrifType FROM [_tblTarrif] WHERE TarrifID = C.[FlateRateCategory]) AS TarrifType,
                C.[CustReference] as DPCNO,
                C.[Active]
            FROM
                [_tblCustomers3] C
            WHERE
                (@ActiveStatus IS NULL OR C.[Active] = @ActiveStatus OR (@ActiveStatus = 0 AND C.[Active] IS NULL))
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for the active status
                    command.Parameters.AddWithValue("@ActiveStatus", (object)isActive ?? DBNull.Value);

                    // Execute the query and fill the DataTable
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }



    }
}