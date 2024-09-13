<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acceptance.aspx.cs" Inherits="BrightHRSystem.Acceptance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bright Payroll System</title>
    <script type="text/javascript" src="js/common.js"></script>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="overflow: hidden; width:1200px;">
            <div">
                <img src="images/header.png" />
            </div>
            <div style="margin-top:20px; margin-left:20px;">
                <asp:Label ID="LabelMessage" runat="server" Text="You accepted application already."></asp:Label>
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Close Browser" Height="41px" Width="176px" OnClientClick="close_win();"/>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
