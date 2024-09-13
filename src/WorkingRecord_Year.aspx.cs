using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Drawing;
using ComFunction;

namespace BrightHRSystem
{
    public partial class WorkingRecord_Year : System.Web.UI.Page
    {
        protected string s_FileType = "YearlyWorkingData";
        protected bool b_existSelect = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                    HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                    HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                    ComFunc.Language("WorkingRecord_Year.aspx", form1);

                    if ("" == ComFunc.UseSession(Page, "selected_id"))
                    {
                        Session["selected_id"] = ComFunc.UseSession(Page, "user_id");
                    }

                    UserCode.Text = ComFunc.UseSession(Page, "selected_id");
                    setBeforeAfter();

                    string s_userName = ComFunc.Get_UserName(ComFunc.UseSession(Page, "selected_id"));
                    DropDownListPIC.DataBind();
                    for (int i = 0; i < DropDownListPIC.Items.Count; i++)
                    { // check the index of dropdown list.
                        if (s_userName == DropDownListPIC.Items[i].Value.Trim())
                        {
                            DropDownListPIC.SelectedIndex = i;
                            break;
                        }
                    }

                    // set value.
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Year");
                    dt.Columns.Add("Month");
                    dt.Columns.Add("Official Days");
                    dt.Columns.Add("Actual Days");
                    dt.Columns.Add("Official Time");
                    dt.Columns.Add("Actual Time");
                    dt.Columns.Add("Normal OT");
                    dt.Columns.Add("Holiday Work");
                    dt.Columns.Add("Holiday OT");
                    dt.Columns.Add("PAID");
                    dt.Columns.Add("ABSN");
                    dt.Columns.Add("LATE");
                    dt.Columns.Add("LATE_TIME");
                    dt.Columns.Add("ERLY");
                    dt.Columns.Add("ERLY_TIME");
                    dt.Columns.Add("HLWK");
                    dt.Columns.Add("SPHL");
                    dt.Columns.Add("COMP");

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
                    string s_day = "";

                    if ("" != ComFunc.UseSession(Page, "selected_year"))
                    {
                        for (int j = 0; j < DDYear.Items.Count; j++)
                        { // check the index of dropdown list.
                            if (ComFunc.UseSession(Page, "selected_year") == DDYear.Items[j].Value.Trim())
                            {
                                DDYear.SelectedIndex = j;
                                break;
                            }
                        }
                    }

