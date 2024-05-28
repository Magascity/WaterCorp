﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="WaterCorp.crm.SendMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

         <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Send Message</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />


         <asp:HiddenField ID="HFCustomerID" runat="server" />
                   <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDpcno" CssClass="col-md-2 col-form-label">DPC-No</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDpcno" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDpcno"
            CssClass="text-danger" ErrorMessage="The Dpcno field is required." />
    </div>
</div>

           <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtLastname" CssClass="col-md-2 col-form-label">Lastname</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastname"
            CssClass="text-danger" ErrorMessage="The Lastname field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtOthernames" CssClass="col-md-2 col-form-label">Firstname</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtOthernames" CssClass="form-control" TextMode="SingleLine" ReadOnly="true"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOthernames"
            CssClass="text-danger" ErrorMessage="The Othernames field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtCustomerAddress" CssClass="col-md-2 col-form-label">Customer Address</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtCustomerAddress" CssClass="form-control" ReadOnly="true"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerAddress"
            CssClass="text-danger" ErrorMessage="The Address field is required." />
    </div>
</div>

              
     <div class="row">
         <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 col-form-label">Email</asp:Label>
         <div class="col-md-10">
             <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" ReadOnly="true" />
             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                 CssClass="text-danger" ErrorMessage="The email field is required." />
         </div>
     </div>

<div class="row">
    <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-2 col-form-label">Mobile</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" ReadOnly="true" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile"
            CssClass="text-danger" ErrorMessage="The Mobile field is required." />
    </div>
</div>

               

         


    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDescription" CssClass="col-md-2 col-form-label">Message</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription"
            CssClass="text-danger" ErrorMessage="The Description field is required." />
    </div>
</div>
    
        <div class="row">
    <asp:Label runat="server" AssociatedControlID="chkEmail" CssClass="col-md-4 col-form-label">Select Notification by Email</asp:Label>
    <div class="col-md-8">
      
       <asp:CheckBox ID="chkEmail" runat="server"  CssClass="form-check-input" />
        
    </div>
</div>

                                     <div class="row">
    <asp:Label runat="server" AssociatedControlID="chkSMS" CssClass="col-md-4 col-form-label">Select Notification SMS</asp:Label>
    <div class="col-md-8">
        <asp:CheckBox ID="chkSMS" runat="server"  CssClass="form-check-input" />
        <br />
        
        
    </div>
</div>

   
        
         <p></p>


     <div class="row">
         <div class="offset-md-2 col-md-10">
             <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
             <asp:Label ID="lblMessage" runat="server" ></asp:Label>
         </div>
     </div>
 </main>
        <%-- </ContentTemplate>
</asp:UpdatePanel>--%>



</asp:Content>
