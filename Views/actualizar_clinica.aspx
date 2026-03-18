<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="actualizar_clinica.aspx.cs" Inherits="SIE_KEY_USER.Views.actualizar_clinica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="clinica_form" runat="server" >

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/actualizacion.png" id="img_deVentanas" />

        <div id="clinica_center">

            <asp:GridView ID="GridView1" runat="server" PageSize="5" AllowPaging="true" SelectedIndex="-1" CssClass="grid" PagerStyle-CssClass="pgt" OnRowDataBound="GridView1_RowDataBound" >
                <RowStyle CssClass="Row" />
                <AlternatingRowStyle CssClass="AltRow" />
                <EmptyDataTemplate>
                    No se encontraron registros.
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:Button ID="Button2" runat="server" Text="Generar archivo" CssClass="clinica_actualizar" OnClick="Button2_Click" OnClientClick="PlaySound(); return true" />

        </div>

        <asp:Button ID="Button1" runat="server" Text="regresar" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>