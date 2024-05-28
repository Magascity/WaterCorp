<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewMeterStock.aspx.cs" Inherits="WaterCorp.Store.ViewMeterStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <h4>View Meter Stock</h4>


        <div class="row">
<div class="col-md-10">
  <%-- <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" CssClass="btn btn-success" />--%>
    <asp:Button ID="btnNewMeter" runat="server" OnClick="btnNewMeter_Click" Text="Add New Meter" CssClass="btn btn-info" />
   <p></p>
</div>
   </div>
    <div class="panel-body">
         
    <div class="table-responsive">
    
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="MeterID" DataSourceID="SqlDataSource1" >
        <Columns>
            
            <asp:BoundField DataField="MeterID" HeaderText="MeterID" InsertVisible="False" ReadOnly="True" SortExpression="MeterID" />
            <asp:BoundField DataField="MeterName" HeaderText="MeterName" SortExpression="MeterName" />
            <asp:BoundField DataField="MeterModel" HeaderText="MeterModel" SortExpression="MeterModel" />
            <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" SortExpression="Manufacturer" />
            <asp:BoundField DataField="MeterNo" HeaderText="MeterNo" SortExpression="MeterNo" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" DataFormatString="{0:d}" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
            <asp:CheckBoxField DataField="Isssued" HeaderText="Isssued" SortExpression="Isssued" />
        </Columns>


    </asp:GridView>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MeterID], [MeterName], [MeterModel], [Manufacturer], [MeterNo], [DateCreated], [CreatedBy], [Isssued] FROM [tblMeterRecords]"></asp:SqlDataSource>

        </div>
        </div>
</asp:Content>
