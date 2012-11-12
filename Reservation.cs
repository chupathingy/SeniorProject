using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace seniorProjDBWrapper
{
	public class Reservation
	{
		public string resDate;
		public string resStartTime;
		public string resEndTime;
		public string resRoom;
		public string resUserID;
		public string resID;

		public string cancelMsg = null;
		public string changeMsg = null;
		public string makeResMsg = null;

		//constructor 
		public Reservation (string date, string startTime, string endTime, string room, string userId)
		{
			resDate = date;
			resStartTime = startTime;
			resEndTime = endTime;
			resRoom = room;
			resUserID = userId;

			//need to throw an exception here
			resID = SubmitToDB();
		}

		public bool Cancel ()
		{
			DateTime time = DateTime.Now;
			string dateFormat = "yyyy-MM-dd";
			string timeFormat = "HH:mm:ss";

			string today = time.ToString(dateFormat);

			//check for possible errors before submitting the cancellation to DB
			if (resDate < today) {
				cancelMsg = "Unable to cancel reservation: date has already passed.";
				return false;
			} else if (resDate == today) {
				if (resStartTime < time.ToString(timeFormat)) {
					cancelMsg = "Unable to cancel reservation: reserved time has already passed.";
					return false;
				}
			}

			//connect to DB for cancellation
			DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
			wrap.Connect();
			wrap.CancelReservation(resDate, resStartTime, resEndTime, resRoom, resUserID);
			wrap.Disconnect();

			cancelMsg = "Cancellation successful.";
			return true;
		}

		public string CancelStatus ()
		{
			return cancelMsg;
		}

		public bool Change (string newDate, string newStartTime, string newEndTime)
		{
			DateTime time = DateTime.Now;
			string dateFormat = "yyyy-MM-dd";
			string timeFormat = "HH:mm:ss";
			
			string today = time.ToString(dateFormat);

			//check for possible errors before submitting the change to DB
			if (resDate < today) {
				changeMsg = "Unable to change reservation: date has already passed.";
				return false;
			} else if (resDate == today) {
				if (resStartTime < time.ToString(timeFormat)) {
					changeMsg = "Unable to change reservation: reserved time has already passed.";
					return false;
				}
			}

			//connect to DB for change
			DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
			wrap.Connect();
			wrap.ChangeReservation(resDate, resStartTime, resEndTime, resRoom, resUserID);
			wrap.Disconnect();
			
			cancelMsg = "Change successful.";
			return true;
		}

		public bool Change (string newStartTime, string newEndTime)
		{

		}

		public string ChangeStatus ()
		{
			return changeMsg;
		}

		public void ConfirmPrompt ()
		{

		}

		public string GetDate ()
		{
			return resDate;
		}

		public string GetStartTime ()
		{
			return resStartTime;
		}

		public string GetEndTime ()
		{
			return resEndTime;
		}

		public string GetRoomName ()
		{
			return resRoom;
		}

		public string GetUserID ()
		{
			return resUserID;
		}

		public string MakeStatus ()
		{

		}

		public bool NotifyByEmail ()
		{

		}

		public bool ReminderByEmail ()
		{

		}

		public int SubmitToDB ()
		{
			//dbwrapper will check for errors for this method

			int reservationID;

			DBWrapper wrap = new DBWrapper("129.22.124.108", "finalproject", "devon", "devon");
			wrap.Connect();
			reservationID = wrap.MakeReservation(resDate, resStartTime, resEndTime, resRoom, resUserID);
			
			wrap.Disconnect();

			return reservationID;
		}

		public string SubmitStatus ()
		{

		}

		public List<Reservation> ViewReservations ()
		{
			//needs to be moved to a different class


		}

		public List<Reservation> ViewReservations (string startDate, string endDate)
		{
			//needs to be moved to a different class
		}

		public List<Reservation> ViewReservationHistory ()
		{
			//needs to be moved to a different class
		}

		public List<Reservation> ViewReservationHistory (string startDate, string endDate)
		{
			//needs to be moved to a different class
		}


	}
}

