<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Vacaciones.aspx.cs" Inherits="SIE_KEY_USER.Views.Vacaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="vacaciones" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>
        <script>
            $(document).ready(function () {
                $("#<%=TextBox1.ClientID%>").keydown(function (e) {
                    // Allow: backspace, delete, tab, escape, enter and .
                    //if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 || // ret: 110 and 190 keycodes removed for not allowing decimal point or period
                        // Allow: Ctrl+A
                        (e.keyCode == 65 && e.ctrlKey === true) ||
                        // Allow: Ctrl+C
                        (e.keyCode == 67 && e.ctrlKey === true) ||
                        // Allow: Ctrl+X
                        (e.keyCode == 88 && e.ctrlKey === true) ||
                        // Allow: home, end, left, right
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                        // let it happen, don't do anything
                        return;
                    }
                    // Ensure that it is a number and stop the keypress
                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });
            });
        </script>
        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/vacaciones.png" id="img_deVentanas" />

        <div id="center_vaca">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table  id="tb_bemp">
                        <tr>
                            <td class="t_right">Empleado:</td>
                            <td class="t_left">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="CajaTexto" TextMode="Number" placeholder="Número de empleado" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="des_buscar" ClientIDMode="Static" runat="server" Text="Buscar" OnClick="des_buscar_Click" OnClientClick="PlaySound(); return true" />
                            </td>
                            <td id="wdtd" style="width: 87px">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="loading" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loading.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                    <div style="overflow-y:auto; width:900px; height:300px;" >
            <asp:GridView ID="GridView1" runat="server"   CssClass="grid"  SelectedIndex="-1"  OnRowDataBound="GridView1_RowDataBound" >
                <RowStyle CssClass="Row" />
                <AlternatingRowStyle CssClass="AltRow" />
                <Columns>
                    <asp:TemplateField HeaderText="Seleccionar">
                        <ItemTemplate>
                            <asp:CheckBox ID="seleccionar" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <EmptyDataTemplate>
                    No se encontraron registros.
                </EmptyDataTemplate>
            </asp:GridView>
</div>
                    
            <asp:Button ID="ok_modal" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Capturada en TRESS" CssClass="ok_actdat"  OnClientClick= "return false"   />

                    
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
                            <asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="vaca_generar_Click"  OnClientClick="PlaySound();" />
                            <asp:Button ID="noaceptar_ac" ClientIDMode="Static" runat="server" Text="No" OnClientClick="PlaySound();" data-dismiss="modal" />
                            <%--<asp:Button ID="Button3" runat="server" Text="Cerrar" CssClass="btn btn-default" data-dismiss="modal" />--%>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:Button ID="Button1" runat="server" Text="Menú" CssClass="boton" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />

    </form>
     <script type="text/javascript">
        
         $(document).ready(function () {
            /* $("#aceptar_ac.ClientID%>,#btnTestDownloadZip").click(function () {
                 var zipFileName = $("#<=hidden_lastZipFileName.ClientID%>").val();
                var zipFilePath = $("#<=hidden_lastZipFilePath.ClientID%>").val();
                //alert(zipFilePath);
                window.open('confirmar_datos.aspx?ZipFileName=' + zipFileName + '&ZipFilePath=' + zipFilePath);
             });*/

             
         });

         function download() {
             var zipFileName = $("#<%=hidden_lastZipFileName.ClientID%>").val();
             var zipFilePath = $("#<%=hidden_lastZipFilePath.ClientID%>").val();
             //alert(zipFilePath);
             window.open('confirmar_datos.aspx?ZipFileName=' + zipFileName + '&ZipFilePath=' + zipFilePath);

             window.open('Vacaciones.aspx','_self');
         }
    </script>
</asp:Content>
