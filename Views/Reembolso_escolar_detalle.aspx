<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Reembolso_escolar_detalle.aspx.cs" Inherits="SIE_KEY_USER.Views.Reembolso_escolar_detalle" %>

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
               $("#<%=TextProm.ClientID%>").keydown(function (e) {
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

        <asp:Button ID="Button1" CssClass="boton" runat="server" Text="Regresar" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />
        
        
        
        <div id="center_confPrestamo">
            <asp:Label ID="titulo_confPrestamo" ClientIDMode="Static" runat="server" Text="Detalle de solicitud de reembolso"></asp:Label>
        <div id="barra_gris"></div>
        <table id="Table1" class="table" runat="server">
            <tr>
                <td class="t_right" ><asp:Label ID="NumEmp" runat="server" Text="Num. Empleado:" ></asp:Label></td>
                <td class="t_left"><asp:TextBox ID="TextNumEmp" runat="server" Enabled="False" Wrap="True"></asp:TextBox></td>
                <td class="t_right" ><asp:Label ID="NomEmp" runat="server" Text="Nombre de Empleado:"></asp:Label></td>
                <td class="t_left"><asp:TextBox ID="TextNom" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
        </table>



        <table id="Table2" class="table" runat="server"   >
            <tr>
                <td class="t_right"><asp:Label ID="FecSol" runat="server" Text="Fecha de Solicitud:"></asp:Label></td>
                <td class="t_left"><asp:TextBox ID="TextFec" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t_right"><asp:Label ID="Kardex" runat="server" Text="Tipo kárdex:"></asp:Label></td>
                <td class="t_left"><asp:TextBox ID="TextKard" runat="server" Enabled="False" Text="REMBOL"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t_right"><asp:Label ID="Escuela" runat="server" Text="Escuela:"></asp:Label></td>
                <td class="t_left"><asp:TextBox ID="TextEsc" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t_right"><asp:Label ID="Periodo" runat="server" Text="Periodo:"></asp:Label></td>
                <td class="t_left"><asp:DropDownList ID="DropDP" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t_right"><asp:Label ID="Promedio" runat="server" Text="Promedio:"></asp:Label></td>
                <td class="t_left"><asp:TextBox ID="TextProm" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t_right"><asp:Label ID="Porcentaje" runat="server" Text="Porcentaje:"></asp:Label></td>
                <td class="t_left"><asp:TextBox ID="TextPorc" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
        </table>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Boleta de Calificaciones" />
        <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="aceptar_fam" ClientIDMode="Static" runat="server" Text="Aceptar"  OnClientClick="PlaySound(); return true" OnClick="guardar_configurar_Click" />

            <asp:Button ID="ok_modal" ClientIDMode="Static" CssClass="rechazar_fam" runat="server" Text="Rechazar" OnClientClick="return false" />

            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">

                <div class="modal-dialog">
    
                    <!-- Modal content-->
                    <div class="modal-content" id="fondo_bloqueado" runat="server">
                        
                        <div class="modal-header">
                            <asp:Button ID="Button3" runat="server" Text="&times;" CssClass="close" data-dismiss="modal" />
                            <h4 class="modal-title">Motivo de rechazo</h4>
                        </div>
                        
                        <div class="modal-body">
                                    
                            <div>
                                <asp:TextBox ID="area_txtAgreg" ClientIDMode="Static" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>

                            <asp:Label ID="Label1" runat="server" Text="¿Seguro que deseas rechazar familiar?"></asp:Label>

                        </div>
                        
                        <div class="modal-footer">
                            <asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Si" OnClick="aceptar_ac_Click" OnClientClick="PlaySound(); return true" />
                            <asp:Button ID="noaceptar_ac" ClientIDMode="Static" runat="server" Text="No" />
                            <asp:Button ID="Button4" runat="server" Text="Cerrar" CssClass="btn btn-default" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
    </div>
    </form>
    
</asp:Content>