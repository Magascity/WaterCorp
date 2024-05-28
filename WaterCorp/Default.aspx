<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WaterCorp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 


    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">KADSWAC</h1>
            <p class="lead">Welcome to KADSWAC ERP</p>
            <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-md">Learn more &raquo;</a></p>--%>
        </section>

        <div class="row">
             <!-- Image Section -->
    <section class="col-md-8" aria-labelledby="hostingTitle">
        <!-- Image -->
        <img src="images/kadswacFlyer1.png" alt="Your Image" class="img-fluid" />
    </section>
            <%--<section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Getting started</h2>
                <p>
                    ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
                A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="librariesTitle">
                <h2 id="librariesTitle">Get more libraries</h2>
                <p>
                    NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
                </p>
            </section>--%>
            <section class="col-md-4" aria-labelledby="hostingTitle">
                   <asp:Panel ID="pnlLogin" runat="server">
                       <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
    <p class="text-danger">
        <asp:Literal runat="server" ID="FailureText" />
    </p>
</asp:PlaceHolder>
    <asp:Login ID="Login1" runat="server" 
        DestinationPageUrl="~/SecurePage.aspx" 
        FailureText="Invalid username or password. Please try again."
        OnAuthenticate="Login1_Authenticate">

        <LayoutTemplate>
            <table>
                <tr>
                    <td>
                        <label for="UserName">Username:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                            ControlToValidate="UserName" ErrorMessage="Username is required." ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="Password">Password:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                            ControlToValidate="Password" ErrorMessage="Password is required." ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                   

                    <td colspan="2">
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" CssClass="btn btn-primary" />
                    </td>
                
                </tr>
            </table>
        </LayoutTemplate>
    </asp:Login>
                                               <p>
     <%--Enable this once you have account confirmation enabled for password reset functionality--%>
    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled" NavigateUrl="~/Account/Forgot.aspx">Forgot your password?</asp:HyperLink>
    
</p>

</asp:Panel>
            </section>
        </div>
    </main>

</asp:Content>
