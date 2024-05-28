﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerUpdates.aspx.cs" Inherits="WaterCorp.crm.CustomerUpdates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <main aria-labelledby="title">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
                <h4>Create a new Customer account</h4>
                <hr />
                <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <asp:HiddenField ID="HFRecordID" runat="server" />
                <asp:HiddenField ID="HFTarrif" runat="server" />
                <asp:HiddenField ID="HFConsumption" runat="server" />
                <asp:HiddenField ID="HFCharges" runat="server" />

                <div class="row">
                    <div class="col-md-6">
                        <!-- Column 1 -->
                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtDPCNo" CssClass="col-md-4 col-form-label">DPCNo</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtDPCNo" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDPCNo"
                                    CssClass="text-danger" ErrorMessage="The DPCNo field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtCustomerName" CssClass="col-md-4 col-form-label">Customer</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCustomerName" CssClass="form-control" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerName"
                                    CssClass="text-danger" ErrorMessage="The Customer name field is required." />
                            </div>
                        </div>

                        <%--<div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtFirstname" CssClass="col-md-4 col-form-label">Firstname</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstname"
                                    CssClass="text-danger" ErrorMessage="The Firstname field is required." />
                            </div>
                        </div>--%>

                       <%-- <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtMiddlename" CssClass="col-md-4 col-form-label">Middle Name</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtMiddlename" CssClass="form-control" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMiddlename"
                                    CssClass="text-danger" ErrorMessage="The Middle names field is required." />
                                <br />
                            </div>
                        </div>--%>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtCustomerAddress" CssClass="col-md-4 col-form-label">Customer Address</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCustomerAddress" CssClass="form-control" TextMode="MultiLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerAddress"
                                    CssClass="text-danger" ErrorMessage="The Address field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-4 col-form-label">Mobile</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" TextMode="SingleLine" MaxLength="11" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile"
                                    CssClass="text-danger" ErrorMessage="The Mobile field is required." />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMobile"
                                    CssClass="text-danger" ErrorMessage="Enter exactly 11 numbers." ValidationExpression="\d{11}" />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-4 col-form-label">Email</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                                    CssClass="text-danger" ErrorMessage="The email field is required." />
                            </div>
                        </div>



                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="ddlDistrictCode" CssClass="col-md-4 col-form-label">District Code</asp:Label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlDistrictCode" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDistrictCode" InitialValue="0"
                                    CssClass="text-danger" ErrorMessage="The District Code field is required." />
                            </div>
                        </div>


                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtZoneCode" CssClass="col-md-4 col-form-label">Zone-Code</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtZoneCode" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtZoneCode"
                                    CssClass="text-danger" ErrorMessage="The Zone Code field is required." />
                            </div>
                        </div>



                    </div>

                    <div class="col-md-6">
                        <!-- Column 2 -->



                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtSubzone" CssClass="col-md-4 col-form-label">Sub-zone</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtSubzone" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSubzone"
                                    CssClass="text-danger" ErrorMessage="The Sub Code field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtRound" CssClass="col-md-4 col-form-label">Round</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtRound" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRound"
                                    CssClass="text-danger" ErrorMessage="The Round field is required." />
                            </div>
                        </div>



                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtFoliono" CssClass="col-md-4 col-form-label">Foliono</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtFoliono" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFoliono"
                                    CssClass="text-danger" ErrorMessage="The Foliono field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="ddlTarrifCategory" CssClass="col-md-4 col-form-label">Tarrif Type</asp:Label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlTarrifCategory" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTarrifCategory_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTarrifCategory" InitialValue="0"
                                    CssClass="text-danger" ErrorMessage="The Tarrif Category field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="chkMetered" CssClass="col-md-4 col-form-label">Meter Status</asp:Label>
                            <div class="col-md-8">
                                <asp:CheckBox ID="chkMetered" runat="server" CssClass="form-control" Enabled="false" />
                                <asp:Label ID="lblMetered" runat="server"></asp:Label>
                                <br />
                                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="chkMetered"
        CssClass="text-danger" ErrorMessage="The email field is required." />--%>
</di
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtMeterNo" CssClass="col-md-4 col-form-label">Meter-no</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtMeterNo" CssClass="form-control" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeterNo"
                                    CssClass="text-danger" ErrorMessage="The Meterno field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtTarrif" CssClass="col-md-4 col-form-label">Tarrif</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtTarrif" CssClass="form-control" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTarrif"
                                    CssClass="text-danger" ErrorMessage="The Tarrif field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtConsumption" CssClass="col-md-4 col-form-label">Consumption</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtConsumption" CssClass="form-control" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConsumption"
                                    CssClass="text-danger" ErrorMessage="The Consumption field is required." />
                            </div>
                        </div>



                    </div>
                </div>

                <div class="row">
                    <div class="offset-md-2 col-md-10">
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
                        <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Visible="false" Text="Verify" CssClass="btn btn-success" />
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </div>
            </main>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
