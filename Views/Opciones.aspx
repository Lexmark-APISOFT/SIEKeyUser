<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Opciones.aspx.cs" Inherits="SIE_KEY_USER.Views.Opciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <form id="opciones_form" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/opciones_kiosko.png" id="img_deVentanas" />

        <div id="center_opciones">

            <asp:GridView ID="GridView1" runat="server" PageSize="12" AllowPaging="true" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="-1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" >
                <RowStyle CssClass="Row" />
                <AlternatingRowStyle CssClass="AltRow" />
                <Columns>
                    <asp:TemplateField HeaderText="Habilitado">
                        <ItemTemplate>
                            <asp:SqlDataSource ID="opcion" DataSourceMode="DataReader" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="select a.[id_opcion], a.[descripcion] from [SIE].[dbo].[opciones_menu] a" runat="server"></asp:SqlDataSource>
                            <asp:DropDownList ID="seleccionar" runat="server" DataSourceID="opcion" DataTextField="descripcion" DataValueField="id_opcion" ></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <EmptyDataTemplate>
                    No se encontraron registros.
                </EmptyDataTemplate>
            </asp:GridView>

        </div>

        <asp:Button ID="opciones_guardar" ClientIDMode="Static" runat="server" Text="Guardar" OnClick="opciones_guardar_Click" OnClientClick="PlaySound(); return true" />

        <asp:Button ID="Button1" runat="server" Text="Menú" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>