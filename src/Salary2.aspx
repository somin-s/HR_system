<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salary2.aspx.cs" Inherits="BrightHRSystem.Salary2" %>
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
            <asp:Label ID="Label2" runat="server" Text="Yearly Salary" BackColor="#99CCFF" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="font-size:small; float:left; width: 450px; margin-top:10px; margin-left:5px;">
                <fieldset style="border-style:dotted;">
                    <div style="float:left; width: 450px;">
                        <asp:Label ID="Label1" runat="server" Text="20XX Yearly Salary" Font-Bold="False" Font-Overline="False" Font-Size="XX-Large" Font-Underline="True"></asp:Label>
                        <br />
                        <asp:Label ID="Label3" runat="server" Text="&nbsp;&nbsp;&nbsp;Start Day:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Font-Bold="True"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="1-JAN, 20XX"></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text="Close Day:&nbsp;&nbsp;&nbsp;&nbsp;" Font-Bold="True"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text="31-DEC, 20XX"></asp:Label>
                    </div>
                </fieldset>
            </div>
            <table style="float:right;">
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Disp Data:&nbsp;&nbsp;&nbsp;&nbsp;" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Value="2013"></asp:ListItem>
                            <asp:ListItem Value="2014"></asp:ListItem>
                            <asp:ListItem Value="2015"></asp:ListItem>
                            <asp:ListItem Value="2016"></asp:ListItem>
                            <asp:ListItem Value="2017"></asp:ListItem>
                            <asp:ListItem Value="2018"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button_Print" runat="server" Text="Excel" Width="150px" Height="30px" OnClick="Button_Print_Click"/>
                    </td>
                    <td>
                        <asp:Button ID="Button_Print2" runat="server" Text="Personal Data" Width="150px" 
                            Height="30px" OnClientClick="return unDeveloped()"
                            OnClick="Button_Print2_Click" Visible="False"/>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <div ID="Select_Area" runat="server">
                <asp:Button ID="Button_Comp" runat="server" Text="Company Year" Width="180px" Height="30px" OnClick="Button_Comp_Click"/>
                <asp:Button ID="Button_Normal" runat="server" Text="Normal Year" Width="180px" Height="30px" OnClick="Button_Normal_Click"/>
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="Double" BorderWidth="5px" CellPadding="4" EnableModelValidation="True" ForeColor="Black" AutoGenerateSelectButton="True" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID" />
                    <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name" />
                    <asp:BoundField HeaderText="DATA0" DataField="DATA0" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA1" DataField="DATA1" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA2" DataField="DATA2" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA3" DataField="DATA3" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA4" DataField="DATA4" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA5" DataField="DATA5" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA6" DataField="DATA6" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA7" DataField="DATA7" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA8" DataField="DATA8" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA9" DataField="DATA9" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA10" DataField="DATA10" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DATA11" DataField="DATA11" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="BONUS" DataField="BONUS" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
<HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="TOTAL" DataField="TOTAL" SortExpression="Condition" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Right">
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
        </div>
    </form>
</body>
</html>
