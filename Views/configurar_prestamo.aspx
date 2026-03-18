<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="configurar_prestamo.aspx.cs" Inherits="SIE_KEY_USER.Views.configurar_prestamo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="configurar_prestamo_form" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
                <asp:ScriptReference Path="//code.jquery.com/jquery-1.10.2.js" />
                <asp:ScriptReference Path="//code.jquery.com/ui/1.11.4/jquery-ui.js" />
                <asp:ScriptReference Path="~/Scripts/datepicker/datepicker_icon_trigger.js" />
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>
         <script>
             

             $(document).ready(function () {
                 $(".datepicker1").datepicker({
                     showOn: "button",
                     buttonImage: "../Images/calendar.png",
                     buttonImageOnly: true,
                     buttonText: "Select date",
                     dateFormat: "yy-mm-dd",
                 });
                 $(".ui-datepicker-trigger").css("width", "8%");
                 $(".ui-datepicker-trigger").css("margin-left", "4%");
                 $(".datepicker1").change(function () {
                     var start = $('#<%=TextBox1.ClientID%>').datepicker('getDate');
                     var end = $('#<%=TextBox2.ClientID%>').datepicker('getDate');

                     // end - start returns difference in milliseconds 
                     var diff = new Date(end - start);

                     // get days
                     var days = Math.floor(diff / 1000 / 60 / 60 / 24 / 7) + 1;
                     $('#<%=hdnField.ClientID%>').val(days);
                     $('#<%=TextBox3.ClientID%>').val(days);
                 });

                $(".txtmount").keydown(function (e) {
                    // Allow: backspace, delete, tab, escape, enter and .
                    //if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 || // ret: 110 and 190 keycodes removed for not allowing decimal point or period
                        // Allow: Ctrl+A
                        (e.keyCode == 65 && e.ctrlKey === true) ||
                        // Allow: Ctrl+C
                        (e.keyCode == 67 && e.ctrlKey === true) ||
                        // Allow: Ctrl+X
                        (e.keyCode == 88 && e.ctrlKey === true) ||
                        // Allow: home, end, left, right
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                        // let it happen, don't do anything
                        return;
                    }
                    // Ensure that it is a number and stop the keypress
                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });
            });

        </script>
        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/prestamos.png" id="img_deVentanas" />

        <div id="center_confPrestamo">

            <asp:Label ID="titulo_confPrestamo" ClientIDMode="Static" runat="server" Text="Configuración de periodo de préstamos de fondo de ahorro"></asp:Label>

            <div id="barra_gris"></div>

            <table id="tb_confPrestamo">
                <tr>
                    <td class="t_right" >Fecha de inicio:</td>
                    <td class="t_left" ><asp:TextBox ID="TextBox1" CssClass="datepicker1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="t_right">Fecha de término:</td>
                    <td class="t_left"><asp:TextBox ID="TextBox2" CssClass="datepicker1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="t_right">Número de semanas máximo<br />para pagar:</td>
                    <td class="t_left"><asp:TextBox ID="TextBox3" runat="server" TextMode="Number" CssClass="txtmount" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="t_right">Número de semanas mínimo<br />para pagar:</td>
                    <td class="t_left"><asp:TextBox ID="TextBox4" runat="server" TextMode="Number" CssClass="txtmount"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="t_right">Tasa de interés:</td>
                    <td class="t_left"><asp:TextBox ID="TextBox5" runat="server" CssClass="txtmount" ></asp:TextBox></td>
                </tr>
            </table>

            <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>

            <asp:Button ID="guardar_configurar" ClientIDMode="Static" runat="server" Text="Guardar" OnClick="guardar_configurar_Click" OnClientClick="PlaySound(); return true" />

            <asp:Button ID="cancelar_configurar" ClientIDMode="Static" runat="server" Text="Cancelar" OnClick="cancelar_configurar_Click" OnClientClick="PlaySound(); return true" />

        </div>
        <asp:HiddenField runat="server" ID="hdnField" />
        <asp:Button ID="Button1" runat="server" Text="Regresar" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>
