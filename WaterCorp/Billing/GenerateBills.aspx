<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerateBills.aspx.cs" Inherits="WaterCorp.Billing.GenerateBills" Async="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    

    <style>
        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(255, 255, 255, 0.8);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
        }
    </style>

    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <!-- Your existing UI elements here -->

                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="txtBillingPeriod" CssClass="col-md-2 col-form-label">Billing Period</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtBillingPeriod" CssClass="form-control" TextMode="Month" Width="300px" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod"
                            CssClass="text-danger" ErrorMessage="The Billing Period field is required." />
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-10">
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="Generate Bill" class="btn btn-large btn-primary " OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Label ID="lblMessage" runat="server" Class="col-md-2 control-label"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
            <div class="loading-overlay">
                <!-- Add a loading spinner or progress bar here -->
                <img src="~/images/Loadingprogress.gif" alt="Loading..." runat="server" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>



