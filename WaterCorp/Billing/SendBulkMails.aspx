<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendBulkMails.aspx.cs" Inherits="WaterCorp.Billing.SendBulkMails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Add your CSS styles here */
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }

        .email-container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .header {
            background-color: #4CAF50;
            color: #fff;
            padding: 15px;
            text-align: center;
            border-radius: 5px 5px 0 0;
        }

        .content {
            padding: 20px;
        }

        .footer {
            background-color: #f5f5f5;
            padding: 15px;
            text-align: center;
            border-radius: 0 0 5px 5px;
        }
    </style>

    <div class="content">
            <p>Dear {FirstName} {LastName},</p>
            <p>{Content}</p>
            <p>Thank you for choosing our services!</p>
        </div>
        <div class="footer">
            <p>Contact us at support@yourcompany.com | Visit our website: www.yourcompany.com</p>
        </div>

</asp:Content>
