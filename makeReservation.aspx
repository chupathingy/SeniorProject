<%@ Page Language="C#" CodeFile="~/Reservation.cs" Inherits="Reservations"%>
<%@ Import Namespace="seniorProjDBWrapper" %>

<script runat="server">
void Submit_Click(Object sender, EventArgs e)
{
    String date = year.ToString() + "-" + month.ToString() + "-" + day.ToString(); 
    Reservation r = new Reservation(date, startTime.ToString(), endTime.ToString(), room.ToString(), name.ToString());
   // ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Make A New Reservation</title>
</head>
<body>
	<form id="Form1" runat="server">
	<div>
	Name: <asp:TextBox id="name" runat="server" /> <br>
	Month: <asp:TextBox id="month" runat="server" /><br>
	Day: <asp:TextBox id="day" runat="server" /><br>
	Year: <asp:TextBox id="year" runat="server" /><br>
	Room: <asp:TextBox ID="room" runat="server" /><br>
	Time Start: <asp:TextBox ID="startTime" runat="server" /><br>
	Time End: <asp:TextBox ID="endTime" runat="server" /><br>
	<asp:Button ID="Make_Reservation"
                Text="Submit"
                OnCLick="Submit_Click"
                runat="server" /><br>
	</div>
	</form>
    <div align="center">
		<asp:Label ID="reservationList" runat="server" Visible="true" Text="" />
       </div>
</body>
</html>