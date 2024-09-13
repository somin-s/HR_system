using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Acceptance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ComFunc.Language("Acceptance.aspx", form1);
                    string s_state = Request.QueryString["state"];
                    string s_seqID = "";
                    if (null == s_state || "" == s_state)
                    {
                        LabelMessage.Text = "System Error";
                    }

                    DataClassesDataContext db = new DataClassesDataContext();
                    TB_R_WORKINGREPORT_D tb_wd = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                        x.TOKEN1 == s_state
                        );
                    if (null != tb_wd)
                    {
                        s_seqID = tb_wd.seqID.ToString();
                        tb_wd.TOKEN1 = "";
                        tb_wd.APPROVE_DATE1 = DateTime.Now;
                        tb_wd.APPROVE_COMMENT1 = "E-mail Approval";
                        db.SubmitChanges();
                    }

                    TB_R_WORKINGREPORT_D tb_wd2 = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                        x.TOKEN2 == s_state
                        );
                    if (null != tb_wd2)
                    {
                        s_seqID = tb_wd2.seqID.ToString();
                        tb_wd2.TOKEN2 = "";
                        tb_wd.APPROVE_DATE2 = DateTime.Now;
                        tb_wd.APPROVE_COMMENT2 = "E-mail Approval";
                        db.SubmitChanges();
                    }

                    TB_R_WORKINGREPORT_D tb_wd3 = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                        x.TOKEN3 == s_state
                        );
                    if (null != tb_wd3)
                    {
                        s_seqID = tb_wd3.seqID.ToString();
                        tb_wd3.TOKEN3 = "";
                        tb_wd.APPROVE_DATE3 = DateTime.Now;
                        tb_wd.APPROVE_COMMENT3 = "E-mail Approval";
                        db.SubmitChanges();
                    }

                    TB_R_WORKINGREPORT_D tb_wd4 = db.TB_R_WORKINGREPORT_Ds.SingleOrDefault(x =>
                        x.seqID == ComFunc.ConvertInt(s_seqID)
                        );
                    if (null != tb_wd4)
                    {
                        if ((ComFunc.checkApproved(tb_wd4.APPROVER1, tb_wd4.APPROVE_DATE1)) &&
                            (ComFunc.checkApproved(tb_wd4.APPROVER2, tb_wd4.APPROVE_DATE2)) &&
                            (ComFunc.checkApproved(tb_wd4.APPROVER3, tb_wd4.APPROVE_DATE3)))
                        {
                            tb_wd4.STATUS = "Approved";
                        }
                        else
                        {
                            tb_wd4.STATUS = "Approving";
                        }
                        db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string error_msg = @"System Error E1401";
                ComFunc.WriteLogLocal(error_msg, ex.Message);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), @"window.setTimeout(function(){alert('" + error_msg + "');},0);", true);
            }
        }
    }
}