<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBills.aspx.cs" Inherits="KadswacOnline.Customer.ViewBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <h4>View Bills</h4>


    <div class="container">
        <asp:HiddenField ID="HFTotalDue" runat="server" />
        <asp:HiddenField ID="HFDpcNo" runat="server" />

        <div class="row">
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtDPCNo" CssClass="col-form-label">DPCNo</asp:Label>
                <asp:TextBox runat="server" ID="txtDPCNo" CssClass="form-control" ReadOnly="true" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDPCNo" CssClass="text-danger" ErrorMessage="The DPCNo field is required." />
            </div>
        </div>

        


        <asp:Panel ID="PnlDetails" runat="server" Visible="True">
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


            <asp:Label ID="lblMessage" runat="server" ></asp:Label>


        </asp:Panel>


        <div class="panel-body">

            <div class="table-responsive">

                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="WaterBillID" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">

                    <Columns>


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


                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="" Text="Pay Bill" OnClick="Assess" />
                            </ItemTemplate>
                            <ControlStyle BorderStyle="None" CssClass="btn btn-warning" />
                        </asp:TemplateField>


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
  FROM [WaterBills] WHERE DPCNo = @DPCNo">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtDPCNo" Name="DPCNo" PropertyName="Text" DefaultValue="01-87-45-89-40" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>
            <asp:Panel ID="pnlPayment" Visible="true" runat="server">
            <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount: $0.00"></asp:Label>
            <br />

            <asp:Button ID="btnPayAll" runat="server" Text="Pay All Bills" CssClass="btn btn-success" OnClick="btnPayAll_Click" />
       </asp:Panel>
                </div>

    </div>

</asp:Content>
