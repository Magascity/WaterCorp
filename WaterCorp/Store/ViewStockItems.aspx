<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStockItems.aspx.cs" Inherits="WaterCorp.Store.ViewStockItems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <h4>View Stock Items</h4>


        <div class="row">
<div class="col-md-10">
   <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" CssClass="btn btn-success" />
   <p></p>
</div>
   </div>
    <div class="panel-body">
         
    <div class="table-responsive">
    
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="StockID" DataSourceID="SqlDataSource1" OnPreRender="GridView1_PreRender">
        <Columns>
            
            <asp:BoundField DataField="StockID" HeaderText="StockID" InsertVisible="False" ReadOnly="True" SortExpression="StockID" />
            <asp:BoundField DataField="ItemName" HeaderText="ItemName" SortExpression="ItemName" />
            <asp:BoundField DataField="ItemDescription" HeaderText="ItemDescription" SortExpression="ItemDescription" />
            <asp:BoundField DataField="ItemSerialNumber" HeaderText="ItemSerialNumber" SortExpression="ItemSerialNumber" />
            <asp:BoundField DataField="Category" HeaderText="ItemCategory" SortExpression="Category" />
            <asp:BoundField DataField="SubCategory" HeaderText="SubCategory" SortExpression="SubCategory" />
            <asp:BoundField DataField="StockUnit" HeaderText="StockUnit" SortExpression="StockUnit" />
            <asp:BoundField DataField="UnitPrice" HeaderText="Purchase Price" SortExpression="UnitPrice" />
            <asp:BoundField DataField="ReOrderQty" HeaderText="ReOrderQty" SortExpression="ReOrderQty" />
            <asp:BoundField DataField="ReOrderLevel" HeaderText="ReOrderLevel" SortExpression="ReOrderLevel" />
            <asp:BoundField DataField="QuantityOnHand" HeaderText="QuantityOnHand" SortExpression="QuantityOnHand" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
        </Columns>


    </asp:GridView>


<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="
SELECT T. [StockID], T.[ItemName], T.[ItemDescription], T.[ItemSerialNumber], (SELECT [Category] FROM [tblStockCategory] where [CategoryID] = T.[ItemCategory]) Category ,(SELECT [Unit] FROM [tblStockUnits] where  [UnitID] =  T.[Unit]) StockUnit, (SELECT [SubCategory] FROM [tblStockSubCategory] where  [ID] = T.[SubCategory]) SubCategory, T.[UnitPrice], T.[ReOrderQty], T.[ReOrderLevel], T.[QuantityOnHand], T.[DateCreated] FROM [ItemTable] T"></asp:SqlDataSource>

        </div>
        </div>

</asp:Content>
