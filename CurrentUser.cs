using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    public class CurrentUser
    {
        //fields
        private string uID;
        private string uName;
        private string uLastLogin;
        private int banned;

        //constructor
        public CurrentUser(string userID, string userName, string userLastLogin, int bnd)
        {
            uID = userID;
            uName = userName;
            uLastLogin = userLastLogin;
            banned = bnd;
        }
    }
}