<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateMetering.aspx.cs" Inherits="WaterCorp.crm.UpdateMetering" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Update Meter Info</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />
         <asp:HiddenField ID="HFRecordID" runat="server" />
         <asp:HiddenField ID="HFConsumption" runat="server" />
         <asp:HiddenField ID="HFCharges" runat="server" />

                          <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtRecordID" CssClass="col-md-2 col-form-label">RecordID</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtRecordID" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRecordID"
            CssClass="text-danger" ErrorMessage="The Record field is required." />
    </div>
</div>

         <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDpcNo" CssClass="col-md-2 col-form-label">Dpc-No</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDpcNo" CssClass="form-control" ReadOnly="true" TextMode="Email" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDpcNo"
            CssClass="text-danger" ErrorMessage="The email field is required." />
    </div>
</div>

                 <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtLastname" CssClass="col-md-2 col-form-label">Lastname</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastname"
            CssClass="text-danger" ErrorMessage="The Lastname field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtFirstname" CssClass="col-md-2 col-form-label">First name</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstname"
            CssClass="text-danger" ErrorMessage="The First name field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtCustomerAddress" CssClass="col-md-2 col-form-label">Customer Address</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtCustomerAddress" CssClass="form-control" ReadOnly="true" TextMode="MultiLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerAddress"
            CssClass="text-danger" ErrorMessage="The Address field is required." />
    </div>
</div>

                 <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-2 col-form-label">Mobile</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" MaxLength="11" />
       <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile"
            CssClass="text-danger" ErrorMessage="The Mobile field is required." />
        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMobile"
            CssClass="text-danger" ErrorMessage="Enter exactly 11 numbers." ValidationExpression="\d{11}" />--%>
        <br />
    </div>
</div>

     <div class="row">
         <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 col-form-label">Email</asp:Label>
         <div class="col-md-10">
             <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ReadOnly="true" TextMode="Email" /><br />
            <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                 CssClass="text-danger" ErrorMessage="The email field is required." />--%>
         </div>
     </div>

     <div class="row">
    <asp:Label runat="server" AssociatedControlID="chkMetered" CssClass="col-md-2 col-form-label">Meter Status</asp:Label>
    <div class="col-md-10">
        <asp:CheckBox ID="chkMetered" runat="server" CssClass="form-control" Enabled="false" />
        <asp:Label ID="lblMetered" runat="server" ></asp:Label>
        <br />
        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="chkMetered"
            CssClass="text-danger" ErrorMessage="The email field is required." />--%>
    </div>
</div>

          <div class="row">
     <asp:Label runat="server" AssociatedControlID="txtMeterNo" CssClass="col-md-2 col-form-label">Meter No</asp:Label>
     <div class="col-md-10">
         <asp:TextBox runat="server" ID="txtMeterNo" CssClass="form-control" ReadOnly="true"  /><br />
        <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
             CssClass="text-danger" ErrorMessage="The email field is required." />--%>
     </div>
 </div>

         <div class="row">
    <asp:Label runat="server" AssociatedControlID="ddlMeterStatus" CssClass="col-md-2 col-form-label">Metered Status</asp:Label>
    <div class="col-md-10">
        <asp:DropDownList ID="ddlMeterStatus" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlMeterStatus_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="0">--Select Meter Status--</asp:ListItem>
            <asp:ListItem>Metered</asp:ListItem>
            <asp:ListItem>Un-Metered</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMeterStatus" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The Metered field is required." />
    </div>
</div>

<asp:Panel ID="PnlMeters" runat="server" Visible="false">

    <div class="row">
        <asp:Label runat="server" AssociatedControlID="ddlMeterNo" CssClass="col-md-2 col-form-label">Metered No</asp:Label>
        <div class="col-md-10">
            <asp:DropDownList ID="ddlMeterNo" CssClass="form-control" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMeterNo" InitialValue="0"
                CssClass="text-danger" ErrorMessage="The Meter No field is required." />
        </div>
    </div>

    <div class="row">
        <asp:Label runat="server" AssociatedControlID="txtMeterCharges" CssClass="col-md-2 col-form-label">Meter Charges</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtMeterCharges" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeterCharges"
                CssClass="text-danger" ErrorMessage="The Meter Charges field is required." />
        </div>
    </div>
</asp:Panel>





     <div class="row">
         <div class="offset-md-2 col-md-10">
             <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Update" CssClass="btn btn-primary" />
           <%-- <asp:Button ID="btnContinue" runat="server" OnClick="btnContinue_Click" Text="Continue" CssClass="btn btn-info" />--%>
             <asp:Label ID="lblMessage" runat="server" ></asp:Label></div>

         
        
     </div>


 </main>
         </ContentTemplate>
</asp:UpdatePanel>




</asp:Content>
