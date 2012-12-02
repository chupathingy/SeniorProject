using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

        //properties
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
        public Reservation(string date, string startTime, string endTime, string room, string userId)
        {
            resDate = date;
            resStartTime = startTime;
            resEndTime = endTime;
            resRoom = room;
            resUserID = userId;

            resID = SubmitToDB();
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

        public bool NotifyByEmail()
        {
            return true;
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