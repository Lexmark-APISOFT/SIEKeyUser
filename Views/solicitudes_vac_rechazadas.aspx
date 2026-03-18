<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="solicitudes_vac_rechazadas.aspx.cs" Inherits="SIE_KEY_USER.Views.solicitudes_vac_rechazadas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <script type="text/javascript">
        $(document).ready( function () {
             $('gv_CheckList').DataTable();
        });

     

          $(document).ready(function () {
              $("#<%=gv_CheckList.ClientID%>").DataTable({
                    "bFilter": false,
                  
                    "lengthMenu": [[5,10,25,50,100], [5,10,25,50,100]],
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
       </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
             <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:30px"/>
    <form runat="server" >
   
          <h3>Solicitudes Rechazadas</h3>
        <div>
             <h5>Búsqueda de Empleado
                 <asp:TextBox id="txtSearch" runat="server" type="text" /><asp:Button ID="Button1" runat="server" onclick="Button1_Click" BackColor="#33CC33" ForeColor="White" Height="34px" Text="Buscar" Width="74px" />
             </h5>
        </div>
        <asp:GridView ID="gv_CheckList" runat="server"  AutoGenerateColumns="False" style="margin-left:auto; margin-right:auto;  width:1300px; height:100px;" class="table table-bordered table-hover table-responsive grid " OnPreRender="gv_CheckList_PreRender">
            <Columns>

                 <asp:TemplateField HeaderText="Seleccionar">
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

        <br />
        <div>
          
             <asp:Button ID="btnSolicitudesPen" onclick="enviarSolicitudesPen_Click" runat="server" BackColor="Green" ForeColor="White" Height="34px" Text="Enviar a Pendientes" Width="174px" />
             <asp:Button ID="btnHideSelectedRows" onclick="HideSelectedRows_Click" runat="server" BackColor="Green" ForeColor="White" Height="34px" Text="Ocultar registros" Width="174px" />

        </div>
            <asp:Button ID="btnRegresar" Text="Volver" runat="server"  OnClick="btnRegresar_Click" style="background-color:red; font-size:14px; width:110px; height:34px; color:white; position:absolute; right:25px; "/>
        </form>
</asp:Content>
