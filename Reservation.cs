using System;

namespace seniorProjDBWrapper
{
	public class Reservation
	{
		public string resDate;
		public string resStartTime;
		public string resEndTime;
		public string resRoom;
		public string resID;

		//constructor 
		public Reservation (string date, string startTime, string endTime, string room, string id)
		{
			resDate = date;
			resStartTime = startTime;
			resEndTime = endTime;
			resRoom = room;
			resID = id;
		}

		public bool Cancel ()
		{

		}

		public string CancelStatus ()
		{

		}

		public bool Change (string newDate, string newStartTime, string newEndTime)
		{

		}

		public bool Change (string newStartTime, string newEndTime)
		{

		}

		public string ChangeStatus ()
		{

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
			return resID;
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

		}

		public string SubmitStatus ()
		{

		}

		public List<Reservation> ViewReservations ()
		{

		}

		public List<Reservation> ViewReservations (string startDate, string endDate)
		{

		}

		public List<Reservation> ViewReservationHistory ()
		{

		}

		public List<Reservation> ViewReservationHistory (string startDate, string endDate)
		{

		}


	}
}

