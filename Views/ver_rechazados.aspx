<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="ver_rechazados.aspx.cs" Inherits="SIE_KEY_USER.Views.ver_rechazados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <form id="ver_rechazados_form" runat="server">

        <%------------------------------------- ScriptManager para correr update panel --------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>
        <%------------------------------------- FIN --------------------------------%>
        <script>
            function CheckAll(oCheckBox) {
                var GridView1 = document.getElementById("<%=GridView1.ClientID %>");

                for (i = 1; i < GridView1.rows.length; i++) {
                    GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckBox.checked;
                }
            }
           
        </script>
        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/agregar_familiares.png" id="img_deVentanas" />

        <div id="center_rechazados">

            <asp:Label ID="titulo_fam" ClientIDMode="Static" runat="server" Text="Rechazados"></asp:Label>
            
            <%------------------------------------- UpdatePanel llamada al ajax --------------------------------%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <table id="tb_des_buscar">
                        <tr>
                            <td class="t_right">Empleado:</td>
                            <td class="t_left"><asp:TextBox ID="TextBox1" runat="server" TextMode="Number" placeholder="Número de empleado" CssClass="CajaTexto" ></asp:TextBox></td>
                            <td><asp:Button ID="des_buscar" ClientIDMode="Static" runat="server" Text="Buscar" OnClick="des_buscar_Click" OnClientClick="PlaySound(); return true" /></td>
                            <td id="wdtd">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="loading" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loading.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="nombre_codigo" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                <asp:Label ID="codigos" ClientIDMode="Static" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <asp:GridView ID="GridView1" runat="server" PageSize="5" AllowPaging="True" CssClass="grid" PagerStyle-CssClass="pgt" OnPageIndexChanging="GridView1_PageIndexChanging" >
                        <PagerStyle CssClass="pgt" />
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
                     <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%------------------------------------- FIN --------------------------------%>

    </div>
         <asp:Button ID="btn_del" ClientIDMode="Static" runat="server"  Text="Eliminar" CssClass="ok_actdat"  OnClick="btn_del_Click"   />
        <asp:Button ID="btn_modif" ClientIDMode="Static" runat="server"  Text="Regresar a pendientes" CssClass="ok_actdat" OnClick="btn_modif_Click"  />
    <asp:Button ID="Button1" runat="server" Text="Regresar" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>

</asp:Content>
