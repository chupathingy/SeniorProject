<%@ Page Language="C#" %>

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
        // Display the greeting label text.
        Reservations.Text = "There is a reservation here";
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
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
			Month: <input type="text" name="month"><br>
			Day: <input type="text" name="day"><br>
			Year: <input type="text" name="year"><br>
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