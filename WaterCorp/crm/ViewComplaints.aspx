<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewComplaints.aspx.cs" Inherits="WaterCorp.crm.ViewComplaints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="row">

    <div class="col-lg-12">

        <div class="card">
            <div class="card-body">
                <h4>View Customer Complaints</h4>
                <br />
                <div class="row g-3">

                    <%--<div class="col-md-8">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="DPCNo, Mobile, Surname, Email"></asp:TextBox>

                    </div>--%>
                    <%--<div class="col-md-4">

                        <asp:Button ID="btnSearch" runat="server" Text="Add New Customer" CssClass="btn btn-info" OnClick="btnSearch_Click" />

                        <br />
                    </div>--%>
                </div>
                <p></p>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="ComplaintID" DataSourceID="SqlDataSource1">
                        <Columns>

                           <%-- <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />--%>
                            <asp:BoundField DataField="ComplaintID" HeaderText="ComplaintID" InsertVisible="False" ReadOnly="True" SortExpression="ComplaintID" />
                            <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />                            
                            <asp:BoundField DataField="DpcNo" HeaderText="DpcNo" SortExpression="DpcNo" />                            
                            <asp:BoundField DataField="Service" HeaderText="Service" SortExpression="Service" ReadOnly="True" />
                            <asp:BoundField DataField="Subservice" HeaderText="Subservice" SortExpression="Subservice" ReadOnly="True" />
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                            <asp:BoundField DataField="FileData" HeaderText="FileData" SortExpression="FileData" />
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                            <asp:BoundField DataField="DateLogged" HeaderText="DateLogged" SortExpression="DateLogged" />
                            <asp:BoundField DataField="DateResolved" HeaderText="DateResolved" SortExpression="DateResolved" />
                            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="" Text="Escalate" OnClick="Assign" />
                                </ItemTemplate>
                                <ControlStyle BorderStyle="None" CssClass="btn btn-danger" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <span style="color: #FF0000"><strong>No Records Found </strong></span>
                        </EmptyDataTemplate>

                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT C.[ComplaintID]
      ,C.[CustomerID]
                        ,C.DpcNo
      ,(SELECT [ServiceName] FROM [Services] where [ServiceID] = C.[ServiceID]) [Service]
      ,(SELECT [SubServiceName] FROM [SubServices] where SubserviceID = C.[SubServiceID]) Subservice
      ,C.[Description]
      ,C.[FileImage]
      ,C.[FileData]
	  ,C.[Status]
      ,C.[DateLogged]
      ,C.[DateResolved]
      ,C.[CreatedBy]
   FROM [Complaints] C where C.Status = 'Open'"></asp:SqlDataSource>

                </div>




            </div>
        </div>

    </div>

</div>


</asp:Content>
