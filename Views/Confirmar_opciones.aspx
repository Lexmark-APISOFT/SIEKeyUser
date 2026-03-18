<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Confirmar_opciones.aspx.cs" Inherits="SIE_KEY_USER.Views.Confirmar_opciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="confirmaropcion_form" runat="server" >

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <div id="center">

            <asp:Label ID="aviso_confirmar" ClientIDMode="Static" runat="server" Text="Tus cambios han sido guardados exitosamente."></asp:Label>
            <br />
            <asp:Button ID="continuar_opciones" ClientIDMode="Static" runat="server" Text="Continuar" OnClick="continuar_opciones_Click" OnClientClick="PlaySound(); return true" />
            
        </div>

    </form>

</asp:Content>
