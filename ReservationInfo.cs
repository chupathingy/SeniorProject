using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    public class ReservationInfo
    {
        public ReservationInfo()
        {
        }

        public List<Reservation> ViewReservations(string date)
        {
            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            List<Reservation> dayRes = wrap.GetReservationsByDay(date);

            wrap.Disconnect();

            return dayRes;
        }

        public List<Reservation> ViewReservationsByRoom(string date, string rm)
        {
            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            List<Reservation> rmRes = wrap.GetReservationsByRoom(date, rm);

            wrap.Disconnect();

            return rmRes;
        }

        public List<Reservation> ViewReservationsByUser(string usrID)
        {
            DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
            wrap.Connect();
            List<Reservation> usrRes = wrap.GetReservationsByUser(usrID);

            wrap.Disconnect();

            return usrRes;
        }

        public List<Reservation> ViewReservationHistory(string startDate, string endDate)
        {
            return null;
        }
    }
}