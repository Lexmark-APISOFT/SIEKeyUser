<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Reimpresion.aspx.cs" Inherits="SIE_KEY_USER.Views.Reimpresion"  Async="true" ValidateRequest="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="reimpresion_form" runat="server" autocomplete="off">
        
        <%------------------------------------- ScriptManager para correr update panel --------------------------------%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>

        <script>
            function CheckAll(oCheckBox) {
                var GridView1 = document.getElementById("<%=GridView1.ClientID %>");

                for (i = 1; i < GridView1.rows.length; i++) {
                    GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckBox.checked;
                }
            }
            $(document).ready(function () {
                $(".txtmount").keydown(function (e) {
                    // Allow: backspace, delete, tab, escape, enter and . and ,
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 188]) !== -1 ||
                    //if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 || // ret: 110 and 190 keycodes removed for not allowing decimal point or period
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
               
                   
                //});
            });
        </script>
        <%------------------------------------- FIN --------------------------------%>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/reimpresion_cartas.png" id="img_deVentanas" />

        <div id="center_reimpresion">

            <%------------------------------------- UpdatePanel llamada al ajax --------------------------------%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <table id="tb_reimpresion">
                        <tr>
                            <td class="t_right">Empleado:</td>
                            <td class="t_left">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="CajaTexto" TextMode="Number" placeholder="Número de empleado" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="des_buscar" ClientIDMode="Static" runat="server" Text="Buscar" OnClick="des_buscar_Click" OnClientClick="PlaySound(); return true" />
                            </td>
                            <td id="wdtd">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="loading" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loading.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button3" runat="server" Text="Cartas con salario" OnClick="Button3_Click" />
                    <div style="overflow-y:auto; width:100%; height:450px;" >
                    <asp:GridView ID="GridView1" ClientIDMode="Static" runat="server"  AllowPaging="false" CssClass="grid1" PagerStyle-CssClass="pgt" SelectedIndex="-1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" >
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
                            <asp:TemplateField HeaderText="Salario" ItemStyle-Width="110px">
                                <ItemTemplate>
                                    <asp:Label ID="no_aplica" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="txtmount" AutoCompleteType="Disabled"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            No se encontraron registros.
                        </EmptyDataTemplate>
                    </asp:GridView>
                        </div>
                        <div>
                           <asp:Image ID="printing" Width="5%" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loading.gif"  CssClass="hide" />
                        </div>
                    <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                </ContentTemplate>
            </asp:UpdatePanel>
            <%------------------------------------- FIN --------------------------------%>
             <asp:Button ID="constancia_trabajo" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Constancia de trabajo" CssClass="ok_actdat" OnClick="generar_Click" style="display:none" />
        <asp:Button ID="constancia_visa" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Visa Láser" CssClass="ok_actdat" OnClick="generar_Click" style="display:none"  />
        <asp:Button ID="constancia_migracion" ClientIDMode="Static" runat="server" data-toggle="modal" Text="Migración" CssClass="ok_actdat" OnClick="generar_Click" style="display:none"  />
            <asp:Button ID="btn_del" ClientIDMode="Static" runat="server"  Text="Eliminar" CssClass="ok_actdat"  OnClick="btn_del_Click"   />
            <div class="row">
                <div class="span4">
                    <asp:Button ID="imprimir_cartas" ClientIDMode="Static" runat="server" Text="Imprimir" OnClick="imprimir_cartas_Click" OnClientClick="PlaySound(); return true" />
                </div>
            </div>
            
            <div class="row">
                <div class="panel panel-success" runat="server" id="panel_print_log" visible="false">
                    <div class="panel-heading">Log de impresión</div>
                    <div class="panel-body">
                        <%--<asp:Label Text="" runat="server" ID="lblPrintLog" />--%>
                        <asp:TextBox ID="txtPrintLog" runat="server" TextMode="MultiLine" ReadOnly="true" Visible="false" Width="500px" Height="80px"></asp:TextBox>
                    </div>
                </div>
            </div>
            

        </div>
        
        <!--OnClientClick= "return false"  --> 
                    
            <div class="modal fade" id="myModal" role="dialog">

                <div class="modal-dialog">
    
                    <!-- Modal content-->
                    <div class="modal-content">
                        
                        <div class="modal-header">
                            <asp:Button ID="Button2" runat="server" Text="&times;" CssClass="close" data-dismiss="modal" />
                            <h4 class="modal-title">Confirmar</h4>
                        </div>
                        <div class="modal-body">
                            
                            <asp:Label ID="Label2" runat="server" Text="¿Seguro que deseas generar archivo?"></asp:Label>
                            <asp:HiddenField ID="hidden_lastZipFileName" runat="server" Value="0" />
                            <asp:HiddenField ID="hidden_lastZipFilePath" runat="server" Value="0" />
                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="aceptar_ac_Click" OnClientClick="PlaySound(); return true; window.open('google.com');" />--%>
                            <%--<asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="aceptar_ac_Click" OnClientClick="PlaySound();window.open('confirmar_datos.aspx');" />--%>
                            <asp:Button ID="aceptar_ac" ClientIDMode="Static" runat="server" Text="Sí" OnClick="generar_Click"  OnClientClick="PlaySound();" />
                            <asp:Button ID="noaceptar_ac" ClientIDMode="Static" runat="server" Text="No" OnClientClick="PlaySound();" data-dismiss="modal" />
                            <%--<asp:Button ID="Button3" runat="server" Text="Cerrar" CssClass="btn btn-default" data-dismiss="modal" />--%>
                        </div>
                    </div>
                </div>
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
          
            //var zipFileName = $("#<=hidden_lastZipFileName.ClientID%>").val();
            //var zipFilePath = $("#<=hidden_lastZipFilePath.ClientID%>").val();
            //alert(zipFilePath);
            //window.open('confirmar_datos.aspx?ZipFileName=' + zipFileName + '&ZipFilePath=' + zipFilePath);

            window.open('Reimpresion.aspx', '_self');
        }
    </script>
    <script>
        //METHOD TO SHOW A SPINNER AND BLOCK IMPRIMIR BUTTON
        $(document).ready(function () {
            $('#<%= imprimir_cartas.ClientID %>').click(function () {
                $("#<%= printing.ClientID %>").removeClass("hide").addClass("show-element"); // Hide the button on click
                $(this).addClass("disabled-button");
               // Make an AJAX call to the server to start the task
               $.ajax({
                   url: 'Reimpresion/PrintDocumentsAsync',
                    type: 'POST',
                    contentType: 'application/json',
                    success: function () {
                        // Task started successfully
                        // You can update UI or perform additional actions if needed
                        console.log('success');
                        $("#<%= printing.ClientID %>").removeClass("hide").addClass("show-element"); // Hide the button on click
                    },
                    error: function () {
                        // Error occurred during task start
                        // You can handle the error or show an error message to the user
                        console.log('error');

                   },
                    complete: function () {
                        // Task completed, show the button again
                        console.log('completed');
                        $("#<%= printing.ClientID %>").hide(); // Hide the button on click
                        $('#<%= imprimir_cartas.ClientID %>').removeClass("disabled_button");
                    }
                });
            });
        });

    </script>
</asp:Content>