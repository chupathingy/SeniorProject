<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject._default" %>

<%@ Import Namespace="FinalProject" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
    </script>
</head>
<body>
<form id="form0" runat="server">
    <div id="Page0" runat="server">
        <div><p id="ErrorText1" runat="server" text="" style="color:red"/></div>
        <div>
            Case ID:
            <asp:TextBox ID="caseID" runat="server" />
        </div>
        <div>
            Password:
            <asp:TextBox ID="password" runat="server" />
        </div>
        <asp:Button ID="GoToProfile" runat="server" OnClick="GoToProfile_Click" Text="Login" />
    </div>
    <div id="Page1" runat="server" style="display:none">
        Enter your name:
        <asp:TextBox id="txt1" runat="server" />
        <asp:Button ID="Button1" Text="Submit" runat="server" />
        <asp:Label id="label1" runat="server" text="label"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ResDate" HeaderText="Date" SortExpression="ResDate" />
                <asp:BoundField DataField="ResStartTime" HeaderText="Start Time" SortExpression="ResStartTime" />
                <asp:BoundField DataField="ResEndTime" HeaderText="End Time" SortExpression="ResEndTime" />
                <asp:BoundField DataField="ResRoom" HeaderText="Room" SortExpression="ResRoom" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ViewReservationsByUser" TypeName="FinalProject.ReservationInfo">
            <SelectParameters>
                <asp:ControlParameter ControlID="txt1" Name="usrID" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    <asp:Button ID="GoToSearch" runat="server" OnClick="GoToSearch_Click" Text="Search Available Reservations" />
    <asp:Button ID="GoToMake" runat="server" OnClick="GoToMake_Click" Text="Make a Reservation" />
    </div>
    <div id="Page2" runat="server" style="display:none">

    </div>
    <div id="Page3" runat="server" style="display:none">

    </div>

</form>
</body>
</html>
