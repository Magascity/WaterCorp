<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewMeter.aspx.cs" Inherits="WaterCorp.Store.AddNewMeter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

              <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
     <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Create a new Meter Stock</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />
         
   <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtMeterName" CssClass="col-md-2 col-form-label">Meter Name</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtMeterName" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeterName"
            CssClass="text-danger" ErrorMessage="The Meter Name field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtModel" CssClass="col-md-2 col-form-label">Meter Model</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtModel" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModel"
            CssClass="text-danger" ErrorMessage="The Meter Model field is required." />
    </div>
</div>

        


    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtManufacturer" CssClass="col-md-2 col-form-label">Manufacturer</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtManufacturer" CssClass="form-control" TextMode="SingleLine"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtManufacturer"
            CssClass="text-danger" ErrorMessage="The Manufacturer field is required." />
      
    </div>
</div>

     <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtMeterNo" CssClass="col-md-2 col-form-label">Meter-No</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtMeterNo" CssClass="form-control" TextMode="SingleLine"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeterNo"
            CssClass="text-danger" ErrorMessage="The Meter-no field is required." />
      
    </div>
</div>

     

    


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
