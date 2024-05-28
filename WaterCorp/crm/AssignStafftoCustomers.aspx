<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignStafftoCustomers.aspx.cs" Inherits="WaterCorp.crm.AssignStafftoCustomers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

        <div class="col-lg-12">

            <div class="card">
                <div class="card-body">
                    <br />
                    <div class="row g-3">

                        <div class="col-md-8">


                            <div class="row">
                                <asp:Label runat="server" AssociatedControlID="ddlDistrict" CssClass="col-md-4 col-form-label">District</asp:Label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDistrict" InitialValue="0"
                                        CssClass="text-danger" ErrorMessage="The District field is required." />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="row">
                            <asp:Label runat="server" AssociatedControlID="ddlStaff" CssClass="col-md-4 col-form-label">Staff</asp:Label>
                            <div class="col-md-8">
                                <%--<asp:TextBox ID="txtStaff" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStaff" 
                                    CssClass="text-danger" ErrorMessage="The Staff field is required." />
                            </div>
                               
                                </div>
                             <asp:Button ID="btnAssign" runat="server" CssClass="btn btn-success" Text="Assign Staff" OnClick="btnAssign_Click" />
                            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                            <br />
                        </div>
                    </div>
                    <p></p>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="CustReference" DataSourceID="SqlDataSource1">
                            <Columns>

                                 <asp:TemplateField>
                        <HeaderTemplate> 
                            <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                                <%-- <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />--%>
                                <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                                <%--<asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                <asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="Firstname" />
                                <asp:BoundField DataField="Othername" HeaderText="Othername" SortExpression="Othername" />--%>
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
                                <asp:BoundField DataField="AssignedTo" HeaderText="Staff Assigned To" SortExpression="AssignedTo" />

                                

                            </Columns>
                            <EmptyDataTemplate>
                                <span style="color: #FF0000"><strong>No Records Found </strong></span>
                            </EmptyDataTemplate>

                        </asp:GridView>

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT C.[RecordID]
      ,C.[LastName]
        ,C.CustomerName
      ,C.[Firstname]
      ,C.[Othername]
      ,C.[Email]
      ,C.[PhoneNumber]
      ,C.[CustomerAddress]
      ,(SELECT TOP 1 [DistrictName] FROM [_tblSbus] WHERE Code = C.[DistrictCode]) AS District
,C.[DistrictCode]
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
      ,AssignedTo
  FROM [_tblCustomers3] C Where C.Verified = 1  and C.[DistrictCode] = @DistrictCode order by C.RecordID Desc ">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlDistrict" Name="DistrictCode" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </div>




                </div>
            </div>

        </div>

    </div>


    <script type="text/javascript">
        function CheckAll(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>


</asp:Content>
