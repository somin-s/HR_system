<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee_Detail.aspx.cs" Inherits="BrightHRSystem.Employee_Detail" %>

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
                        <asp:MenuItem Text="Back" Value="Back" NavigateUrl="~/Employee.aspx"></asp:MenuItem>
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
            <div style="margin-top:3px; margin-left:5px;">
                <asp:Button ID="Button_Before" runat="server" Text="←" Height="20px" Width="40px" OnClick="Button_Before_Click"/>
                <asp:Label ID="Label7" runat="server" Text="&nbsp;&nbsp;"></asp:Label>
                <asp:Button ID="Button_After" runat="server" Text="→" Height="20px" Width="40px" OnClick="Button_After_Click"/>
            </div>
            <div style="margin-top:20px; margin-left:10px; float:left;">
                <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; float:left;">
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelID" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="input02">
                            <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label17" runat="server" Text="Thai Name"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelLocalName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label9" runat="server" Text="Short Name"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelShortName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label6" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelEmpTyp" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label8" runat="server" Text="Termination Date"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelTermDt" runat="server" Text=""></asp:Label>
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
                            <asp:Label ID="Label1Dep" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label13" runat="server" Text="Division"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="Label1Div" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label16" runat="server" Text="Section"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelSec" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label15" runat="server" Text="Position"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelPos" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title01">
                            <asp:Label ID="Label18" runat="server" Text="Job Category"></asp:Label>
                        </td>
                        <td class="input01">
                            <asp:Label ID="LabelJobC" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:20px; margin-left:150px; float:left; width: 131px;">
                <asp:Image ID="ImagePic" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="160px" Width="120px"/>
            </div>
            <table style="float:right;">
                <tr>
                    <td>
                        <asp:Button ID="Button_Back" runat="server" Text="Back" Width="60px" 
                            Height="30px" OnClick="Button_Back_Click"/>
                        <asp:Button ID="Button_Edit" runat="server" Text="Edit" Width="60px" Enabled="false"
                            Height="30px" OnClick="Button_Edit_Click"/>
                        <asp:Button ID="Button_Delete" runat="server" Text="Delete" Width="60px"  Enabled="false"
                            Height="30px" OnClick="Button_Delete_Click"
                            OnClientClick="return confirm('Do you want to Delete this data?')"/>
                    </td>
                </tr>
            </table>
        </div>
        <ajaxToolkit:TabContainer ID="tab" runat="server" AutoPostBack="true" Width="1000px" ActiveTabIndex="0" OnActiveTabChanged="tab_ActiveTabChanged">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Basic">
                <ContentTemplate><table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top:5px;"><tr><td class="title01"><asp:Label ID="Label21" runat="server" Text="Birth Day"></asp:Label></td><td class="input01"><asp:Label ID="LabelBirth" runat="server"></asp:Label></td><td style="width:50px;"><asp:Label ID="LabelAge" runat="server"></asp:Label></td><td><asp:Label ID="Label22" runat="server" Text="(old)"></asp:Label></td></tr></table><table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;"><tr><td class="title01"><asp:Label ID="Label23" runat="server" Text="Entering Day"></asp:Label></td><td class="input01"><asp:Label ID="LabelEntDt" runat="server"></asp:Label></td><td style="width:50px;"><asp:Label ID="LabelEntYear" runat="server"></asp:Label></td><td><asp:Label ID="LabelEntYearUnit" runat="server"></asp:Label></td></tr></table><table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;"><tr><td class="title01"><asp:Label ID="Label25" runat="server" Text="Address 1"></asp:Label></td><td style="width:450px;"><asp:Label ID="LabelAdd1" runat="server"></asp:Label></td></tr><tr><td class="title01"><asp:Label ID="Label27" runat="server" Text="Address 2"></asp:Label></td><td style="width:450px;"><asp:Label ID="LabelAdd2" runat="server"></asp:Label></td></tr></table><table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;"><tr><td class="title01"><asp:Label ID="Label29" runat="server" Text="TEL"></asp:Label></td><td class="input01"><asp:Label ID="LabelTEL" runat="server"></asp:Label></td></tr><tr><td class="title01"><asp:Label ID="Label31" runat="server" Text="Mobile"></asp:Label></td><td class="input01"><asp:Label ID="LabelMob" runat="server"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;"><tr><td class="title01"><asp:Label ID="Label33" runat="server" Text="E-mail"></asp:Label></td><td style="width:250px;"><asp:Label ID="LabelMail" runat="server"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;"><tr><td class="title01"><asp:Label ID="Label20" runat="server" Text="Bank Name"></asp:Label></td><td style="width:250px;"><asp:Label ID="LabelBankName" runat="server"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;"><tr><td class="title01"><asp:Label ID="Label26" runat="server" Text="Account No."></asp:Label></td><td style="width:250px;"><asp:Label ID="LabelAccount" runat="server"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse;"><tr><td class="title01"><asp:Label ID="Label14" runat="server" Text="Language"></asp:Label></td><td class="input01"><asp:Label ID="LabelLang" runat="server"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 5px;"><tr><td class="title01"><asp:Label ID="Label5" runat="server" Text="Supervisor"></asp:Label></td><td class="input01"><asp:Label ID="LabelParents" runat="server"></asp:Label></td><td><asp:Label ID="LabelParentsName" runat="server" Width="300px"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 5px;"><tr><td class="title01"><asp:Label ID="Label12" runat="server" Text="Approver 1"></asp:Label></td><td class="input01"><asp:Label ID="LabelApprover1" runat="server"></asp:Label></td><td><asp:Label ID="LabelApproverName1" runat="server" Width="300px"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;"><tr><td class="title01"><asp:Label ID="Label19" runat="server" Text="Approver 2"></asp:Label></td><td class="input01"><asp:Label ID="LabelApprover2" runat="server"></asp:Label></td><td><asp:Label ID="LabelApproverName2" runat="server" Width="300px"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 2px;"><tr><td class="title01"><asp:Label ID="Label24" runat="server" Text="Approver 3"></asp:Label></td><td class="input01"><asp:Label ID="LabelApprover3" runat="server"></asp:Label></td><td><asp:Label ID="LabelApproverName3" runat="server" Width="300px"></asp:Label></td></tr></table>
                    <table border="1" cellspacing="0" cellpadding="1" style="border-collapse: collapse; margin-top: 5px; margin-bottom:10px;"><tr><td class="title01"><asp:Label ID="Label1" runat="server" Text="Authorization"></asp:Label></td><td class="input01"><asp:CheckBoxList ID="CheckBoxListAuth" runat="server" RepeatDirection="Horizontal" Width="700px" Enabled="False" RepeatColumns="4"><asp:ListItem>WORKING RECORD</asp:ListItem><asp:ListItem>ORGANIZATIONAL</asp:ListItem><asp:ListItem>EMPLOYEE EDIT(ALL)</asp:ListItem><asp:ListItem>SALARY</asp:ListItem><asp:ListItem>WORKING RECORD IMPORT</asp:ListItem><asp:ListItem>EMPLOYEE VIEW</asp:ListItem><asp:ListItem>EMPLOYEE EDIT(MINE)</asp:ListItem><asp:ListItem>SETTING</asp:ListItem></asp:CheckBoxList></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="License" Width="100px">
                <ContentTemplate><div style="margin-top:5px; margin-left:5px; margin-bottom:5px;"><asp:GridView ID="GridView2" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"><AlternatingRowStyle BackColor="White" ForeColor="#284775"/><Columns><asp:BoundField DataField="LICENSE_NAME" HeaderText="License Name" SortExpression="LICENSE_NAME" /><asp:BoundField DataField="REGISTRY_NUMBER" HeaderText="Registry Num" SortExpression="REGISTRY_NUMBER" /><asp:BoundField DataField="ACQUISITION_DT" HeaderText="Acquisition Date" SortExpression="ACQUISITION_DT" DataFormatString="{0:yyyy/MM/dd}"/><asp:BoundField DataField="REMARK" HeaderText="Remark" SortExpression="REMARK" /></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [LICENSE_NAME], [REGISTRY_NUMBER], [ACQUISITION_DT], [REMARK] FROM [TB_R_LICENSE] WHERE ([ID] = @ID)"><SelectParameters><asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" /></SelectParameters></asp:SqlDataSource><table ID="NoData2" runat="server" style="height: 100px; width:500px;" visible="False"><tr align="center" runat="server"><td bgcolor="#CCCCCC" runat="server"><asp:Label ID="Label36" runat="server" Text="Do not have a Data."></asp:Label></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Work Experience" Width="100px">
                <ContentTemplate><div style="margin-top:5px; margin-left:5px; margin-bottom:5px;"><asp:GridView ID="GridView3" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"><AlternatingRowStyle BackColor="White" ForeColor="#284775"/><Columns><asp:BoundField DataField="DATE" HeaderText="Date" SortExpression="DATE" DataFormatString="{0:yyyy/MM/dd}"/><asp:BoundField DataField="DETAIL" HeaderText="Detail" SortExpression="DETAIL" /></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div><asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DATE], [DETAIL] FROM [TB_R_WORK_EXPERIENCE] WHERE ([ID] = @ID)"><SelectParameters><asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" /></SelectParameters></asp:SqlDataSource><table ID="NoData3" runat="server" style="height: 100px; width:500px;" visible="False"><tr align="center" runat="server"><td bgcolor="#CCCCCC" runat="server"><asp:Label ID="Label35" runat="server" Text="Do not have a Data."></asp:Label></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Education" Width="100px">
                <ContentTemplate><div style="margin-top:5px; margin-left:5px; margin-bottom:5px;"><asp:GridView ID="GridView4" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource4" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"><AlternatingRowStyle BackColor="White" ForeColor="#284775"/><Columns><asp:BoundField DataField="EDUCATION" HeaderText="Education" SortExpression="EDUCATION" /><asp:BoundField DataField="SCHOOL_NAME" HeaderText="School Name" SortExpression="SCHOOL_NAME" /><asp:BoundField DataField="SUBJECT" HeaderText="Subject" SortExpression="SUBJECT" /><asp:BoundField DataField="GRADUATION_DATE" HeaderText="Graduation Date" SortExpression="GRADUATION_DATE" DataFormatString="{0:yyyy/MM/dd}"/></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div><asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [EDUCATION], [SCHOOL_NAME], [SUBJECT], [GRADUATION_DATE] FROM [TB_R_EDUCATION] WHERE ([ID] = @ID)"><SelectParameters><asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" /></SelectParameters></asp:SqlDataSource><table ID="NoData4" runat="server" style="height: 100px; width:500px;" visible="False"><tr align="center" runat="server"><td bgcolor="#CCCCCC" runat="server"><asp:Label ID="Label37" runat="server" Text="Do not have a Data."></asp:Label></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="Training" Width="100px">
                <ContentTemplate><div style="margin-top:5px; margin-left:5px; margin-bottom:5px;"><asp:GridView ID="GridView5" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource5" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"><AlternatingRowStyle BackColor="White" ForeColor="#284775"/><Columns><asp:BoundField DataField="DATE" HeaderText="Date" SortExpression="DATE" DataFormatString="{0:yyyy/MM/dd}"/><asp:BoundField DataField="DETAIL" HeaderText="Detail" SortExpression="DETAIL" /><asp:BoundField DataField="HOST" HeaderText="Host" SortExpression="HOST" /></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div><asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DATE], [DETAIL], [HOST] FROM [TB_R_TRAINING] WHERE ([ID] = @ID)"><SelectParameters><asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" /></SelectParameters></asp:SqlDataSource><table ID="NoData5" runat="server" style="height: 100px; width:500px;" visible="False"><tr align="center" runat="server"><td bgcolor="#CCCCCC" runat="server"><asp:Label ID="Label38" runat="server" Text="Do not have a Data."></asp:Label></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="Family Makeup" Width="100px">
                <ContentTemplate><div style="margin-top:5px; margin-left:5px; margin-bottom:5px;"><asp:GridView ID="GridView6" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource6" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"><AlternatingRowStyle BackColor="White" ForeColor="#284775"/><Columns><asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME" /><asp:BoundField DataField="RELATIONSHIP" HeaderText="Relationship" SortExpression="RELATIONSHIP" /><asp:BoundField DataField="BIRTHDAY" HeaderText="Birthday" SortExpression="BIRTHDAY" DataFormatString="{0:yyyy/MM/dd}"/><asp:BoundField DataField="LIVING_TOGETHER" HeaderText="Living" SortExpression="LIVING_TOGETHER" /><asp:BoundField DataField="PROVIDE" HeaderText="Provide" SortExpression="PROVIDE" /><asp:BoundField DataField="EDUCATION" HeaderText="Education" SortExpression="EDUCATION" /></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div><asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [NAME], [RELATIONSHIP], [BIRTHDAY], [LIVING_TOGETHER], [PROVIDE], [EDUCATION] FROM [TB_R_FAMILY] WHERE ([ID] = @ID)"><SelectParameters><asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" /></SelectParameters></asp:SqlDataSource><table ID="NoData6" runat="server" style="height: 100px; width:500px;" visible="False"><tr align="center" runat="server"><td bgcolor="#CCCCCC" runat="server"><asp:Label ID="Label39" runat="server" Text="Do not have a Data."></asp:Label></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel8" runat="server" HeaderText="Salary Data" Width="100px" Visible="False">
                <ContentTemplate><div style="margin-top:5px; margin-left:5px; margin-bottom:5px;"><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource8" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"><AlternatingRowStyle BackColor="White" ForeColor="#284775"/><Columns><asp:BoundField DataField="DESC" HeaderText="Desc" SortExpression="DESC" /><asp:BoundField DataField="AMOUNT" HeaderText="Amount" SortExpression="AMOUNT" /><asp:BoundField DataField="REMARK" HeaderText="REMARK" SortExpression="REMARK" /></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div><asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DESC], CONVERT(varchar, CONVERT(MONEY,AMOUNT), 1) AS [AMOUNT], [REMARK] FROM [TB_R_SALARY] WHERE ([ID] = @ID)"><SelectParameters><asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" /></SelectParameters></asp:SqlDataSource><table ID="Table1" runat="server" style="height: 100px; width:500px;" visible="False"><tr id="Tr1" align="center" runat="server"><td id="Td1" bgcolor="#CCCCCC" runat="server"><asp:Label ID="Label10" runat="server" Text="Do not have a Data."></asp:Label></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="Revision" Width="100px">
                <ContentTemplate><div style="margin-top:5px; margin-left:5px; margin-bottom:5px;"><asp:GridView ID="GridView7" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource7" ForeColor="#333333" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"><AlternatingRowStyle BackColor="White" ForeColor="#284775"/><Columns><asp:BoundField DataField="DATE" HeaderText="Date" SortExpression="DATE" DataFormatString="{0:yyyy/MM/dd}"/><asp:BoundField DataField="DETAIL" HeaderText="Detail" SortExpression="DETAIL" /><asp:BoundField DataField="BEFORE" HeaderText="Before" SortExpression="BEFORE" /><asp:BoundField DataField="AFTER" HeaderText="After" SortExpression="AFTER" /></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div><asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="SELECT [DATE], [DETAIL], [BEFORE], [AFTER] FROM [TB_R_REVISION] WHERE ([ID] = @ID)"><SelectParameters><asp:SessionParameter Name="ID" SessionField="selected_id" Type="String" /></SelectParameters></asp:SqlDataSource><table ID="NoData7" runat="server" style="height: 100px; width:500px;" visible="False"><tr align="center" runat="server"><td bgcolor="#CCCCCC" runat="server"><asp:Label ID="Label40" runat="server" Text="Do not have a Data."></asp:Label></td></tr></table></ContentTemplate>
            
</ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
