<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DynamicBillingReport.aspx.cs" Inherits="WaterCorp.Reports.DynamicBillingReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
     <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Print Bills</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />
         
    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDistrictCode" CssClass="col-md-2 col-form-label">DistrictCode</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDistrictCode" CssClass="form-control"  />
       <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txttxtDistrictCode"
            CssClass="text-danger" ErrorMessage="The Ditsrict Code field is required." />--%>
    </div>
</div>
         <p></p>
             <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtzoneCode" CssClass="col-md-2 col-form-label">ZoneCode</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtzoneCode" CssClass="form-control"  />
      <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtzoneCode"
            CssClass="text-danger" ErrorMessage="The Ditsrict Code field is required." />--%>
    </div>
</div>
         <p></p>
                      <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtsubzone" CssClass="col-md-2 col-form-label">Sub Zone</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtsubzone" CssClass="form-control"  />
      <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtzoneCode"
            CssClass="text-danger" ErrorMessage="The Ditsrict Code field is required." />--%>
    </div>
</div>
         <p></p>

                               <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtRound" CssClass="col-md-2 col-form-label">Round</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtRound" CssClass="form-control"  />
      <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtzoneCode"
            CssClass="text-danger" ErrorMessage="The Ditsrict Code field is required." />--%>
    </div>
</div>
         <p></p>

         <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtfoliono" CssClass="col-md-2 col-form-label">Foliono</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtfoliono" CssClass="form-control"  />
      <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtzoneCode"
            CssClass="text-danger" ErrorMessage="The Ditsrict Code field is required." />--%>
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
                <LocalReport ReportPath="Reports\WaterBill.rdlc">
                </LocalReport>
        </rsweb:ReportViewer>
        </asp:Panel>




        <%-- </ContentTemplate>
</asp:UpdatePanel>--%>

</asp:Content>
