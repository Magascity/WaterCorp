<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayBills.aspx.cs" Inherits="KadswacOnline.Customer.PayBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <main aria-labelledby="title">

                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
                <asp:HiddenField ID="HFID" runat="server" />
                <asp:HiddenField ID="HFBillingPeriod" runat="server" />
                <h4>Update Payment</h4>
                <hr />
                <asp:ValidationSummary runat="server" CssClass="text-danger" />

                    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtBillID" CssClass="col-md-2 col-form-label">Bill No</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtBillID" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillID"
            CssClass="text-danger" ErrorMessage="The Bill No field is required." />
    </div>
</div>



                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtDPCNo" CssClass="col-md-2 col-form-label">DPC No</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtDPCNo" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDPCNo"
                            CssClass="text-danger" ErrorMessage="The DPCNo field is required." />
                    </div>
                </div>


                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-2 col-form-label">Mobile</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile"
                            CssClass="text-danger" ErrorMessage="The Mobile field is required." />
                    </div>
                </div>


                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 col-form-label">Email</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                            CssClass="text-danger" ErrorMessage="The Email field is required." />
                    </div>
                </div>

                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtBillingPeriod" CssClass="col-md-2 col-form-label">Billing Period</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtBillingPeriod" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod"
                            CssClass="text-danger" ErrorMessage="The Billing Period field is required." />
                    </div>
                </div>
                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtCustomerName" CssClass="col-md-2 col-form-label">Customer Name</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtCustomerName" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerName"
                            CssClass="text-danger" ErrorMessage="The Customer Name field is required." />
                    </div>
                </div>

                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtAmountDue" CssClass="col-md-2 col-form-label">Amount Due</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtAmountDue" CssClass="form-control" ReadOnly="true" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmountDue"
                            CssClass="text-danger" ErrorMessage="The Amount Due field is required." />
                    </div>
                </div>

               <%-- <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtAmount" CssClass="col-md-2 col-form-label">Amount Tendered </asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmount"
                            CssClass="text-danger" ErrorMessage="The Amount Tendered field is required." />
                    </div>
                </div>--%>


                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="ddlModeOfPayment" CssClass="col-md-2 col-form-label">Mode Of payment </asp:Label>
                    <div class="col-md-10">

                        <asp:DropDownList ID="ddlModeOfPayment" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select Mode Of Payment --</asp:ListItem>
                            <asp:ListItem>Cash</asp:ListItem>
                            <asp:ListItem>POS</asp:ListItem>
                            <asp:ListItem>Transfer</asp:ListItem>
                        </asp:DropDownList>


                        <asp:Panel ID="PnlRef" Visible="false" runat="server">
                            <div class="row">
                                <asp:Label runat="server" AssociatedControlID="txtTransRef" CssClass="col-form-label">Trans Ref </asp:Label>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="txtTransRef" CssClass="form-control" TextMode="SingleLine" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTransRef"
                                        CssClass="text-danger" ErrorMessage="The Transaction Ref field is required." />
                                </div>
                            </div>

                        </asp:Panel>

                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlModeOfPayment" InitialValue="0"
                            CssClass="text-danger" ErrorMessage="The mode of Payment field is required." />
                    </div>
                </div>



                <div class="row">
                    <div class="offset-md-2 col-md-10">
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </div>
            </main>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
