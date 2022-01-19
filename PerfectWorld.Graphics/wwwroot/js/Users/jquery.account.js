/*
 * Document Ready
 */
$().ready(function ($) {

    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            if (regexp.constructor != RegExp)
                regexp = new RegExp(regexp);
            else if (regexp.global)
                regexp.lastIndex = 0;
            return this.optional(element) || regexp.test(value);
        },
        "Please check your input."
    );
    $("#regisrFuego").validate({
        ignore: [],
        rules: {
            Usuario: {
                required: true,
                minlength: 3,
                maxlength: 10,
                regex: /[a-z0-9]/
            },
            Contrasena: {
                required: true,
                minlength: 4,
                maxlength: 10,
               regex: /[a-zA-Z0-9]/
            },
            Correo: {
                required: true,
                email: true
            },
            Secret: {
                required: true,
                minlength: 4,
                maxlength: 10,
                regex:/[A-Za-z0-9]/
            },
            valid: {
                required: true,
            }
        }, messages: {
            Usuario: {
                required: "El nombre de PJ es Requerido",
                minlength: "La contraseña debe tener un minimo de 3 caracteres",
                maxlength: "La contraseña debe tener un maximo de 10 caracteres",
                regex:"Solo se permiten campos en el rango [a-z0-9]"
            },
            Contrasena: {
                required: "La contraseña es un campo requerido",
                minlength: "La contraseña debe tener un minimo de 4 caracteres",
                maxlength: "La contraseña debe tener un maximo de 10 caracteres",
                regex: "Solo se permiten campos en el rango [a-zA-Z0-9]"

            },
            Correo: {
                required: "El correo es un campo requerido",
                email: "El correo debe tener un formato: pw@domain.tld"
            },
            Secret: {
                required: "Debe ingresar una palabra secreta",
                minlength: "Su palabra debe tener un minimo de 4 caracteres",
                maxlength: "La contraseña debe tener un maximo de 10 caracteres",
                regex: "Solo se permiten campos en el rango [a-zA-Z0-9]"

            },
            valid: {
                required: function() {
                    alert("Debe confirmar el Captcha");
                }
            }
        }
    });
});

/*
 * Funcion de guardado para registro de usuario
 */
function GuardarRegistro() {

    

    var Send = {
        Id: 0,
        Usuario: $("#Usuario").val(),
        Contrasena: $("#Contrasena").val(),
        Secret: $("#Secret").val(),
        Correo: $("#Correo").val()
    }
    var captchResponse = $("#validar-grecaptcha").val();
    console.log(Send);
    if (captchResponse == 0) {
       
    } else {
        fns.PostDataAsync("api/PWF/Account/AddOrUpdarte", JSON.stringify(Send), function (request) {
            
            if (request["state"]) {
                $(location).attr('href', "/Users/PerfectWorldFuego?key=" + request["data"]);
            } else {
                if (!request["message"] == "") {

                    alert(request["message"]);
                }
            }
        });
    }
}

$("#regisrFuego").submit(function (e) {
    e.preventDefault();
    $("#regisrFuego").valid();
    GuardarRegistro();
});

$("#Usuario").keyup(function () {
    this.value = this.value.toLowerCase()

});

