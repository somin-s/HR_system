using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Salary_SpecialAllowance : System.Web.UI.Page
    {
        protected static int i_d1id = 0;
        protected static int i_d2id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    i_d1id = 0;
                    i_d2id = 0;

                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                        x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                        x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")) &&
                        x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                        );

                    if (null != tb_h)
                    {
                        TB_R_PAYROLL_D tb_d = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                            x.HEADER_ID == tb_h.SEQ_ID &&
                            x.USER_ID == ComFunc.UseSession(Page, "selected_id")
                            );

                        if (null != tb_d)
                        {
                            i_d1id = tb_d.SEQ_ID;
                            TB_R_PAYROLL_D2 tb_d2 = db.TB_R_PAYROLL_D2s.SingleOrDefault(x =>
                                x.D1_ID == i_d1id
                                );
                            if (null != tb_d2)
                            {
                                i_d2id = tb_d2.SEQ_ID;
                                if (null != tb_d2)
                                {
                                    TextBox1.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL1);
                                    //TextBox10.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL2);
                                    TextBox3.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL3);
                                    TextBox4.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL4);

                                    double d_tmp = 0;
                                    if (null != tb_d2.SPECIAL5)
                                    {
                                        d_tmp = ComFunc.ConvertDouble(tb_d2.SPECIAL5.ToString());
                                        d_tmp = d_tmp / 30;
                                        DDDays1.SelectedIndex = ComFunc.ConvertInt(d_tmp.ToString());
                                    }
                                    if (null != tb_d2.SPECIAL6)
                                    {
                                        d_tmp = ComFunc.ConvertDouble(tb_d2.SPECIAL6.ToString());
                                        d_tmp = d_tmp / 30;
                                        DDDays2.SelectedIndex = ComFunc.ConvertInt(d_tmp.ToString());
                                    }
                                    if (null != tb_d2.SPECIAL7)
                                    {
                                        d_tmp = ComFunc.ConvertDouble(tb_d2.SPECIAL7.ToString());
                                        d_tmp = d_tmp / 30;
                                        DDDays3.SelectedIndex = ComFunc.ConvertInt(d_tmp.ToString());
                                    }
                                    if (null != tb_d2.SPECIAL2)
                                    {
                                        d_tmp = ComFunc.ConvertDouble(tb_d2.SPECIAL2.ToString());
                                        d_tmp = d_tmp / 50;
                                        DDDays4.SelectedIndex = ComFunc.ConvertInt(d_tmp.ToString());
                                    }
                                    ChangeDays();

                                    if (null != tb_d2.SPECIAL8)
                                    {
                                        switch (tb_d2.SPECIAL8.ToString())
                                        {
                                            case "0":
                                            case "0.00":
                                                DDGrade.SelectedIndex = 0;
                                                TextBox8.Text = tb_d2.SPECIAL8.ToString();
                                                break;
                                            case "300.00":
                                                DDGrade.SelectedIndex = 1;
                                                TextBox8.Text = tb_d2.SPECIAL8.ToString();
                                                break;
                                            case "400.00":
                                                DDGrade.SelectedIndex = 2;
                                                TextBox8.Text = tb_d2.SPECIAL8.ToString();
                                                break;
                                            case "500.00":
                                                DDGrade.SelectedIndex = 3;
                                                TextBox8.Text = tb_d2.SPECIAL8.ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    ChangeAmt();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3301";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void DDGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DDGrade.SelectedValue)
            {
                case "0":
                    TextBox8.Text = "0";
                    break;
                case "1":
                    TextBox8.Text = "300";
                    break;
                case "2":
                    TextBox8.Text = "400";
                    break;
                case "3":
                    TextBox8.Text = "500";
                    break;
                default:
                    break;
            }
            ChangeAmt();
        }

        protected void ChangeAmt(object sender, EventArgs e)
        {
            ChangeAmt();
        }

        protected void UpdateAmt(TextBox tb)
        {
            if (0 != ComFunc.ConvertDouble(tb.Text))
            {
                tb.Text = ComFunc.ConvertMoney(tb.Text);
            }
            else
            {
                tb.Text = "0";
            }
        }

        protected void ChangeAmt()
        {
            double d_total = 0;

            UpdateAmt(TextBox1);
            UpdateAmt(TextBox10);
            UpdateAmt(TextBox3);
            UpdateAmt(TextBox4);
            UpdateAmt(TextBox5);
            UpdateAmt(TextBox6);
            UpdateAmt(TextBox7);
            UpdateAmt(TextBox8);

            d_total = ComFunc.ConvertDouble(TextBox1.Text) +
                        ComFunc.ConvertDouble(TextBox10.Text) +
                        ComFunc.ConvertDouble(TextBox3.Text) +
                        ComFunc.ConvertDouble(TextBox4.Text) +
                        ComFunc.ConvertDouble(TextBox5.Text) +
                        ComFunc.ConvertDouble(TextBox6.Text) +
                        ComFunc.ConvertDouble(TextBox7.Text) +
                        ComFunc.ConvertDouble(TextBox8.Text);

            TextBox9.Text = ComFunc.ConvertMoney(d_total);
        }

        // Get from last month.
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool b_get = false;
                int i_year = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year"));
                int i_month = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month"));

                // last month;
                if (1 != i_month)
                {
                    i_month = i_month - 1;
                }
                else
                {
                    i_year = i_year - 1;
                    i_month = 12;
                }

                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.YEAR == i_year &&
                    x.MONTH == i_month &&
                    x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                    );

                if (null != tb_h)
                {
                    TB_R_PAYROLL_D tb_d = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                        x.HEADER_ID == tb_h.SEQ_ID &&
                        x.USER_ID == ComFunc.UseSession(Page, "selected_id")
                        );

                    if (null != tb_d)
                    {
                        TB_R_PAYROLL_D2 tb_d2 = db.TB_R_PAYROLL_D2s.SingleOrDefault(x =>
                            x.D1_ID == tb_d.SEQ_ID
                            );
                        if (null != tb_d2)
                        {
                            TextBox1.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL1);
                            //TextBox10.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL2);
                            TextBox3.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL3);
                            TextBox4.Text = ComFunc.ConvertMoney(tb_d2.SPECIAL4);
                            ChangeAmt();
                            b_get = true;
                        }
                    }
                }

                if (false == b_get)
                {
                    string msg = @"Do not have last month data.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + msg + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3302";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        // Get Working Record Data.
        protected void ButtonGet1_Click(object sender, EventArgs e)
        {
            try
            {
                bool b_get = false;
                DataClassesDataContext db = new DataClassesDataContext();
                int i_yearThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year"));
                int i_monthThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month"));
                int i_yearLast = 0;
                int i_monthLast = 0;
                int i_yearTarget = 0;
                int i_monthTarget = 0;
                if (1 == i_monthThis)
                {
                    i_yearLast = i_yearThis - 1;
                    i_monthLast = 12;
                }
                else
                {
                    i_yearLast = i_yearThis;
                    i_monthLast = i_monthThis - 1;
                }

                if ("This" == ComFunc.getSetting("WRTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }
                TB_R_WORKINGREPORT_H tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                   x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                   x.YEAH == i_yearTarget.ToString() &&
                   x.MONTH == i_monthTarget.ToString()
                   );

                if ("This" == ComFunc.getSetting("OTTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }

                int i_ActWorkDay = 0;

                var tb_wd = from y in db.TB_R_WORKINGREPORT_Ds
                            where y.HEADER_ID == tb_wh1.seqID.ToString()
                            select y;
                if (null != tb_wd)
                {
                    foreach (var row in tb_wd)
                    {
                        if ("" != ComFunc.ConvertStr(row.APPLY_STARTING))
                        {
                            i_ActWorkDay = i_ActWorkDay + 1;
                        }
                    }

                    DDDays1.SelectedIndex = i_ActWorkDay;

                    ChangeDays();
                    b_get = true;
                }

                if (false == b_get)
                {
                    string msg = @"Do not have Working Record data.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + msg + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3303";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void ButtonGet2_Click(object sender, EventArgs e)
        {
            try
            {
                bool b_get = false;
                DataClassesDataContext db = new DataClassesDataContext();
                int i_yearThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year"));
                int i_monthThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month"));
                int i_yearLast = 0;
                int i_monthLast = 0;
                int i_yearTarget = 0;
                int i_monthTarget = 0;
                if (1 == i_monthThis)
                {
                    i_yearLast = i_yearThis - 1;
                    i_monthLast = 12;
                }
                else
                {
                    i_yearLast = i_yearThis;
                    i_monthLast = i_monthThis - 1;
                }

                if ("This" == ComFunc.getSetting("WRTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }
                TB_R_WORKINGREPORT_H tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                   x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                   x.YEAH == i_yearTarget.ToString() &&
                   x.MONTH == i_monthTarget.ToString()
                   );

                if ("This" == ComFunc.getSetting("OTTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }

                int i_ActWorkDay = 0;

                var tb_wd = from y in db.TB_R_WORKINGREPORT_Ds
                            where y.HEADER_ID == tb_wh1.seqID.ToString()
                            select y;
                if (null != tb_wd)
                {
                    foreach (var row in tb_wd)
                    {
                        if ("" != ComFunc.ConvertStr(row.APPLY_STARTING))
                        {
                            i_ActWorkDay = i_ActWorkDay + 1;
                        }
                    }

                    DDDays2.SelectedIndex = i_ActWorkDay;

                    ChangeDays();
                    b_get = true;
                }

                if (false == b_get)
                {
                    string msg = @"Do not have Working Record data.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + msg + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3303";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void ButtonGet3_Click(object sender, EventArgs e)
        {
            try
            {
                bool b_get = false;
                DataClassesDataContext db = new DataClassesDataContext();
                int i_yearThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year"));
                int i_monthThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month"));
                int i_yearLast = 0;
                int i_monthLast = 0;
                int i_yearTarget = 0;
                int i_monthTarget = 0;
                if (1 == i_monthThis)
                {
                    i_yearLast = i_yearThis - 1;
                    i_monthLast = 12;
                }
                else
                {
                    i_yearLast = i_yearThis;
                    i_monthLast = i_monthThis - 1;
                }

                if ("This" == ComFunc.getSetting("WRTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }
                TB_R_WORKINGREPORT_H tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                   x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                   x.YEAH == i_yearTarget.ToString() &&
                   x.MONTH == i_monthTarget.ToString()
                   );

                if ("This" == ComFunc.getSetting("OTTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }

                int i_ActWorkDay = 0;

                var tb_wd = from y in db.TB_R_WORKINGREPORT_Ds
                            where y.HEADER_ID == tb_wh1.seqID.ToString()
                            select y;
                if (null != tb_wd)
                {
                    foreach (var row in tb_wd)
                    {
                        if ("" != ComFunc.ConvertStr(row.APPLY_STARTING))
                        {
                            i_ActWorkDay = i_ActWorkDay + 1;
                        }
                    }

                    DDDays3.SelectedIndex = i_ActWorkDay;

                    ChangeDays();
                    b_get = true;
                }

                if (false == b_get)
                {
                    string msg = @"Do not have Working Record data.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + msg + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3303";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void ButtonGet4_Click(object sender, EventArgs e)
        {
            try
            {
                bool b_get = false;
                DataClassesDataContext db = new DataClassesDataContext();
                int i_yearThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year"));
                int i_monthThis = ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month"));
                int i_yearLast = 0;
                int i_monthLast = 0;
                int i_yearTarget = 0;
                int i_monthTarget = 0;
                if (1 == i_monthThis)
                {
                    i_yearLast = i_yearThis - 1;
                    i_monthLast = 12;
                }
                else
                {
                    i_yearLast = i_yearThis;
                    i_monthLast = i_monthThis - 1;
                }

                if ("This" == ComFunc.getSetting("WRTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }
                TB_R_WORKINGREPORT_H tb_wh1 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                   x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                   x.YEAH == i_yearTarget.ToString() &&
                   x.MONTH == i_monthTarget.ToString()
                   );

                if ("This" == ComFunc.getSetting("OTTargetMonth"))
                {
                    i_yearTarget = i_yearThis;
                    i_monthTarget = i_monthThis;
                }
                else
                {
                    i_yearTarget = i_yearLast;
                    i_monthTarget = i_monthLast;
                }

                int i_ActWorkDay = 0;

                var tb_wd = from y in db.TB_R_WORKINGREPORT_Ds
                            where y.HEADER_ID == tb_wh1.seqID.ToString()
                            select y;
                if (null != tb_wd)
                {
                    foreach (var row in tb_wd)
                    {
                        if ("" != ComFunc.ConvertStr(row.APPLY_STARTING))
                        {
                            i_ActWorkDay = i_ActWorkDay + 1;
                        }
                    }

                    DDDays4.SelectedIndex = i_ActWorkDay;

                    ChangeDays();
                    b_get = true;
                }

                if (false == b_get)
                {
                    string msg = @"Do not have Working Record data.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + msg + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3303";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        // Save.
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                Session["DetailAllowanceAmt"] = TextBox9.Text;

                DataClassesDataContext db = new DataClassesDataContext();
                if (0 == i_d2id)
                {
                    TB_R_PAYROLL_D2 tb_d2 = new TB_R_PAYROLL_D2
                    {
                        D1_ID = i_d1id,
                        SPECIAL1 = ComFunc.ConvertDecimal(TextBox1.Text),
                        SPECIAL2 = ComFunc.ConvertDecimal(TextBox10.Text),
                        SPECIAL3 = ComFunc.ConvertDecimal(TextBox3.Text),
                        SPECIAL4 = ComFunc.ConvertDecimal(TextBox4.Text),
                        SPECIAL5 = ComFunc.ConvertDecimal(TextBox5.Text),
                        SPECIAL6 = ComFunc.ConvertDecimal(TextBox6.Text),
                        SPECIAL7 = ComFunc.ConvertDecimal(TextBox7.Text),
                        SPECIAL8 = ComFunc.ConvertDecimal(TextBox8.Text),
                        UPDATE_DATE = DateTime.Now,
                        UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                        CREATE_DATE = DateTime.Now,
                        CREATE_BY = ComFunc.UseSession(Page, "user_id")
                    };
                    db.TB_R_PAYROLL_D2s.InsertOnSubmit(tb_d2);
                    db.SubmitChanges();
                }
                else
                {
                    TB_R_PAYROLL_D2 tb_d2 = db.TB_R_PAYROLL_D2s.SingleOrDefault(x =>
                        x.SEQ_ID == i_d2id
                        );

                    if (null != tb_d2)
                    {
                        tb_d2.SPECIAL1 = ComFunc.ConvertDecimal(TextBox1.Text);
                        tb_d2.SPECIAL2 = ComFunc.ConvertDecimal(TextBox10.Text);
                        tb_d2.SPECIAL3 = ComFunc.ConvertDecimal(TextBox3.Text);
                        tb_d2.SPECIAL4 = ComFunc.ConvertDecimal(TextBox4.Text);
                        tb_d2.SPECIAL5 = ComFunc.ConvertDecimal(TextBox5.Text);
                        tb_d2.SPECIAL6 = ComFunc.ConvertDecimal(TextBox6.Text);
                        tb_d2.SPECIAL7 = ComFunc.ConvertDecimal(TextBox7.Text);
                        tb_d2.SPECIAL8 = ComFunc.ConvertDecimal(TextBox8.Text);
                        tb_d2.UPDATE_DATE = DateTime.Now;
                        tb_d2.UPDATE_BY = ComFunc.UseSession(Page, "user_id");

                        db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3304";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void DDDays1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDays();
        }

        protected void DDDays2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDays();
        }

        protected void DDDays3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDays();
        }

        protected void DDDays4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDays();
        }

        protected void ChangeDays()
        {
            try
            {
                int days1 = ComFunc.ConvertInt(DDDays1.SelectedValue);
                int days2 = ComFunc.ConvertInt(DDDays2.SelectedValue);
                int days3 = ComFunc.ConvertInt(DDDays3.SelectedValue);
                int days4 = ComFunc.ConvertInt(DDDays4.SelectedValue);

                TextBox5.Text = ComFunc.ConvertMoney((days1 * 30).ToString());
                TextBox6.Text = ComFunc.ConvertMoney((days2 * 30).ToString());
                TextBox7.Text = ComFunc.ConvertMoney((days3 * 30).ToString());
                TextBox10.Text = ComFunc.ConvertMoney((days4 * 50).ToString());

                ChangeAmt();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3305";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }
    }
}