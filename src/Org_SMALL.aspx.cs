using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Org_SMALL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");
                ComFunc.Language("Org.aspx", form1);
            }
        }

        protected void Button_BIG_Click(object sender, EventArgs e)
        {
            Response.Redirect("Org.aspx");
        }

        protected void Button_SMALL_Click(object sender, EventArgs e)
        {
            Response.Redirect("Org_Small.aspx");
        }
    }
}