<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calendario_vacaciones.aspx.cs" Inherits="SIE.Views.calendario_vacaciones" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modulo de Vacaciones</title>

</head>
<script language="C#" runat="server">

    void Selection_Change(Object s, EventArgs e)
    {

        ListItem li = new ListItem();
        li.Text = Calendar1.SelectedDate.ToShortDateString();
        BulletedList1.Items.Add(li);

        Calendar1.SelectedDates.Clear();
        SelectedDatesCollection dates = Calendar1.SelectedDates;

        foreach (ListItem litem in BulletedList1.Items)
        {
            DateTime date = Convert.ToDateTime(litem.Text);
            dates.Add(date);
            
        }
        

    }

    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);
    }

    void Calendar1_DayRender(object s, DayRenderEventArgs e)
    {
        //Inhabilitar dias festivos
        if (e.Day.IsWeekend)
        {
            e.Day.IsSelectable = false;
            e.Cell.ForeColor = System.Drawing.Color.BlueViolet;
        }


    }

</script>
    <style>
        .pos{
            position: absolute;
                top: 30px;
                left: 600px;
        }

        .pos2{
            position: absolute;
                top: 600px;
                left: 600px;
        }
    </style>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:Image ImageUrl="~/Images/LexmarkLogo_RGB_300.png" runat="server" />
            <div class="pos">
            <asp:Label > Días Hábiles:  <asp:Label ID="lblDiasHabiles" Text="text" runat="server" />  </asp:Label>
                <br>
               <asp:Label> Empleado: <asp:Label ID="lblNombreEmpleado" Text="" runat="server" /></asp:Label>
            </div>
        </div>

        <br />
    
        <asp:Calendar
            ID="Calendar1" 
            runat="server"
             SelectionMode="DayWeekMonth"
           ShowGridLines="True" 
            OnSelectionChanged="Selection_Change"
              OnDayRender="Calendar1_DayRender"
            NextPrevFormat="FullMonth"
            ForeColor="WhiteSmoke"
            DayNameFormat="Full"
            Font-Names="Book Antiqua"
            Font-Size="Medium"
            align="right"
            width="1200px"
            height="500px"                     
            >
            <DayHeaderStyle
                 BackColor="#142d4c"
                ForeColor="WhiteSmoke"
                 />
            <DayStyle
                 BackColor="LightGray"
                 ForeColor="#142d4c"
                 BorderColor="Khaki"
                 BorderWidth="1"
                 Font-Bold="true"
                 Font-Italic="true"
                 />
             <OtherMonthDayStyle BackColor="gray" />
            <NextPrevStyle
                 Font-Italic="true"
                 Font-Names="Arial CE"
                 />
            <SelectedDayStyle
                 BackColor="DarkOrange"
                 BorderColor="Orange"
                 />
            <SelectorStyle
                 BackColor="#142d4c"
                 ForeColor="Snow"
                 Font-Names="Times New Roman Greek"
                 Font-Size="Small"
                 BorderColor="Olive"
                 BorderWidth="1"
                 />

        </asp:Calendar>

        <div class="pos2">
         <asp:Label>  <asp:Label ID="txtFirstDate" Text="" runat="server" /></asp:Label>
            <br />
         <asp:Label> <asp:Label ID="txtLastDate" Text="" runat="server" /></asp:Label>
            </div>

           <asp:BulletedList
             ID="BulletedList1"
             runat="server"
             Visible="true"
             >
        </asp:BulletedList>
    </form>



    
</body>
</html>
