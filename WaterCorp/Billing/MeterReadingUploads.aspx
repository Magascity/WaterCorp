<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MeterReadingUploads.aspx.cs" Inherits="WaterCorp.Billing.MeterReadingUploads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

              <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtBillingPeriod" CssClass="col-md-2 col-form-label">Billing Period</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtBillingPeriod" CssClass="form-control" TextMode="Month" Width="300px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod"
            CssClass="text-danger" ErrorMessage="The Billing Period field is required." />
    </div>
</div>


    <%-- <div class="form-group row">
     <label class="col-md-3">Year</label>
     <div class="col-md-9">
         <asp:TextBox ID="txtBillingPeriod" CssClass="form-control input-sm" runat="server" TextMode="Month"></asp:TextBox>
         
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod" CssClass="text-danger" ErrorMessage="The Billing Period is Required." />
     </div>
     </div>--%>

 

    <div class="col-md-10">

        <asp:FileUpload ID="fupFile" runat="server" CssClass="form-control input-sm" />


        <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="fupFile"
                CssClass="text-danger" ErrorMessage="The File Field is Required." />--%>
    </div>



    <div class="form-group">

        <div class="col-md-10">
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Upload Meter Readings" class="btn btn-large btn-primary " OnClick="btnSave_Click" />
            &nbsp
        <asp:Label ID="lblMessage" runat="server" Class="col-md-2 control-label"></asp:Label>
            &nbsp;<asp:Label ID="LtrlFinalMessage" runat="server" Class="col-md-2 control-label"></asp:Label>
        </div>
    </div>



    <div class="panel-body">
        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover">
            </asp:GridView>

        </div>
    </div>
</asp:Content>
