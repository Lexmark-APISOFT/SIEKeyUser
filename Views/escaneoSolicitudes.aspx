<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="escaneoSolicitudes.aspx.cs" Inherits="SIE_KEY_USER.Views.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: none;
        }
    </style>
    <script type="text/javascript">

        var foliosEscaneados = [];
        var activeRowsFoliosEscaneados = [];

        function enterFolio(e) {

            var folio = document.getElementById("TextBox1").value;

            if (window.event) { // IE                  
                keynum = e.keyCode;
            } else if (e.which) { // Netscape/Firefox/Opera                 
                keynum = e.which;
            }
            if (keynum == 13 && folio != "") {
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

            }

        }

        function returnMenu() {
            window.location.href = "MenuKey.aspx";
        }

        function generate() {

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

            var selected = selectedFolios.toString();



            $.ajax({
                type: "POST",
                url: "escaneoSolicitudes.aspx/generarArchivo", // Replace with the actual URL of your ASPX page 
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
                url: "escaneoSolicitudes.aspx/downloadCSV",
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">

       <button id="btnBack" runat="server" class="btnBack" style="top:20px" onclick="returnMenu()"></button>

       <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
       <div id="fsFolios" runat="server" class="ScanFolios">
            
            <div class="searchsectionFS">
                <div class="delimitante2">
                    <input type="text" id="TextBox1" name="scans" class="txtsearch2" style="left:15%; height:18%; border-radius:0px" onkeypress="enterFolio(event)"/>
                    <button class="btnsearchFS2" onclick="scannedFolios()" ></button>
                    <iframe id="iframe" style="display:none;"></iframe>
                    <button id="foliosToGen" style="background: #182d1a;" onclick="generate()" class="btnActsScanFast2">Generar Archivo</button>
               </div>
            </div>
            <div class="rectangle-4-list2">
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
</asp:Content>
