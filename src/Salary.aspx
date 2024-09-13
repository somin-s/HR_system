<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salary.aspx.cs" Inherits="BrightHRSystem.Salary" %>
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
            <asp:Label ID="Label2" runat="server" Text="Salary Management" BackColor="#99CCFF" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="font-size:small; float:left; width: 600px; margin-top:10px; margin-left:5px;">
                <fieldset style="border-style:dotted;">
                    <div style="float:left;">
                        <asp:Label ID="Label1" runat="server" Text="XXX-20XX Salary Data" Font-Bold="False" Font-Overline="False" Font-Size="XX-Large" Font-Underline="True"></asp:Label>
                        <br />
                        <asp:Label ID="Label3" runat="server" Text="&nbsp;&nbsp;&nbsp;Pay Day:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Font-Bold="True"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="XX-XXX, XXXX"></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text="Term:&nbsp;&nbsp;&nbsp;&nbsp;" Font-Bold="True"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text="XX-XXX, XXXX - XX-XXX, XXXX"></asp:Label>
                    </div>
                    <div style="float:right; margin-top:10px; margin-right:5px;">
                        <asp:Button ID="Button_Next" runat="server" Text="Next Saraly Start"
                            OnClientClick="return confirm('Do you want to start the next month Salary Data?\nCan not modify this month Salary Data after click Ok.')" OnClick="Button_Next_Click"/>
                    </div>
                </fieldset>
            </div>
            <table style="float:right;">
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Disp Data:&nbsp;&nbsp;&nbsp;&nbsp;" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataValueField="SEQ_ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button_Paid" runat="server" Text="Paid Fix" Width="110px" 
                            Height="30px" OnClientClick="return confirm('Do you want to set condition to Paid for all of this month data?')"
                            OnClick="Button_Paid_Click"/>
                        <asp:Button ID="Button_Refresh" runat="server" Text="Refresh" Width="110px" 
                            Height="30px" OnClientClick="return confirm('Do you want to get latest user list again?\n(System will not delete current data)')" OnClick="Button_Refresh_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <asp:DropDownList ID="DropDownListExcel" runat="server">
                <asp:ListItem Value="0">Salary List</asp:ListItem>
                <asp:ListItem Value="1">Pay Off Table</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button_Print" runat="server" Text="Excel" Width="110px" Height="30px" OnClick="Button_Print_Click"/>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" EnableModelValidation="True" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField HeaderText="ID" SortExpression="USER_ID" HeaderStyle-Width="35" DataField="USER_ID">
                        <HeaderStyle Width="35px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Name" SortExpression="NAME" HeaderStyle-Width="80" DataField="NAME" >
                        <HeaderStyle Width="240px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CONDITION" HeaderText="Condition" SortExpression="CONDITION">
                        <HeaderStyle Width="100px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="INCOME_TOTAL" HeaderText="Income Total" SortExpression="INCOME_TOTAL" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0066CC">
                        <HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SOCIAL_SECURITY" HeaderText="Social Security" SortExpression="SOCIAL_SECURITY" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0066CC">
                        <HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="INCOME_TAX" HeaderText="Income Tax" SortExpression="INCOME_TAX" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0066CC">
                        <HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="REDUCTION" HeaderText="Reduction" SortExpression="REDUCTION" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0066CC">
                        <HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SALARY_PAYMENT" HeaderText="Payment" SortExpression="SALARY_PAYMENT" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#0033CC" HeaderStyle-ForeColor="#FF0066">
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle Font-Bold="True"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="INPUT_F" HeaderText="Input Flag" SortExpression="INPUT_F" HeaderStyle-BackColor="#669999">
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRINT_F" HeaderText="Print Flag" SortExpression="PRINT_F" HeaderStyle-BackColor="#669999">
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PAID_F" HeaderText="Paid Flag" SortExpression="PAID_F" HeaderStyle-BackColor="#669999">
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TRANSFER" HeaderText="Transfer" SortExpression="TRANSFER" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#CC00CC">
                        <HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="HANDOVER" HeaderText="Hand Over" SortExpression="HANDOVER" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#CC00CC">
                        <HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="
SELECT H.[YEAR]
      ,H.[MONTH]
	  ,D.[USER_ID]
	  ,U.NAME
	  ,D.CONDITION
	  ,CONVERT(varchar, CONVERT(MONEY,D.[INCOME_TOTAL]), 1) AS [INCOME_TOTAL]
	  ,CONVERT(varchar, CONVERT(MONEY,D.[SOCIAL_SECURITY]), 1) AS [SOCIAL_SECURITY]
	  ,CONVERT(varchar, CONVERT(MONEY,D.[INCOME_TAX]), 1) AS [INCOME_TAX]
	  ,CONVERT(varchar, CONVERT(MONEY,D.[PROV_EMP] + D.[PROV_COMP]), 1) AS [REDUCTION]
	  ,CONVERT(varchar, CONVERT(MONEY,D.[SALARY_PAYMENT]), 1) AS [SALARY_PAYMENT]
	  ,D.INPUT_F
	  ,D.PRINT_F
	  ,D.PAID_F
	  ,CONVERT(varchar, CONVERT(MONEY,D.TRANSFER), 1) AS [TRANSFER]
	  ,CONVERT(varchar, CONVERT(MONEY,D.HANDOVER), 1) AS [HANDOVER]
  FROM [TB_R_PAYROLL_H] AS H
  LEFT OUTER JOIN [TB_R_PAYROLL_D] AS D
  ON H.[SEQ_ID] = D.HEADER_ID
  LEFT OUTER JOIN [TB_R_USER] AS U
  ON D.[USER_ID] = U.ID
  WHERE H.[YEAR] = @selected_year AND H.[MONTH] = @selected_month AND H.[BONUS] = @selected_bonus
  ORDER BY D.[USER_ID]
                ">
                <SelectParameters>
                    <asp:SessionParameter Name="selected_year" SessionField="selected_year" />
                    <asp:SessionParameter Name="selected_month" SessionField="selected_month" />
                    <asp:SessionParameter Name="selected_bonus" SessionField="selected_bonus" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <asp:Label ID="Label12" runat="server" Text="Total" Font-Bold="True"></asp:Label>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                <tr>
                    <td class="title01">
                        <asp:Label ID="Label9" runat="server" Text="Income Total" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="input01" style="text-align:right;">
                        <asp:Label ID="Label_Income" runat="server" Font-Size="Medium" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="title01">
                        <asp:Label ID="Label11" runat="server" Text="Pay Amount" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="input01" style="text-align:right;">
                        <asp:Label ID="Label_Paid" runat="server" Font-Size="Medium" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="title01">
                        <asp:Label ID="Label13" runat="server" Text="Transfer Amount"></asp:Label>
                    </td>
                    <td class="input01" style="text-align:right;">
                        <asp:Label ID="Label_Transfer" runat="server" Font-Size="Medium"></asp:Label>
                    </td>
                    <td class="title01">
                        <asp:Label ID="Label15" runat="server" Text="Handover Amount"></asp:Label>
                    </td>
                    <td class="input01" style="text-align:right;">
                        <asp:Label ID="Label_Handover" runat="server" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
