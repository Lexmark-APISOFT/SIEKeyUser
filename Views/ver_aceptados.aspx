<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="ver_aceptados.aspx.cs" Inherits="SIE_KEY_USER.Views.ver_aceptados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="ver_aceptados_form" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/agregar_familiares.png" id="img_deVentanas" />

        <div id="center_aceptados">

            <asp:Label ID="titulo_fam" ClientIDMode="Static" runat="server" Text="Aceptados" ></asp:Label>
            <div style="overflow-y:auto; width:900px; height:450px;" >
            <asp:GridView ID="GridView1" ClientIDMode="Static" runat="server"  AllowPaging="false" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="-1" OnPageIndexChanging="GridView1_PageIndexChanging" >
                <RowStyle CssClass="Row" />
                <AlternatingRowStyle CssClass="AltRow" />
                <EmptyDataTemplate>
                    No se encontraron registros.
                </EmptyDataTemplate>
            </asp:GridView>
</div>
            <asp:Button ID="ok_modal" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Generar archivo" CssClass="ok_actdat" OnClientClick="return false"  />
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
                            <asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="aceptar_ac_Click" OnClientClick="PlaySound();" />
                            <asp:Button ID="noaceptar_ac" ClientIDMode="Static" runat="server" Text="No" OnClientClick="PlaySound();" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <asp:Label Text="" runat="server" ID="lblErrMsg" />
        <asp:Button ID="Button1" runat="server" Text="Regresar" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>
   <%-- <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=aceptar_ac.ClientID%>,#btnTestDownloadZip").click(function () {
                var zipFileName = $("#<%=hidden_lastZipFileName.ClientID%>").val();
                var zipFilePath = $("#<%=hidden_lastZipFilePath.ClientID%>").val();
                //alert(zipFilePath);
                window.open('confirmar_datos.aspx?ZipFileName=' + zipFileName + '&ZipFilePath=' + zipFilePath);
            });
        });
    </script>--%>
</asp:Content>