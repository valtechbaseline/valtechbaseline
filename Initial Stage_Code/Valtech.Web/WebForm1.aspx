<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <title></title>
    <script>
    // Try HTML5 geolocation
    jQuery(window).load(function () {        
        var lat = getCookie("lat");
        if (lat != "") {            
            document.getElementById('<%=LatitudeTextBox.ClientID%>').value = lat;
            document.getElementById('<%=LongitudeTextBox.ClientID%>').value = getCookie("long");
        }
        else {
            if (getCookie('noGeoLocation') === "true") return; // already tried to get geolocation this session and it didn't work
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {                  
                    document.getElementById('<%=LatitudeTextBox.ClientID%>').value = position.coords.latitude;
                    document.getElementById('<%=LongitudeTextBox.ClientID%>').value = position.coords.longitude;
                    setCookie("lat", position.coords.latitude, 365);
                    setCookie("long", position.coords.longitude, 365);
                }, function () {
                    handleNoGeolocation(true);
                });
            } else {
                // Browser doesn't support Geolocation
                handleNoGeolocation(false);
            }
        }
 
        function handleNoGeolocation(errorFlag) {
            setCookie('noGeoLocation', true);
            var content = '';
            if (errorFlag) {
                content = 'Please enable your location service and GPS.';
            } else {
                content = 'Error: Your browser doesn\'t support geolocation.';
            }
        }
 
 
        function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        }
 
 
        function getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1);
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        }
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:textbox cssclass="hidden" id="LatitudeTextBox" runat="server"></asp:textbox>
    <asp:textbox cssclass="hidden" id="LongitudeTextBox" runat="server"></asp:textbox>
    </div>
    </form>
</body>
</html>
