
//********************************************** Funcion para poner un popup flotante dentro de la ventana ***************************//
$(document).ready(function () {
    $("#ok_modal").click(function () {
        $("#myModal").modal();
        document.getElementById("#fondo_bloqueado").style.display = 'block';
    });
   
    $("#myModal").click(function () {
        document.getElementById("#fondo_bloqueado").style.display = 'block';
    });
   
    $("#Button3").click(function () {
        document.getElementById("#fondo_bloqueado").style.display = 'none';
    });
    
});