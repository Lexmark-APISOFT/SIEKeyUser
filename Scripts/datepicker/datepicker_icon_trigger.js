$(function () {

    $(".datepicker").datepicker({
        showOn: "button",
        buttonImage: "../Images/calendar.png",
        buttonImageOnly: true,
        buttonText: "Select date",
        dateFormat: "yy-mm-dd",
    });

    $(".ui-datepicker-trigger").css("width", "8%");
    $(".ui-datepicker-trigger").css("margin-left", "4%");
});

$(function ($) {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currenttext: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: '',
    };

    $.datepicker.setDefaults($.datepicker.regional['es']);
});

$(".datepicker").datepicker({ minDate: 0 });