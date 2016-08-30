<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrackUser.aspx.cs" Inherits="ValtechBaseLine.Web.TrackUser" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <sc:visitoridentification runat="server" />
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <table>
                 <tr>
                    <td colspan="2">Note Session will expire in three min. It will generate new contactId if you cant save
                    </td>

                </tr>
                <tr>
                    <td></td>
                    <td>
                       
                        <asp:Button runat="server" ID="btnGetSummary" Text="Get Summary" OnClick="btnGetSummary_Click" />&nbsp;
                         <asp:Button runat="server" ID="btnSession" Text="Save & Session Abondon" OnClick="btnSession_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Literal runat="server" ID="ltpath"></asp:Literal>
                    </td>
                </tr>

                <tr>
                    <td colspan="2"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
