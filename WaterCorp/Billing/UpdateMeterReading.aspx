<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateMeterReading.aspx.cs" Inherits="WaterCorp.Billing.UpdateMeterReading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Create a new Customer account</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />
         <asp:HiddenField ID="HFTarrif" runat="server" />
         <asp:HiddenField ID="HFConsumption" runat="server" />
         <asp:HiddenField ID="HFCharges" runat="server" />
                 <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDpcNo" CssClass="col-md-2 col-form-label">DPCNo</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDpcNo" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDpcNo"
            CssClass="text-danger" ErrorMessage="The DpcNo field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtBillingPeriod" CssClass="col-md-2 col-form-label">Billing Period</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtBillingPeriod" CssClass="form-control" TextMode="Month" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod"
            CssClass="text-danger" ErrorMessage="The Billing Period field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtMeterReading" CssClass="col-md-2 col-form-label">Meter Reading</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtMeterReading" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeterReading"
            CssClass="text-danger" ErrorMessage="The Meter Reading field is required." />
    </div>
</div>


    
           



     <div class="row">
         <div class="offset-md-2 col-md-10">
             <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
             <asp:Label ID="lblMessage" runat="server" ></asp:Label>
         </div>
     </div>
 </main>
         </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
