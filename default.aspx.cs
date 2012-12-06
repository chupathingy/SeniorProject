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
        private Reservation pageOneSelectedRes;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GoToProfile_Click(object sender, EventArgs e)
        {
            string uid = caseID.Text;
            string pswrd = password.Text;

//            DBWrapper wrap = new DBWrapper("172.20.4.155", "finalproject", "devon", "devon");
//            wrap.Connect();
//            bool loginCheck = wrap.VerifyLogin(uid, pswrd);
//            if (loginCheck)
//            {
//                wrap.UpdateUserLastLogin(uid);
                password.Text = "";
                Page0.Style.Add("display", "none");
                Page1.Style.Add("display", "block");
                GridView2.Style.Add("display", "none");
//            }
//            else
//            {
                ErrorText1.InnerText = "Incorrect Login. Please try again.";
//            }
//            wrap.Disconnect();
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
            DateLabel.Text = searchResCalendar.SelectedDate.ToString("yyyy-MM-dd");
        }

        /**************Back Button For Page 2 and 3***************/
        protected void BackToHome_Click(object sender, EventArgs e)
        {
            Page2.Style.Add("display", "none");
            Page3.Style.Add("display", "none");
            Page1.Style.Add("display", "block");
        }

        /**************Search Button For Page 2*********************/
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            tests.InnerText = DateLabel.Text + SearchRoomSelect.SelectedValue;
            ObjectDataSource ods3 = new ObjectDataSource("FinalProject.ReservationInfo", "ViewReservationsByRoom");
            ControlParameter dateCtl = new ControlParameter("date", System.TypeCode.String, "DateLabel", "Text");
            ControlParameter roomCtl = new ControlParameter("rm", System.TypeCode.String, "SearchRoomSelect", "SelectedValue");

            ods3.SelectParameters.Add(dateCtl);
            ods3.SelectParameters.Add(roomCtl);

            GridView3.DataSource = ods3;
            GridView3.DataBind();
        }

        protected void MakeNew_Click(object sender, EventArgs e)
        {
            String resDate = reservationCalendar.SelectedDate.ToString("yyyy-MM-dd");
//            Reservation res = new Reservation(resDate, TextBoxMakeStart.Text, TextBoxMakeEnd.Text, MakeRoomSelect.SelectedValue, "test", true);

//            ErrorTextMakeRes.InnerText = res.MakeStatus();
        }

        /******************Page 1 Handlers**********************/
        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView1.Style.Add("display", "none");
            GridView2.Style.Add("display", "table");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.SelectedRow.BackColor = System.Drawing.Color.LightBlue;
            // Get the currently selected row using the SelectedRow property.
            GridViewRow row = GridView1.SelectedRow;

            // Make a reservation object to be interacted with
//            pageOneSelectedRes = new Reservation(row.Cells[0].Text, row.Cells[1].Text, row.Cells[2].Text, row.Cells[3].Text, caseID.Text, false);


            //prompt available options for this reservation object? Currently Buttons displayed statically
        }

        protected void ChangeRes_Click(object sender, EventArgs e)
        {
            //need to prompt for new date, time, etc. before submiting to DB

            //need to update GridView thing, too
        }

        protected void CancelRes_Click(object sender, EventArgs e)
        {
//            pageOneSelectedRes.Cancel();

//            ErrorText2.InnerText = pageOneSelectedRes.CancelStatus();

            //need to update the GridView thingy
        }
        /*******************End Page 1 Handlers*********************/


        /*protected void ObjectDataSource3_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters.Add("DateLabel", DateLabel.Text);
            e.InputParameters.Add("SearchRoomSelect", SearchRoomSelect.SelectedValue);

            ObjectDataSource3.Select();
            GridView3.
        }*/

    }
}