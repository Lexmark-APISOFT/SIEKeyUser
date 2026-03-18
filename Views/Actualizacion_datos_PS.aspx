<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Actualizacion_datos_PS.aspx.cs" Inherits="SIE_KEY_USER.Views.Actualizacion_datos_PS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

    <form id="actualizacion_form" runat="server">

        <!-------------------------------------------------------- SCRIPT MANAGER TOOLKIT MASK -------------------------------------->
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/bootstrap/popover_ayuda.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>
        <!-------------------------------------------------------- FIN -------------------------------------->

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>
        <div>
            <img src="../Images/MenuKey/actualizacion.png" id="img_deVentanas" />
        </div>
        <div id="center_ac">
            <div style="overflow-x:auto;  height:450px; width:100%" >
                <asp:GridView ID="GridView1" runat="server" AllowPaging="false" CssClass="gridD" PagerStyle-CssClass="pgt" SelectedIndex="-1" 
                    OnRowDataBound="GridView1_RowDataBound" Width="4000px" >
                    <RowStyle CssClass="Row" />
                    <AlternatingRowStyle CssClass="AltRow" />

                    <EmptyDataTemplate>
                        No se encontraron registros.
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <!-------------------------------------------------------------- Bootstrap JS modal popup --------------------------------------->
            <asp:Button ID="ok_modal" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Generar archivo PS" CssClass="ok_actdat" OnClientClick="return false" />

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
            <!------------------------------------------------------------------ FIN ------------------------------------------------------>

            <asp:Button ID="irclinica" ClientIDMode="Static" runat="server" Text="Clinicas" OnClick="irclinica_Click" OnClientClick="PlaySound(); return true" Visible="False" />

            <%--<asp:Button ID="Button4" runat="server" Text="Tets Generate zip file" CssClass="btn-warning" Visible="false" />--%>
            

        </div>

        <asp:Button ID="btn_MainMenu" runat="server" Text="Regresar" CssClass="boton" OnClick="btn_MainMenu_Click" OnClientClick="PlaySound(); return true" />
        <br />
        <asp:Label Text="" runat="server" ID="lblErrMsg" />
        <%--<asp:Button Text="test Jquery" runat="server" OnClientClick="return false;" ID="btnTestJquery" Visible="false" />--%>
        <input type="button" id="btnTestDownloadZip" name="tstDownloadFile" value="testing downloading zip file" hidden="hidden" />
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
