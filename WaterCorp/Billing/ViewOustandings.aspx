<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOustandings.aspx.cs" Inherits="WaterCorp.Billing.ViewOustandings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

    <h4>View Bills</h4>
    <asp:HiddenField ID="HFSequenceNumber" runat="server" />

    <div class="container">
        <asp:HiddenField ID="HFTotalDue" runat="server" />

        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtDPCNo" CssClass="col-form-label">DPCNo</asp:Label>
                <asp:TextBox runat="server" ID="txtDPCNo" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDPCNo" CssClass="text-danger" ErrorMessage="The DPCNo field is required." />
            </div>
        </div>

               <p></p>

        <div class="row">
            <div class="col-md-12">
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
               
                <asp:Label ID="lblMessage" runat="server" ></asp:Label><p></p>
            </div>
        </div>


        <asp:Panel ID="PnlDetails" runat="server" Visible="false">
    <div class="row">
    <div class="col-md-6">
        <asp:Label runat="server" AssociatedControlID="txtCustomerName" CssClass="col-form-label">Customer Name</asp:Label>
        <asp:TextBox runat="server" ID="txtCustomerName" CssClass="form-control" ReadOnly="true" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerName" CssClass="text-danger" ErrorMessage="The Name field is required." />
    </div>

    <div class="col-md-6">
        <asp:Label runat="server" AssociatedControlID="txtCustomerEmail" CssClass="col-form-label">Email</asp:Label>
        <asp:TextBox runat="server" ID="txtCustomerEmail" CssClass="form-control" ReadOnly="true" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerEmail" CssClass="text-danger" ErrorMessage="The Email field is required." />
    </div>
</div>

<div class="row">
    <div class="col-md-6">
        <asp:Label runat="server" AssociatedControlID="txtCustomerPhone" CssClass="col-form-label">Mobile</asp:Label>
        <asp:TextBox runat="server" ID="txtCustomerPhone" CssClass="form-control" ReadOnly="true" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerPhone" CssClass="text-danger" ErrorMessage="The Mobile field is required." />
    </div>

    <div class="col-md-6">
        <asp:Label runat="server" AssociatedControlID="txtAddress" CssClass="col-form-label">Address</asp:Label>
        <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" ReadOnly="true" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress" CssClass="text-danger" ErrorMessage="The Address field is required." />
    </div>
</div>





        </asp:Panel>


        <div class="panel-body">

            <div class="table-responsive">

                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="WaterBillID" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">

                    <Columns>

                           <asp:TemplateField>
                        <HeaderTemplate> 
                            <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="WaterBillID" HeaderText="WaterBillID" InsertVisible="False" ReadOnly="True" SortExpression="WaterBillID" />
                        <asp:BoundField DataField="DPCNo" HeaderText="DPCNo" SortExpression="DPCNo" />
                        <asp:BoundField DataField="MeterNo" HeaderText="MeterNo" SortExpression="MeterNo" />
                        <asp:BoundField DataField="BillingPeriod" DataFormatString="{0:d}" HeaderText="BillingPeriod" SortExpression="BillingPeriod" />
                        <asp:BoundField DataField="PresentReading" HeaderText="PresentReading" SortExpression="PresentReading" />
                        <asp:BoundField DataField="PreviousReading" HeaderText="PreviousReading" SortExpression="PreviousReading" />
                        <asp:BoundField DataField="Consumption" HeaderText="Consumption" SortExpression="Consumption" />
                        <asp:BoundField DataField="CurrentCharge" HeaderText="CurrentCharge" SortExpression="CurrentCharge" />
                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" SortExpression="PaymentDate" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="PaymentAmount" HeaderText="PaymentAmount" SortExpression="PaymentAmount"  />
                        
                        <asp:BoundField DataField="Paid" HeaderText="Payment Status" SortExpression="Paid" />


                    </Columns>


                </asp:GridView>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [WaterBillID]
      ,[DPCNo]
      ,[CustomerName]
      ,[Address]
       ,Mobile
       ,Email
      ,[MeterNo]
      ,[CustomerID]
      ,[BillingPeriod]
      ,[PresentReading]
      ,[PreviousReading]
      ,[Consumption]
      ,[CurrentCharge]
      ,[OutstandingCharges]
      ,[LastPaymentDate]
      ,[LastPaymentAmount]
      ,[TotalDue]
      ,[CreatedBy]
      ,[DateCreated]
      ,[PaymentDate]
      ,[PaymentAmount]
                ,Paid
  FROM [WaterBills] WHERE DPCNo = @DPCNo and PaymentAmount &lt;= 0">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtDPCNo" Name="DPCNo" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>
            <asp:Panel ID="pnlPayment" Visible="false" runat="server">
            <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount: $0.00"></asp:Label>
            <br />
                <p></p>
            <asp:Button ID="btnPayAll" runat="server" Text="Pay Select Bills" CssClass="btn btn-success" OnClick="btnPayAll_Click" />
       </asp:Panel>
                </div>


                 <asp:Panel ID="pnlPaymentMode" runat="server" Visible="false">

                        <div class="row">
                    <asp:Label runat="server" AssociatedControlID="ddlModeOfPayment" CssClass="col-form-label">Mode Of payment </asp:Label>
                    <div class="col-md-6">

                        <asp:DropDownList ID="ddlModeOfPayment" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select Mode Of Payment --</asp:ListItem>
                            <asp:ListItem>Cash</asp:ListItem>
                            <asp:ListItem>POS</asp:ListItem>
                            <asp:ListItem>Transfer</asp:ListItem>
                        </asp:DropDownList>
                        
                            
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlModeOfPayment" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The mode of Payment field is required." />
                </div>
                </div>

                        </asp:Panel>
                        <asp:Panel ID="PnlRef" Visible="false" runat="server">
                            <div class="row">
                                <asp:Label runat="server" AssociatedControlID="txtTransRef" CssClass="col-form-label">Trans Ref </asp:Label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtTransRef" CssClass="form-control" TextMode="SingleLine" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTransRef"
                                        CssClass="text-danger" ErrorMessage="The Transaction Ref field is required." />
                                </div>
                            </div>


                            
               

                        </asp:Panel>


        <asp:Panel ID="pnlButton" runat="server" Visible="false">
            <p></p>
             <div class="row">
     <div class="col-md-6">
         <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
         <asp:Label ID="Label1" runat="server"></asp:Label>
     </div>
 </div>

        </asp:Panel>




    </div>


     <script type="text/javascript">
         function CheckAll(Checkbox) {
             var GridVwHeaderCheckbox = document.getElementById("<%=GridView1.ClientID %>");
             for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                 GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
             }
         }
     </script>

    <script type="text/javascript">
        function calculateTotalAmountDue() {
            var totalAmountDue = 0;
            var gridView = document.getElementById('<%= GridView1.ClientID %>');

        for (var i = 1; i < gridView.rows.length; i++) {
            var chkSelect = gridView.rows[i].cells[0].getElementsByTagName("input")[0];
            var currentChargeCell = gridView.rows[i].cells[8]; // Assuming CurrentCharge is in the 8th cell (index 7)

            if (chkSelect.checked) {
                var currentCharge = parseFloat(currentChargeCell.innerText.trim());
                if (!isNaN(currentCharge)) {
                    totalAmountDue += currentCharge;
                }
            }
        }

        // Display the TotalAmountDue
        document.getElementById('<%= lblTotalAmount.ClientID %>').innerText = "Total Amount Due: " + totalAmountDue.toFixed(2);
        }
    </script>



</asp:Content>
