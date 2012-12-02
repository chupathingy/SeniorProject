using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            label1.Text = "new label text.";
        }

        protected void GoToProfile_Click(object sender, EventArgs e)
        {
            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            bool loginCheck = wrap.VerifyLogin(caseID.Text, password.Text);
            wrap.Disconnect();
            if (loginCheck)
            {
                caseID.Text = "";
                password.Text = "";
                Page0.Style.Add("display", "none");
                Page1.Style.Add("display", "block");
            }
            else
            {
                ErrorText1.InnerText = "Incorrect Login. Please try again.";
            }
        }

        protected void GoToSearch_Click(object sender, EventArgs e)
        {

        }

        protected void GoToMake_Click(object sender, EventArgs e)
        {

        }

    }
}