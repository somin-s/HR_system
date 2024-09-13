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
    public partial class WorkingRecord_Acceptance : System.Web.UI.Page
    {
        protected string s_FileType = "ApplicationList";
        protected string s_FileType2 = "ApprovalList";
        protected bool b_existSelect = false;
        protected static bool b_approved1 = false;
        protected static bool b_approved2 = false;

        protected static ArrayList s_Gridview2SeqID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                    HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                    HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                    ComFunc.Language("WorkingRecord_Acceptance.aspx", form1);

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

                    b_approved1 = false;
                    b_approved2 = false;

                    setApplicationList();
                    setApprovalList();
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1101";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void setApplicationList()
        {
            try
            {
                // set value.
                DataTable dt = new DataTable();
                dt.Columns.Add("Year");
                dt.Columns.Add("Month");
                dt.Columns.Add("Day");
                dt.Columns.Add("Week");
                dt.Columns.Add("Type");
                dt.Columns.Add("Detail");
                dt.Columns.Add("Status");
                dt.Columns.Add("Approver1");
                dt.Columns.Add("Date1");
                dt.Columns.Add("Comment1");
                dt.Columns.Add("Approver2");
                dt.Columns.Add("Date2");
                dt.Columns.Add("Comment2");
                dt.Columns.Add("Approver3");
                dt.Columns.Add("Date3");
                dt.Columns.Add("Comment3");

                int i_year = 0;
                int i_month = 0;
                int i_day = 0;
                int i_startday = ComFunc.ConvertInt(ComFunc.getSetting("StartDay"));

                DataRow row;
                DataClassesDataContext db = new DataClassesDataContext();
                var tb_wh = from a in db.TB_R_WORKINGREPORT_Hs
                            where a.USER_ID == ComFunc.UseSession(Page, "selected_id")
                            select a;
                foreach (var row2 in tb_wh)
                {
                    var tb_wd = from b in db.TB_R_WORKINGREPORT_Ds
                                where b.HEADER_ID == row2.seqID.ToString().Trim()
                                select b;
                    foreach (var row3 in tb_wd)
                    {

                        if ("" != ComFunc.ConvertStr(row3.ATT1) || "" != ComFunc.ConvertStr(row3.ATT2))
                        {
                            if ("Approved" != ComFunc.ConvertStr(row3.STATUS) || b_approved1)
                            {
                                TB_R_USER app1 = db.TB_R_USERs.SingleOrDefault(x =>
                                    x.ID == row3.APPROVER1
                                    );
                                string s_app1 = "";
                                if (null != app1)
                                {
                                    s_app1 = app1.SHORTNAME;
                                }
                                TB_R_USER app2 = db.TB_R_USERs.SingleOrDefault(x =>
                                    x.ID == row3.APPROVER2
                                    );
                                string s_app2 = "";
                                if (null != app2)
                                {
                                    s_app2 = app2.SHORTNAME;
                                }
                                TB_R_USER app3 = db.TB_R_USERs.SingleOrDefault(x =>
                                    x.ID == row3.APPROVER3
                                    );
                                string s_app3 = "";
                                if (null != app3)
                                {
                                    s_app3 = app3.SHORTNAME;
                                }

                                string s_type = "";
                                if ("" != ComFunc.ConvertStr(row3.ATT1) && "" != ComFunc.ConvertStr(row3.ATT2))
                                {
                                    s_type = ComFunc.ConvertStr(row3.ATT1) + "+" + ComFunc.ConvertStr(row3.ATT2);
                                }
                                else
                                {
                                    s_type = ComFunc.ConvertStr(row3.ATT1) + ComFunc.ConvertStr(row3.ATT2);
                                }

                                i_day = row3.DAY.Value;
                                if (i_startday != 1 && i_startday <= i_day)
                                {
                                    i_year = ComFunc.ConvertInt(row2.YEAH);
                                    i_month = ComFunc.ConvertInt(row2.MONTH);
                                    if (1 == i_month)
                                    {
                                        i_year -= 1;
                                        i_month = 12;
                                    }
                                    else
                                    {
                                        i_month -= 1;
                                    }
                                }
                                else
                                {
                                    i_year = ComFunc.ConvertInt(row2.YEAH);
                                    i_month = ComFunc.ConvertInt(row2.MONTH);
                                }

                                row = dt.NewRow();
                                row["Year"] = i_year.ToString();
                                row["Month"] = i_month.ToString();
                                row["Day"] = i_day.ToString();
                                row["Week"] = row3.WEEK;
                                row["Type"] = s_type;
                                row["Detail"] = row3.DETAIL;
                                row["Status"] = row3.STATUS;
                                row["Approver1"] = s_app1;
                                row["Date1"] = ComFunc.ConvertDateToStr(row3.APPROVE_DATE1);
                                row["Comment1"] = row3.APPROVE_COMMENT1;
                                row["Approver2"] = s_app2;
                                row["Date2"] = ComFunc.ConvertDateToStr(row3.APPROVE_DATE2);
                                row["Comment2"] = row3.APPROVE_COMMENT2;
                                row["Approver3"] = s_app3;
                                row["Date3"] = ComFunc.ConvertDateToStr(row3.APPROVE_DATE3);
                                row["Comment3"] = row3.APPROVE_COMMENT3;
                                dt.Rows.Add(row);
                            }
                        }
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1102";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void setApprovalList()
        {
            try
            {
                s_Gridview2SeqID = new ArrayList();

                // set value.
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Year");
                dt.Columns.Add("Month");
                dt.Columns.Add("Day");
                dt.Columns.Add("Week");
                dt.Columns.Add("Type");
                dt.Columns.Add("Detail");
                dt.Columns.Add("Status");
                dt.Columns.Add("Approver1");
                dt.Columns.Add("Date1");
                dt.Columns.Add("Comment1");
                dt.Columns.Add("Approver2");
                dt.Columns.Add("Date2");
                dt.Columns.Add("Comment2");
                dt.Columns.Add("Approver3");
                dt.Columns.Add("Date3");
                dt.Columns.Add("Comment3");

                int i_year = 0;
                int i_month = 0;
                int i_day = 0;
                int i_startday = ComFunc.ConvertInt(ComFunc.getSetting("StartDay"));

                DataRow row;
                DataClassesDataContext db = new DataClassesDataContext();

                var tb_wd = from b in db.TB_R_WORKINGREPORT_Ds
                            where b.APPROVER1 == ComFunc.UseSession(Page, "selected_id") ||
                                    b.APPROVER2 == ComFunc.UseSession(Page, "selected_id") ||
                                    b.APPROVER3 == ComFunc.UseSession(Page, "selected_id")
                            select b;
                foreach (var row3 in tb_wd)
                {
                    if ("" != ComFunc.ConvertStr(row3.ATT1) || "" != ComFunc.ConvertStr(row3.ATT2))
                    {
                        if ("Approved" != ComFunc.ConvertStr(row3.STATUS) || b_approved2)
                        {
                            TB_R_WORKINGREPORT_H tb_wh = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                                    x.seqID == ComFunc.ConvertInt(row3.HEADER_ID)
                                );

                            TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                                x.ID == tb_wh.USER_ID
                                );

                            TB_R_USER app1 = db.TB_R_USERs.SingleOrDefault(x =>
                                x.ID == row3.APPROVER1
                                );
                            string s_app1 = "";
                            if (null != app1)
                            {
                                s_app1 = app1.SHORTNAME;
                            }
                            TB_R_USER app2 = db.TB_R_USERs.SingleOrDefault(x =>
                                x.ID == row3.APPROVER2
                                );
                            string s_app2 = "";
                            if (null != app2)
                            {
                                s_app2 = app2.SHORTNAME;
                            }
                            TB_R_USER app3 = db.TB_R_USERs.SingleOrDefault(x =>
                                x.ID == row3.APPROVER3
                                );
                            string s_app3 = "";
                            if (null != app3)
                            {
                                s_app3 = app3.SHORTNAME;
                            }

                            string s_type = "";
                            if ("" != ComFunc.ConvertStr(row3.ATT1) && "" != ComFunc.ConvertStr(row3.ATT2))
                            {
                                s_type = ComFunc.ConvertStr(row3.ATT1) + "+" + ComFunc.ConvertStr(row3.ATT2);
                            }
                            else
                            {
                                s_type = ComFunc.ConvertStr(row3.ATT1) + ComFunc.ConvertStr(row3.ATT2);
                            }

                            i_day = row3.DAY.Value;
                            if (i_startday != 1 && i_startday <= i_day)
                            {
                                i_year = ComFunc.ConvertInt(tb_wh.YEAH);
                                i_month = ComFunc.ConvertInt(tb_wh.MONTH);
                                if (1 == i_month)
                                {
                                    i_year -= 1;
                                    i_month = 12;
                                }
                                else
                                {
                                    i_month -= 1;
                                }
                            }
                            else
                            {
                                i_year = ComFunc.ConvertInt(tb_wh.YEAH);
                                i_month = ComFunc.ConvertInt(tb_wh.MONTH);
                            }

                            row = dt.NewRow();
                            row["ID"] = tb_u.ID;
                            row["Name"] = tb_u.NAME;
                            row["Year"] = i_year.ToString();
                            row["Month"] = i_month.ToString();
                            row["Day"] = i_day.ToString();
                            row["Week"] = row3.WEEK;
                            row["Type"] = s_type;
                            row["Detail"] = row3.DETAIL;
                            row["Status"] = row3.STATUS;
                            row["Approver1"] = s_app1;
                            row["Date1"] = ComFunc.ConvertDateToStr(row3.APPROVE_DATE1);
                            row["Comment1"] = row3.APPROVE_COMMENT1;
                            row["Approver2"] = s_app2;
                            row["Date2"] = ComFunc.ConvertDateToStr(row3.APPROVE_DATE2);
                            row["Comment2"] = row3.APPROVE_COMMENT2;
                            row["Approver3"] = s_app3;
                            row["Date3"] = ComFunc.ConvertDateToStr(row3.APPROVE_DATE3);
                            row["Comment3"] = row3.APPROVE_COMMENT3;
                            dt.Rows.Add(row);

                            s_Gridview2SeqID.Add(row3.seqID);
                        }
                    }
                }

                GridView2.SelectedIndex = -1;
                GridView2.DataSource = dt;
                if (ComFunc.UseSession(Page, "user_id") != ComFunc.UseSession(Page, "selected_id"))
                {
                    GridView2.AutoGenerateSelectButton = false;
                }
                else
                {
                    GridView2.AutoGenerateSelectButton = true;
                }
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1103";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 1;
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
                string error_msg = @"System Error E1104";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Before_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idBefore;
            Response.Redirect("WorkingRecord_Acceptance.aspx");
        }

        protected void Button_After_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idAfter;
            Response.Redirect("WorkingRecord_Acceptance.aspx");
        }

        protected void DropDownListPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s_userID = ComFunc.Get_UserID(DropDownListPIC.SelectedValue);
            if ("" != s_userID)
            {
                Session["selected_id"] = s_userID;
                Response.Redirect("WorkingRecord_Acceptance.aspx");
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
                string error_msg = @"System Error E1105";
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

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxDate.Text = "";
            TextBoxComment.Text = "";
            ApproveArea.Visible = true;
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_WORKINGREPORT_D tb_wd = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                                            x.seqID == ComFunc.ConvertInt(s_Gridview2SeqID[GridView2.SelectedIndex].ToString())
                                            );
                if (null != tb_wd)
                {
                    if (0 == RBApprove.SelectedIndex)
                    {
                        if (ComFunc.UseSession(Page, "user_id") == tb_wd.APPROVER1.Trim())
                        {
                            tb_wd.APPROVE_COMMENT1 = TextBoxComment.Text;
                            if (null != ComFunc.ConvertDate(TextBoxDate.Text))
                            {
                                tb_wd.APPROVE_DATE1 = ComFunc.ConvertDate(TextBoxDate.Text);
                                tb_wd.TOKEN1 = "";
                            }
                            else
                            {
                                tb_wd.APPROVE_DATE1 = null;
                            }
                        }
                        if (ComFunc.UseSession(Page, "user_id") == tb_wd.APPROVER2.Trim())
                        {
                            tb_wd.APPROVE_COMMENT2 = TextBoxComment.Text;
                            if (null != ComFunc.ConvertDate(TextBoxDate.Text))
                            {
                                tb_wd.APPROVE_DATE2 = ComFunc.ConvertDate(TextBoxDate.Text);
                                tb_wd.TOKEN2 = "";
                            }
                            else
                            {
                                tb_wd.APPROVE_DATE2 = null;
                            }
                        }
                        if (ComFunc.UseSession(Page, "user_id") == tb_wd.APPROVER3.Trim())
                        {
                            tb_wd.APPROVE_COMMENT3 = TextBoxComment.Text;
                            if (null != ComFunc.ConvertDate(TextBoxDate.Text))
                            {
                                tb_wd.APPROVE_DATE3 = ComFunc.ConvertDate(TextBoxDate.Text);
                                tb_wd.TOKEN3 = "";
                            }
                            else
                            {
                                tb_wd.APPROVE_DATE3 = null;
                            }
                        }
                        db.SubmitChanges();

                        tb_wd = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                                x.seqID == ComFunc.ConvertInt(s_Gridview2SeqID[GridView2.SelectedIndex].ToString())
                                );

                        if ((ComFunc.checkApproved(tb_wd.APPROVER1, tb_wd.APPROVE_DATE1)) &&
                            (ComFunc.checkApproved(tb_wd.APPROVER2, tb_wd.APPROVE_DATE2)) &&
                            (ComFunc.checkApproved(tb_wd.APPROVER3, tb_wd.APPROVE_DATE3)))
                        {
                            tb_wd.STATUS = "Approved";
                        }
                        else
                        {
                            tb_wd.STATUS = "Approving";
                        }
                        db.SubmitChanges();
                    }
                    else
                    {
                        if (ComFunc.UseSession(Page, "user_id") == tb_wd.APPROVER1.Trim())
                        {
                            tb_wd.APPROVE_COMMENT1 = TextBoxComment.Text;
                            tb_wd.APPROVE_DATE1 = null;
                        }
                        if (ComFunc.UseSession(Page, "user_id") == tb_wd.APPROVER2.Trim())
                        {
                            tb_wd.APPROVE_COMMENT2 = TextBoxComment.Text;
                            tb_wd.APPROVE_DATE2 = null;
                        }
                        if (ComFunc.UseSession(Page, "user_id") == tb_wd.APPROVER3.Trim())
                        {
                            tb_wd.APPROVE_COMMENT3 = TextBoxComment.Text;
                            tb_wd.APPROVE_DATE3 = null;
                        }
                        db.SubmitChanges();

                        ComFunc.sendEmailNotApproved(s_Gridview2SeqID[GridView2.SelectedIndex].ToString());
                    }
                    string message = @"Update selected application.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
                ApproveArea.Visible = false;
                setApplicationList();
                setApprovalList();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1106";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void CheckBox_Approved1_CheckedChanged(object sender, EventArgs e)
        {
            b_approved1 = CheckBox_Approved1.Checked;
            tab.ActiveTabIndex = 0;
            setApplicationList();
        }

        protected void CheckBox_Approved2_CheckedChanged(object sender, EventArgs e)
        {
            b_approved2 = CheckBox_Approved2.Checked;
            tab.ActiveTabIndex = 1;
            setApprovalList();
        }
    }
}