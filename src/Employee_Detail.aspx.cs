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
    public partial class Employee_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ("" == ComFunc.UseSession(Page, "selected_id"))
                {
                    Response.Redirect("Employee.aspx");
                }

                try
                {
                    HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                    HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                    HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                    ComFunc.LanguageTab("Employee_Detail.aspx", form1);

                    string s_id = ComFunc.UseSession(Page, "selected_id");

                    setBeforeAfter();

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

                    LabelID.Text = tb_u.ID;
                    LabelName.Text = tb_u.NAME;
                    LabelLocalName.Text = tb_u.LOCALNAME;

                    string s_sex = "";
                    if ('M' == tb_u.SEX)
                    {
                        s_sex = "Male";
                    }
                    else
                    {
                        s_sex = "Female";
                    }
                    LabelSex.Text = s_sex;
                    LabelShortName.Text = tb_u.SHORTNAME;
                    LabelEmpTyp.Text = s_emp;
                    LabelTermDt.Text = ComFunc.ConvertNullableDate(tb_u.TERMINATION_DT);
                    Label1Dep.Text = s_dep;
                    Label1Div.Text = s_div;
                    LabelSec.Text = s_sec;
                    LabelPos.Text = s_pos;
                    LabelJobC.Text = s_job;
                    LabelBirth.Text = ComFunc.ConvertNullableDate(tb_u.BIRTHDAY);
                    LabelAge.Text = ComFunc.CheckYears(LabelBirth.Text);
                    LabelEntDt.Text = ComFunc.ConvertNullableDate(tb_u.ENTERINGDAY);
                    string[] s_Array = ComFunc.CheckDays(LabelEntDt.Text).Split(':');
                    if (2 == s_Array.Length)
                    {
                        LabelEntYear.Text = s_Array[0];
                        LabelEntYearUnit.Text = s_Array[1];
                    }
                    LabelAdd1.Text = tb_u.ADDRESS1;
                    LabelAdd2.Text = tb_u.ADDRESS2;
                    LabelTEL.Text = tb_u.TEL;
                    LabelMob.Text = tb_u.MOBILE;
                    LabelMail.Text = tb_u.EMAIL;

                    LabelBankName.Text = tb_u.BANK_NAME;
                    LabelAccount.Text = tb_u.ACCOUNT_NO;

                    switch (tb_u.LANG)
                    {
                        case '0':
                            LabelLang.Text = "English";
                            break;
                        case '1':
                            LabelLang.Text = "Japanese";
                            break;
                        case '2':
                            LabelLang.Text = "Thai";
                            break;
                        default:
                            break;
                    }

                    LabelParents.Text = tb_u.PARENTS;
                    LabelParentsName.Text = ComFunc.Get_UserName(LabelParents.Text);

                    LabelApprover1.Text = tb_u.APPROVER1;
                    LabelApproverName1.Text = ComFunc.Get_UserName(LabelApprover1.Text);
                    LabelApprover2.Text = tb_u.APPROVER2;
                    LabelApproverName2.Text = ComFunc.Get_UserName(LabelApprover2.Text);
                    LabelApprover3.Text = tb_u.APPROVER3;
                    LabelApproverName3.Text = ComFunc.Get_UserName(LabelApprover3.Text);

                    ImagePic.ImageUrl = tb_u.IMAGE;

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
                    foreach(string s_flag in s_Auth)
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

                    if (('1' == tb_umy.AUTH3) ||
                        ('1' == tb_umy.AUTH7 && s_id == ComFunc.UseSession(Page, "user_id")))
                    {
                        Button_Edit.Enabled = true;
                        Button_Delete.Enabled = true;
                    }
                    if ('1' == tb_umy.AUTH4)
                    {
                        TabPanel8.Visible = true;
                    }

                    GridView2.DataBind();
                    if (0 == GridView2.Rows.Count)
                    {
                        NoData2.Visible = true;
                    }
                    GridView3.DataBind();
                    if (0 == GridView3.Rows.Count)
                    {
                        NoData3.Visible = true;
                    }
                    GridView4.DataBind();
                    if (0 == GridView4.Rows.Count)
                    {
                        NoData4.Visible = true;
                    }
                    GridView5.DataBind();
                    if (0 == GridView5.Rows.Count)
                    {
                        NoData5.Visible = true;
                    }
                    GridView6.DataBind();
                    if (0 == GridView6.Rows.Count)
                    {
                        NoData6.Visible = true;
                    }
                    GridView7.DataBind();
                    if (0 == GridView7.Rows.Count)
                    {
                        NoData7.Visible = true;
                    }

                    if ("" != ComFunc.UseSession(Page, "selected_SalaryTab"))
                    {
                        tab.ActiveTabIndex = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_SalaryTab"));
                    }
                }
                catch (Exception ex)
                {
                    string error_msg = @"System Error E2101";
                    ComFunc.WriteLogLocal(error_msg, ex.Message);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                }
            }
        }

        protected void Button_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee.aspx");
        }

        protected void Button_Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee_Edit.aspx");
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            string message = "";
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                string s_id = ComFunc.UseSession(Page, "selected_id");
                TB_R_USER tb = db.TB_R_USERs.SingleOrDefault(x =>
                    x.ID == s_id
                    );
                db.TB_R_USERs.DeleteOnSubmit(tb);
                db.SubmitChanges();

                message = @"System delete the data.";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);

            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E2102";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
            Response.Redirect("Employee.aspx");
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
                string error_msg = @"System Error E2103";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Before_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idBefore;
            Response.Redirect("Employee_Detail.aspx");
        }

        protected void Button_After_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idAfter;
            Response.Redirect("Employee_Detail.aspx");
        }

        protected void tab_ActiveTabChanged(object sender, EventArgs e)
        {
            Session["selected_SalaryTab"] = tab.ActiveTabIndex.ToString();
        }
    }
}