using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComFunction;

namespace BrightHRSystem
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HCus.Text = "Company : " + ComFunc.UseSession(Page, "cus_name");
                HUserID.Text = "Code : " + ComFunc.UseSession(Page, "user_id");
                HUserName.Text = "Name : " + ComFunc.UseSession(Page, "user_name");

                ComFunc.Language("Menu.aspx", form1);

                WR_Area.Visible = false;
                Org_Area.Visible = false;
                Emp_Area.Visible = false;
                Salary_Area.Visible = false;
                Set_Area.Visible = false;

                DataClassesDataContext db = new DataClassesDataContext();
                TB_R_USER tb = db.TB_R_USERs.SingleOrDefault(x =>
                    x.ID == ComFunc.UseSession(Page, "user_id")
                    );
                if (null != tb)
                {
                    if ('1' == tb.AUTH1)
                    {
                        WR_Area.Visible = true;
                        if ('0' == tb.AUTH3)
                        {
                            HyperLink4.Visible = false;
                        }
                    }
                    if ('1' == tb.AUTH2)
                    {
                        Org_Area.Visible = true;
                    }
                    if ('1' == tb.AUTH4)
                    {
                        Salary_Area.Visible = true;
                    }
                    if ('1' == tb.AUTH6)
                    {
                        Emp_Area.Visible = true;
                    }
                    if ('1' == tb.AUTH8)
                    {
                        Set_Area.Visible = true;
                    }
                }
            }
        }
    }
}