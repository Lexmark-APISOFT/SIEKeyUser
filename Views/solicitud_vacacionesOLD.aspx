<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="solicitud_vacacionesOLD.aspx.cs" Inherits="SIE_KEY_USER.Views.solicitud_vacaciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <script type="text/javascript">


         $(document).ready(function () {
            var table =$("#<%=gv_CheckList.ClientID%>").DataTable({
                    "bFilter": false,
                    "bStateSave": true,
                    "lengthMenu": [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
                    "bDeferRender":true,
                    "language": {
                        "sProcessing":     "Procesando...",
	                    "sLengthMenu":     "Mostrar _MENU_ registros",
	                    "sZeroRecords":    "No se encontraron resultados",
	                    "sEmptyTable":     "NingÃºn dato disponible en esta tabla",
	                    "sInfo":           "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
	                    "sInfoEmpty":      "Mostrando registros del 0 al 0 de un total de 0 registros",
	                    "sInfoFiltered":   "(filtrado de un total de _MAX_ registros)",
	                    "sInfoPostFix":    "",
	                    "sSearch":         "Buscar:",
	                    "sUrl":            "",
	                    "sInfoThousands":  ",",
	                    "sLoadingRecords": "Cargando...",
	                    "oPaginate": {
		                    "sFirst":    "Primero",
		                    "sLast":     "Ãšltimo",
		                    "sNext":     "Siguiente",
		                    "sPrevious": "Anterior"
	                },
	                "oAria": {
		                "sSortAscending":  ": Activar para ordenar la columna de manera ascendente",
		                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
	                }
            }
            });
          });

        function SelectAllCheckboxes(headerCheckbox) {
            var checkboxes = document.querySelectorAll("#<%= gv_CheckList.ClientID %> input[type='checkbox']");
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = headerCheckbox.checked;
            }
            $("#<%=gv_CheckList.ClientID%>").ajax.reload(null, false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
    <form runat="server" >
   
          <h3>Solicitudes de Vacaciones</h3>

        <div>
             <h5>Búsqueda de Empleado
                 <asp:TextBox id="txtSearch" runat="server" type="text" /><asp:Button ID="Button1" runat="server" onclick="Button1_Click" BackColor="#33CC33" ForeColor="White" Height="34px" Text="Buscar" Width="74px" />
             </h5>
        </div>
 <asp:GridView ID="gv_CheckList" runat="server"  AutoGenerateColumns="False"  style="margin-left:auto; margin-right:auto;  width:1300px; height:100px;"  class="table table-bordered table-hover table-responsive grid" OnPreRender="gv_CheckList_PreRender" OnPageIndexChanging="gv_CheckList_PageIndexChanging">
            <Columns>

                 <asp:TemplateField HeaderText="Seleccionar">
                        <HeaderTemplate>
                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAllCheckboxes(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkb_status" runat="server" />
                </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField Visible="False"> 
                    <ItemTemplate >
                        <asp:Label ID="hfIdSolVac" runat="server" DataField="id_sol_vac" Text='<%# Eval("id_sol_vac") %>' />
                    </ItemTemplate>
                     </asp:TemplateField>
                   <asp:BoundField HeaderText="# Reloj" DataField="cb_codigo" SortExpression="cb_codigo" />
                    <asp:BoundField HeaderText="Inicio" DataField="fech_ini" SortExpression="fech_ini" />
                    <asp:BoundField DataField="fech_fin" HeaderText="Fin" SortExpression="fech_fin" />
                    <asp:BoundField DataField="duracion" HeaderText="Duración" SortExpression="duracion" />
                    <asp:BoundField DataField="pe_week_number" HeaderText="Periodo de Nómina" SortExpression="pe_week_number" />
                    <asp:BoundField DataField="pe_periodo_fecha" HeaderText="Fecha inicial-final" SortExpression="pe_periodo_fecha" />
                    <asp:BoundField DataField="pe_fecha_pago" HeaderText="Fecha de pago" SortExpression="pe_fecha_pago" />
            </Columns>
                <EmptyDataTemplate>
                            No hay solicitudes pendientes.
                        </EmptyDataTemplate>
        </asp:GridView>
        <%-- <asp:GridView ID="gvSeleccionPago" runat="server" AutoGenerateColumns="False"  class="table table-bordered table-hover table-responsive grid">
            <Columns>
                    <asp:TemplateField HeaderText ="Seleccionar">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectSolicitud" runat="server"  />
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="# Reloj" DataField="cb_codigo" SortExpression="cb_codigo" />
                    <asp:BoundField HeaderText="Inicio" DataField="fech_ini" SortExpression="fech_ini" />
                    <asp:BoundField DataField="fech_fin" HeaderText="Fin" SortExpression="fech_fin" />
                    <asp:BoundField DataField="duracion" HeaderText="Duracion" SortExpression="duracion" />
                    <asp:BoundField DataField="pe_week_number" HeaderText="Periodo de Nomina" SortExpression="pe_week_number" />
                    <asp:BoundField DataField="pe_periodo_fecha" HeaderText="Fecha inicial-final" SortExpression="pe_periodo_fecha" />
                    <asp:BoundField DataField="pe_fecha_pago" HeaderText="Fecha de pago" SortExpression="pe_fecha_pago" />

            </Columns>
        </asp:GridView>--%>
        <div>
            <asp:Button ID="btnAceptarSol" onclick="Aceptar_Click" runat="server" BackColor="#33CC33" ForeColor="White" Height="34px" Text="Aceptar Solicitudes" Width="174px" />
             <asp:Button ID="btnRechazarSol" onclick="Rechazar_Click" runat="server" BackColor="Red" ForeColor="White" Height="34px" Text="Rechazar Solicitudes" Width="174px" />
        <div>
            <br />
    <asp:Button ID="btnVerAceptadas" runat="server" OnClick="verSolicitudesAceptadas_Click" style="background-color: rgb(68, 68, 68);" ForeColor="White" Height="34px" Text="Ver Solicitudes Aceptadas" Width="200px" />
            <br />
            <br />
        <asp:Button ID="btnVerRechazadas" runat="server" OnClick="verSolicitudesRechazadas_Click" style="background-color: rgb(68, 68, 68);" ForeColor="White" Height="34px" Text="Ver Solicitudes Rechazadas" Width="200px" />
            <br />
            <br />
            <asp:Button ID="btnDisponibilidad" runat="server" OnClick="modificarDisp_Click" style="background-color: rgb(68, 68, 68);" ForeColor="White" Height="34px" Text="Modificar Cupos" Width="200px" />
            <br />
            <br />
              <asp:Button ID="Button2" runat="server" OnClick="modificarDias_Click" style="background-color: rgb(68, 68, 68);" ForeColor="White" Height="34px" Text="Modificar dias" Width="200px" />
        </div>
        </div>
    <asp:Button ID="btnRegresar" Text="Volver" runat="server"  OnClick="btnRegresar_Click" style="background-color:red; font-size:14px; width:110px; height:34px; color:white; position:absolute; right:25px; "/>
        <br />

        </form>


</asp:Content>
