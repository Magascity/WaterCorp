<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendBulkSms.aspx.cs" Inherits="WaterCorp.crm.SendBulkSms" %>
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
                   
                </div>
                <p></p>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="CustReference" DataSourceID="SqlDataSource1" AllowPaging="True" PageSize="10">

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

             

                </div>




            </div>
        </div>

    </div>


 <div class="col-12">
                  <label for="inputEmail4" class="form-label">Message</label>
                 <asp:TextBox ID="txtMessage" runat="server" class="form-control" TextMode="MultiLine" ></asp:TextBox>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMessage"
                    CssClass="text-danger" ErrorMessage="The Message field is required." />
                </div>



          <asp:Button ID="btnSendSMS" Text="Send SMS" runat="server" CssClass="btn btn-primary"
                Font-Bold="true" OnClick="btnSendSMS_Click" /><br /><br />

            
            <asp:Label ID="lblMessage" runat="server" />          
                     
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
