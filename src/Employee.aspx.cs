using System;
using System.Collections;
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
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");
                ComFunc.Language("Employee.aspx", form1);
                Button_Print.OnClientClick = ComFunc.getMessage("C001");

                Session["search_id"] = "%%";
                Session["search_name"] = "%%";
                Session["selected_id"] = "";
            }
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            GridView1.SelectedIndex = -1;
            Session["search_id"] = "%" + TextBoxUser_ID.Text + "%";
            Session["search_name"] = "%" + TextBoxUserName.Text + "%";
        }

        protected void RefleshGridview1()
        {
            TextBoxUser_ID.Text = "";
            TextBoxUserName.Text = "";
            Session["search_id"] = "%%";
            Session["search_name"] = "%%";
            GridView1.DataBind();
            GridView1.SelectedIndex = -1;
        }

        protected void Button_Clear_Click(object sender, EventArgs e)
        {
            RefleshGridview1();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selected_id"] = GridView1.SelectedRow.Cells[1].Text.Trim();
            Response.Redirect("Employee_Detail.aspx");
        }

        protected void Button_New_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = "";
            Response.Redirect("Employee_Edit.aspx");
        }

        protected void Button_GenOrg_Click(object sender, EventArgs e)
        {
            if (false == ComFunc.CreateTextOrg())
            {
                string message = @"Can not generate XML file.\nPlease contact to Administrator.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else
            {
                string message = @"System update Organizational Chart normally.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
        }

        protected string s_FileType = "DepartmentList";
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
                new data("Department", EXCEL_STR),
                new data("Position", EXCEL_STR),
                new data("Employee Type", EXCEL_STR),
                new data("E-mail", EXCEL_STR),
                new data("Entering Day", EXCEL_STR),
                new data("TEL", EXCEL_STR),
                new data("Mobile", EXCEL_STR)
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
                    xlsCreator1.Pos(count, 0).Str = d[count].value1;
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
                string error_msg = @"System Error E2001";
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
    }
}