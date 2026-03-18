<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/_Layout.Master" CodeBehind="matriz_disponibilidad2.aspx.cs" Inherits="SIE_KEY_USER.Views.matriz_disponibilidad2" %>

<asp:Content ID="content1" ContentPlaceHolderID="cph_JS" runat="server">
    <script type="text/javascript">
        //Valor maximo de las columnas del griview empezando de 0
        var totalColumnas = 9;
        var limiteFiltros = 1; // Especifica el limite hasta donde se mostraran los dropdown lists para la aplicacion de filtros
        var inicioFiltros = 1; // Especifca desde que columna se comenzaran a mostrar los dropdown lists para la aplicacion de filtros
        
        
        $(document).ready(function () {
            
            //var table = $("#<%=gv_CheckList.ClientID%>").DataTable({
            /*    ordering: false,
                select: true,
                colReorder: true,
                "stateSave": true,
                "lengthMenu": [[8,10,16,24,-1], [8,10,16,24,"All"]],
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
            });*/

            $('td>a').click(function (e) {
            if ($(this).text() == "Habilitar") {
                //alert("entre");

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
                    '", valCbCodigo:"' + $('#<%=valCbCodigo.ClientID%>').val() +
                    '", valClasifi:"' + $('#<%=valClasifi.ClientID%>').val() + '"}';

                //alert("Entre");
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
        });

    </script>

</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
    <form runat="server" >
        <h3>Matriz de Disponibilidad </h3>
        <br />
        <br />
        <%--<asp:Label ID="lblAlert"  runat="server" style="position:absolute;right:30px; top:140px; font-size:12px"/>--%>
        <br />
        

        <asp:Label ID="lblDisponibilidad" runat="server" Text="Disponibilidad" />&nbsp;&nbsp;
        <asp:TextBox id="tbDisponibilidad"  runat="server" Enabled="false"/>&nbsp;&nbsp;
        <asp:Button ID="btnDisponibilidad" runat="server" Text="Aplicar Disponibilidad" class="btn" Enabled="false" CommandArgument="entre" OnClick="btnDisponibilidad_Click"/> <br /><br />
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
        <asp:HiddenField ID="valCbCodigo" runat="server" />
        <asp:HiddenField ID="valClasifi" runat="server" />
        <asp:HiddenField ID="valCantidadDispo" runat="server" Value="0" />

        <asp:GridView   ID="gv_CheckList"   runat="server"  OnPreRender="gv_CheckList_PreRender" OnPageIndexChanged="gv_CheckList_PageIndexChanged"
            AutoGenerateColumns="False" style="margin-left:auto; margin-right:auto;  width:1300px; height:100px;">

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
                                <asp:Label ID="lblIdColavora" runat="server" Text='<%# Eval("CB_CODIGO")%>'/>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:Label ID="lblHColavora" runat="server" Text="ID Colavorador"/>
                                <asp:LinkButton ID="btnIdColavora" runat="server" Text="ID Colacorador" style="display:none"/>
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

                <asp:TemplateField HeaderText="Cantidad de disponibilidad" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox id="textDisponibilidad"  runat="server" Text='<%# Eval("num_disp") %>' style="display:none"/>
                            <asp:Label ID="lblCantidadDisp" runat="server" Text='<%# Eval("num_disp") %>' />
                        </ItemTemplate>
                </asp:TemplateField>
                    
                <asp:TemplateField HeaderText="Habilitar vacaciones" >
                    <ItemTemplate>
                        <asp:LinkButton ID="btnHabilitar"  runat="server" ShowHeader="True" Text="Habilitar" />
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