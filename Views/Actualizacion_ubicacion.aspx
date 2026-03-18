<%@  Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Actualizacion_ubicacion.aspx.cs" Inherits="SIE_KEY_USER.Views.Actualizacion_ubicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form runat="server">

        <audio id="audio" src="../Content/Cick.wav" ></audio>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/bootstrap/popover_ayuda.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>
        <div class="container">
            <div>
                <img src="../Images/MenuKey/actualizar_ubicacion.png" id="img_deVentanas" />
            </div>
            <div class="col-lg-12" style="height:100px;"></div>
            <div class="col-md-9 col-md-offset-2" >
                <div >
                    <div style="overflow-x:auto;  height:450px; width:100%" >
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="false" CssClass="grid" PagerStyle-CssClass="pgt" SelectedIndex="-1" Width="600px" >
                            <RowStyle CssClass="Row" />
                            <AlternatingRowStyle CssClass="AltRow" />

                            <EmptyDataTemplate>
                                No se encontraron registros.
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <asp:Label Text="" runat="server" ID="lblErrMsg" />
                </div>
                
                 <!-------------------------------------------------------------- Bootstrap JS modal popup --------------------------------------->
            <asp:Button ID="ok_modal" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Generar archivo" CssClass="ok_actdat" OnClientClick="return false" />

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
                            
                            <asp:Label ID="Label1" runat="server" Text="¿Seguro que deseas generar archivo?"></asp:Label>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                            <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
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
            <!------------------------------------------------------------------ FIN ------------------------------------------------------>
            </div>

            <asp:Button ID="btn_MainMenu" runat="server" Text="Menú" CssClass="boton" OnClick="btn_MainMenu_Click" OnClientClick="PlaySound(); return true" />
            <br />
            
            <asp:HiddenField ID="hidden_lastZipFileName" runat="server" Value="0" />
            <asp:HiddenField ID="hidden_lastZipFilePath" runat="server" Value="0" />
        </div>
        <input type="button" id="btnTestDownloadZip" name="tstDownloadFile" value="testing downloading zip file" hidden="hidden" />
    </form>

  <script type="text/javascript">
      $(document).ready(function () {
          $("#<%=aceptar_ac.ClientID%>,#btnTestDownloadZip").click(function () {
                var zipFileName = $("#<%=hidden_lastZipFileName.ClientID%>").val();
                var zipFilePath = $("#<%=hidden_lastZipFilePath.ClientID%>").val();
                //alert(zipFilePath);
                window.open('confirmar_datos.aspx?ZipFileName=' + zipFileName + '&ZipFilePath=' + zipFilePath);
            });
        });
    </script>

</asp:Content>
