<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salary_SpecialAllowance.aspx.cs" Inherits="BrightHRSystem.Salary_SpecialAllowance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
</head>
<body>
    <form id="form1" runat="server" style="font-size:small">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Allowance Detail Input" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            <div style="margin-top:20px; margin-left:5px; margin-bottom:5px;">
                <asp:Label ID="Label3" runat="server" Text="Fix Allowance" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Get from last month" OnClick="Button1_Click" />
                <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse;">
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label2" runat="server" Text="ฐานเงินเดือน  Basic"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label8" runat="server" Text="ค่าเช่าบ้าน    House"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label18" runat="server" Text="อื่นๆ             Other"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:20px; margin-left:5px; margin-bottom:5px;">
                <asp:Label ID="Label5" runat="server" Text="Days Allowance" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse;">
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label13" runat="server" Text="ค่าเดินทาง     Travel"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:DropDownList ID="DDDays4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDays4_SelectedIndexChanged">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList>
                            days
                            <asp:Button ID="Button7" runat="server" Text="Get Working Record Data" OnClick="ButtonGet4_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label4" runat="server" Text="ค่าอาหาร       Food"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:DropDownList ID="DDDays1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDays1_SelectedIndexChanged">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList>
                            days
                            <asp:Button ID="Button2" runat="server" Text="Get Working Record Data" OnClick="ButtonGet1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label10" runat="server" Text="ค่าสภาพงาน Environment"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:DropDownList ID="DDDays2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDays2_SelectedIndexChanged">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList>
                            days
                            <asp:Button ID="Button5" runat="server" Text="Get Working Record Data" OnClick="ButtonGet2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label12" runat="server" Text="ค่ากะ            Night"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:DropDownList ID="DDDays3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDays3_SelectedIndexChanged">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList>
                            days
                            <asp:Button ID="Button6" runat="server" Text="Get Working Record Data" OnClick="ButtonGet3_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:20px; margin-left:5px; margin-bottom:5px;">
                <asp:Label ID="Label7" runat="server" Text="Attendance Allowance" Font-Bold="True" Font-Size="Large"></asp:Label>
                <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse;">
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label14" runat="server" Text="เบี้ยขยัน        Complete"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" Width="80px" BackColor="#FFCCFF" style="text-align: right;" AutoPostBack="True" OnTextChanged="ChangeAmt">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                            <asp:DropDownList ID="DDGrade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDGrade_SelectedIndexChanged">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                            </asp:DropDownList>
                            grade
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:20px; margin-left:5px; margin-bottom:5px;">
                <asp:Label ID="Label9" runat="server" Text="Total" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
                <table border="1" cellspacing="0" cellpadding="3" style="border-collapse: collapse;">
                    <tr>
                        <td style="background-color: #CCCCCC; height:30px; width:160px;">
                            <asp:Label ID="Label11" runat="server" Text="Total"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox9" runat="server" Width="80px" BackColor="#CCCCCC" style="text-align: right;" Font-Bold="True" ForeColor="Red" ReadOnly="True">0</asp:TextBox>
                        </td>
                        <td style="width:300px;">
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:20px; margin-left:5px; margin-bottom:5px; float:left;">
                <asp:Button ID="Button3" runat="server" Text="Close" Font-Size="Large" Height="35px" Width="125px" 
                    OnClientClick="window.close()"/>
            </div>
            <div style="margin-top:20px; margin-right:20px; margin-bottom:5px; float:right;">
                <asp:Button ID="Button4" runat="server" Text="Update" Font-Size="Large" Height="35px" Width="125px" OnClick="Button4_Click" 
                    OnClientClick="window.close()"/>
            </div>
        </div>
    </form>
</body>
</html>
