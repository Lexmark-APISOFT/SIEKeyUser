<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SIE_KEY_USER.Views.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function closeWIndow() {
            window.close();
        }
    </script>
    <form id="default_form" runat="server">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav"></audio>

        <div id="titulo">
            SIE
        </div> 

        <div id="sub-t">
            Sistema de Información para el Empleado
            <br />
            Recursos Humanos
        </div>

        <div id="center_default">

            <table id="tb_default">
                <tr>
                    <td class="etiquetaderecha">Usuario:</td>
                    <td class="CajaTexto"><asp:TextBox ID="txtUsername" runat="server" CssClass="CajaTexto" placeholder="Usuario" AutoCompleteType="Disabled"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="etiquetaderecha">Contraseña:</td>
                    <td class="CajaTexto"><asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="CajaTexto" placeholder="Contraseña" AutoCompleteType="Disabled" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="entrar" ClientIDMode="Static" runat="server" Text="Entrar" OnClick="entrar_Click" OnClientClick="PlaySound(); return true" /></td>
                    <td></td>
                </tr>
            </table>

            <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        </div>

    </form>

</asp:Content>