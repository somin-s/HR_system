<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BrightHRSystem._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bright Payroll System</title>
</head>
<body>
	<form id="form1" runat="server" style="background-image: url('images/BackImage.png');">
		<div align='center'>
			<table border="0" cellpadding="0" cellspacing="0" width="461" style='border-collapse: collapse; width: 346pt'>
				<tr>
					<td>
                        <img src="images/header.png" />
					</td>
				</tr>
				<tr>
					<td height="65" style="text-align: right">
                        <asp:Label ID="lblVersion" runat="server" Text="Ver. 1.6.1" Font-Size="XX-Small"></asp:Label>
                        <asp:Label ID="HSpace" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                        <asp:Label ID="HCus" runat="server" Text=""></asp:Label>
					</td>
				</tr>
				<tr>
					<asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" Height="160px" Width="326px" onauthenticate="Login1_Authenticate">
						<TextBoxStyle Font-Size="0.8em" />
						<LayoutTemplate>
                            <table cellpadding="4" cellspacing="0" style="border-collapse:collapse;">
                                <tr>
                                    <td>
                                        <table cellpadding="0" style="height:160px;width:326px;">
                                            <tr>
                                                <td align="center" colspan="2" style="color:White;background-color:#5D7B9D;font-size:0.9em;font-weight:bold;">Log In</td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color:Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" Text="Log In" ValidationGroup="Login1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
						<LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
						<InstructionTextStyle Font-Italic="True" ForeColor="Black" />
						<TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
					</asp:Login>
				</tr>
				<tr>
					<td height="40" style="text-align: center; margin-top:5px;">
                        <asp:Button ID="Button1" runat="server" Text="One Time Password" Width="200px" OnClick="Button1_Click" OnClientClick="return confirm('Do you want to generate a new password?')"/>
					</td>
				</tr>
				<tr>
					<td height="220px" style="text-align: center; margin-top:5px;">
						<hr style="margin-top: 10px; width: 600px" />
                        <asp:TextBox ID="txtNews" runat="server" Height="200px" TextMode="MultiLine" Width="600px" ReadOnly="True"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td height="18" style="height: 13.5pt; text-align: center">
						<hr style="color: #0000ff; margin-top: 100px; width: 1200px" />
						<asp:Label ID="lblCopyright" runat="server" Text="Copyright© 2013 BRIGHT SYSTEM JAPAN CO., LTD."></asp:Label>
					</td>
				</tr>
			</table>
		</div>
	</form>
</body>
</html>
