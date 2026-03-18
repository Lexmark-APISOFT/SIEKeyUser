<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master"  AutoEventWireup="true" CodeBehind="disponibilidad_vacaciones.aspx.cs" Inherits="SIE_KEY_USER.Views.disponibilidad_vacaciones" UICulture="es" Culture="es-Es" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <script type="text/javascript">
        $(document).ready( function () {
             $('gv_CheckList').DataTable();
            } );

        $(document).ready(function () {
            $("#<%=gv_CheckList.ClientID%>").DataTable({
                    "stateSave": true,
                    "lengthMenu": [[8], [8]],
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

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >

    <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
    
        <form runat="server" >
        <h3>Modificar Disponibilidad</h3>
        <div  >
            <asp:Label Text="Seleccionar tipo: " runat="server" style="position:absolute; right:217px; top:125px; font-weight: bold;"/>
            <asp:DropDownList AutoPostBack="true"  ID="ddlTipoDisp"  onselectedindexchanged="ddlTipoDisp_SelectedIndexChanged"  style="width:205px; height:23px; position:absolute; right:0px; top:125px; border:ridge;  border-color:limegreen;"   runat="server" ></asp:DropDownList>
            <br/><br/>
        </div>
            <br />

        <asp:SqlDataSource ID="sdsFilterType" DataSourceMode="DataReader" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="select f.[desc_Filter] from [SIE].[dbo].[filtros_vac] f" runat="server"></asp:SqlDataSource>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblFilterType" runat="server" Text="Tipo de filtro"></asp:Label>&nbsp;&nbsp;
        <asp:DropDownList ID="ddFilterType" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblFilter" runat="server" Text="Filtro"></asp:Label>&nbsp;&nbsp;
        <asp:DropDownList ID="ddFilter" runat="server" Width="81px">
        </asp:DropDownList>
            <br />

        <asp:GridView ID="gv_CheckList"  runat="server" AutoGenerateColumns="False" OnRowUpdating="gv_CheckList_RowUpdating" OnPreRender="gv_CheckList_PreRender" OnRowCancelingEdit="gv_CheckList_RowCancelingEdit" OnRowEditing="gv_CheckList_RowEditing" style="margin-left:auto; margin-right:auto;  width:1300px; height:100px;" class="table table-bordered table-hover table-responsive grid">
            <Columns>
                <asp:TemplateField Visible="true" HeaderText="ID"> 
                    <ItemTemplate >
                        <asp:Label ID="lblIdDisp" runat="server" Text='<%# Eval("id_disp") %>' />
                    </ItemTemplate>
                    <ItemTemplate >
                        <asp:Label ID="lblIdDisp" runat="server" Text='<%# Eval("id_disp") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ReadOnly="true" HeaderText="Turno" DataField="cb_turno" SortExpression="cb_turno" />
                <asp:BoundField ReadOnly="true" HeaderText="Planta" DataField="planta_descript" SortExpression="planta_descript" />
                <asp:BoundField ReadOnly="true" DataField="dep_descript" HeaderText="Departamento" SortExpression="dep_descript" />
                <asp:BoundField ReadOnly="true" DataField="area_descript" HeaderText="Área" SortExpression="area_descript" />
                <asp:BoundField ReadOnly="true" DataField="pu_descrip" HeaderText="Puesto" SortExpression="pu_descrip" />

                <asp:TemplateField HeaderText="Tipo de Disponibilidad" >
                    <ItemTemplate>
                        <asp:DropDownList  AutoPostBack="false" ID="ddlSeleccionTipo" runat="server"  DataTextField="desc_tipo" DataValueField="desc_tipo" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" /> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cantidad de disponibilidad " >
                    <EditItemTemplate>
                        <asp:TextBox id="tbDisponibilidad"  runat="server"  Text='<%# Eval("num_disp") %>' /> 
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCantDisp" runat="server" Text='<%# Eval("num_disp") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:Commandfield showeditbutton="true" EditText="Editar" UpdateText="Aceptar" CancelText="Cancelar" headertext="Actualizar"/>
            </Columns>
                
                <EmptyDataTemplate>
                            No se encontraron datos disponibles en esta tabla
                </EmptyDataTemplate>
        </asp:GridView>
                <br />
        <div>
            <asp:Button ID="btnRegresar" Text="Volver" runat="server"  OnClick="btnRegresar_Click" style="background-color:red; font-size:14px; width:110px; height:34px; color:white; position:absolute; right:25px; "/>
        </div>
        </form>
</asp:Content>
