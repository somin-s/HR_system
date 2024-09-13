using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Setting_Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                    HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                    HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                    ComFunc.Language("Setting_Calendar.aspx", form1);

                    string s_currentYear = DateTime.Now.ToString("yyyy");
                    if ("" != ComFunc.UseSession(Page, "current_year"))
                    {
                        s_currentYear = ComFunc.UseSession(Page, "current_year");
                    }

                    for (int i = 0; i < DropDownListYear.Items.Count; i++)
                    { // check the index of dropdown list.
                        if (s_currentYear == DropDownListYear.Items[i].Value)
                        {
                            DropDownListYear.SelectedIndex = i;
                            break;
                        }
                    }
                    settingCalendar();
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4001";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void settingCalendar()
        {
            try
            {
                SelectedDatesCollection theDates1 = Calendar1.SelectedDates;
                SelectedDatesCollection theDates2 = Calendar2.SelectedDates;
                SelectedDatesCollection theDates3 = Calendar3.SelectedDates;
                SelectedDatesCollection theDates4 = Calendar4.SelectedDates;
                SelectedDatesCollection theDates5 = Calendar5.SelectedDates;
                SelectedDatesCollection theDates6 = Calendar6.SelectedDates;
                SelectedDatesCollection theDates7 = Calendar7.SelectedDates;
                SelectedDatesCollection theDates8 = Calendar8.SelectedDates;
                SelectedDatesCollection theDates9 = Calendar9.SelectedDates;
                SelectedDatesCollection theDates10 = Calendar10.SelectedDates;
                SelectedDatesCollection theDates11 = Calendar11.SelectedDates;
                SelectedDatesCollection theDates12 = Calendar12.SelectedDates;
                theDates1.Clear();
                theDates2.Clear();
                theDates3.Clear();
                theDates4.Clear();
                theDates5.Clear();
                theDates6.Clear();
                theDates7.Clear();
                theDates8.Clear();
                theDates9.Clear();
                theDates10.Clear();
                theDates11.Clear();
                theDates12.Clear();

                string y = DropDownListYear.SelectedValue;
                string f = y + "-01-31";
                DateTime firstMonth = Convert.ToDateTime(f);
                Calendar1.VisibleDate = firstMonth;

                DateTime m2 = firstMonth.Date.AddMonths(1);
                Calendar2.VisibleDate = m2;

                DateTime m3 = firstMonth.Date.AddMonths(2);
                Calendar3.VisibleDate = m3;

                DateTime m4 = firstMonth.Date.AddMonths(3);
                Calendar4.VisibleDate = m4;

                DateTime m5 = firstMonth.Date.AddMonths(4);
                Calendar5.VisibleDate = m5;

                DateTime m6 = firstMonth.Date.AddMonths(5);
                Calendar6.VisibleDate = m6;

                DateTime m7 = firstMonth.Date.AddMonths(6);
                Calendar7.VisibleDate = m7;

                DateTime m8 = firstMonth.Date.AddMonths(7);
                Calendar8.VisibleDate = m8;

                DateTime m9 = firstMonth.Date.AddMonths(8);
                Calendar9.VisibleDate = m9;

                DateTime m10 = firstMonth.Date.AddMonths(9);
                Calendar10.VisibleDate = m10;

                DateTime m11 = firstMonth.Date.AddMonths(10);
                Calendar11.VisibleDate = m11;

                DateTime m12 = firstMonth.Date.AddMonths(11);
                Calendar12.VisibleDate = m12;

                DataClassesDataContext db = new DataClassesDataContext();
                var tb = from x in db.TB_M_HOLIDAYs select x.DATE_HOLIDAY;
                foreach (var DATE_HOLIDAY in tb)
                {
                    DateTime tmp = DATE_HOLIDAY.Value;
                    string t = tmp.ToString("MM");


                    if (t.Substring(0, 2) == "01")
                    {
                        theDates1.Add(tmp);
                        //Calendar1.VisibleDate = firstMonth;
                    }
                    else if (t.Substring(0, 2) == "02")
                    {
                        theDates2.Add(tmp);
                        //DateTime m2 = firstMonth.Date.AddMonths(1);
                        //Calendar2.VisibleDate = m2;
                    }
                    else if (t.Substring(0, 2) == "03")
                    {
                        theDates3.Add(tmp);
                        //Calendar3.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "04")
                    {
                        theDates4.Add(tmp);
                        //Calendar4.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "05")
                    {
                        theDates5.Add(tmp);
                        //Calendar5.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "06")
                    {
                        theDates6.Add(tmp);
                        //Calendar6.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "07")
                    {
                        theDates7.Add(tmp);
                        //Calendar7.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "08")
                    {
                        theDates8.Add(tmp);
                        //Calendar8.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "09")
                    {
                        theDates9.Add(tmp);
                        //Calendar9.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "10")
                    {
                        theDates10.Add(tmp);
                        //Calendar10.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "11")
                    {
                        theDates11.Add(tmp);
                        //Calendar11.VisibleDate = tmp;
                    }
                    else if (t.Substring(0, 2) == "12")
                    {
                        theDates12.Add(tmp);
                        //Calendar12.VisibleDate = tmp;
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4002";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool b_flg = false;
            try
            {
                if (days.Text != "")
                {
                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_M_HOLIDAY tb = db.TB_M_HOLIDAYs.SingleOrDefault(x =>
                        x.DATE_HOLIDAY == new DateTime(
                                                        ComFunc.ConvertInt(DropDownListYear.SelectedValue),
                                                        ComFunc.ConvertInt(months.Text),
                                                        ComFunc.ConvertInt(days.Text)
                                                        )
                        );
                    if (null == tb)
                    {
                        TB_M_HOLIDAY tb2 = new TB_M_HOLIDAY
                        {
                            DATE_HOLIDAY = new DateTime(
                                                        ComFunc.ConvertInt(DropDownListYear.SelectedValue),
                                                        ComFunc.ConvertInt(months.Text),
                                                        ComFunc.ConvertInt(days.Text)
                                                        )
                        };
                        db.TB_M_HOLIDAYs.InsertOnSubmit(tb2);
                        db.SubmitChanges();
                    }
                    b_flg = true;
                }
                else
                {
                    string message = @"Please select a target date.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4003";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
            if (true == b_flg)
            {
                Response.Redirect("Setting_Calendar.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            bool b_flg = false;
            try
            {
                if (days.Text != "")
                {
                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_M_HOLIDAY tb = db.TB_M_HOLIDAYs.SingleOrDefault(x =>
                        x.DATE_HOLIDAY == new DateTime(
                                                        ComFunc.ConvertInt(DropDownListYear.SelectedValue),
                                                        ComFunc.ConvertInt(months.Text),
                                                        ComFunc.ConvertInt(days.Text)
                                                        )
                        );
                    if (null != tb)
                    {
                        db.TB_M_HOLIDAYs.DeleteOnSubmit(tb);
                        db.SubmitChanges();
                    }
                    b_flg = true;
                }
                else
                {
                    string message = @"Please select a target date.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + message + "');},0);", true);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4004";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
            if (true == b_flg)
            {
                Response.Redirect("Setting_Calendar.aspx");
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar1.SelectedDate.Day.ToString();
            months.Text = Calendar1.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar2.SelectedDate.Day.ToString();
            months.Text = Calendar2.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar3_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar3.SelectedDate.Day.ToString();
            months.Text = Calendar3.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar4_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar4.SelectedDate.Day.ToString();
            months.Text = Calendar4.SelectedDate.Month.ToString();
        }

        protected void Calendar5_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar5.SelectedDate.Day.ToString();
            months.Text = Calendar5.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar6_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar6.SelectedDate.Day.ToString();
            months.Text = Calendar6.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar7_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar7.SelectedDate.Day.ToString();
            months.Text = Calendar7.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar8_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar8.SelectedDate.Day.ToString();
            months.Text = Calendar8.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar9_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar9.SelectedDate.Day.ToString();
            months.Text = Calendar9.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar10_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar10.SelectedDate.Day.ToString();
            months.Text = Calendar10.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar11_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar11.SelectedDate.Day.ToString();
            months.Text = Calendar11.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void Calendar12_SelectionChanged(object sender, EventArgs e)
        {
            days.Text = Calendar12.SelectedDate.Day.ToString();
            months.Text = Calendar12.SelectedDate.Month.ToString();

            settingCalendar();
        }

        protected void DropDownListYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["current_year"] = DropDownListYear.SelectedValue;
            settingCalendar();
        }
    }
}
