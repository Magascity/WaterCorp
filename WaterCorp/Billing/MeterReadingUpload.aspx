<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MeterReadingUpload.aspx.cs" Inherits="WaterCorp.Billing.MeterReadingUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FileUpload ID="fupUploads" runat="server" />
    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload File" />
    <asp:Label ID="lblResults" runat="server"></asp:Label>
</asp:Content>
