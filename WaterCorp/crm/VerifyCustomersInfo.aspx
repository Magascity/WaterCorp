<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerifyCustomersInfo.aspx.cs" Inherits="WaterCorp.crm.VerifyCustomersInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="row">

    <div class="col-lg-12">
        <h4>Verify New Customers</h4>
        <div class="card">
            <div class="card-body">
                <br />
                <div class="row g-3">
                    <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                    <%--<div class="col-md-8">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="DPCNo, Mobile, Surname, Email"></asp:TextBox>

                    </div>--%>
                   <%-- <div class="col-md-4">

                        <asp:Button ID="btnSearch" runat="server" Text="Add New Customer" CssClass="btn btn-info" OnClick="btnSearch_Click" />

                        <br />
                    </div>--%>
                </div>
                <p></p>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="CustReference" DataSourceID="SqlDataSource1">
                        <Columns>

                           <%-- <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />--%>
                            <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                            <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                            <%--<asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                            <asp:BoundField DataField="Middlename" HeaderText="Middlename" SortExpression="Middlename" />--%>
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="PhoneNumber" HeaderText="Mobile" SortExpression="PhoneNumber" />
                            <asp:BoundField DataField="CustomerAddress" HeaderText="CustomerAddress" SortExpression="CustomerAddress" />
                            <%--<asp:BoundField DataField="District" HeaderText="District" SortExpression="District" ReadOnly="True" />--%>
                            <asp:TemplateField HeaderText="District" SortExpression="District">
                             <EditItemTemplate>
                            <asp:DropDownList ID="ddlDistrict" runat="server" DataSourceID="DistrictDataSource"
                            DataTextField="DistrictName" DataValueField="Code" SelectedValue='<%# Bind("ZoneCode") %>'>
                            </asp:DropDownList>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
                            
                            <asp:BoundField DataField="ZoneCode" HeaderText="ZoneCode" SortExpression="ZoneCode" />
                            <asp:BoundField DataField="Subzone" HeaderText="Subzone" SortExpression="Subzone" />
                            <asp:BoundField DataField="Round" HeaderText="Round" SortExpression="Round" />
                            <asp:BoundField DataField="Foliono" HeaderText="Foliono" SortExpression="Foliono" />
                            <asp:CheckBoxField DataField="Metered" HeaderText="Metered" SortExpression="Metered" />
                            <asp:BoundField DataField="MeterNo" HeaderText="MeterNo" SortExpression="MeterNo" />
                            <asp:BoundField DataField="MeterCharge" HeaderText="MeterCharge" SortExpression="MeterCharge" />

                            <asp:TemplateField HeaderText="CustomerCategory" SortExpression="CustomerCategory">
    <EditItemTemplate>
        <asp:DropDownList ID="ddlCustomerCategory" runat="server" DataSourceID="CustomerCategoryDataSource"
            DataTextField="TarrifType" DataValueField="TarrifID" SelectedValue='<%# Bind("FlateRateCategory") %>'>
        </asp:DropDownList>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:Label ID="lblCustomerCategory" runat="server" Text='<%# Eval("CustomerCategory") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

                           <%-- <asp:BoundField DataField="CustomerCategory" HeaderText="CustomerCategory" SortExpression="CustomerCategory" ReadOnly="True" />--%>
                            <asp:BoundField DataField="Tarrif" HeaderText="Tarrif" SortExpression="Tarrif" />
                            <asp:BoundField DataField="Consumption" HeaderText="Consumption" SortExpression="Consumption" />
                            <asp:BoundField DataField="CustReference" HeaderText="DPCno" SortExpression="CustReference" ReadOnly="True" />
                            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" DataFormatString="{0:d}" />


                            <asp:BoundField DataField="Latitude" HeaderText="Latitude" SortExpression="Latitude" />
                            <asp:BoundField DataField="Longitude" HeaderText="Longitude" SortExpression="Longitude" />


                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="" Text="Verify" OnClick="VerifyInfo" />
                                </ItemTemplate>
                                <ControlStyle BorderStyle="None" CssClass="btn btn-warning" />
                            </asp:TemplateField>


                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="" Text="Update" OnClick="UpdateInfo" />
                                </ItemTemplate>
                                <ControlStyle BorderStyle="None" CssClass="btn btn-primary" />
                            </asp:TemplateField>


                        </Columns>
                        <EmptyDataTemplate>
                            <span style="color: #FF0000"><strong>No Records Found </strong></span>
                        </EmptyDataTemplate>

                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT C.[RecordID]
      ,C.CustomerName
       ,C.[Email]
      ,C.[PhoneNumber]
      ,C.[CustomerAddress]
      ,(SELECT TOP 1 [DistrictName] FROM [_tblSbus] WHERE Code = C.[DistrictCode]) AS District
      ,C.[ZoneCode]
      ,C.[Subzone]
      ,C.[Round]
      ,C.[Foliono]
      ,C.[Metered]
      ,C.[MeterNo]
      ,C.[MeterCharge]
      , (SELECT [TarrifType] FROM [_tblTarrif] where TarrifID = C.[FlateRateCategory]) CustomerCategory
      ,C.[Tarrif]
      ,C.[Consumption]
      ,C.[CustReference]
      ,C.[DateCreated]
      ,[Latitude]
      ,[Longitude]
  FROM [_tblCustomers3] C where Verified = 0 order by C.RecordID Desc"></asp:SqlDataSource>

                </div>

                <asp:SqlDataSource ID="DistrictDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
    SelectCommand="SELECT [Code], [DistrictName] FROM [_tblSbus]">
</asp:SqlDataSource>

                <asp:SqlDataSource ID="CustomerCategoryDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
    SelectCommand="SELECT [TarrifID], [TarrifType] FROM [_tblTarrif]">
</asp:SqlDataSource>

            </div>
        </div>

    </div>

</div>



</asp:Content>
