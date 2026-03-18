<%@ Page Title="KU-Solicitudes vacaciones" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="solicitud_vacaciones.aspx.cs" Inherits="SIE_KEY_USER.Views.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
    
    <style>
        
        body {
            background-image: none;
        }
        /*table.dataTable th{*//*
            background-color:#519d59;*/
        /*}
        table{
        }
        table-bordered.dataTable {
        }
        table-hover.dataTable {
        }
        table-responsive.dataTable {
        }
        :root{
            --theadColor: #519d59;
        }*/
        .tableSoliVac{
            border:5px solid #000;
            width:100% !important;
            height:100%;
        }

        .tableSoliVac table tbody{
            border-radius:10px !important;
        }
        .tableSoliVac th{
            background-color: #519d59;
            color:white;
            font-family:Helvetica, Arial, sans-serif;
            font-size:20px;
        }
        .tableSoliVac td{
            border:0px;
            text-align:center;
        }
        .tableSoliVac tr:hover{
            background-color: #519D59;
            color: #fff;
        }

        .dataTables_wrapper .dataTables_paginate .paginate_button.current,
        .dataTables_wrapper .dataTables_paginate .paginate_button:hover{
            border-radius: 50px;
        }

        .dataTables_wrapper > div{
           margin: 5px;
           z-index: -1;
        }
 

    </style>
    <script type="text/javascript">
        var table;
         var foliosEscaneados = [];
        var activeRowsFoliosEscaneados = [];
        

        $(document).ready(function () {

            table =$("#<%=gv_CheckList.ClientID%>").DataTable({
                    "bFilter": false,
                    "bStateSave": true,
                    "lengthMenu": [5, 10, 25, 50, 100],
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


        <%--function SelectAllCheckboxes(headerCheckbox) {
            var checkboxes = document.querySelectorAll("#<%= gv_CheckList.ClientID %> input[type='checkbox']");
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = headerCheckbox.checked;
            }
            $("#<%=gv_CheckList.ClientID%>").ajax.reload(null, false);
        }--%>

        function SelectAllCheckboxes(headerCheckbox) {
            var checkboxes = document.querySelectorAll("#<%= gv_CheckList.ClientID %> input[type='checkbox']");
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = headerCheckbox.checked;
            }
            //$("#<%=gv_CheckList.ClientID%>").ajax.reload(null, false);
            
            //$.ajax({
            //    type: "POST",
            //    url: "solicitud_vacacionesRemastered.aspx/currPageIndex",
            //    data:,
            //    success: function (data) { // Handle the server response if needed
            //        /*table.pagination.numOfPage >= response;*/
            //        console.log(data);
            //    },
            //    error: function (error) {
            //        console.log("Error: Couldn't get the page");
            //    },
            //}); 
        }

        //function closeFSlayout(FSlayout) {
        //    var fastScanLayout = document.getElementById("fsFolios");
        //    fastScanLayout.hidden = true;

        //}

        function closeFSlayout() {
            var fastScanLayout = document.getElementById("<%= fsFolios.ClientID %>");
            var blurred = document.getElementById("blurredBkgd");
            fastScanLayout.hidden = true;
            var main = document.getElementById("<%= main.ClientID %>");
            main.style.removeProperty("z-index");
            main.style.removeProperty("filter");
            main.style.removeProperty("opacity");


        }

        function enterFolio(e) {

            var folio = document.getElementById("TextBox1").value;

            if (window.event) { // IE                  
                keynum = e.keyCode;
            } else if (e.which) { // Netscape/Firefox/Opera                 
                keynum = e.which;
            }
            if (keynum == 13 && folio!="") {
                scannedFolios();
            }

        }

        function scannedFolios() {
            if (document.getElementById("TextBox1").value != "") {
                var folio = document.getElementById("TextBox1").value;
                var newRow = "<tr> <td class='checkCellTable'> <input type='checkbox' value=" + folio + " style='width:100%' checked /></td ><td>" + folio + "</td></tr > ";
                $("#scanedList").prepend(newRow);

                activeRowsFoliosEscaneados.push(newRow);
                foliosEscaneados.push(folio);

                document.getElementById("TextBox1").value = "";

                $.ajax({
                    type: "POST",
                    url: "solicitud_vacaciones.aspx/acceptScanned", // Replace with the actual URL of your ASPX page 
                    data: JSON.stringify({ folio: folio }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) { // Handle the server response if needed
                        console.log("SUCCESS");
                    },
                    error: function (error) {
                        console.log("Error:", error);
                    },
                });
            }

        }

        function generarArchivo() {

            var fastScanLayout = document.getElementById("<%= fsFolios.ClientID %>");

            var fSchecked = $('#scanedList input[type=checkbox]:checked');


            var selectedFolios = [];

            for (var i = 0; i < fSchecked.length;i++) {
                for (var j = 0; j < foliosEscaneados.length; j++) {
                    if (fSchecked[i].value == foliosEscaneados[j]) {
                        selectedFolios.push(foliosEscaneados[j]);
                        activeRowsFoliosEscaneados.splice(j, 1);
                        foliosEscaneados.splice(j, 1);
                        j -= 1;
                    }
                }
            }
            


            $("#scanedList").html(activeRowsFoliosEscaneados);

            //var fdFolios = new FormData();
            //fdFolios.append("selectedFolios", selectedFolios);

            var selected = selectedFolios.toString();

           

            $.ajax({
                type: "POST",
                url: "solicitud_vacaciones.aspx/generarArchivo", // Replace with the actual URL of your ASPX page 
                data: JSON.stringify({ folios: selected }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) { // Handle the server response if needed
                    console.log(response.d);
                    DownloadFile(response.d);
                    
                },
                error: function (error) {
                    console.log("Error:", error);
                },
            }); 

        }

        function Base64ToBytes(base64) {
            var s = window.atob(base64);
            var bytes = new Uint8Array(s.length);
            for (var i = 0; i < s.length; i++) {
                bytes[i] = s.charCodeAt(i);
            }
            return bytes;
        };

        function DownloadFile(fileName) {
            $.ajax({
                type: "POST",
                url: "solicitud_vacaciones.aspx/downloadCSV",
                data: JSON.stringify({ path: fileName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(r.d);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }

        function enviarSolicitudesPen() {

            var fastScanLayout = document.getElementById("<%= fsFolios.ClientID %>");

            var fSchecked = $('#scanedList input[type=checkbox]:checked');


            var selectedFolios = [];

            for (var i = 0; i < fSchecked.length; i++) {
                for (var j = 0; j < foliosEscaneados.length; j++) {
                    if (fSchecked[i].value == foliosEscaneados[j]) {
                        selectedFolios.push(foliosEscaneados[j]);
                        activeRowsFoliosEscaneados.splice(j, 1);
                        foliosEscaneados.splice(j, 1);
                        j -= 1;
                    }
                }
            }



            $("#scanedList").html(activeRowsFoliosEscaneados);

            //var fdFolios = new FormData();
            //fdFolios.append("selectedFolios", selectedFolios);

            var selected = selectedFolios.toString();



            $.ajax({
                type: "POST",
                url: "solicitud_vacaciones.aspx/enviarSolicitudesPen", // Replace with the actual URL of your ASPX page 
                data: JSON.stringify({ folios: selected }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) { // Handle the server response if needed 
                    console.log(response.d);
                },
                error: function (error) {
                    console.log("Error:", error);
                },
            });


        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    
    

    <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
    <div id="fsFolios" runat="server" class="fastScanFolios">
            
            <button class="close_button" onclick="closeFSlayout()"></button>
            <div class="searchsectionFS">
                <div class="delimitante">
                    <input type="text" id="TextBox1" name="scans" class="txtsearch2" onkeypress="enterFolio(event)"/>
                    <button class="btnsearchFS" onclick="scannedFolios()" ></button>
                    <iframe id="iframe" style="display:none;"></iframe>
                    <button style="background: #D2AC61;" class="btnActsScanFast" onclick="generarArchivo()">Generar Archivo</button>
                    <button  style="background: #F10F0F;" class="btnActsScanFast" onclick="enviarSolicitudesPen()">Enviar a pendientes</button>
               </div>
            </div>
            <div class="rectangle-4-list">
                <div id="scrollableList" class="scrollableList" runat="server">
                    <table id="scanedList">
                        <%--<tr>
                            <td class="checkCellTable"><input type="checkbox" checked/></td>
                            <td>FOLIO</td>
                        </tr>--%>
                    </table>
                </div>
            </div>
        </div>
     <form runat="server">
        <div id="main" runat="server" style="position:relative">
        <h2>Solicitudes de Vacaciones</h2>

 
        <div >
            <asp:Label ID="Label1" Text="No. Reloj" runat="server" style="color: #000; text-align: center; font-size: 25px;font-style: normal;font-weight: 400;line-height: normal;"/><asp:TextBox id="txtSearch" CssClass="txtsearch1" runat="server" type="text"/>
        
            <asp:Button ID="Button1" runat="server" CssClass="btnsearch" onclick="Button1_Click" />
        </div>
        
        <asp:LinkButton ID="btnBack" runat="server" CssClass="btnBack" onclick="backButton_Click" />
        
        <div style="position:relative; display:inline-flex; margin-top:100px">
        
        <div style="float:left; margin-bottom:50px;">

         <asp:GridView ID="gv_CheckList" runat="server"  AutoGenerateColumns="False" style="position:relative;" CssClass="tableSoliVac" OnPreRender="gv_CheckList_PreRender" OnPageIndexChanging="gv_CheckList_PageIndexChanging">
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
                    <asp:BoundField HeaderText="Fecha Inicio" DataField="fech_ini" SortExpression="fech_ini" />
                    <asp:BoundField DataField="fech_fin" HeaderText="Fecha Fin" SortExpression="fech_fin" />
                    <asp:BoundField DataField="duracion" HeaderText="Duración" SortExpression="duracion" />
                    <asp:BoundField DataField="pe_week_number" HeaderText="Periodo de Nómina" SortExpression="pe_week_number" />
                    <asp:BoundField DataField="pe_periodo_fecha" HeaderText="Fecha inicial-final" SortExpression="pe_periodo_fecha" />
                    <asp:BoundField DataField="pe_fecha_pago" HeaderText="Fecha de pago" SortExpression="pe_fecha_pago" />
            </Columns>
                <EmptyDataTemplate>
                            No hay solicitudes pendientes.
                        </EmptyDataTemplate>
        </asp:GridView>
        </div>
        <div style="float:right; margin-left:20px;margin-top:20px;">
            <asp:Button ID="btnFastScan" runat="server" OnClick="openFSlayout" CssClass="btnFastScan" Text="Fast Scan"/>
            <br />
            <asp:Button ID="btnVerAceptadas" runat="server" OnClick="verSolicitudesAceptadas_Click" CssClass="btnRedirecter" Text="Ver aceptadas"/>
            <br />
            <asp:Button ID="btnVerRechazadas" runat="server" OnClick="verSolicitudesRechazadas_Click" CssClass="btnRedirecter" Text="Ver rechazadas"/>
            <br />
            <asp:Button ID="btnDisponibilidad" runat="server" OnClick="modificarDisp_Click" CssClass="btnRedirecter" Text="Cupos"/>
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="modificarDias_Click"  CssClass="btnRedirecter" Text="Días"/>
            <br />
        </div>
        </div>
        <div style="margin-top:0px; height:100px; position:center;">
            <asp:Button ID="btnAceptarSol" onclick="Aceptar_Click" runat="server" CssClass="btnaccept" ForeColor="White" Text="Aceptar"  style="margin-right:60px; text-align: center; font-size: 20px; font-style: normal; font-weight: 400; line-height: normal;"/>
             <asp:Button ID="btnRechazarSol" onclick="Rechazar_Click" runat="server" CssClass="btnreject" ForeColor="White" Text="Rechazar" style="margin-left:60px; text-align: center; font-size: 20px; font-style: normal; font-weight: 400; line-height: normal;"  />
        
        </div>
        </div>
   </form>
</asp:Content>
