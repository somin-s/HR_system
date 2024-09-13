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
    public partial class Salary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                    ComFunc.Language("Salary.aspx", form1);
                    Button_Print.OnClientClick = ComFunc.getMessage("C001");

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SEQ_ID");
                    dt.Columns.Add("TEXT");

                    DataClassesDataContext db = new DataClassesDataContext();
                    DataRow dt_row;
                    string s_selected = "";
                    var tb = from a in db.TB_R_PAYROLL_Hs orderby a.SEQ_ID descending select a;

                    foreach (var row in tb)
                    {
                        if ("" == s_selected)
                        {
                            s_selected = row.SEQ_ID.ToString();
                        }

                        DateTime d_target = new DateTime(row.YEAR.Value, row.MONTH.Value, 1);
                        dt_row = dt.NewRow();
                        dt_row["SEQ_ID"] = row.SEQ_ID.ToString();
                        if (1 == row.BONUS)
                        {
                            if (1 == ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")))
                            {
                                dt_row["TEXT"] = "Bonus " + d_target.ToString("yyyy");
                            }
                            else
                            {
                                dt_row["TEXT"] = "Bonus " + d_target.AddYears(-1).ToString("yyyy");
                            }
                        }
                        else
                        {
                            dt_row["TEXT"] = d_target.ToString("MMM") + "-" + d_target.ToString("yyyy") + " Salary Data";
                        }
                        dt.Rows.Add(dt_row);
                    }
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataBind();

                    if ("" != ComFunc.UseSession(Page, "selected_year") ||
                        "" != ComFunc.UseSession(Page, "selected_month") ||
                        "" != ComFunc.UseSession(Page, "selected_bonus"))
                    {
                        TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                                x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                                x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")) &&
                                x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                            );
                        s_selected = tb_h.SEQ_ID.ToString();
                        DropDownList1.SelectedValue = s_selected;
                    }

                    UpdateScreen(s_selected);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3001";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void UpdateScreen(string s_SeqID)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.SEQ_ID == ComFunc.ConvertInt(s_SeqID)
                    );

                DateTime d_target = new DateTime(tb_h.YEAR.Value, tb_h.MONTH.Value, 1);
                if (1 == tb_h.BONUS)
                {
                    if (1 == ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")))
                    {
                        Label1.Text = "Bonus " + d_target.ToString("yyyy");
                    }
                    else
                    {
                        Label1.Text = "Bonus " + d_target.AddYears(-1).ToString("yyyy");
                    }
                }
                else
                {
                    Label1.Text = d_target.ToString("MMM") + "-" + d_target.ToString("yyyy") + " Salary Data";
                }

                int iDaysInMonth = DateTime.DaysInMonth(d_target.Year, d_target.Month);

                int i_Payday = 0;
                if ("End" == ComFunc.getSetting("S_PayDay") ||
                    "B" == ComFunc.getSetting("S_PayDay").Substring(0, 1))
                {
                    i_Payday = iDaysInMonth;
                }
                else
                {
                    i_Payday = ComFunc.ConvertInt(ComFunc.getSetting("S_PayDay"));
                }

                if ("Y" == ComFunc.getSetting("SkipHoldayForPayment"))
                {
                    DateTime dt;
                    TB_M_HOLIDAY tb_ho;
                    bool b_loop = true;
                    while (true == b_loop)
                    {
                        dt = new DateTime(tb_h.YEAR.Value, tb_h.MONTH.Value, i_Payday);
                        tb_ho = db.TB_M_HOLIDAYs.SingleOrDefault(x =>
                            x.DATE_HOLIDAY == dt
                            );
                        if (null == tb_ho || 1 > i_Payday)
                        {
                            b_loop = false;
                        }
                        else
                        {
                            i_Payday--;
                        }
                    }
                }

                int i_before = 0;
                if ("B" == ComFunc.getSetting("S_PayDay").Substring(0, 1))
                {
                    i_before = ComFunc.ConvertInt(ComFunc.getSetting("S_PayDay").Substring(1));
                    i_Payday = i_Payday - i_before;
                    if ("Y" == ComFunc.getSetting("SkipHoldayForPayment"))
                    {
                        DateTime dt;
                        TB_M_HOLIDAY tb_ho;
                        bool b_loop = true;
                        while (true == b_loop)
                        {
                            dt = new DateTime(tb_h.YEAR.Value, tb_h.MONTH.Value, i_Payday);
                            tb_ho = db.TB_M_HOLIDAYs.SingleOrDefault(x =>
                                x.DATE_HOLIDAY == dt
                                );
                            if (null == tb_ho || 1 > i_Payday)
                            {
                                b_loop = false;
                            }
                            else
                            {
                                i_Payday--;
                            }
                        }
                    }
                }

                int i_Startday = 0;
                int i_Closeday = 0;
                if ("End" == ComFunc.getSetting("S_CloseDay"))
                {
                    i_Closeday = iDaysInMonth;
                    i_Startday = 1;
                }
                else
                {
                    i_Closeday = ComFunc.ConvertInt(ComFunc.getSetting("S_CloseDay"));
                    i_Startday = i_Closeday + 1;
                }

                Label5.Text = i_Payday.ToString() + "-" + d_target.ToString("MMM") + ", " + d_target.ToString("yyyy");
                Session["PayDay"] = Label5.Text;

                string s_Term = "";
                if (0 == tb_h.BONUS.Value)
                {
                    if (1 == i_Startday)
                    {
                        s_Term = s_Term + i_Startday.ToString() + "-" + d_target.ToString("MMM") + ", " + d_target.ToString("yyyy");
                    }
                    else
                    {
                        s_Term = s_Term + i_Startday.ToString() + "-" + d_target.AddMonths(-1).ToString("MMM") + ", " + d_target.AddMonths(-1).ToString("yyyy");
                    }
                    s_Term = s_Term + " - ";
                    s_Term = s_Term + i_Closeday.ToString() + "-" + d_target.ToString("MMM") + ", " + d_target.ToString("yyyy");
                }
                else
                {
                    if(1 == ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")))
                    {
                        s_Term = s_Term + "1-Jan, " + d_target.ToString("yyyy");
                        s_Term = s_Term + " - ";
                        s_Term = s_Term + "31-Dec, " + d_target.ToString("yyyy");
                    }
                    else
                    {
                        s_Term = s_Term + "1-" + d_target.AddMonths(1).ToString("MMM") + ", " + d_target.AddYears(-1).ToString("yyyy");
                        s_Term = s_Term + " - ";
                        s_Term = s_Term + DateTime.DaysInMonth(d_target.Year, d_target.Month) + "-" + 
                                    d_target.ToString("MMM") + ", " + 
                                    d_target.ToString("yyyy");
                    }
                }
                Label7.Text = s_Term;
                Session["PayTerm"] = s_Term;

                decimal d_Income = 0;
                decimal d_Paid = 0;
                decimal d_Handover = 0;
                decimal d_Transfer = 0;
                var tb_d = from x in db.TB_R_PAYROLL_Ds where x.HEADER_ID == tb_h.SEQ_ID select x;
                foreach (var row in tb_d)
                {
                    if (null != row.SALARY_PAYMENT)
                    {
                        d_Income = d_Income + row.INCOME_TOTAL.Value;
                        d_Paid = d_Paid + row.SALARY_PAYMENT.Value;
                        d_Handover = d_Handover + row.HANDOVER.Value;
                        d_Transfer = d_Transfer + row.TRANSFER.Value;
                    }
                }
                Label_Income.Text = ComFunc.ConvertMoney(d_Income.ToString());
                Label_Paid.Text = ComFunc.ConvertMoney(d_Paid.ToString());
                Label_Handover.Text = ComFunc.ConvertMoney(d_Handover.ToString());
                Label_Transfer.Text = ComFunc.ConvertMoney(d_Transfer.ToString());

                Session["selected_year"] = tb_h.YEAR.Value.ToString();
                Session["selected_month"] = tb_h.MONTH.Value.ToString();
                Session["selected_bonus"] = tb_h.BONUS.Value.ToString();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3002";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Next_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                var tb_seq = from a in db.TB_R_PAYROLL_Hs
                             select a.SEQ_ID;

                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.SEQ_ID == tb_seq.Max()
                    );

                int i_year = 0;
                int i_month = 0;
                int i_bonus = 0;

                if (12 == tb_h.MONTH.Value)
                {
                    i_year = tb_h.YEAR.Value + 1;
                    i_month = 1;
                }
                else
                {
                    i_year = tb_h.YEAR.Value;
                    i_month = tb_h.MONTH.Value + 1;
                }

                if (i_month == ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")) &&
                    "Y" == ComFunc.getSetting("BonusSlip") &&
                    0 == tb_h.BONUS)
                {
                    i_year = tb_h.YEAR.Value;
                    i_month = tb_h.MONTH.Value;
                    i_bonus = 1;
                }

                // create header data.
                TB_R_PAYROLL_H tb_h_new = new TB_R_PAYROLL_H
                {
                    YEAR = i_year,
                    MONTH = i_month,
                    BONUS = i_bonus,
                    UPDATE_DATE = DateTime.Now,
                    UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                    CREATE_DATE = DateTime.Now,
                    CREATE_BY = ComFunc.UseSession(Page, "user_id")
                };
                db.TB_R_PAYROLL_Hs.InsertOnSubmit(tb_h_new);
                db.SubmitChanges();

                tb_seq = from a in db.TB_R_PAYROLL_Hs
                         select a.SEQ_ID;

                tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.SEQ_ID == tb_seq.Max()
                    );

                // create detail data.
                var tb_u = from x in db.TB_R_USERs orderby x.ID where x.TERMINATION_DT == null select x;
                foreach (var row in tb_u)
                {
                    TB_R_PAYROLL_D tb_d = new TB_R_PAYROLL_D
                    {
                        HEADER_ID = tb_h.SEQ_ID,
                        USER_ID = row.ID,
                        CONDITION = "Not entered",
                        UPDATE_DATE = DateTime.Now,
                        UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                        CREATE_DATE = DateTime.Now,
                        CREATE_BY = ComFunc.UseSession(Page, "user_id")
                    };
                    db.TB_R_PAYROLL_Ds.InsertOnSubmit(tb_d);
                }
                db.SubmitChanges();

                UpdateScreen(tb_h.SEQ_ID.ToString());
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3003";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selected_id"] = GridView1.SelectedRow.Cells[1].Text.Trim();
            Response.Redirect("Salary_Detail.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateScreen(DropDownList1.SelectedValue);
        }

        protected string s_FileType = "SalaryList";
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

        protected void Button_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                // create detail data.
                DataClassesDataContext db = new DataClassesDataContext();
                var tb_u = from x in db.TB_R_USERs orderby x.ID where x.TERMINATION_DT == null select x;
                int i_header = ComFunc.ConvertInt(DropDownList1.SelectedValue);
                foreach (var row in tb_u)
                {
                    TB_R_PAYROLL_D tb_d1 = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                        x.HEADER_ID == i_header &&
                        x.USER_ID == row.ID
                        );

                    if (null == tb_d1)
                    {
                        TB_R_PAYROLL_D tb_d2 = new TB_R_PAYROLL_D
                        {
                            HEADER_ID = i_header,
                            USER_ID = row.ID,
                            CONDITION = "Not entered",
                            UPDATE_DATE = DateTime.Now,
                            UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                            CREATE_DATE = DateTime.Now,
                            CREATE_BY = ComFunc.UseSession(Page, "user_id")
                        };
                        db.TB_R_PAYROLL_Ds.InsertOnSubmit(tb_d2);
                    }
                }
                db.SubmitChanges();

                GridView1.DataBind();
                UpdateScreen(DropDownList1.SelectedValue);
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3004";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Print_Click(object sender, EventArgs e)
        {
            switch (DropDownListExcel.SelectedValue)
            {
                case "0":
                    Download_Report(Generate_Report());
                    break;
                case "1":
                    Download_Report2(Generate_Report2(), "Payoff");
                    break;
                case "2":
                    Download_Report2(Generate_Report3(), "RM01");
                    break;
                case "3":
                    Download_Report2(Generate_Report4(), "RM05");
                    break;
                default:
                    break;
            }
        }

        protected void Button_Paid_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                    x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month"))
                    );
                TB_R_PAYROLL_D tb_d;

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (0 != ComFunc.ConvertDouble(row.Cells[8].Text))
                    {
                        tb_d = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                                        x.HEADER_ID == tb_h.SEQ_ID &&
                                        x.USER_ID == row.Cells[1].Text
                                        );
                        if (null != tb_d)
                        {
                            tb_d.PAID_F = 'Y';
                            tb_d.CONDITION = "Paid";
                            db.SubmitChanges();
                        }
                    }
                }
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3005";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        #region download excel list.
        private ExcelCreator.XlsCreator xlsCreator1 = new ExcelCreator.XlsCreator();

        protected string Generate_Report()
        {
            string s_tmpReport = "SalaryList.xls";
            string TmplateFilePath = ComFunc.getSetting("ReportPath") + s_tmpReport;
            string sDateTime = System.DateTime.Now.ToString();
            sDateTime = sDateTime.Replace("/", "");
            sDateTime = sDateTime.Replace(":", "");
            sDateTime = sDateTime.Replace(" ", "");
            string sFilePath = ComFunc.getSetting("TempPath") + "SalaryList_" + sDateTime + ".xls";

            try
            {
                // copy template to temporary folder.
                File.Copy(TmplateFilePath, sFilePath);
                xlsCreator1.OpenBook(sFilePath, "");

                DateTime d_target = new DateTime(ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")),
                                                    ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")), 1);
                xlsCreator1.Cell("**DATE").Str = d_target.ToString("MMM") + "-" + d_target.ToString("yyyy");
                xlsCreator1.Cell("**CREATEON").Str = DateTime.Now.ToString(ComFunc.getSetting("DateFormat"));

                if ("T001" == ComFunc.getSetting("CompanyCD"))
                {
                    // for Tsuchiya.

                    xlsCreator1.Cell("**BSAL").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL1").Str = "Special Allowance";
                    xlsCreator1.Cell("**SPAL2").Str = "Travel Allowance";
                    xlsCreator1.Cell("**SPAL3").Str = "House Allowance";
                    xlsCreator1.Cell("**SPAL4").Str = "Other Allowance";
                    xlsCreator1.Cell("**SPAL5").Str = "Food Allowance";
                    xlsCreator1.Cell("**SPAL6").Str = "Night Allowance";
                    xlsCreator1.Cell("**SPAL7").Str = "Environment Allowance";
                    xlsCreator1.Cell("**SPAL8").Str = "Complete Allowance";
                    xlsCreator1.Cell("**SPAL9").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL10").ColWidth = 0;
                }
                else
                {
                    xlsCreator1.Cell("**BSAL").Str = "Basic Allowance";
                    xlsCreator1.Cell("**SPAL1").Str = "Special Allowance";

                    xlsCreator1.Cell("**SPAL2").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL3").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL4").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL5").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL6").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL7").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL8").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL9").ColWidth = 0;
                    xlsCreator1.Cell("**SPAL10").ColWidth = 0;
                }

                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                        x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                        x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")) &&
                        x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                    );
                var tb_d = from x in db.TB_R_PAYROLL_Ds where x.HEADER_ID == tb_h.SEQ_ID orderby x.USER_ID select x;
                int i_row = 6;
                foreach (var row in tb_d)
                {
                    TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                        x.ID == row.USER_ID
                        );
                    if (null != tb_u)
                    {
                        if (0 != ComFunc.ConvertDouble(row.USER_ID))
                        {
                            xlsCreator1.Pos(0, i_row).Value = ComFunc.ConvertDouble(row.USER_ID);
                        }
                        else
                        {
                            xlsCreator1.Pos(0, i_row).Str = row.USER_ID;
                        }

                        xlsCreator1.Pos(1, i_row).Str = tb_u.NAME;

                        // working data.
                        xlsCreator1.Pos(2, i_row).Value = row.WORKING_DAYS;
                        xlsCreator1.Pos(3, i_row).Value = row.ABCENSE_DAYS;
                        xlsCreator1.Pos(4, i_row).Value = row.PAID_HOLIDAY_DAYS;
                        xlsCreator1.Pos(5, i_row).Value = row.WORKING_TIMES;
                        xlsCreator1.Pos(6, i_row).Value = row.NORMALOTTIME;
                        xlsCreator1.Pos(7, i_row).Value = row.HOLIDAYWORKTIME;
                        xlsCreator1.Pos(8, i_row).Value = row.HOLIDAYOTTIME;

                        // Exemptions.
                        const double MAX_INCOME_FROM_EMPLOYMENT = 60000;
                        const double RATE_INCOME_FROM_EMPLOYMENT = 0.4;
                        const double PERSONAL_ALLOWANCE = 30000;
                        const double SPOUSE_ALLOWANCE = 30000;
                        const double CHILD_ALLOWANCE = 15000;
                        const double EDUCATION_ALLOWANCE = 2000;
                        const double PARENTS_ALLOWANCE = 30000;
                        const double MAX_LIFE_INSURANCE = 100000;
                        const double MAX_PROVIDENT_FUND = 500000;
                        const double MAX_EQUITY_FUND = 500000;
                        const double MAX_HOME_MORTGAGE = 100000;

                        double d_YearSalary_Fix = ComFunc.ConvertDouble(ComFunc.ConvertMoney(row.YEARS_FIXED));
                        double d_YearSalary_Exp = ComFunc.ConvertDouble(ComFunc.ConvertMoney(row.YEARS_EXPECTATION));
                        double d_Employment = (d_YearSalary_Fix + d_YearSalary_Exp) * RATE_INCOME_FROM_EMPLOYMENT;
                        d_Employment = MAX_INCOME_FROM_EMPLOYMENT < d_Employment ? MAX_INCOME_FROM_EMPLOYMENT : d_Employment;
                        bool b_Spouse_Allowance = false;
                        if ('Y' == row.SPOUSE_ALLOWANCE)
                        {
                            b_Spouse_Allowance = true;
                        }
                        else
                        {
                            b_Spouse_Allowance = false;
                        }
                        double d_Spouse = true == b_Spouse_Allowance ? SPOUSE_ALLOWANCE : 0;
                        double d_Child = CHILD_ALLOWANCE * ComFunc.ConvertDouble(row.CHILD_ALLOWANCE);
                        double d_Educaion = EDUCATION_ALLOWANCE * ComFunc.ConvertDouble(row.EDUCATION_ALLOWANCE);
                        double d_Parents = PARENTS_ALLOWANCE * ComFunc.ConvertDouble(row.PARENTS_ALLOWANCE);
                        double d_LifeInsurance = ComFunc.ConvertDouble(row.LIFE_INSURANCE);
                        d_LifeInsurance = MAX_LIFE_INSURANCE < d_LifeInsurance ? MAX_LIFE_INSURANCE : d_LifeInsurance;
                        double d_ProvidentFund = ComFunc.ConvertDouble(row.APPROVED_PROVIDENT);
                        d_ProvidentFund = MAX_PROVIDENT_FUND < d_ProvidentFund ? MAX_PROVIDENT_FUND : d_ProvidentFund;
                        double d_EquityFund = ComFunc.ConvertDouble(row.LONG_TERM_EQUITY);
                        d_EquityFund = MAX_EQUITY_FUND < d_EquityFund ? MAX_EQUITY_FUND : d_EquityFund;
                        double d_HomeMortgage = ComFunc.ConvertDouble(row.HOME_MORTGAGE);
                        d_HomeMortgage = MAX_HOME_MORTGAGE < d_HomeMortgage ? MAX_HOME_MORTGAGE : d_HomeMortgage;
                        double d_SocialInsurance = ComFunc.ConvertDouble(row.SOCIAL);
                        double d_Charitable = ComFunc.ConvertDouble(row.CHARITABLE);

                        double d_TaxableIncome = (d_YearSalary_Fix + d_YearSalary_Exp) - (
                            d_Employment + PERSONAL_ALLOWANCE + d_Spouse + d_Child + d_Educaion + d_Parents + d_LifeInsurance +
                            d_ProvidentFund + d_EquityFund + d_HomeMortgage + d_SocialInsurance + d_Charitable);
                        if (0 > d_TaxableIncome)
                        {
                            d_TaxableIncome = 0;
                        }

                        // 0%. 0 - 150,000.
                        double d_Tax1 = 0;
                        // 5%. 150,001 - 300,000.
                        double d_Tax2 = 0;
                        if (d_TaxableIncome >= 150001 && 300000 > d_TaxableIncome)
                        {
                            d_Tax2 = (d_TaxableIncome - 150000) * 0.05;
                        }
                        else if (300000 <= d_TaxableIncome)
                        {
                            d_Tax2 = 150000 * 0.05;
                        }
                        // 10%. 300,001 - 500,000.
                        double d_Tax3 = 0;
                        if (d_TaxableIncome >= 300001 && 500000 > d_TaxableIncome)
                        {
                            d_Tax3 = (d_TaxableIncome - 300000) * 0.1;
                        }
                        else if (500000 <= d_TaxableIncome)
                        {
                            d_Tax3 = 200000 * 0.1;
                        }
                        // 15%. 500,001 - 750,000.
                        double d_Tax4 = 0;
                        if (d_TaxableIncome >= 500001 && 750000 > d_TaxableIncome)
                        {
                            d_Tax4 = (d_TaxableIncome - 500000) * 0.15;
                        }
                        else if (750000 <= d_TaxableIncome)
                        {
                            d_Tax4 = 250000 * 0.15;
                        }
                        // 20%. 750,001 - 1,000,000.
                        double d_Tax5 = 0;
                        if (d_TaxableIncome >= 750001 && 1000000 > d_TaxableIncome)
                        {
                            d_Tax5 = (d_TaxableIncome - 750000) * 0.2;
                        }
                        else if (1000000 <= d_TaxableIncome)
                        {
                            d_Tax5 = 250000 * 0.2;
                        }
                        // 25%. 1,000,001 - 2,000,000.
                        double d_Tax6 = 0;
                        if (d_TaxableIncome >= 1000001 && 2000000 > d_TaxableIncome)
                        {
                            d_Tax6 = (d_TaxableIncome - 1000000) * 0.25;
                        }
                        else if (2000000 <= d_TaxableIncome)
                        {
                            d_Tax6 = 1000000 * 0.25;
                        }
                        // 30%. 2,000,001 - 4,000,000.
                        double d_Tax7 = 0;
                        if (d_TaxableIncome >= 2000001 && 4000000 > d_TaxableIncome)
                        {
                            d_Tax7 = (d_TaxableIncome - 2000000) * 0.3;
                        }
                        else if (4000000 <= d_TaxableIncome)
                        {
                            d_Tax7 = 2000000 * 0.3;
                        }
                        // 35%. 4,000,001 - .
                        double d_Tax8 = 0;
                        if (4000000 <= d_TaxableIncome)
                        {
                            d_Tax8 = (d_TaxableIncome - 4000000) * 0.35;
                        }
                        double d_PersonalIncomeTax = d_Tax1 + d_Tax2 + d_Tax3 + d_Tax4 + d_Tax5 + d_Tax6 + d_Tax7 + d_Tax8;
                        double d_PersonalIncomeTaxMonth = d_PersonalIncomeTax / 12;

                        xlsCreator1.Pos(9, i_row).Value = d_YearSalary_Fix + d_YearSalary_Exp;
                        xlsCreator1.Pos(10, i_row).Value = d_Employment;
                        xlsCreator1.Pos(11, i_row).Value = 30000;
                        xlsCreator1.Pos(12, i_row).Value = d_Spouse;
                        xlsCreator1.Pos(13, i_row).Value = d_Child;
                        xlsCreator1.Pos(14, i_row).Value = d_Educaion;
                        xlsCreator1.Pos(15, i_row).Value = d_Parents;
                        xlsCreator1.Pos(16, i_row).Value = d_LifeInsurance;
                        xlsCreator1.Pos(17, i_row).Value = d_ProvidentFund;
                        xlsCreator1.Pos(18, i_row).Value = d_EquityFund;
                        xlsCreator1.Pos(19, i_row).Value = d_HomeMortgage;
                        xlsCreator1.Pos(20, i_row).Value = d_SocialInsurance;
                        xlsCreator1.Pos(21, i_row).Value = d_Charitable;

                        xlsCreator1.Pos(22, i_row).Value = d_TaxableIncome;
                        xlsCreator1.Pos(23, i_row).Value = d_Tax1;
                        xlsCreator1.Pos(24, i_row).Value = d_Tax2;
                        xlsCreator1.Pos(25, i_row).Value = d_Tax3;
                        xlsCreator1.Pos(26, i_row).Value = d_Tax4;
                        xlsCreator1.Pos(27, i_row).Value = d_Tax5;
                        xlsCreator1.Pos(28, i_row).Value = d_Tax6;
                        xlsCreator1.Pos(29, i_row).Value = d_Tax7;
                        xlsCreator1.Pos(30, i_row).Value = d_Tax8;
                        xlsCreator1.Pos(31, i_row).Value = d_PersonalIncomeTax;
                        xlsCreator1.Pos(32, i_row).Value = d_PersonalIncomeTaxMonth;

                        // income.
                        xlsCreator1.Pos(33, i_row).Value = row.BASIC_SALARY;
                        xlsCreator1.Pos(34, i_row).Value = row.UNIT_PRICE;
                        xlsCreator1.Pos(35, i_row).Value = row.OT_PRICE;
                        xlsCreator1.Pos(36, i_row).Value = row.HOLIDAY_WORK_PRICE;
                        xlsCreator1.Pos(37, i_row).Value = row.HOLIDAY_OT_PRICE;

                        if ("T001" == ComFunc.getSetting("CompanyCD"))
                        {
                            // for Tsuchiya.
                            double d_baseT = 0;
                            double d_FoodT = 0;
                            double d_TravelT = 0;
                            double d_HouseT = 0;
                            double d_EnvironmentT = 0;
                            double d_NightT = 0;
                            double d_CompleteT = 0;
                            double d_OtherT = 0;

                            TB_R_PAYROLL_D2 tb_d2 = db.TB_R_PAYROLL_D2s.SingleOrDefault(x =>
                                x.D1_ID == row.SEQ_ID
                                );
                            if (null != tb_d2)
                            {
                                d_baseT = ComFunc.ConvertDouble(tb_d2.SPECIAL1);
                                d_TravelT = ComFunc.ConvertDouble(tb_d2.SPECIAL2);
                                d_HouseT = ComFunc.ConvertDouble(tb_d2.SPECIAL3);
                                d_OtherT = ComFunc.ConvertDouble(tb_d2.SPECIAL4);
                                d_FoodT = ComFunc.ConvertDouble(tb_d2.SPECIAL5);
                                d_EnvironmentT = ComFunc.ConvertDouble(tb_d2.SPECIAL6);
                                d_NightT = ComFunc.ConvertDouble(tb_d2.SPECIAL7);
                                d_CompleteT = ComFunc.ConvertDouble(tb_d2.SPECIAL8);
                            }
                            
                            xlsCreator1.Pos(39, i_row).Value = row.SPECIAL_ALLOWANCE;
                            xlsCreator1.Pos(40, i_row).Value = d_TravelT;
                            xlsCreator1.Pos(41, i_row).Value = d_HouseT;
                            xlsCreator1.Pos(42, i_row).Value = d_OtherT;
                            xlsCreator1.Pos(43, i_row).Value = d_FoodT;
                            xlsCreator1.Pos(44, i_row).Value = d_EnvironmentT;
                            xlsCreator1.Pos(45, i_row).Value = d_NightT;
                            xlsCreator1.Pos(46, i_row).Value = d_CompleteT;
                        }
                        else
                        {
                            xlsCreator1.Pos(38, i_row).Value = row.BASIC_ALLOWANCE;
                            xlsCreator1.Pos(39, i_row).Value = row.SPECIAL_ALLOWANCE;
                        }

                        xlsCreator1.Pos(49, i_row).Value = row.TRANSPOTATION;
                        xlsCreator1.Pos(50, i_row).Value = row.HANDOVER;
                        xlsCreator1.Pos(51, i_row).Value = row.INCOME_TOTAL;

                        // total paid.
                        xlsCreator1.Pos(52, i_row).Value = row.INCOME_TOTAL;
                        xlsCreator1.Pos(53, i_row).Value = row.SOCIAL_SECURITY;
                        xlsCreator1.Pos(54, i_row).Value = row.INCOME_TAX;
                        xlsCreator1.Pos(55, i_row).Value = row.PROV_EMP;
                        xlsCreator1.Pos(56, i_row).Value = row.PROV_COMP;
                        xlsCreator1.Pos(57, i_row).Value = row.SALARY_PAYMENT;
                        xlsCreator1.Pos(58, i_row).Value = row.TRANSFER;
                        xlsCreator1.Pos(59, i_row).Value = row.HANDOVER;

                        for (int i = 0; i < 60; i++)
                        {
                            xlsCreator1.Pos(i, i_row).Attr.LineLeft = ExcelCreator.xlLineStyle.lsNormal;
                            xlsCreator1.Pos(i, i_row).Attr.LineBottom = ExcelCreator.xlLineStyle.lsNormal;
                            xlsCreator1.Pos(i, i_row).Attr.LineRight = ExcelCreator.xlLineStyle.lsNormal;
                            xlsCreator1.Pos(i, i_row).Attr.LineTop = ExcelCreator.xlLineStyle.lsNormal;
                        }

                        i_row++;
                    }
                }

                // Total.
                xlsCreator1.Pos(1, i_row).Str = "TOTAL";

                int i_cellCD = 67;  // 'C'
                int i_cellCD2 = 64;  // '@'
                string s2 = "";

                for (int i = 0; i < 58; i++)
                {
                    char c = Convert.ToChar(i_cellCD);
                    string s1 = c.ToString();
                    xlsCreator1.Pos(2 + i, i_row).Value =
                        "=SUM(" + s2 + s1 + "7:" + s2 + s1 + i_row.ToString() + ")";
                    if (90 == i_cellCD)
                    {
                        i_cellCD = 65;  // 'A'
                        i_cellCD2++;
                        char c2 = Convert.ToChar(i_cellCD2);
                        s2 = c2.ToString();
                    }
                    else
                    {
                        i_cellCD++;
                    }
                }
                for (int i = 1; i < 60; i++)
                {
                    xlsCreator1.Pos(i, i_row).Attr.FontStyle = ExcelCreator.xlFontStyle.xsBold;
                    if (1 == i)
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineLeft = ExcelCreator.xlLineStyle.lsThick;
                    }
                    else
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineLeft = ExcelCreator.xlLineStyle.lsNormal;
                    }
                    if (59 == i)
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineRight = ExcelCreator.xlLineStyle.lsThick;
                    }
                    else
                    {
                        xlsCreator1.Pos(i, i_row).Attr.LineRight = ExcelCreator.xlLineStyle.lsNormal;
                    }
                    xlsCreator1.Pos(i, i_row).Attr.LineBottom = ExcelCreator.xlLineStyle.lsThick;
                    xlsCreator1.Pos(i, i_row).Attr.LineTop = ExcelCreator.xlLineStyle.lsThick;
                }

                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3006";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        protected void Download_Report(string sFilePath)
        {
            DateTime d_target = new DateTime(ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")),
                                                ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")), 1);
            string filename = "SalaryList_" + d_target.ToString("yyyyMM") + ".xls";

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

        protected void Download_Report2(string sFilePath, string sFileType)
        {
            string filename = sFileType + "_" + ComFunc.UseSession(Page, "selected_year") + ComFunc.UseSession(Page, "selected_month") + ".xls";

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

        protected string Generate_Report2()
        {
            string s_tmpPayoff = "Payoff.xls";
            string TmplateFilePath = ComFunc.getSetting("ReportPath") + s_tmpPayoff;
            string sDateTime = System.DateTime.Now.ToString();
            sDateTime = sDateTime.Replace("/", "");
            sDateTime = sDateTime.Replace(":", "");
            sDateTime = sDateTime.Replace(" ", "");
            string sFilePath = ComFunc.getSetting("TempPath") + "Payoff_" + sDateTime + ".xls";

            try
            {
                // copy template to temporary folder.
                File.Copy(TmplateFilePath, sFilePath);
                xlsCreator1.OpenBook(sFilePath, "");

                DateTime d_target = new DateTime(ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")),
                                                    ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")), 1);
                xlsCreator1.Cell("**COMPNAME").Str = ComFunc.getSetting("CompanyName");
                xlsCreator1.Cell("**COMPADDRESS").Str = ComFunc.getSetting("CompanyAddress");
                xlsCreator1.Cell("**DOCDESC").Str = d_target.ToString("MMM") + "-" + d_target.ToString("yyyy") + " Salary Data";
                xlsCreator1.Cell("**DOCDATE").Str = DateTime.Now.ToString("yyyy/MM/dd");

                int i_cnt = 0;
                DataClassesDataContext db = new DataClassesDataContext();
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (0 != ComFunc.ConvertDouble(row.Cells[12].Text))
                    {
                        TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                                            x.ID == row.Cells[1].Text);
                        if (null != tb_u)
                        {
                            xlsCreator1.Pos(1, 6 + i_cnt).Value = i_cnt + 1;
                            xlsCreator1.Pos(2, 6 + i_cnt).Str = row.Cells[1].Text;
                            xlsCreator1.Pos(3, 6 + i_cnt).Str = row.Cells[2].Text;
                            xlsCreator1.Pos(5, 6 + i_cnt).Str = ComFunc.ConvertStr(tb_u.BANK_NAME);
                            xlsCreator1.Pos(7, 6 + i_cnt).Str = ComFunc.ConvertStr(tb_u.ACCOUNT_NO);
                            xlsCreator1.Pos(8, 6 + i_cnt).Value = ComFunc.ConvertDouble(row.Cells[12].Text);
                            i_cnt++;
                        }
                    }
                }

                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3007";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        protected string Generate_Report3()
        {
            string s_tmpPayoff = "RM01.xls";
            string TmplateFilePath = ComFunc.getSetting("ReportPath") + s_tmpPayoff;
            string sDateTime = System.DateTime.Now.ToString();
            sDateTime = sDateTime.Replace("/", "");
            sDateTime = sDateTime.Replace(":", "");
            sDateTime = sDateTime.Replace(" ", "");
            string sFilePath = ComFunc.getSetting("TempPath") + "RM01_" + sDateTime + ".xls";

            try
            {
                // copy template to temporary folder.
                File.Copy(TmplateFilePath, sFilePath);
                xlsCreator1.OpenBook(sFilePath, "");



                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3008";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        protected string Generate_Report4()
        {
            string s_tmpPayoff = "RM05.xls";
            string TmplateFilePath = ComFunc.getSetting("ReportPath") + s_tmpPayoff;
            string sDateTime = System.DateTime.Now.ToString();
            sDateTime = sDateTime.Replace("/", "");
            sDateTime = sDateTime.Replace(":", "");
            sDateTime = sDateTime.Replace(" ", "");
            string sFilePath = ComFunc.getSetting("TempPath") + "RM01_" + sDateTime + ".xls";

            try
            {
                // copy template to temporary folder.
                File.Copy(TmplateFilePath, sFilePath);
                xlsCreator1.OpenBook(sFilePath, "");



                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3009";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }
        #endregion
    }
}