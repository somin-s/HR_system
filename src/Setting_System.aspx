<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting_System.aspx.cs" Inherits="BrightHRSystem.Setting_System" %>
<%@ Register Assembly="obout_EasyMenu_Pro" Namespace="OboutInc.EasyMenu_Pro" TagPrefix="oem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bright Payroll System</title>
    <script type="text/javascript" src="js/common.js"></script>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="overflow: hidden; width:1200px;">
            <div>
                <img src="images/header.png" />
            </div>
            <div style="float: left;">
                <oem:EasyMenu Position="Horizontal" StyleFolder="~/css/EasyMenuStyles/Horizontal1" id="EasyMenuMain" runat="server" ShowEvent="Always" DataSourceID="SqlDataSourceMenu" DataIdField="ID" DataTextField="InnerHtml" DataUrlField="Url" DataParentIdField="ParentId" SubMenuPlaceHolderID="EasyMenuItemsContainer">
                <CSSClassesCollection>
			        <oem:CSSClasses ObjectType="OboutInc.EasyMenu_Pro.MenuItem" ComponentSubMenuCellOver="ParentItemSubMenuCellOver"
				        ComponentContentCell="ParentItemContentCell" Component="ParentItem" ComponentSubMenuCell="ParentItemSubMenuCell"
				        ComponentIconCellOver="ParentItemIconCellOver" ComponentIconCell="ParentItemIconCell"
				        ComponentOver="ParentItemOver" ComponentContentCellOver="ParentItemContentCellOver"></oem:CSSClasses>
			        <oem:CSSClasses ObjectType="OboutInc.EasyMenu_Pro.MenuSeparator" ComponentSubMenuCellOver="ParentSeparatorSubMenuCellOver"
				        ComponentContentCell="ParentSeparatorContentCell" Component="ParentSeparator"
				        ComponentSubMenuCell="ParentSeparatorSubMenuCell" ComponentIconCellOver="ParentSeparatorIconCellOver"
				        ComponentIconCell="ParentSeparatorIconCell" ComponentOver="ParentSeparatorOver"
				        ComponentContentCellOver="ParentSeparatorContentCellOver"></oem:CSSClasses>
		        </CSSClassesCollection>
                <CommonSubMenuProperties Align="Under" Width="165" OffsetHorizontal="2" ShowEvent = "MouseOver" StyleFolder="styles/horizontal1" RepeatColumns="1" Position="Vertical"  />
                </oem:EasyMenu>
                <asp:SqlDataSource ID="SqlDataSourceMenu" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT * FROM [TB_R_MENU] WHERE ([UserCD] = @UserCD)">
                    <SelectParameters>
                        <asp:SessionParameter Name="UserCD" SessionField="user_id" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:PlaceHolder ID="EasyMenuItemsContainer" runat="server"></asp:PlaceHolder>
            </div>
            <div style="float: right; font-size:small;">
                <asp:Label ID="HCus" runat="server" Text=""></asp:Label>
                <asp:Label ID="HSpace" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label><br />
                <asp:Label ID="HUserID" runat="server" Text=""></asp:Label>
                <asp:Label ID="HSpace2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                <asp:Label ID="HUserName" runat="server" Text=""></asp:Label>
            </div>
            <asp:Label ID="Label2" runat="server" Text="System Setting" BackColor="#CC9900" 
                Width="1180px" style="font-size:large;"></asp:Label>
        </div>
        <div style="font-size:small; margin-top:20px; margin-left:20px;">
            <asp:Button ID="ButtonSave" runat="server" Text="Save System Setting" OnClick="ButtonSave_Click"
                OnClientClick="return confirm('Do you want to update the data?')"/>
            <br /><br />
            <asp:Label ID="Label9" runat="server" Text="Common Setting"></asp:Label>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label3" runat="server" Text="Company Name"></asp:Label>
                    </td>
                    <td class="input01">
                        <asp:TextBox ID="TextCompName" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label1" runat="server" Text="Company Name Thai"></asp:Label>
                    </td>
                    <td class="input01">
                        <asp:TextBox ID="TextCompNameThai" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label4" runat="server" Text="Company Address"></asp:Label>
                    </td>
                    <td class="input01">
                        <asp:TextBox ID="TextCompAddress" runat="server" Width="600px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label6" runat="server" Text="One Time Password"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonOneTime" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonOneTime_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Use</asp:ListItem>
                            <asp:ListItem>Not Use</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label7" runat="server" Text="Password Expire Day"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPassExpire" runat="server" Width="40px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label14" runat="server" Text="Default Password"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextDefaultPass" runat="server" Width="120px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="Label10" runat="server" Text="Salary Management"></asp:Label>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label5" runat="server" Text="Company Year Start Month"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownCompYear" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label20" runat="server" Text="Target Working Data"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonTargetWR" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">This Month</asp:ListItem>
                            <asp:ListItem>Last Month</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width:400px;">
                        <asp:Label ID="Label21" runat="server" Text="#Target month of working data when generate salary data." ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label22" runat="server" Text="Target OT Data"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonTargetOT" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">This Month</asp:ListItem>
                            <asp:ListItem>Last Month</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width:400px;">
                        <asp:Label ID="Label23" runat="server" Text="#Target month of OT data when generate salary data." ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="ButtonAddPHSH" runat="server" Text="Add Pay Holiday" OnClick="ButtonAddPHSH_Click"
                OnClientClick="return confirm('Do you want to Add Pay Holiday Data for each users?')"/>
            <br /><br /><br />
            <asp:Label ID="Label15" runat="server" Text="Working Record Management"></asp:Label>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label16" runat="server" Text="Allow to edit other Working Data"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonAllowEdit" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Allow</asp:ListItem>
                            <asp:ListItem>Not Allow</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width:400px;">
                        <asp:Label ID="Label18" runat="server" Text="#But this setting is for someone who has authority to edit all of employee data." ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label17" runat="server" Text="Allow to edit own data by unauthorized user"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonAllowEdit2" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Allow</asp:ListItem>
                            <asp:ListItem>Not Allow</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width:400px;">
                        <asp:Label ID="Label19" runat="server" Text="#Cannot edit own Working Record even if that user has authority to edit own employee data." ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label8" runat="server" Text="Working Record Device Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextImportData" runat="server" Width="120px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label11" runat="server" Text="Start Time of the business day"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownStart" runat="server" 
                            DataSourceID="SqlDataSource1" DataTextField="TIME_DATA" DataValueField="TIME_DATA" >
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label12" runat="server" Text="Finish Time of the business day"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownFinish" runat="server" 
                            DataSourceID="SqlDataSource1" DataTextField="TIME_DATA" DataValueField="TIME_DATA" >
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label24" runat="server" Text="Calc target of Late Time"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonLateInput" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Input Time</asp:ListItem>
                            <asp:ListItem>Device Time</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width:400px;">
                        <asp:Label ID="Label25" runat="server" Text="#Target to calculation of total Late time in YEARLY SALARY screen." ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label26" runat="server" Text="Calc target of Early Time"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonEarlyInput" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Input Time</asp:ListItem>
                            <asp:ListItem>Device Time</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width:400px;">
                        <asp:Label ID="Label27" runat="server" Text="#Target to calculation of total Early time in YEARLY SALARY screen." ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                <tr>
                    <td class="title01" style="width:140px;">
                        <asp:Label ID="Label13" runat="server" Text="Basic Working Place"></asp:Label>
                    </td>
                    <td class="input01">
                        <asp:TextBox ID="TextWorkPlace" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [TIME_DATA] FROM [TB_M_TIME] WHERE (([TIME_TYPE1] = '1') OR ([TIME_TYPE3] = @TIME_TYPE3))">
                <SelectParameters>
                    <asp:SessionParameter Name="TIME_TYPE3" SessionField="separate_time" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
