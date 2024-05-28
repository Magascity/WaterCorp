using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WaterCorp.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WaterCorp.Usermanagement
{
    public partial class CreateUsers : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillSbus();
            }
        }

        private void FillSbus()
        {
            ddlSbu.Items.Clear();
            ddlSbu.Items.Insert(0, new ListItem("--Select Business Unit--", "0"));


            // Define the Select statement.
            // Three pieces of information are needed: the unique id,
            // and the first and last name.
            string selectSQL = "SELECT [Id],[DistrictName],[Code] FROM [_tblSbus]";

            // Define the ADO.NET objects.
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            // Try to open database and read information.
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();


                // list box text, and store the unique ID in the Value property.
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["DistrictName"].ToString();
                    newItem.Value = reader["DistrictName"].ToString();
                    ddlSbu.Items.Add(newItem);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                //lblMessage.Text = "Error reading Faculty";
                //lblMessage.Text += err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        //protected void CreateUser_Click(object sender, EventArgs e)
        //{
        //    // Create an instance of ApplicationDbContext with the CustomConnection string
        //    var customContext = ApplicationDbContext.Create("CustomConnection");

        //    // Create an instance of UserStore with the custom context
        //    var userStore = new UserStore<ApplicationUser>(customContext);

        //    // Create an instance of ApplicationUserManager with the UserStore
        //    var manager = new ApplicationUserManager(userStore);

        //    // Continue with your existing logic
        //  //  var signInManager = new ApplicationSignInManager(manager);

        //    var user = new ApplicationUser()
        //    {
        //        UserName = Email.Text,
        //        Email = Email.Text,
        //        LastName = txtLastname.Text,
        //        Othernames = txtOthernames.Text,
        //        Sbu = ddlSbu.SelectedItem.Value
        //    };

        //    IdentityResult result = manager.Create(user, Password.Text);

        //    if (result.Succeeded)
        //    {
        //        // Your success handling logic

        //        Response.Redirect("ManageUsers");
        //    }
        //    else
        //    {
        //        ErrorMessage.Text = result.Errors.FirstOrDefault();
        //    }
        //}


        protected void CreateUser_Click(object sender, EventArgs e)
        {

            // Create an instance of ApplicationDbContext with the CustomConnection string
            // var customContext = ApplicationDbContext.Create("CustomConnection");

            // Use the custom context to get the ApplicationUserManager and ApplicationSignInManager
            // var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(customContext));




            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text, LastName = txtLastname.Text, Othernames = txtOthernames.Text, Sbu = ddlSbu.SelectedItem.Value };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                //signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

                Response.Redirect("ManangeUsers");
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}