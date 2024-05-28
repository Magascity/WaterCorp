<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendBulkEmail.aspx.cs" Inherits="WaterCorp.crm.SendBulkEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Button ID="btnSendBulkEmails" runat="server" Text="Send Mails" />

    <asp:Label ID="lblStatus" runat="server" ></asp:Label>
</asp:Content>
