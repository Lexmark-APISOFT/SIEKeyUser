///**************************** seleccionar todos los checkbox **********************/
$(document).ready(function () {
    
    $("#CheckBox1").click(function () {
        var GridView1 = document.getElementById("#GridView1");

        for (i = 1; i < GridView1.rows.length; i++) {
            GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckBox.checked;
        }
    });

});
/**************************** FIN **********************/