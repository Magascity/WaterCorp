<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InactiveCustomersList.aspx.cs" Inherits="WaterCorp.Reports.InactiveCustomersList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <div class="row">

    <div class="col-lg-12">

        <div class="card">
            <div class="card-body">
                <br />
                <div class="row g-3">
                    <h4>In-active Customers</h4>
                    <%--<div class="col-md-8">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="DPCNo, Mobile, Surname, Email"></asp:TextBox>

                    </div>--%>
                    <div class="col-md-4">

                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-info" OnClick="btnExport_Click" />

                        <br />
                    </div>
                </div>
                <p></p>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="CustReference"  AllowPaging="true" DataSourceID="SqlDataSource1" OnPreRender="GridView1_PreRender">
                        <Columns>

                           <%-- <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />--%>
                            <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                            <asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                            <asp:BoundField DataField="Othername" HeaderText="Othername" SortExpression="Othername" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="Phonenumber" HeaderText="Phonenumber" SortExpression="Phonenumber" />
                            <asp:BoundField DataField="CustomerAddress" HeaderText="CustomerAddress" SortExpression="CustomerAddress" />
                            <asp:BoundField DataField="DistrictName" HeaderText="DistrictName" SortExpression="DistrictName" ReadOnly="True" />
                            <asp:BoundField DataField="DistrictCode" HeaderText="DistrictCode" SortExpression="DistrictCode" />
                            <asp:BoundField DataField="ZoneCode" HeaderText="ZoneCode" SortExpression="ZoneCode" />
                            <asp:BoundField DataField="Subzone" HeaderText="Subzone" SortExpression="Subzone" />
                            <asp:BoundField DataField="Round" HeaderText="Round" SortExpression="Round" />
                            <asp:BoundField DataField="Foliono" HeaderText="Foliono" SortExpression="Foliono" />
                            <asp:CheckBoxField DataField="Metered" HeaderText="Metered" SortExpression="Metered" />
                            <asp:BoundField DataField="MeterNo" HeaderText="MeterNo" SortExpression="MeterNo" />
                            <asp:BoundField DataField="TarrifType" HeaderText="TarrifType" SortExpression="TarrifType" ReadOnly="True" />
                            <asp:BoundField DataField="CustReference" HeaderText="DPCno" SortExpression="CustReference" ReadOnly="True" />
                            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                           


                        </Columns>
                        <EmptyDataTemplate>
                            <span style="color: #FF0000"><strong>No Records Found </strong></span>
                        </EmptyDataTemplate>

                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT C.[RecordID]
      ,C.[LastName]
      ,C.[Firstname]
      ,C.[Othername]
      ,C.[Email]
      ,C.[Phonenumber]
      ,C.[CustomerAddress]
      ,(Select Top 1 DistrictName from [_tblSbus] where Code = C.[DistrictCode]) DistrictName
	  ,C.[DistrictCode]
      ,C.[ZoneCode]
      ,C.[Subzone]
      ,C.[Round]
      ,C.[Foliono]
      ,C.[Metered]
      ,C.[MeterNo]
      ,(Select TarrifType from [_tblTarrif] where TarrifID = C.[FlateRateCategory]) TarrifType 
      ,C.[CustReference]
      ,C.[Active]      
  FROM [_tblCustomers3] C where  C.Active = 0 and  C.Verified = 1 order by C.RecordID Desc"></asp:SqlDataSource>

                </div>




            </div>
        </div>

    </div>

</div>

</asp:Content>
