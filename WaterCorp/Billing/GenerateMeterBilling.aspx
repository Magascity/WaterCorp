<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerateMeterBilling.aspx.cs" Inherits="WaterCorp.Billing.GenerateMeterBilling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <main aria-labelledby="title">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
                <h4>Generate Bill for Metered Customers</h4>
                <hr />
                <asp:ValidationSummary runat="server" CssClass="text-danger" />



                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="ddlDistrictCode" CssClass="col-md-2 col-form-label">District Code</asp:Label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlDistrictCode" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDistrictCode" InitialValue="0"
                            CssClass="text-danger" ErrorMessage="The District Code field is required." />
                    </div>
                </div>


                <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtBillingPeriod" CssClass="col-md-2 col-form-label">Billing Period</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtBillingPeriod" CssClass="form-control" TextMode="Month" Width="300px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillingPeriod"
            CssClass="text-danger" ErrorMessage="The Billing Period field is required." />
    </div>
</div>

                <div class="row">
                    <div class="col-md-10">
                    <asp:Button ID="btnProcess" runat="server" Text="Process Bill" OnClick="btnProcess_Click" CssClass="btn btn-warning" />
                    <asp:Label ID="lblMessage" runat="server" >  </asp:Label>
                    </div>
    
                </div>
</div>

                <!-- Progress bar -->
                <div id="progressBarContainer" style="display:none; margin-top: 20px;">
                    <div id="progressBar" class="progress">
                        <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"></div>
                    </div>
                </div>


            </main>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- JavaScript for progress bar -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        function ShowProgressBar() {
            // Show the progress bar
            $('#progressBar').css('width', '0%');
            $('#progressBarContainer').show();

            // Perform the AJAX call
            $.ajax({
                type: 'POST',
                url: 'GenerateMeterBilling.aspx/GenerateMonthlyUnmeteredWaterBill',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    // Hide the progress bar when the process is complete
                    $('#progressBarContainer').hide();
                    // Redirect to another page or perform any other action
                    window.location.href = 'Default.aspx';
                },
                error: function (xhr, status, error) {
                    // Handle errors
                    console.log(error);
                    alert('An error occurred while processing the request.');
                    $('#progressBarContainer').hide();
                }
            });
        }
    </script>

</asp:Content>
