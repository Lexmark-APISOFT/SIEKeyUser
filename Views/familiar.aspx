<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="familiar.aspx.cs" Inherits="SIE_KEY_USER.Views.familiar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="familiar" runat="server">

        <%------------------------------------- ScriptManager para correr update panel --------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>
        <script>
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

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/agregar_familiares.png" id="img_deVentanas" />

        <div id="center_familiar">

            <asp:Label ID="titulo_familiar" ClientIDMode="Static" runat="server" Text="Solicitudes pendientes por número de empleado"></asp:Label>

            <div id="barra_gris"></div>

            <%------------------------------------- UpdatePanel llamada al ajax --------------------------------%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
                    <table id="tb_familiar">
                        <tr>
                            <td class="t_right">Empleado:</td>
                            <td class="t_left"><asp:TextBox ID="TextBox1" runat="server" CssClass="CajaTexto" TextMode="Number" placeholder="Número de empleado" ></asp:TextBox></td>
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

                    <asp:Label ID="prettyName" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                    <div id="center_familiar1">

                        <asp:GridView ID="GridView1" ClientIDMode="Static" runat="server" PageSize="5" AllowPaging="true" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="0" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                            <RowStyle CssClass="Row" />
                            <AlternatingRowStyle CssClass="AltRow" />
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="fam_ancho" HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" >Ver familiares..</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros.
                            </EmptyDataTemplate>
                        </asp:GridView>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <%------------------------------------- FIN --------------------------------%>

        </div>

        <asp:Button ID="Button1" runat="server" Text="Menú" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

        <table id="tb_fam_ver">
            <tr>
                <td><asp:LinkButton ID="LinkButton1" runat="server" CssClass="agregar_ver" OnClick="LinkButton1_Click" OnClientClick="PlaySound(); return true" >Ver Aceptados</asp:LinkButton></td>
            </tr>
            <tr>
                <td><asp:LinkButton ID="LinkButton2" runat="server" CssClass="agregar_ver" OnClick="LinkButton2_Click" OnClientClick="PlaySound(); return true" >Ver Rechazados</asp:LinkButton></td>
            </tr>
        </table>

    </form>

</asp:Content>