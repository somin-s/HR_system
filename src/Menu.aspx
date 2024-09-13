<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="BrightHRSystem.Menu" %>
<%@ Register Assembly="obout_EasyMenu_Pro" Namespace="OboutInc.EasyMenu_Pro" TagPrefix="oem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bright Payroll System</title>
    <script type="text/javascript" src="js/common.js"></script>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID ="manager" runat ="server"></asp:ScriptManager>
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
        </div>
        <div align='center' style="margin-top:100px; width:1200px;">
            <div ID="WR_Area" runat="server" class="picDiv">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/WorkingRecord.jpg" PostBackUrl="~/WorkingRecord.aspx"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Working Record"></asp:Label>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WorkingRecord.aspx" Text="MONTHLY WORKING RECORD" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/WorkingRecord_Year.aspx" Text="YEARLY WORKING RECORD"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/WorkingRecord_Acceptance.aspx" Text="APPLICATION LIST"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/WorkingRecord_Import.aspx" Text="IMPORT WORKING DATA"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
            <div ID="Org_Area" runat="server" class="picDiv">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Org.png" PostBackUrl="~/Org.aspx"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Organization Chart"></asp:Label>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Org.aspx" Text="ORGANIZATION CHART"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
            <div ID="Emp_Area" runat="server" class="picDiv">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/staff.png" PostBackUrl="~/Employee.aspx"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Employee Data"></asp:Label>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Employee.aspx" Text="EMPLOYEE DATA"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
            <div ID="Salary_Area" runat="server" class="picDiv">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/Salary.png" PostBackUrl="~/Salary.aspx"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Salary Management"></asp:Label>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="Salary.aspx" Text="MONTHLY SALARY"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="Salary2.aspx" Text="YEARLY SALARY"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
            <div ID="Set_Area" runat="server" class="picDiv">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/Setting.png" PostBackUrl="~/Setting_Department.aspx"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Setting"></asp:Label>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Setting_Department.aspx" Text="DEPARTMENT"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Setting_Division.aspx" Text="DIVISION"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/Setting_Section.aspx" Text="SECTION"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Setting_Employee_type.aspx" Text="EMPLOYEE TYPE"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/Setting_Job.aspx" Text="JOB"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/Setting_License.aspx" Text="LICENSE"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/Setting_Position.aspx" Text="POSITION"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/Setting_Calendar.aspx" Text="CALENDAR"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="MenuDetail">
                            <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/Setting_System.aspx" Text="SYSTEM"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
