using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Setting_System : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");
                ComFunc.Language("Setting_System.aspx", form1);

                Session["separate_time"] = "0";
                if ("15" == ComFunc.getSetting("SeparateTime"))
                {
                    Session["separate_time"] = "1";
                }

                DropDownStart.DataBind();
                DropDownFinish.DataBind();

                setData();
            }
        }

        protected void setData()
        {
            try
            {
                DataClassesDataContext db = new DataClassesDataContext();
                var tb = from x in db.TB_M_SETTINGs select x;
                string s_item = "";
                string s_value = "";
                foreach (var row in tb)
                {
                    s_item = ComFunc.ConvertStr(row.ITEM);
                    s_value = ComFunc.ConvertStr(row.VALUE);
                    switch (s_item)
                    {
                        // common.
                        case "CompanyName":
                            TextCompName.Text = s_value;
                            break;
                        case "CompanyName_Thai":
                            TextCompNameThai.Text = s_value;
                            break;
                        case "CompanyAddress":
                            TextCompAddress.Text = s_value;
                            break;
                        case "PassExpireDay":
                            if ("0" == s_value)
                            {
                                RadioButtonOneTime.SelectedIndex = 1;
                                TextBoxPassExpire.Text = "";
                                TextBoxPassExpire.Enabled = false;
                            }
                            else
                            {
                                RadioButtonOneTime.SelectedIndex = 0;
                                TextBoxPassExpire.Text = s_value;
                                TextBoxPassExpire.Enabled = true;
                            }
                            break;
                        case "DefaultPass":
                            TextDefaultPass.Text = s_value;
                            break;

                        // payroll.
                        case "CompStartMonth":
                            DropDownCompYear.SelectedValue = s_value;
                            break;
                        case "WRTargetMonth":
                            if ("This" == s_value)
                            {
                                RadioButtonTargetWR.SelectedIndex = 0;
                            }
                            else
                            {
                                RadioButtonTargetWR.SelectedIndex = 1;
                            }
                            break;
                        case "OTTargetMonth":
                            if ("This" == s_value)
                            {
                                RadioButtonTargetOT.SelectedIndex = 0;
                            }
                            else
                            {
                                RadioButtonTargetOT.SelectedIndex = 1;
                            }
                            break;

                        // working record.
                        case "AllowEditOthers":
                            if ("Y" == s_value)
                            {
                                RadioButtonAllowEdit.SelectedIndex = 0;
                            }
                            else
                            {
                                RadioButtonAllowEdit.SelectedIndex = 1;
                            }
                            break;
                        case "AllowEditOwn":
                            if ("Y" == s_value)
                            {
                                RadioButtonAllowEdit2.SelectedIndex = 0;
                            }
                            else
                            {
                                RadioButtonAllowEdit2.SelectedIndex = 1;
                            }
                            break;
                        case "CalcTargetLate":
                            if ("Input" == s_value)
                            {
                                RadioButtonLateInput.SelectedIndex = 0;
                            }
                            else
                            {
                                RadioButtonLateInput.SelectedIndex = 1;
                            }
                            break;
                        case "CalcTargetEarly":
                            if ("Input" == s_value)
                            {
                                RadioButtonEarlyInput.SelectedIndex = 0;
                            }
                            else
                            {
                                RadioButtonEarlyInput.SelectedIndex = 1;
                            }
                            break;
                        case "ImportDataName":
                            TextImportData.Text = s_value;
                            break;
                        case "DefaultStart":
                            if (4 == s_value.Length)
                            {
                                s_value = s_value + " ";
                            }
                            DropDownStart.SelectedValue = s_value;
                            break;
                        case "DefaultFinish":
                            if (4 == s_value.Length)
                            {
                                s_value = s_value + " ";
                            }
                            DropDownFinish.SelectedValue = s_value;
                            break;
                        case "DefaultWorkPlace":
                            TextWorkPlace.Text = s_value;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4801";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void UpdateDatabase(string s_item, string s_value)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            TB_M_SETTING tb = db.TB_M_SETTINGs.SingleOrDefault(x =>
                x.ITEM == s_item
                );
            string s_valueOld = tb.VALUE.Trim();
            if (s_valueOld != s_value)
            {
                tb.VALUE = s_value;
                tb.UPDATE_DATE = DateTime.Now;
                tb.UPDATE_BY = ComFunc.UseSession(Page, "user_id");
                db.SubmitChanges();
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDatabase("CompanyName", TextCompName.Text);
                UpdateDatabase("CompanyName_Thai", TextCompNameThai.Text);
                UpdateDatabase("CompanyAddress", TextCompAddress.Text);

                string s_PassExpireDay = "0";
                if (0 == RadioButtonOneTime.SelectedIndex)
                {
                    s_PassExpireDay = ComFunc.ConvertInt(TextBoxPassExpire.Text).ToString();
                }
                UpdateDatabase("PassExpireDay", s_PassExpireDay);

                UpdateDatabase("DefaultPass", TextDefaultPass.Text);

                UpdateDatabase("CompStartMonth", DropDownCompYear.SelectedValue);

                string s_Allow = "Last";
                if (0 == RadioButtonTargetWR.SelectedIndex)
                {
                    s_Allow = "This";
                }
                UpdateDatabase("WRTargetMonth", s_Allow);

                s_Allow = "Last";
                if (0 == RadioButtonTargetOT.SelectedIndex)
                {
                    s_Allow = "This";
                }
                UpdateDatabase("OTTargetMonth", s_Allow);

                s_Allow = "N";
                if (0 == RadioButtonAllowEdit.SelectedIndex)
                {
                    s_Allow = "Y";
                }
                UpdateDatabase("AllowEditOthers", s_Allow);

                s_Allow = "N";
                if (0 == RadioButtonAllowEdit2.SelectedIndex)
                {
                    s_Allow = "Y";
                }
                UpdateDatabase("AllowEditOwn", s_Allow);

                s_Allow = "Device";
                if (0 == RadioButtonLateInput.SelectedIndex)
                {
                    s_Allow = "Input";
                }
                UpdateDatabase("CalcTargetLate", s_Allow);

                s_Allow = "Device";
                if (0 == RadioButtonEarlyInput.SelectedIndex)
                {
                    s_Allow = "Input";
                }
                UpdateDatabase("CalcTargetEarly", s_Allow);

                UpdateDatabase("ImportDataName", TextImportData.Text);

                UpdateDatabase("DefaultStart", DropDownStart.SelectedValue.Trim());
                UpdateDatabase("DefaultFinish", DropDownFinish.SelectedValue.Trim());

                UpdateDatabase("DefaultWorkPlace", TextWorkPlace.Text);

                
                string s_msg = @"System Update Data";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + s_msg + "');},0);", true);
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E4802";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }

        protected void RadioButtonOneTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 == RadioButtonOneTime.SelectedIndex)
            {
                TextBoxPassExpire.Enabled = true;
                TextBoxPassExpire.Text = "7";
            }
            else
            {
                TextBoxPassExpire.Enabled = false;
                TextBoxPassExpire.Text = "";
            }
        }

        protected void ButtonAddPHSH_Click(object sender, EventArgs e)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var tb_sa = from a in db.TB_R_SALARies
                        where a.REMARK == DateTime.Now.Year.ToString()
                        select a;
            if (null != tb_sa)
            {
                int cnt = 0;
                foreach (var row in tb_sa)
                {
                    cnt++;
                }
                if (0 != cnt)
                {
                    string error_msg = @"Already have Pay holiday items of this year.";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
                    return;
                }
            }

            TB_R_SALARY tb;
            var tb_u = from a in db.TB_R_USERs select a;
            if (null != tb_u)
            {
                foreach (var row in tb_u)
                {
                    tb = new TB_R_SALARY
                    {
                        ID = row.ID,
                        DESC = "Paid Holiday",
                        AMOUNT = 0,
                        REMARK = DateTime.Now.Year.ToString(),
                        UPDATE_DATE = DateTime.Now,
                        UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                        CREATE_DATE = DateTime.Now,
                        CREATE_BY = ComFunc.UseSession(Page, "user_id")
                    };
                    db.TB_R_SALARies.InsertOnSubmit(tb);
                    tb = new TB_R_SALARY
                    {
                        ID = row.ID,
                        DESC = "Special Holiday",
                        AMOUNT = 0,
                        REMARK = DateTime.Now.Year.ToString(),
                        UPDATE_DATE = DateTime.Now,
                        UPDATE_BY = ComFunc.UseSession(Page, "user_id"),
                        CREATE_DATE = DateTime.Now,
                        CREATE_BY = ComFunc.UseSession(Page, "user_id")
                    };
                    db.TB_R_SALARies.InsertOnSubmit(tb);
                    db.SubmitChanges();
                }
            }
        }
    }
}
