<%@ Page Language="C#" %>
<script runat="server">
void Submit_Click(Object sender, EventArgs e)
{
    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
}
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Insert title here</title>
</head>
<body>
	<form id="Form1" runat="server">
	<div>
	Name: <asp:TextBox id="name" runat="server" /> <br>
	Month: <input type="text" name="month"><br>
	Day: <input type="text" name="day"><br>
	Year: <input type="text" name="year"><br>
	Room: <input type="text" name="room"><br>
	Time Start: <input type="text" name="start"><br>
	Time End: <input type="text" name="start"><br>
	<asp:Button ID="Make_Reservation"
                Text="Submit"
                OnCLick="Submit_Click"
                runat="server" /><br>
	</div>
	</form>
</body>
</html>