<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FinalProject._default" %>

<%@ Import Namespace="FinalProject" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script>

    </script>
</head>
<body>
<form id="form0" runat="server">
    <!-- Login Page -->
    <div id="Page0" runat="server" style="display:none">
        <p id="ErrorText1" runat="server" style="color:red"/>
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
    <div id="Page1" runat="server" style="display:none">
        Hello User!
        <p><asp:Button ID="Button1" Text="See All Reservations" runat="server" OnClick="Button1_Click" /></p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" AutoGenerateSelectButton="true" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
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
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" AutoGenerateSelectButton="true">
            <Columns>
                <asp:BoundField DataField="ResDate" HeaderText="ResDate" SortExpression="ResDate" />
                <asp:BoundField DataField="ResStartTime" HeaderText="ResStartTime" SortExpression="ResStartTime" />
                <asp:BoundField DataField="ResEndTime" HeaderText="ResEndTime" SortExpression="ResEndTime" />
                <asp:BoundField DataField="ResRoom" HeaderText="ResRoom" SortExpression="ResRoom" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="ViewReservationsByUser" TypeName="FinalProject.ReservationInfo">
            <SelectParameters>
                <asp:ControlParameter ControlID="caseID" Name="usrID" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div>
            <asp:Button ID="ChangeRes" runat="server" Text="Change Reservation" OnClick="ChangeRes_Click"/>
            <asp:Button ID="CancelRes" runat="server" Text="Cancel Reservation" OnClick="CancelRes_Click"/>
        </div>
        <div>
            <asp:Button ID="GoToSearch" runat="server" OnClick="GoToSearch_Click" Text="Search Available Reservations" />
            <asp:Button ID="GoToMake" runat="server" OnClick="GoToMake_Click" Text="Make a Reservation" />
        </div>
        <p id="ErrorText2" runat="server" style="color:red"/>
    </div>
    
    <!-- View Reservation Page -->
    <div id="Page2" runat="server"  style="display:none">
        <table border="0" align="center">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Select date:" />
                    <asp:Calendar id="searchResCalendar" runat="server" OnSelectionChanged="GetDate"
                        ForeColor="#000099" TodayDayStyle-BackColor="#000099" TodayDayStyle-ForeColor="White" 
                        SelectedDayStyle-BackColor="#000099" SelectedDayStyle-BorderColor="White" OtherMonthDayStyle-Wrap="false" Height="249px" Width="355px"
                        TitleStyle-BackColor="#000099" TitleStyle-ForeColor="White">
                        <OtherMonthDayStyle Wrap="False" />
                        <SelectedDayStyle BackColor="#000099" BorderColor="White" />
                        <TodayDayStyle BackColor="#000099" ForeColor="White" />
                    </asp:Calendar>
                </td>
                <td>
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ResDate" HeaderText="ResDate" SortExpression="ResDate" />
                            <asp:BoundField DataField="ResStartTime" HeaderText="ResStartTime" SortExpression="ResStartTime" />
                            <asp:BoundField DataField="ResEndTime" HeaderText="ResEndTime" SortExpression="ResEndTime" />
                            <asp:BoundField DataField="ResRoom" HeaderText="ResRoom" SortExpression="ResRoom" />
                        </Columns>
                     </asp:GridView>
                     <!--<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="ViewReservationsByRoom" TypeName="FinalProject.ReservationInfo">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DateLabel" Name="date" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="SearchRoomSelect" Name="rm" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>-->
                    <p id="tests" runat="server"></p>  
                </td>
            </tr>
            <tr>
                <td>
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
                         <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>

    <!-- Make Reservation Page -->
    <div id="Page3" runat="server">

            <table align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="To make a reservation, please fill out all fields below:" />
                            <div><asp:Label ID="Label2" runat="server" Text="Date" /></div>
                            <asp:Calendar runat="server" ID="reservationCalendar" OnSelectionChanged="GetDate" 
                                ForeColor="#000099" TodayDayStyle-BackColor="#000099" TodayDayStyle-ForeColor="White" 
                                SelectedDayStyle-BackColor="#000099" SelectedDayStyle-BorderColor="White" OtherMonthDayStyle-Wrap="false" Height="249px" Width="355px"
                                TitleStyle-BackColor="#000099" TitleStyle-ForeColor="White"/>
                    </td>
                    <td align="center">
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
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="center">
                    <p id="ErrorTextMakeRes" runat="server" style="color:red"/>
                    <asp:Button ID="SubmitReservation" runat="server" OnClick="MakeNew_Click" Text="Submit" />
                    <asp:Button ID="Back_Page3" runat="server" OnClick="BackToHome_Click" Text="Back" />
                    </td>
                </tr>
            </table>
    </div>

</form>
</body>
</html>
