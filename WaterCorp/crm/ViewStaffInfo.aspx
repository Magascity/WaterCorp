<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStaffInfo.aspx.cs" Inherits="WaterCorp.crm.ViewStaffInfo" %>

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

                            <asp:Button ID="btnSearch" runat="server" Text="Add New Staff" CssClass="btn btn-info" OnClick="btnSearch_Click" />

                            <asp:HiddenField ID="HFSbuID" runat="server" />
                            <br />
                        </div>
                    </div>
                    <p></p>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False"  AllowPaging="True" AllowSorting="True" DataSourceID="SqlDataSource1">
                            <Columns>
                                <asp:BoundField DataField="RecordID" HeaderText="RecordID" SortExpression="RecordID" ReadOnly="true" InsertVisible="False" />
                                <asp:BoundField DataField="PersonnelID" HeaderText="PersonnelID" SortExpression="PersonnelID" />
                                <asp:BoundField DataField="Lastname" HeaderText="Lastname" SortExpression="Lastname" />
                                <asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                                <asp:BoundField DataField="Othername" HeaderText="Othername" SortExpression="Othername" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                                <asp:BoundField DataField="SBU" HeaderText="SBU" ReadOnly="True" SortExpression="SBU" />
                                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                                <asp:BoundField DataField="DateCreated" DataFormatString="{0:d}" HeaderText="DateCreated" SortExpression="DateCreated" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="" Text="Update" OnClick="UpdateStaff" />
                                    </ItemTemplate>
                                    <ControlStyle BorderStyle="None" CssClass="btn btn-info" />
                                </asp:TemplateField>



                            </Columns>
                            <EmptyDataTemplate>
                                <span style="color: #FF0000"><strong>No Records Found </strong></span>
                            </EmptyDataTemplate>

                        </asp:GridView>



                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand=" SELECT S.[RecordID], S.[PersonnelID], S.[Lastname], S.[Firstname], S.[Othername], S.[Email], S.[Mobile], (
SELECT    [DistrictName]
  FROM [_tblSbus] where Id =S.[SBU]) SBU, S.[CreatedBy], S.[DateCreated] FROM [tblStaffInfo] S
  "></asp:SqlDataSource>



                    </div>




                </div>
            </div>

        </div>

    </div>



</asp:Content>
