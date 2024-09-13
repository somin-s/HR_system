using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ComFunction;
using System.IO;
using System.Text;

namespace BrightHRSystem
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.RemoveAll();
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){resizeTo(1250,750);},0);", true);
                Login1.Focus();

                if (0 == ComFunc.ConvertInt(ComFunc.getSetting("PassExpireDay")))
                {
                    Button1.Visible = false;
                }

                HCus.Text = "Company : " + ComFunc.getSetting("CompanyName");

                string s_cus_cd = ComFunc.getSetting("CompanyCD");
                Session["cus_cd"] = s_cus_cd;
                string s_cus_name = ComFunc.getSetting("CompanyName");
                Session["cus_name"] = s_cus_name;

                try
                {
                    string fileName = @"c:\website\history.txt";
                    Encoding SJIS = Encoding.GetEncoding("Shift_JIS");

                    foreach (string line in File.ReadLines(fileName, SJIS))
                    {
                        txtNews.Text = txtNews.Text + line + "\n";
                    }
                }
                catch (Exception ex)
                {
                    // nothing to do.
                }
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Login(0);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Login(1);
        }

        protected void Login(int i_type)
        {
            string message = "";
            bool b_flag = false;
            try
            {
                TextBox t1 = (TextBox)Login1.FindControl("CompanyCD");
                string s_Comp = "";
                if (null != t1)
                {
                    s_Comp = t1.Text;
                }
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_USER tb = db.TB_R_USERs.SingleOrDefault(x =>
                    x.ID == Login1.UserName
                    );
                if (null == tb)
                {
                    ComFunc.WriteLogLocal("LOGIN FALID USER NAME", "User Name : " + Login1.UserName + " Password : " + Login1.Password);
                    message = @"User Name is incorrect.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                    return;
                }

                if (0 == i_type)
                {
                    if (Login1.Password != tb.CURRENT_PASSWORD.Trim())
                    {
                        ComFunc.WriteLogLocal("LOGIN FALID PASSWORD", "User Name : " + Login1.UserName + " Password : " + Login1.Password);
                        message = @"Password is incorrect.";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                        return;
                    }

                    int i_year = tb.EXPIRE_PASSWORD.Value.Year;
                    int i_month = tb.EXPIRE_PASSWORD.Value.Month;
                    int i_day = tb.EXPIRE_PASSWORD.Value.Day;
                    DateTime ex_data = new DateTime(i_year, i_month, i_day);
                    if ((DateTime.Now < ex_data) || (0 == ComFunc.ConvertInt(ComFunc.getSetting("PassExpireDay"))))
                    {
                        string s_user_id = tb.ID.Trim();
                        Session["user_id"] = s_user_id;
                        Session["selected_id"] = s_user_id;
                        string s_user_name = tb.NAME;
                        Session["user_name"] = s_user_name;

                        var tb_rmd = from x in db.TB_R_MENUs where x.UserCD == s_user_id select x;
                        foreach (var x in tb_rmd)
                        {
                            db.TB_R_MENUs.DeleteOnSubmit(x);
                        }
                        db.SubmitChanges();

                        TB_R_MENU tb_rm;
                        var tb_mm = from x in db.TB_M_MENUs orderby x.ID select x;
                        foreach (var row in tb_mm)
                        {
                            string s_InnerHtml = "";
                            if ('1' == tb.LANG)
                            {
                                s_InnerHtml = row.InnerHtml_JP;
                            }
                            else if ('2' == tb.LANG)
                            {
                                s_InnerHtml = row.InnerHtml_TH;
                            }
                            else
                            {
                                s_InnerHtml = row.InnerHtml_EN;
                            }

                            tb_rm = new TB_R_MENU
                            {
                                ID = row.ID,
                                InnerHtml = s_InnerHtml,
                                Url = row.Url,
                                ParentId = row.ParentId,
                                UserCD = s_user_id
                            };

                            if (
                                ('0' == tb.AUTH1 && 2 == row.ID)
                                || ('0' == tb.AUTH2 && 3 == row.ID)
                                || ('0' == tb.AUTH6 && 4 == row.ID)
                                || ('0' == tb.AUTH4 && 5 == row.ID)
                                || ('0' == tb.AUTH8 && 6 == row.ID)
                                || ('0' == tb.AUTH3 && 204 == row.ID)
                                )
                            {
                                // not do anything.
                            }
                            else
                            {
                                db.TB_R_MENUs.InsertOnSubmit(tb_rm);
                            }
                        }
                        db.SubmitChanges();

                        Session["current_year"] = "";
                        Session["selected_year"] = "";
                        Session["selected_month"] = "";
                        Session["selected_bonus"] = "";
                        Session["selected_SalaryTab"] = "";

                        ComFunc.CreateSalaryData(s_user_id);

                        ComFunc.WriteLogLocal("LOGIN", "LOGIN");

                        b_flag = true;
                    }
                    else
                    {
                        ComFunc.WriteLogLocal("LOGIN FALID PASSWORD EXPIRE", "User Name : " + Login1.UserName + " Password : " + Login1.Password);
                        message = @"Your password was expired.\nPlease click the One Time Password button for generate a new password.";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                    }
                }
                else if (1 == i_type)
                {
                    if ((Login1.Password != ComFunc.getSetting("DefaultPass") && (Login1.Password != tb.CURRENT_PASSWORD.Trim())))
                    {
                        message = @"Password is incorrect.\nPlease input your password or Company Default password.";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                        return;
                    }
                    string pass = ComFunc.GenerateRandomChar(8);
                    DateTime exDate = DateTime.Now.AddDays(ComFunc.ConvertInt(ComFunc.getSetting("PassExpireDay")));

                    string s_return = ComFunc.sendEmail(tb.NAME, tb.EMAIL.Trim(), pass);
                    if ("" != s_return)
                    {
                        message = @"System can not send e-mail.\nPlease contact to Administrator.\n" + s_return;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                        return;
                    }

                    tb.CURRENT_PASSWORD = pass;
                    tb.EXPIRE_PASSWORD = exDate;
                    db.SubmitChanges();

                    message = @"System send a new password to your registed e-mail address.\nPlease check a your e-mail account.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E0001";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }

            if(true == b_flag)
            {
                Response.Redirect("Menu.aspx");
            }
        }
    }
}
