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
            string uid = caseID.Text;
            string pswrd = password.Text;

            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            bool loginCheck = wrap.VerifyLogin(uid, pswrd);
            if (loginCheck)
            {
                wrap.UpdateUserLastLogin(uid);
                caseID.Text = "";
                password.Text = "";
                Page0.Style.Add("display", "none");
                Page1.Style.Add("display", "block");
            }
            else
            {
                ErrorText1.InnerText = "Incorrect Login. Please try again.";
            }
            wrap.Disconnect();
        }

        protected void GoToSearch_Click(object sender, EventArgs e)
        {
            Page1.Style.Add("display", "none");
            Page2.Style.Add("display", "block");
        }

        protected void GoToMake_Click(object sender, EventArgs e)
        {
            Page1.Style.Add("display", "none");
            Page3.Style.Add("display", "block");
        }

        protected void GetDate(object sender, EventArgs e)
        {
            DateLabel.Text = "Reservations for " +  searchResCalendar.SelectedDate.ToShortDateString() + ":";
        }

        protected void BackToHome_Click(object sender, EventArgs e)
        {
            Page2.Style.Add("display", "none");
            Page3.Style.Add("display", "none");
            Page1.Style.Add("display", "block");
        }

        protected void MakeNew_Click(object sender, EventArgs e)
        {
            String resDate = reservationCalendar.SelectedDate.ToString("yyyy-MM-dd");
            Reservation res = new Reservation(resDate, TextBoxMakeStart.Text, TextBoxMakeEnd.Text, MakeRoomSelect.SelectedValue, "test", true);

            ErrorTextMakeRes.InnerText = res.MakeStatus();
        }

    }
}