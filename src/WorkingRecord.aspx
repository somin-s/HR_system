<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkingRecord.aspx.cs" Inherits="BrightHRSystem.WorkingRecord" %>
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
        </div>
        <div style="margin-left:20px;">
            <table class="backcolor">
                <tr>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="←" Height="30px" Width="30px" 
                            onclick="Button2_Click" />
                    </td>
                    <td style="text-align:center; font-size:medium">
                        <asp:LinkButton ID="LinkButton1" runat="server" Height="42px" Width="60px" 
                            onclick="LinkButton1_Click" BackColor="#FF99FF" BorderColor="Black" 
                            BorderStyle="Double" >This<br/>Month</asp:LinkButton>
                    </td>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="→" Height="30px" Width="30px" 
                            onclick="Button3_Click" />
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="" Width="20px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelYear" runat="server" Text="" Font-Size="XX-Large"></asp:Label>
                    </td>
                    <td valign="bottom">
                        <asp:Label ID="Label4" runat="server" Text="year"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelMonth" runat="server" Text="" Font-Size="XX-Large"></asp:Label>
                    </td>
                    <td valign="bottom">
                        <asp:Label ID="Label6" runat="server" Text="month"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="" Width="40px"></asp:Label>
                    </td>
                    <td>
                        <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse;">
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
                                    <asp:Label ID="Label8" runat="server" Text="Name" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListPIC" runat="server"　Width="300px" DataSourceID="SqlDataSource4" DataTextField="NAME" DataValueField="NAME" AppendDataBoundItems="True" OnSelectedIndexChanged="DropDownListPIC_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME] FROM [TB_R_USER]"></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #CCCCCC">
                                    <asp:Label ID="Label18" runat="server" Text="Place" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxPlace" runat="server" Width="240px" MaxLength="50"></asp:TextBox>
                                    <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" />
                                </td>
                            </tr>
                        </table>                
                    </td>
                    <td>
                        <asp:Button ID="ButtonGenerate" runat="server" Text="Generate Report" Height="40px" 
                            Width="120px" onclick="ButtonGenerate_Click" style="margin-left: 20px;"/>
                    </td>
                    <td>
                        <asp:Button ID="PrintButton" runat="server" Text="Print" Height="40px" 
                            Width="120px" onclick="PrintButton_Click" style="margin-left: 10px;"/>
                    </td>
                    <td>
                        <asp:Button ID="PrintButton2" runat="server" Text="Montyly Report" Height="40px" 
                            Width="120px" style="margin-left: 10px;" OnClick="PrintButton2_Click"/>
                    </td>
                </tr>
            </table>

            <div ID="WorkingData" runat="server" style="margin-top: 5px; font-size:small;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="Double" BorderWidth="5px" CellPadding="4" DataKeyNames="seqID" DataSourceID="SqlDataSource1" EnableModelValidation="True" OnRowDataBound="GridView1_RowDataBound" OnRowUpdated="GridView1_RowUpdated" ForeColor="Black" GridLines="Vertical" OnRowCommand="GridView1_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle HorizontalAlign="Center"/>
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="DAY" HeaderText="day" SortExpression="DAY" ReadOnly="True"/>
                        <asp:BoundField DataField="WEEK" HeaderText="" SortExpression="WEEK" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Start" SortExpression="APPLY_STARTING" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList4" runat="server" 
                                    DataSourceID="SqlDataSource2" DataTextField="TIME_DATA" DataValueField="TIME_DATA" 
                                    SelectedValue='<%# Bind("APPLY_STARTING") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("APPLY_STARTING") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Finish" SortExpression="APPLY_LEAVING" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList5" runat="server" 
                                    DataSourceID="SqlDataSource2" DataTextField="TIME_DATA" DataValueField="TIME_DATA" 
                                    SelectedValue='<%# Bind("APPLY_LEAVING") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("APPLY_LEAVING") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DATA_STARTING" HeaderText="Data Start" SortExpression="DATA_STARTING" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px" />
                        <asp:BoundField DataField="DATA_LEAVING" HeaderText="Data Finish" SortExpression="DATA_LEAVING" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px" />
                        <asp:TemplateField HeaderText="ATT1" SortExpression="ATT1">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("ATT1") %>'>
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="PAID">PAID</asp:ListItem>
                                    <asp:ListItem Value="HFPD">HFPD</asp:ListItem>
                                    <asp:ListItem Value="ABSN">ABSN</asp:ListItem>
                                    <asp:ListItem Value="HFAB">HFAB</asp:ListItem>
                                    <asp:ListItem Value="LATE">LATE</asp:ListItem>
                                    <asp:ListItem Value="ERLY">ERLY</asp:ListItem>
                                    <asp:ListItem Value="HLWK">HLWK</asp:ListItem>
                                    <asp:ListItem Value="SPHL">SPHL</asp:ListItem>
                                    <asp:ListItem Value="COMP">COMP</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ATT1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ATT2" SortExpression="ATT2">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList3" runat="server" SelectedValue='<%# Bind("ATT2") %>'>
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="LATE">LATE</asp:ListItem>
                                    <asp:ListItem Value="ERLY">ERLY</asp:ListItem>
                                    <asp:ListItem Value="HFPD">HFPD</asp:ListItem>
                                    <asp:ListItem Value="HFAB">HFAB</asp:ListItem>
                                    <asp:ListItem Value="SHFT">SHFT</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ATT2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WORKING_PLACE" HeaderText="Another Place" SortExpression="WORKING_PLACE" />
                        <asp:TemplateField HeaderText="Rest" SortExpression="REST1" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("REST1") %>'>
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="1:00">1:00</asp:ListItem>
                                    <asp:ListItem Value="0:00">0:00</asp:ListItem>
                                    <asp:ListItem Value="0:30">0:30</asp:ListItem>
                                    <asp:ListItem Value="1:30">1:30</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("REST1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WORKING_H_NORMAL" HeaderText="Total" SortExpression="WORKING_H_NORMAL" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px" />
                        <asp:TemplateField HeaderText="OT Start" SortExpression="OVERTIME_STARTING" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList6" runat="server" 
                                    DataSourceID="SqlDataSource3" DataTextField="TIME_DATA" DataValueField="TIME_DATA" 
                                    SelectedValue='<%# Bind("OVERTIME_STARTING") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("OVERTIME_STARTING") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OT Finish" SortExpression="OVERTIME_LEAVING" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList7" runat="server" 
                                    DataSourceID="SqlDataSource3" DataTextField="TIME_DATA" DataValueField="TIME_DATA" 
                                    SelectedValue='<%# Bind("OVERTIME_LEAVING") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("OVERTIME_LEAVING") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WORKING_H_OT" HeaderText="Normal OT" SortExpression="WORKING_H_OT" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px" />
                        <asp:BoundField DataField="WEEKEND_H_NORMAL" HeaderText="Holiday" SortExpression="WEEKEND_H_NORMAL" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px" />
                        <asp:BoundField DataField="WEEKEND_H_OT" HeaderText="Holiday OT" SortExpression="WEEKEND_H_OT" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px" />
                        <asp:BoundField DataField="TOTAL" HeaderText="Total" SortExpression="TOTAL" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40px" />
                        <asp:TemplateField HeaderText="Detail" SortExpression="DETAIL">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DETAIL") %>' Width="300" TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DETAIL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" ReadOnly="True" HeaderStyle-Width="50" />
                        <asp:ButtonField ButtonType="Button" Text="Accept" CommandName="Accept" HeaderStyle-Width="50" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" DeleteCommand="DELETE FROM [TB_R_WORKINGREPORT_D] WHERE [seqID] = @seqID" InsertCommand="INSERT INTO [TB_R_WORKINGREPORT_D] ([DAY], [WEEK], [ATT1], [ATT2], [WORKING_PLACE], [APPLY_STARTING], [APPLY_LEAVING], [DATA_STARTING], [DATA_LEAVING], [OVERTIME_STARTING], [OVERTIME_LEAVING], [WORKING_H_NORMAL], [WORKING_H_OT], [WEEKEND_H_NORMAL], [WEEKEND_H_OT], [REST1], [TOTAL], [DETAIL], [STATUS]) VALUES (@DAY, @WEEK, @ATT1, @ATT2, @WORKING_PLACE, @APPLY_STARTING, @APPLY_LEAVING, @DATA_STARTING, @DATA_LEAVING, @OVERTIME_STARTING, @OVERTIME_LEAVING, @WORKING_H_NORMAL, @WORKING_H_OT, @WEEKEND_H_NORMAL, @WEEKEND_H_OT, @REST1, @TOTAL, @DETAIL, @STATUS)" SelectCommand="SELECT [seqID], [DAY], [WEEK], [ATT1], [ATT2], [WORKING_PLACE], [APPLY_STARTING], [APPLY_LEAVING],[DATA_STARTING], [DATA_LEAVING], [OVERTIME_STARTING], [OVERTIME_LEAVING], [WORKING_H_NORMAL], [WORKING_H_OT], [WEEKEND_H_NORMAL], [WEEKEND_H_OT], [REST1], [TOTAL], [DETAIL], [STATUS] FROM [TB_R_WORKINGREPORT_D] WHERE ([HEADER_ID] = @HEADER_ID)" UpdateCommand="UPDATE [TB_R_WORKINGREPORT_D] SET [ATT1] = @ATT1, [ATT2] = @ATT2, [WORKING_PLACE] = @WORKING_PLACE, [APPLY_STARTING] = @APPLY_STARTING, [APPLY_LEAVING] = @APPLY_LEAVING, [OVERTIME_STARTING] = @OVERTIME_STARTING, [OVERTIME_LEAVING] = @OVERTIME_LEAVING, [REST1] = @REST1, [TOTAL] = @TOTAL, [DETAIL] = @DETAIL, [STATUS] = @STATUS WHERE [seqID] = @seqID">
                    <DeleteParameters>
                        <asp:Parameter Name="seqID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="DAY" Type="Int32" />
                        <asp:Parameter Name="WEEK" Type="String" />
                        <asp:Parameter Name="ATT1" Type="String" />
                        <asp:Parameter Name="ATT2" Type="String" />
                        <asp:Parameter Name="WORKING_PLACE" Type="String" />
                        <asp:Parameter Name="APPLY_STARTING" Type="String" />
                        <asp:Parameter Name="APPLY_LEAVING" Type="String" />
                        <asp:Parameter Name="DATA_STARTING" Type="String" />
                        <asp:Parameter Name="DATA_LEAVING" Type="String" />
                        <asp:Parameter Name="OVERTIME_STARTING" Type="String" />
                        <asp:Parameter Name="OVERTIME_LEAVING" Type="String" />
                        <asp:Parameter Name="WORKING_H_NORMAL" Type="String" />
                        <asp:Parameter Name="WORKING_H_OT" Type="String" />
                        <asp:Parameter Name="WEEKEND_H_NORMAL" Type="String" />
                        <asp:Parameter Name="WEEKEND_H_OT" Type="String" />
                        <asp:Parameter Name="REST1" Type="String" />
                        <asp:Parameter Name="TOTAL" Type="String" />
                        <asp:Parameter Name="DETAIL" Type="String" />
                        <asp:Parameter Name="STATUS" Type="String" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:SessionParameter Name="HEADER_ID" SessionField="header_id" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ATT1" Type="String" />
                        <asp:Parameter Name="ATT2" Type="String" />
                        <asp:Parameter Name="WORKING_PLACE" Type="String" />
                        <asp:Parameter Name="APPLY_STARTING" Type="String" />
                        <asp:Parameter Name="APPLY_LEAVING" Type="String" />
                        <asp:Parameter Name="OVERTIME_STARTING" Type="String" />
                        <asp:Parameter Name="OVERTIME_LEAVING" Type="String" />
                        <asp:Parameter Name="REST1" Type="String" />
                        <asp:Parameter Name="TOTAL" Type="String" />
                        <asp:Parameter Name="DETAIL" Type="String" />
                        <asp:Parameter Name="STATUS" Type="String" />
                        <asp:Parameter Name="seqID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [TIME_DATA] FROM [TB_M_TIME] WHERE (([TIME_TYPE1] = '1') OR ([TIME_TYPE3] = @TIME_TYPE3))">
                    <SelectParameters>
                        <asp:SessionParameter Name="TIME_TYPE3" SessionField="separate_time" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [TIME_DATA] FROM [TB_M_TIME] WHERE (([TIME_TYPE2] = '1') OR ([TIME_TYPE4] = @TIME_TYPE4))">
                    <SelectParameters>
                        <asp:SessionParameter Name="TIME_TYPE4" SessionField="separate_time" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:Label ID="Label12" runat="server" Text="Total Days"></asp:Label>
                <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label11" runat="server" Text="Paid Holiday"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_Paid" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="title01">
                            <asp:Label ID="Label13" runat="server" Text="Absence"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_Absence" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="title01">
                            <asp:Label ID="Label15" runat="server" Text="Holiday Work"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_Holiday" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="title01">
                            <asp:Label ID="Label17" runat="server" Text="Real Work"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_Real" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label14" runat="server" Text="Total Time"></asp:Label>
                <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label16" runat="server" Text="Normal OT"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_NormalOT" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="title01">
                            <asp:Label ID="Label19" runat="server" Text="Holiday Work"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_HolidayWork" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="title01">
                            <asp:Label ID="Label21" runat="server" Text="Holiday OT"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_HolidayOT" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="title01">
                            <asp:Label ID="Label23" runat="server" Text="Total Time"></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="Label_TotalTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <div ID="YN2_1" runat="server" visible="false">
                    <asp:Label ID="Label20" runat="server" Text="Saturday Work"></asp:Label>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label22" runat="server" Text="Expect"></asp:Label>
                            </td>
                            <td class="input02">
                                <asp:Label ID="LabelSatExp" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="title01">
                                <asp:Label ID="Label25" runat="server" Text="Actual"></asp:Label>
                            </td>
                            <td class="input02">
                                <asp:Label ID="LabelSatAct" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="title01">
                                <asp:Label ID="Label24" runat="server" Text="Difference"></asp:Label>
                            </td>
                            <td class="input02">
                                <asp:Label ID="LabelSatDeff" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="title01">
                                <asp:Label ID="Label26" runat="server" Text="OT Calc"></asp:Label>
                            </td>
                            <td class="input02">
                                <asp:Label ID="LabelOtCalc" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <table ID="NoData" runat="server" style="margin-top: 20px; height: 200px; width:1000px;" visible="false">
            <tr align="center">
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Do not have a Working Recorde yet."></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
