<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting_Calendar.aspx.cs" Inherits="BrightHRSystem.Setting_Calendar" %>
<%@ Register Assembly="obout_EasyMenu_Pro" Namespace="OboutInc.EasyMenu_Pro" TagPrefix="oem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bright Payroll System</title>
    <script type="text/javascript" src="js/common.js"></script>
    <script type="text/javascript">
        function InputCheck() {
            if (document.getElementById("days").value == "") {
                alert("Please choose some date to operate.");
            }
        }
    </script>
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
            <asp:Label ID="Label2" runat="server" Text="Calendar Setting" BackColor="#CC9900" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <table id="table1" runat="server" border="1" cellspacing="0" cellpadding="3"
                style="border-collapse: collapse; margin-left: 20px; margin-top: 20px;">
                <tr>
                    <td style="background-color: #CCCCCC">
                        <asp:Label ID="Label1" runat="server" Text="Year" Width="54px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged">
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                            <asp:ListItem>2017</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #CCCCCC">
                        <asp:Label ID="Label18" runat="server" Text="Month" Width="54px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="months" runat="server" ReadOnly="True" Width="35px"></asp:TextBox>
                    </td>
                    <td style="background-color: #CCCCCC">
                        <asp:Label ID="Label17" runat="server" Text="Day" Width="45px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="days" runat="server" Width="44px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="margin-left:20px; margin-top:3px;">
                <asp:Button ID="Button1" runat="server" Text="Add New Holiday" Width="140px" Style="margin-bottom: 10px;"
                    OnClientClick="return InputCheck();" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="Delete Holiday" Width="140px" Style="margin-bottom: 10px;"
                    OnClientClick="" OnClick="Button2_Click" />
            </div>
            <table id="tableSchedule" runat="server" border="1" cellspacing="0"
                cellpadding="3" style="border-collapse: collapse; margin-left: 20px; margin-top: 10px;">
                <tr>
                    <td>
                        <asp:Calendar ID="Calendar1" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar2" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar2_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar3" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar3_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar4" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar4_SelectionChanged"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Calendar ID="Calendar5" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar5_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar6" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar6_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar7" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar7_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar8" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar8_SelectionChanged"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Calendar ID="Calendar9" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar9_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar10" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar10_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar11" runat="server" Width="200px" NextMonthText="" PrevMonthText=""
                            OnSelectionChanged="Calendar11_SelectionChanged"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar12" runat="server" Width="200px" NextMonthText="" OnSelectionChanged="Calendar12_SelectionChanged"
                            PrevMonthText=""></asp:Calendar>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
