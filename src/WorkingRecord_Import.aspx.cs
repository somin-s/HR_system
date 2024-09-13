using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComFunction;

namespace BrightHRSystem
{
    public partial class WorkingRecord_Import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                ComFunc.Language("WorkingRecord_Import.aspx", form1);
            }
        }

        protected void Button_Import_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = string.Empty;
                if (fu_ImportCSV.PostedFile.ContentType.Equals("application/vnd.ms-excel"))
                {
                    fu_ImportCSV.SaveAs(@"C:\upload\" + fu_ImportCSV.FileName);
                    GridView1.DataSource = (DataTable)ReadToEnd(@"C:\upload\" + fu_ImportCSV.FileName);
                    GridView1.DataBind();
                }
                else
                {
                    string message = @"Please select csv file.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1201";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        private object ReadToEnd(string filePath)
        {
            try
            {
                DataTable dtDataSource = new DataTable();
                string[] fileContent = File.ReadAllLines(filePath);
                if (fileContent.Count() > 0)
                {
                    //Create data table columns
                    string[] columns = fileContent[0].Split(',');
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        columns[i] = columns[i].Replace("@@@", ",");
                        columns[i] = columns[i].Replace("\"", "");
                        dtDataSource.Columns.Add(columns[i]);
                    }

                    //Add row data
                    for (int i = 1; i < fileContent.Count(); i++)
                    {
                        string[] rowData = fileContent[i].Split(',');
                        for (int j = 0; j < rowData.Length; j++)
                        {
                            rowData[j] = rowData[j].Replace("@@@", ",");
                            rowData[j] = rowData[j].Replace("\"", "");
                        }
                        dtDataSource.Rows.Add(rowData);
                    }
                }
                return dtDataSource;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1202";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return null;
            }
        }

        protected bool CheckRow(GridViewRow row)
        {
            try
            {
                DateTime dt;
                string s_cell0 = ComFunc.ConvertFromGridview(row.Cells[0].Text);
                string s_cell1 = ComFunc.ConvertFromGridview(row.Cells[1].Text);
                string s_cell2 = ComFunc.ConvertFromGridview(row.Cells[2].Text);
                string s_cell3 = ComFunc.ConvertFromGridview(row.Cells[3].Text);
                if ("" == s_cell0.Trim())
                {
                    return false;
                }
                else if ("" == s_cell1.Trim())
                {
                    return false;
                }
                else if (false == DateTime.TryParse(s_cell1.Trim(), out dt))
                {
                    return false;
                }
                else if ("" == s_cell2.Trim())
                {
                    return false;
                }
                else if ("" == s_cell3.Trim())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1203";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return false;
            }
        }

        protected void Button_Upload_Click(object sender, EventArgs e)
        {
            try
            {
                if (0 == GridView1.Rows.Count)
                {
                    string message = @"Please import csv file before upload.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
                else
                {
                DataClassesDataContext db = new DataClassesDataContext();
                    DateTime dt;
                    double d_start = 0;
                    double d_finish = 0;
                    string s_start = "";
                    string s_finish = "";
                    TB_R_WORKINGREPORT_H tb_wh;
                    TB_R_WORKINGREPORT_D tb_wd;
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (true == CheckRow(row))
                        {
                            if (true == DateTime.TryParse(row.Cells[1].Text, out dt))
                            {
                                string s_year1 = "";
                                string s_month1 = "";
                                string s_year2 = "";
                                string s_month2 = "";
                                string s_yearH = "";
                                string s_monthH = "";

                                if ("Last" == ComFunc.getSetting("TargetMonth"))
                                {
                                    s_year1 = dt.Year.ToString();
                                    s_month1 = dt.Month.ToString();
                                    double d_year = ComFunc.ConvertDouble(dt.Year.ToString());
                                    double d_month = ComFunc.ConvertDouble(dt.Month.ToString());
                                    if (12 == d_month)
                                    {
                                        d_year += 1;
                                        d_month = 1;
                                    }
                                    else
                                    {
                                        d_month += 1;
                                    }
                                    s_year2 = d_year.ToString();
                                    s_month2 = d_month.ToString();
                                    s_yearH = s_year1;
                                    s_monthH = s_month1;
                                }
                                else
                                {
                                    s_year2 = dt.Year.ToString();
                                    s_month2 = dt.Month.ToString();
                                    double d_year = ComFunc.ConvertDouble(dt.Year.ToString());
                                    double d_month = ComFunc.ConvertDouble(dt.Month.ToString());
                                    int i_startday = ComFunc.ConvertInt(ComFunc.getSetting("StartDay"));
                                    if (i_startday <= dt.Day)
                                    {
                                        if (12 == d_month)
                                        {
                                            d_year += 1;
                                            d_month = 1;
                                        }
                                        else
                                        {
                                            d_month += 1;
                                        }
                                        s_year2 = d_year.ToString();
                                        s_month2 = d_month.ToString();
                                    }
                                    if (1 == d_month)
                                    {
                                        d_year -= 1;
                                        d_month = 12;
                                    }
                                    else
                                    {
                                        d_month -= 1;
                                    }
                                    s_year1 = d_year.ToString();
                                    s_month1 = d_month.ToString();
                                    s_yearH = s_year2;
                                    s_monthH = s_month2;
                                } 
                                
                                tb_wh = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                                    x.USER_ID == row.Cells[0].Text.Trim() &&
                                    x.YEAH == s_yearH &&
                                    x.MONTH == s_monthH
                                    );
                                if (null == tb_wh)
                                {
                                    Generate_Data(row.Cells[0].Text.Trim(), s_year1, s_month1, s_year2, s_month2);

                                    tb_wh = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                                        x.USER_ID == row.Cells[0].Text.Trim() &&
                                        x.YEAH == s_yearH &&
                                        x.MONTH == s_monthH
                                        );
                                }

                                tb_wd = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                                    x.HEADER_ID == tb_wh.seqID.ToString() &&
                                    x.DAY == dt.Day
                                    );
                                if (null != tb_wd)
                                {
                                    d_start = ComFunc.ConvertDoubleFromTime(row.Cells[2].Text, true);
                                    d_finish = ComFunc.ConvertDoubleFromTime(row.Cells[3].Text, false);
                                    s_start = ComFunc.ConvertDoubletoTime(d_start);
                                    s_finish = ComFunc.ConvertDoubletoTime(d_finish);

                                    tb_wd.APPLY_STARTING = s_start;
                                    tb_wd.APPLY_LEAVING = s_finish;
                                    tb_wd.DATA_STARTING = row.Cells[2].Text;
                                    tb_wd.DATA_LEAVING = row.Cells[3].Text;
                                    tb_wd.REST1 = "1:00";
                                    db.SubmitChanges();

                                    UpdatedData(tb_wd.seqID.ToString(), row.Cells[0].Text.Trim());
                                }
                            }
                        }
                    }

                    string message = @"System upload working record data.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1204";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Generate_Data(string s_userID, string s_Year, string s_Month, string s_Year2, string s_Month2)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_WORKINGREPORT_H tb_wh1;
                TB_R_WORKINGREPORT_H tb_wh2;
                TB_R_WORKINGREPORT_H tb_wh3;
                if ("Last" == ComFunc.getSetting("TargetMonth"))
                {
                    // check exist data.
                    tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == s_userID &&
                        x.YEAH == s_Year &&
                        x.MONTH == s_Month
                        );
                    if (null != tb_wh1)
                    {
                        return;
                    }

                    // create new data on header.
                    tb_wh2 = new TB_R_WORKINGREPORT_H
                    {
                        USER_ID = s_userID,
                        YEAH = s_Year,
                        MONTH = s_Month,
                        WORKING_PLACE = ComFunc.getSetting("DefaultWorkPlace")
                    };
                    db.TB_R_WORKINGREPORT_Hs.InsertOnSubmit(tb_wh2);
                    db.SubmitChanges();

                    // create new data on detail.
                    tb_wh3 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == s_userID &&
                        x.YEAH == s_Year &&
                        x.MONTH == s_Month
                        );
                }
                else
                {
                    // check exist data.
                    tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == s_userID &&
                        x.YEAH == s_Year2 &&
                        x.MONTH == s_Month2
                        );
                    if (null != tb_wh1)
                    {
                        return;
                    }

                    // create new data on header.
                    tb_wh2 = new TB_R_WORKINGREPORT_H
                    {
                        USER_ID = s_userID,
                        YEAH = s_Year2,
                        MONTH = s_Month2,
                        WORKING_PLACE = ComFunc.getSetting("DefaultWorkPlace")
                    };
                    db.TB_R_WORKINGREPORT_Hs.InsertOnSubmit(tb_wh2);
                    db.SubmitChanges();

                    // create new data on detail.
                    tb_wh3 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                        x.USER_ID == s_userID &&
                        x.YEAH == s_Year2 &&
                        x.MONTH == s_Month2
                        );
                }

                string seqID = "";
                if (null != tb_wh3)
                {
                    seqID = tb_wh3.seqID.ToString();
                    TB_R_WORKINGREPORT_D tb_wd1;
                    TB_R_WORKINGREPORT_D tb_wd2;

                    int i_startday = ComFunc.ConvertInt(ComFunc.getSetting("StartDay"));
                    for (int i = i_startday; i < DateTime.DaysInMonth(int.Parse(s_Year), int.Parse(s_Month)) + 1; i++)
                    {
                        tb_wd1 = new TB_R_WORKINGREPORT_D
                        {
                            HEADER_ID = seqID,
                            DAY = i,
                            WEEK = Convert.ToDateTime(
                                        s_Year + "/" +
                                        s_Month + "/" +
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
                                        s_Year2 + "/" +
                                        s_Month2 + "/" +
                                        i.ToString()).DayOfWeek.ToString().Substring(0, 3)
                        };
                        db.TB_R_WORKINGREPORT_Ds.InsertOnSubmit(tb_wd2);
                        db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1205";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void UpdatedData(string s_seqID, string s_userID)
        {
            string message = "";

            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_WORKINGREPORT_D tb_wd = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                    x.seqID == ComFunc.ConvertInt(s_seqID)
                    );
                if (null != tb_wd)
                {
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
                            s_holiday = ComFunc.ConvertDoubletoTime(d_Finish - d_Start - d_Rest);
                            s_holidayot = ComFunc.ConvertDoubletoTime(d_OtFinish - d_OtStart);
                            s_total = ComFunc.ConvertDoubletoTime((d_Finish - d_Start - d_Rest) + (d_OtFinish - d_OtStart));
                            if (null == s_status || "" == s_status)
                            {
                                s_status = "Nonacceptance";
                            }
                            break;
                        case "PAID":
                        case "HFPD":
                        case "ABSN":
                        case "HFAB":
                        case "LATE":
                        case "ERLY":
                        case "SPHL":
                        case "COMP":
                            s_normal = ComFunc.ConvertDoubletoTime(d_Finish - d_Start - d_Rest);
                            s_ot = ComFunc.ConvertDoubletoTime(d_OtFinish - d_OtStart);
                            s_total = ComFunc.ConvertDoubletoTime((d_Finish - d_Start - d_Rest) + (d_OtFinish - d_OtStart));
                            if (null == s_status || "" == s_status)
                            {
                                s_status = "Nonacceptance";
                            }
                            break;
                        default:
                            s_normal = ComFunc.ConvertDoubletoTime(d_Finish - d_Start - d_Rest);
                            s_ot = ComFunc.ConvertDoubletoTime(d_OtFinish - d_OtStart);
                            s_total = ComFunc.ConvertDoubletoTime((d_Finish - d_Start - d_Rest) + (d_OtFinish - d_OtStart));
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
                        default:
                            break;
                    }

                    TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                        x.ID == s_userID
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
                }
                else
                {
                    message = @"System can not get work time data.\nPlease contact to Administrator";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1206";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }
   }
}