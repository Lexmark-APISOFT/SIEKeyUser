<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="matriz_disponibilidad.aspx.cs" Inherits="SIE_KEY_USER.Views.matriz_disponibilidad" %>
     <asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <link href="../Content/Vacaciones.css" rel="stylesheet" type="text/css" />
         <style>
             .availability-input {
                 width: 100%;
                 padding: 12px 20px;
                 margin: 8px 0;
                 display: inline-block;
                 border: 1px solid #ccc;
                 border-radius: 20px;
                 box-sizing: border-box;
             }
         </style>
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
        $(document).ready(function () {
            $("#<%=btnApplySelection.ClientID%>").click(function () {
                $("#myModal").modal("show");
                $("#<%=btnAcceptAvailability.ClientID%>").prop('Value', "Aplicar selección");


                $.ajax({
                    type: "POST",
                    url: "matriz_disponibilidad.aspx/decideButton", // Replace with the actual URL of your ASPX page 
                    data: JSON.stringify({ bPress: 1 }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) { // Handle the server response if needed 
                        console.log("SUCCESS");
                    },
                    error: function (error) {
                        console.log("Error:", error);
                    },
                }); 
                

            });
            $("#<%=btnApplyAll.ClientID%>").click(function () {
                $("#myModal").modal("show");
                $("#<%=btnAcceptAvailability.ClientID%>").prop('Value', "Aolicar a todos");

                $.ajax({
                    type: "POST",
                    url: "matriz_disponibilidad.aspx/decideButton", // Replace with the actual URL of your ASPX page 
                    data: JSON.stringify({ bPress: 2 }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) { // Handle the server response if needed 
                        console.log("SUCCESS");
                    },
                    error: function (error) {
                        console.log("Error:", error);
                    },
                }); 

            });
     
        });
   
        $(document).ready(function () {
            $('#table-row').hide();
            dataTable = $("#<%=gv_CheckList.ClientID%>").DataTable({
                "bFilter": true,
                "dom":'lrtip',
                "lengthMenu": [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
                "stateSave": false,
                "paging": true,
                "searching": true,
                "filter": true,
                "bAutoWidth": false,
                "ordering": true,
                "info": false

            });
            buildSelect(dataTable);

            $('#filters-row').on('change', function () {
                $('#table-row').show();
                dataTable.column.adjust();

                dataTable.search(this.value).draw();
            });
            dataTable.on('draw', function () {
                buildSelect(dataTable);
            });


        });
        function buildSelect(table) {
            var counter = 0;
            table.columns([1, 2, 3, 4, 5, 6, 7, 8]).every(function () {
                var column = table.column(this, {
                    search: 'applied'
                });
                counter++;
                var columnName = table.column(this.header()).header().textContent.trim();
                var select = $('<select class="btn btn-default dropdown-toggle form-control"><option class="dropdown-menu" value="">' + columnName +'</option><option  value="__select_all__">Select All</option></select>')
                    .appendTo($('#dropdown' + counter).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        if (val === '__select_all__') {
                            column.search('').draw();
                        } else {
                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        }
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>');

                });

                // The rebuild will clear the exisiting select, so it needs to be repopulated
                var currSearch = column.search();
                if (currSearch) {
                    // Use RegEx to find the selected value from the unique values of the column.
                    // This will use the Regular Expression returned from column.search to find the first matching item in column.data().unique
                    select.val(column.data().unique().toArray().find((e) => e.match(new RegExp(currSearch))));
                }
            });
        }


    </script>


        </asp:Content>
        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
            <form runat="server" >
                <div class="container-fluid">
                    <div class="Row">
                      <h3 style="margin-top:40px;margin-bottom:10px;">Matriz de Disponibilidad</h3>
                    </div>
                    <div class="row" style="display:flex;">
                            <asp:LinkButton CssClass="btn btn-success"  style="margin-bottom:30px;margin-left:30px;border-radius:35%;" runat="server" OnClick="btnRegresar_Click">
                                <i class="glyphicon glyphicon-arrow-left" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div id="filters-row" class="Row">
                        <div class=" col-md-12" style="margin-bottom:30px;">
                            <div class="col-md-1">
                                <p><span id="dropdown1" class="dropdown">
                                  </span>
                                </p>
                            </div>
                            <div class="col-md-1">
                                <p><span id="dropdown2" class="dropdown">
                                   
                                    </span>
                                </p>
                            </div>
                            <div class="col-md-2">
                                <p><span id="dropdown3" class="dropdown">
                                    
                                  </span>
                                </p>
                            </div>
                            <div class="col-md-1">
                                <p><span id="dropdown4" class="dropdown">
                                    
                                   
                                  </span>
                                </p>
                            </div>
                            <div class="col-md-2">
                                <p><span id="dropdown5" class="dropdown">
                                    
  
                                  </span>
                                </p>
                            </div>
                            <div class="col-md-1">
                                <p><span id="dropdown6" class="dropdown">
                                    
                                    
                                  </span>
                                </p>
                            </div>
                            <div class="col-md-2">
                                <p><span id="dropdown7" class="dropdown">
                                    

                                  </span>
                                </p>
                            </div>
                            <div class="col-md-2">
                                <p><span id="dropdown8" class="dropdown">
                                    

                                  </span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="Info" runat="server" Text=""></asp:Label>
                    <div id="table-row" class="Row">
                        <div class="col-md-offset-1 col-md-10" style="margin-bottom:40px;">
                             <asp:GridView ID="gv_CheckList" runat="server"  AutoGenerateColumns="False"  style="margin-left:auto; margin-right:auto;  width:1300px; height:100px;"  class="table table-bordered table-hover table-responsive grid"  OnPreRender="gv_CheckList_PreRender"  Visible="true" >
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
                        </div>
                    </div>
                    <div class="Row">
                        <div class="col-md-12">
                            <asp:Button ID="btnApplySelection" runat="server" Text="Aplicar a la seleccion"  class="apply-availability-button"  OnClientClick="return false"/>
                             <asp:LinkButton ID="ButtonUndo" runat="server" OnClick="undoChanges">
                             <img id="img1" src="../Images/undo.png" class="botonUndo" runat="server" />
                             </asp:LinkButton>
                            <asp:Button ID="btnApplyAll" runat="server" Text="Aplicar a todo"  class="apply-availability-button"  OnClientClick="return false" CssClass="apply-availability-button" style="margin: 10px 10px 10px 10px;"/>                            
                        </div>
                    </div>
               
              
            <!-- Modal -->
                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" ">
                  <div class="modal-dialog" role="document">
                    <div class="modal-content" style="background-color:#1e9827;border-radius:35px;">
                      <div class="modal-body">
                          <h2 style="color:white;font-weight:bold;">Ingrese la disponibilidad que desea aplicar</h2>
                        <input id="txtAvailabilityInput" class="availability-input"  type="text" placeholder="Disponibilidad" runat="server"/>
                        <asp:Button id="btnAcceptAvailability" class="btn btn-success" style="border-radius: 15px !important;"  runat="server" onclick="changeCupos_Click" Text="Aplicar"/>
                        <button id="btnDenyAvailability" type="button" class="btn btn-danger" style="border-radius: 15px !important;" data-dismiss="modal">Cancelar</button>
                      </div>
                    </div>
                  </div>
                </div>
               </form>
        </asp:Content>