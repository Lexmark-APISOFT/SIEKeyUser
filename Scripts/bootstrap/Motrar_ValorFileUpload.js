/********************************* MOSTRAR EL NOMBRE DEL ARCHIVO QUE SE ADJUNTO EN EL FILEUPLOAD EN UNA ETIQUETA *********************************************/

function uploadClick() {
    var fname = $('#head_FileUpload1').val().replace(/C:\\fakepath\\/i, '');
    //  alert(fname); ************** Mostrar la alerta con el nombre del archivo cargado
    $('#<%=archivo.ClientID%>').html(fname);
};

//$(document).ready(function uploadClick1() {
//    var fname1 = $('#head_FileUpload2').val().replace(/C:\\fakepath\\/i, '');
//    //  alert(fname); ************** Mostrar la alerta con el nombre del archivo cargado
//    $('#<%=archivo2.ClientID%>').html(fname1);
//});