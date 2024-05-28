<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCustomers.aspx.cs" Inherits="WaterCorp.crm.ViewCustomers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

    <div class="col-lg-12">

        <div class="card">
            <div class="card-body">
                <br />
                <div class="row g-3">

                    <%--<div class="col-md-8">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="DPCNo, Mobile, Surname, Email"></asp:TextBox>

                    </div>--%>
                    <div class="col-md-4">

                        <asp:Button ID="btnSearch" runat="server" Text="Add New Customer" CssClass="btn btn-info" OnClick="btnSearch_Click" />

                        <br />
                    </div>
                </div>
                <p></p>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="CustReference" DataSourceID="SqlDataSource1">
                        <Columns>

                           <%-- <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />--%>
                            <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                           <%-- <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                            <asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                            <asp:BoundField DataField="Middlename" HeaderText="Middlename" SortExpression="Middlename" />--%>
                           <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="PhoneNumber" HeaderText="Mobile" SortExpression="PhoneNumber" />
                            <asp:BoundField DataField="CustomerAddress" HeaderText="CustomerAddress" SortExpression="CustomerAddress" />
                            <asp:BoundField DataField="District" HeaderText="District" SortExpression="District" ReadOnly="True" />
                            <asp:BoundField DataField="ZoneCode" HeaderText="ZoneCode" SortExpression="ZoneCode" />
                            <asp:BoundField DataField="Subzone" HeaderText="Subzone" SortExpression="Subzone" />
                            <asp:BoundField DataField="Round" HeaderText="Round" SortExpression="Round" />
                            <asp:BoundField DataField="Foliono" HeaderText="Foliono" SortExpression="Foliono" />
                            <asp:CheckBoxField DataField="Metered" HeaderText="Metered" SortExpression="Metered" />
                            <asp:BoundField DataField="MeterNo" HeaderText="MeterNo" SortExpression="MeterNo" />
                            <asp:BoundField DataField="MeterCharge" HeaderText="MeterCharge" SortExpression="MeterCharge" />
                            <asp:BoundField DataField="CustomerCategory" HeaderText="CustomerCategory" SortExpression="CustomerCategory" ReadOnly="True" />
                            <asp:BoundField DataField="Tarrif" HeaderText="Tarrif" SortExpression="Tarrif" />
                            <asp:BoundField DataField="Consumption" HeaderText="Consumption" SortExpression="Consumption" />
                            <asp:BoundField DataField="CustReference" HeaderText="DPCno" SortExpression="CustReference" ReadOnly="True" />
                            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Latitude" HeaderText="Latitude" SortExpression="Latitude" />
                            <asp:BoundField DataField="Longitude" HeaderText="Longitude" SortExpression="Longitude" />

                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="" Text="Update Info" OnClick="Assess"/>
                                </ItemTemplate>
                                <ControlStyle BorderStyle="None" CssClass="btn btn-info" />
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
FROM [_tblCustomers3] C Where C.Verified = 1 order by C.RecordID Desc"></asp:SqlDataSource>

                  <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT C.[RecordID]
      ,C.[LastName]
      ,C.[Firstname]
      ,C.[Middlename]
      ,C.CustomerName
      ,C.[Email]
      ,C.[Mobile]
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
      ,C.[DPCno]
      ,C.[DateCreated]
      ,[Latitude]
      ,[Longitude]
  FROM [_tblCustomers3] C Where C.Verified = 1 order by C.RecordID Desc"></asp:SqlDataSource>--%>

                </div>




            </div>
        </div>

    </div>

</div>


</asp:Content>