                    DataRow row;
                    DataClassesDataContext db = new DataClassesDataContext();
                    for (int i = 0; i < 12; i++)
                    {
                        row = dt.NewRow();
                        row["Year"] = DDYear.SelectedItem.ToString();
                        row["Month"] = (i + 1).ToString();

                        TB_R_WORKINGREPORT_H tb_wh = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                            x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                            x.YEAH == DDYear.SelectedItem.ToString() &&
                            x.MONTH == (i + 1).ToString()
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
                                s_day = DDYear.SelectedItem + "/" + (i + 1).ToString() + "/" + x.DAY.ToString();
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
                                        default:
                                            break;
                                    }
                                }
                            }
                        }

                        if (0 != d_OfficialDays)
                        {
                            row["Official Days"] = d_OfficialDays.ToString();
                        }
                        if (0 != d_ActualDays)
                        {
                            row["Actual Days"] = d_ActualDays.ToString();
                        }
                        row["Official Time"] = ComFunc.ConvertDoubletoTime(d_OfficialDays * 8);
                        row["Actual Time"] = ComFunc.ConvertDoubletoTime(d_ActualTime);
                        row["Normal OT"] = ComFunc.ConvertDoubletoTime(d_NormalOT);
                        row["Holiday Work"] = ComFunc.ConvertDoubletoTime(d_HolidayWork);
                        row["Holiday OT"] = ComFunc.ConvertDoubletoTime(d_HolidayOT);
                        if (0 != d_PAID)
                        {
                            row["PAID"] = d_PAID.ToString();
                        }
                        if (0 != d_ABSN)
                        {
                            row["ABSN"] = d_ABSN.ToString();
                        }
                        if (0 != d_LATE)
                        {
                            row["LATE"] = d_LATE.ToString();
                            row["LATE_TIME"] = ((int)ts_LateTime.TotalHours).ToString() + ":" + String.Format("{0:D2}", ts_LateTime.Minutes);
                        }
                        if (0 != d_ERLY)
                        {
                            row["ERLY"] = d_ERLY.ToString();
                            row["ERLY_TIME"] = ((int)ts_EarlyTime.TotalHours).ToString() + ":" + String.Format("{0:D2}", ts_EarlyTime.Minutes);
                        }
                        if (0 != d_HLWK)
                        {
                            row["HLWK"] = d_HLWK.ToString();
                        }
                        if (0 != d_SPHL)
                        {
                            row["SPHL"] = d_SPHL.ToString();
                        }
                        if (0 != d_COMP)
                        {
                            row["COMP"] = d_COMP.ToString();
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

                        dt.Rows.Add(row);
                    }
                    row = dt.NewRow();
                    row["Month"] = "Total";
                    row["Official Days"] = d_OfficialDaysTTL.ToString();
                    row["Actual Days"] = d_ActualDaysTTL.ToString();
                    row["Official Time"] = ComFunc.ConvertDoubletoTime(d_ActualDaysTTL * 8);
                    row["Actual Time"] = ComFunc.ConvertDoubletoTime(d_ActualTimeTTL);
                    row["Normal OT"] = ComFunc.ConvertDoubletoTime(d_NormalOTTTL);
                    row["Holiday Work"] = ComFunc.ConvertDoubletoTime(d_HolidayWorkTTL);
                    row["Holiday OT"] = ComFunc.ConvertDoubletoTime(d_HolidayOTTTL);
                    row["PAID"] = d_PAIDTTL.ToString();
                    row["ABSN"] = d_ABSNTTL.ToString();
                    row["LATE"] = d_LATETTL.ToString();
                    row["LATE_TIME"] = ((int)ts_TTLLateTime.TotalHours).ToString() + ":" + String.Format("{0:D2}", ts_TTLLateTime.Minutes);
                    row["ERLY"] = d_ERLYTTL.ToString();
                    row["ERLY_TIME"] = ((int)ts_TTLEarlyTime.TotalHours).ToString() + ":" + String.Format("{0:D2}", ts_TTLEarlyTime.Minutes);
                    row["HLWK"] = d_HLWKTTL.ToString();
                    row["SPHL"] = d_SPHLTTL.ToString();
                    row["COMP"] = d_COMPTTL.ToString();
                    dt.Rows.Add(row);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.Rows[GridView1.Rows.Count - 1].Font.Bold = true;
                    GridView1.Rows[GridView1.Rows.Count - 1].ForeColor = Color.Red;

                    TB_R_SALARY tb_paid = db.TB_R_SALARies.SingleOrDefault(x =>
                        x.ID == ComFunc.UseSession(Page, "selected_id") &&
                        x.DESC == "Paid Holiday" &&
                        x.REMARK == DDYear.SelectedItem.ToString()
                        );
                    TB_R_SALARY tb_spec = db.TB_R_SALARies.SingleOrDefault(x =>
                        x.ID == ComFunc.UseSession(Page, "selected_id") &&
                        x.DESC == "Special Holiday" &&
                        x.REMARK == DDYear.SelectedItem.ToString()
                        );

                    if (null != tb_paid)
                    {
                        Label_PaidYear.Text = String.Format("{0:f1}", tb_paid.AMOUNT);
                        Label_PaidUsed.Text = String.Format("{0:f1}", d_PAIDTTL);
                        Label_PaidRemain.Text = String.Format("{0:f1}", tb_paid.AMOUNT - ComFunc.ConvertDoubletoDecimal(d_PAIDTTL));
                    }
                    if (null != tb_spec)
                    {
                        Label_SpecialYear.Text = String.Format("{0:f1}", tb_spec.AMOUNT);
                        Label_SpecialUsed.Text = String.Format("{0:f1}", d_SPHLTTL);
                        Label_SpecialRemain.Text = String.Format("{0:f1}", tb_spec.AMOUNT - ComFunc.ConvertDoubletoDecimal(d_SPHLTTL));
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1301";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected static string s_idBefore = "";
        protected static string s_idAfter = "";

        protected void setBeforeAfter()
        {
            try
            {
                s_idBefore = "";
                s_idAfter = "";
                string s_id = ComFunc.UseSession(Page, "selected_id");
                bool b_find = false;
                DataClassesDataContext db = new DataClassesDataContext();
                var tb_u = from x in db.TB_R_USERs orderby x.ID select x.ID;
                foreach (var ID in tb_u)
                {
                    if (true == b_find)
                    {
                        s_idAfter = ID.Trim();
                        break;
                    }

                    if (ID.Trim() == s_id)
                    {
                        b_find = true;
                    }
                    else
                    {
                        s_idBefore = ID.Trim();
                    }
                }

                if ("" == s_idBefore)
                {
                    Button_Before.Enabled = false;
                }
                else if ("" == s_idAfter)
                {
                    Button_After.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1302";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Before_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idBefore;
            Response.Redirect("WorkingRecord_Year.aspx");
        }

        protected void Button_After_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idAfter;
            Response.Redirect("WorkingRecord_Year.aspx");
        }

        protected void DropDownListPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s_userID = ComFunc.Get_UserID(DropDownListPIC.SelectedValue);
            if ("" != s_userID)
            {
                Session["selected_id"] = s_userID;
                Response.Redirect("WorkingRecord_Year.aspx");
            }
        }

        protected void Button_Print_Click(object sender, EventArgs e)
        {
            // generate and download report.
            Download_Report(Generate_Report());
        }

        #region download excel list.
        private ExcelCreator.XlsCreator xlsCreator1 = new ExcelCreator.XlsCreator();

        protected string Generate_Report()
        {
            // data target.
            ArrayList s_Array = new ArrayList();
            int i_StartColumn = b_existSelect ? 1 : 0;

            for (int i = i_StartColumn; i < GridView1.Columns.Count + i_StartColumn; i++)
            {
                s_Array.Add(GridView1.HeaderRow.Cells[i].Text);
            }

            // File Name.
            string s_TempPath = ComFunc.getSetting("TempPath");
            string sDateTime = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00");
            string sFilePath = s_TempPath + s_FileType + "_" + sDateTime + ".xls";

            try
            {
                xlsCreator1.CreateBook(sFilePath, 3, ExcelCreator.xlVersion.ver2003);

                for (int count = 0; count < s_Array.Count; count++)
                {
                    xlsCreator1.Pos(count, 0).Value = s_Array[count];
                    xlsCreator1.Pos(count, 0).Attr.BackColor = (ExcelCreator.xlColor)50;
                    xlsCreator1.Pos(count, 0).Attr.FontColor = ExcelCreator.xlColor.xcWhite;
                    xlsCreator1.Pos(count, 0).Attr.FontStyle = ExcelCreator.xlFontStyle.xsBold;
                }

                // Detail data.
                int i = 1;
                string s_Data = "";
                double d_out = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    for (int column = 0; column < s_Array.Count; column++)
                    {
                        // check blank.
                        if ("&nbsp;" == row.Cells[column + i_StartColumn].Text)
                        {
                            s_Data = "";
                        }
                        else
                        {
                            s_Data = ComFunc.ConvertFromGridview(row.Cells[column + i_StartColumn].Text);
                        }

                        // check number.
                        if (double.TryParse(s_Data, out d_out))
                        {
                            xlsCreator1.Pos(column, i).Value = d_out;
                        }
                        else
                        {
                            xlsCreator1.Pos(column, i).Str = s_Data;
                        }
                    }
                    i++;
                }

                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1303";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        protected void Download_Report(string sFilePath)
        {
            string[] sFilePathArray = sFilePath.Split('\\');
            string filename = sFilePathArray[sFilePathArray.Length - 1];

            if ("" == sFilePath)
            {
                string message = @"File Download Error.\nPlease contact to Administrator.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else
            {
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", HttpUtility.UrlEncode(filename)));
                Response.ContentType = "application/msexcel";
                Response.WriteFile(sFilePath);
                Response.End();
            }
        }
        #endregion

        protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selected_year"] = DDYear.SelectedItem.ToString();
            Response.Redirect("WorkingRecord_Year.aspx");
        }

    }
}