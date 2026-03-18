<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Reembolso_aprobacion.aspx.cs" Inherits="SIE_KEY_USER.Views.Reembolso_aprobacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="prestamos_form" runat="server" style="margin-top: 65px">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>

       <script>
           $(document).ready(function () {
               $("#<%=Monto.ClientID%>").keydown(function (e) {
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

        <img src="../Images/MenuKey/reembolso_escolar.png" id="img_deVentanas" />

        
        
        
        
        <div id="center_confPrestamo">
            <asp:Label ID="titulo_confPrestamo" ClientIDMode="Static" runat="server" Text="Formulario aprobación"></asp:Label>
        <div id="barra_gris"></div>
        
            <table class="table">
                <tr>
                    <td class="t_right">Monto:</td>
                    <td class="t_left"><asp:TextBox ID="Monto" runat="server" Wrap="True"></asp:TextBox> </td>
                </tr>
                <tr>
                    <td class="t_right">Observaciones:</td>
                    <td class="t_left"><asp:TextBox ID="Observaciones" runat="server"  TextMode="MultiLine" Rows="3" Width="220px" Enabled="False"></asp:TextBox> </td>
                </tr>
            </table>


       
         
        <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="aceptar_fam" ClientIDMode="Static" runat="server" Text="Guardar"  OnClientClick="PlaySound(); return true" OnClick="aceptar_fam_Click"  />

            <asp:Button ID="rechazar_fam" ClientIDMode="Static" CssClass="rechazar_fam" runat="server" Text="Cancelar" OnClick="rechazar_fam_Click"  />

            
    </div>
    </form>
    
</asp:Content>
