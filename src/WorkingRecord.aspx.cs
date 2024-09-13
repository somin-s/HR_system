using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using ComFunction;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BrightHRSystem
{
    public partial class WorkingRecord : System.Web.UI.Page
    {
        protected static int i_startday = 0;
        protected static ArrayList s_Gridview1SeqID;
        protected static bool b_printable = false;
        protected static bool b_editOwn = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                    HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                    HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");
                    ComFunc.Language("WorkingRecord.aspx", form1);

                    PrintButton.OnClientClick = ComFunc.getMessage("C001");

                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                        x.ID == ComFunc.UseSession(Page, "user_id")
                        );
                    if ('1' != tb_u.AUTH3 &&
                        "N" == ComFunc.getSetting("AllowEditOwn"))
                    {
                        b_editOwn = false;
                    }

                    i_startday = ComFunc.ConvertInt(ComFunc.getSetting("StartDay"));

                    setThisMonth();

                    Session["header_id"] = "";
                    Session["separate_time"] = "0";
                    if ("15" == ComFunc.getSetting("SeparateTime"))
                    {
                        Session["separate_time"] = "1";
                    }

                    DropDownListPIC.DataBind();
                    DropDownListPIC.SelectedIndex = 0;
                    for (int i = 0; i < DropDownListPIC.Items.Count; i++)
                    { // check the index of dropdown list.
                        if (ComFunc.UseSession(Page, "user_name") == DropDownListPIC.Items[i].Value.Trim())
                        {
                            DropDownListPIC.SelectedIndex = i;
                            break;
                        }
                    }

                    // for YN2.
                    if ("Y001" == ComFunc.UseSession(Page, "cus_cd"))
                    {
                        YN2_1.Visible = true;
                    }

                    if ("1" == ComFunc.getSetting("ImportData"))
                    {
                        GridView1.Columns[5].HeaderText = ComFunc.getSetting("ImportDataName") + " Start";
                        GridView1.Columns[6].HeaderText = ComFunc.getSetting("ImportDataName") + " Finish";
                    }
                    else
                    {
                        GridView1.Columns[5].Visible = false;
                        GridView1.Columns[6].Visible = false;
                    }

                    UpdateRecorde();
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1001";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void UpdateRecorde()
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                b_printable = true;
                UserCode.Text = ComFunc.Get_UserID(DropDownListPIC.SelectedValue);
                Session["selected_id"] = ComFunc.Get_UserID(DropDownListPIC.SelectedValue);
                TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                    x.ID == ComFunc.UseSession(Page, "user_id")
                    );
                bool b_edit = false;

                if ('1' == tb_u.AUTH3 &&
                    "Y" == ComFunc.getSetting("AllowEditOthers"))
                {
                    b_edit = true;
                }
                if (ComFunc.UseSession(Page, "user_id") == UserCode.Text)
                {
                    b_edit = true;
                }
                if (true == b_edit)
                {
                    ButtonGenerate.Enabled = true;
                    GridView1.Enabled = true;
                    TextBoxPlace.Enabled = true;
                }
                else
                {
                    ButtonGenerate.Enabled = false;
                    GridView1.Enabled = false;
                    TextBoxPlace.Enabled = false;
                }

                TB_R_WORKINGREPORT_H tb;
                if ("Last" == ComFunc.getSetting("TargetMonth"))
                {
                    Session["year"] = LabelYear.Text;
                    Session["month"] = LabelMonth.Text;
                    double d_year = ComFunc.ConvertDouble(LabelYear.Text);
                    double d_month = ComFunc.ConvertDouble(LabelMonth.Text);
                    if (12 == d_month)
                    {
                        d_year += 1;
                        d_month = 1;
                    }
                    else
                    {
                        d_month += 1;
                    }
                    Session["year2"] = d_year.ToString();
                    Session["month2"] = d_month.ToString();

                    tb = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == ComFunc.Get_UserID(DropDownListPIC.SelectedValue) &&
                        x.YEAH == ComFunc.UseSession(Page, "year") &&
                        x.MONTH == ComFunc.UseSession(Page, "month")
                        );
                }
                else
                {
                    Session["year2"] = LabelYear.Text;
                    Session["month2"] = LabelMonth.Text;
                    double d_year = ComFunc.ConvertDouble(LabelYear.Text);
                    double d_month = ComFunc.ConvertDouble(LabelMonth.Text);
                    if (1 == d_month)
                    {
                        d_year -= 1;
                        d_month = 12;
                    }
                    else
                    {
                        d_month -= 1;
                    }
                    Session["year"] = d_year.ToString();
                    Session["month"] = d_month.ToString();

                    tb = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == ComFunc.Get_UserID(DropDownListPIC.SelectedValue) &&
                        x.YEAH == ComFunc.UseSession(Page, "year2") &&
                        x.MONTH == ComFunc.UseSession(Page, "month2")
                        );
                }

                if (null == tb)
                {
                    WorkingData.Visible = false;
                    NoData.Visible = true;
                    ButtonGenerate.Visible = true;
                    TextBoxPlace.Text = "";
                }
                else
                {
                    string seqID = tb.seqID.ToString();
                    Session["header_id"] = seqID;
                    TextBoxPlace.Text = tb.WORKING_PLACE;
                    TextBoxPlace.Text = TextBoxPlace.Text.Trim();
                    WorkingData.Visible = true;
                    NoData.Visible = false;
                    ButtonGenerate.Visible = false;

                    s_Gridview1SeqID = new ArrayList();
                    var tb_wd = from x in db.TB_R_WORKINGREPORT_Ds where x.HEADER_ID == tb.seqID.ToString() select x;
                    foreach (var x in tb_wd)
                    {
                        s_Gridview1SeqID.Add(x.seqID);
                        if ("" != ComFunc.ConvertStr(x.STATUS) && "Approved" != ComFunc.ConvertStr(x.STATUS))
                        {
                            b_printable = false;
                        }
                    }
                }

                updateTotal();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1002";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string s_day = "";
                if ("day" == e.Row.Cells[1].Text)
                {
                    return;
                }

                if (i_startday <= ComFunc.ConvertDouble(e.Row.Cells[1].Text))
                {
                    s_day = ComFunc.UseSession(Page, "year") + "-" + ComFunc.UseSession(Page, "month") + "-" + e.Row.Cells[1].Text;
                    e.Row.Cells[1].Text = ComFunc.UseSession(Page, "month") + "/" + e.Row.Cells[1].Text;
                }
                else
                {
                    s_day = ComFunc.UseSession(Page, "year2") + "-" + ComFunc.UseSession(Page, "month2") + "-" + e.Row.Cells[1].Text;
                    e.Row.Cells[1].Text = ComFunc.UseSession(Page, "month2") + "/" + e.Row.Cells[1].Text;
                }
                DateTime? dt = ComFunc.ConvertDate(s_day);

                DataClassesDataContext db = new DataClassesDataContext();
                TB_M_HOLIDAY tb = db.TB_M_HOLIDAYs.SingleOrDefault(x =>
                    x.DATE_HOLIDAY == dt
                    );

                if (null != tb)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.LightPink;
                    }
                }

                // for YN2.
                if ("Y001" == ComFunc.UseSession(Page, "cus_cd"))
                {
                    if ("Sat" == e.Row.Cells[2].Text)
                    {
                        for (int i = 0; i < e.Row.Cells.Count - 1; i++)
                        {
                            e.Row.Cells[i].BackColor = Color.SkyBlue;
                        }
                    }
                }

                if ("Nonacceptance" != e.Row.Cells[19].Text.Trim())
                {
                    e.Row.Cells[20].Text = "";
                }

                if (false == b_editOwn)
                {
                    DropDownList d1 = (DropDownList)e.Row.Cells[3].FindControl("DropDownList4");
                    if (null != d1)
                    {
                        d1.Enabled = false;
                    }
                    DropDownList d2 = (DropDownList)e.Row.Cells[4].FindControl("DropDownList5");
                    if (null != d2)
                    {
                        d2.Enabled = false;
                    }
                    DropDownList d3 = (DropDownList)e.Row.Cells[10].FindControl("DropDownList1");
                    if (null != d3)
                    {
                        d3.Enabled = false;
                    }
                    DropDownList d4 = (DropDownList)e.Row.Cells[12].FindControl("DropDownList6");
                    if (null != d4)
                    {
                        d4.Enabled = false;
                    }
                    DropDownList d5 = (DropDownList)e.Row.Cells[13].FindControl("DropDownList7");
                    if (null != d5)
                    {
                        d5.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1003";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected int CountNormalDay()
        {
            int i_Normal = 0;
            int i_Holiday = 0;
            string s_day = "";

            try
            {
                foreach (GridViewRow Row in GridView1.Rows)
                {
                    string[] s_Array = Row.Cells[1].Text.Split('/');
                    if (1 < s_Array.Length)
                    {
                        if (i_startday <= ComFunc.ConvertDouble(s_Array[1]))
                        {
                            s_day = ComFunc.UseSession(Page, "year") + "/" + Row.Cells[1].Text;
                        }
                        else
                        {
                            s_day = ComFunc.UseSession(Page, "year2") + "/" + Row.Cells[1].Text;
                        }

                        DateTime? dt = ComFunc.ConvertDate(s_day);

                        DataClassesDataContext db = new DataClassesDataContext();
                        TB_M_HOLIDAY tb = db.TB_M_HOLIDAYs.SingleOrDefault(x =>
                            x.DATE_HOLIDAY == dt
                            );

                        if (null != tb)
                        {
                            i_Holiday++;
                        }
                        else
                        {
                            i_Normal++;
                        }
                    }
                }

                return (i_Normal);
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1004";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return 0;
            }
        }

        protected void ButtonGenerate_Click(object sender, EventArgs e)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TB_R_WORKINGREPORT_H tb_wh1;
            TB_R_WORKINGREPORT_H tb_wh2;
            TB_R_WORKINGREPORT_H tb_wh3;

            try
            {
                if ("Last" == ComFunc.getSetting("TargetMonth"))
                {
                    // check exist data.
                    tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                        x.YEAH == ComFunc.UseSession(Page, "year") &&
                        x.MONTH == ComFunc.UseSession(Page, "month")
                        );
                    if (null != tb_wh1)
                    {
                        return;
                    }

                    // create new data on header.
                    tb_wh2 = new TB_R_WORKINGREPORT_H
                    {
                        USER_ID = ComFunc.UseSession(Page, "selected_id"),
                        YEAH = ComFunc.UseSession(Page, "year"),
                        MONTH = ComFunc.UseSession(Page, "month"),
                        WORKING_PLACE = ComFunc.getSetting("DefaultWorkPlace")
                    };
                    db.TB_R_WORKINGREPORT_Hs.InsertOnSubmit(tb_wh2);
                    db.SubmitChanges();
                    TextBoxPlace.Text = ComFunc.getSetting("DefaultWorkPlace");

                    // create new data on detail.
                    tb_wh3 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                        x.YEAH == ComFunc.UseSession(Page, "year") &&
                        x.MONTH == ComFunc.UseSession(Page, "month")
                        );
                }
                else
                {
                    // check exist data.
                    tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                        x.YEAH == ComFunc.UseSession(Page, "year2") &&
                        x.MONTH == ComFunc.UseSession(Page, "month2")
                        );
                    if (null != tb_wh1)
                    {
                        return;
                    }

                    // create new data on header.
                    tb_wh2 = new TB_R_WORKINGREPORT_H
                    {
                        USER_ID = ComFunc.UseSession(Page, "selected_id"),
                        YEAH = ComFunc.UseSession(Page, "year2"),
                        MONTH = ComFunc.UseSession(Page, "month2"),
                        WORKING_PLACE = ComFunc.getSetting("DefaultWorkPlace")
                    };
                    db.TB_R_WORKINGREPORT_Hs.InsertOnSubmit(tb_wh2);
                    db.SubmitChanges();
                    TextBoxPlace.Text = ComFunc.getSetting("DefaultWorkPlace");

                    // create new data on detail.
                    tb_wh3 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                        x.YEAH == ComFunc.UseSession(Page, "year2") &&
                        x.MONTH == ComFunc.UseSession(Page, "month2")
                        );
                }

                string seqID = "";
                if (null != tb_wh3)
                {
                    seqID = tb_wh3.seqID.ToString();
                    TB_R_WORKINGREPORT_D tb_wd1;
                    TB_R_WORKINGREPORT_D tb_wd2;

                    for (int i = i_startday; i < DateTime.DaysInMonth(int.Parse(ComFunc.UseSession(Page, "year")), int.Parse(ComFunc.UseSession(Page, "month"))) + 1; i++)
                    {
                        tb_wd1 = new TB_R_WORKINGREPORT_D
                        {
                            HEADER_ID = seqID,
                            DAY = i,
                            WEEK = Convert.ToDateTime(
                                        ComFunc.UseSession(Page, "year") + "/" +
                                        ComFunc.UseSession(Page, "month") + "/" +
                                        i.ToString()).DayOfWeek.ToString().Substring(0, 3)
                        };
                        db.TB_R_WORKINGREPORT_Ds.InsertOnSubmit(tb_wd1);
                        db.SubmitChanges();
                    }
                    for (int i = 1; i < i_startday; i++)
                    {
                        tb_wd2 = new TB_R_WORKINGREPORT_D
                        {
                            HEADER_ID = seqID,
                            DAY = i,
                            WEEK = Convert.ToDateTime(
                                        ComFunc.UseSession(Page, "year2") + "/" +
                                        ComFunc.UseSession(Page, "month2") + "/" +
                                        i.ToString()).DayOfWeek.ToString().Substring(0, 3)
                        };
                        db.TB_R_WORKINGREPORT_Ds.InsertOnSubmit(tb_wd2);
                        db.SubmitChanges();
                    }
                    UpdateRecorde();
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1005";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int NewYear = int.Parse(LabelYear.Text);
                int NewMonth = int.Parse(LabelMonth.Text) - 1;
                if (0 == NewMonth)
                {
                    NewYear = NewYear - 1;
                    NewMonth = 12;
                }
                LabelYear.Text = NewYear.ToString();
                LabelMonth.Text = NewMonth.ToString();
                UpdateRecorde();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1006";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                int NewYear = int.Parse(LabelYear.Text);
                int NewMonth = int.Parse(LabelMonth.Text) + 1;
                if (13 == NewMonth)
                {
                    NewYear = NewYear + 1;
                    NewMonth = 1;
                }
                LabelYear.Text = NewYear.ToString();
                LabelMonth.Text = NewMonth.ToString();
                UpdateRecorde();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1007";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void setThisMonth()
        {
            try
            {
                int i_Year = 0;
                int i_Month = 0;
                i_Year = DateTime.Now.Year;
                i_Month = DateTime.Now.Month;
                if ("Last" == ComFunc.getSetting("TargetMonth"))
                {
                    if (i_startday > DateTime.Now.Day)
                    {
                        if (1 == i_Month)
                        {
                            i_Year -= 1;
                            i_Month = 12;
                        }
                        else
                        {
                            i_Month -= 1;
                        }
                    }
                }
                else
                {
                    if (i_startday <= DateTime.Now.Day)
                    {
                        if (12 == i_Month)
                        {
                            i_Year += 1;
                            i_Month = 1;
                        }
                        else
                        {
                            i_Month += 1;
                        }
                    }
                }
                LabelYear.Text = i_Year.ToString();
                LabelMonth.Text = i_Month.ToString();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1008";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            setThisMonth();
            UpdateRecorde();
        }

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            string message = "";

            if (true == NoData.Visible)
            {
                message = @"Please generate a report first.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else if (false == b_printable)
            {
                message = ComFunc.getMessage("M001");
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                UpdateRecorde();
            }
            else
            {
                // generate and download working record report.
                Download_Report(Generate_Report());
            }
        }

        protected string Get_TemplateValue(GridViewRow row, int i_cell, string s_id1, string s_id2)
        {
            try
            {
                string s_Return = "";
                Label l1 = (Label)row.Cells[i_cell].FindControl(s_id1);
                DropDownList d1 = (DropDownList)row.Cells[i_cell].FindControl(s_id2);
                if (null != l1)
                {
                    s_Return = ComFunc.ConvertFromGridview(l1.Text);
                }
                else if (null != d1)
                {
                    s_Return = d1.SelectedValue;
                }
                return s_Return;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1010";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        private ExcelCreator.XlsCreator xlsCreator1 = new ExcelCreator.XlsCreator();

        protected string Generate_Report()
        {
            string s_tmpWR = ComFunc.getSetting("ReportName");
            string TmplateFilePath = ComFunc.getSetting("ReportPath") + s_tmpWR;
            string sDateTime = System.DateTime.Now.ToString();
            sDateTime = sDateTime.Replace("/", "");
            sDateTime = sDateTime.Replace(":", "");
            sDateTime = sDateTime.Replace(" ", "");
            string sFilePath = ComFunc.getSetting("TempPath") + "WR_" + sDateTime + ".xls";

            try
            {
                // copy template to temporary folder.
                File.Copy(TmplateFilePath, sFilePath);
                xlsCreator1.OpenBook(sFilePath, "");

                // header portion.
                xlsCreator1.Pos(4, 1).Value = ComFunc.ConvertDouble(LabelYear.Text);
                xlsCreator1.Pos(1, 2).Value = ComFunc.ConvertDouble(LabelMonth.Text);
                //xlsCreator1.Pos(3, 3).Value = ComFunc.ConvertDouble(Label_Real.Text);
                xlsCreator1.Pos(3, 3).Value = CountNormalDay();
                xlsCreator1.Pos(10, 2).Value = TextBoxPlace.Text;
                xlsCreator1.Pos(16, 1).Value = UserCode.Text;
                xlsCreator1.Pos(16, 2).Value = DropDownListPIC.SelectedValue;

                // body portion.
                int RowCount = 7;

                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Date, week.
                    if ("" != ComFunc.ConvertFromGridview(row.Cells[1].Text))
                    {
                        xlsCreator1.Pos(1, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[1].Text);
                        xlsCreator1.Pos(2, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[2].Text);
                    }

                    // Start Finish.
                    xlsCreator1.Pos(3, RowCount).Str = Get_TemplateValue(row, 3, "Label5", "DropDownList4");
                    xlsCreator1.Pos(4, RowCount).Str = Get_TemplateValue(row, 4, "Label6", "DropDownList5");

                    // ATT.
                    xlsCreator1.Pos(5, RowCount).Str = Get_TemplateValue(row, 7, "Label3", "DropDownList2");
                    xlsCreator1.Pos(6, RowCount).Str = Get_TemplateValue(row, 8, "Label4", "DropDownList3");

                    // Another Working Place.
                    xlsCreator1.Pos(7, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[9].Text);

                    // Fixed Time.
                    xlsCreator1.Pos(11, RowCount).Str = Get_TemplateValue(row, 10, "Label2", "DropDownList1");
                    xlsCreator1.Pos(12, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[11].Text);

                    // Over Work Time.
                    xlsCreator1.Pos(13, RowCount).Str = Get_TemplateValue(row, 12, "Label7", "DropDownList6");
                    xlsCreator1.Pos(14, RowCount).Str = Get_TemplateValue(row, 13, "Label8", "DropDownList7");
                    xlsCreator1.Pos(15, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[14].Text);
                    xlsCreator1.Pos(16, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[15].Text);
                    xlsCreator1.Pos(17, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[16].Text);

                    // Total.
                    xlsCreator1.Pos(18, RowCount).Str = ComFunc.ConvertFromGridview(row.Cells[17].Text);

                    // color
                    // for YN2.
                    if ("Y001" == ComFunc.UseSession(Page, "cus_cd"))
                    {

                        if ("Sat" == ComFunc.ConvertFromGridview(row.Cells[2].Text))
                        {
                            for (int i = 1; i < 20; i++)
                            {
                                xlsCreator1.Pos(i, RowCount).Attr.BackColor = (ExcelCreator.xlColor)27;
                            }
                        }
                    }

                    if (Color.LightPink == row.Cells[2].BackColor)
                    {
                        for (int i = 1; i < 20; i++)
                        {
                            xlsCreator1.Pos(i, RowCount).Attr.BackColor = (ExcelCreator.xlColor)45;
                        }
                    }
                    RowCount++;
                }

                // footer portion.
                xlsCreator1.Pos(3, 40).Value = ComFunc.ConvertDouble(Label_Paid.Text);
                xlsCreator1.Pos(7, 40).Value = ComFunc.ConvertDouble(Label_Absence.Text);
                xlsCreator1.Pos(11, 40).Value = ComFunc.ConvertDouble(Label_Holiday.Text);
                xlsCreator1.Pos(15, 40).Value = ComFunc.ConvertDouble(Label_Real.Text);
                xlsCreator1.Pos(3, 42).Str = Label_NormalOT.Text;
                xlsCreator1.Pos(7, 42).Str = Label_HolidayWork.Text;
                xlsCreator1.Pos(11, 42).Str = Label_HolidayOT.Text;
                xlsCreator1.Pos(15, 42).Str = Label_TotalTime.Text;
                xlsCreator1.Pos(15, 38).Str = Label_NormalOT.Text;
                xlsCreator1.Pos(16, 38).Str = Label_HolidayWork.Text;
                xlsCreator1.Pos(17, 38).Str = Label_HolidayOT.Text;
                xlsCreator1.Pos(18, 38).Str = Label_TotalTime.Text;

                // for YN2.
                if ("Y001" == ComFunc.UseSession(Page, "cus_cd"))
                {
                    xlsCreator1.Pos(3, 44).Str = LabelSatExp.Text;
                    xlsCreator1.Pos(7, 44).Str = LabelSatAct.Text;
                    xlsCreator1.Pos(11, 44).Str = LabelSatDeff.Text;
                    xlsCreator1.Pos(15, 44).Str = LabelOtCalc.Text;
                }

                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1011";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        protected void Download_Report(string sFilePath)
        {
            string filename = "WR" + "_" + UserCode.Text.Trim() + "_" + LabelYear.Text.Trim() + LabelMonth.Text.Trim() + ".xls";

            Response.ClearContent();
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(filename)));
            Response.ContentType = "application/msexcel";
            Response.WriteFile(sFilePath);
            Response.End();
        }

        protected void DropDownListPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRecorde();
        }

        protected void updateTotal()
        {
            try
            {
                double d_Paid = 0;
                double d_Absence = 0;
                double d_Holiday = 0;
                double d_Real = 0;
                double d_SatExp = 0;
                double d_SatAct = 0;
                string s_Start = "";
                string s_ATT = "";
                string s_ATT2 = "";

                GridView1.DataBind();
                foreach (GridViewRow row in GridView1.Rows)
                {
                    s_Start = Get_TemplateValue(row, 3, "Label5", "DropDownList4");
                    s_ATT = Get_TemplateValue(row, 7, "Label3", "DropDownList2");
                    s_ATT2 = Get_TemplateValue(row, 8, "Label4", "DropDownList3");

                    // Date, week.
                    if ("" != s_Start)
                    {
                        if (("HFPD" == s_ATT) || ("HFAB" == s_ATT))
                        {
                            d_Real = d_Real + 0.5;
                        }
                        else
                        {
                            d_Real = d_Real + 1;
                        }
                    }
                    switch (s_ATT)
                    {
                        case "PAID":
                            d_Paid = d_Paid + 1;
                            break;
                        case "HFPD":
                            d_Paid = d_Paid + 0.5;
                            break;
                        case "ABSN":
                            d_Absence = d_Absence + 1;
                            break;
                        case "HFAB":
                            d_Absence = d_Absence + 0.5;
                            break;
                        case "HLWK":
                            d_Holiday = d_Holiday + 1;
                            break;
                        default:
                            break;
                    }
                    switch (s_ATT2)
                    {
                        case "PAID":
                            d_Paid = d_Paid + 1;
                            break;
                        case "HFPD":
                            d_Paid = d_Paid + 0.5;
                            break;
                        case "ABSN":
                            d_Absence = d_Absence + 1;
                            break;
                        case "HFAB":
                            d_Absence = d_Absence + 0.5;
                            break;
                        case "HLWK":
                            d_Holiday = d_Holiday + 1;
                            break;
                        default:
                            break;
                    }
                    if ("Sat" == row.Cells[2].Text)
                    {
                        d_SatExp += 8;
                        d_SatAct += ComFunc.ConvertDoubleFromTime(row.Cells[11].Text, true);
                    }
                }
                Label_Paid.Text = d_Paid.ToString();
                Label_Absence.Text = d_Absence.ToString();
                Label_Holiday.Text = d_Holiday.ToString();
                Label_Real.Text = d_Real.ToString();
                LabelSatExp.Text = ComFunc.ConvertDoubletoTime(d_SatExp);
                LabelSatAct.Text = ComFunc.ConvertDoubletoTime(d_SatAct);
                LabelSatDeff.Text = ComFunc.ConvertDoubletoTime(d_SatExp - d_SatAct);
                double d_NormalOT = 0;
                double d_HolidayWork = 0;
                double d_HolidayOT = 0;
                double d_Total = 0;

                // get total from table.
                DataClassesDataContext db = new DataClassesDataContext();
                var tb = from a in db.TB_R_WORKINGREPORT_Ds
                         from b in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WORKING_H_OT).DefaultIfEmpty()
                         from c in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WEEKEND_H_NORMAL).DefaultIfEmpty()
                         from d in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WEEKEND_H_OT).DefaultIfEmpty()
                         from e in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.TOTAL).DefaultIfEmpty()
                         where a.HEADER_ID == ComFunc.UseSession(Page, "header_id")
                         select new
                         {
                             t1 = b.seqID == null ? 0 : b.seqID,
                             t2 = c.seqID == null ? 0 : c.seqID,
                             t3 = d.seqID == null ? 0 : d.seqID,
                             t4 = e.seqID == null ? 0 : e.seqID,
                         };
                foreach (var row in tb)
                {
                    d_NormalOT += row.t1;
                    d_HolidayWork += row.t2;
                    d_HolidayOT += row.t3;
                    d_Total += row.t4;
                }

                if (0 != d_NormalOT)
                {
                    d_NormalOT = d_NormalOT * 0.25;
                }
                if (0 != d_HolidayWork)
                {
                    d_HolidayWork = d_HolidayWork * 0.25;
                }
                if (0 != d_HolidayOT)
                {
                    d_HolidayOT = d_HolidayOT * 0.25;
                }
                if (0 != d_Total)
                {
                    d_Total = d_Total * 0.25;
                }

                Label_NormalOT.Text = ComFunc.ConvertDoubletoTime(d_NormalOT);
                Label_HolidayWork.Text = ComFunc.ConvertDoubletoTime(d_HolidayWork);
                Label_HolidayOT.Text = ComFunc.ConvertDoubletoTime(d_HolidayOT);
                Label_TotalTime.Text = ComFunc.ConvertDoubletoTime(d_Total);

                if (0 > (d_NormalOT - (d_SatExp - d_SatAct)))
                {
                    LabelOtCalc.Text = "00:00";
                }
                else
                {
                    LabelOtCalc.Text = ComFunc.ConvertDoubletoTime(d_NormalOT - (d_SatExp - d_SatAct));
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1013";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            string message = "";

            try
            {
                if (e.Exception != null)
                {
                    message = @"invalid Value.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                    // make the error handled so it does not take over the whole page
                    e.ExceptionHandled = true;
                    return;
                }
                else
                {
                    string s_seqID = s_Gridview1SeqID[GridView1.EditIndex].ToString();

                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_R_WORKINGREPORT_D tb_wd = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                        x.seqID == ComFunc.ConvertInt(s_seqID)
                        );
                    if (null != tb_wd)
                    {
                        if ("Y" == ComFunc.getSetting("CheckRemainingDays"))
                        {
                            TB_R_WORKINGREPORT_H tb_wh = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                                x.seqID == ComFunc.ConvertInt(tb_wd.HEADER_ID)
                                );
                            string s_Year = "";
                            if ("Last" == ComFunc.getSetting("TargetMonth"))
                            {
                                s_Year = ComFunc.UseSession(Page, "year");
                            }
                            else
                            {
                                s_Year = ComFunc.UseSession(Page, "year2");
                            }
                            TB_R_SALARY tb_paid = db.TB_R_SALARies.SingleOrDefault(x =>
                                x.ID == tb_wh.USER_ID &&
                                x.DESC == "Paid Holiday" &&
                                x.REMARK == s_Year
                                );
                            TB_R_SALARY tb_spec = db.TB_R_SALARies.SingleOrDefault(x =>
                                x.ID == tb_wh.USER_ID &&
                                x.DESC == "Special Holiday" &&
                                x.REMARK == s_Year
                                );
                            double d_PDAmt = 0;
                            double d_SPAmt = 0;
                            if (null != tb_paid)
                            {
                                d_PDAmt = ComFunc.ConvertDouble(tb_paid.AMOUNT);
                            }
                            if (null != tb_spec)
                            {
                                d_SPAmt = ComFunc.ConvertDouble(tb_spec.AMOUNT);
                            }
                            double d_PDAct = 0;
                            double d_SPAct = 0;

                            for (int i = 0; i < 12; i++)
                            {
                                TB_R_WORKINGREPORT_H tb_whCalc = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                                    x.USER_ID == tb_wh.USER_ID &&
                                    x.YEAH == s_Year &&
                                    x.MONTH == (i + 1).ToString()
                                    );
                                if (null != tb_whCalc)
                                {
                                    var tb_wdCalc = from x in db.TB_R_WORKINGREPORT_Ds where x.HEADER_ID == tb_whCalc.seqID.ToString() select x;
                                    foreach (var x in tb_wdCalc)
                                    {
                                        if (null != x.ATT1)
                                        {
                                            switch (x.ATT1.Trim())
                                            {
                                                case "PAID":
                                                    d_PDAct++;
                                                    break;
                                                case "HFPD":
                                                    d_PDAct = d_PDAct + 0.5;
                                                    break;
                                                case "SPHL":
                                                    d_SPAct++;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        if (null != x.ATT2)
                                        {
                                            switch (x.ATT2.Trim())
                                            {
                                                case "PAID":
                                                    d_PDAct++;
                                                    break;
                                                case "HFPD":
                                                    d_PDAct = d_PDAct + 0.5;
                                                    break;
                                                case "SPHL":
                                                    d_SPAct++;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (d_PDAmt < d_PDAct || d_SPAmt < d_SPAct)
                            {
                                tb_wd.ATT1 = null;
                                tb_wd.ATT2 = null;
                                db.SubmitChanges();
                                UpdateRecorde();

                                message = @"Your remaining amount is not enough.";
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);

                                return;
                            }
                        }

                        double d_Start = ComFunc.ConvertDoubleFromTime(tb_wd.APPLY_STARTING, true);
                        double d_Finish = ComFunc.ConvertDoubleFromTime(tb_wd.APPLY_LEAVING, true);
                        double d_Rest = ComFunc.ConvertDoubleFromTime(tb_wd.REST1, true);
                        double d_OtStart = ComFunc.ConvertDoubleFromTime(tb_wd.OVERTIME_STARTING, true);
                        double d_OtFinish = ComFunc.ConvertDoubleFromTime(tb_wd.OVERTIME_LEAVING, true);

                        if ("SHFT" != tb_wd.ATT2)
                        {
                            double d_DefStart = ComFunc.ConvertDoubleFromTime(ComFunc.getSetting("DefaultStart"), true);
                            if (d_DefStart > d_Start)
                            {
                                d_Start = d_DefStart;
                            }
                            double d_DefFinish = ComFunc.ConvertDoubleFromTime(ComFunc.getSetting("DefaultFinish"), true);
                            if (d_DefFinish < d_Finish)
                            {
                                d_Finish = d_DefFinish;
                            }
                        }

                        string s_normal = "";
                        string s_ot = "";
                        string s_holiday = "";
                        string s_holidayot = "";
                        string s_total = "";
                        string s_status = tb_wd.STATUS;
                        string s_approver1 = "";
                        string s_approver2 = "";
                        string s_approver3 = "";

                        switch (tb_wd.ATT1)
                        {
                            case "HLWK":
                                if (0 != d_Start && 0 != d_Finish)
                                {
                                    s_holiday = ComFunc.ConvertDoubletoTime(d_Finish - d_Start - d_Rest);
                                    s_total = ComFunc.ConvertDoubletoTime((d_Finish - d_Start - d_Rest) + (d_OtFinish - d_OtStart));
                                }
                                if (0 != d_OtStart && 0 != d_OtFinish)
                                {
                                    s_holidayot = ComFunc.ConvertDoubletoTime(d_OtFinish - d_OtStart);
                                }
                                if (null == s_status || "" == s_status)
                                {
                                    s_status = "Nonacceptance";
                                }
                                break;
                            case "PAID":
                            case "ABSN":
                            case "SPHL":
                            case "COMP":
                                if (null == s_status || "" == s_status)
                                {
                                    s_status = "Nonacceptance";
                                }
                                break;
                            case "HFPD":
                            case "HFAB":
                            case "LATE":
                            case "ERLY":
                                if (0 != d_Start && 0 != d_Finish)
                                {
                                    s_normal = ComFunc.ConvertDoubletoTime(d_Finish - d_Start - d_Rest);
                                    s_total = ComFunc.ConvertDoubletoTime((d_Finish - d_Start - d_Rest) + (d_OtFinish - d_OtStart));
                                }
                                if (0 != d_OtStart && 0 != d_OtFinish)
                                {
                                    s_ot = ComFunc.ConvertDoubletoTime(d_OtFinish - d_OtStart);
                                }
                                if (null == s_status || "" == s_status)
                                {
                                    s_status = "Nonacceptance";
                                }
                                break;
                            default:
                                if (0 != d_Start && 0 != d_Finish)
                                {
                                    s_normal = ComFunc.ConvertDoubletoTime(d_Finish - d_Start - d_Rest);
                                    s_total = ComFunc.ConvertDoubletoTime((d_Finish - d_Start - d_Rest) + (d_OtFinish - d_OtStart));
                                }
                                if (0 != d_OtStart && 0 != d_OtFinish)
                                {
                                    s_ot = ComFunc.ConvertDoubletoTime(d_OtFinish - d_OtStart);
                                }
                                s_status = "";
                                break;
                        }

                        switch (tb_wd.ATT2)
                        {
                            case "OT":
                                if (null == s_status || "" == s_status)
                                {
                                    s_status = "Nonacceptance";
                                }
                                break;
                            case "HFPD":
                            case "HFAB":
                                if (null == s_status || "" == s_status)
                                {
                                    s_status = "Nonacceptance";
                                }
                                break;
                            default:
                                break;
                        }

                        TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                            x.ID == ComFunc.Get_UserID(DropDownListPIC.SelectedValue)
                            );
                        if ("" != s_status)
                        {
                            s_approver1 = tb_u.APPROVER1;
                            s_approver2 = tb_u.APPROVER2;
                            s_approver3 = tb_u.APPROVER3;
                        }

                        TB_R_WORKINGREPORT_D tb_wd2 = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                            x.seqID == ComFunc.ConvertInt(s_seqID)
                            );
                        tb_wd2.WORKING_H_NORMAL = s_normal;
                        tb_wd2.WORKING_H_OT = s_ot;
                        tb_wd2.WEEKEND_H_NORMAL = s_holiday;
                        tb_wd2.WEEKEND_H_OT = s_holidayot;
                        tb_wd2.TOTAL = s_total;
                        tb_wd2.STATUS = s_status;
                        tb_wd2.APPROVER1 = s_approver1;
                        tb_wd2.APPROVER2 = s_approver2;
                        tb_wd2.APPROVER3 = s_approver3;
                        tb_wd2.APPROVE_COMMENT1 = "";
                        tb_wd2.APPROVE_COMMENT2 = "";
                        tb_wd2.APPROVE_COMMENT3 = "";
                        tb_wd2.APPROVE_DATE1 = null;
                        tb_wd2.APPROVE_DATE2 = null;
                        tb_wd2.APPROVE_DATE3 = null;
                        db.SubmitChanges();

                        UpdateRecorde();
                    }
                    else
                    {
                        message = @"System can not get work time data.\nPlease contact to Administrator";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1014";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (true == NoData.Visible)
                {
                    string message = @"Please generate a report first.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
                else
                {
                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_R_WORKINGREPORT_H tb = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.seqID == ComFunc.ConvertInt(ComFunc.UseSession(Page, "header_id"))
                        );
                    tb.WORKING_PLACE = TextBoxPlace.Text;
                    db.SubmitChanges();
                    UpdateRecorde();
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1015";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Accept")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    string s_seqID = s_Gridview1SeqID[index].ToString();
                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_R_WORKINGREPORT_D tb_wd = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                        x.seqID == ComFunc.ConvertInt(s_seqID)
                        );
                    tb_wd.STATUS = "Requesting";
                    db.SubmitChanges();

                    ComFunc.sendEmailAcceptance(s_seqID);

                    string message = "System send a request acceptance E-Mail to approver.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
                UpdateRecorde();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1016";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void PrintButton2_Click(object sender, EventArgs e)
        {
            // generate and download working record report.
            Download_Report2(Generate_Report2());
        }

        protected string Generate_Report2()
        {
            string s_tmpReport = "WorkingRecordList.xls";
            string TmplateFilePath = ComFunc.getSetting("ReportPath") + s_tmpReport;
            string sDateTime = System.DateTime.Now.ToString();
            sDateTime = sDateTime.Replace("/", "");
            sDateTime = sDateTime.Replace(":", "");
            sDateTime = sDateTime.Replace(" ", "");
            string sFilePath = ComFunc.getSetting("TempPath") + "WorkingRecordList_" + sDateTime + ".xls";

            try
            {
                // copy template to temporary folder.
                File.Copy(TmplateFilePath, sFilePath);
                xlsCreator1.OpenBook(sFilePath, "");

                DateTime d_target = new DateTime(ComFunc.ConvertInt(LabelYear.Text.Trim()),
                                                    ComFunc.ConvertInt(LabelMonth.Text.Trim()), 1);
                xlsCreator1.Cell("**DATE").Str = d_target.ToString("MMM") + "-" + d_target.ToString("yyyy");
                xlsCreator1.Cell("**CREATEON").Str = DateTime.Now.ToString(ComFunc.getSetting("DateFormat"));

                DataClassesDataContext db = new DataClassesDataContext();
                var tb_d = from x in db.TB_R_USERs orderby x.ID select x;
                int i_row = 6;
                foreach (var row in tb_d)
                {
                    string s_userName = row.NAME;
                    DropDownListPIC.DataBind();
                    for (int i = 0; i < DropDownListPIC.Items.Count; i++)
                    { // check the index of dropdown list.
                        if (s_userName == DropDownListPIC.Items[i].Value.Trim())
                        {
                            DropDownListPIC.SelectedIndex = i;
                            break;
                        }
                    }

                    double d_OfficialDays = 0;
                    double d_ActualDays = 0;
                    double d_OfficialTime = 0;
                    double d_ActualTime = 0;
                    double d_NormalOT = 0;
                    double d_HolidayWork = 0;
                    double d_HolidayOT = 0;
                    double d_PAID = 0;
                    double d_ABSN = 0;
                    double d_LATE = 0;
                    double d_ERLY = 0;
                    double d_HLWK = 0;
                    double d_SPHL = 0;
                    double d_COMP = 0;

                    double d_OfficialDaysTTL = 0;
                    double d_ActualDaysTTL = 0;
                    double d_OfficialTimeTTL = 0;
                    double d_ActualTimeTTL = 0;
                    double d_NormalOTTTL = 0;
                    double d_HolidayWorkTTL = 0;
                    double d_HolidayOTTTL = 0;
                    double d_PAIDTTL = 0;
                    double d_ABSNTTL = 0;
                    double d_LATETTL = 0;
                    double d_ERLYTTL = 0;
                    double d_HLWKTTL = 0;
                    double d_SPHLTTL = 0;
                    double d_COMPTTL = 0;

                    TimeSpan tsTmp = new TimeSpan(0, 0, 0);
                    TimeSpan ts_LateTime = new TimeSpan(0, 0, 0);
                    TimeSpan ts_EarlyTime = new TimeSpan(0, 0, 0);
                    TimeSpan ts_TTLLateTime = new TimeSpan(0, 0, 0);
                    TimeSpan ts_TTLEarlyTime = new TimeSpan(0, 0, 0);
                    DateTime dtTmp, dt_DfStart, dt_DfFinish;
                    dt_DfStart = DateTime.Parse("2000/01/01 " + ComFunc.getSetting("DefaultStart"));
                    dt_DfFinish = DateTime.Parse("2000/01/01 " + ComFunc.getSetting("DefaultFinish"));
                    bool b_ImportLate = false;
                    if ("0" != ComFunc.getSetting("ImportData") &&
                        "Input" != ComFunc.getSetting("CalcTargetLate"))
                    {
                        b_ImportLate = true;
                    }
                    bool b_ImportEarly = false;
                    if ("0" != ComFunc.getSetting("ImportData") &&
                        "Input" != ComFunc.getSetting("CalcTargetEarly"))
                    {
                        b_ImportEarly = true;
                    }

                    if (0 != ComFunc.ConvertDouble(row.ID))
                    {
                        xlsCreator1.Pos(0, i_row).Value = ComFunc.ConvertDouble(row.ID);
                    }
                    else
                    {
                        xlsCreator1.Pos(0, i_row).Str = row.ID;
                    }
                    xlsCreator1.Pos(1, i_row).Str = row.NAME;

                    TB_R_WORKINGREPORT_H tb_wh = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == row.ID &&
                        x.YEAH == d_target.ToString("yyyy") &&
                        x.MONTH == ComFunc.ConvertInt(d_target.ToString("MM")).ToString()
                        );
                    if (null != tb_wh)
                    {
                        // get total from table.
                        var tb = from a in db.TB_R_WORKINGREPORT_Ds
                                    from b in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WORKING_H_OT).DefaultIfEmpty()
                                    from c in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WEEKEND_H_NORMAL).DefaultIfEmpty()
                                    from d in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WEEKEND_H_OT).DefaultIfEmpty()
                                    from f in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.TOTAL).DefaultIfEmpty()
                                    where a.HEADER_ID == tb_wh.seqID.ToString()
                                    select new
                                    {
                                        t1 = b.seqID == null ? 0 : b.seqID,
                                        t2 = c.seqID == null ? 0 : c.seqID,
                                        t3 = d.seqID == null ? 0 : d.seqID,
                                        t4 = f.seqID == null ? 0 : f.seqID,
                                    };
                        foreach (var row2 in tb)
                        {
                            d_NormalOT += row2.t1;
                            d_HolidayWork += row2.t2;
                            d_HolidayOT += row2.t3;
                            d_ActualTime += row2.t4;
                        }

                        if (0 != d_NormalOT)
                        {
                            d_NormalOT = d_NormalOT * 0.25;
                        }
                        if (0 != d_HolidayWork)
                        {
                            d_HolidayWork = d_HolidayWork * 0.25;
                        }
                        if (0 != d_HolidayOT)
                        {
                            d_HolidayOT = d_HolidayOT * 0.25;
                        }
                        if (0 != d_ActualTime)
                        {
                            d_ActualTime = d_ActualTime * 0.25;
                        }

                        var tb_wd = from x in db.TB_R_WORKINGREPORT_Ds where x.HEADER_ID == tb_wh.seqID.ToString() select x;
                        foreach (var x in tb_wd)
                        {
                            string s_day = d_target.ToString("yyyy") + "/" + d_target.ToString("MM") + "/" + x.DAY.ToString();
                            DateTime? date = ComFunc.ConvertDate(s_day);
                            TB_M_HOLIDAY tb_h = db.TB_M_HOLIDAYs.SingleOrDefault(y =>
                                y.DATE_HOLIDAY == date
                                );
                            if (null == tb_h)
                            {
                                d_OfficialDays++;
                            }
                            if (null != x.APPLY_STARTING && "" != x.APPLY_STARTING.Trim() &&
                                null != x.APPLY_LEAVING && "" != x.APPLY_LEAVING.Trim())
                            {
                                d_ActualDays++;
                            }
                            if (null != x.ATT1)
                            {
                                switch (x.ATT1.Trim())
                                {
                                    case "PAID":
                                        d_PAID++;
                                        break;
                                    case "HFPD":
                                        d_PAID = d_PAID + 0.5;
                                        break;
                                    case "ABSN":
                                        d_ABSN++;
                                        break;
                                    case "HFAB":
                                        d_ABSN = d_ABSN + 0.5;
                                        break;
                                    case "LATE":
                                        d_LATE++;
                                        if (true == b_ImportLate)
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.DATA_STARTING, out dtTmp))
                                            {
                                                if (dtTmp > dt_DfStart)
                                                {
                                                    ts_LateTime = ts_LateTime + (dtTmp - dt_DfStart);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.APPLY_STARTING, out dtTmp))
                                            {
                                                if (dtTmp > dt_DfStart)
                                                {
                                                    ts_LateTime = ts_LateTime + (dtTmp - dt_DfStart);
                                                }
                                            }
                                        }
                                        break;
                                    case "ERLY":
                                        d_ERLY++;
                                        if (true == b_ImportEarly)
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.DATA_LEAVING, out dtTmp))
                                            {
                                                if (dt_DfFinish > dtTmp)
                                                {
                                                    ts_EarlyTime = ts_EarlyTime + (dt_DfFinish - dtTmp);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.APPLY_LEAVING, out dtTmp))
                                            {
                                                if (dt_DfFinish > dtTmp)
                                                {
                                                    ts_EarlyTime = ts_EarlyTime + (dt_DfFinish - dtTmp);
                                                }
                                            }
                                        }
                                        break;
                                    case "HLWK":
                                        d_HLWK++;
                                        break;
                                    case "SPHL":
                                        d_SPHL++;
                                        break;
                                    case "COMP":
                                        d_COMP++;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (null != x.ATT2)
                            {
                                switch (x.ATT2.Trim())
                                {
                                    case "LATE":
                                        d_LATE++;
                                        if (true == b_ImportLate)
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.DATA_STARTING, out dtTmp))
                                            {
                                                if (dtTmp > dt_DfStart)
                                                {
                                                    ts_LateTime = ts_LateTime + (dtTmp - dt_DfStart);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.APPLY_STARTING, out dtTmp))
                                            {
                                                if (dtTmp > dt_DfStart)
                                                {
                                                    ts_LateTime = ts_LateTime + (dtTmp - dt_DfStart);
                                                }
                                            }
                                        }
                                        break;
                                    case "ERLY":
                                        d_ERLY++;
                                        if (true == b_ImportEarly)
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.DATA_LEAVING, out dtTmp))
                                            {
                                                if (dt_DfFinish > dtTmp)
                                                {
                                                    ts_EarlyTime = ts_EarlyTime + (dt_DfFinish - dtTmp);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (true == DateTime.TryParse("2000/01/01 " + x.APPLY_LEAVING, out dtTmp))
                                            {
                                                if (dt_DfFinish > dtTmp)
                                                {
                                                    ts_EarlyTime = ts_EarlyTime + (dt_DfFinish - dtTmp);
                                                }
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    if (0 != d_OfficialDays)
                    {
                        xlsCreator1.Pos(2, i_row).Value = d_OfficialDays;
                    }
                    if (0 != d_ActualDays)
                    {
                        xlsCreator1.Pos(3, i_row).Value = d_ActualDays;
                    }
                    xlsCreator1.Pos(4, i_row).Str = ComFunc.ConvertDoubletoTime(d_OfficialDays * 8);
                    xlsCreator1.Pos(5, i_row).Str = ComFunc.ConvertDoubletoTime(d_ActualTime);
                    xlsCreator1.Pos(6, i_row).Str = ComFunc.ConvertDoubletoTime(d_NormalOT);
                    xlsCreator1.Pos(7, i_row).Str = ComFunc.ConvertDoubletoTime(d_HolidayWork);
                    xlsCreator1.Pos(8, i_row).Str = ComFunc.ConvertDoubletoTime(d_HolidayOT);
                    if (0 != d_PAID)
                    {
                        xlsCreator1.Pos(9, i_row).Value = d_PAID;
                    }
                    if (0 != d_ABSN)
                    {
                        xlsCreator1.Pos(10, i_row).Value = d_ABSN;
                    }
                    if (0 != d_LATE)
                    {
                        xlsCreator1.Pos(11, i_row).Value = d_LATE;
                        xlsCreator1.Pos(12, i_row).Str = ((int)ts_LateTime.TotalHours).ToString() + ":" + String.Format("{0:D2}", ts_LateTime.Minutes);
                    }
                    if (0 != d_ERLY)
                    {
                        xlsCreator1.Pos(13, i_row).Value = d_ERLY;
                        xlsCreator1.Pos(14, i_row).Str = ((int)ts_EarlyTime.TotalHours).ToString() + ":" + String.Format("{0:D2}", ts_EarlyTime.Minutes);
                    }
                    if (0 != d_HLWK)
                    {
                        xlsCreator1.Pos(15, i_row).Value = d_HLWK;
                    }
                    if (0 != d_SPHL)
                    {
                        xlsCreator1.Pos(16, i_row).Value = d_SPHL;
                    }
                    if (0 != d_COMP)
                    {
                        xlsCreator1.Pos(17, i_row).Value = d_COMP;
                    }

                    d_OfficialDaysTTL = d_OfficialDaysTTL + d_OfficialDays;
                    d_ActualDaysTTL = d_ActualDaysTTL + d_ActualDays;
                    d_OfficialTimeTTL = d_OfficialTimeTTL + d_OfficialTime;
                    d_ActualTimeTTL = d_ActualTimeTTL + d_ActualTime;
                    d_NormalOTTTL = d_NormalOTTTL + d_NormalOT;
                    d_HolidayWorkTTL = d_HolidayWorkTTL + d_HolidayWork;
                    d_HolidayOTTTL = d_HolidayOTTTL + d_HolidayOT;
                    d_PAIDTTL = d_PAIDTTL + d_PAID;
                    d_ABSNTTL = d_ABSNTTL + d_ABSN;
                    d_LATETTL = d_LATETTL + d_LATE;
                    d_ERLYTTL = d_ERLYTTL + d_ERLY;
                    d_HLWKTTL = d_HLWKTTL + d_HLWK;
                    d_SPHLTTL = d_SPHLTTL + d_SPHL;
                    d_COMPTTL = d_COMPTTL + d_COMP;

                    ts_TTLLateTime = ts_TTLLateTime + ts_LateTime;
                    ts_TTLEarlyTime = ts_TTLEarlyTime + ts_EarlyTime;
                    ts_LateTime = new TimeSpan(0, 0, 0);
                    ts_EarlyTime = new TimeSpan(0, 0, 0);

                    d_OfficialDays = d_ActualDays = d_OfficialTime = d_ActualTime = d_NormalOT = d_HolidayWork = d_HolidayOT = d_PAID = d_ABSN = d_LATE = d_ERLY = d_HLWK = d_SPHL = d_COMP = 0;

                    TB_R_SALARY tb_paid = db.TB_R_SALARies.SingleOrDefault(x =>
                        x.ID == row.ID &&
                        x.DESC == "Paid Holiday" &&
                        x.REMARK == d_target.ToString("yyyy")
                        );
                    TB_R_SALARY tb_spec = db.TB_R_SALARies.SingleOrDefault(x =>
                        x.ID == row.ID &&
                        x.DESC == "Special Holiday" &&
                        x.REMARK == d_target.ToString("yyyy")
                        );

                    if (null != tb_paid)
                    {
                        xlsCreator1.Pos(18, i_row).Value = tb_paid.AMOUNT;
                        xlsCreator1.Pos(19, i_row).Value = d_PAIDTTL;
                        xlsCreator1.Pos(20, i_row).Value = tb_paid.AMOUNT - ComFunc.ConvertDoubletoDecimal(d_PAIDTTL);
                    }
                    if (null != tb_spec)
                    {
                        xlsCreator1.Pos(21, i_row).Value = tb_spec.AMOUNT;
                        xlsCreator1.Pos(22, i_row).Value = d_SPHLTTL;
                        xlsCreator1.Pos(23, i_row).Value = tb_spec.AMOUNT - ComFunc.ConvertDoubletoDecimal(d_SPHLTTL);
                    }

                    for (int i = 0; i < 24; i++)
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineLeft = ExcelCreator.xlLineStyle.lsNormal;
                        xlsCreator1.Pos(i, i_row).Attr.LineBottom = ExcelCreator.xlLineStyle.lsNormal;
                        xlsCreator1.Pos(i, i_row).Attr.LineRight = ExcelCreator.xlLineStyle.lsNormal;
                        xlsCreator1.Pos(i, i_row).Attr.LineTop = ExcelCreator.xlLineStyle.lsNormal;
                    }

                    i_row++;
                }

                // Total.
                xlsCreator1.Pos(1, i_row).Str = "TOTAL";

                int i_cellCD = 67;  // 'C'
                int i_cellCD2 = 64;  // '@'
                string s2 = "";

                for (int i = 0; i < 22; i++)
                {
                    char c = Convert.ToChar(i_cellCD);
                    string s1 = c.ToString();
                    xlsCreator1.Pos(2 + i, i_row).Value =
                        "=SUM(" + s2 + s1 + "7:" + s2 + s1 + i_row.ToString() + ")";
                    if (90 == i_cellCD)
                    {
                        i_cellCD = 65;  // 'A'
                        i_cellCD2++;
                        char c2 = Convert.ToChar(i_cellCD2);
                        s2 = c2.ToString();
                    }
                    else
                    {
                        i_cellCD++;
                    }
                }
                for (int i = 1; i < 24; i++)
                {
                    xlsCreator1.Pos(i, i_row).Attr.FontStyle = ExcelCreator.xlFontStyle.xsBold;
                    if (1 == i)
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineLeft = ExcelCreator.xlLineStyle.lsThick;
                    }
                    else
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineLeft = ExcelCreator.xlLineStyle.lsNormal;
                    }
                    if (23 == i)
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineRight = ExcelCreator.xlLineStyle.lsThick;
                    }
                    else
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineRight = ExcelCreator.xlLineStyle.lsNormal;
                    }
                    xlsCreator1.Pos(i, i_row).Attr.LineBottom = ExcelCreator.xlLineStyle.lsThick;
                    xlsCreator1.Pos(i, i_row).Attr.LineTop = ExcelCreator.xlLineStyle.lsThick;
                } 
                
                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3006";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        protected void Download_Report2(string sFilePath)
        {
            string filename = "WorkingRecordList" + "_" + LabelYear.Text.Trim() + LabelMonth.Text.Trim() + ".xls";

            Response.ClearContent();
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(filename)));
            Response.ContentType = "application/msexcel";
            Response.WriteFile(sFilePath);
            Response.End();
        }    
    }
}