<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomersList.aspx.cs" Inherits="WaterCorp.Reports.CustomersList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <div class="row">

    <div class="col-lg-12">

        <div class="card">
            <div class="card-body">
                <h4>Customer List by Active, Inactive, All</h4>
                <br />
                <div class="row g-3">

                    <div class="col-md-8">
                        
                        <asp:DropDownList ID="ddlActiveStatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlActiveStatus_SelectedIndexChanged">
    
    <asp:ListItem Text="Active" Value="1" />
    <asp:ListItem Text="Inactive" Value="0" />
</asp:DropDownList>

                    </div>
                    <%--<div class="col-md-4">

                        <asp:Button ID="btnSearch" runat="server" Text="Add New Customer" CssClass="btn btn-info" OnClick="btnSearch_Click" />

                        <br />
                    </div>--%>
                </div>
                <p></p>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="true">
                        <Columns>

                           <%-- <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />                            --%>

                 
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
