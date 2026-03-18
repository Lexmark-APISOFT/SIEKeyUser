
//--------------------------------------- ACIVAR BOTON DESPUES DE LLENAR TODOS LOS CAMPOS ------------------------->
ok_fam = "";

function activar(agregar_fam_form, ok_fam) {
    cont = 0;

    for (i = 0; i <= agregar_fam_form.elements.length; i++) {
        if (agregar_fam_form.elements[i].value != "") {
            cont = cont + 1;
        }

        if (ok_fam == "") {
            if (agregar_fam_form.elements[i].type == "submit") {
                ok_fam = agregar_fam_form.elements[i].name;
            }
        }
        if (cont == agregar_fam_form.elements.length) {
            agregar_fam_form.ok_fam.disabled = false;
            $(ok_fam).css({ 'background-color': 'rgb(0,128,0)', 'border-color': 'rgb(0,128,0)' });
        }
        else {
            agregar_fam_form.ok_fam.disabled = true;
            $(ok_fam).css({ 'background-color': 'rgb(127,127,127)', 'border-color': 'rgb(127,127,127)' });
        }
    }
}

//disabled = "disabled"