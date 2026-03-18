<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="confirmar_datos.aspx.cs" Inherits="SIE_KEY_USER.Views.confirmar_datos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="confirmar_datos" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <div class="center">

            <asp:Label ID="aviso_confirmar" ClientIDMode="Static" runat="server" Text="Los datos se han generado exitosamente."></asp:Label>

            <br />

            <asp:Button ID="guardar_configurar" ClientIDMode="Static" runat="server" Text="Continuar" OnClick="guardar_configurar_Click" OnClientClick="PlaySound(); return true" />
            <br />
            <asp:Label Text="" runat="server" ID="lblErrMsg" />

        </div>

    </form>

</asp:Content>
