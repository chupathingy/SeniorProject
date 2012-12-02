using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    public class DBWrapper
    {
        private MySqlConnection sqlConn;
        private string connStr;
        private bool isConnected;

        //public static void Main ()
        //{
        //	DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
        //	wrap.Connect();
        //	wrap.GetReservations("2012-10-14");
        //	Console.WriteLine(wrap.MakeReservation("2012-10-17", "12:00:00", "13:20:00", "squash1", "axl206"));
        //	wrap.Disconnect();
        //}

        public DBWrapper(string svr, string db, string user, string pswrd)
        {
            this.connStr = "Server=" + svr + ";Database=" + db + ";Uid=" + user + ";Pwd=" + pswrd + ";";

            try
            {
                sqlConn = new MySqlConnection(this.connStr);
            }
            catch (Exception e)
            {
                Exception myExcep = new Exception("Error connecting to mySql server. Internal error message: " + e.Message, e);
                throw myExcep;
            }

            this.isConnected = false;
        }

        public DBWrapper(string connStr)
        {
            this.connStr = connStr;

            try
            {
                sqlConn = new MySqlConnection(this.connStr);
            }
            catch (Exception e)
            {
                Exception myExcep = new Exception("Error connecting to mySql server. Internal error message: " + e.Message, e);
                throw myExcep;
            }

            this.isConnected = false;
        }

        public void Connect()
        {
            bool success = true;

            if (this.isConnected == false)
            {
                try
                {
                    this.sqlConn.Open();
                }
                catch (Exception e)
                {
                    this.isConnected = false;
                    success = false;
                    Exception myExcep = new Exception("Error trying to open connection to the SQL server. Error: " + e.Message, e);

                    throw myExcep;
                }

                if (success)
                {
                    this.isConnected = true;
                }
            }
        }

        public void Disconnect()
        {
            if (this.isConnected)
            {
                this.sqlConn.Close();
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }

        public string MakeReservation(string date, string startTime, string endTime, string room, string UserId)
        {
            int rowsInserted;
            string idval;
            /*string Check = "SELECT * FROM (SELECT * FROM (SELECT id, startTime, endTime FROM reservations WHERE roomName='" + 
                room + "' AND date='" + date + "') AS t WHERE startTime >= '" + 
                    endTime + "' AND endTime >= '" + startTime + "') AS u WHERE endTime <= '" + startTime + "';";*/

            string Check = "SELECT * FROM (SELECT id, startTime, endTime FROM reservations WHERE roomName='" +
                room + "' AND date='" + date + "') AS t WHERE startTime >= '" +
                    endTime + "' AND endTime >= '" + startTime + "';";

            string InsertQuery = "INSERT INTO reservations(date, startTime, endTime, roomName, userID) values ('" +
                date + "', '" + startTime + "', '" + endTime + "', '" + room + "', '" + UserId + "');";

            string GetIDQuery = "SELECT id FROM reservations WHERE date='" + date + "' AND startTime='" + startTime
                + "' AND endTime='" + endTime + "' AND roomName='" + room + "' AND userID='" + UserId + "';";

            string UpdateResCount = "UPDATE date SET reservations=reservations+1 WHERE date='" + date + "';";

            MySqlCommand checkAvail = new MySqlCommand(Check, this.sqlConn);
            MySqlCommand insert = new MySqlCommand(InsertQuery, this.sqlConn);
            MySqlCommand getID = new MySqlCommand(GetIDQuery, this.sqlConn);
            MySqlCommand updateCount = new MySqlCommand(UpdateResCount, this.sqlConn);

            try
            {
                //make sure insert parameters are valid
                if (checkAvail.ExecuteScalar() == null)
                {
                    //insert row
                    rowsInserted = insert.ExecuteNonQuery();
                    if (rowsInserted != 1)
                    {
                        throw new System.InvalidOperationException("Failed to insert reservation into database.");
                    }
                }

                //update number of reservations in date table
                updateCount.ExecuteNonQuery();

                //get the unique ID number of the reservation
                MySqlDataReader myReader = getID.ExecuteReader();
                myReader.Read();
                idval = myReader.GetString(0);
                myReader.Close();
            }
            catch (Exception e)
            {
                Exception myExcep = new Exception("Could not make reservation. Error: " + e.Message, e);
                throw (myExcep);
            }

            //return resID as STRING
            return idval;
        }

        public List<Reservation> GetReservationsByDay(string date)
        {
            string Query = "SELECT startTime, endTime, roomName, userID FROM reservations WHERE date='" + date + "';";

            MySqlCommand getReservations = new MySqlCommand(Query, this.sqlConn);
            MySqlDataReader myReader = getReservations.ExecuteReader();

            List<Reservation> reservations = new List<Reservation>();

            try
            {
                while (myReader.Read())
                {
                    Reservation res = new Reservation(date, myReader.GetString(0), myReader.GetString(1), myReader.GetString(2), myReader.GetString(3));
                    reservations.Add(res);
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                Exception myExcp = new Exception("Could not verify user. Error: " + e.Message, e);
                throw (myExcp);
            }

            return reservations;
        }

        public List<Reservation> GetReservationsByRoom(string date, string rm)
        {
            string Query = "SELECT startTime, endTime, roomName, userID FROM reservations WHERE room='" + rm + "' AND date='" + date + "';";

            MySqlCommand getReservations = new MySqlCommand(Query, this.sqlConn);
            MySqlDataReader myReader = getReservations.ExecuteReader();

            List<Reservation> reservations = new List<Reservation>();

            try
            {
                while (myReader.Read())
                {
                    Reservation res = new Reservation(date, myReader.GetString(0), myReader.GetString(1), myReader.GetString(2), myReader.GetString(3));
                    reservations.Add(res);
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                Exception myExcp = new Exception("Could not verify user. Error: " + e.Message, e);
                throw (myExcp);
            }

            return reservations;
        }

        public List<Reservation> GetReservationsByUser(string uid)
        {
            string Query = "SELECT date, startTime, endTime, roomName FROM reservations WHERE userID='" + uid + "';";

            MySqlCommand getReservationsByUser = new MySqlCommand(Query, this.sqlConn);
            MySqlDataReader myReader = getReservationsByUser.ExecuteReader();

            List<Reservation> reservations = new List<Reservation>();

            try
            {
                while (myReader.Read())
                {
                    string translateDate = myReader.GetDateTime(0).ToString("yyyy-MM-dd");
                    Reservation res = new Reservation(translateDate, myReader.GetString(1), myReader.GetString(2), myReader.GetString(3), uid);
                    reservations.Add(res);
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                Exception myExcp = new Exception("Could not verify user. Error: " + e.Message, e);
                throw (myExcp);
            }

            return reservations;
        }

        public List<Reservation> GetReservationsByUser(string uid, string startDate, string endDate)
        {
            string Query = "SELECT date, startTime, endTime, roomName FROM reservations WHERE userID='"
                + uid + "' AND date >= '" + startDate + "' AND date <= '" + endDate + "';";

            MySqlCommand getReservationsByUser = new MySqlCommand(Query, this.sqlConn);
            MySqlDataReader myReader = getReservationsByUser.ExecuteReader();

            List<Reservation> reservations = new List<Reservation>();

            try
            {
                while (myReader.Read())
                {
                    string translateDate = myReader.GetDateTime(0).ToString("yyyy-MM-dd");
                    Reservation res = new Reservation(translateDate, myReader.GetString(1), myReader.GetString(2), myReader.GetString(3), uid);
                    reservations.Add(res);
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                Exception myExcp = new Exception("Could not verify user. Error: " + e.Message, e);
                throw (myExcp);
            }

            return reservations;
        }

        public bool CancelReservation(string reservID)
        {
            //get needs to be executed separately first because we have to get date first
            string date;
            string GetResDateQuery = "SELECT * FROM reservations WHERE id='" + reservID + "';";
            MySqlCommand getResDate = new MySqlCommand(GetResDateQuery, this.sqlConn);
            try
            {
                MySqlDataReader myReader = getResDate.ExecuteReader();
                myReader.Read();
                date = myReader.GetDateTime(1).ToString("yyyy-MM-dd");
                myReader.Close();
            }
            catch (Exception e)
            {
                Exception myExp = new Exception("Could not get reservation date for deletion. Error: " + e.Message, e);
                throw (myExp);
            }

            //then execute rest of delete
            string UpdateResCountQuery = "UPDATE date SET reservations=reservations-1 WHERE date='" + date + "';";
            string DeleteQuery = "DELETE FROM reservations WHERE id='" + reservID + "';";

            MySqlCommand updateCount = new MySqlCommand(UpdateResCountQuery, this.sqlConn);
            MySqlCommand deleteRes = new MySqlCommand(DeleteQuery, this.sqlConn);

            try
            {
                //delete reservation from reservations table
                deleteRes.ExecuteNonQuery();

                //update number of reservations in date table
                updateCount.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Exception myExp = new Exception("Could not delete reservation. Error: " + e.Message, e);
                throw (myExp);
            }
            return true;
        }

        public int ChangeReservation(string rID, string newDate, string newStartTime, string newEndTime, string newRoom, string ID)
        {
            //change reservation is the same as deleting an old reservation and making a new one

            string Query = "UPDATE reservations SET date='" + newDate + "', startTime='" + newStartTime
                + "', endTime='" + newEndTime + "' AND roomName='" + newRoom + "' WHERE id='" + ID + "';";

            try
            {
                MySqlCommand changeRes = new MySqlCommand(Query, this.sqlConn);
            }
            catch (Exception e)
            {
                Exception myExp = new Exception("Could not delete reservation. Error: " + e.Message, e);
                throw (myExp);
            }

            return 1;
        }

        public CurrentUser GetUserInfo(string uid)
        {
            //get user info
            CurrentUser cUser;
            string RetrieveInfoQuery = "SELECT userName, lastLogin, banned FROM users WHERE userID ='" + uid + "';";

            MySqlCommand getUserInfo = new MySqlCommand(RetrieveInfoQuery, this.sqlConn);

            try
            {
                MySqlDataReader myReader = getUserInfo.ExecuteReader();
                myReader.Read();
                cUser = new CurrentUser(uid, myReader.GetString(0), myReader.GetString(1), Convert.ToInt16(myReader.GetString(2)));

                myReader.Close();
            }
            catch (Exception e)
            {
                Exception myExcp = new Exception("Could not retrieve user information. Error: " + e.Message, e);
                throw (myExcp);
            }

            //update last login
            DateTime dt = DateTime.Now;
            string UpdateLoginQuery = "UPDATE users SET lastlogin = '" + String.Format("{0:yyyy'-'MM'-'dd HH':'mm':'ss}", dt) +
                "' WHERE userid = '" + uid + "';";

            try
            {
                MySqlCommand updateLogin = new MySqlCommand(UpdateLoginQuery, this.sqlConn);
            }
            catch (Exception e)
            {
                Exception myExp = new Exception("Could not delete reservation. Error: " + e.Message, e);
                throw (myExp);
            }

            //return object
            return cUser;
        }

        public void UpdateUserLastLogin(string uid)
        {
            string UpdateLoginQuery = "UPDATE users SET lastLogin=current_timestamp() WHERE userID='" + uid + "';";
            MySqlCommand updateLogin = new MySqlCommand(UpdateLoginQuery, this.sqlConn);

            try
            {
                updateLogin.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Exception myExp = new Exception("Could not update user's last-login. Error: " + e.Message, e);
                throw (myExp);
            }
        }

        public bool VerifyLogin(string uid, string pwd)
        {
            string VerificationQuery = "SELECT * FROM users WHERE userID='" + uid + "' AND password='" + pwd + "';";
            MySqlCommand verify = new MySqlCommand(VerificationQuery, this.sqlConn);

            try
            {
                //make sure user ID and pswrd are valid
                if (String.Compare((string)verify.ExecuteScalar(), uid) == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Exception myExp = new Exception("Could not verify login information. Error: " + e.Message, e);
                throw (myExp);
            }
        }
    }
}