<%@ Page Language="C#" CodeFile="~/DBWrapper.cs" Inherits="DBWrapper" %>
<%@ Import Namespace="seniorProjDBWrapper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    void Page_Load(Object sender, EventArgs e)
    {
        // Manually register the event-handling method for// the Click event of the Button control.
        GetDate.Click += new EventHandler(this.Get_Reservations);
    }

    void Get_Reservations(Object sender,
                           EventArgs e)
    {
        String date = year.ToString() + "-" + month.ToString() + "-" + day.ToString(); 
        public void ViewReservations (string date)
               {
                       DBWrapper wrap = new DBWrapper("localhost", "finalproject", "devon", "devon");
                       wrap.Connect();
                       List<Reservation> dayRes = wrap.GetReservationsByDay(date);
                       
                       wrap.Disconnect();

               }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>View Reservations</title>
</head>
<body>
    <table width="100%" height="100%" border="1">
  <tr>
    <td width="70%">
        <div align="center">
		<asp:Label ID="Reservations" runat="server" Visible="true" Text="" />
        </div>
	</td>
    <td width="30%">
		<form id="Form2" runat="server">
		<div align="center">
			Month: <asp:TextBox id="month" runat="server" /><br>
	        Day: <asp:TextBox id="day" runat="server" /><br>
	        Year: <asp:TextBox id="year" runat="server" /><br>
			<asp:Button ID="GetDate"
                Text="View Reservations"
                OnCLick="Get_Reservations"
                runat="server" /></br>
			<a href="makeReservation.aspx" target="_blank">Make A Reservation</a>
		</div>
		</form>
	</td>
  </tr>
</table>
      
</body>
</html>