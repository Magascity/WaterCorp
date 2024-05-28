<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateComplaintsStatus.aspx.cs" Inherits="WaterCorp.crm.UpdateComplaintsStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <main aria-labelledby="title">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
                <h4>Update Customer Complaints Status</h4>
                <hr />
                <asp:ValidationSummary runat="server" CssClass="text-danger" />

                <asp:HiddenField ID="HFComplaintID" runat="server" />
                <asp:HiddenField ID="HFCustomerID" runat="server" />


                <div class="row">
                    <div class="col-md-6">
                        <!-- Column 1 -->
                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtComplaintID" CssClass="col-md-4 col-form-label">Complaint ID</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtComplaintID" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtComplaintID"
                                    CssClass="text-danger" ErrorMessage="The ComplaintID field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtCustomerID" CssClass="col-md-4 col-form-label">Customer-ID</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCustomerID" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerID"
                                    CssClass="text-danger" ErrorMessage="The Customer ID field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtDpcNo" CssClass="col-md-4 col-form-label">DPC-No</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtDpcNo" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDpcNo"
                                    CssClass="text-danger" ErrorMessage="The DPC-No field is required." />
                            </div>
                        </div>


                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtCustomername" CssClass="col-md-4 col-form-label">Customer Name</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCustomername" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomername"
                                    CssClass="text-danger" ErrorMessage="The Customer Name field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtAddress" CssClass="col-md-4 col-form-label">Customer Address</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress"
                                    CssClass="text-danger" ErrorMessage="The Address field is required." />
                            </div>
                        </div>


                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-4 col-form-label">Mobile</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress"
                                    CssClass="text-danger" ErrorMessage="The Address field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-4 col-form-label">Email</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                                    CssClass="text-danger" ErrorMessage="The Email field is required." />
                            </div>
                        </div>


                    </div>

                    <div class="col-md-6">


                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="ddlService" CssClass="col-md-4 col-form-label">Service</asp:Label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlService" InitialValue="0"
                                    CssClass="text-danger" ErrorMessage="The Service field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="ddlSubService" CssClass="col-md-4 col-form-label">Sub-Service</asp:Label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlSubService" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSubService" InitialValue="0"
                                    CssClass="text-danger" ErrorMessage="The Sub Service field is required." />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtDescription" CssClass="col-md-4 col-form-label">Description</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription"
                                    CssClass="text-danger" ErrorMessage="The Description field is required." />
                            </div>
                        </div>



                    <div class="row">
                        <asp:Label runat="server" AssociatedControlID="ddlSBU" CssClass="col-md-4 col-form-label">SBU</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlSBU" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSBU" InitialValue="0"
                                CssClass="text-danger" ErrorMessage="The Business field is required." />
                        </div>
                    </div>

                     <asp:Panel ID="PnlAck" runat="server" Visible="false" >
                    <div class="row">
                        <asp:Label runat="server" AssociatedControlID="txtAcknowledgement" CssClass="col-md-4 col-form-label">Acknoledgement Notes</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtAcknowledgement" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAcknowledgement"
                                CssClass="text-danger" ErrorMessage="The Acknowledgment field is required." />
                        </div>
                    </div>
                </asp:Panel>

                         <asp:Panel ID="PnlResolution" runat="server" Visible="false" >
    <div class="row">
        <asp:Label runat="server" AssociatedControlID="txtResolution" CssClass="col-md-4 col-form-label">Resolution Notes</asp:Label>
        <div class="col-md-8">
            <asp:TextBox ID="txtResolution" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtResolution"
                CssClass="text-danger" ErrorMessage="The Resolution field is required." />
        </div>
    </div>
</asp:Panel>





                </div>
                </div>

                <div class="row">
                    <div class="offset-md-2 col-md-10">
                        <asp:Button ID="btnAck" runat="server" OnClick="btnAck_Click" Text="Acknowledge" CssClass="btn btn-primary" />
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Resolve" CssClass="btn btn-primary" />
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </div>
            </main>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>



</asp:Content>
