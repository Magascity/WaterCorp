<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBusinessDistricts.aspx.cs" Inherits="WaterCorp.Billing.ViewBusinessDistricts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    
    <h4>View Business Districts</h4>


    <div class="container">
        
          <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" PostBackUrl="~/Billing/CreateBusinessDistricts.aspx">Add New Business District</asp:LinkButton>
        <br />
        <br />
        <div class="panel-body">
          

            <div class="table-responsive">

                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="Id" >

                    <Columns>


                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="DistrictName" HeaderText="DistrictName" SortExpression="DistrictName" />
                        <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                        <asp:CommandField ButtonType="Button" ShowEditButton="True">
                        <ControlStyle BorderStyle="None" CssClass="btn btn-info" />
                        </asp:CommandField>


                    </Columns>


                </asp:GridView>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT  [Id]
      ,[DistrictName]
      ,[Code]
  FROM [_tblSbus]
" UpdateCommand="UPDATE [_tblSbus]
   SET [DistrictName] = @DistrictName
      ,[Code] = @Code
 WHERE Id = @ID">
                    <UpdateParameters>
                        <asp:Parameter Name="DistrictName" />
                        <asp:Parameter Name="Code" />
                        <asp:Parameter Name="ID" />
                    </UpdateParameters>
                </asp:SqlDataSource>

            </div>
            
                </div>

    </div>




</asp:Content>
