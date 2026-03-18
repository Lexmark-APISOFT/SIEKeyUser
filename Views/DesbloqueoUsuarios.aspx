<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="DesbloqueoUsuarios.aspx.cs" Inherits="SIE_KEY_USER.Views.DesbloqueoUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="desbloqueo_form" runat="server">

        <%------------------------------------- ScriptManager para correr update panel --------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
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

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text="" ></asp:Label>

        <img src="../Images/MenuKey/desbloqueo.png" id="img_deVentanas" />

        <div id="center_des_buscar">

            <%------------------------------------- UpdatePanel llamada al ajax --------------------------------%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <table id="tb_des_buscar">
                        <tr>
                            <td class="t_right">Empleado:</td>
                            <td class="t_left"><asp:TextBox ID="TextBox1" TextMode="Number" runat="server" placeholder="Número de empleado" CssClass="CajaTexto"></asp:TextBox></td>
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
                    
                    <asp:GridView ID="GridView1" runat="server" PageSize="5" AllowPaging="true" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="-1" OnPageIndexChanging="GridView1_PageIndexChanging" >
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

                    <div id="div_mensaje">
                        <asp:Label ID="mensaje" runat="server" Text=""></asp:Label>
                    </div>

                    <asp:Button ID="des_desbloquear" ClientIDMode="Static" runat="server" Text="Desbloquear" OnClick="des_desbloquear_Click" OnClientClick="PlaySound(); return true" />
                    <asp:Button ID="des_reestablecer" ClientIDMode="Static" runat="server" Text="Reestablecer contraseña" OnClick="des_reestablecer_Click" OnClientClick="PlaySound(); return true" />

                </ContentTemplate>
            </asp:UpdatePanel>
            <%------------------------------------- FIN --------------------------------%>

        </div>

        <asp:Button ID="Button1" runat="server" Text="Menú" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>
