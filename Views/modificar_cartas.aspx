<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="modificar_cartas.aspx.cs" Inherits="SIE_KEY_USER.Views.modificar_cartas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="modificarCartas" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/modificar_carta.png" id="img_deVentanas" />

        <div id="center_modificar">

            <asp:Label ID="titulo_modificar" ClientIDMode="Static" runat="server" Text="Elige la carta a la que desees modificar su estructura"></asp:Label>

            <div id="barra_gris"></div>

            <table id="tb_modificar">
                <tr>
                    <td><asp:Button ID="constanciaTrabajo" runat="server" Text="Constancia de Trabajo" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                    <td><asp:Button ID="migracion" runat="server" Text="Permiso de Migración" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                </tr>
                <tr>
                    <td><asp:Button ID="visaLaser" runat="server" Text="Carta para Visa Laser" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                    <td><asp:Button ID="altaimss" runat="server" Text="Carta alta IMSS" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                </tr>
                <tr>
                    <td><asp:Button ID="cambioTurno" runat="server" Text="Carta cambio horario IMSS" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                    <td><asp:Button ID="cambioClinica" runat="server" Text="Carta cambio clínica IMSS" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                </tr>
                <tr>
                    <td><asp:Button ID="ingresoGuarderia" runat="server" Text="Carta ingreso guardería" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                    <td><asp:Button ID="vacacionesGuarderia" runat="server" Text="Carta vacaciones guardería" CssClass="boton_cartas" OnClick="Button_Click" OnClientClick="PlaySound(); return true" /></td>
                </tr>
            </table>

            <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        </div>

        <asp:Button ID="Button9" runat="server" Text="Menú" CssClass="boton" OnClick="Button9_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>