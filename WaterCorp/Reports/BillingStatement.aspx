<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BillingStatement.aspx.cs" Inherits="WaterCorp.Reports.BillingStatement" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

         <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Print Bill Statement</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />
         
    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDPCNO" CssClass="col-md-2 col-form-label">DPC-No</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDPCNO" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDPCNO"
            CssClass="text-danger" ErrorMessage="The DPC NO field is required." />
    </div>
</div>



     <div class="row">
         <div class="offset-md-2 col-md-10">
             <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
             <asp:Label ID="lblMessage" runat="server" ></asp:Label>
         </div>
     </div>
 </main>



     <asp:Panel ID="PnlReport" runat="server" visible="false">

     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="945px" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PageCountMode="Actual" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
         <LocalReport ReportPath="Reports\CustomerBillingStatement.rdlc">
         </LocalReport>
 </rsweb:ReportViewer>
 </asp:Panel>
</asp:Content>
