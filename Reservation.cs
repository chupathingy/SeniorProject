using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    public class Reservation
    {
        //fields
        private string resID;
        private string resDate;
        private string resStartTime;
        private string resEndTime;
        private string resRoom;
        private string resUserID;

        private string cancelMsg = null;
        private string changeMsg = null;
        private string makeResMsg = null;

        //properties (for data binding)
        public string ResID
        {
            get { return resID; }
            set { resID = value; }
        }

        public string ResDate
        {
            get { return resDate; }
            set { resDate = value; }
        }

        public string ResStartTime
        {
            get { return resStartTime; }
            set { resStartTime = value; }
        }

        public string ResEndTime
        {
            get { return resEndTime; }
            set { resEndTime = value; }
        }

        public string ResRoom
        {
            get { return resRoom; }
            set { resRoom = value; }
        }

        public string ResUserID
        {
            get { return resUserID; }
            set { resUserID = value; }
        }

        //constructor 
        public Reservation(string date, string startTime, string endTime, string room, string userId, bool makeNew)
        {
            resDate = date;
            resStartTime = startTime;
            resEndTime = endTime;
            resRoom = room;
            resUserID = userId;

            if (makeNew)
            {
                resID = SubmitToDB();
                NotifyByEmail();
            }
        }

        public bool Cancel()
        {
            DateTime time = DateTime.Now;
            string timeFormat = "HH:mm:ss";

            string today = time.ToString("yyyy-MM-dd");

            //check for possible time-based errors before submitting the cancellation to DB
            if (string.Compare(resDate, today) < 0)
            {
                cancelMsg = "Unable to cancel reservation: date has already passed.";
                return false;
            }
            else if (resDate == today)
            {
                if (string.Compare(resStartTime, time.ToString(timeFormat)) < 0)
                {
                    cancelMsg = "Unable to cancel reservation: reserved time has already passed.";
                    return false;
                }
            }

            //check for possible user-based errors before submitting the cancellation to DB



            //connect to DB for cancellation
            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            wrap.CancelReservation(resID);
            wrap.Disconnect();

            cancelMsg = "Cancellation successful.";
            return true;
        }

        public string CancelStatus()
        {
            return cancelMsg;
        }

        public bool Change(string newDate, string newStartTime, string newEndTime)
        {
            DateTime time = DateTime.Now;
            string timeFormat = "HH:mm:ss";

            string today = time.ToString("yyyy-MM-dd");

            //check for possible errors before submitting the change to DB
            if (string.Compare(resDate, today) < 0)
            {
                changeMsg = "Unable to change reservation: date has already passed.";
                return false;
            }
            else if (resDate == today)
            {
                if (string.Compare(resStartTime, time.ToString(timeFormat)) < 0)
                {
                    changeMsg = "Unable to change reservation: reserved time has already passed.";
                    return false;
                }
            }

            //check for possible user-based errors before submitting the cancellation to DB


            //connect to DB for change
            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            wrap.ChangeReservation(resID, resDate, resStartTime, resEndTime, resRoom, resUserID);
            wrap.Disconnect();

            cancelMsg = "Change successful.";
            return true;
        }

        public bool Change(string newStartTime, string newEndTime)
        {
            return true;
        }

        public string ChangeStatus()
        {
            return changeMsg;
        }

        public void ConfirmPrompt()
        {

        }

        public string GetDate()
        {
            return resDate;
        }

        public string GetStartTime()
        {
            return resStartTime;
        }

        public string GetEndTime()
        {
            return resEndTime;
        }

        public string GetResID()
        {
            return resID;
        }

        public string GetRoomName()
        {
            return resRoom;
        }

        public string GetUserID()
        {
            return resUserID;
        }

        public string MakeStatus()
        {
            return makeResMsg;
        }

        private bool NotifyByEmail()
        {
            try
            {
                string body = "This e-mail serves as a confirmation that someone has made a reservation for " +
                    resRoom + " on " + resDate + " from " + resStartTime + " to " + resEndTime +
                    ". <br /><br />If you did not make this reservation, you probably got hacked lolz.";

                //NetworkCredential loginInfo = new NetworkCredential(gMailAccount, password);
                MailMessage notification = new MailMessage();
                notification.From = new MailAddress("noreply@case.edu");
                notification.To.Add(new MailAddress(resUserID + "@case.edu"));
                notification.Subject = "You Made a Reservation at Veale!";
                notification.Body = body;
                notification.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.case.edu");
                client.Send(notification);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReminderByEmail()
        {
            return true;
        }

        private string SubmitToDB()
        {
            //dbwrapper will check for errors for this method

            string reservationID;

            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            reservationID = wrap.MakeReservation(resDate, resStartTime, resEndTime, resRoom, resUserID);

            wrap.Disconnect();

            if (reservationID == null)
            {
                makeResMsg = "Error in making reservation. Data not submitted to DB.";
            }
            else
                makeResMsg = "Successfully made reservation: submitted to DB.";

            return reservationID;
        }

        public string SubmitStatus()
        {
            return null;
        }

    }
}