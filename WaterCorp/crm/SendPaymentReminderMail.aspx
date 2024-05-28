<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendPaymentReminderMail.aspx.cs" Inherits="WaterCorp.crm.SendPaymentReminderMail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="message"></asp:Label>
<div id="progressBarContainer" style="display:none;">
    <div id="progressBar" class="progress-bar" role="progressbar" style="width: 0%;" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
</div>

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    $(document).ready(function () {
        $('#progressBarContainer').show();
        $('#progressBar').animate({ width: '100%' }, 5000, function () {
            $('#progressBarContainer').hide();
        });
    });
</script>


</asp:Content>
