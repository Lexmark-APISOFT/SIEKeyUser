<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/_Layout.Master" CodeBehind="listaReprogramaciones.aspx.cs" Inherits="SIE_KEY_USER.Views.listaReprogramaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_JS" runat="server">

    <link href="../Content/DataTables/css/jquery.dataTables.css" rel="stylesheet" />
    <script src="../Scripts/DataTables/jquery.dataTables.js"></script>

    <script type="text/javascript" src="/bower_components/moment/min/moment.min.js"></script>
    <script type="text/javascript" src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/bower_components/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js"></script>
    <link rel="stylesheet" href="/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/bower_components/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css" />

    <script type="text/javascript">
        
        $(document).ready(function () {
            var table = $("#<%=gv_Lista_de_asistencia_perfecta.ClientID%>").DataTable({
                "stateSave": true,
                "lengthMenu": [[8,16,-1], [8,16,"All"]],
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
	                }
                }
            });

            //alert("inicio " + $('#<%=isCalInicioOn.ClientID%>').val() + ", fin-" + $('#<%=isCalFinOn.ClientID%>').val());
            
            if ($('#<%=isCalInicioOn.ClientID%>').val() == "1") {
                $("#contenedorCalendario").css("display", "block");
                $("#divInicioCiclo").css("display", "block");

                $("#divFinCiclo").css("display", "none");
            }

            if ($('#<%=isCalFinOn.ClientID%>').val() == "1") {
                $("#contenedorCalendario").css("display", "block");
                $("#divFinCiclo").css("display", "block");

                $("#divInicioCiclo").css("display", "none");
            }

            $("a[title='Go to the previous month']").click(function () {
                var parent = $(this).parents("div[id='divInicioCiclo']");
                if (parent[0]) {
                    $('#<%=isCalInicioOn.ClientID%>').val("1");
                    //alert(parent[0].id + " anterior");
                }
                
                parent = $(this).parents("div[id='divFinCiclo']");
                if (parent[0]) {
                    $('#<%=isCalFinOn.ClientID%>').val("1");
                    //alert(parent[0].id + " anterior");
                }
                
            });

            $("a[title='Go to the next month']").click(function () {

                var parent = $(this).parents("div[id='divInicioCiclo']");
                if (parent[0]) {
                    $('#<%=isCalInicioOn.ClientID%>').val("1");
                    //alert(parent[0].id + " siguiente");
                }
                
                parent = $(this).parents("div[id='divFinCiclo']");
                if (parent[0]) {
                    $('#<%=isCalFinOn.ClientID%>').val("1");
                    //alert(parent[0].id + " siguiente");
                }
                
            });

            $("#head_btnOcultarCalendarios").click(function (e) {
                //alert("entre");
                e.preventDefault();

                $('#<%=isCalInicioOn.ClientID%>').val("0");
                $("#divInicioCiclo").css("display", "none");

                $('#<%=isCalFinOn.ClientID%>').val("0");
                $("#divFinCiclo").css("display", "none");

                $("#contenedorCalendario").css("display", "none");
            });

            $("#head_txtInicioCiclo").focus(function (e) {

                if ($('#<%=isCalFinOn.ClientID%>').val() == 1) {
                    //alert("inicio - "+ $('#<%=isCalInicioOn.ClientID%>').val()+" / "+$('#<%=isCalFinOn.ClientID%>').val());
                    $('#<%=isCalFinOn.ClientID%>').val("0");
                    $("#divFinCiclo").css("display", "none");
                }
                
                $("#contenedorCalendario").css("display", "block");
                $("#divInicioCiclo").css("display", "block");
                $('#<%=isCalInicioOn.ClientID%>').val("1");
            });

            $("#head_txtFinCiclo").focus(function (e) {
                
                if ($('#<%=isCalInicioOn.ClientID%>').val() == 1) {
                    //alert("fin - "+ $('#<%=isCalInicioOn.ClientID%>').val()+" / "+$('#<%=isCalFinOn.ClientID%>').val());
                    $('#<%=isCalInicioOn.ClientID%>').val("0");
                    $("#divInicioCiclo").css("display", "none");
                }

                $("#contenedorCalendario").css("display", "block");
                $("#divFinCiclo").css("display", "block");
                $('#<%=isCalFinOn.ClientID%>').val("1");
            });
            
        });

   </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    

    <form runat="server">

        <asp:Label ID="lblNombreEmpleado"  Text="" runat="server" style="position:absolute;right:30px" />

        <asp:Label runat="server" Font-Size="X-Large" Text="Lista de asistencia perfecta"></asp:Label>
        <br /> <br />

        <asp:Label ID="lblInicioCiclo" runat="server" Font-Size="Small">Fecha inicio:  </asp:Label>
        <asp:TextBox ID="txtInicioCiclo" runat="server" ></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblFinCiclo" runat="server" Font-Size="Small">Fecha fin:</asp:Label>
        <asp:TextBox ID="txtFinCiclo" runat="server" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAplicarCiclo" runat="server" Text="Guardar Periodo" OnClick="btnAplicarCiclo_Click"/>&nbsp;&nbsp;
        <asp:Button ID="btnOcultarCalendarios" runat="server" Text="Ocultar Calendarios"/>

        <asp:HiddenField ID="isCalInicioOn" runat="server" Value="0"/>
        <asp:HiddenField ID="isCalFinOn" runat="server" Value="0"/>

        <div id="contenedorCalendario"style="width:600px; height:170px; margin-left: auto; margin-right: auto; display:none">
            <div id="divInicioCiclo" style="width:200px; margin-left:-40px; display:none">
                <asp:Calendar ID="calFechaInicio" runat="server" OnSelectionChanged="calFechaInicio_SelectionChanged"></asp:Calendar>
            </div>
            <div id="divFinCiclo" style="width:200px; margin-left:230px; display:none">
                <asp:Calendar ID="calFechaFin" runat="server" OnSelectionChanged="calFechaFin_SelectionChanged"></asp:Calendar>
            </div>
        </div>
        <br/><br/>
        
        <div style="width:1300px; margin-left: auto; margin-right: auto; display: block;">
        <asp:GridView ID="gv_Lista_de_asistencia_perfecta" runat="server" AutoGenerateColumns="false" style="width:1300px; height:100px;" class="table table-bordered table-hover table-responsive grid" OnPreRender="gv_lista_reprogramaciones_PreRender">
            <Columns>
               <asp:BoundField DataField="cb_codigo" HeaderText="Numero de Reloj" SortExpression="cb_codigo" />
                <asp:BoundField DataField="cb_nombre" HeaderText="Tipo" SortExpression="cb_nombre" />
                <asp:BoundField DataField="supervisor" HeaderText="Año" SortExpression="supervisor" />
                <asp:BoundField DataField="planta" HeaderText="Derecho Gozo" SortExpression="planta" />
            </Columns>

            <EmptyDataTemplate>
                        No existen registros que mostrars.
            </EmptyDataTemplate>
        </asp:GridView>
        </div>
        <br /> 

        <asp:Label ID="lblErrMsg" runat="server" Font-Size="Large" ForeColor="Red" ></asp:Label><br /> <br />

        <asp:Button ID="btnExportar" Text="Exportar" runat="server" OnClick="btnExportar_Click" CssClass="btn btn-success btn-lg"/><br /> <br />
        <asp:Button ID="btnRegresar" Text="Regresar" runat="server" OnClick="btnRegresar_Click" CssClass="btn btn-warning btn-lg" />
    </form>

</asp:Content>