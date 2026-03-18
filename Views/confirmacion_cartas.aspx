<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="configurar_prestamo.aspx.cs" Inherits="SIE_KEY_USER.Views.configurar_prestamo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="confirmacion_cartas" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="titulo_agre" ClientIDMode="Static" runat="server" Text="Mensaje de confirmación"></asp:Label>

        <div id="texto_agre">
            Tu carta ha sido solicitada, favor de pasar a recogerla al departamento de recursos humanos centrales
            los días miércoles o viernes de 2:00 a 4:00 p.m.
        </div>

        <asp:Button ID="confir_aceptar" ClientIDMode="Static" runat="server" Text="Continuar" class="btn btn-success" OnClick="confir_aceptar_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>
