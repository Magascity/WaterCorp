<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewMeterReading.aspx.cs" Inherits="WaterCorp.Billing.ViewMeterReading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h4>View Meter Reading</h4>


        <div class="row">
<div class="col-md-10">
   <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" CssClass="btn btn-success" />
   <p></p>
</div>
   </div>
    <div class="panel-body">
         
    <div class="table-responsive">
    
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="MeterReadingID" DataSourceID="SqlDataSource1" OnPreRender="GridView1_PreRender">
        <Columns>
            
            <asp:BoundField DataField="MeterReadingID" HeaderText="MeterReadingID" InsertVisible="False" ReadOnly="True" SortExpression="MeterReadingID" />
            <asp:BoundField DataField="DPCno" HeaderText="DPCno" SortExpression="DPCno" />
            <asp:BoundField DataField="BillingPeriod" HeaderText="BillingPeriod" SortExpression="BillingPeriod" DataFormatString="{0:d}" />
            <asp:BoundField DataField="MeterReading" HeaderText="MeterReading" SortExpression="MeterReading" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
        </Columns>


    </asp:GridView>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MeterReadingID], [DPCno], [BillingPeriod], [MeterReading], [CreatedBy], [DateCreated] FROM [MeterReadings]"></asp:SqlDataSource>

        </div>
        </div>

</asp:Content>
