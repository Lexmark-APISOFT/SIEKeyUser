<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="prestamos.aspx.cs" Inherits="SIE_KEY_USER.Views.prestamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="prestamos_form" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/prestamos.png" id="img_deVentanas" />

        <div id="center_prestamos">
            <asp:Label ID="titulo_prestamos" ClientIDMode="Static" runat="server" Text="Solicitudes pendientes"></asp:Label>
                <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" PageSize="25" AllowPaging="false" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="-1" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <RowStyle CssClass="Row" />
                    <AlternatingRowStyle CssClass="AltRow" />
                    <EmptyDataTemplate>
                        No se encontraron registros.
                    </EmptyDataTemplate>
                </asp:GridView>
            <asp:GridView ID="gridCSV" runat="server" ClientIDMode="Static" PageSize="25" AllowPaging="false" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="-1" OnPageIndexChanging="GridView1_PageIndexChanging" Visible="False">
                    <RowStyle CssClass="Row" />
                    <AlternatingRowStyle CssClass="AltRow" />
                    <EmptyDataTemplate>
                        No se encontraron registros.
                    </EmptyDataTemplate>
                </asp:GridView>
            <asp:Button ID="ok_modal" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Generar archivo" CssClass="ok_actdat" OnClientClick="return false" />
            <div class="modal fade" id="myModal" role="dialog">

                <div class="modal-dialog">
    
                    <!-- Modal content-->
                    <div class="modal-content">
                        
                        <div class="modal-header">
                            <asp:Button ID="Button2" runat="server" Text="&times;" CssClass="close" data-dismiss="modal" />
                            <h4 class="modal-title">Confirmar</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="Label1" runat="server" Text="¿Seguro que deseas generar archivo?"></asp:Label>
                            <asp:HiddenField ID="hidden_lastZipFileName" runat="server" Value="0" />
                            <asp:HiddenField ID="hidden_lastZipFilePath" runat="server" Value="0" />
                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="aceptar_ac_Click" OnClientClick="PlaySound(); return true; window.open('google.com');" />--%>
                            <%--<asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="aceptar_ac_Click" OnClientClick="PlaySound();window.open('confirmar_datos.aspx');" />--%>
                            <asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="aceptar_ac_Click" OnClientClick="PlaySound();" />
                            <asp:Button ID="noaceptar_ac" ClientIDMode="Static" runat="server" Text="No" OnClientClick="PlaySound();" data-dismiss="modal" />
                            
                            <%--<asp:Button ID="Button3" runat="server" Text="Cerrar" CssClass="btn btn-default" data-dismiss="modal" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button ID="boton_prestamos" ClientIDMode="Static" runat="server" Text="Configurar periodo" OnClick="boton_prestamos_Click" OnClientClick="PlaySound(); return true" />
        <br/>
        <asp:Button ID="boton_prestamos1" ClientIDMode="Static" runat="server" Text="Ver solicitudes generadas" OnClick="boton_prestamos1_Click" OnClientClick="PlaySound(); return true" />
        <asp:Button ID="Button1" CssClass="boton" runat="server" Text="Menú" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />
        <asp:Label Text="" runat="server" ID="lblErrMsg" />
        <input type="button" id="btnTestDownloadZip" name="tstDownloadFile" value="testing downloading zip file" hidden="hidden" />
    </form>
</asp:Content>
