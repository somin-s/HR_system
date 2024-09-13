<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkingRecord_Acceptance.aspx.cs" Inherits="BrightHRSystem.WorkingRecord_Acceptance" %>
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
            <asp:Label ID="Label2" runat="server" Text="Acceptance Data" BackColor="#CC99FF" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="margin-top:3px; margin-left:5px;">
                <asp:Button ID="Button_Before" runat="server" Text="←" Height="20px" Width="40px" OnClick="Button_Before_Click"/>
                <asp:Label ID="Label9" runat="server" Text="&nbsp;&nbsp;"></asp:Label>
                <asp:Button ID="Button_After" runat="server" Text="→" Height="20px" Width="40px" OnClick="Button_After_Click"/>
            </div>
            <div style="font-size:small; float:left; width: 700px; margin-top:10px; margin-left:5px;">
                <asp:Label ID="Label3" runat="server" Text="Acceptance Data" Font-Bold="False" Font-Overline="False" Font-Size="XX-Large" Font-Underline="True"></asp:Label>
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
        <div style="margin-left:20px; margin-top:10px;">
            <ajaxToolkit:TabContainer ID="tab" runat="server" ActiveTabIndex="0">
		        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Application List">
	                <ContentTemplate>
                        <asp:CheckBox ID="CheckBox_Approved1" runat="server" Text="Include approved application" OnCheckedChanged="CheckBox_Approved1_CheckedChanged" AutoPostBack="True"/>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="Double" BorderWidth="5px" CellPadding="4" EnableModelValidation="True" ForeColor="Black" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="Year" DataField="Year" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Month" DataField="Month" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Day" DataField="Day" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Week" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Type" DataField="Type" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Detail" DataField="Detail">
                                <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="Status"/>
                                <asp:BoundField HeaderText="Approver 1" DataField="Approver1"/>
                                <asp:BoundField HeaderText="Date" DataField="Date1" DataFormatString="{0:yyyy/MM/dd}"/>
                                <asp:BoundField HeaderText="Comment" DataField="Comment1"/>
                                <asp:BoundField HeaderText="Approver 2" DataField="Approver2"/>
                                <asp:BoundField HeaderText="Date" DataField="Date2" DataFormatString="{0:yyyy/MM/dd}"/>
                                <asp:BoundField HeaderText="Comment" DataField="Comment2"/>
                                <asp:BoundField HeaderText="Approver 3" DataField="Approver3"/>
                                <asp:BoundField HeaderText="Date" DataField="Date2" DataFormatString="{0:yyyy/MM/dd}"/>
                                <asp:BoundField HeaderText="Comment" DataField="Comment2"/>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" Height="20px" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
			        </ContentTemplate>
		        </ajaxToolkit:TabPanel>
		        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Approval List">
	                <ContentTemplate>
                        <div ID="ApproveArea" runat="server" style="margin-top:5px; margin-left:5px;" visible="False">
                            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="RBApprove" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">Approve</asp:ListItem>
                                            <asp:ListItem Value="1">Not Approve</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="title01">
                                        <asp:Label ID="Label5" runat="server" Text="Approve Date"></asp:Label>
                                    </td>
                                    <td class="input02">
                                        <asp:TextBox ID="TextBoxDate" runat="server" Width="100px"></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="Calendarextender1" runat="server" TargetControlID="TextBoxDate" Format="yyyy/MM/dd"></ajaxtoolkit:CalendarExtender>
                                    </td>
                                    <td class="title01">
                                        <asp:Label ID="Label4" runat="server" Text="Comment"></asp:Label>
                                    </td>
                                    <td class="input01">
                                        <asp:TextBox ID="TextBoxComment" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClientClick="return confirm('Do you want to update a selected application?')" OnClick="ButtonUpdate_Click"/>
                        </div>
                        <asp:CheckBox ID="CheckBox_Approved2" runat="server" Text="Include approved application" OnCheckedChanged="CheckBox_Approved2_CheckedChanged" AutoPostBack="True"/>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Double" BorderWidth="5px" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" AutoGenerateSelectButton="True" OnRowCommand="GridView2_RowCommand" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ID" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Name" DataField="Name"/>
                                <asp:BoundField HeaderText="Year" DataField="Year" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Month" DataField="Month" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Day" DataField="Day" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Type" DataField="Type" >
                                <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Detail" DataField="Detail">
                                <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="Status"/>
                                <asp:BoundField HeaderText="Approver 1" DataField="Approver1"/>
                                <asp:BoundField HeaderText="Date" DataField="Date1"/>
                                <asp:BoundField HeaderText="Comment" DataField="Comment1"/>
                                <asp:BoundField HeaderText="Approver 2" DataField="Approver2"/>
                                <asp:BoundField HeaderText="Date" DataField="Date2"/>
                                <asp:BoundField HeaderText="Comment" DataField="Comment2"/>
                                <asp:BoundField HeaderText="Approver 3" DataField="Approver3"/>
                                <asp:BoundField HeaderText="Date" DataField="Date2"/>
                                <asp:BoundField HeaderText="Comment" DataField="Comment2"/>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" Height="20px" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
			        </ContentTemplate>
		        </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
    </form>
</body>
</html>
