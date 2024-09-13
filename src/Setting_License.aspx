<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting_License.aspx.cs" Inherits="BrightHRSystem.Setting_License" %>
<%@ Register Assembly="obout_EasyMenu_Pro" Namespace="OboutInc.EasyMenu_Pro" TagPrefix="oem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
            <asp:Label ID="Label2" runat="server" Text="License Setting" BackColor="#CC9900" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="font-size:small; float:left; width: 950px;">
                <fieldset style="border-style:dotted;">
                    <legend style="background-color: #FFFFFF">Search Condition</legend>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; float:left;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label3" runat="server" Text="License CD"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxID" runat="server" Width="160px"></asp:TextBox>
                            </td>
                            <td class="title01">
                                <asp:Label ID="Label4" runat="server" Text="Detail"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxDetail" runat="server"  Width="240px"></asp:TextBox>
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
                        <asp:Button ID="Button_New" runat="server" Text="New" Width="80px" 
                            Height="30px" OnClick="Button_New_Click"/>
                        <asp:Button ID="Button_Print" runat="server" Text="Excel" Width="80px" 
                            Height="30px" OnClick="Button_Print_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div ID="DetailArea" runat="server" visible="false" style="font-size:small; width: 600px; margin-top: 10px;">
            <fieldset style="border-style:dotted;">
                <legend style="background-color: #FFFFFF">Detail</legend>
                <div>
                    <table ID="Button_Table1" runat="server" >
                        <tr>
                            <td>
                                <asp:Button ID="Button_Update" runat="server" Text="Update" Width="100" 
                                    OnClientClick="return confirm('Do you want to update the data?')" 
                                    onclick="Button_Update_Click"/>
                            </td>
                            <td>
                                <asp:Button ID="Button_Delete" runat="server" Text="Delete" Width="100" 
                                    OnClientClick="return confirm('Do you want to delete the data?')" OnClick="Button_Delete_Click" 
                                    />
                            </td>
                        </tr>
                    </table>
                    <table ID="Button_Table2" runat="server" >
                        <tr>
                            <td>
                                <asp:Button ID="Button_Create" runat="server" Text="Create" Width="100" 
                                    OnClientClick="return confirm('Do you want to create the new data?')" OnClick="Button_Create_Click" 
                                    />
                            </td>
                            <td>
                                <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" Width="100" 
                                    OnClick="Button_Cancel_Click" 
                                    />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="Label6" runat="server" Text="License Information"></asp:Label>
                <br />
                <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                    <tr>
                        <td class="title02">
                            <asp:Label ID="Label1" runat="server" Text="License CD *"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:TextBox ID="TextBoxID_D" runat="server" Width="160px" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                    <tr>
                        <td class="title02">
                            <asp:Label ID="Label8" runat="server" Text="Detail *"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:TextBox ID="TextBoxDetail_D" runat="server" Width="160px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>

            </fieldset>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CC9966" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataKeyNames="LICENSE_CD" DataSourceID="SqlDataSource1" EnableModelValidation="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="LICENSE_CD" HeaderText="License CD" ReadOnly="True" SortExpression="LICENSE_CD" />
                    <asp:BoundField DataField="DETAIL" HeaderText="Detail" SortExpression="DETAIL" />
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [LICENSE_CD], [DETAIL] FROM [TB_M_LICENSE] WHERE (([LICENSE_CD] LIKE @LICENSE_CD) AND ([DETAIL] LIKE @DETAIL))">
                <SelectParameters>
                    <asp:SessionParameter Name="LICENSE_CD" SessionField="search_id" Type="String" />
                    <asp:SessionParameter Name="DETAIL" SessionField="search_name" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
