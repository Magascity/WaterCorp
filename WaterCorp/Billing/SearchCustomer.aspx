<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchCustomer.aspx.cs" Inherits="WaterCorp.Billing.SearchCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

    <div class="col-lg-12">

        <div class="card">
            <div class="card-body">
                <br />
                <div class="row g-3">

                    <div class="col-md-8">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="DPCNo, Mobile, Surname, Email"></asp:TextBox>

                    </div>
                    <div class="col-md-4">

                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />

                        <br />
                    </div>
                </div>
                <p></p>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False">
                        <Columns>

                           <%-- <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />--%>
                            <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                            <asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                            <asp:BoundField DataField="CustomerAddress" HeaderText="CustomerAddress" SortExpression="CustomerAddress" />
                            <asp:BoundField DataField="DistrictCode" HeaderText="DistrictCode" SortExpression="DistrictCode" />
                            <asp:BoundField DataField="ZoneCode" HeaderText="ZoneCode" SortExpression="ZoneCode" />
                            <asp:BoundField DataField="Subzone" HeaderText="Subzone" SortExpression="Subzone" />
                            <asp:BoundField DataField="Round" HeaderText="Round" SortExpression="Round" />
                            <asp:BoundField DataField="Foliono" HeaderText="Foliono" SortExpression="Foliono" />
                            <asp:CheckBoxField DataField="Metered" HeaderText="Metered" SortExpression="Metered" />
                            <asp:BoundField DataField="MeterNo" HeaderText="MeterNo" SortExpression="MeterNo" />
                            <asp:BoundField DataField="MeterCharge" HeaderText="MeterCharge" SortExpression="MeterCharge" />
                            <asp:BoundField DataField="Category" HeaderText="FlateRateCategory" SortExpression="Category" />
                            <asp:BoundField DataField="Tarrif" HeaderText="Tarrif" SortExpression="Tarrif" />
                            <asp:BoundField DataField="Consumption" HeaderText="Consumption" SortExpression="Consumption" />
                            <asp:BoundField DataField="DPCno" HeaderText="DPCno" SortExpression="DPCno" />
                            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="" Text="Enter Meter Reading" OnClick="Assess" />
                                </ItemTemplate>
                                <ControlStyle BorderStyle="None" CssClass="btn btn-primary" />
                            </asp:TemplateField>


                           


                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="" Text="Pay Bills" />
                                </ItemTemplate>
                                <ControlStyle BorderStyle="None" CssClass="btn btn-info" />
                            </asp:TemplateField>


                           


                        </Columns>
                        <EmptyDataTemplate>
                            <span style="color: #FF0000"><strong>No Records Found </strong></span>
                        </EmptyDataTemplate>

                    </asp:GridView>

                </div>




            </div>
        </div>

    </div>

</div>


</asp:Content>
