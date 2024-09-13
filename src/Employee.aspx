<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="BrightHRSystem.Employee" %>
<%@ Register Assembly="obout_EasyMenu_Pro" Namespace="OboutInc.EasyMenu_Pro" TagPrefix="oem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bright Payroll System</title>
    <script type="text/javascript" src="js/common.js"></script>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
</head>
<body>
    <form id="form1" runat="server" style="font-size:small">
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
            <asp:Label ID="Label2" runat="server" Text="Employee Info" BackColor="#CCCCFF" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="font-size:small; float:left; width: 800px;">
                <fieldset style="border-style:dotted;">
                    <legend style="background-color: #FFFFFF">Search Condition</legend>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; float:left;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxUser_ID" runat="server" Width="160px"></asp:TextBox>
                            </td>
                            <td class="title01">
                                <asp:Label ID="Label4" runat="server" Text="User Name"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxUserName" runat="server"  Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="float: right;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="Button_Search" runat="server" Text="Search" Width="100" 
                                        onclick="Button_Search_Click"/>
                                    <asp:Button ID="Button_Clear" runat="server" Text="Clear" Width="100" 
                                        onclick="Button_Clear_Click"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
            </div>
            <table style="float:right;">
                <tr>
                    <td>
                        <asp:Button ID="Button_GenOrg" runat="server" Text="Update OrgChart" Height="30px" OnClick="Button_GenOrg_Click"
                            OnClientClick="return confirm('Do you want to Update the Organizational Chart?')"/>
                        <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
                        <asp:Button ID="Button_New" runat="server" Text="New" Width="80px" 
                            Height="30px" OnClick="Button_New_Click"/>
                        <asp:Button ID="Button_Print" runat="server" Text="Excel" Width="80px" Height="30px" OnClick="Button_Print_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" DataSourceID="SqlDataSource1" EnableModelValidation="True" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" AllowSorting="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="Employee Type" HeaderText="Employee Type" SortExpression="Employee Type" />
                    <asp:BoundField DataField="E-mail" HeaderText="E-mail" SortExpression="E-mail" />
                    <asp:BoundField DataField="Entering Day" HeaderText="Entering Day" ReadOnly="True" SortExpression="Entering Day" />
                    <asp:BoundField DataField="TEL" HeaderText="TEL" SortExpression="TEL" />
                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT U.[ID] AS [ID]
      ,U.[NAME] AS [Name]
      ,DEP.DETAIL AS [Department]
      ,POS.DETAIL AS [Position]
      ,EMP.DETAIL AS [Employee Type]
      ,[EMAIL] AS [E-mail]
      ,CONVERT(VARCHAR,[ENTERINGDAY],111) AS [Entering Day]
      ,[TEL] AS [TEL]
      ,[MOBILE] AS [Mobile]
FROM [TB_R_USER] AS U
LEFT OUTER JOIN TB_M_DEPARTMENT AS DEP
ON U.DEPARTMENT_CD = DEP.DEPARTMENT_CD
LEFT OUTER JOIN TB_M_POSITION AS POS
ON U.POSITION_CD = POS.POSITION_CD
LEFT OUTER JOIN TB_M_EMPLOYEE_TYPE AS EMP
ON U.EMPLOYEE_TYPE = EMP.EMPLOYEE_TYPE
WHERE (([ID] LIKE @ID) AND ([NAME] LIKE @NAME))">
                <SelectParameters>
                    <asp:SessionParameter Name="ID" SessionField="search_id" />
                    <asp:SessionParameter Name="NAME" SessionField="search_name" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
