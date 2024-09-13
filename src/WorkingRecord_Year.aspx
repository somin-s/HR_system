<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkingRecord_Year.aspx.cs" Inherits="BrightHRSystem.WorkingRecord_Year" %>
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
            <asp:Label ID="Label2" runat="server" Text="Year Working Data" BackColor="#CC99FF" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="margin-top:3px; margin-left:5px;">
                <asp:Button ID="Button_Before" runat="server" Text="←" Height="20px" Width="40px" OnClick="Button_Before_Click"/>
                <asp:Label ID="Label9" runat="server" Text="&nbsp;&nbsp;"></asp:Label>
                <asp:Button ID="Button_After" runat="server" Text="→" Height="20px" Width="40px" OnClick="Button_After_Click"/>
            </div>
            <div style="font-size:small; float:left; width: 700px; margin-top:10px; margin-left:5px;">
                <asp:DropDownList ID="DDYear" Font-Size="XX-Large" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged">
                    <asp:ListItem>2014</asp:ListItem>
                    <asp:ListItem Selected="True">2015</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label3" runat="server" Text="&nbsp;&nbsp;Working Data" Font-Bold="False" Font-Overline="False" Font-Size="XX-Large" Font-Underline="True"></asp:Label>
                <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse; margin-top:5px;">
                    <tr>
                        <td style="background-color: #CCCCCC">
                            <asp:Label ID="Label7" runat="server" Text="No." Width="50px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="UserCode" runat="server" Text="" Width="300px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC">
                            <asp:Label ID="Label1" runat="server" Text="Name" Width="50px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListPIC" runat="server"　Width="300px" DataSourceID="SqlDataSource4" DataTextField="NAME" DataValueField="NAME" AppendDataBoundItems="True" OnSelectedIndexChanged="DropDownListPIC_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME] FROM [TB_R_USER]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="float:right;">
                <tr>
                    <td>
                        <asp:Button ID="Button_Print" runat="server" Text="Excel" Width="100px" 
                            Height="30px" OnClientClick="return confirm('Do you want to download the Yearly Working Data?')"
                            OnClick="Button_Print_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 1309px">
            <div style="margin-left:20px; margin-top:10px; font-size:small; float:left;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="Double" BorderWidth="5px" CellPadding="4" EnableModelValidation="True" ForeColor="Black" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="Year" DataField="Year" HeaderStyle-Width="40" />
                        <asp:BoundField HeaderText="Month" DataField="Month" HeaderStyle-Width="40" />
                        <asp:BoundField HeaderText="Official Days" DataField="Official Days" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Actual Days" DataField="Actual Days" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Official Time" DataField="Official Time" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Actual Time" DataField="Actual Time" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Normal OT" DataField="Normal OT" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Holiday Work" DataField="Holiday Work" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Holiday OT" DataField="Holiday OT" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="PAID" DataField="PAID" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="ABSN" DataField="ABSN" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="LATE" DataField="LATE" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="LATE TIME" DataField="LATE_TIME" HeaderStyle-Width="60" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="ERLY" DataField="ERLY" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="ERLY TIME" DataField="ERLY_TIME" HeaderStyle-Width="60" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="HLWK" DataField="HLWK" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="SPHL" DataField="SPHL" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="COMP" DataField="COMP" HeaderStyle-Width="40" ItemStyle-HorizontalAlign="Right"/>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" Height="20" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <div style="margin-top:10px; float:left;">
                    <asp:Label ID="Label12" runat="server" Text="Paid Holiday"></asp:Label>
                    <div style="text-align:center;">
                        <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                            <tr>
                                <td style="background-color: #CCCCCC">
                                    <asp:Label ID="Label11" runat="server" Text="Year Amount" Width="50px"></asp:Label>
                                </td>
                                <td style="background-color: #CCCCCC">
                                    <asp:Label ID="Label4" runat="server" Text="Used" Width="50px"></asp:Label>
                                </td>
                                <td style="background-color: #CCCCCC">
                                    <asp:Label ID="Label10" runat="server" Text="Remain" Width="50px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label_PaidYear" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label_PaidUsed" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label_PaidRemain" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="margin-top:10px; margin-left:20px; float:left;">
                    <asp:Label ID="Label5" runat="server" Text="Special Holiday"></asp:Label>
                    <div style="text-align:center;">
                        <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                            <tr>
                                <td style="background-color: #CCCCCC">
                                    <asp:Label ID="Label6" runat="server" Text="Year Amount" Width="50px"></asp:Label>
                                </td>
                                <td style="background-color: #CCCCCC">
                                    <asp:Label ID="Label8" runat="server" Text="Used" Width="50px"></asp:Label>
                                </td>
                                <td style="background-color: #CCCCCC">
                                    <asp:Label ID="Label13" runat="server" Text="Remain" Width="50px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label_SpecialYear" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label_SpecialUsed" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label_SpecialRemain" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
