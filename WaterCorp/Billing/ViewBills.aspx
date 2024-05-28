<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBills.aspx.cs" Inherits="WaterCorp.Billing.ViewBills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h4>View Bills</h4>


    <div class="container">
    <div class="row">
        <div class="col-md-6">
            <asp:Label runat="server" AssociatedControlID="txtBillingPeriod" CssClass="col-form-label">Billing Period</asp:Label>
            <asp:TextBox runat="server" ID="txtBillingPeriod" CssClass="form-control" TextMode="Month" Width="300px"/>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod" CssClass="text-danger" ErrorMessage="The Billing Period field is required." />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <asp:Label runat="server" AssociatedControlID="txtDPCNo" CssClass="col-form-label">DPCNo</asp:Label>
            <asp:TextBox runat="server" ID="txtDPCNo" CssClass="form-control" />
            <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtDPCNo" CssClass="text-danger" ErrorMessage="The DPCNo field is required." />--%>
        </div>

        <div class="col-md-6">
            <asp:Label runat="server" AssociatedControlID="txtMobileNo" CssClass="col-form-label">MobileNo</asp:Label>
            <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" />
            <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobileNo" CssClass="text-danger" ErrorMessage="The Mobile No field is required." />--%>
        </div>
    </div>
        <p></p>

    <div class="row">
        <div class="col-md-12">
            <asp:Button runat="server" ID="btnExport" Text="Show Bills" OnClick="btnExport_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</div>


    <div class="panel-body">

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="WaterBillID" DataSourceID="SqlDataSource1" >
                <Columns>

                    <asp:BoundField DataField="WaterBillID" HeaderText="WaterBillID" InsertVisible="False" ReadOnly="True" SortExpression="WaterBillID" />
                    <asp:BoundField DataField="DPCNo" HeaderText="DPCNo" SortExpression="DPCNo" />
                    <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="MeterNo" HeaderText="MeterNo" SortExpression="MeterNo" />
                    <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                    <asp:BoundField DataField="BillingPeriod" DataFormatString="{0:d}" HeaderText="BillingPeriod" SortExpression="BillingPeriod" />
                    <asp:BoundField DataField="PresentReading" HeaderText="PresentReading" SortExpression="PresentReading" />
                    <asp:BoundField DataField="PreviousReading" HeaderText="PreviousReading" SortExpression="PreviousReading" />
                    <asp:BoundField DataField="Consumption" HeaderText="Consumption" SortExpression="Consumption" />
                    <asp:BoundField DataField="CurrentCharge" HeaderText="CurrentCharge" SortExpression="CurrentCharge" />
                    <asp:BoundField DataField="OutstandingCharges" HeaderText="OutstandingCharges" SortExpression="OutstandingCharges" />
                    <asp:BoundField DataField="LastPaymentDate" DataFormatString="{0:d}" HeaderText="LastPaymentDate" SortExpression="LastPaymentDate" />
                    <asp:BoundField DataField="LastPaymentAmount" HeaderText="LastPaymentAmount" SortExpression="LastPaymentAmount" />
                    <asp:BoundField DataField="TotalDue" HeaderText="TotalDue" SortExpression="TotalDue" />
                    <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                    <asp:BoundField DataField="DateCreated" DataFormatString="{0:d}" HeaderText="DateCreated" SortExpression="DateCreated" />
                    <asp:BoundField DataField="PaymentDate" DataFormatString="{0:d}" HeaderText="PaymentDate" SortExpression="PaymentDate" />
                    <asp:BoundField DataField="PaymentAmount" HeaderText="PaymentAmount" SortExpression="PaymentAmount" />
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
  FROM [WaterBills] WHERE [BillingPeriod] = @SelectedDate">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtBillingPeriod" DbType="Date" Name="SelectedDate" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>
    </div>

</asp:Content>
