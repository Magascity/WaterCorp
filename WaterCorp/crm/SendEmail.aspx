<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="WaterCorp.crm.SendEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Log Customer Complaint</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />


         <asp:HiddenField ID="HFCustomerID" runat="server" />
        

           <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtLastname" CssClass="col-md-2 col-form-label">Lastname</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastname"
            CssClass="text-danger" ErrorMessage="The Lastname field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtOthernames" CssClass="col-md-2 col-form-label">Othernames</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtOthernames" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOthernames"
            CssClass="text-danger" ErrorMessage="The Othernames field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtCustomerAddress" CssClass="col-md-2 col-form-label">Customer Address</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtCustomerAddress" CssClass="form-control"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerAddress"
            CssClass="text-danger" ErrorMessage="The Address field is required." />
    </div>
</div>

              
     <div class="row">
         <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 col-form-label">Email</asp:Label>
         <div class="col-md-10">
             <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                 CssClass="text-danger" ErrorMessage="The email field is required." />
         </div>
     </div>



               <div class="row">
    <asp:Label runat="server" AssociatedControlID="ddlService" CssClass="col-md-2 col-form-label">Service Complaint</asp:Label>
    <div class="col-md-10">
        <asp:DropDownList ID="ddlService" CssClass="form-control" runat="server"  ></asp:DropDownList>        
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlService" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The Service field is required." />
    </div>
</div>

             <div class="row">
    <asp:Label runat="server" AssociatedControlID="ddlSubService" CssClass="col-md-2 col-form-label">Sub-Service</asp:Label>
    <div class="col-md-10">
        <asp:DropDownList ID="ddlSubService" CssClass="form-control" runat="server"> </asp:DropDownList>        
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSubService" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The Sub Service field is required." />
    </div>
</div>


    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDescription" CssClass="col-md-2 col-form-label">Description</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription"
            CssClass="text-danger" ErrorMessage="The Description field is required." />
    </div>
</div>
    
  
    <div class="row">
    <asp:Label runat="server" AssociatedControlID="fupUpload" CssClass="col-md-2 col-form-label">Photo/Video</asp:Label>
    <div class="col-md-10">
        <asp:FileUpload ID="fupUpload" runat="server" CssClass="form-control"  />
        <%--<asp:TextBox runat="server" ID="TextBox1" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription"
            CssClass="text-danger" ErrorMessage="The Description field is required." />--%>
    </div>
</div>
        
         <p></p>


     <div class="row">
         <div class="offset-md-2 col-md-10">
             <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Submit" CssClass="btn btn-primary" />
             <asp:Label ID="lblMessage" runat="server" ></asp:Label>
         </div>
     </div>
 </main>
         </ContentTemplate>



   
</asp:Content>
