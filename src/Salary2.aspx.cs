using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Salary2 : System.Web.UI.Page
    {
        protected static bool b_company = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (false == ComFunc.Check_AuthSalary(ComFunc.UseSession(Page, "user_id")))
                {
                    Response.Redirect("Menu.aspx");
                }

                HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                ComFunc.Language("Salary2.aspx", form1);
                Button_Print.OnClientClick = ComFunc.getMessage("C001");

                Session["search_id"] = "%%";
                Session["search_name"] = "%%";
                Session["selected_id"] = "";

                if(1 == ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")))
                {
                    Select_Area.Visible = false;
                }

                int i_calc = 0;
                if (DateTime.Now.Month < ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")))
                {
                    i_calc = 1;
                }
                string s_targetYear = (DateTime.Now.Year - i_calc).ToString();
                DropDownList1.SelectedValue = s_targetYear;

                setData(DropDownList1.SelectedValue, ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")));
            }
        }

        protected void setData(string s_year, int i_startMonth)
        {
            try
            {
                Label1.Text = s_year + " Yearly Salary";

                int i_year = ComFunc.ConvertInt(s_year);
                int[] i_targetYear = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                int[] i_targetMonth = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 12; i++)
                {
                    if (12 < (i_startMonth + i))
                    {
                        i_targetYear[i] = i_year + 1;
                        i_targetMonth[i] = i_startMonth + i - 12;
                    }
                    else
                    {
                        i_targetYear[i] = i_year;
                        i_targetMonth[i] = i_startMonth + i;
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NAME");
                DateTime dtTmp;
                for (int i = 0; i < 12; i++)
                {
                    dtTmp = new DateTime(i_targetYear[i], i_targetMonth[i], 1);
                    dt.Columns.Add("DATA" + i.ToString());
                    GridView1.Columns[i + 2].HeaderText = dtTmp.ToString("MMM-yyyy");
                    if (0 == i)
                    {
                        Label5.Text = "1-" + dtTmp.ToString("MMM-yyyy");
                    }
                    else if (11 == i)
                    {
                        Label7.Text = DateTime.DaysInMonth(dtTmp.Year, dtTmp.Month).ToString() +
                                        "-" + dtTmp.ToString("MMM-yyyy");
                    }
                }
                dt.Columns.Add("BONUS");
                dt.Columns.Add("TOTAL");

                DataClassesDataContext db = new DataClassesDataContext();
                int[] i_headerSeq = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 12; i++)
                {
                    TB_R_PAYROLL_H tb_ph = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                        x.YEAR == i_targetYear[i] && x.MONTH == i_targetMonth[i] && x.BONUS == 0
                        );
                    if (null != tb_ph)
                    {
                        i_headerSeq[i] = tb_ph.SEQ_ID;
                    }
                    tb_ph = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                        x.YEAR == i_targetYear[i] && x.MONTH == i_targetMonth[i] && x.BONUS == 1
                        );
                    if (null != tb_ph)
                    {
                        i_headerSeq[12] = tb_ph.SEQ_ID;
                    }
                }

                decimal[] d_MonthTotal = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                decimal d_UserTotal = 0;
                decimal d_GrandTotal = 0;
                DataRow dt_row;
                var tb_u = from x in db.TB_R_USERs orderby x.ID select x;
                foreach (var row in tb_u)
                {
                    dt_row = dt.NewRow();
                    dt_row["ID"] = ComFunc.ConvertStr(row.ID);
                    dt_row["Name"] = ComFunc.ConvertStr(row.NAME);

                    for (int i = 0; i < 13; i++)
                    {
                        TB_R_PAYROLL_D tb_pd = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                            x.HEADER_ID == i_headerSeq[i] && x.USER_ID == row.ID
                            );
                        if (null != tb_pd && null != tb_pd.INCOME_TOTAL)
                        {
                            dt_row[i + 2] = ComFunc.ConvertMoney(tb_pd.INCOME_TOTAL);
                            d_MonthTotal[i] = d_MonthTotal[i] + tb_pd.INCOME_TOTAL.Value;
                            d_UserTotal = d_UserTotal + tb_pd.INCOME_TOTAL.Value;
                        }
                    }

                    dt_row["TOTAL"] = ComFunc.ConvertMoney(d_UserTotal);
                    d_UserTotal = 0;
                    dt.Rows.Add(dt_row);
                }

                dt_row = dt.NewRow();
                dt_row["Name"] = "TOTAL";
                for (int i = 0; i < 13; i++)
                {
                    dt_row[i + 2] = ComFunc.ConvertMoney(d_MonthTotal[i]);
                    d_GrandTotal = d_GrandTotal + d_MonthTotal[i];
                }
                dt_row["TOTAL"] = ComFunc.ConvertMoney(d_GrandTotal);
                dt.Rows.Add(dt_row);

                GridView1.DataSource = dt;
                GridView1.DataBind();
                if("Y" != ComFunc.getSetting("BonusSlip"))
                {
                    GridView1.Columns[14].Visible = false;
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3201";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (true == b_company)
            {
                setData(DropDownList1.SelectedValue, ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")));
            }
            else
            {
                setData(DropDownList1.SelectedValue, 1);
            }
        }

        protected void Button_Print2_Click(object sender, EventArgs e)
        {
            if (-1 == GridView1.SelectedIndex)
            {
                string message = @"Please select any user first.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                return;
            }
        }

        protected string s_FileType = "YearlySalaryList";
        protected bool b_existSelect = true;
        protected const int EXCEL_STR = 0;
        protected const int EXCEL_NUM = 1;
        protected const int EXCEL_MON = 2;
        struct data
        {
            public data(string _value1, int _value2)
            {
                this.value1 = _value1;
                this.value2 = _value2;
            }
            public string value1;
            public int value2;
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
            int i_StartColumn = b_existSelect ? 1 : 0;
            data[] d = new data[]
            {
                new data("ID", EXCEL_STR),
                new data("Name", EXCEL_STR),
                new data("DATA0", EXCEL_MON),
                new data("DATA1", EXCEL_MON),
                new data("DATA2", EXCEL_MON),
                new data("DATA3", EXCEL_MON),
                new data("DATA4", EXCEL_MON),
                new data("DATA5", EXCEL_MON),
                new data("DATA6", EXCEL_MON),
                new data("DATA7", EXCEL_MON),
                new data("DATA8", EXCEL_MON),
                new data("DATA9", EXCEL_MON),
                new data("DATA10", EXCEL_MON),
                new data("DATA11", EXCEL_MON),
                new data("DATA12", EXCEL_MON),
                new data("TOTAL", EXCEL_MON)
            };

            // File Name.
            string s_TempPath = ComFunc.getSetting("TempPath");
            string sDateTime = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00");
            string sFilePath = s_TempPath + s_FileType + "_" + sDateTime + ".xls";

            try
            {
                xlsCreator1.CreateBook(sFilePath, 3, ExcelCreator.xlVersion.ver2003);

                for (int count = 0; count < d.Length; count++)
                {
                    xlsCreator1.Pos(count, 0).Str = GridView1.Columns[count].HeaderText;
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
                    for (int column = 0; column < d.Length; column++)
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
                        switch (d[column].value2)
                        {
                            case EXCEL_STR:
                                xlsCreator1.Pos(column, i).Str = s_Data;
                                break;
                            case EXCEL_NUM:
                                if (double.TryParse(s_Data, out d_out))
                                {
                                    xlsCreator1.Pos(column, i).Value = d_out;
                                }
                                break;
                            case EXCEL_MON:
                                if (double.TryParse(s_Data, out d_out))
                                {
                                    xlsCreator1.Pos(column, i).Value = d_out;
                                    xlsCreator1.Pos(column, i).Attr.Format = "#,##0.00_ ;[Red]-#,##0.00";
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    i++;
                }

                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3202";
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

        protected void Button_Comp_Click(object sender, EventArgs e)
        {
            b_company = true;
            setData(DropDownList1.SelectedValue, ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")));
        }

        protected void Button_Normal_Click(object sender, EventArgs e)
        {
            b_company = false;
            setData(DropDownList1.SelectedValue, 1);
        }
    }
}