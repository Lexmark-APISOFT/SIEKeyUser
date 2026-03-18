<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true"  CodeBehind="matriz_disponibilidadant.aspx.cs" Inherits="SIE_KEY_USER.Views.matriz_disponibilidadant" %>

     <asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <link href="~/Content/Vacaciones.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var dataTable;

        function CheckAll(oCheckBox) {
            var gv_CheckList = document.getElementById("<%=gv_CheckList.ClientID %>");

              for (i = 1; i < gv_CheckList.rows.length; i++) {
                  gv_CheckList.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckBox.checked;
              }
        }
        $(document).ready(function () {
            $('gv_CheckList').DataTable().e;
            
        });

        $("#<%=buttonResetTable.ClientID%>").click(function () {
            

            

            dataTable.destroy();

            dataTable = $("#<%=gv_CheckList.ClientID%>").DataTable({
                "drawCallback": function () {
                    this.api().state.clear();
                }
            });

            //location.reload();


        });
               
        $(document).ready(function () {
            dataTable = $("#<%=gv_CheckList.ClientID%>").DataTable({
                /*"bFilter": false,
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
                 }*/
                   
                "bFilter": true,
                "lengthMenu": [[5, 10, 25, 50, 100],[5, 10, 25, 50, 100]],
                "stateSave": true,
                "paging": true,
                "searching": true,
                "filter": true,
                "bAutoWidth": false,
                "ordering": true,
                "initComplete": function () {
                    this.api()
                        .columns([1, 2, 3, 4, 5, 6, 7, 8])
                        .every(function () {
                            var column = this;
                            var select = $('<select><option value=""></option></select>')
                                .appendTo($(column.footer()).empty())
                                .on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());

                                    column.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                            column
                                .data()
                                .unique()
                                .sort()
                                .each(function (d, j) {
                                    select.append('<option value="' + d + '">' + d + '</option>');
                                });
                        });
                }

            });
          });

    </script>


        </asp:Content>
        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
            <form runat="server" >
   
                  <h3>Matriz de Disponibilidad</h3>

                <div>
                    <div>
                     <asp:TextBox ID="txtdisponibilidad" runat="server" Type="text" class="cantInput"></asp:TextBox>
                     <%-- <asp:Button ID="btnAceptarSolicitud" OnClick="Aceptar_Clicks" runat="server" BackColor="#33CC33" ForeColor="White" Height="34px" Text="Aplicar Disponibilidad" Width="174px" /> --%>
                     <%-- <asp:Button ID="btnAceptarSolicitud" OnClick="Aceptar_Clicks" runat="server" Text="Aplicar Disponibilidad"  /> --%>

                       
                        <asp:Button ID="Button1" runat="server" Text="Aplicar Disponibilidad" OnClick="Dispo_click" class="boton1" />
                        <asp:LinkButton ID="ButtonUndo" runat="server" OnClick="undoChanges">
                            <img id="img1" src="../Images/undo.png" class="botonUndo" runat="server" />
                        </asp:LinkButton>
                        <asp:Button ID="ButtonAll" runat="server" Text="Aplicar disponibilidad a todos" OnClick="changeAllSolicitudesVac" class="botonAplicar_ATodos" />

                       </div>
                       
                       
                       
                       <%--<asp:Button ID="ButtonUndo" runat="server" Text="undo" OnClick="undoChanges" class="botonUndo" />--%>

                            <%-- <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Dispo_click" /> --%>

                    <br />
                   
                    <asp:Label ID="Info" runat="server" Text=""></asp:Label>
                </div>
                <asp:GridView ID="gv_CheckList" runat="server"  AutoGenerateColumns="False"  style="margin-left:auto; margin-right:auto;  width:1300px; height:100px;"  class="table table-bordered table-hover table-responsive grid" OnPreRender="gv_CheckList_PreRender" ShowFooter="true">
                    <Columns>
                            <asp:TemplateField  HeaderText="Seleccionar">
                                <HeaderTemplate>
                                    <input id="CheckBox1" type="checkbox" runat="server" onclick="CheckAll(this)"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkb_status" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField Visible="false"> 
                            <ItemTemplate >
                                <asp:Label ID="hfIdSolVac" runat="server" DataField="id_disp"  Text='<%# Eval("id_disp") %>' />
                            </ItemTemplate>
                             </asp:TemplateField>
                            <asp:BoundField DataField="cb_turno" HeaderText="Turno"  SortExpression="cb_turno" />
                            <asp:BoundField DataField="pl_descrip" HeaderText="Planta"  SortExpression="pl_descrip" />
                            <asp:BoundField DataField="dpt_descrip" HeaderText="Departamento" SortExpression="dpt_descrip" />
                            <asp:BoundField DataField="ar_descrip" HeaderText="Area" SortExpression="ar_descrip" />
                            <asp:BoundField DataField="clas_descrip" HeaderText="Clasificacion" SortExpression="clas_descrip" />
                            <asp:BoundField DataField="pu_descrip" HeaderText="Puesto" SortExpression="pu_descrip" />
                            <asp:BoundField DataField="ocupantes" HeaderText="Ocupantes" SortExpression="ocupantes" />
                            <asp:BoundField DataField="num_disp" HeaderText="Disponibilidad" SortExpression="num_disp" />
                    </Columns>
                        <EmptyDataTemplate>
                                    No se encontraron datos disponibles en esta tabla.
                        </EmptyDataTemplate>
                </asp:GridView>

                 <div>
                     <div style=" padding:30px;">
                     <asp:Button ID="buttonResetTable" runat="server" Text="Reset"  class="botonResetTable" />
                     </div>
                     <asp:Button ID="btnRegresar" Text="Volver" runat="server"  OnClick="btnRegresar_Click" style="background-color:red; font-size:14px; width:110px; height:34px; color:white;"/>
                </div>

                </form>


        </asp:Content>


            <%--
            table.columns().flatten().each( function ( colIdx ) {
                // Create the select list and search operation
                if (colIdx < totalColumnas-limiteFiltros && colIdx >= inicioFiltros) {
                    var select = $('<select id="Columna' + colIdx + '" class=".multiselect-wrapper" style="color:black;text-align:center;" />')
                        .appendTo(
                            table.column(colIdx).header()
                        )
                        .on('change', function () {
                            table
                                .column(colIdx)
                                .search($(this).val())
                                .draw();
                        });
                    
                    select.append($('<option value="">-Filtro-</option>'));

                    // Get the search data for the first column and add to the select list
                    table
                        .column(colIdx)
                        .cache('search')
                        .sort()
                        .unique()
                        .each(function (d) {
                            //select.append($('<option value="'+d+'"><label><input type="checkbox"><span>'+d+'</span></input></label></option>'));
                            select.append($('<option value="' + d + '">' + d + '</option>'));
                        });
                }
            });

            function reiniciarFiltros() {
                $('#<%=valTurno.ClientID%>').val(null);
                $('#<%=valPlanta.ClientID%>').val(null);
                $('#<%=valDept.ClientID%>').val(null);
                $('#<%=valArea.ClientID%>').val(null);
                $('#<%=valPuesto.ClientID%>').val(null);
                $('#<%=valCbCodigo.ClientID%>').val(null);
                $('#<%=valClasifi.ClientID%>').val(null);

                $("#head_gv_CheckList_chbHSelect").prop('checked', false);
                $("#head_gv_CheckList_chbHSelect").prop('disabled', true);
                
                $("#head_gv_CheckList").children("thead").children("tr").children("th").each(function (indice,elemento) {
                    
                    if (indice >= inicioFiltros && indice < totalColumnas - limiteFiltros) {
                        $(elemento).children("select").val("");
                        $(elemento).children("a").css("display", "none");
                        $(elemento).children("span").css("display", "block");
                    }
                });
               
                table.columns().search("").draw();
            }

            //Funcion para verificar si existe algun checkbox seleccionado 
           /* function isAnyClicked() {
                var isClicked = false;

                table.rows({ filter: 'applied' }).nodes().each(function (elemento, indexRow) {
                    
                    var col = $(elemento).children("td");

                    if ($(col[0]).children("span").children("input[type=checkbox]").is(':checked')) {
                        isClicked = true;
                    }
                });
                
                return isClicked;
            }*/

            //Reiniciar filtros
            $('#<%=btnReiniciarFiltros.ClientID%>').click(function (e) {
                e.preventDefault();
                reiniciarFiltros();
            });

            //Evento de cambio de filtro (select)
            $('th>select').change(function () {
                var valorFiltro = $(this).val();

                //Deshabilitar el espacio de aplicacion de disponibilidad 
                $("#head_tbDisponibilidad").prop("disabled", true);
                $("#head_btnDisponibilidad").prop("disabled", true);
                $('#<%=valCantidadDispo.ClientID%>').val("0");
                
                if (!isAnyClicked()) {
                    //alert("hay marcados");
                    $("#head_lblDisponibilidad").text("Disponibilidad");
                }
                
                //Asignacion del valor del <select> = filtro seleccionado
                switch (this.id) {
                    case "Columna1":
                         $('#<%=valTurno.ClientID%>').val(valorFiltro);
                        break;
                    case "Columna2":
                        $('#<%=valPlanta.ClientID%>').val(valorFiltro);
                        break;
                    case "Columna3":
                        $('#<%=valDept.ClientID%>').val(valorFiltro);
                        break;
                    case "Columna4":
                        $('#<%=valArea.ClientID%>').val(valorFiltro);
                        break;
                    case "Columna5":
                         $('#<%=valPuesto.ClientID%>').val(valorFiltro);
                        break;
                    case "Columna6":
                        $('#<%=valCbCodigo.ClientID%>').val(valorFiltro);
                        break;
                    case "Columna7":
                        $('#<%=valClasifi.ClientID%>').val(valorFiltro);
                        break;
                    default:

                        break;
                }
               
                //alert("turno " + $('#<%=valTurno.ClientID%>').val() + " - planta " + $('#<%=valPlanta.ClientID%>').val()
                //+ " - dept " + $('#<%=valDept.ClientID%>').val() + " - area " + $('#<%=valArea.ClientID%>').val()
                //+ " - puesto " + $('#<%=valPuesto.ClientID%>').val() + " - id " + $('#<%=valCbCodigo.ClientID%>').val()
                //+ " - clasificacion " + $('#<%=valClasifi.ClientID%>').val());
                            
                //Deshabilitacion de todos los elementos <linkbutton> del encabezado y habilitacion de los elementos <label>
                $(this).parents("tr").children("th").each(function (index, elemento) {
                    if (index < totalColumnas-limiteFiltros && index >= inicioFiltros) {
                        $(elemento).children("a").css("display", "none");
                        $(elemento).children("span").css("display", "block");
                    }
                });
                <%--
                //Habilitacion del elemento <linkButton> (encabezado) del filtro seleccionado
                if (valorFiltro != "") {
                    $('select#' + this.id).siblings("span").css("display", "none");
                    $('select#' + this.id).siblings("a").css("display", "block");
                }

                //Habilitacion del texbox y boton de aplicacion de disponibilidad
                //Marcado de los chechboxes
                if ($('#<%=valTurno.ClientID%>').val() != "" || $('#<%=valPlanta.ClientID%>').val() != "" || $('#<%=valDept.ClientID%>').val() != "" || $('#<%=valArea.ClientID%>').val() != ""
                || $('#<%=valPuesto.ClientID%>').val() != "" || $('#<%=valCbCodigo.ClientID%>').val() != "" || $('#<%=valClasifi.ClientID%>').val() != "") {
                    
                    $('#<%=valCantidadDispo.ClientID%>').val("1");
                    $("#head_btnDisponibilidad").prop("disabled", false);
                    $("#head_lblDisponibilidad").text("Disponibilidad Elementos Seleccionados");

                    $("#head_gv_CheckList_chbHSelect").prop('checked', true);
                    $("#head_gv_CheckList_chbHSelect").prop('disabled', false);

                    table.rows({ filter: 'applied' }).nodes().each(function (elemento, indexRow) {
                        var col = $(elemento).children("td");
                        $(col[0]).children("span").children("input[type=checkbox]").prop('checked', true);
                        $(col[0]).children("span").children("input[type=checkbox]").prop('disabled', false);
                    });
                    
                } else {
                    //test
                    $('#<%=valCantidadDispo.ClientID%>').val();
                    $("#head_tbDisponibilidad").prop("disabled", true);
                    $("#head_btnDisponibilidad").prop("disabled", true);
                    $("#head_lblDisponibilidad").text("Disponibilidad");
                    
                    $("#head_gv_CheckList_chbHSelect").prop('checked', false);
                    $("#head_gv_CheckList_chbHSelect").prop('disabled', true);

                    table.rows({ filter: 'applied' }).nodes().each(function (elemento, indexRow) {
                        
                        var col = $(elemento).children("td");
                        $(col[0]).children("span").children("input[type=checkbox]").prop('checked', false);
                        $(col[0]).children("span").children("input[type=checkbox]").prop('disabled', true);
                    });

                }
                
            });

            //Evento onClick de elementos <a>(linkButton) en el encabezado
            $('th>a').click(function (e) {
                
                if ($(this).siblings("select").length > 0) {
                    e.preventDefault();

                    var lenghtTable = $("#head_gv_CheckList_length").val();
                   
                    var sbls = $(this).siblings("select");
                    
                    if (sbls != null) {
                        $("#head_tbDisponibilidad").prop("disabled", false);
                        $("#head_btnDisponibilidad").prop("disabled", false);

                        $(this).css("display", "none");
                        $(this).siblings("span").css("display", "block");
                        
                        var tipoFiltro = $('#<%=tipoFiltro.ClientID%>').val($(this).text());
                        var valFiltro = $('#<%=valFiltro.ClientID%>').val($(sbls[0]).val());
                        //alert("tipoFiltro " + $(tipoFiltro).val() + " valFiltro " + $(valFiltro).val());

                        $("#head_lblDisponibilidad").text("Disponibilidad "+ tipoFiltro.val().toLowerCase() + " - " + valFiltro.val());
                    }
                }
            });

            //Evento para elementos de edicion de valores <a>(LinkButton) en gridview (Columna Habilitar)
            $('td>a').click(function (e) {

                if ($(this).text() == "Habilitar") {

                    var colmns = $(this).parents('tr').children();
                    var filtroIndividual = $(colmns[6]).children("span").text();

                    $('#<%=valCbCodigo.ClientID%>').val(filtroIndividual);
                    
                    $('#<%=valTurno.ClientID%>').val($(colmns[1]).children("span").text());
                    $('#<%=valPlanta.ClientID%>').val($(colmns[2]).children("span").text()); 
                    $('#<%=valDept.ClientID%>').val($(colmns[3]).children("span").text());
                    $('#<%=valArea.ClientID%>').val($(colmns[4]).children("span").text());
                    $('#<%=valPuesto.ClientID%>').val($(colmns[5]).children("span").text());
                    $('#<%=valClasifi.ClientID%>').val($(colmns[7]).children("span").text());
                    $('#<%=valCantidadDispo.ClientID%>').val($(colmns[8]).children("span").text());
                    
                            
                    //alert("turno " + $('#<%=valTurno.ClientID%>').val() + " - planta " + $('#<%=valPlanta.ClientID%>').val()
                      //  + " - dept " + $('#<%=valDept.ClientID%>').val() + " - area " + $('#<%=valArea.ClientID%>').val()
                      //  + " - puesto " + $('#<%=valPuesto.ClientID%>').val() + " - id " + $('#<%=valCbCodigo.ClientID%>').val()
                      //  + " - clasificacion " + $('#<%=valClasifi.ClientID%>').val() + " cantDisp" + $('#<%=valCantidadDispo.ClientID%>').val());
                    
                    //Funcion para actualizar el valor de la cantidad de disponibilidad en la BD
                    var parametros = '{cantidadDisp: "' + $('#<%=valCantidadDispo.ClientID%>').val() +
                        '", valTurno: "' + $('#<%=valTurno.ClientID%>').val() +
                        '", valPlanta:"' + $('#<%=valPlanta.ClientID%>').val() +
                        '", valDept:"' + $('#<%=valDept.ClientID%>').val() +
                        '", valArea:"' + $('#<%=valArea.ClientID%>').val() +
                        '", valPuesto:"' + $('#<%=valPuesto.ClientID%>').val() +
                       // '", valCbCodigo:"' + $('#<%=valCbCodigo.ClientID%>').val() +
                        '", valClasifi:"' + $('#<%=valClasifi.ClientID%>').val() + '"}';

                     $.ajax({
                         type: 'POST',
                         url: '<%= ResolveUrl("matriz_disponibilidad.aspx/EditClick") %>',
                         data: parametros,
                         contentType: "application/json; charset=uft-8",
                         dataType: "json",
                         success: function (response) {
                             //alert("correcto - " + response.d);
                             $('#<%=Info.ClientID%>').val(response.d);
                         },
                         error: function (response) {
                             alert("Error - " + response.responseText);
                         }
                     });

                    reiniciarFiltros();
                }
                
            });

            //Evento checked del elemento <checkbox> del encabezado
            $("#head_gv_CheckList_chbHSelect").click(function (e) {
                
                if ($(this).is(':checked')) {
                    $("input[type=checkbox]").prop('checked', true);
                    table.rows({ filter: 'applied' }).nodes().each(function (elemento, indexRow) {
                        var col = $(elemento).children("td");
                        $(col[0]).children("span").children("input[type=checkbox]").prop('checked', true);
                     });
                    
                } else {
                    
                    $("input[type=checkbox]").prop('checked', false);

                    table.rows({ filter: 'applied' }).nodes().each(function (elemento, indexRow) {
                        var col = $(elemento).children("td");
                        $(col[0]).children("span").children("input[type=checkbox]").prop('checked', false);
                    });
                }

                if (!isAnyClicked()) {
                    $("#head_tbDisponibilidad").prop("disabled", true);
                    $("#head_tbDisponibilidad").text("0");
                    $("#head_tbDisponibilidad").val("0");
                    $('#<%=valCantidadDispo.ClientID%>').val("0");
                    $("#head_btnDisponibilidad").prop("disabled", true);
                    $("#head_lblDisponibilidad").text("Disponibilidad");
                } else {
                    $("#head_tbDisponibilidad").text("1");
                    $("#head_tbDisponibilidad").val("1");
                    $('#<%=valCantidadDispo.ClientID%>').val("1");
                    $("#head_btnDisponibilidad").prop("disabled", false);
                    $("#head_lblDisponibilidad").text("Disponibilidad Elementos Seleccionados");
                }
                
            });

            //Evento checked de los elementos <checkbox> del encabezado
            $('span>input[type=checkbox]').click(function (e) {
                
                if (this.id != "head_gv_CheckList_chbHSelect") {
                    //alert("hola " + this.id);

                    if (isAnyClicked()) {
                        //test
                        $("#head_tbDisponibilidad").text();
                        $("#head_tbDisponibilidad").val();
                        $('#<%=valCantidadDispo.ClientID%>').val();
                        $("#head_btnDisponibilidad").prop("disabled", false);
                        $("#head_lblDisponibilidad").text("Disponibilidad Elementos Seleccionados");
                    } else {
                        //test
                        $("#head_gv_CheckList_chbHSelect").prop('checked', false);
                        $("#head_tbDisponibilidad").prop("disabled", true);
                        $("#head_tbDisponibilidad").text();
                        $("#head_tbDisponibilidad").val();
                        $('#<%=valCantidadDispo.ClientID%>').val();
                        $("#head_btnDisponibilidad").prop("disabled", true);
                        $("#head_lblDisponibilidad").text("Disponibilidad");
                    }
                }
            });

            $("#head_tbDisponibilidad").blur(function (e) {
                 $('#<%=valCantidadDispo.ClientID%>').val($("#head_tbDisponibilidad").val());
            })

            //Validacion de solo enteros en los elementos <textbox>
            $("input[type=text]").keydown(function () {
                //$(this).css("background-color", "yellow");
                if (event.shiftKey) {
                    event.preventDefault();
                }

                if (event.keyCode == 46 || event.keyCode == 8) {
                }
                else {
                    if (event.keyCode < 95) {
                        if (event.keyCode < 48 || event.keyCode > 57) {
                            event.preventDefault();
                        }
                    }
                    else {
                        if (event.keyCode < 96 || event.keyCode > 105) {
                            event.preventDefault();
                        }
                    }
                }
            });


            $("#tbDisponibilidad").on("input",function (e) {
                //alert("change");
                if ($("#tbDisponibilidad").val() == "-1") {
                    //alert("change");
                    reiniciarFiltros();
                    $("#tbDisponibilidad").val("0");
                }
            });

            
        });
        </script>
        --%>
   
    <%--
    <script type="text/javascript">

        bundles.UseCdn = true;
        var jqueryCdnPath = 
        "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.min.js";

        bundles.Add(new ScriptBundle("~/bundles/jquery",
                jqueryCdnPath).Include(
                    "~/Scripts/jquery-{version}.js"));


    </script>
    --%>
    <%-- 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
    <form runat="server" >
        <h3>Matriz de Disponibilidad </h3>
        <br />
        <br />
        <%--<asp:Label ID="lblAlert"  runat="server" style="position:absolute;right:30px; top:140px; font-size:12px"/> %>
        <br />
        

        <asp:Label ID="lblDisponibilidad" runat="server" Text="Disponibilidad" />&nbsp;&nbsp;
        <asp:TextBox id="tbDisponibilidad"  runat="server" Enabled="true"/>&nbsp;&nbsp;
        <asp:Button ID="btnDisponibilidad" runat="server" Text="Aplicar Disponibilidad" class="btn" Enabled="true" CommandArgument="entre" OnClick="btnDisponibilidad_Click"/> <br /><br />
        <asp:Button ID="btnReiniciarFiltros" runat="server" Text="Reiniciar Filtros" class="btn"/>
        <br />
        <br />
        <br/><asp:Label ID="Info" runat="server" Text="" CssClass="alert" />

        <asp:HiddenField ID="tipoFiltro" runat="server" Value="h1" />
        <asp:HiddenField ID="valFiltro" runat="server" Value="h2" />
        <asp:HiddenField ID="valTurno" runat="server" />
        <asp:HiddenField ID="valPlanta" runat="server" />
        <asp:HiddenField ID="valDept" runat="server" />
        <asp:HiddenField ID="valArea" runat="server" />
        <asp:HiddenField ID="valPuesto" runat="server" />
        <!--<asp:HiddenField ID="valCbCodigo" runat="server" />-->
        <asp:HiddenField ID="valClasifi" runat="server" />
        <asp:HiddenField ID="valocupantes" runat="server" />
        <asp:HiddenField ID="valCantidadDispo" runat="server" Value="0" />

        <asp:GridView   ID="gv_CheckList"   runat="server"  OnPreRender="gv_CheckList_PreRender" 
            AutoGenerateColumns="False" style="margin-left:auto; margin-right:auto;  width:1300px; height:100px;  "  class="table table-bordered table-hover table-responsive grid">

            <Columns>
                    <asp:TemplateField Visible="true"> 
                        <ItemTemplate >         
                            <asp:CheckBox ID="chbSelect" runat="server" Enabled="false"/>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="lblHSelect" runat="server" Text="Seleccionar"/> &nbsp;
                            <asp:CheckBox ID="chbHSelect" runat="server" Enabled="false"/>
                        </HeaderTemplate>
                     </asp:TemplateField>
                    <asp:TemplateField Visible="true"> 
                        <ItemTemplate >         
                            <asp:Label ID="lblIdTurno" runat="server" Text='<%# Eval("CB_TURNO")%>'/>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="lblHTurno" runat="server" Text="Turno"/>
                            <asp:linkButton type="button" ID="btnTurno" runat="server" Text="Turno" style="display:none"/>
                        </HeaderTemplate>
                     </asp:TemplateField>
                    
                    <asp:TemplateField Visible="true"> 
                        <ItemTemplate >         
                            <asp:Label ID="lblIdPlanta" runat="server" Text='<%# Eval("pl_descrip")%>'/>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="lblHPlanta" runat="server" Text="Planta"/>
                            <asp:linkButton ID="btnPlanta" runat="server" Text="Planta" style="display:none"/>
                        </HeaderTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField Visible="true"> 
                        <ItemTemplate >         
                            <asp:Label ID="lblIdDept" runat="server" Text='<%# Eval("dpt_descrip")%>'/>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="lblHDept" runat="server" Text="Departamento"/>
                            <asp:LinkButton ID="btnDept" runat="server" Text="Departamento" style="display:none"/>
                        </HeaderTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField Visible="true"> 
                        <ItemTemplate >         
                            <asp:Label ID="lblIdArea" runat="server" Text='<%# Eval("ar_descrip")%>'/>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="lblHArea" runat="server" Text="Area"/>
                            <asp:LinkButton ID="btnArea" runat="server" Text="Area" style="display:none"/>
                        </HeaderTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField Visible="true"> 
                        <ItemTemplate >         
                            <asp:Label ID="lblIdPuesto" runat="server" Text='<%# Eval("PU_DESCRIP")%>'/>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="lblHPuesto" runat="server" Text="Puesto"/>
                            <asp:LinkButton ID="btnPuesto" runat="server" Text="Puesto" style="display:none"/>
                        </HeaderTemplate>
                     </asp:TemplateField>
               
                  
                                     
                    <asp:TemplateField Visible="true"> 
                        <ItemTemplate >         
                            <asp:Label ID="lblClasifi" runat="server" Text='<%# Eval("CB_CLASIFI")%>'/>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="lblHClasifi" runat="server" Text="Clasificacion"/>
                            <asp:LinkButton ID="btnClasifi" runat="server" Text="Clasificacion" style="display:none"/>
                        </HeaderTemplate>
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="Ocupantes cantidad" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox id="textocupantes"  runat="server" Text='<%# Eval("ocupantes") %>' style="display:none"/>
                            <asp:Label ID="lblCantidadocupantes" runat="server" Text='<%# Eval("ocupantes") %>' />
                        </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cantidad de disponibilidad" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox id="textDisponibilidad"  runat="server" Text='<%# Eval("num_disp") %>' style="display:none"/>
                            <asp:Label ID="lblCantidadDisp" runat="server" Text='<%# Eval("num_disp") %>' />
                        </ItemTemplate>
                </asp:TemplateField>
                    
            </Columns>
            
                <EmptyDataTemplate>
                            No se encontraron datos disponibles en esta tabla
                </EmptyDataTemplate>
        </asp:GridView>

        <br />
        <div>
            <asp:Button ID="btnRegresar" Text="Volver" runat="server"  OnClick="btnRegresar_Click" style="background-color:red; font-size:14px; width:110px; height:34px; color:white; position:absolute; margin-right:auto; margin-left: auto;"/>

        </div>
      </form>
</asp:Content>
--%>