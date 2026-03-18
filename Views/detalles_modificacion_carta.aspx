<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="detalles_modificacion_carta.aspx.cs" Inherits="SIE_KEY_USER.Views.detalles_modificacion_carta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <form id="form_detalles_modificacion_carta" runat="server">
        <div class="row">
             <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <%--!---------------------------- Bootstrap JS modal popup --------------------------------------%>
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
                <%--!---------------------------- FIN --------------------------------------%>

                <%--!---------------------------- Bootstrap JS modal popup --------------------------------------%>
                <asp:ScriptReference Path="~/Scripts/bootstrap/Motrar_ValorFileUpload.js" />
                <%--!---------------------------- FIN --------------------------------------%>

                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>
            <script>
                function uploadClick() {
                    var fname = $('#head_FileUpload1').val().replace(/C:\\fakepath\\/i, '');
                    //  alert(fname); ************** Mostrar la alerta con el nombre del archivo cargado
                    $('#<%=archivo.ClientID%>').html(fname);
                };
            </script>
        <img src="../Images/MenuKey/modificar_carta.png" id="img_deVentanas" />
            <div class="col-md-12">
                <h2>

                    <asp:Label ID="lbl_modmsg" runat="server" Text="" />
                </h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <%--<asp:GridView runat="server" ID="GV_HistorialCarta" DataSourceID="sqlDS_HistorialCarta"></asp:GridView>--%>
            </div>
        </div>
        
        <div class="form-group">
            <label for="inputDocxFile">Actualizar carta:<span class="">
                <asp:FileUpload ID="FileUpload1" runat="server" onchange="return uploadClick();"  />
                </span>
                <asp:Label ID="archivo" runat="server" CssClass="archivo_btn" Text=""></asp:Label>
            </label>
            &nbsp;<!--<input type="file" id="inputDocxFile" name="inputDocxFile" accept=".docx" />--><p class="help-block">El nuevo archivo de Word a subir reemplazará al existente.</p>
            
        </div>

        <div class="form-group">
            <%--<div class="col-md-12">--%>
                <asp:Button ID="btn_ModificarC" Text="Modificar Carta" runat="server" CssClass="btn btn-default" OnClick="btn_ModificarC_Click" />
                <asp:Button ID="ok_modal" Text="Reemplazar Carta" runat="server"  CssClass="btn btn-default" ClientIDMode="Static" OnClientClick="return false" />
            <%--</div>--%>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:Label Text="" runat="server" ID="lbl_statusMsg" />
            </div>
            <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Regresar" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />
        <div id="center_ac_fam2">

<!-------------------------------------------------------------- Bootstrap JS modal popup --------------------------------------->
            
            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">

                <div class="modal-dialog">
    
                    <!-- Modal content-->
                    <div class="modal-content">
                        
                        <div class="modal-header">
                            <asp:Button ID="Button2" runat="server" Text="&times;" CssClass="close" data-dismiss="modal" />
                            <h4 class="modal-title">Confirmar</h4>
                        </div>
                        <div class="modal-body">
                            
                            <asp:Label ID="Label1" runat="server" Text="¿Está seguro que desea reemplazar la carta?"></asp:Label>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Si" OnClick="aceptar_ac_Click" OnClientClick="PlaySound(); return true" />
                            <asp:Button ID="noaceptar_ac" ClientIDMode="Static" runat="server" Text="No" />
                            <asp:Button ID="Button3" runat="server" Text="Cerrar" CssClass="btn btn-default" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
<!------------------------------------------------------------------ FIN ------------------------------------------------------>
    </form>
    

        
    <script type="text/javascript">
        $(document).ready(function () {
            var myfile = "";

            //$('#<%=ok_modal.ClientID%>').click(function (e) {
                //e.preventDefault();
                //$('#inputDocxFile').trigger('click');
            //});

            $('#inputDocxFile').on('change', function () {
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                if (ext=="docx") {
                    alert("Archivo Word con extensión correcta: " + ext); return true;
                } else {
                    alert("Archivo incorrecto de Word, extensión inválida: " + ext); return false;
                }
            });
        });
    </script>

    
</asp:Content>
