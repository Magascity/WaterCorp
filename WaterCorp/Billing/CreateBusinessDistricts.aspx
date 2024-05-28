<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBusinessDistricts.aspx.cs" Inherits="WaterCorp.Billing.CreateBusinessDistricts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

                    <main aria-labelledby="title">
    <%--<h2 id="title"><%: Title %>.</h2>--%>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <h4>Create New District</h4>
    <hr />
    <asp:ValidationSummary runat="server" CssClass="text-danger" />

                

    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDistrictName" CssClass="col-md-2 col-form-label">District Name</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDistrictName" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDistrictName"
            CssClass="text-danger" ErrorMessage="The District Name field is required." />
    </div>
</div>

                <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtDistrictCode" CssClass="col-md-2 col-form-label">District Code</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtDistrictCode" CssClass="form-control"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDistrictCode"
            CssClass="text-danger" ErrorMessage="The Lastname field is required." />
    </div>
</div>

    
     
        <br />
    <div class="row">
        <div class="offset-md-2 col-md-10">

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                />
            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
        </div>
    </div>
</main>

</asp:Content>
