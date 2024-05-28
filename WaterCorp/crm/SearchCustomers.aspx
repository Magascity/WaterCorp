<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchCustomers.aspx.cs" Inherits="WaterCorp.crm.SearchCustomers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

        <div class="col-lg-12">

            <div class="card">
                <div class="card-body">
                    <br />
                    <%--<div class="row g-3">

                        <div class="col-md-8">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="DPCNo, Mobile, Surname, Email"></asp:TextBox>
                            
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />

                           
                            <br />
                        </div>
                            
                    </div>--%>


                <div class="row g-3">
    <div class="col-md-8">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="DPCNo, Mobile, Surname, Email"></asp:TextBox>
    </div>
    <div class="col-md-2">
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info mt-2" OnClick="btnSearch_Click" />
    </div>
</div>


                    <p></p>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False">
                            <Columns>

                               <%-- <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />--%>
                                <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                                <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                                <%--<asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                <asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                                <asp:BoundField DataField="Middlename" HeaderText="Middlename" SortExpression="Middlename" />--%>
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:BoundField DataField="PhoneNumber" HeaderText="Mobile" SortExpression="PhoneNumber" />
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
                                <asp:BoundField DataField="CustReference" HeaderText="DPCno" SortExpression="CustReference" />
                                <asp:BoundField DataField="Latitude" HeaderText="Latitude" SortExpression="Latitude" />
                                <asp:BoundField DataField="Longitude" HeaderText="Longitude" SortExpression="Longitude" />
                                <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="" Text="Update Info" OnClick="Assess" />
                                    </ItemTemplate>
                                    <ControlStyle BorderStyle="None" CssClass="btn btn-primary" />
                                </asp:TemplateField>


                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="" Text="View Bill" OnClick="CustomerBill" />
                                    </ItemTemplate>
                                    <ControlStyle BorderStyle="None" CssClass="btn btn-info" />
                                </asp:TemplateField>


                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Button3" runat="server" CausesValidation="false" CommandName="" Text="Update Geolocation" OnClick="GeoInfo" />
                                    </ItemTemplate>
                                    <ControlStyle BorderStyle="None" CssClass="btn btn-secondary" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Button4" runat="server" CausesValidation="false" CommandName="" Text="Log Complaint" OnClick="LogComplaint" />
                                    </ItemTemplate>
                                    <ControlStyle BorderStyle="None" CssClass="btn btn-danger" />
                                </asp:TemplateField>


                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Button5" runat="server" CausesValidation="false" CommandName="" Text="Update Meter" OnClick="UpdateMeter" />
                                    </ItemTemplate>
                                    <ControlStyle BorderStyle="None" CssClass="btn btn-warning" />
                                </asp:TemplateField>


                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Button6" runat="server" CausesValidation="false" CommandName="" Text="Send Message" OnClick="SendMessage" />
                                    </ItemTemplate>
                                    <ControlStyle BorderStyle="None" CssClass="btn btn-success" />
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
