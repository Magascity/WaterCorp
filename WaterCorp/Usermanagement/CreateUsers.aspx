﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUsers.aspx.cs" Inherits="WaterCorp.Usermanagement.CreateUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
   <%-- <h2 id="title"><%: Title %>.</h2>--%>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <h4>Create a new account</h4>
    <hr />
    <asp:ValidationSummary runat="server" CssClass="text-danger" />

    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtLastname" CssClass="col-md-2 col-form-label">Lastname</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastname"
            CssClass="text-danger" ErrorMessage="The Lastname field is required." />
    </div>
</div>
            <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtOthernames" CssClass="col-md-2 col-form-label">Othernames</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtOthernames" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOthernames"
            CssClass="text-danger" ErrorMessage="The Othernames field is required." />
    </div>
</div>

                    <div class="row">
    <asp:Label runat="server" AssociatedControlID="ddlSbu" CssClass="col-md-2 col-form-label">Business Unit</asp:Label>
    <div class="col-md-10">
        <asp:DropDownList ID="ddlSbu" runat="server" CssClass="form-control"></asp:DropDownList>        
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSbu"
            CssClass="text-danger" ErrorMessage="The Business Unit field is required." InitialValue="0" />
    </div>
</div>

    <div class="row">
        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 col-form-label">Email</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                CssClass="text-danger" ErrorMessage="The email field is required." />
        </div>
    </div>
    <div class="row">
        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 col-form-label">Password</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                CssClass="text-danger" ErrorMessage="The password field is required." />
        </div>
    </div>
    <div class="row">
        <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 col-form-label">Confirm password</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
        </div>
    </div>
        <br />
    <div class="row">
        <div class="offset-md-2 col-md-10">
            <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-primary" />
        </div>
    </div>
</main>

</asp:Content>
