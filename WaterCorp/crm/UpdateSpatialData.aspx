<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateSpatialData.aspx.cs" Inherits="WaterCorp.crm.UpdateSpatialData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="HFRecordID" runat="server" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_GOOGLE_MAPS_API_KEY&libraries=places" async defer></script>

    <script>
        window.onload = function () {
            initMap();
        };
    </script>

    <div>
            <h1>Customer Property Location</h1>
            <div id="map" style="height: 300px;"></div>
        <br />
            <%--<div id="map2" style="height: 300px;"></div> <br />--%>
           
        </div>

        <script>
            //var map = L.map('map').setView([0, 0], 2); // Set initial view
            var map = L.map('map').setView([10.5264, 7.4388], 13); // Default to Kaduna, Nigeria

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; OpenStreetMap contributors'
            }).addTo(map);

            // Add marker to the map when the user clicks
            map.on('click', function (e) {
                var latlng = e.latlng;
                L.marker(latlng).addTo(map);
                // Save latlng to hidden fields or use AJAX to send it to the server
                document.getElementById('<%= LatitudeHiddenField.ClientID %>').value = latlng.lat;
                document.getElementById('<%= LongitudeHiddenField.ClientID %>').value = latlng.lng;
                document.getElementById('<%= LatitudeTextBox.ClientID %>').value = latlng.lat;
                document.getElementById('<%= LongitudeTextBox.ClientID %>').value = latlng.lng;

            });
        </script>


   <%-- <script>
        var map2;
        var marker;

        function initMap() {
            map2 = new google.maps.Map(document.getElementById('map2'), {
                center: { lat: 10.5264, lng: 7.4388 }, // Default to Kaduna, Nigeria
                zoom: 13
            });

            // Add click event listener to update textboxes and add marker
            map2.addListener('click', function (e) {
                updateCoordinates(e.latLng.lat(), e.latLng.lng());
            });
        }

        function updateCoordinates(latitude, longitude) {
            // Update textboxes with latitude and longitude
            document.getElementById('<%= LatitudeTextBox.ClientID %>').value = latitude;
                document.getElementById('<%= LongitudeTextBox.ClientID %>').value = longitude;

            // Remove existing marker (if any)
            if (marker) {
                marker.setMap(null);
            }

            // Add a new marker to the map
            marker = new google.maps.Marker({
                position: { lat: latitude, lng: longitude },
                map2: map2
            });
        }

        // Initialize the map
        google.maps.event.addDomListener(window, 'load', initMap);
    </script>--%>

  
     <div class="row">
     <asp:Label runat="server" AssociatedControlID="LatitudeTextBox" CssClass="col-md-4 col-form-label">Latitude</asp:Label>
     <div class="col-md-8">
        
         <asp:TextBox ID="LatitudeTextBox" runat="server" ReadOnly="true" CssClass="form-control" />
         <br />
     </div>
 </div>

     <div class="row">
     <asp:Label runat="server" AssociatedControlID="LongitudeTextBox" CssClass="col-md-4 col-form-label">Longitude</asp:Label>
     <div class="col-md-8">
         
        <asp:TextBox ID="LongitudeTextBox" runat="server" ReadOnly="true" CssClass="form-control" />
         <br />
     </div>
 </div>

      <asp:HiddenField ID="LatitudeHiddenField" runat="server" />
  <asp:HiddenField ID="LongitudeHiddenField" runat="server" />

     <asp:Button ID="SaveLocationButton" runat="server" Text="Save Location" CssClass="btn btn-warning" OnClick="SaveLocationButton_Click" />
    <asp:Label ID="lblMessage" runat="server" ></asp:Label>
</asp:Content>
