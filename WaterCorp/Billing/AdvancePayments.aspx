<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdvancePayments.aspx.cs" Inherits="WaterCorp.Billing.AdvancePayments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Advance Payment</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />
         <asp:HiddenField ID="HFTarrif" runat="server" />
         <asp:HiddenField ID="HFConsumption" runat="server" />
         <asp:HiddenField ID="HFCharges" runat="server" />
                 <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDpcNo" CssClass="col-md-2 col-form-label">DPCNo</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDpcNo" CssClass="form-control"  TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDpcNo"
            CssClass="text-danger" ErrorMessage="The DpcNo field is required." /><br />

        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />

        <asp:Label ID="lblResult" runat="server" ></asp:Label>
    </div>
        
                     
</div>


         <asp:Panel ID="Panel1" runat="server" Visible="false">
             <p></p>
             <div class="table-responsive">
                 <asp:GridView ID="GridView1" CssClass="table table-hover" runat="server"></asp:GridView>
                 </div>
             <p></p>
                     <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtAmountDue" CssClass="col-md-2 col-form-label">Total Due</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtAmountDue" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmountDue"
            CssClass="text-danger" ErrorMessage="The Amount Due field is required." />
    </div>
</div>

     <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtAmount" CssClass="col-md-2 col-form-label">Mode Of payment </asp:Label>
    <div class="col-md-10">

        <asp:DropDownList ID="ddlModeOfPayment" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged">
            <asp:ListItem Value="0">--Select Mode Of Payment --</asp:ListItem>
            <asp:ListItem>Cash</asp:ListItem>
            <asp:ListItem>POS</asp:ListItem>
            <asp:ListItem>Transfer</asp:ListItem>
        </asp:DropDownList>
        </div>   
         </div>
        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtTransRef" CssClass="col-form-label">Trans Ref </asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtTransRef" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTransRef"
            CssClass="text-danger" ErrorMessage="The Transaction Ref field is required." />
    </div>
</div>



     <div class="row">
         <div class="offset-md-2 col-md-10">
             <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
             <asp:Label ID="lblMessage" runat="server" ></asp:Label>
         </div>
     </div>

    </asp:Panel>
 </main>
         </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
