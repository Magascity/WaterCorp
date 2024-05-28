using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class ViewStaffInfo : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    BindGridView();
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateStaffInfo");
        }


        protected void UpdateStaff(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{


            //Reference the Button.
            Button btnUpdateStaff = sender as Button;

            //Reference the GridView Row.
            GridViewRow row = btnUpdateStaff.NamingContainer as GridViewRow;

            //Save the GridView Row in Session.
            Session["Row"] = row;
            //Redirect to other Page.
            Response.Redirect("UpdateStaffInfo.aspx");
            // Response.Redirect("Da.aspx");

        }

        //protected void BindGridView()
        //{
        //    GridView1.DataBind();
        //}

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        // Get the row being updated
        //        GridViewRow row = GridView1.Rows[e.RowIndex];

        //        // Get the ID of the record being updated
        //        int recordId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        //        // Find the DropDownList in the row
        //        DropDownList ddlSBU = (DropDownList)row.FindControl("ddlSBU");

        //        // Get the selected value from the DropDownList
        //        int selectedSBUId = Convert.ToInt32(ddlSBU.SelectedValue);

        //        // Find other controls in the row
        //        TextBox txtPersonnelID = (TextBox)row.FindControl("txtPersonnelID");
        //        TextBox txtLastName = (TextBox)row.FindControl("txtLastName");
        //        TextBox txtFirstName = (TextBox)row.FindControl("txtFirstName");
        //        TextBox txtOtherName = (TextBox)row.FindControl("txtOtherName");
        //        TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
        //        TextBox txtMobile = (TextBox)row.FindControl("txtMobile");
        //        //TextBox txtCreatedBy = (TextBox)row.FindControl("txtCreatedBy");
        //        //TextBox txtDateCreated = (TextBox)row.FindControl("txtDateCreated");

        //        // Get the values from the controls
        //        string personnelID = txtPersonnelID.Text;
        //        string lastName = txtLastName.Text;
        //        string firstName = txtFirstName.Text;
        //        string otherName = txtOtherName.Text;
        //        string email = txtEmail.Text;
        //        string mobile = txtMobile.Text;
        //        //string createdBy = txtCreatedBy.Text;
        //        //DateTime dateCreated = DateTime.ParseExact(txtDateCreated.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        //        // Update the record in the database with the new SBU and other values
        //        UpdateStaffSBU(recordId, personnelID, lastName, firstName, otherName, email, mobile, selectedSBUId);

        //        // Cancel the edit mode
        //        GridView1.EditIndex = -1;

        //        // Rebind the GridView to refresh the data
        //        BindGridView();

        //        // Optionally, display a success message or perform any other actions
        //        // Response.Write("Record updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions here
        //        // For example:
        //        // Response.Write("An error occurred: " + ex.Message);
        //    }
        //}

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        // Get the row being updated
        //        GridViewRow row = GridView1.Rows[e.RowIndex];

        //        // Get the ID of the record being updated
        //        int recordId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        //        // Find the DropDownList in the row
        //        DropDownList ddlSBU = (DropDownList)row.FindControl("ddlSBU");

        //        // Get the selected value from the DropDownList
        //        int selectedSBUId = Convert.ToInt32(ddlSBU.SelectedValue);

        //        // Find other controls in the row
        //        TextBox txtPersonnelID = (TextBox)row.FindControl("txtPersonnelID");
        //        TextBox txtLastName = (TextBox)row.FindControl("txtLastName");
        //        TextBox txtFirstName = (TextBox)row.FindControl("txtFirstName");
        //        TextBox txtOtherName = (TextBox)row.FindControl("txtOtherName");
        //        TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
        //        TextBox txtMobile = (TextBox)row.FindControl("txtMobile");

        //        // Get the values from the controls
        //        string personnelID = txtPersonnelID.Text;
        //        string lastName = txtLastName.Text;
        //        string firstName = txtFirstName.Text;
        //        string otherName = txtOtherName.Text;
        //        string email = txtEmail.Text;
        //        string mobile = txtMobile.Text;

        //        // Update the record in the database with the new SBU and other values
        //        UpdateStaffSBU(recordId, personnelID, lastName, firstName, otherName, email, mobile, selectedSBUId);

        //        // Cancel the edit mode
        //        GridView1.EditIndex = -1;

        //        // Rebind the GridView to refresh the data
        //        BindGridView();

        //        // Optionally, display a success message or perform any other actions
        //        // Response.Write("Record updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions here
        //        // For example:
        //        // Response.Write("An error occurred: " + ex.Message);
        //    }
        //}

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        // Get the row being updated
        //        GridViewRow row = GridView1.Rows[e.RowIndex];

        //        // Get the ID of the record being updated
        //        int recordId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        //        // Find the DropDownList in the row
        //        DropDownList ddlSBU = (DropDownList)row.FindControl("ddlSBU");

        //        // Find other controls in the row
        //        TextBox txtPersonnelID = (TextBox)row.FindControl("txtPersonnelID");
        //        TextBox txtLastName = (TextBox)row.FindControl("txtLastName");
        //        TextBox txtFirstName = (TextBox)row.FindControl("txtFirstName");
        //        TextBox txtOtherName = (TextBox)row.FindControl("txtOtherName");
        //        TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
        //        TextBox txtMobile = (TextBox)row.FindControl("txtMobile");

        //        // Get the values from the controls
        //        string personnelID = txtPersonnelID.Text;
        //        string lastName = txtLastName.Text;
        //        string firstName = txtFirstName.Text;
        //        string otherName = txtOtherName.Text;
        //        string email = txtEmail.Text;
        //        string mobile = txtMobile.Text;

        //        // Get the selected value from the DropDownList
        //        int selectedSBUId = Convert.ToInt32(ddlSBU.SelectedValue);

        //        // Check if the selected value exists in the DropDownList
        //        ListItem selectedItem = ddlSBU.Items.FindByValue(selectedSBUId.ToString());
        //        if (selectedItem != null)
        //        {
        //            // Update the record in the database with the new SBU and other values
        //            UpdateStaffSBU(recordId, personnelID, lastName, firstName, otherName, email, mobile, selectedSBUId);

        //            // Cancel the edit mode
        //            GridView1.EditIndex = -1;

        //            // Rebind the GridView to refresh the data
        //            BindGridView();
        //        }
        //        else
        //        {
        //            // Handle the case where the selected value does not exist in the DropDownList
        //            // For example, display an error message or log the issue
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions here
        //        // For example:
        //        // Response.Write("An error occurred: " + ex.Message);
        //    }
        //}


        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        // Get the row being updated
        //        GridViewRow row = GridView1.Rows[e.RowIndex];

        //        // Get the ID of the record being updated
        //        int recordId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        //        // Find the DropDownList in the row
        //        DropDownList ddlSBU = (DropDownList)row.FindControl("ddlSBU");

        //        // Get the selected value from the DropDownList
        //        int selectedSBUId;
        //        if (ddlSBU.SelectedItem != null)
        //        {
        //            selectedSBUId = Convert.ToInt32(ddlSBU.SelectedValue);
        //        }
        //        else
        //        {
        //            // Handle the case where no item is selected in the DropDownList
        //            // For example, display an error message or log the issue
        //            return;
        //        }

        //        // Find other controls in the row
        //        TextBox txtPersonnelID = (TextBox)row.FindControl("txtPersonnelID");
        //        TextBox txtLastName = (TextBox)row.FindControl("txtLastName");
        //        TextBox txtFirstName = (TextBox)row.FindControl("txtFirstName");
        //        TextBox txtOtherName = (TextBox)row.FindControl("txtOtherName");
        //        TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
        //        TextBox txtMobile = (TextBox)row.FindControl("txtMobile");

        //        // Get the values from the controls
        //        string personnelID = txtPersonnelID.Text;
        //        string lastName = txtLastName.Text;
        //        string firstName = txtFirstName.Text;
        //        string otherName = txtOtherName.Text;
        //        string email = txtEmail.Text;
        //        string mobile = txtMobile.Text;

        //        // Update the record in the database with the new SBU and other values
        //        UpdateStaffSBU(recordId, personnelID, lastName, firstName, otherName, email, mobile, selectedSBUId);

        //        // Cancel the edit mode
        //        GridView1.EditIndex = -1;

        //        // Rebind the GridView to refresh the data
        //        BindGridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions here
        //        // For example:
        //        // Response.Write("An error occurred: " + ex.Message);
        //    }
        //}


        //private void UpdateStaffSBU(int recordId, string personnelID, string lastName, string firstName, string otherName, string email, string mobile, int newSBUId)
        //{
        //    // Your SQL query to update the fields
        //    string query = @"UPDATE tblStaffInfo 
        //             SET 
        //                 [PersonnelID] = @PersonnelID,
        //                 [Lastname] = @LastName,
        //                 [Firstname] = @FirstName,
        //                 [Othername] = @OtherName,
        //                 [Email] = @Email,
        //                 [Mobile] = @Mobile,
        //                 [SBU] = @SBUId
        //             WHERE RecordID = @RecordId";

        //    // Create a SqlConnection object with your connection string
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        // Create a SqlCommand object with your SQL query and SqlConnection
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            // Add parameters to the SqlCommand
        //            command.Parameters.AddWithValue("@RecordId", recordId);
        //            command.Parameters.AddWithValue("@PersonnelID", personnelID);
        //            command.Parameters.AddWithValue("@LastName", lastName);
        //            command.Parameters.AddWithValue("@FirstName", firstName);
        //            command.Parameters.AddWithValue("@OtherName", otherName);
        //            command.Parameters.AddWithValue("@Email", email);
        //            command.Parameters.AddWithValue("@Mobile", mobile);
        //            command.Parameters.AddWithValue("@SBUId", newSBUId);

        //            // Open the database connection
        //            connection.Open();

        //            // Execute the SQL command
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow row = GridView1.Rows[e.RowIndex];
        //        int recordId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        //        DropDownList ddlSBU = (DropDownList)row.FindControl("ddlSBU");
        //        int selectedSBUId = Convert.ToInt32(ddlSBU.SelectedValue);

        //        HFSbuID.Value = selectedSBUId.ToString();

        //        // Find other controls in the row
        //        TextBox txtPersonnelID = (TextBox)row.FindControl("txtPersonnelID");
        //        TextBox txtLastName = (TextBox)row.FindControl("txtLastName");
        //        TextBox txtFirstName = (TextBox)row.FindControl("txtFirstName");
        //        TextBox txtOtherName = (TextBox)row.FindControl("txtOtherName");
        //        TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
        //        TextBox txtMobile = (TextBox)row.FindControl("txtMobile");

        //        // Get the values from the controls
        //        string personnelID = txtPersonnelID.Text;
        //        string lastName = txtLastName.Text;
        //        string firstName = txtFirstName.Text;
        //        string otherName = txtOtherName.Text;
        //        string email = txtEmail.Text;
        //        string mobile = txtMobile.Text;
        //        string sbuID = HFSbuID.Value;

        //        // Update the record in the database with the new SBU ID and other values
        //        UpdateStaffSBU(recordId, personnelID, lastName, firstName, otherName, email, mobile, sbuID);

        //        GridView1.EditIndex = -1;
        //        BindGridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //    }
        //}

        //private void UpdateStaffSBU(int recordId, string personnelID, string lastName, string firstName, string otherName, string email, string mobile, string newSBUId)
        //{
        //    string query = @"UPDATE tblStaffInfo 
        //             SET 
        //                 [PersonnelID] = @PersonnelID,
        //                 [Lastname] = @LastName,
        //                 [Firstname] = @FirstName,
        //                 [Othername] = @OtherName,
        //                 [Email] = @Email,
        //                 [Mobile] = @Mobile,
        //                 [SBU] = @SBUId
        //             WHERE RecordID = @RecordId";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@RecordId", recordId);
        //            command.Parameters.AddWithValue("@SBUId", Int32.Parse(newSBUId));
        //            command.Parameters.AddWithValue("@PersonnelID", personnelID);
        //            command.Parameters.AddWithValue("@LastName", lastName);
        //            command.Parameters.AddWithValue("@FirstName", firstName);
        //            command.Parameters.AddWithValue("@OtherName", otherName);
        //            command.Parameters.AddWithValue("@Email", email);
        //            command.Parameters.AddWithValue("@Mobile", mobile);


        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}



        // Method to update the SBU field in the database
        //private void UpdateStaffSBU(int recordId, string personnelID, string lastName, string firstName, string otherName, string email, string mobile, int newSBUId, string createdBy, DateTime dateCreated)
        //private void UpdateStaffSBU(int recordId, string personnelID, string lastName, string firstName, string otherName, string email, string mobile, int newSBUId)
        //{
        //    // Your database connection string


        //    // Your SQL query to update the fields
        //    string query = @"UPDATE tblStaffInfo 
        //             SET 
        //                 [PersonnelID] = @PersonnelID,
        //                 [Lastname] = @LastName,
        //                 [Firstname] = @FirstName,
        //                 [Othername] = @OtherName,
        //                 [Email] = @Email,
        //                 [Mobile] = @Mobile,
        //                 [SBU] = @SBUId

        //             WHERE RecordID = @RecordId";

        //    //string query = @"UPDATE tblStaffInfo 
        //    //         SET 
        //    //             [PersonnelID] = @PersonnelID,
        //    //             [Lastname] = @LastName,
        //    //             [Firstname] = @FirstName,
        //    //             [Othername] = @OtherName,
        //    //             [Email] = @Email,
        //    //             [Mobile] = @Mobile,
        //    //             [SBU] = @SBUId
        //    //             --[CreatedBy] = @CreatedBy,
        //    //             --[DateCreated] = @DateCreated
        //    //         WHERE RecordID = @RecordId";

        //    // Create a SqlConnection object with your connection string
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        // Create a SqlCommand object with your SQL query and SqlConnection
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            // Add parameters to the SqlCommand

        //            command.Parameters.AddWithValue("@RecordId", recordId);
        //            command.Parameters.AddWithValue("@PersonnelID", personnelID);
        //            command.Parameters.AddWithValue("@LastName", lastName);
        //            command.Parameters.AddWithValue("@FirstName", firstName);
        //            command.Parameters.AddWithValue("@OtherName", otherName);
        //            command.Parameters.AddWithValue("@Email", email);
        //            command.Parameters.AddWithValue("@Mobile", mobile);
        //            command.Parameters.AddWithValue("@SBUId", newSBUId);
        //            //command.Parameters.AddWithValue("@CreatedBy", createdBy);
        //            //command.Parameters.AddWithValue("@DateCreated", dateCreated);


        //            // Open the database connection
        //            connection.Open();

        //            // Execute the SQL command
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
        //    {
        //        // Find the DropDownList in the row
        //        DropDownList ddlSBU = (DropDownList)e.Row.FindControl("ddlSBU");

        //        // Get the current data item bound to the row
        //        DataRowView drv = (DataRowView)e.Row.DataItem;

        //        // Set the DropDownList data source
        //      //  ddlSBU.DataSource = SqlDataSource2; // Use SqlDataSource2 directly
        //        ddlSBU.DataTextField = "DistrictName";
        //        ddlSBU.DataValueField = "Id";
        //        ddlSBU.DataBind();

        //        // Set the selected value based on the DistrictName
        //        ddlSBU.SelectedValue = drv["SBU"].ToString();
        //    }
        //}

    }
}