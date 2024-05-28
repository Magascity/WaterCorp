using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class ViewCustomers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //protected void Assess(object sender, EventArgs e)
        //{
        //    //if (User.Identity.IsAuthenticated)
        //    //{


        //    //Reference the Button.
        //    Button btnPayroll = sender as Button;

        //    //Reference the GridView Row.
        //    GridViewRow row = btnPayroll.NamingContainer as GridViewRow;

        //    //Save the GridView Row in Session.
        //    Session["Row"] = row;
        //    //Redirect to other Page.
        //    Response.Redirect("UpdateCustomerInfo.aspx");
        //    // Response.Redirect("Da.aspx");

        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewCustomer");
        }
    }
}