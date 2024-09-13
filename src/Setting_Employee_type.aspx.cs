using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Setting_Employee_type : System.Web.UI.Page
    {
        protected string s_FileType = "Employee_typeList";
        protected bool b_existSelect = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                ComFunc.Language("Setting_Employee_type.aspx", form1);
                Button_Print.OnClientClick = ComFunc.getMessage("C001");

                // set session for search condition.
                Session["search_id"] = "%%";
                Session["search_name"] = "%%";
                Session["selected_id"] = "";
            }
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            // set session for search condition.
            Session["search_id"] = "%" + TextBoxID.Text + "%";
            Session["search_name"] = "%" + TextBoxDetail.Text + "%";

            // screen show condition.
            DetailArea.Visible = false;
            GridView1.SelectedIndex = -1;
        }

        protected void Button_Clear_Click(object sender, EventArgs e)
        {
            // clear input.
            TextBoxID.Text = "";
            TextBoxDetail.Text = "";

            // set session for search condition.
            Session["search_id"] = "%%";
            Session["search_name"] = "%%";

            // screen show condition.
            DetailArea.Visible = false;
            GridView1.SelectedIndex = -1;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // screen show condition.
            DetailArea.Visible = true;
            Button_Table1.Visible = true;
            Button_Table2.Visible = false;

            // convert data from Gridview1 to detail area.
            Session["selected_id"] = GridView1.SelectedRow.Cells[1].Text;
            TextBoxID_D.Text = ComFunc.ConvertFromGridview(GridView1.SelectedRow.Cells[1].Text);
            TextBoxDetail_D.Text = ComFunc.ConvertFromGridview(GridView1.SelectedRow.Cells[2].Text);
        }

        protected void Button_New_Click(object sender, EventArgs e)
        {
            // screen show condition.
            DetailArea.Visible = true;
            Button_Table1.Visible = false;
            Button_Table2.Visible = true;
            GridView1.SelectedIndex = -1;

            // initialization detail area for input a new data.
            TextBoxID_D.Text = "";
            TextBoxDetail_D.Text = "";
        }

        protected void UpdateScreen(string s_type)
        {
            string message = "System " + s_type + " the data.";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);

            // screen show condition.
            DetailArea.Visible = false;
            GridView1.SelectedIndex = -1;
            GridView1.DataBind();
        }

        protected void Button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_M_EMPLOYEE_TYPE tb = db.TB_M_EMPLOYEE_TYPEs.SingleOrDefault(x =>
                        x.EMPLOYEE_TYPE == ComFunc.UseSession(Page, "selected_id")
                        );
                    tb.DETAIL = TextBoxDetail_D.Text;
                    tb.UPDATE_DATE = DateTime.Now;
                    tb.UPDATE_BY = ComFunc.UseSession(Page, "user_id");
                    db.SubmitChanges();

                    UpdateScreen("Update");
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4301";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_M_EMPLOYEE_TYPE tb = new TB_M_EMPLOYEE_TYPE
                    {
                        EMPLOYEE_TYPE = TextBoxID_D.Text,
                        DETAIL = TextBoxDetail_D.Text,
                        UPDATE_DATE = DateTime.Now,
                        UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                        CREATE_DATE = DateTime.Now,
                        CREATE_BY = ComFunc.UseSession(Page, "user_id")
                    };
                    db.TB_M_EMPLOYEE_TYPEs.InsertOnSubmit(tb);
                    db.SubmitChanges();

                    UpdateScreen("Create");
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4302";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_M_EMPLOYEE_TYPE tb = db.TB_M_EMPLOYEE_TYPEs.SingleOrDefault(x =>
                    x.EMPLOYEE_TYPE == ComFunc.UseSession(Page, "selected_id")
                    );
                db.TB_M_EMPLOYEE_TYPEs.DeleteOnSubmit(tb);
                db.SubmitChanges();

                UpdateScreen("Delete");
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4303";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected bool CheckInput()
        {
            bool b_ret = false;
            string message = "";

            if ("" == TextBoxID_D.Text)
            {
                message = @"Please Input a CD.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else if ("" == TextBoxDetail_D.Text)
            {
                message = @"Please Input a Detail.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else
            {
                b_ret = true;
            }

            return b_ret;
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            DetailArea.Visible = false;
            GridView1.SelectedIndex = -1;
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
                string error_msg = @"System Error E4304";
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
