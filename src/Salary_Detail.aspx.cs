using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Salary_Detail : System.Web.UI.Page
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

                    ComFunc.Language("Salary_Detail.aspx", form1);
                    Button_Print.OnClientClick = ComFunc.getMessage("C002");

                    setBeforeAfter();

                    UserCode.Text = ComFunc.UseSession(Page, "selected_id");
                    string s_userName = ComFunc.Get_UserName(UserCode.Text);
                    DropDownListPIC.DataBind();
                    for (int i = 0; i < DropDownListPIC.Items.Count; i++)
                    { // check the index of dropdown list.
                        if (s_userName == DropDownListPIC.Items[i].Value.Trim())
                        {
                            DropDownListPIC.SelectedIndex = i;
                            break;
                        }
                    }
                    DateTime d_target = new DateTime(ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")),
                                                        ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")), 1);
                    if (1 == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus")))
                    {
                        if (1 == ComFunc.ConvertInt(ComFunc.getSetting("CompStartMonth")))
                        {
                            Label3.Text = "Bonus " + d_target.ToString("yyyy");
                        }
                        else
                        {
                            Label3.Text = "Bonus " + d_target.AddYears(-1).ToString("yyyy");
                        }
                    }
                    else
                    {
                        Label3.Text = d_target.ToString("MMM") + "-" + d_target.ToString("yyyy") + " Salary Data";
                    }

                    DataClassesDataContext db = new DataClassesDataContext();
                    var tb_slip = from x in db.TB_M_SLIPTYPEs orderby x.SEQ_ID select x;
                    int i_cnt = 0;
                    foreach (var row in tb_slip)
                    {
                        DDSlipType.Items.Add(row.SLIP_NAME);
                        i_cnt++;
                    }
                    if (0 == i_cnt)
                    {
                        DDSlipType.Visible = false;
                    }

                    if ("Y" == ComFunc.getSetting("SpecialAllowance"))
                    {
                        btnDetailAllowance.Visible = true;
                    }

                    setValue();
                    CalcTotalIncome();

                    Session["DetailAllowanceAmt"] = "";
                }

                if ("Y" == ComFunc.getSetting("SpecialAllowance"))
                {
                    if ("" != ComFunc.UseSession(Page, "DetailAllowanceAmt"))
                    {
                        TextBoxBasicAllowance.Text = ComFunc.ConvertMoney(ComFunc.UseSession(Page, "DetailAllowanceAmt"));
                        CalcTotalIncome();
                        Session["DetailAllowanceAmt"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3101";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                    x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")) &&
                    x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                    );

                TB_R_PAYROLL_D tb_d = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                    x.HEADER_ID == tb_h.SEQ_ID &&
                    x.USER_ID == ComFunc.UseSession(Page, "selected_id")
                    );

                // Types of Work Items.
                tb_d.WORKING_DAYS = ComFunc.ConvertDecimal(TextBoxWorkingDays.Text);
                tb_d.REAL_WORK_DAYS = ComFunc.ConvertDecimal(TextBoxRealWorkDays.Text);
                tb_d.ABCENSE_DAYS = ComFunc.ConvertDecimal(TextBoxAbcenseDays.Text);
                tb_d.PAID_HOLIDAY_DAYS = ComFunc.ConvertDecimal(TextBoxPaidHolidayDays.Text);
                tb_d.BUSINESS_TRIP = ComFunc.ConvertDecimal(TextBoxBusinessTripDays.Text);
                tb_d.WORKING_TIMES = ComFunc.ConvertDecimalFromTime(TextBoxWorkTime.Text);
                tb_d.REAL_WORKING_TIMES = ComFunc.ConvertDecimalFromTime(TextBoxRealWorkTime.Text);
                tb_d.NORMALOTTIME = ComFunc.ConvertDecimalFromTime(TextBoxNormalOTTime.Text);
                tb_d.HOLIDAYWORKTIME = ComFunc.ConvertDecimalFromTime(TextBoxHolidayWorkTime.Text);
                tb_d.HOLIDAYOTTIME = ComFunc.ConvertDecimalFromTime(TextBoxHolidayOTTime.Text);
                tb_d.REMAIN_PAID_HOLIDAY = ComFunc.ConvertDecimal(TextBoxRemainPaidHoliday.Text);
                tb_d.USED_PAID_HOLIDAY = ComFunc.ConvertDecimal(TextBoxUsedPaidHoliday.Text);

                // Types of Incomes.
                tb_d.BASIC_SALARY = ComFunc.ConvertDecimal(TextBoxBasicSalary.Text);
                tb_d.UNIT_PRICE = ComFunc.ConvertDecimal(TextBoxUnitPrice.Text);
                tb_d.OT_PRICE = ComFunc.ConvertDecimal(TextBoxOTPrice.Text);
                tb_d.HOLIDAY_WORK_PRICE = ComFunc.ConvertDecimal(TextBoxHolidayWorkPrice.Text);
                tb_d.HOLIDAY_OT_PRICE = ComFunc.ConvertDecimal(TextBoxHolidayOTPrice.Text);
                tb_d.BASIC_ALLOWANCE = ComFunc.ConvertDecimal(TextBoxBasicAllowance.Text);
                tb_d.SPECIAL_ALLOWANCE = ComFunc.ConvertDecimal(TextBoxSpecialAllowance.Text);
                tb_d.TRANSPOTATION = ComFunc.ConvertDecimal(TextBoxTranspotation.Text);

                // Total Paid.
                tb_d.HANDOVER = ComFunc.ConvertDecimal(TextBoxHandOver1.Text);
                tb_d.TRANSFER = ComFunc.ConvertDecimal(TextBoxTransfer.Text);
                tb_d.INCOME_TOTAL = ComFunc.ConvertDecimal(TextBoxIncomeTotal1.Text);
                if (0 == ComFunc.ConvertDecimal(TextBoxSocialSecurity.Text))
                {
                    CheckBoxSocialSecurity.Checked = false;
                }
                else
                {
                    CheckBoxSocialSecurity.Checked = true;
                }
                tb_d.SOCIAL_SECURITY = ComFunc.ConvertDecimal(TextBoxSocialSecurity.Text);
                tb_d.INCOME_TAX = ComFunc.ConvertDecimal(TextBoxIncomeTax.Text);
                tb_d.PROV_EMP = ComFunc.ConvertDecimal(TextBoxProvEmp.Text);
                tb_d.PROV_COMP = ComFunc.ConvertDecimal(TextBoxProvComp.Text);

                tb_d.SALARY_PAYMENT = ComFunc.ConvertDecimal(TextBoxSalaryPayment.Text);

                // Exemptions.
                tb_d.YEARS_FIXED = ComFunc.ConvertDecimal(TextBox_Amount1.Text);
                tb_d.YEARS_EXPECTATION = ComFunc.ConvertDecimal(TextBox_Amount2.Text);
                if (true == CheckBox_Amount.Checked)
                {
                    tb_d.SPOUSE_ALLOWANCE = 'Y';
                }
                else
                {
                    tb_d.SPOUSE_ALLOWANCE = 'N';
                }
                tb_d.CHILD_ALLOWANCE = ComFunc.ConvertInt(DropDownList_Amount1.SelectedValue);
                tb_d.EDUCATION_ALLOWANCE = ComFunc.ConvertInt(DropDownList_Amount2.SelectedValue);
                tb_d.PARENTS_ALLOWANCE = ComFunc.ConvertInt(DropDownList_Amount3.SelectedValue);
                tb_d.LIFE_INSURANCE = ComFunc.ConvertDecimal(TextBox_Amount8.Text);
                tb_d.APPROVED_PROVIDENT = ComFunc.ConvertDecimal(TextBox_Amount9.Text);
                tb_d.LONG_TERM_EQUITY = ComFunc.ConvertDecimal(TextBox_Amount10.Text);
                tb_d.HOME_MORTGAGE = ComFunc.ConvertDecimal(TextBox_Amount11.Text);
                tb_d.SOCIAL = ComFunc.ConvertDecimal(TextBox_Amount12.Text);
                tb_d.CHARITABLE = ComFunc.ConvertDecimal(TextBox_Amount13.Text);

                tb_d.CONDITION = "Inputting";
                tb_d.INPUT_F = 'Y';

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3102";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
            Response.Redirect("Salary.aspx");
        }

        protected void setValue()
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                    x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")) &&
                    x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                    );

                TB_R_PAYROLL_D tb_d = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                    x.HEADER_ID == tb_h.SEQ_ID &&
                    x.USER_ID == ComFunc.UseSession(Page, "selected_id")
                    );

                // Types of Work Items.
                TextBoxWorkingDays.Text = tb_d.WORKING_DAYS.ToString();
                TextBoxRealWorkDays.Text = tb_d.REAL_WORK_DAYS.ToString();
                TextBoxAbcenseDays.Text = tb_d.ABCENSE_DAYS.ToString();
                TextBoxPaidHolidayDays.Text = tb_d.PAID_HOLIDAY_DAYS.ToString();
                TextBoxBusinessTripDays.Text = tb_d.BUSINESS_TRIP.ToString();
                TextBoxWorkTime.Text = ComFunc.ConvertDecimaltoTime(tb_d.WORKING_TIMES);
                TextBoxRealWorkTime.Text = ComFunc.ConvertDecimaltoTime(tb_d.REAL_WORKING_TIMES);
                TextBoxNormalOTTime.Text = ComFunc.ConvertDecimaltoTime(tb_d.NORMALOTTIME);
                TextBoxHolidayWorkTime.Text = ComFunc.ConvertDecimaltoTime(tb_d.HOLIDAYWORKTIME);
                TextBoxHolidayOTTime.Text = ComFunc.ConvertDecimaltoTime(tb_d.HOLIDAYOTTIME);
                TextBoxRemainPaidHoliday.Text = tb_d.REMAIN_PAID_HOLIDAY.ToString();
                TextBoxUsedPaidHoliday.Text = tb_d.USED_PAID_HOLIDAY.ToString();

                // Types of Incomes.
                TextBoxBasicSalary.Text = ComFunc.ConvertMoney(tb_d.BASIC_SALARY);
                TextBoxUnitPrice.Text = ComFunc.ConvertMoney(tb_d.UNIT_PRICE);
                TextBoxOTPrice.Text = ComFunc.ConvertMoney(tb_d.OT_PRICE);
                TextBoxHolidayWorkPrice.Text = ComFunc.ConvertMoney(tb_d.HOLIDAY_WORK_PRICE);
                TextBoxHolidayOTPrice.Text = ComFunc.ConvertMoney(tb_d.HOLIDAY_OT_PRICE);
                TextBoxBasicAllowance.Text = ComFunc.ConvertMoney(tb_d.BASIC_ALLOWANCE);
                TextBoxSpecialAllowance.Text = ComFunc.ConvertMoney(tb_d.SPECIAL_ALLOWANCE);
                TextBoxTranspotation.Text = ComFunc.ConvertMoney(tb_d.TRANSPOTATION);

                // Total Paid.
                TextBoxHandOver1.Text = ComFunc.ConvertMoney(tb_d.HANDOVER);
                TextBoxHandOver2.Text = ComFunc.ConvertMoney(tb_d.HANDOVER);
                TextBoxTransfer.Text = ComFunc.ConvertMoney(tb_d.TRANSFER);
                TextBoxIncomeTotal1.Text = ComFunc.ConvertMoney(tb_d.INCOME_TOTAL);
                TextBoxIncomeTotal2.Text = ComFunc.ConvertMoney(tb_d.INCOME_TOTAL);
                TextBoxSocialSecurity.Text = ComFunc.ConvertMoney(tb_d.SOCIAL_SECURITY);
                if (0 == ComFunc.ConvertDecimal(TextBoxSocialSecurity.Text))
                {
                    CheckBoxSocialSecurity.Checked = false;
                }
                TextBoxProvEmp.Text = ComFunc.ConvertMoney(tb_d.PROV_EMP);
                TextBoxProvComp.Text = ComFunc.ConvertMoney(tb_d.PROV_COMP);
                TextBoxSalaryPayment.Text = ComFunc.ConvertMoney(tb_d.SALARY_PAYMENT);

                // Exemptions.
                TextBox_Amount1.Text = ComFunc.ConvertMoney(tb_d.YEARS_FIXED);
                TextBox_Amount2.Text = ComFunc.ConvertMoney(tb_d.YEARS_EXPECTATION);

                if ('Y' == tb_d.SPOUSE_ALLOWANCE)
                {
                    CheckBox_Amount.Checked = true;
                }
                else
                {
                    CheckBox_Amount.Checked = false;
                }
                DropDownList_Amount1.DataBind();
                for (int i = 0; i < DropDownList_Amount1.Items.Count; i++)
                { // check the index of dropdown list.
                    if (tb_d.CHILD_ALLOWANCE == ComFunc.ConvertInt(DropDownList_Amount1.Items[i].Value))
                    {
                        DropDownList_Amount1.SelectedIndex = i;
                        break;
                    }
                }
                DropDownList_Amount2.DataBind();
                for (int i = 0; i < DropDownList_Amount2.Items.Count; i++)
                { // check the index of dropdown list.
                    if (tb_d.EDUCATION_ALLOWANCE == ComFunc.ConvertInt(DropDownList_Amount2.Items[i].Value))
                    {
                        DropDownList_Amount2.SelectedIndex = i;
                        break;
                    }
                }
                DropDownList_Amount3.DataBind();
                for (int i = 0; i < DropDownList_Amount3.Items.Count; i++)
                { // check the index of dropdown list.
                    if (tb_d.PARENTS_ALLOWANCE == ComFunc.ConvertInt(DropDownList_Amount3.Items[i].Value))
                    {
                        DropDownList_Amount3.SelectedIndex = i;
                        break;
                    }
                }

                TextBox_Amount8.Text = ComFunc.ConvertMoney(tb_d.LIFE_INSURANCE);
                TextBox_Amount9.Text = ComFunc.ConvertMoney(tb_d.APPROVED_PROVIDENT);
                TextBox_Amount10.Text = ComFunc.ConvertMoney(tb_d.LONG_TERM_EQUITY);
                TextBox_Amount11.Text = ComFunc.ConvertMoney(tb_d.HOME_MORTGAGE);
                TextBox_Amount12.Text = ComFunc.ConvertMoney(tb_d.SOCIAL);
                TextBox_Amount13.Text = ComFunc.ConvertMoney(tb_d.CHARITABLE);

                ChangeExemptionsCalc();

                TextBoxIncomeTax.Text = ComFunc.ConvertMoney(tb_d.INCOME_TAX);
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3103";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void DropDownListPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s_userID = ComFunc.Get_UserID(DropDownListPIC.SelectedValue);
            if ("" != s_userID)
            {
                Session["selected_id"] = s_userID;
                Response.Redirect("Salary_Detail.aspx");
            }
        }

        protected static string s_idBefore = "";
        protected static string s_idAfter = "";
        protected static string s_idHeader = "";

        protected void setBeforeAfter()
        {
            try
            {
                s_idBefore = "";
                s_idAfter = "";
                s_idHeader = "";

                string s_id = ComFunc.UseSession(Page, "selected_id");
                bool b_find = false;
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_payH = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                    x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")) &&
                    x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                    );
                s_idHeader = tb_payH.SEQ_ID.ToString();
                var tb_payD = from x in db.TB_R_PAYROLL_Ds
                              orderby x.USER_ID
                              where x.HEADER_ID == tb_payH.SEQ_ID
                              select x.USER_ID;
                foreach (var ID in tb_payD)
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
                string error_msg = @"System Error E3104";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void Button_Before_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idBefore;
            Response.Redirect("Salary_Detail.aspx");
        }

        protected void Button_After_Click(object sender, EventArgs e)
        {
            Session["selected_id"] = s_idAfter;
            Response.Redirect("Salary_Detail.aspx");
        }

        protected void Button_Print_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_H tb_h = db.TB_R_PAYROLL_Hs.SingleOrDefault(x =>
                    x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")) &&
                    x.MONTH == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")) &&
                    x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                    );

                TB_R_PAYROLL_D tb_d = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                    x.HEADER_ID == tb_h.SEQ_ID &&
                    x.USER_ID == ComFunc.UseSession(Page, "selected_id")
                    );

                tb_d.CONDITION = "Printed";
                tb_d.PRINT_F = 'Y';

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3105";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }

            // generate and download report.
            Download_Report(Generate_Report());
        }

        private ExcelCreator.XlsCreator xlsCreator1 = new ExcelCreator.XlsCreator();

        protected string Generate_Report()
        {
            DataClassesDataContext db = new DataClassesDataContext();
            string s_tmpWR = ComFunc.getSetting("ReportName");
            string TmplateFilePath = ComFunc.getSetting("ReportPath") + ComFunc.getSetting("SalarySlipFileName");

            if (true == DDSlipType.Visible)
            {
                TB_M_SLIPTYPE tb_slip = db.TB_M_SLIPTYPEs.SingleOrDefault(x =>
                    x.SLIP_NAME == DDSlipType.SelectedValue
                    );
                if (null != tb_slip)
                {
                    TmplateFilePath = ComFunc.getSetting("ReportPath") + ComFunc.ConvertStr(tb_slip.FILE_NAME);
                }
            }

            string sDateTime = DateTime.Now.ToString();
            sDateTime = sDateTime.Replace("/", "");
            sDateTime = sDateTime.Replace(":", "");
            sDateTime = sDateTime.Replace(" ", "");
            string sFilePath = ComFunc.getSetting("TempPath") + "Payslip_" + sDateTime + ".xls";

            try
            {
                // copy template to temporary folder.
                File.Copy(TmplateFilePath, sFilePath);
                xlsCreator1.OpenBook(sFilePath, "");

                TB_R_USER tb = db.TB_R_USERs.SingleOrDefault(x =>
                    x.ID == ComFunc.UseSession(Page, "selected_id")
                    );
                string s_localName = "";
                if ('F' == tb.SEX.Value)
                {
                    s_localName = "น.ส. " + ComFunc.ConvertStr(tb.LOCALNAME);
                }
                else
                {
                    s_localName = "นาย " + ComFunc.ConvertStr(tb.LOCALNAME);
                }

                xlsCreator1.Cell("**COMP_NAME").Str = ComFunc.getSetting("CompanyName_Thai");
                xlsCreator1.Cell("**PRT_DATE").Str = ComFunc.UseSession(Page, "PayDay");
                xlsCreator1.Cell("**ID").Str = ComFunc.UseSession(Page, "selected_id");
                xlsCreator1.Cell("**NAME1").Str = s_localName;
                xlsCreator1.Cell("**NAME2").Str = "( " + s_localName + " )";

                xlsCreator1.Cell("**TERM").Str = ComFunc.UseSession(Page, "PayTerm");
                xlsCreator1.Cell("**ABSENT_AMT").Value = ComFunc.ConvertDouble(TextBoxAbcenseDays.Text);
                xlsCreator1.Cell("**LATE_AMT").Value = 0;

                if ("A001" == ComFunc.getSetting("CompanyCD"))
                {
                    // for Alberry.
                    switch (DDSlipType.SelectedValue.Trim())
                    {
                        case "Salary Slip for Japanese":
                            xlsCreator1.Cell("**SALARY01_AMT").Value = ComFunc.ConvertDouble(TextBoxBasicSalary.Text);
                            xlsCreator1.Cell("**SALARY02_AMT").Value = ComFunc.ConvertDouble(TextBoxBasicAllowance.Text);
                            xlsCreator1.Cell("**SALARY03_AMT").Value = ComFunc.ConvertDouble(TextBoxSpecialAllowance.Text);
                            break;
                        case "Salary Slip for Office Staff":
                            xlsCreator1.Cell("**SALARY01_AMT").Value = ComFunc.ConvertDouble(TextBoxBasicSalary.Text);
                            xlsCreator1.Cell("**SALARY02_AMT").Value = ComFunc.ConvertDouble(TextBoxBasicAllowance.Text);
                            xlsCreator1.Cell("**SALARY03_AMT").Value = ComFunc.ConvertDouble(TextBoxSpecialAllowance.Text);
                            break;
                        case "Salary Slip for Driver":
                            xlsCreator1.Cell("**SALARY01_AMT").Value = ComFunc.ConvertDouble(TextBoxBasicSalary.Text);
                            xlsCreator1.Cell("**SALARY02_AMT").Value = ComFunc.ConvertDouble(TextBoxOTPrice.Text) +
                                ComFunc.ConvertDouble(TextBoxHolidayWorkPrice.Text) +
                                ComFunc.ConvertDouble(TextBoxHolidayOTPrice.Text);
                            xlsCreator1.Cell("**SALARY03_AMT").Value = ComFunc.ConvertDouble(TextBoxBasicAllowance.Text) +
                                ComFunc.ConvertDouble(TextBoxSpecialAllowance.Text);
                            break;
                        default:
                            break;
                    }
                }
                else if ("T001" == ComFunc.getSetting("CompanyCD"))
                {
                    // for Tsuchiya.

                    double d_baseT = 0;
                    double d_FoodT = 0;
                    double d_TravelT = 0;
                    double d_HouseT = 0;
                    double d_EnvironmentT = 0;
                    double d_NightT = 0;
                    double d_CompleteT = 0;
                    double d_OTT = 0;
                    double d_OtherT = 0;

                    bool b_monthly = true;
                    if (null != tb)
                    {
                        TB_M_EMPLOYEE_TYPE tb_e = db.TB_M_EMPLOYEE_TYPEs.SingleOrDefault(x =>
                            x.EMPLOYEE_TYPE == tb.EMPLOYEE_TYPE
                            );
                        if (null != tb_e)
                        {
                            if ("Daily" == tb_e.DETAIL)
                            {
                                b_monthly = false;
                            }
                        }
                    }
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
                            TB_R_PAYROLL_D2 tb_d2 = db.TB_R_PAYROLL_D2s.SingleOrDefault(x =>
                                x.D1_ID == tb_d.SEQ_ID
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
                        }
                    }

                    if (true == b_monthly)
                    {
                        d_baseT = d_baseT + ComFunc.ConvertDouble(TextBoxBasicSalary.Text);
                    }
                    else
                    {
                        d_baseT = d_baseT + ComFunc.ConvertDouble(TextBoxUnitPrice.Text);
                    }

                    d_TravelT = d_TravelT + ComFunc.ConvertDouble(TextBoxTranspotation.Text);

                    d_OTT = ComFunc.ConvertDouble(TextBoxOTPrice.Text) +
                            ComFunc.ConvertDouble(TextBoxHolidayWorkPrice.Text) +
                            ComFunc.ConvertDouble(TextBoxHolidayOTPrice.Text);

                    xlsCreator1.Cell("**SALARY01_AMT").Value = d_baseT;
                    xlsCreator1.Cell("**SALARY02_AMT").Value = d_FoodT;
                    xlsCreator1.Cell("**SALARY03_AMT").Value = d_TravelT;
                    xlsCreator1.Cell("**SALARY04_AMT").Value = d_HouseT;
                    xlsCreator1.Cell("**SALARY05_AMT").Value = d_EnvironmentT;
                    xlsCreator1.Cell("**SALARY06_AMT").Value = d_NightT;
                    xlsCreator1.Cell("**SALARY07_AMT").Value = d_CompleteT;
                    xlsCreator1.Cell("**SALARY08_AMT").Value = d_OTT;
                    xlsCreator1.Cell("**SALARY09_AMT").Value = d_OtherT;
                    xlsCreator1.Cell("**OTHERS").Value = 0;
                }
                else
                {
                    xlsCreator1.Cell("**SALARY01_AMT").Value = ComFunc.ConvertDouble(TextBoxBasicSalary.Text) +
                        ComFunc.ConvertDouble(TextBoxBasicAllowance.Text) +
                        ComFunc.ConvertDouble(TextBoxSpecialAllowance.Text);
                    xlsCreator1.Cell("**SALARY03_AMT").Value = ComFunc.ConvertDouble(TextBoxOTPrice.Text) +
                        ComFunc.ConvertDouble(TextBoxHolidayWorkPrice.Text) +
                        ComFunc.ConvertDouble(TextBoxHolidayOTPrice.Text);
                }

                xlsCreator1.Cell("**TAXAMT").Value = ComFunc.ConvertDouble(TextBoxIncomeTax.Text);
                xlsCreator1.Cell("**SOCIAL_AMT").Value = ComFunc.ConvertDouble(TextBoxSocialSecurity.Text);
                xlsCreator1.Cell("**PRVF_EMP").Value = ComFunc.ConvertDouble(TextBoxProvEmp.Text); ;
                xlsCreator1.Cell("**PRVF_COMP").Value = ComFunc.ConvertDouble(TextBoxProvComp.Text); ;

                var tb_ph = from x in db.TB_R_PAYROLL_Hs
                            orderby x.MONTH
                            where x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year"))
                                && x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                            select x;
                decimal d_FixedSalary = 0;
                decimal d_FixedTax = 0;
                decimal d_FixedSocial = 0;
                foreach (var row in tb_ph)
                {
                    TB_R_PAYROLL_D tb_pd = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                        x.HEADER_ID == row.SEQ_ID && x.USER_ID == ComFunc.UseSession(Page, "selected_id")
                        );
                    if (null != tb_pd)
                    {
                        if (null != tb_pd.INCOME_TOTAL)
                        {
                            d_FixedSalary = d_FixedSalary + tb_pd.INCOME_TOTAL.Value;
                        }
                        if (null != tb_pd.INCOME_TAX)
                        {
                            d_FixedTax = d_FixedTax + tb_pd.INCOME_TAX.Value;
                        }
                        if (null != tb_pd.SOCIAL_SECURITY)
                        {
                            d_FixedSocial = d_FixedSocial + tb_pd.SOCIAL_SECURITY.Value;
                        }
                    }
                }
                xlsCreator1.Cell("**YEAR_AMT").Value = d_FixedSalary;
                xlsCreator1.Cell("**YEAR_TAX").Value = d_FixedTax;
                xlsCreator1.Cell("**YEAR_SOCIAL").Value = d_FixedSocial;

                xlsCreator1.Cell("**ACCOUNTNO").Str = ComFunc.ConvertStr(tb.ACCOUNT_NO);
                xlsCreator1.Cell("**BANK").Str = ComFunc.ConvertStr(tb.BANK_NAME);

                xlsCreator1.CloseBook(true);

                return sFilePath;
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3106";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                return "";
            }
        }

        protected void Download_Report(string sFilePath)
        {
            string filename = "Payslip_" +
                                ComFunc.UseSession(Page, "selected_year") + "_" +
                                ComFunc.UseSession(Page, "selected_month") + "_" +
                                ComFunc.UseSession(Page, "selected_id") + "_" +
                                ".xls";

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

        protected void ChangedAmountIncome(object sender, EventArgs e)
        {
            CalcTotalIncome();
        }

        protected void ChangedAmountPaid(object sender, EventArgs e)
        {
            CalcTotalIncome();
        }

        protected void ChangedAmountExemption(object sender, EventArgs e)
        {
            ChangeExemptionsCalc();
            CalcTotalIncome();
        }

        protected void ChangeExemptionsCalc()
        {
            try
            {
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

                double d_YearSalary_Fix = ComFunc.ConvertDouble(TextBox_Amount1.Text);
                double d_YearSalary_Exp = ComFunc.ConvertDouble(TextBox_Amount2.Text);
                double d_Employment = (d_YearSalary_Fix + d_YearSalary_Exp) * RATE_INCOME_FROM_EMPLOYMENT;
                d_Employment = MAX_INCOME_FROM_EMPLOYMENT < d_Employment ? MAX_INCOME_FROM_EMPLOYMENT : d_Employment;
                double d_Spouse = true == CheckBox_Amount.Checked ? SPOUSE_ALLOWANCE : 0;
                double d_Child = CHILD_ALLOWANCE * ComFunc.ConvertDouble(DropDownList_Amount1.SelectedValue);
                double d_Educaion = EDUCATION_ALLOWANCE * ComFunc.ConvertDouble(DropDownList_Amount2.SelectedValue);
                double d_Parents = PARENTS_ALLOWANCE * ComFunc.ConvertDouble(DropDownList_Amount3.SelectedValue);
                double d_LifeInsurance = ComFunc.ConvertDouble(TextBox_Amount8.Text);
                d_LifeInsurance = MAX_LIFE_INSURANCE < d_LifeInsurance ? MAX_LIFE_INSURANCE : d_LifeInsurance;
                double d_ProvidentFund = ComFunc.ConvertDouble(TextBox_Amount9.Text);
                d_ProvidentFund = MAX_PROVIDENT_FUND < d_ProvidentFund ? MAX_PROVIDENT_FUND : d_ProvidentFund;
                double d_EquityFund = ComFunc.ConvertDouble(TextBox_Amount10.Text);
                d_EquityFund = MAX_EQUITY_FUND < d_EquityFund ? MAX_EQUITY_FUND : d_EquityFund;
                double d_HomeMortgage = ComFunc.ConvertDouble(TextBox_Amount11.Text);
                d_HomeMortgage = MAX_HOME_MORTGAGE < d_HomeMortgage ? MAX_HOME_MORTGAGE : d_HomeMortgage;
                double d_SocialInsurance = ComFunc.ConvertDouble(TextBox_Amount12.Text);
                double d_Charitable = ComFunc.ConvertDouble(TextBox_Amount13.Text);
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

                TextBox_Amount1.Text = ComFunc.ConvertMoney(d_YearSalary_Fix);
                TextBox_Amount2.Text = ComFunc.ConvertMoney(d_YearSalary_Exp);
                TextBox_Amount3.Text = ComFunc.ConvertMoney(d_Employment);
                TextBox_Amount4.Text = ComFunc.ConvertMoney(d_Spouse);
                TextBox_Amount5.Text = ComFunc.ConvertMoney(d_Child);
                TextBox_Amount6.Text = ComFunc.ConvertMoney(d_Educaion);
                TextBox_Amount7.Text = ComFunc.ConvertMoney(d_Parents);
                TextBox_Amount8.Text = ComFunc.ConvertMoney(d_LifeInsurance);
                TextBox_Amount9.Text = ComFunc.ConvertMoney(d_ProvidentFund);
                TextBox_Amount10.Text = ComFunc.ConvertMoney(d_EquityFund);
                TextBox_Amount11.Text = ComFunc.ConvertMoney(d_HomeMortgage);
                TextBox_Amount12.Text = ComFunc.ConvertMoney(d_SocialInsurance);
                TextBox_Amount13.Text = ComFunc.ConvertMoney(d_Charitable);
                TextBox_Amount14.Text = ComFunc.ConvertMoney(d_TaxableIncome);
                TextBox_Amount15.Text = ComFunc.ConvertMoney(d_Tax1);
                TextBox_Amount16.Text = ComFunc.ConvertMoney(d_Tax2);
                TextBox_Amount17.Text = ComFunc.ConvertMoney(d_Tax3);
                TextBox_Amount18.Text = ComFunc.ConvertMoney(d_Tax4);
                TextBox_Amount19.Text = ComFunc.ConvertMoney(d_Tax5);
                TextBox_Amount20.Text = ComFunc.ConvertMoney(d_Tax6);
                TextBox_Amount21.Text = ComFunc.ConvertMoney(d_Tax7);
                TextBox_Amount22.Text = ComFunc.ConvertMoney(d_Tax8);
                TextBox_Amount23.Text = ComFunc.ConvertMoney(d_PersonalIncomeTax);
                TextBox_Amount24.Text = ComFunc.ConvertMoney(d_PersonalIncomeTaxMonth);
                if (0 != d_PersonalIncomeTaxMonth)
                {
                    TextBoxIncomeTax.Text = ComFunc.ConvertMoney(d_PersonalIncomeTaxMonth);
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3107";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected DateTime getDatetime(int i_year, int i_month, int i_day)
        {
            int i_StartDay = ComFunc.ConvertInt(ComFunc.getSetting("StartDay"));
            if (i_day >= i_StartDay)
            {
                if (1 == i_month)
                {
                    i_year = i_year - 1;
                    i_month = 12;
                }
                else
                {
                    i_month = i_month - 1;
                }
            }
            return new DateTime(i_year, i_month, i_day);
        }

        protected void Button_GetWR_Click(object sender, EventArgs e)
        {
            try
            {
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
                TB_R_WORKINGREPORT_H tb_wh2 = db.TB_R_WORKINGREPORT_Hs.SingleOrDefault(x =>
                   x.USER_ID == ComFunc.UseSession(Page, "selected_id") &&
                   x.YEAH == i_yearTarget.ToString() &&
                   x.MONTH == i_monthTarget.ToString()
                   );

                int i_WorkingDay = 0;
                int i_ActWorkDay = 0;

                var tb_wd = from y in db.TB_R_WORKINGREPORT_Ds
                            where y.HEADER_ID == tb_wh1.seqID.ToString()
                            select y;
                DateTime dt;
                foreach (var row in tb_wd)
                {
                    dt = getDatetime(ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year")),
                        ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_month")),
                        ComFunc.ConvertInt(row.DAY.ToString()));
                    TB_M_HOLIDAY tb_ho = db.TB_M_HOLIDAYs.SingleOrDefault(x =>
                        x.DATE_HOLIDAY == dt
                        );
                    if (null == tb_ho)
                    {
                        i_WorkingDay = i_WorkingDay + 1;
                    }
                    if ("" != ComFunc.ConvertStr(row.APPLY_STARTING))
                    {
                        i_ActWorkDay = i_ActWorkDay + 1;
                    }
                }
                TextBoxWorkingDays.Text = i_WorkingDay.ToString();
                TextBoxRealWorkDays.Text = i_ActWorkDay.ToString();

                double d_NormalOT = 0;
                double d_HolidayWork = 0;
                double d_HolidayOT = 0;
                double d_Total = 0;

                var tb_d2 = from a in db.TB_R_WORKINGREPORT_Ds
                         from b in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WORKING_H_OT).DefaultIfEmpty()
                         from c in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WEEKEND_H_NORMAL).DefaultIfEmpty()
                         from d in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.WEEKEND_H_OT).DefaultIfEmpty()
                         from f in db.TB_M_TIMEs.Where(x => x.TIME_DATA == a.TOTAL).DefaultIfEmpty()
                            where a.HEADER_ID == tb_wh2.seqID.ToString()
                         select new
                         {
                             t1 = b.seqID == null ? 0 : b.seqID,
                             t2 = c.seqID == null ? 0 : c.seqID,
                             t3 = d.seqID == null ? 0 : d.seqID,
                             t4 = f.seqID == null ? 0 : f.seqID,
                         };
                foreach (var row in tb_d2)
                {
                    d_NormalOT += row.t1;
                    d_HolidayWork += row.t2;
                    d_HolidayOT += row.t3;
                    d_Total += row.t4;
                }

                if (0 != d_NormalOT)
                {
                    d_NormalOT = d_NormalOT * 0.25;
                }
                if (0 != d_HolidayWork)
                {
                    d_HolidayWork = d_HolidayWork * 0.25;
                }
                if (0 != d_HolidayOT)
                {
                    d_HolidayOT = d_HolidayOT * 0.25;
                }
                if (0 != d_Total)
                {
                    d_Total = d_Total * 0.25;
                }

                TextBoxWorkTime.Text = ComFunc.ConvertDoubletoTime(i_WorkingDay * 8);
                TextBoxRealWorkTime.Text = ComFunc.ConvertDoubletoTime(d_Total);
                TextBoxNormalOTTime.Text = ComFunc.ConvertDoubletoTime(d_NormalOT);
                TextBoxHolidayWorkTime.Text = ComFunc.ConvertDoubletoTime(d_HolidayWork);
                TextBoxHolidayOTTime.Text = ComFunc.ConvertDoubletoTime(d_HolidayOT);

            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3108";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void CalcTotalIncome()
        {
            try
            {
                decimal d_Total = ComFunc.ConvertDecimal(TextBoxBasicSalary.Text) +
                                    ComFunc.ConvertDecimal(TextBoxUnitPrice.Text) +
                                    ComFunc.ConvertDecimal(TextBoxOTPrice.Text) +
                                    ComFunc.ConvertDecimal(TextBoxHolidayWorkPrice.Text) +
                                    ComFunc.ConvertDecimal(TextBoxHolidayOTPrice.Text) +
                                    ComFunc.ConvertDecimal(TextBoxBasicAllowance.Text) +
                                    ComFunc.ConvertDecimal(TextBoxSpecialAllowance.Text) +
                                    ComFunc.ConvertDecimal(TextBoxTranspotation.Text);

                TextBoxIncomeTotal1.Text = ComFunc.ConvertMoney(d_Total);
                TextBoxIncomeTotal2.Text = ComFunc.ConvertMoney(d_Total);

                decimal d_Social = ComFunc.ConvertDecimal(TextBoxSocialSecurity.Text);
                if (true == CheckBoxSocialSecurity.Checked)
                {
                    if (0 == d_Social || 750 == d_Social)
                    {
                        d_Social = d_Total * 0.05m;
                        if (d_Social > 750)
                        {
                            d_Social = 750;
                        }
                    }
                }
                TextBoxSocialSecurity.Text = ComFunc.ConvertMoney(d_Social);

                decimal d_IncomeTax = ComFunc.ConvertDecimal(TextBoxIncomeTax.Text);
                decimal d_ProvEmp = ComFunc.ConvertDecimal(TextBoxProvEmp.Text);
                decimal d_ProvComp = ComFunc.ConvertDecimal(TextBoxProvComp.Text);
                decimal d_Payment = d_Total - d_Social - d_IncomeTax - d_ProvEmp - d_ProvComp;
                TextBoxSalaryPayment.Text = ComFunc.ConvertMoney(d_Payment);

                decimal d_HandOver = ComFunc.ConvertDecimal(TextBoxHandOver1.Text);
                decimal d_Transfer = d_Payment - d_HandOver;
                TextBoxTransfer.Text = ComFunc.ConvertMoney(d_Transfer);
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3109";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void ButtonGetUserSetting_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                decimal d_base = 0;
                decimal d_BasicAllowance = 0;
                decimal d_SpecialAllowance = 0;
                decimal d_Transpotation = 0;
                decimal d_dayly = 0;
                decimal d_unit = 0;
                decimal d_ActWk = 0;
                decimal d_NormalOTRat = 1.5m;
                decimal d_HolidayOTRat = 3.0m;
                decimal d_NormalOT = 0;
                decimal d_HolidayWork = 0;
                decimal d_HolidayOT = 0;
                decimal d_ProvEmp = 0;
                decimal d_ProvComp = 0;

                bool b_monthly = true;
                TB_R_USER tb_u = db.TB_R_USERs.SingleOrDefault(x =>
                    x.ID == ComFunc.UseSession(Page, "selected_id")
                    );
                if (null != tb_u)
                {
                    TB_M_EMPLOYEE_TYPE tb_e = db.TB_M_EMPLOYEE_TYPEs.SingleOrDefault(x =>
                        x.EMPLOYEE_TYPE == tb_u.EMPLOYEE_TYPE
                        );
                    if (null != tb_e)
                    {
                        if ("Daily" == tb_e.DETAIL)
                        {
                            b_monthly = false;
                        }
                    }
                }

                decimal d_HolidayWorkRat = 0;
                if (true == b_monthly)
                {
                    d_HolidayWorkRat = ComFunc.ConvertDecimal(ComFunc.getSetting("HolidayWorkRateM"));
                }
                else
                {
                    d_HolidayWorkRat = ComFunc.ConvertDecimal(ComFunc.getSetting("HolidayWorkRateD"));
                }

                var tb_sa = from x in db.TB_R_SALARies where x.ID == ComFunc.UseSession(Page, "selected_id") select x;
                foreach (var row in tb_sa)
                {
                    switch (row.DESC.Trim())
                    {
                        case "Base":
                            d_base = row.AMOUNT.Value;
                            d_dayly = d_base / 30;
                            d_unit = d_base / 30 / 8;
                            d_ActWk = ComFunc.ConvertDecimalFromTime(TextBoxRealWorkTime.Text);
                            if (true == b_monthly)
                            {
                                TextBoxBasicSalary.Text = ComFunc.ConvertMoney(d_base);
                                LabelBasicSalary.Text = row.REMARK.Trim();
                                TextBoxUnitPrice.Text = "";
                                LabelUnitPrice.Text = ComFunc.ConvertMoney(d_unit) + "(" +
                                                        ComFunc.ConvertMoney(d_base) +
                                                        " / 30 / 8) * Real Working Times";
                            }
                            else
                            {
                                TextBoxBasicSalary.Text = "";
                                TextBoxUnitPrice.Text = ComFunc.ConvertMoney(d_unit * d_ActWk);
                                LabelUnitPrice.Text = ComFunc.ConvertMoney(d_unit) + "(" +
                                                        ComFunc.ConvertMoney(d_base) +
                                                        " / 30 / 8) * " + TextBoxRealWorkTime.Text +"(Real Working Times)";
                            }

                            d_NormalOT = ComFunc.ConvertDecimalFromTime(TextBoxNormalOTTime.Text);
                            LabelOTPrice.Text = ComFunc.ConvertMoney(d_unit * d_NormalOTRat) + "(" +
                                                        ComFunc.ConvertMoney(d_unit) + " * " + d_NormalOTRat.ToString() +
                                                        ") * Normal OT Times(" + TextBoxNormalOTTime.Text + ")";
                            d_NormalOT = d_unit * d_NormalOT * d_NormalOTRat;
                            TextBoxOTPrice.Text = ComFunc.ConvertMoney(d_NormalOT);

                            d_HolidayWork = ComFunc.ConvertDecimalFromTime(TextBoxHolidayWorkTime.Text);
                            LabelHolidayWorkPrice.Text = ComFunc.ConvertMoney(d_unit * d_HolidayWorkRat) + "(" +
                                                            ComFunc.ConvertMoney(d_unit) + " * " + d_HolidayWorkRat.ToString() +
                                                            ") * Holiday Work Times(" + TextBoxHolidayWorkTime.Text + ")";
                            d_HolidayWork = d_unit * d_HolidayWork * d_HolidayWorkRat;
                            TextBoxHolidayWorkPrice.Text = ComFunc.ConvertMoney(d_HolidayWork);

                            d_HolidayOT = ComFunc.ConvertDecimalFromTime(TextBoxHolidayOTTime.Text);
                            LabelHolidayOTPrice.Text = ComFunc.ConvertMoney(d_unit * d_HolidayOTRat) + "(" +
                                                            ComFunc.ConvertMoney(d_unit) + " * " + d_HolidayOTRat.ToString() +
                                                            ") * Holiday OT Times(" + TextBoxHolidayOTTime.Text + ")";
                            d_HolidayOT = d_unit * d_HolidayOT * d_HolidayOTRat;
                            TextBoxHolidayOTPrice.Text = ComFunc.ConvertMoney(d_HolidayOT);
                            break;
                        case "Basic Allowance":
                            d_BasicAllowance = row.AMOUNT.Value;
                            TextBoxBasicAllowance.Text = ComFunc.ConvertMoney(d_BasicAllowance);
                            LabelBasicAllowance.Text = row.REMARK.Trim();
                            break;
                        case "Special Allowance":
                            d_SpecialAllowance = row.AMOUNT.Value;
                            TextBoxSpecialAllowance.Text = ComFunc.ConvertMoney(d_SpecialAllowance);
                            LabelSpecialAllowance.Text = row.REMARK.Trim();
                            break;
                        case "Transpotation":
                            d_Transpotation = row.AMOUNT.Value;
                            TextBoxTranspotation.Text = ComFunc.ConvertMoney(d_Transpotation);
                            LabelTranspotation.Text = row.REMARK.Trim();
                            break;
                        case "Provident Fund of employee":
                            d_ProvEmp = row.AMOUNT.Value;
                            TextBoxProvEmp.Text = ComFunc.ConvertMoney(d_ProvEmp);
                            break;
                        case "Provident Fund of company":
                            d_ProvComp = row.AMOUNT.Value;
                            TextBoxProvComp.Text = ComFunc.ConvertMoney(d_ProvComp);
                            break;
                        default:
                            break;
                    }
                }

                CheckBoxSocialSecurity.Checked = true;
                CalcTotalIncome();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3110";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void ButtonGetUserData_Click(object sender, EventArgs e)
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                var tb_ph = from x in db.TB_R_PAYROLL_Hs orderby x.MONTH
                            where x.YEAR == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_year"))
                                    && x.BONUS == ComFunc.ConvertInt(ComFunc.UseSession(Page, "selected_bonus"))
                            select x;
                decimal d_FixedSalary = 0;
                foreach (var row in tb_ph)
                {
                    TB_R_PAYROLL_D tb_pd = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                        x.HEADER_ID == row.SEQ_ID && x.USER_ID == ComFunc.UseSession(Page, "selected_id")
                        );
                    if (null != tb_pd && null != tb_pd.INCOME_TOTAL)
                    {
                        d_FixedSalary = d_FixedSalary + tb_pd.INCOME_TOTAL.Value;
                    }
                }
                TextBox_Amount1.Text = ComFunc.ConvertMoney(d_FixedSalary);
                TextBox_Amount2.Text = "";

                var tb_f = from x in db.TB_R_FAMILies where x.ID == ComFunc.UseSession(Page, "selected_id") select x;
                int i_wife = 0;
                int i_child = 0;
                int i_educa = 0;
                int i_parents = 0;
                foreach (var row in tb_f)
                {
                    if ('Y' == row.PROVIDE)
                    {
                        switch (row.RELATIONSHIP.Trim())
                        {
                            case "Wife":
                                i_wife++;
                                break;
                            case "Daughter":
                            case "Son":
                                i_child++;
                                if ('Y' == row.EDUCATION)
                                {
                                    i_educa++;
                                }
                                break;
                            case "Father":
                            case "Mother":
                                i_parents++;
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (0 < i_wife)
                {
                    CheckBox_Amount.Checked = true;
                }
                const int MAX_CHILD = 3;
                if (0 < i_child)
                {
                    if (MAX_CHILD < i_child)
                    {
                        DropDownList_Amount1.SelectedIndex = MAX_CHILD;
                    }
                    else
                    {
                        DropDownList_Amount1.SelectedIndex = i_child;
                    }
                }
                if (0 < i_educa)
                {
                    if (MAX_CHILD < i_educa)
                    {
                        DropDownList_Amount2.SelectedIndex = MAX_CHILD;
                    }
                    else
                    {
                        DropDownList_Amount2.SelectedIndex = i_educa;
                    }
                }
                const int MAX_PARENTS = 2;
                if (0 < i_parents)
                {
                    if (MAX_PARENTS < i_parents)
                    {
                        DropDownList_Amount3.SelectedIndex = MAX_PARENTS;
                    }
                    else
                    {
                        DropDownList_Amount3.SelectedIndex = i_parents;
                    }
                }

                // Social insurance contributions.
                decimal d_Social = ComFunc.ConvertDecimal(TextBoxSocialSecurity.Text);
                d_Social = d_Social * 12;
                TextBox_Amount12.Text = ComFunc.ConvertMoney(d_Social);

                ChangeExemptionsCalc();
                CalcTotalIncome();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3111";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void CheckBoxSocialSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (false == CheckBoxSocialSecurity.Checked)
            {
                TextBoxSocialSecurity.Text = "0";
            }
            CalcTotalIncome();
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string s_id = ComFunc.UseSession(Page, "selected_id");
                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_PAYROLL_D tb = db.TB_R_PAYROLL_Ds.SingleOrDefault(x =>
                    x.HEADER_ID == ComFunc.ConvertInt(s_idHeader) && x.USER_ID == s_id
                    );
                db.TB_R_PAYROLL_Ds.DeleteOnSubmit(tb);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E3112";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
            Response.Redirect("Salary.aspx");
        }
    }
}