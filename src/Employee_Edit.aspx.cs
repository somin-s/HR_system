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
    public partial class Employee_Edit : System.Web.UI.Page
    {
        protected static string s_imagePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                    HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                    HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                    ComFunc.LanguageTab("Employee_Edit.aspx", form1);

                    s_imagePath = "";

                    if ("" == ComFunc.UseSession(Page, "selected_id"))
                    {
                        tab.Tabs[1].Visible = false;
                        tab.Tabs[2].Visible = false;
                        tab.Tabs[3].Visible = false;
                        tab.Tabs[4].Visible = false;
                        tab.Tabs[5].Visible = false;
                        tab.Tabs[6].Visible = false;
                        tab.Tabs[7].Visible = false;
                    }
                    else
                    {
                        string s_id = ComFunc.UseSession(Page, "selected_id");
                        DataClassesDataContext db = new DataClassesDataContext();
                        TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                            x.ID == s_id
                            );
                        TB_R_USER tb_umy = db.TB_R_USERs.SingleOrDefault(x =>
                            x.ID == ComFunc.UseSession(Page, "user_id")
                            );
                        TB_M_EMPLOYEE_TYPE tb_emp = db.TB_M_EMPLOYEE_TYPEs.SingleOrDefault(x =>
                            x.EMPLOYEE_TYPE == tb_u.EMPLOYEE_TYPE
                            );
                        string s_emp = null == tb_emp ? "" : tb_emp.DETAIL.Trim();
                        TB_M_DEPARTMENT tb_dep = db.TB_M_DEPARTMENTs.SingleOrDefault(x =>
                            x.DEPARTMENT_CD == tb_u.DEPARTMENT_CD
                            );
                        string s_dep = null == tb_dep ? "" : tb_dep.DETAIL.Trim();
                        TB_M_DIVISION tb_div = db.TB_M_DIVISIONs.SingleOrDefault(x =>
                            x.DIVISION_CD == tb_u.DIVISION_CD
                            );
                        string s_div = null == tb_div ? "" : tb_div.DETAIL.Trim();
                        TB_M_SECTION tb_sec = db.TB_M_SECTIONs.SingleOrDefault(x =>
                            x.SECTION_CD == tb_u.SECTION_CD
                            );
                        string s_sec = null == tb_sec ? "" : tb_sec.DETAIL.Trim();
                        TB_M_POSITION tb_pos = db.TB_M_POSITIONs.SingleOrDefault(x =>
                            x.POSITION_CD == tb_u.POSITION_CD
                            );
                        string s_pos = null == tb_pos ? "" : tb_pos.DETAIL.Trim();
                        TB_M_JOB tb_job = db.TB_M_JOBs.SingleOrDefault(x =>
                            x.JOB_CD == tb_u.JOB_CD
                            );
                        string s_job = null == tb_job ? "" : tb_job.DETAIL.Trim();

                        TextBoxID.Text = tb_u.ID;
                        TextBoxID.Enabled = false;

                        TextBoxName.Text = ComFunc.ConvertStr(tb_u.NAME);
                        TextBoxLocalName.Text = ComFunc.ConvertStr(tb_u.LOCALNAME);

                        string s_sex = "";
                        if ('M' == tb_u.SEX)
                        {
                            s_sex = "Male";
                        }
                        else
                        {
                            s_sex = "Female";
                        }
                        for (int i = 0; i < DropDownListSex.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_sex == DropDownListSex.Items[i].Value.Trim())
                            {
                                DropDownListSex.SelectedIndex = i;
                                break;
                            }
                        }

                        TextBoxShortName.Text = tb_u.SHORTNAME.Trim();

                        DropDownListEmpTyp.DataBind();
                        for (int i = 0; i < DropDownListEmpTyp.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_emp == DropDownListEmpTyp.Items[i].Value.Trim())
                            {
                                DropDownListEmpTyp.SelectedIndex = i;
                                break;
                            }
                        }

                        TextBoxTermDt.Text = ComFunc.ConvertNullableDate(tb_u.TERMINATION_DT);

                        DropDownListDep.DataBind();
                        for (int i = 0; i < DropDownListDep.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_dep == DropDownListDep.Items[i].Value.Trim())
                            {
                                DropDownListDep.SelectedIndex = i;
                                break;
                            }
                        }

                        DropDownListDiv.DataBind();
                        for (int i = 0; i < DropDownListDiv.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_div == DropDownListDiv.Items[i].Value.Trim())
                            {
                                DropDownListDiv.SelectedIndex = i;
                                break;
                            }
                        }

                        DropDownListSec.DataBind();
                        for (int i = 0; i < DropDownListSec.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_sec == DropDownListSec.Items[i].Value.Trim())
                            {
                                DropDownListSec.SelectedIndex = i;
                                break;
                            }
                        }

                        DropDownListPos.DataBind();
                        for (int i = 0; i < DropDownListPos.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_pos == DropDownListPos.Items[i].Value.Trim())
                            {
                                DropDownListPos.SelectedIndex = i;
                                break;
                            }
                        }

                        DropDownListJobC.DataBind();
                        for (int i = 0; i < DropDownListJobC.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_job == DropDownListJobC.Items[i].Value.Trim())
                            {
                                DropDownListJobC.SelectedIndex = i;
                                break;
                            }
                        }

                        TextBoxBirth.Text = ComFunc.ConvertNullableDate(tb_u.BIRTHDAY);
                        LabelAge.Text = ComFunc.CheckYears(TextBoxBirth.Text);
                        TextBoxEntDt.Text = ComFunc.ConvertNullableDate(tb_u.ENTERINGDAY);
                        string[] s_Array = ComFunc.CheckDays(TextBoxEntDt.Text).Split(':');
                        if (2 == s_Array.Length)
                        {
                            LabelEntYear.Text = s_Array[0];
                            LabelEntYearUnit.Text = s_Array[1];
                        }
                        TextBoxAdd1.Text = tb_u.ADDRESS1.Trim();
                        TextBoxAdd2.Text = tb_u.ADDRESS2.Trim();
                        TextBoxTEL.Text = tb_u.TEL.Trim();
                        TextBoxMob.Text = tb_u.MOBILE.Trim();
                        TextBoxMail.Text = tb_u.EMAIL.Trim();

                        TextBoxBankName.Text = ComFunc.ConvertStr(tb_u.BANK_NAME);
                        TextBoxAccount.Text = ComFunc.ConvertStr(tb_u.ACCOUNT_NO);

                        RadioButtonListLang.DataBind();
                        for (int i = 0; i < RadioButtonListLang.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (tb_u.LANG.ToString() == RadioButtonListLang.Items[i].Value.Trim())
                            {
                                RadioButtonListLang.SelectedIndex = i;
                                break;
                            }
                        }

                        TextBoxParents.Text = tb_u.PARENTS;
                        string s_ParentsName = ComFunc.Get_UserName(TextBoxParents.Text);
                        DropDownList3.DataBind();
                        for (int i = 0; i < DropDownList3.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_ParentsName == DropDownList3.Items[i].Value.Trim())
                            {
                                DropDownList3.SelectedIndex = i;
                                break;
                            }
                        }

                        TextBoxApprover1.Text = tb_u.APPROVER1;
                        string s_Approver1 = ComFunc.Get_UserName(TextBoxApprover1.Text);
                        DropDownList5.DataBind();
                        for (int i = 0; i < DropDownList5.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_Approver1 == DropDownList5.Items[i].Value.Trim())
                            {
                                DropDownList5.SelectedIndex = i;
                                break;
                            }
                        }

                        TextBoxApprover2.Text = tb_u.APPROVER2;
                        string s_Approver2 = ComFunc.Get_UserName(TextBoxApprover2.Text);
                        DropDownList6.DataBind();
                        for (int i = 0; i < DropDownList6.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_Approver2 == DropDownList6.Items[i].Value.Trim())
                            {
                                DropDownList6.SelectedIndex = i;
                                break;
                            }
                        }

                        TextBoxApprover3.Text = tb_u.APPROVER3;
                        string s_Approver3 = ComFunc.Get_UserName(TextBoxApprover3.Text);
                        DropDownList7.DataBind();
                        for (int i = 0; i < DropDownList7.Items.Count; i++)
                        { // check the index of dropdown list.
                            if (s_Approver3 == DropDownList7.Items[i].Value.Trim())
                            {
                                DropDownList7.SelectedIndex = i;
                                break;
                            }
                        }

                        ImagePic.ImageUrl = tb_u.IMAGE;
                        s_imagePath = tb_u.IMAGE;

                        string[] s_Auth = { tb_u.AUTH1.ToString(),
                                        tb_u.AUTH2.ToString(),
                                        tb_u.AUTH3.ToString(),
                                        tb_u.AUTH4.ToString(),
                                        tb_u.AUTH5.ToString(),
                                        tb_u.AUTH6.ToString(),
                                        tb_u.AUTH7.ToString(),
                                        tb_u.AUTH8.ToString()
                                        };
                        int i_count = 0;
                        foreach (string s_flag in s_Auth)
                        {
                            if ("1" == s_flag)
                            {
                                CheckBoxListAuth.Items[i_count].Selected = true;
                            }
                            else
                            {
                                CheckBoxListAuth.Items[i_count].Selected = false;
                            }
                            i_count++;
                        }

                        if ('1' == tb_umy.AUTH3)
                        {
                            CheckBoxListAuth.Enabled = true;
                        }

                        if ('1' == tb_umy.AUTH4)
                        {
                            TabPanel8.Visible = true;
                        }

                        //if (ComFunc.UseSession(Page, "selected_id") == ComFunc.UseSession(Page, "user_id")
                        //    && 0 == ComFunc.ConvertInt(ComFunc.getSetting("PassExpireDay")))
                        if (0 == ComFunc.ConvertInt(ComFunc.getSetting("PassExpireDay")))
                        {
                            Password_Area.Visible = true;
                        }

                        if ("" != ComFunc.UseSession(Page, "selected_SalaryTab"))
                        {
                            tab.ActiveTabIndex = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_SalaryTab"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error_msg = @"System Error E2201";
                    ComFunc.WriteLogLocal(error_msg, ex.Message);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                }
            }
        }

        protected void Button_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee_Detail.aspx");
        }

        protected bool CheckInput()
        {
            bool b_ret = false;
            DateTime dt_out;

            string message = "";

            if ("" == TextBoxID.Text)
            {
                TextBoxID.Focus();
                message = @"Please input a ID.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else if ("" == TextBoxName.Text)
            {
                TextBoxName.Focus();
                message = @"Please input a Name.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else if (true == Password_Area.Visible && "" != TextBoxPassword.Text &&
                        TextBoxPassword.Text != TextBoxPassword2.Text)
            {
                TextBoxPassword.Text = "";
                TextBoxPassword2.Text = "";
                TextBoxPassword.Focus();

                message = @"Input Password are incorrect. Please input again.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            /*else if (0 == DropDownListSex.SelectedIndex)
            {
                message = @"Please select a Sex.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }
            else if (false == DateTime.TryParse(TextBoxTermDt.Text, out dt_out))
            {
                message = @"Please input a Termination Date.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
            }*/
            else
            {
                b_ret = true;
            }

            return b_ret;
        }

        protected void Button_Save_Click(object sender, EventArgs e)
        {
            bool b_flg = false;
            try
            {
                string message = "";

                if (CheckInput())
                {
                    string s_id = ComFunc.UseSession(Page, "selected_id");
                    char[] s_Auth = new char[8];
                    for (int i = 0; i < CheckBoxListAuth.Items.Count; i++)
                    {
                        if (true == CheckBoxListAuth.Items[i].Selected)
                        {
                            s_Auth[i] = '1';
                        }
                        else
                        {
                            s_Auth[i] = '0';
                        }
                    }

                    if ("" != s_id)
                    {
                        DataClassesDataContext db = new DataClassesDataContext();
                        TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                            x.ID == s_id
                            );

                        tb_u.ID = TextBoxID.Text;
                        tb_u.NAME = TextBoxName.Text;
                        tb_u.LOCALNAME = TextBoxLocalName.Text;
                        tb_u.SEX = ComFunc.Get_Sex(DropDownListSex.SelectedValue);
                        tb_u.SHORTNAME = TextBoxShortName.Text;
                        tb_u.EMPLOYEE_TYPE = ComFunc.Get_EmpTypCD(DropDownListEmpTyp.SelectedValue);
                        tb_u.TERMINATION_DT = ComFunc.ConvertDate(TextBoxTermDt.Text);
                        tb_u.DEPARTMENT_CD = ComFunc.Get_DepCD(DropDownListDep.SelectedValue);
                        tb_u.DIVISION_CD = ComFunc.Get_DivCD(DropDownListDiv.SelectedValue);
                        tb_u.SECTION_CD = ComFunc.Get_SecCD(DropDownListSec.SelectedValue);
                        tb_u.POSITION_CD = ComFunc.Get_PosCD(DropDownListPos.SelectedValue);
                        tb_u.JOB_CD = ComFunc.Get_JobCD(DropDownListJobC.SelectedValue);
                        tb_u.BIRTHDAY = ComFunc.ConvertDate(TextBoxBirth.Text);
                        tb_u.ENTERINGDAY = ComFunc.ConvertDate(TextBoxEntDt.Text);
                        tb_u.ADDRESS1 = TextBoxAdd1.Text;
                        tb_u.ADDRESS2 = TextBoxAdd2.Text;
                        tb_u.TEL = TextBoxTEL.Text;
                        tb_u.MOBILE = TextBoxMob.Text;
                        tb_u.EMAIL = TextBoxMail.Text;
                        tb_u.BANK_NAME = TextBoxBankName.Text;
                        tb_u.ACCOUNT_NO = TextBoxAccount.Text;
                        tb_u.LANG = RadioButtonListLang.SelectedValue[0];
                        tb_u.PARENTS = TextBoxParents.Text;
                        tb_u.APPROVER1 = TextBoxApprover1.Text;
                        tb_u.APPROVER2 = TextBoxApprover2.Text;
                        tb_u.APPROVER3 = TextBoxApprover3.Text;
                        tb_u.IMAGE = s_imagePath;
                        tb_u.AUTH1 = s_Auth[0];
                        tb_u.AUTH2 = s_Auth[1];
                        tb_u.AUTH3 = s_Auth[2];
                        tb_u.AUTH4 = s_Auth[3];
                        tb_u.AUTH5 = s_Auth[4];
                        tb_u.AUTH6 = s_Auth[5];
                        tb_u.AUTH7 = s_Auth[6];
                        tb_u.AUTH8 = s_Auth[7];

                        if (true == Password_Area.Visible && "" != TextBoxPassword.Text)
                        {
                            tb_u.CURRENT_PASSWORD = TextBoxPassword.Text;
                        }

                        tb_u.UPDATE_DATE = DateTime.Now;
                        tb_u.UPDATE_BY = ComFunc.UseSession(Page, "user_id");

                        db.SubmitChanges();

                        message = @"System Save modified informations.";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                    }
                    else
                    {
                        DataClassesDataContext db = new DataClassesDataContext();
                        TB_R_USER tb_u = new TB_R_USER
                        {
                            ID = TextBoxID.Text,
                            NAME = TextBoxName.Text,
                            LOCALNAME = TextBoxLocalName.Text,
                            SHORTNAME = TextBoxShortName.Text,
                            DEPARTMENT_CD = ComFunc.Get_DepCD(DropDownListDep.Text.Trim()),
                            DIVISION_CD = ComFunc.Get_DivCD(DropDownListDiv.SelectedValue),
                            SECTION_CD = ComFunc.Get_SecCD(DropDownListSec.SelectedValue),
                            POSITION_CD = ComFunc.Get_PosCD(DropDownListPos.SelectedValue),
                            JOB_CD = ComFunc.Get_JobCD(DropDownListJobC.SelectedValue),
                            EMPLOYEE_TYPE = ComFunc.Get_EmpTypCD(DropDownListEmpTyp.SelectedValue),
                            CURRENT_PASSWORD = ComFunc.getSetting("DefaultPass"),
                            EXPIRE_PASSWORD = new DateTime(2000,1,1),
                            SEX = ComFunc.Get_Sex(DropDownListSex.SelectedValue),
                            EMAIL = TextBoxMail.Text,
                            BANK_NAME = TextBoxBankName.Text,
                            ACCOUNT_NO = TextBoxAccount.Text,
                            LANG = RadioButtonListLang.SelectedValue[0],
                            TERMINATION_DT = ComFunc.ConvertDate(TextBoxTermDt.Text),
                            BIRTHDAY = ComFunc.ConvertDate(TextBoxBirth.Text),
                            ENTERINGDAY = ComFunc.ConvertDate(TextBoxEntDt.Text),
                            ADDRESS1 = TextBoxAdd1.Text,
                            ADDRESS2 = TextBoxAdd2.Text,
                            TEL = TextBoxTEL.Text,
                            MOBILE = TextBoxMob.Text,
                            PARENTS = TextBoxParents.Text,
                            APPROVER1 = TextBoxApprover1.Text,
                            APPROVER2 = TextBoxApprover2.Text,
                            APPROVER3 = TextBoxApprover3.Text,
                            IMAGE = s_imagePath,
                            AUTH1 = s_Auth[0],
                            AUTH2 = s_Auth[1],
                            AUTH3 = s_Auth[2],
                            AUTH4 = s_Auth[3],
                            AUTH5 = s_Auth[4],
                            AUTH6 = s_Auth[5],
                            AUTH7 = s_Auth[6],
                            AUTH8 = s_Auth[7],
                            UPDATE_DATE = DateTime.Now,
                            UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                            CREATE_DATE = DateTime.Now,
                            CREATE_BY = ComFunc.UseSession(Page, "user_id")
                        };
                        db.TB_R_USERs.InsertOnSubmit(tb_u);
                        db.SubmitChanges();

                        ComFunc.CreateSalaryData(TextBoxID.Text);

                        message = @"System Create user data.";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                    }
                    Session["selected_id"] = TextBoxID.Text.Trim();

                    b_flg = true;
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2202";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }

            if (true == b_flg)
            {
                Response.Redirect("Employee_Detail.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                HttpPostedFile posted = File1.PostedFile;
                string s_Filename = System.IO.Path.GetFileName(posted.FileName);
                string s_path = "";
                string s_folder = "";
                string message = "";

                if (s_Filename == "")
                {
                    message = @"Please select a uploadfile.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                    return;
                }

                s_folder = ComFunc.getSetting("ImagePath");
                string sDateTime = DateTime.Now.ToString("yyyyMMddhhmmss");
                s_path = s_folder + sDateTime + Path.GetExtension(s_Filename);

                if (true == File.Exists(s_path))
                {
                    string error_msg = @"System Error E2211";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                    return;
                }
                else
                {
                    posted.SaveAs(s_path);

                    FileStream fs = File.Open(s_path, FileMode.Open);
                    System.Drawing.Image Bitmap = System.Drawing.Image.FromStream(fs);

                    double oW = 120;
                    double oH = 160;

                    double s = Math.Min(oW / Bitmap.Width, oH / Bitmap.Height);
                    double sW = s * Bitmap.Width;
                    double sH = s * Bitmap.Height;

                    System.Drawing.Image outBmp = new System.Drawing.Bitmap((int)oW, (int)oH);
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(outBmp);

                    g.DrawImage(Bitmap, ((int)oW - (int)sW) / 2, ((int)oH - (int)sH) / 2, (int)sW, (int)sH);

                    try
                    {
                        fs.Close();
                        outBmp.Save(s_path, System.Drawing.Imaging.ImageFormat.Jpeg);

                        s_folder = "~/pic/";
                        s_path = s_folder + sDateTime + Path.GetExtension(s_Filename);
                        s_imagePath = s_path;
                        ImagePic.ImageUrl = s_path;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Bitmap.Dispose();
                        outBmp.Dispose();
                        g.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2203";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s_imagePath = "";
            ImagePic.ImageUrl = "";
        }

        protected void Button_Add1_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_LICENSE tb = new TB_R_LICENSE
                {
                    ID = ComFunc.UseSession(Page, "selected_id"),
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_LICENSEs.InsertOnSubmit(tb);
                db.SubmitChanges();

                // screen show condition.
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2204";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Add2_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_WORK_EXPERIENCE tb = new TB_R_WORK_EXPERIENCE
                {
                    ID = ComFunc.UseSession(Page, "selected_id"),
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_WORK_EXPERIENCEs.InsertOnSubmit(tb);
                db.SubmitChanges();

                // screen show condition.
                GridView3.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2205";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Add3_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_EDUCATION tb = new TB_R_EDUCATION
                {
                    ID = ComFunc.UseSession(Page, "selected_id"),
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_EDUCATIONs.InsertOnSubmit(tb);
                db.SubmitChanges();

                // screen show condition.
                GridView4.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2206";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Add4_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_TRAINING tb = new TB_R_TRAINING
                {
                    ID = ComFunc.UseSession(Page, "selected_id"),
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_TRAININGs.InsertOnSubmit(tb);
                db.SubmitChanges();

                // screen show condition.
                GridView5.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2207";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Add5_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_FAMILY tb = new TB_R_FAMILY
                {
                    ID = ComFunc.UseSession(Page, "selected_id"),
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_FAMILies.InsertOnSubmit(tb);
                db.SubmitChanges();

                // screen show condition.
                GridView6.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2208";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Add6_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_REVISION tb = new TB_R_REVISION
                {
                    ID = ComFunc.UseSession(Page, "selected_id"),
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_REVISIONs.InsertOnSubmit(tb);
                db.SubmitChanges();

                // screen show condition.
                GridView7.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2209";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Add7_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_SALARY tb = new TB_R_SALARY
                {
                    ID = ComFunc.UseSession(Page, "selected_id"),
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_SALARies.InsertOnSubmit(tb);
                db.SubmitChanges();

                // screen show condition.
                GridView8.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2210";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 1;
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 2;
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 3;
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 4;
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 5;
        }

        protected void GridView7_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 7;
        }

        protected void GridView8_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tab.ActiveTabIndex = 6;
        }

        protected void TextBoxBirth_TextChanged(object sender, EventArgs e)
        {
            LabelAge.Text = ComFunc.CheckYears(TextBoxBirth.Text);
        }

        protected void TextBoxEntDt_TextChanged(object sender, EventArgs e)
        {
            string[] s_Array = ComFunc.CheckDays(TextBoxEntDt.Text).Split(':');
            if (2 == s_Array.Length)
            {
                LabelEntYear.Text = s_Array[0];
                LabelEntYearUnit.Text = s_Array[1];
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("None" == DropDownList3.SelectedValue)
            {
                TextBoxParents.Text = DropDownList3.SelectedValue;
            }
            else
            {
                TextBoxParents.Text = ComFunc.Get_UserID(DropDownList3.SelectedValue);
            }
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("None" == DropDownList5.SelectedValue)
            {
                TextBoxApprover1.Text = DropDownList5.SelectedValue;
            }
            else
            {
                TextBoxApprover1.Text = ComFunc.Get_UserID(DropDownList5.SelectedValue);
            }
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("None" == DropDownList6.SelectedValue)
            {
                TextBoxApprover2.Text = DropDownList6.SelectedValue;
            }
            else
            {
                TextBoxApprover2.Text = ComFunc.Get_UserID(DropDownList6.SelectedValue);
            }
        }

        protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("None" == DropDownList7.SelectedValue)
            {
                TextBoxApprover3.Text = DropDownList7.SelectedValue;
            }
            else
            {
                TextBoxApprover3.Text = ComFunc.Get_UserID(DropDownList7.SelectedValue);
            }
        }

        protected void tab_ActiveTabChanged(object sender, EventArgs e)
        {
            Session["selected_SalaryTab"] = tab.ActiveTabIndex.ToString();
        }
    }
}