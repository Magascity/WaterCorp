using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WaterCorp.crm
{
    public partial class SendBulkSms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            int totalRecords = GridView1.PageCount * GridView1.PageSize;
            List<string> phoneNumbers = new List<string>();

            for (int i = 0; i < GridView1.PageCount; i++)
            {
                GridView1.PageIndex = i;
                GridView1.DataBind();

                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    if (chk != null && chk.Checked)
                    {
                        string firstNameVariable = gvrow.Cells[2].Text;
                        string messageVariable = txtMessage.Text;
                        string message = String.Format("{0} {1} {2} {3}", "Dear ", firstNameVariable, ",", messageVariable);

                        string Phone = gvrow.Cells[6].Text;
                        string formatedPhoneno = Phone.Substring(1, 10);

                        phoneNumbers.Add(formatedPhoneno);
                        // Optionally, send the message here instead of adding to the list
                        // msg.SendSMS(message, formatedPhoneno);
                    }
                }
            }

            // Send SMS to all collected phone numbers
            clsSMS msg = new clsSMS();
            foreach (string phoneNumber in phoneNumbers)
            {
                string messageVariable = txtMessage.Text;
                string message = String.Format("{0} {1}", "Dear Customer,", messageVariable);
                msg.SendSMS(message, phoneNumber);
            }

            lblMessage.Text = "Messages Sent Successfully!";
        }

    }
}