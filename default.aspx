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
    <!-- Login Page -->
    <div id="Page0" runat="server" style="display:none">
        <div><p id="ErrorText1" runat="server" style="color:red"/></div>
        <div>
            Case ID:
            <asp:TextBox ID="caseID" runat="server" />
        </div>
        <div>
            Password:
            <asp:TextBox TextMode="Password" ID="password" runat="server" />
        </div>
        <asp:Button ID="GoToProfile" runat="server" OnClick="GoToProfile_Click" Text="Login" />
    </div>

    <!-- Profile Page -->
    <div id="Page1" runat="server">
        Enter your name:
        <asp:TextBox id="txt1" runat="server" />
        <asp:Button ID="Button1" Text="See All Reservations" runat="server" OnClick="Button1_Click" />
        <asp:Label id="label1" runat="server" text="label"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ResDate" HeaderText="ResDate" SortExpression="ResDate" />
                <asp:BoundField DataField="ResStartTime" HeaderText="ResStartTime" SortExpression="ResStartTime" />
                <asp:BoundField DataField="ResEndTime" HeaderText="ResEndTime" SortExpression="ResEndTime" />
                <asp:BoundField DataField="ResRoom" HeaderText="ResRoom" SortExpression="ResRoom" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ViewUpcomingUserRes" TypeName="FinalProject.ReservationInfo">
            <SelectParameters>
                <asp:ControlParameter ControlID="caseID" Name="usrID" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2">
            <Columns>
                <asp:BoundField DataField="ResDate" HeaderText="ResDate" SortExpression="ResDate" />
                <asp:BoundField DataField="ResStartTime" HeaderText="ResStartTime" SortExpression="ResStartTime" />
                <asp:BoundField DataField="ResEndTime" HeaderText="ResEndTime" SortExpression="ResEndTime" />
                <asp:BoundField DataField="ResRoom" HeaderText="ResRoom" SortExpression="ResRoom" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="ViewReservationsByUser" TypeName="FinalProject.ReservationInfo">
            <SelectParameters>
                <asp:ControlParameter ControlID="txt1" Name="usrID" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    <asp:Button ID="GoToSearch" runat="server" OnClick="GoToSearch_Click" Text="Search Available Reservations" />
    <asp:Button ID="GoToMake" runat="server" OnClick="GoToMake_Click" Text="Make a Reservation" />
    </div>
    
    <!-- View Reservation Page -->
    <div id="Page2" runat="server" style="display:none">
        Select date:
        <asp:Calendar id="searchResCalendar" runat="server" OnSelectionChanged="GetDate"/>
        <asp:Label ID="DateLabel" runat="server" />
        <div>
            Room:
            <asp:DropDownList runat="server" ID="SearchRoomSelect">
                <asp:ListItem Text="--Select One--" Value="" />
                <asp:ListItem value="racquetball1">Racquetball 1</asp:ListItem>
                <asp:ListItem value="racquetball2">Racquetball 2</asp:ListItem>
                <asp:ListItem value="racquetball3">Racquetball 3</asp:ListItem>
                <asp:ListItem value="racquetball4">Racquetball 4</asp:ListItem>
                <asp:ListItem value="racquetball5">Racquetball 5</asp:ListItem>
                <asp:ListItem value="squash1">Squash 1</asp:ListItem>
                <asp:ListItem value="squash2">Squash 2</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:Button ID="BackButton" runat="server" OnClick="BackToHome_Click" Text="Back" />
            <asp:Button ID="SearchButton" runat="server" Text="Search" />
        </div>
        <asp:GridView ID="GridView3" runat="server" DataSourceID="ObjectDataSource3" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ResDate" HeaderText="ResDate" SortExpression="ResDate" />
                <asp:BoundField DataField="ResStartTime" HeaderText="ResStartTime" SortExpression="ResStartTime" />
                <asp:BoundField DataField="ResEndTime" HeaderText="ResEndTime" SortExpression="ResEndTime" />
                <asp:BoundField DataField="ResRoom" HeaderText="ResRoom" SortExpression="ResRoom" />
            </Columns>
        </asp:GridView>
    
    
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="ViewReservationsByRoom" TypeName="FinalProject.ReservationInfo">
            <SelectParameters>
                <asp:ControlParameter ControlID="searchResCalendar" Name="date" PropertyName="SelectedDate" Type="String" />
                <asp:ControlParameter ControlID="SearchRoomSelect" Name="rm" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    
    </div>

    <!-- Make Reservation Page -->
    <div id="Page3" runat="server" style="display:none">
        <div>
        <asp:Label runat="server" Text="To make a reservation, please fill out all fields below:" />
            </div>
        <asp:Label runat="server" Text="Date" />
        <asp:Calendar runat="server" ID="reservationCalendar" OnSelectionChanged="GetDate" />
       <div>
         Start Time:
            <asp:TextBox runat="server" ID="TextBoxMakeStart" />
        </div>
        <div>
           End Time:
            <asp:TextBox runat="server" ID="TextBoxMakeEnd" />
        </div>
        <div>
            Room:
            <asp:DropDownList runat="server" ID="MakeRoomSelect">
                <asp:ListItem Text="--Select One--" Value="" />
                <asp:ListItem value="racquetball1">Racquetball 1</asp:ListItem>
                <asp:ListItem value="racquetball2">Racquetball 2</asp:ListItem>
                <asp:ListItem value="racquetball3">Racquetball 3</asp:ListItem>
                <asp:ListItem value="racquetball4">Racquetball 4</asp:ListItem>
                <asp:ListItem value="racquetball5">Racquetball 5</asp:ListItem>
                <asp:ListItem value="squash1">Squash 1</asp:ListItem>
                <asp:ListItem value="squash2">Squash 2</asp:ListItem>
            </asp:DropDownList>
        </div>
        <p id="ErrorTextMakeRes" runat="server" style="color:red"/>
        <asp:Button ID="SubmitReservation" runat="server" OnClick="MakeNew_Click" Text="Submit" />
        <asp:Button ID="Back_Page3" runat="server" OnClick="BackToHome_Click" Text="Back" />
    </div>

</form>
</body>
</html>
