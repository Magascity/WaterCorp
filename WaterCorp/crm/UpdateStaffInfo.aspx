<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateStaffInfo.aspx.cs" Inherits="WaterCorp.crm.UpdateStaffInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <main aria-labelledby="title">
    <%--<h2 id="title"><%: Title %>.</h2>--%>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <h4>Create a Employee Info</h4>
    <hr />
    <asp:ValidationSummary runat="server" CssClass="text-danger" />

                <asp:HiddenField ID="HFRecordID" runat="server" />

    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtPersonnelID" CssClass="col-md-2 col-form-label">Personnel ID</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtPersonnelID" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPersonnelID"
            CssClass="text-danger" ErrorMessage="The Personnel ID field is required." />
    </div>
</div>

                <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-md-2 col-form-label">Last-Name</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName"
            CssClass="text-danger" ErrorMessage="The Lastname field is required." />
    </div>
</div>

    
          <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-md-2 col-form-label">First-Name</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName"
            CssClass="text-danger" ErrorMessage="The First name field is required." />
    </div>
</div>

              <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtOtherName" CssClass="col-md-2 col-form-label">Other-Name</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtOtherName" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOtherName"
            CssClass="text-danger" ErrorMessage="The Other name field is required." />
    </div>
</div>

       <div class="row">
    <asp:Label runat="server" AssociatedControlID="ddlDistrictCode" CssClass="col-md-2 col-form-label">SBU</asp:Label>
    <div class="col-md-10">
        <asp:DropDownList ID="ddlDistrictCode" runat="server" CssClass="form-control"></asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDistrictCode" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The District Code field is required." />
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
     <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-2 col-form-label">Mobile</asp:Label>
     <div class="col-md-10">
         <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control"  />
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile"
             CssClass="text-danger" ErrorMessage="The Mobile field is required." />
     </div>
 </div>
   
        <br />
    <div class="row">
        <div class="offset-md-2 col-md-10">

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
        </div>
    </div>
</main>

</asp:Content>
