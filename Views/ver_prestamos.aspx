<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="ver_prestamos.aspx.cs" Inherits="SIE_KEY_USER.Views.ver_prestamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="verPrestamo_form" runat="server">

        <%------------------------------------- ScriptManager para correr update panel --------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <script>
            function CheckAll(oCheckBox) {
                var GridView1 = document.getElementById("<%=GridView1.ClientID %>");

                for (i = 1; i < GridView1.rows.length; i++) {
                    GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckBox.checked;
                }
            }
            $(document).ready(function () {
                $("#<%=TextBox1.ClientID%>").keydown(function (e) {
                    // Allow: backspace, delete, tab, escape, enter and .
                    //if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 || // ret: 110 and 190 keycodes removed for not allowing decimal point or period
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
        <%------------------------------------- FIN --------------------------------%>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text="" CssClass="nombre" ></asp:Label>

        <img src="../Images/MenuKey/prestamos.png" id="img_deVentanas" />

        <div id="center_verprestamos">

            <asp:Label ID="titulo_verprestamos" ClientIDMode="Static" runat="server" Text="Solicitudes generadas"></asp:Label>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <table id="tb_verprestamos">
                        <tr>
                            <td class="t_right">Empleado:</td>
                            <td class="t_left"><asp:TextBox ID="TextBox1" runat="server" TextMode="Number" placeholder="número de empleado" CssClass="CajaTexto" ></asp:TextBox></td>
                            <td><asp:Button ID="des_buscar" ClientIDMode="Static" runat="server" Text="Buscar" OnClick="des_buscar_Click" OnClientClick="PlaySound(); return true" /></td>
                            <td id="wdtd">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="loading" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loading.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>

                    <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" PageSize="5" AllowPaging="true" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="-1" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <RowStyle CssClass="Row" />
                        <AlternatingRowStyle CssClass="AltRow" />
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <HeaderTemplate>
                                    <input id="CheckBox1" type="checkbox" runat="server" onclick="CheckAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="seleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No se encontraron registros.
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Button ID="reiniciar_solicitud" runat="server" Text="Reiniciar Solicitud" OnClick="reiniciar_solicitud_Click" CssClass="quitar_prestamo" />

            <br /> <br />

            <!-------------------------------------------------------------- Bootstrap JS modal popup --------------------------------------->
            <asp:Button ID="ok_modal" ClientIDMode="Static" CssClass="quitar_prestamo" runat="server" Text="Eliminar solicitud" OnClientClick="PlaySound(); return false" />

            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">

                <div class="modal-dialog">
    
                    <!-- Modal content-->
                    <div class="modal-content" id="fondo_bloqueado" runat="server">
                        
                        <div class="modal-header">
                            <asp:Button ID="Button2" runat="server" Text="&times;" CssClass="close" data-dismiss="modal" />
                            <h4 class="modal-title">Motivo de rechazo</h4>
                        </div>
                        
                        <div class="modal-body">

                            <asp:Label ID="Label1" runat="server" Text="¿Seguro que deseas eliminar solicitud?"></asp:Label>

                        </div>
                        
                        <div class="modal-footer">
                            <asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Si" OnClick="aceptar_ac_Click" OnClientClick="PlaySound(); return true" />
                            <asp:Button ID="noaceptar_ac" ClientIDMode="Static" runat="server" Text="No" />
                            <asp:Button ID="Button3" runat="server" Text="Cerrar" CssClass="btn btn-default" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
            <!------------------------------------------------------------------ FIN ------------------------------------------------------>

        </div>

        <asp:Button ID="Button1" runat="server" Text="Regresar" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>