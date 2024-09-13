<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee_Edit.aspx.cs" Inherits="BrightHRSystem.Employee_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bright Payroll System</title>
    <script type="text/javascript" src="js/common.js"></script>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
</head>
<body>
    <form id="form1" runat="server" style="font-size:small">
        <asp:ScriptManager ID ="manager" runat ="server"></asp:ScriptManager>
        <div style="overflow: hidden; width:1200px; margin-bottom:20px;">
            <div>
                <img src="images/header.png" />
            </div>
            <div style="float: left;">
                <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" 
                    Font-Underline="True" style="margin-bottom: 5px; " 
                    ItemWrap="True">
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="1px"/>
                    <Items>
                        <asp:MenuItem Text="Back" Value="Back" NavigateUrl="~/Employee_Detail.aspx"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </div>
            <div style="float: right;">
                <asp:Label ID="HCus" runat="server" Text=""></asp:Label>
                <asp:Label ID="HSpace" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label><br />
                <asp:Label ID="HUserID" runat="server" Text=""></asp:Label>
                <asp:Label ID="HSpace2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                <asp:Label ID="HUserName" runat="server" Text=""></asp:Label>
            </div>
            <asp:Label ID="Label2" runat="server" Text="Employee Info" BackColor="#CCCCFF" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="margin-top:20px; margin-left:10px; float:left;">
                <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; float:left;">
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:TextBox ID="TextBoxID" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:TextBox ID="TextBoxName" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="input02">
                            <asp:DropDownList ID="DropDownListSex" runat="server">
                                <asp:ListItem Selected="True">Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label34" runat="server" Text="Thai Name"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:TextBox ID="TextBoxLocalName" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label19" runat="server" Text="Short Name"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:TextBox ID="TextBoxShortName" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label6" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:DropDownList ID="DropDownListEmpTyp" runat="server" DataSourceID="SqlDataSource1" DataTextField="DETAIL" DataValueField="DETAIL" AppendDataBoundItems="True" >
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DETAIL] FROM [TB_M_EMPLOYEE_TYPE]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label8" runat="server" Text="Termination Date"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:TextBox ID="TextBoxTermDt" runat="server"  Width="100px"></asp:TextBox>
                            <ajaxtoolkit:CalendarExtender ID="Calendarextender1" runat="server" TargetControlID="TextBoxTermDt" Format="yyyy/MM/dd">
                            </ajaxtoolkit:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:20px; margin-left:10px; float:left;">
                <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; float:left;">
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label11" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:DropDownList ID="DropDownListDep" runat="server" DataSourceID="SqlDataSource8" DataTextField="DETAIL" DataValueField="DETAIL" AppendDataBoundItems="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DETAIL] FROM [TB_M_DEPARTMENT]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label13" runat="server" Text="Division"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:DropDownList ID="DropDownListDiv" runat="server" DataSourceID="SqlDataSource9" DataTextField="DETAIL" DataValueField="DETAIL" AppendDataBoundItems="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DETAIL] FROM [TB_M_DIVISION]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label16" runat="server" Text="Section"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:DropDownList ID="DropDownListSec" runat="server" DataSourceID="SqlDataSource10" DataTextField="DETAIL" DataValueField="DETAIL" AppendDataBoundItems="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DETAIL] FROM [TB_M_SECTION]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label15" runat="server" Text="Position"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:DropDownList ID="DropDownListPos" runat="server" DataSourceID="SqlDataSource11" DataTextField="DETAIL" DataValueField="DETAIL" AppendDataBoundItems="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DETAIL] FROM [TB_M_POSITION]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label18" runat="server" Text="Job Category"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:DropDownList ID="DropDownListJobC" runat="server" DataSourceID="SqlDataSource12" DataTextField="DETAIL" DataValueField="DETAIL" AppendDataBoundItems="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DETAIL] FROM [TB_M_JOB]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:20px; margin-left:150px; float:left; width: 131px;">
                <asp:Image ID="ImagePic" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="160px" Width="120px"/>
                <asp:Label ID="Label7" runat="server" Text="Height:160 Width:120"></asp:Label>
            </div>
            <table style="float:right;">
                <tr>
                    <td>
                        <asp:Button ID="Button_Back" runat="server" Text="Back" Width="60px" 
                            Height="30px" OnClick="Button_Back_Click"/>
                        <asp:Button ID="Button_Save" runat="server" Text="Save" Width="60px" 
                            Height="30px" OnClientClick="return confirm('Do you want to Save the data?')" OnClick="Button_Save_Click"/>
                    </td>
                </tr>
            </table>
        </div>

        <div id="Password_Area" runat="server" visible="false" style="margin-bottom:20px;">
            <asp:Label ID="Label37" runat="server" Text="Please input new password if you want to change"></asp:Label>
            <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                <tr>
                    <td class="title01">
                        <asp:Label ID="Label35" runat="server" Text="New Password"></asp:Label>
                    </td>
                    <td class="input01">
                        <asp:TextBox ID="TextBoxPassword" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title01">
                        <asp:Label ID="Label38" runat="server" Text="Confirm"></asp:Label>
                    </td>
                    <td class="input01">
                        <asp:TextBox ID="TextBoxPassword2" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

        <asp:Label ID="Label36" runat="server" Text="Please select a Picture and click the Upload button"></asp:Label>
        <br />
        <INPUT type="file" id="File1" runat="server" style="width: 634px"><br />
        <asp:Button id="Button2" runat="server" Text="Upload" OnClick="Button2_Click" />
        <asp:Button id="Button1" runat="server" Text="Delete Picture" OnClick="Button1_Click"/><br /><br />
        <ajaxToolkit:TabContainer ID="tab" runat="server" AutoPostBack="true" Width="1000px" ActiveTabIndex="0" OnActiveTabChanged="tab_ActiveTabChanged">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Basic">
                <ContentTemplate>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label21" runat="server" Text="Birth Day"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxBirth" runat="server"  Width="100px" AutoPostBack="True" OnTextChanged="TextBoxBirth_TextChanged"></asp:TextBox>
                                <ajaxtoolkit:CalendarExtender ID="Calendarextender2" runat="server" TargetControlID="TextBoxBirth" Format="yyyy/MM/dd" Enabled="True">
                                </ajaxtoolkit:CalendarExtender>
                            </td>
                            <td style="width:50px;">
                                <asp:Label ID="LabelAge" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label41" runat="server" Text="(old)"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label23" runat="server" Text="Entering Day"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxEntDt" runat="server"  Width="100px" AutoPostBack="True" OnTextChanged="TextBoxEntDt_TextChanged"></asp:TextBox>
                                <ajaxtoolkit:CalendarExtender ID="Calendarextender3" runat="server" TargetControlID="TextBoxEntDt" Format="yyyy/MM/dd" Enabled="True">
                                </ajaxtoolkit:CalendarExtender>
                            </td>
                            <td style="width:50px;">
                                <asp:Label ID="LabelEntYear" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelEntYearUnit" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label25" runat="server" Text="Address 1"></asp:Label>
                            </td>
                            <td style="width:450px;">
                                <asp:TextBox ID="TextBoxAdd1" runat="server" Width="440px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label27" runat="server" Text="Address 2"></asp:Label>
                            </td>
                            <td style="width:450px;">
                                <asp:TextBox ID="TextBoxAdd2" runat="server" Width="440px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label29" runat="server" Text="TEL"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxTEL" runat="server" Width="150px" MaxLength="12"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label31" runat="server" Text="Mobile"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:TextBox ID="TextBoxMob" runat="server" Width="150px" MaxLength="12"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label33" runat="server" Text="E-mail"></asp:Label>
                            </td>
                            <td style="width:250px;">
                                <asp:TextBox ID="TextBoxMail" runat="server" Width="240px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label39" runat="server" Text="Bank Name"></asp:Label>
                            </td>
                            <td style="width:250px;">
                                <asp:TextBox ID="TextBoxBankName" runat="server" Width="240px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label40" runat="server" Text="Account No."></asp:Label>
                            </td>
                            <td style="width:250px;">
                                <asp:TextBox ID="TextBoxAccount" runat="server" Width="240px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label32" runat="server" Text="Language"></asp:Label>
                            </td>
                            <td style="width:250px;">
                                <asp:RadioButtonList ID="RadioButtonListLang" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">English</asp:ListItem>
                                    <asp:ListItem Value="1">Japanese</asp:ListItem>
                                    <asp:ListItem Value="2">Thai</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 5px;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label5" runat="server" Text="Supervisor"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxParents" runat="server" Width="150px" MaxLength="10" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AppendDataBoundItems="True" DataSourceID="SqlDataSource15" DataTextField="NAME" DataValueField="NAME">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>None</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource15" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME] FROM [TB_R_USER]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 5px;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label26" runat="server" Text="Approver 1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxApprover1" runat="server" Width="150px" MaxLength="10" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged" AppendDataBoundItems="True" DataSourceID="SqlDataSource15" DataTextField="NAME" DataValueField="NAME">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource16" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME] FROM [TB_R_USER]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label28" runat="server" Text="Approver 2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxApprover2" runat="server" Width="150px" MaxLength="10" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged" AppendDataBoundItems="True" DataSourceID="SqlDataSource15" DataTextField="NAME" DataValueField="NAME">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource17" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME] FROM [TB_R_USER]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label30" runat="server" Text="Approver 3"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxApprover3" runat="server" Width="150px" MaxLength="10" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged" AppendDataBoundItems="True" DataSourceID="SqlDataSource15" DataTextField="NAME" DataValueField="NAME">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource18" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME] FROM [TB_R_USER]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 5px; margin-bottom:10px;">
                        <tr>
                            <td class="title01">
                                <asp:Label ID="Label1" runat="server" Text="Authorization"></asp:Label>
                            </td>
                            <td class="input01">
                                <asp:CheckBoxList ID="CheckBoxListAuth" runat="server" RepeatDirection="Horizontal" Width="700px" Enabled="False" RepeatColumns="4">
                                    <asp:ListItem>WORKING RECORD</asp:ListItem>
                                    <asp:ListItem>ORGANIZATIONAL</asp:ListItem>
                                    <asp:ListItem>EMPLOYEE EDIT(ALL)</asp:ListItem>
                                    <asp:ListItem>SALARY</asp:ListItem>
                                    <asp:ListItem>WORKING RECORD IMPORT</asp:ListItem>
                                    <asp:ListItem>EMPLOYEE VIEW</asp:ListItem>
                                    <asp:ListItem>EMPLOYEE EDIT(MINE)</asp:ListItem>
                                    <asp:ListItem>SETTING</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="License" Width="100px">
                <ContentTemplate>
                    <div style="margin-top:5px; margin-left:5px; margin-bottom:5px;">
                        <asp:Label ID="Label22" runat="server" Text="#can not roll back after modified." ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView2" runat="server" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" DataKeyNames="SEQ_ID" AutoGenerateColumns="False" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" OnRowCommand="GridView2_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                            <Columns>
                                <asp:TemplateField HeaderText="License Name" SortExpression="LICENSE_NAME">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource13" DataTextField="DETAIL" DataValueField="DETAIL" AppendDataBoundItems="True" SelectedValue='<%# Bind("LICENSE_NAME") %>'>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource13" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DETAIL] FROM [TB_M_LICENSE]"></asp:SqlDataSource>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("LICENSE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Registry Num" SortExpression="REGISTRY_NUMBER">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("REGISTRY_NUMBER") %>' MaxLength="50"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("REGISTRY_NUMBER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acquisition Date" SortExpression="ACQUISITION_DT">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACQUISITION_DT", "{0:yyyy/MM/dd}") %>'></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="Calendarextender4" runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd">
                                        </ajaxtoolkit:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ACQUISITION_DT", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark" SortExpression="REMARK">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("REMARK") %>' MaxLength="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("REMARK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SEQ_ID" InsertVisible="False" ReadOnly="True" SortExpression="SEQ_ID">
                                    <ItemStyle ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button ID="Button_Add1" runat="server" Text="Add" Width="60px" OnClick="Button_Add1_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [LICENSE_NAME], [REGISTRY_NUMBER], [ACQUISITION_DT], [REMARK], [SEQ_ID] FROM [TB_R_LICENSE] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [TB_R_LICENSE] WHERE [SEQ_ID] = @SEQ_ID" InsertCommand="INSERT INTO [TB_R_LICENSE] ([LICENSE_NAME], [REGISTRY_NUMBER], [ACQUISITION_DT], [REMARK]) VALUES (@LICENSE_NAME, @REGISTRY_NUMBER, @ACQUISITION_DT, @REMARK)" UpdateCommand="UPDATE [TB_R_LICENSE] SET [LICENSE_NAME] = @LICENSE_NAME, [REGISTRY_NUMBER] = @REGISTRY_NUMBER, [ACQUISITION_DT] = @ACQUISITION_DT, [REMARK] = @REMARK WHERE [SEQ_ID] = @SEQ_ID">
                        <DeleteParameters>
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="LICENSE_NAME" Type="String" />
                            <asp:Parameter Name="REGISTRY_NUMBER" Type="String" />
                            <asp:Parameter Name="ACQUISITION_DT" Type="DateTime" />
                            <asp:Parameter Name="REMARK" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="LICENSE_NAME" Type="String" />
                            <asp:Parameter Name="REGISTRY_NUMBER" Type="String" />
                            <asp:Parameter Name="ACQUISITION_DT" Type="DateTime" />
                            <asp:Parameter Name="REMARK" Type="String" />
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Work Experience" Width="100px">
                <ContentTemplate>
                    <div style="margin-top:5px; margin-left:5px; margin-bottom:5px;">
                        <asp:Label ID="Label9" runat="server" Text="#can not roll back after modified." ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView3" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="#333333" DataKeyNames="SEQ_ID" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" OnRowCommand="GridView3_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" SortExpression="DATE">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DATE", "{0:yyyy/MM/dd}") %>'></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="Calendarextender5" runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd">
                                        </ajaxtoolkit:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DATE", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detail" SortExpression="DETAIL">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DETAIL") %>' Width="400px" MaxLength="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DETAIL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SEQ_ID" InsertVisible="False" ReadOnly="True" SortExpression="SEQ_ID">
                                    <ItemStyle ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button ID="Button_Add2" runat="server" Text="Add" Width="60px" OnClick="Button_Add2_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DATE], [DETAIL], [SEQ_ID] FROM [TB_R_WORK_EXPERIENCE] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [TB_R_WORK_EXPERIENCE] WHERE [SEQ_ID] = @SEQ_ID" InsertCommand="INSERT INTO [TB_R_WORK_EXPERIENCE] ([DATE], [DETAIL]) VALUES (@DATE, @DETAIL)" UpdateCommand="UPDATE [TB_R_WORK_EXPERIENCE] SET [DATE] = @DATE, [DETAIL] = @DETAIL WHERE [SEQ_ID] = @SEQ_ID">
                        <DeleteParameters>
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="DATE" Type="DateTime" />
                            <asp:Parameter Name="DETAIL" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DATE" Type="DateTime" />
                            <asp:Parameter Name="DETAIL" Type="String" />
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Education" Width="100px">
                <ContentTemplate>
                    <div style="margin-top:5px; margin-left:5px; margin-bottom:5px;">
                        <asp:Label ID="Label10" runat="server" Text="#can not roll back after modified." ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView4" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource4" ForeColor="#333333" DataKeyNames="SEQ_ID" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" OnRowCommand="GridView4_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:TemplateField HeaderText="Education" SortExpression="EDUCATION">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EDUCATION") %>' MaxLength="30"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("EDUCATION") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="School Name" SortExpression="SCHOOL_NAME">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SCHOOL_NAME") %>' MaxLength="50"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SCHOOL_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject" SortExpression="SUBJECT">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("SUBJECT") %>' MaxLength="50"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("SUBJECT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Graduation Date" SortExpression="GRADUATION_DATE">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("GRADUATION_DATE", "{0:yyyy/MM/dd}") %>'></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="Calendarextender6" runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd">
                                        </ajaxtoolkit:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("GRADUATION_DATE", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SEQ_ID" InsertVisible="False" ReadOnly="True" SortExpression="SEQ_ID">
                                    <ItemStyle ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button ID="Button_Add3" runat="server" Text="Add" Width="60px" OnClick="Button_Add3_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [EDUCATION], [SCHOOL_NAME], [SUBJECT], [GRADUATION_DATE], [SEQ_ID] FROM [TB_R_EDUCATION] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [TB_R_EDUCATION] WHERE [SEQ_ID] = @SEQ_ID" InsertCommand="INSERT INTO [TB_R_EDUCATION] ([EDUCATION], [SCHOOL_NAME], [SUBJECT], [GRADUATION_DATE]) VALUES (@EDUCATION, @SCHOOL_NAME, @SUBJECT, @GRADUATION_DATE)" UpdateCommand="UPDATE [TB_R_EDUCATION] SET [EDUCATION] = @EDUCATION, [SCHOOL_NAME] = @SCHOOL_NAME, [SUBJECT] = @SUBJECT, [GRADUATION_DATE] = @GRADUATION_DATE WHERE [SEQ_ID] = @SEQ_ID">
                        <DeleteParameters>
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="EDUCATION" Type="String" />
                            <asp:Parameter Name="SCHOOL_NAME" Type="String" />
                            <asp:Parameter Name="SUBJECT" Type="String" />
                            <asp:Parameter Name="GRADUATION_DATE" Type="DateTime" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="EDUCATION" Type="String" />
                            <asp:Parameter Name="SCHOOL_NAME" Type="String" />
                            <asp:Parameter Name="SUBJECT" Type="String" />
                            <asp:Parameter Name="GRADUATION_DATE" Type="DateTime" />
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="Training" Width="100px">
                <ContentTemplate>
                    <div style="margin-top:5px; margin-left:5px; margin-bottom:5px;">
                        <asp:Label ID="Label12" runat="server" Text="#can not roll back after modified." ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView5" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource5" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" DataKeyNames="SEQ_ID" OnRowCommand="GridView5_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:TemplateField HeaderText="Date" SortExpression="DATE">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DATE", "{0:yyyy/MM/dd}") %>'></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="Calendarextender7" runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd">
                                        </ajaxtoolkit:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DATE", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detail" SortExpression="DETAIL">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DETAIL") %>' Width="400px" MaxLength="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DETAIL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Host" SortExpression="HOST">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("HOST") %>' MaxLength="30"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("HOST") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SEQ_ID" InsertVisible="False" ReadOnly="True" SortExpression="SEQ_ID">
                                    <ItemStyle ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button ID="Button4" runat="server" Text="Add" Width="60px" OnClick="Button_Add4_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DATE], [DETAIL], [HOST], [SEQ_ID] FROM [TB_R_TRAINING] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [TB_R_TRAINING] WHERE [SEQ_ID] = @SEQ_ID" InsertCommand="INSERT INTO [TB_R_TRAINING] ([DATE], [DETAIL], [HOST]) VALUES (@DATE, @DETAIL, @HOST)" UpdateCommand="UPDATE [TB_R_TRAINING] SET [DATE] = @DATE, [DETAIL] = @DETAIL, [HOST] = @HOST WHERE [SEQ_ID] = @SEQ_ID">
                        <DeleteParameters>
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="DATE" Type="DateTime" />
                            <asp:Parameter Name="DETAIL" Type="String" />
                            <asp:Parameter Name="HOST" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DATE" Type="DateTime" />
                            <asp:Parameter Name="DETAIL" Type="String" />
                            <asp:Parameter Name="HOST" Type="String" />
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="Family Makeup" Width="100px">
                <ContentTemplate>
                    <div style="margin-top:5px; margin-left:5px; margin-bottom:5px;">
                        <asp:Label ID="Label14" runat="server" Text="#can not roll back after modified." ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView6" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource6" ForeColor="#333333" DataKeyNames="SEQ_ID" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" OnRowCommand="GridView6_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:TemplateField HeaderText="Name" SortExpression="NAME">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("NAME") %>' MaxLength="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Relationship" SortExpression="RELATIONSHIP">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList3" runat="server" SelectedValue='<%# Bind("RELATIONSHIP") %>'>
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Wife</asp:ListItem>
                                            <asp:ListItem>Son</asp:ListItem>
                                            <asp:ListItem>Daughter</asp:ListItem>
                                            <asp:ListItem>Father</asp:ListItem>
                                            <asp:ListItem>Mother</asp:ListItem>
                                        </asp:DropDownList>
<%--                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RELATIONSHIP") %>' MaxLength="30"></asp:TextBox>
                                        --%>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("RELATIONSHIP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Birthday" SortExpression="BIRTHDAY">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BIRTHDAY", "{0:yyyy/MM/dd}") %>'></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="Calendarextender8" runat="server" TargetControlID="TextBox2" Format="yyyy/MM/dd">
                                        </ajaxtoolkit:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BIRTHDAY", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Living" SortExpression="LIVING_TOGETHER">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("LIVING_TOGETHER") %>'>
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Y</asp:ListItem>
                                            <asp:ListItem>N</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("LIVING_TOGETHER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Provide" SortExpression="PROVIDE">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("PROVIDE") %>'>
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Y</asp:ListItem>
                                            <asp:ListItem>N</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("PROVIDE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Education" SortExpression="EDUCATION">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList4" runat="server" SelectedValue='<%# Bind("EDUCATION") %>'>
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Y</asp:ListItem>
                                            <asp:ListItem>N</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("EDUCATION") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SEQ_ID" InsertVisible="False" ReadOnly="True" SortExpression="SEQ_ID">
                                    <ItemStyle ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button ID="Button_Add5" runat="server" Text="Add" Width="60px" OnClick="Button_Add5_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME], [RELATIONSHIP], [BIRTHDAY], [LIVING_TOGETHER], [PROVIDE], [EDUCATION], [SEQ_ID] FROM [TB_R_FAMILY] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [TB_R_FAMILY] WHERE [SEQ_ID] = @SEQ_ID" InsertCommand="INSERT INTO [TB_R_FAMILY] ([NAME], [RELATIONSHIP], [BIRTHDAY], [LIVING_TOGETHER], [PROVIDE], [EDUCATION]) VALUES (@NAME, @RELATIONSHIP, @BIRTHDAY, @LIVING_TOGETHER, @PROVIDE, @EDUCATION)" UpdateCommand="UPDATE [TB_R_FAMILY] SET [NAME] = @NAME, [RELATIONSHIP] = @RELATIONSHIP, [BIRTHDAY] = @BIRTHDAY, [LIVING_TOGETHER] = @LIVING_TOGETHER, [PROVIDE] = @PROVIDE, [EDUCATION] = @EDUCATION WHERE [SEQ_ID] = @SEQ_ID">
                        <DeleteParameters>
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="NAME" Type="String" />
                            <asp:Parameter Name="RELATIONSHIP" Type="String" />
                            <asp:Parameter Name="BIRTHDAY" Type="DateTime" />
                            <asp:Parameter Name="LIVING_TOGETHER" Type="String" />
                            <asp:Parameter Name="PROVIDE" Type="String" />
                            <asp:Parameter Name="EDUCATION" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="NAME" Type="String" />
                            <asp:Parameter Name="RELATIONSHIP" Type="String" />
                            <asp:Parameter Name="BIRTHDAY" Type="DateTime" />
                            <asp:Parameter Name="LIVING_TOGETHER" Type="String" />
                            <asp:Parameter Name="PROVIDE" Type="String" />
                            <asp:Parameter Name="EDUCATION" Type="String" />
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel8" runat="server" HeaderText="Salary Data" Width="100px"  Visible="false">
                <ContentTemplate>
                    <div style="margin-top:5px; margin-left:5px; margin-bottom:5px;">
                        <asp:Label ID="Label24" runat="server" Text="#can not roll back after modified." ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource14" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" DataKeyNames="SEQ_ID"  OnRowCommand="GridView8_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:BoundField DataField="DESC" SortExpression="DESC" ReadOnly="True"/>
                                <asp:BoundField DataField="AMOUNT" HeaderText="Amount" SortExpression="AMOUNT" />
                                <asp:BoundField DataField="REMARK" HeaderText="Remark" SortExpression="REMARK"/>
                                <asp:BoundField DataField="SEQ_ID" InsertVisible="False" ReadOnly="True" SortExpression="SEQ_ID">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button ID="Button_Add7" runat="server" Text="Add" Width="60px" OnClick="Button_Add7_Click"/>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource14" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DESC], CONVERT(varchar, CONVERT(MONEY,AMOUNT), 1) AS [AMOUNT], [REMARK], [SEQ_ID] FROM [TB_R_SALARY] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [TB_R_SALARY] WHERE [SEQ_ID] = @SEQ_ID" InsertCommand="INSERT INTO [TB_R_SALARY] ([AMOUNT], [REMARK]) VALUES (@AMOUNT, @REMARK)" UpdateCommand="UPDATE [TB_R_SALARY] SET [AMOUNT] = @AMOUNT, [REMARK] = @REMARK WHERE [SEQ_ID] = @SEQ_ID">
                        <DeleteParameters>
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="AMOUNT" Type="Decimal" />
                            <asp:Parameter Name="REMARK" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="AMOUNT" Type="Decimal" />
                            <asp:Parameter Name="REMARK" Type="String" />
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <table ID="Table1" runat="server" style="height: 100px; width:500px;" visible="False">
                        <tr id="Tr1" align="center" runat="server">
                            <td id="Td1" bgcolor="#CCCCCC" runat="server">
                                <asp:Label ID="Label20" runat="server" Text="Do not have a Data."></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="Revision" Width="100px">
                <ContentTemplate>
                    <div style="margin-top:5px; margin-left:5px; margin-bottom:5px;">
                        <asp:Label ID="Label17" runat="server" Text="#can not roll back after modified." ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource7" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" DataKeyNames="SEQ_ID" OnRowCommand="GridView7_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:TemplateField HeaderText="Date" SortExpression="DATE">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DATE", "{0:yyyy/MM/dd}") %>' Width="80px"></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="Calendarextender9" runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd">
                                        </ajaxtoolkit:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DATE", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detail" SortExpression="DETAIL">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DETAIL") %>' Width="200px" MaxLength="50"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DETAIL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Before" SortExpression="BEFORE">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("BEFORE") %>' Width="250px" MaxLength="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("BEFORE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="After" SortExpression="AFTER">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("AFTER") %>' Width="250px" MaxLength="100"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("AFTER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SEQ_ID" InsertVisible="False" ReadOnly="True" SortExpression="SEQ_ID">
                                    <ItemStyle ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button ID="Button_Add6" runat="server" Text="Add" Width="60px" OnClick="Button_Add6_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DATE], [DETAIL], [BEFORE], [AFTER], [SEQ_ID] FROM [TB_R_REVISION] WHERE ([ID] = @ID)" DeleteCommand="DELETE FROM [TB_R_REVISION] WHERE [SEQ_ID] = @SEQ_ID" InsertCommand="INSERT INTO [TB_R_REVISION] ([DATE], [DETAIL], [BEFORE], [AFTER]) VALUES (@DATE, @DETAIL, @BEFORE, @AFTER)" UpdateCommand="UPDATE [TB_R_REVISION] SET [DATE] = @DATE, [DETAIL] = @DETAIL, [BEFORE] = @BEFORE, [AFTER] = @AFTER WHERE [SEQ_ID] = @SEQ_ID">
                        <DeleteParameters>
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="DATE" Type="DateTime" />
                            <asp:Parameter Name="DETAIL" Type="String" />
                            <asp:Parameter Name="BEFORE" Type="String" />
                            <asp:Parameter Name="AFTER" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DATE" Type="DateTime" />
                            <asp:Parameter Name="DETAIL" Type="String" />
                            <asp:Parameter Name="BEFORE" Type="String" />
                            <asp:Parameter Name="AFTER" Type="String" />
                            <asp:Parameter Name="SEQ_ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
