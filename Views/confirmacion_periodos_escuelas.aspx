<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="confirmacion_periodos_escuelas.aspx.cs" Inherits="SIE_KEY_USER.Views.confirmacion_periodos_escuelas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="confirmar_datos" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <div class="center">

            <asp:Label ID="aviso_confirmar" ClientIDMode="Static" runat="server" Text=""></asp:Label>

            <br />

            <asp:Button ID="SiPeriodos" ClientIDMode="Static" runat="server" Text="Si" OnClick="guardar_configurar_Click" OnClientClick="PlaySound(); return true" />
            <asp:Button ID="NoPeriodos" ClientIDMode="Static" runat="server" Text="No"  OnClientClick="PlaySound(); return true" OnClick="NoPeriodos_Click" />
            <br />
            <asp:Label Text="" runat="server" ID="lblErrMsg" />

        </div>

    </form>

</asp:Content>