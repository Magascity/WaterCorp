<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendBillNotification.aspx.cs" Inherits="WaterCorp.crm.SendBillNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

         <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>--%>
  <main aria-labelledby="title">
      <p class="text-danger">
          <asp:Literal runat="server" ID="ErrorMessage" />
      </p>
      <h4>Send Billing Notifications</h4>
     

      
          <div class="col-md-6">
              <!-- Column 1 -->

              <div class="row">
                  <asp:Label runat="server" AssociatedControlID="txtBillingPeriod" CssClass="col-md-4 col-form-label">Billing Period</asp:Label>
                  <div class="col-md-8">
                      <asp:TextBox runat="server" ID="txtBillingPeriod" CssClass="form-control" TextMode="Month" />
                      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod"
                          CssClass="text-danger" ErrorMessage="The Billing Period field is required." />
                  </div>
              </div>
              

         
                        <div class="row">
                  <div class="offset-md-2 col-md-10">
                        <asp:Button ID="btnSendEmail" runat="server" Text="Send Notification" CssClass="btn btn-primary" OnClick="btnSendEmail_Click" />
                      <asp:Label ID="lblMessage" runat="server"></asp:Label>
                  </div>
              </div>
  </main>


  
         </div>


  
</asp:Content>
