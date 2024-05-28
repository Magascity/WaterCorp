<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateMeterStock.aspx.cs" Inherits="WaterCorp.Store.CreateMeterStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <script type="text/javascript">
       function filterDropdown() {
           var input, filter, ddl, options, txtValue, i;
           input = document.getElementById('<%= txtFilter.ClientID %>');
    filter = input.value.toUpperCase();
           ddl = document.getElementById('<%= ddlItemCategory.ClientID %>');
           options = ddl.getElementsByTagName('option');

           // Track whether any visible option has been found
           var visibleOptionFound = false;

           for (i = 0; i < options.length; i++) {
               txtValue = options[i].textContent || options[i].innerText;

               if (txtValue.toUpperCase().indexOf(filter) > -1) {
                   options[i].style.display = "";

                   // If this is the first visible option after filtering,
                   // select it and set the flag to true
                   if (!visibleOptionFound) {
                       options[i].selected = true;
                       visibleOptionFound = true;
                   }
               } else {
                   options[i].style.display = "none";
               }
           }
       }


   </script>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <main aria-labelledby="title">
     
     <p class="text-danger">
         <asp:Literal runat="server" ID="ErrorMessage" />
     </p>
     <h4>Create a new Meter Stock</h4>
     <hr />
     <asp:ValidationSummary runat="server" CssClass="text-danger" />
         
   <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtItemName" CssClass="col-md-2 col-form-label">Item Name</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtItemName" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemName"
            CssClass="text-danger" ErrorMessage="The Item Name field is required." />
    </div>
</div>

        <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtItemDescription" CssClass="col-md-2 col-form-label">Item Description</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtItemDescription" CssClass="form-control" TextMode="SingleLine" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemDescription"
            CssClass="text-danger" ErrorMessage="The Item Description field is required." />
    </div>
</div>

        <div class="row">

    
    <asp:Label runat="server" AssociatedControlID="ddlItemCategory" CssClass="col-md-2 col-form-label">Item Category</asp:Label>
     <br />
            <asp:TextBox ID="txtFilter" runat="server" onkeyup="filterDropdown()" />
            
            <div class="col-md-10">
        <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemCategory" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The Category field is required." />
    </div>
</div>

                 <div class="row">
    <asp:Label runat="server" AssociatedControlID="ddlItemSubCategory" CssClass="col-md-2 col-form-label">Sub Category</asp:Label>
    <div class="col-md-10">
        <asp:DropDownList ID="ddlItemSubCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemSubCategory" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The Sub Category field is required." />
    </div>
</div>

    <div class="row">
    <asp:Label runat="server" AssociatedControlID="ddlUnit" CssClass="col-md-2 col-form-label">Unit</asp:Label>
    <div class="col-md-10">
        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnit" InitialValue="0"
            CssClass="text-danger" ErrorMessage="The Unit field is required." />
    </div>
</div>

    <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtUnitPrice" CssClass="col-md-2 col-form-label">Unit Price</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtUnitPrice" CssClass="form-control" TextMode="SingleLine"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUnitPrice"
            CssClass="text-danger" ErrorMessage="The Unit field is required." />
      
    </div>
</div>

     <div class="row">
    <asp:Label runat="server" AssociatedControlID="txtReOrderQty" CssClass="col-md-2 col-form-label">Re-Order Qty</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="txtReOrderQty" CssClass="form-control" TextMode="SingleLine"  />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtReOrderQty"
            CssClass="text-danger" ErrorMessage="The Reorder Qty field is required." />
      
    </div>
</div>

     <div class="row">
         <asp:Label runat="server" AssociatedControlID="txtReOrderLevel" CssClass="col-md-2 col-form-label">Re-Order Level</asp:Label>
         <div class="col-md-10">
             <asp:TextBox runat="server" ID="txtReOrderLevel" CssClass="form-control"  />
             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtReOrderLevel"
                 CssClass="text-danger" ErrorMessage="The ReOrderLevel field is required." />
         </div>
     </div>

    
    
          <div class="row">
     <asp:Label runat="server" AssociatedControlID="txtQuantity" CssClass="col-md-2 col-form-label">Quantity</asp:Label>
     <div class="col-md-10">
         <asp:TextBox runat="server" ID="txtQuantity" CssClass="form-control"  />
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQuantity"
             CssClass="text-danger" ErrorMessage="The Quantity field is required." />
     </div>
 </div>

                       <div class="row">
    <asp:Label runat="server" AssociatedControlID="fupUploadMeters" CssClass="col-md-2 col-form-label">Meter Files</asp:Label>
    <div class="col-md-10">
        <asp:FileUpload ID="fupUploadMeters" CssClass="form-control"  runat="server" />
               <asp:RequiredFieldValidator runat="server" ControlToValidate="fupUploadMeters"
            CssClass="text-danger" ErrorMessage="The File field is required." />
    </div>
</div> 

     <div class="row">
         <div class="offset-md-2 col-md-10">
             <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Register" CssClass="btn btn-primary" />
         </div>
     </div>
 </main>
         </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
