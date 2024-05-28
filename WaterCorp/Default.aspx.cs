using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ForgotPasswordHyperLink.NavigateUrl = "~/Account/Forgot.aspx";

            if (User.Identity.IsAuthenticated == true)
            {
                pnlLogin.Visible = false;
            }


        }
        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var result = signInManager.PasswordSignIn(Login1.UserName, Login1.Password, Login1.RememberMeSet, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    e.Authenticated = true;
                    pnlLogin.Visible = false; // Hide the Login control

                    Response.Redirect("~/Default.aspx");

                    break;
                case SignInStatus.LockedOut:
                    // Handle locked-out user
                    break;
                case SignInStatus.RequiresVerification:
                    // Handle two-factor authentication
                    break;
                case SignInStatus.Failure:
                default:
                    e.Authenticated = false;
                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;
                    break;
            }
        }

    }
}