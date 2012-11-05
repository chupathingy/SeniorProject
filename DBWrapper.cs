using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace seniorProjDBWrapper
{
	/* public partial class SamplePage : System.Web.UI.Page
	{
		string reservationDate;

		protected void View_Click(object sender, EventArgs e)
		{
			//Get all reservations on that date
			reservationDate = year.Text + "-" + month.Text + "-" + day.Text;
			
			DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
			wrap.Connect();
			wrap.GetReservations(reservationDate);
			wrap.Disconnect();
		}

		protected void Submit_Click(object sender, EventArgs e)
		{
			//Make a Reservation using the given information
			DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
			wrap.Connect();
			wrap.MakeReservation(reservationDate, "11:00:00", "12:20:00", "racquetball3", "axl206");
			wrap.Disconnect();
		}
	} */

	public class DBWrapper
	{
		private MySqlConnection sqlConn;
		private string connStr;
		private bool isConnected;

		public static void Main ()
		{
			DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
			wrap.Connect();
			wrap.GetReservations("2012-10-14");
			Console.WriteLine(wrap.MakeReservation("2012-10-17", "12:00:00", "13:20:00", "squash1", "axl206"));
			wrap.Disconnect();
		}



		public DBWrapper (string svr, string db, string user, string pswrd)
		{
			this.connStr = "Server=" + svr + ";Database=" + db + ";Uid=" + user + ";Pwd=" + pswrd + ";";

			try {
				sqlConn = new MySqlConnection (this.connStr);
			} catch (Exception e) {
				Exception myExcep = new Exception("Error connecting to mySql server. Internal error message: " + e.Message, e);
				throw myExcep;
			}

			this.isConnected = false;
		}

		public DBWrapper (string connStr)
		{
			this.connStr = connStr;

			try {
				sqlConn = new MySqlConnection (this.connStr);
			} catch (Exception e) {
				Exception myExcep = new Exception("Error connecting to mySql server. Internal error message: " + e.Message, e);
				throw myExcep;
			}

			this.isConnected = false;
		}

		public void Connect ()
		{
			bool success = true;

			if (this.isConnected == false) {
				try {
					this.sqlConn.Open();
				} catch (Exception e) {
					this.isConnected = false;
					success = false;
					Exception myExcep = new Exception ("Error trying to open connection to the SQL server. Error: " + e.Message, e);

					throw myExcep;
				}

				if (success) {
					this.isConnected = true;
				}
			}
		}

		public void Disconnect ()
		{
			if (this.isConnected) {
				this.sqlConn.Close();
			}
		}

		public bool IsConnected 
		{
			get {
				return this.isConnected;
			}
		}

		public int MakeReservation (string date, string startTime, string endTime, string room, string id)
		{
			int rowsInserted;

			//string Check = "SELECT * FROM (SELECT * FROM (SELECT id, startTime, endTime FROM reservations WHERE roomName='" + 
			//	room + "' AND date='" + date + "') AS t WHERE startTime >= '" + 
			//		endTime + "' AND endTime >= '" + startTime + "') AS u WHERE endTime <= '" + startTime + "';";
			string Check = "SELECT * FROM (SELECT id, startTime, endTime FROM reservations WHERE roomName='" + 
				room + "' AND date='" + date + "') AS t WHERE startTime >= '" + 
					endTime + "' AND endTime >= '" + startTime + "';";

			string Query = "INSERT INTO reservations(date, startTime, endTime, roomName, userID) values ('" + 
				date + "', '" + startTime + "', '" + endTime + "', '" + room + "', '" + id + "');";

			MySqlCommand checkAvail = new MySqlCommand (Check, this.sqlConn);

			try 
			{
				if (checkAvail.ExecuteScalar() == null)
				{
					MySqlCommand insert = new MySqlCommand (Query, this.sqlConn);
					if ((rowsInserted = insert.ExecuteNonQuery()) == 1)
					{
						return 1;
					}
					return 0;
				}
				return -1;
			} 
			catch (Exception e) 
			{
				Exception myExcep = new Exception("Could not make reservation. Error: " + e.Message, e);
				throw (myExcep);
			}
		}

		public void GetReservations (string date)
		{
			string Query = "SELECT startTime, endTime, roomName, userID FROM reservations WHERE date='" + date + "';";

			MySqlCommand getReservations = new MySqlCommand(Query, this.sqlConn);
			MySqlDataReader myReader = getReservations.ExecuteReader();

			try
			{
				while(myReader.Read())
				{
					Console.WriteLine(myReader.GetString(0));
					Console.WriteLine(myReader.GetString(1));
					Console.WriteLine(myReader.GetString(2));
					Console.WriteLine(myReader.GetString(3));
					Console.WriteLine("\n");
				}
				
				myReader.Close();
			}
			catch(Exception e)
			{
				Exception myExcp = new Exception("Could not verify user. Error: " + e.Message, e);
				throw(myExcp);
			}
		}






	}
}

