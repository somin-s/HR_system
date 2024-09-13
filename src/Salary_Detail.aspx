<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salary_Detail.aspx.cs" Inherits="BrightHRSystem.Salary_Detail" %>

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
                <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" 
                    Font-Underline="True" style="margin-bottom: 5px; " 
                    ItemWrap="True">
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="1px"/>
                    <Items>
                        <asp:MenuItem Text="Back" Value="Back" NavigateUrl="~/Salary.aspx"></asp:MenuItem>
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
            <asp:Label ID="Label2" runat="server" Text="Salary Management" BackColor="#99CCFF" 
                Width="1180px" style="font-size:large;"></asp:Label>
            <div style="margin-top:3px; margin-left:5px;">
                <asp:Button ID="Button_Before" runat="server" Text="←" Height="20px" Width="40px" OnClick="Button_Before_Click"/>
                <asp:Label ID="Label9" runat="server" Text="&nbsp;&nbsp;"></asp:Label>
                <asp:Button ID="Button_After" runat="server" Text="→" Height="20px" Width="40px" OnClick="Button_After_Click"/>
            </div>
            <div style="font-size:small; float:left; width: 600px; margin-top:10px; margin-left:5px;">
                <asp:Label ID="Label3" runat="server" Text="XXX-20XX Salary Data" Font-Bold="False" Font-Overline="False" Font-Size="XX-Large" Font-Underline="True"></asp:Label>
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
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:HRConnectionString %>" SelectCommand="
SELECT U.NAME AS [NAME]
  FROM [TB_R_PAYROLL_H] AS H
  LEFT OUTER JOIN [TB_R_PAYROLL_D] AS D
  ON H.[SEQ_ID] = D.HEADER_ID
  LEFT OUTER JOIN [TB_R_USER] AS U
  ON D.[USER_ID] = U.ID
  WHERE H.[YEAR] = @selected_year AND H.[MONTH] = @selected_month
  ORDER BY D.[USER_ID]
                                ">
                                <SelectParameters>
                                    <asp:SessionParameter Name="selected_year" SessionField="selected_year" />
                                    <asp:SessionParameter Name="selected_month" SessionField="selected_month" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="float:right;">
                <tr>
                    <td>
                        <asp:DropDownList ID="DDSlipType" runat="server"></asp:DropDownList>
                        <asp:Button ID="Button_Print" runat="server" Text="Excel Slip" Width="100px" 
                            Height="30px" OnClick="Button_Print_Click"/>
                        <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="100px" 
                            Height="30px" OnClientClick="return confirm('Do you want to Save the Salary Data?')"
                            OnClick="ButtonSave_Click" />
                        <asp:Button ID="Button_Delete" runat="server" Text="Delete" Width="100px" 
                            Height="30px" OnClientClick="return confirm('Do you want to Delete the salary data?')" OnClick="Button_Delete_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <div style="margin-right:20px; float:left;">
                <asp:Label ID="Label57" runat="server" Text="Types of Work Items" Font-Bold="True" Font-Size="Medium"></asp:Label>
                <br />
                <br />
                <asp:Button ID="Button_GetWR" runat="server" Text="Get from Working Record" OnClick="Button_GetWR_Click"/>
                <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse; margin-top:5px; margin-left:5px; margin-bottom:5px;">
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label55" runat="server" Text="Working Days"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxWorkingDays" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label56" runat="server" Text="Working Days of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label58" runat="server" Text="Real Work Days"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxRealWorkDays" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label59" runat="server" Text="Real Work Days of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label60" runat="server" Text="Abcense Days"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxAbcenseDays" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label61" runat="server" Text="Abcense Days of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label62" runat="server" Text="Paid Holiday Days"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPaidHolidayDays" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label63" runat="server" Text="Paid Holiday Days of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label64" runat="server" Text="Business Trip Days"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxBusinessTripDays" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label65" runat="server" Text="Business Trip Days of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label66" runat="server" Text="Working Times"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxWorkTime" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label67" runat="server" Text="Working Times of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label68" runat="server" Text="Real Working Times"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxRealWorkTime" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label69" runat="server" Text="Real Working Times of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label96" runat="server" Text="Normal OT Times"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxNormalOTTime" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label97" runat="server" Text="Normal OT Times of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label98" runat="server" Text="Holiday Work Times"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxHolidayWorkTime" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label99" runat="server" Text="Holiday Work Times of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label100" runat="server" Text="Holiday OT Times"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxHolidayOTTime" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label101" runat="server" Text="Holiday OT Times of Target Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label70" runat="server" Text="Remain Paid Holiday"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxRemainPaidHoliday" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label71" runat="server" Text="Remain Paid Holiday of This User"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label72" runat="server" Text="Used Paid Holiday"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxUsedPaidHoliday" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label73" runat="server" Text="Used Paid Holiday of This User"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Label ID="Label74" runat="server" Text="Types of Incomes" Font-Bold="True" Font-Size="Medium"></asp:Label>
                <br />
                <br />
                <asp:Button ID="ButtonGetUserSetting" runat="server" Text="Get Incomes Amount" OnClick="ButtonGetUserSetting_Click" />
                <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse; margin-top:5px; margin-left:5px; margin-bottom:5px;">
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label75" runat="server" Text="Basic Salary"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxBasicSalary" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelBasicSalary" runat="server" Text="Basic Salary of This User"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label77" runat="server" Text="Unit Price"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxUnitPrice" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelUnitPrice" runat="server" Text="Unit Price of This User"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label79" runat="server" Text="OT Price"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxOTPrice" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelOTPrice" runat="server" Text="OT Price of This User"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label81" runat="server" Text="Holiday Work Price"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxHolidayWorkPrice" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelHolidayWorkPrice" runat="server" Text="Holiday Work Price of This User"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label102" runat="server" Text="Holiday OT Price"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxHolidayOTPrice" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelHolidayOTPrice" runat="server" Text="Holiday OT Price of This User"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label78" runat="server" Text="Basic Allowance"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxBasicAllowance" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelBasicAllowance" runat="server" Text="Basic Allowance of This User"></asp:Label>
                            <asp:Button ID="btnDetailAllowance" runat="server" Text="Detail" Visible="False" 
                                OnClientClick="window.showModalDialog('Salary_SpecialAllowance.aspx', window, 'resizable:0;dialogWidth:600px;dialogHeight:660px');"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label82" runat="server" Text="Special Allowance"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSpecialAllowance" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelSpecialAllowance" runat="server" Text="Special Allowance of This Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label105" runat="server" Text="Transpotation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxTranspotation" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelTranspotation" runat="server" Text="Transpotation of This Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label76" runat="server" Text="Hand Over"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxHandOver1" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountIncome" AutoPostBack="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="Label80" runat="server" Text="Hand Over of This Month"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:120px;">
                            <asp:Label ID="Label104" runat="server" Text="Income Total"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxIncomeTotal1" runat="server" Width="80px" style="text-align: right;" Enabled="false" BackColor="#CCFFCC" >0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:Label ID="LabelIncomeTotal1" runat="server" Text="Income Total of Target Month"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <asp:Label ID="Label83" runat="server" Text="Total Paid" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse; margin-top:5px; margin-left:5px; margin-bottom:5px;">
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label84" runat="server" Text="Income Total"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxIncomeTotal2" runat="server" Width="80px" style="text-align: right;" Enabled="false" BackColor="#CCFFCC" >0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label85" runat="server" Text="Income Total of Target Month"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label86" runat="server" Text="Social security"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxSocialSecurity" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountPaid" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label87" runat="server" Text="0.05% of Income total(MAX 750)"></asp:Label>
                        <asp:CheckBox ID="CheckBoxSocialSecurity" runat="server" Text="" AutoPostBack="True" Checked="true" OnCheckedChanged="CheckBoxSocialSecurity_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label88" runat="server" Text="Income Tax"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxIncomeTax" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountPaid" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label89" runat="server" Text="Income Tax of Target Month"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label106" runat="server" Text="Reduced"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxProvEmp" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountPaid" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label107" runat="server" Text="Reduced of Employee"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label108" runat="server" Text="Reduced "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxProvComp" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountPaid" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label109" runat="server" Text="Reduced of Company"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label90" runat="server" Text="Salary Payment"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxSalaryPayment" runat="server" Width="80px" style="text-align: right;" Enabled="false" BackColor="#CCFFCC" >0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label91" runat="server" Text="Income Total - Social security - Income Tax"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label92" runat="server" Text="Transfer"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxTransfer" runat="server" Width="80px" style="text-align: right;" Enabled="false" BackColor="#CCFFCC" >0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label93" runat="server" Text="Transfer of Target Month"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px; width:120px;">
                        <asp:Label ID="Label94" runat="server" Text="Hand Over"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxHandOver2" runat="server" Width="80px" style="text-align: right;" Enabled="false" BackColor="#CCFFCC" >0</asp:TextBox>
                    </td>
                    <td style="width:300px;">
                        <asp:Label ID="Label95" runat="server" Text="Handover of Target Month"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left:20px; margin-top:10px; font-size:small;">
            <asp:Label ID="Label103" runat="server" Text="Exemptions" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br />
            <br />
                <asp:Button ID="ButtonGetUserData" runat="server" Text="Get from User Data" OnClick="ButtonGetUserData_Click"/>
            <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse; margin-top:5px; margin-left:5px; margin-bottom:5px;">
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label4" runat="server" Text="Year's Salary(fixed)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount1" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label_Desc1" runat="server" Text="Fixed salary in this year until last month."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label5" runat="server" Text="Year's Salary(expectation)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount2" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label_Desc2" runat="server" Text="Expected salary in this year include this month(free input)."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label6" runat="server" Text="Income from employment"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount3" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="40% but not exceeding 60,000 baht"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label10" runat="server" Text="Personal allowance"></asp:Label>
                    </td>
                    <td style="text-align:right;">
                        <asp:Label ID="Label12" runat="server" Text="30,000" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="For the taxpayer"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label13" runat="server" Text="Spouse allowance"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount4" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox_Amount" runat="server" Text="Have a spouse who don't have income" OnCheckedChanged="ChangedAmountExemption" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label14" runat="server" Text="Child allowance"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount5" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList_Amount1" runat="server" OnSelectedIndexChanged="ChangedAmountExemption" AutoPostBack="True">
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label15" runat="server" Text="Limited to three children"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label16" runat="server" Text="Education allowance"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount6" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList_Amount2" runat="server" OnSelectedIndexChanged="ChangedAmountExemption" AutoPostBack="True">
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label17" runat="server" Text="Additional allowance for child studying in educational institution in Thailand"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label18" runat="server" Text="Parents allowance"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount7" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList_Amount3" runat="server" OnSelectedIndexChanged="ChangedAmountExemption" AutoPostBack="True">
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label19" runat="server" Text="30,000 baht for each of taxpayer’s and spouse’s parents if such parent is above 60 years old and earns less than 30,000 baht"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label20" runat="server" Text="Life insurance premium"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount8" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Amount actually paid but not exceeding 100,000 baht each(paid by taxpayer or spouse)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label22" runat="server" Text="Approved provident fund contributions"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount9" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Amount actually paid at the rate not more than 15% of wage, but not exceeding 500,000 baht(paid by taxpayer or spouse)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label24" runat="server" Text="Long term equity fund"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount10" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label25" runat="server" Text="Amount actually paid at the rate not more than 15% of wage, but not exceeding 500,000 baht"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label26" runat="server" Text="Home mortgage interest"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount11" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label27" runat="server" Text="Amount actually paid but not exceeding 100,000 baht"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label53" runat="server" Text="Social insurance contributions"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount12" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label54" runat="server" Text="Amount actually paid each(paid by taxpayer or spouse)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label28" runat="server" Text="Charitable contributions"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount13" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label29" runat="server" Text="Amount actually donated but not exceeding 10% of the income after standard deductions and the above allowances"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label30" runat="server" Text="Taxable Income"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount14" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px">
                        <asp:Label ID="Label32" runat="server" Text="Tax rates of the Personal Income Tax"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label33" runat="server" Text="0%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount15" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label34" runat="server" Text="0-150,000(Exempt)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label35" runat="server" Text="5%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount16" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label36" runat="server" Text="more than 150,000 but less than 300,000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label37" runat="server" Text="10%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount17" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label38" runat="server" Text="more than 300,000 but less than 500,000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label39" runat="server" Text="15%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount18" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label40" runat="server" Text="more than 500,000 but less than 750,000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label41" runat="server" Text="20%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount19" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label42" runat="server" Text="more than 750,000 but less than 1,000,000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label43" runat="server" Text="25%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount20" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label44" runat="server" Text="more than 1,000,000 but less than 2,000,000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label45" runat="server" Text="30%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount21" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label46" runat="server" Text="more than 2,000,000 but less than 4,000,000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCCCCC; height:30px">
                        <asp:Label ID="Label47" runat="server" Text="35%"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount22" runat="server" Width="80px" Enabled="false" style="text-align: right;" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label48" runat="server" Text="Over 4,000,000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFCCFF; height:30px">
                        <asp:Label ID="Label49" runat="server" Text="Personal Income Tax(Year)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount23" runat="server" Width="80px" Enabled="false" style="text-align: right;" BackColor="#CCFFCC" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label50" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFCCFF; height:30px">
                        <asp:Label ID="Label51" runat="server" Text="Personal Income Tax(Month)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Amount24" runat="server" Width="80px" Enabled="false" style="text-align: right;" BackColor="#CCFFCC" OnTextChanged="ChangedAmountExemption" AutoPostBack="True">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label52" runat="server" Text="Year/12"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
