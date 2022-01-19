$().ready(function ($) {

  
    $("#pwform").validate({
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
            }
           
        }, messages: {
            Usuario: {
                required: "User is a required field",
                minlength: "The user must have a minimum of 4 characters",
                maxlength: "The user must have a maximum of 10 characters",
                regex: "Only fields in the range [a-zA-Z0-9] are allowed."
            },
            Contrasena: {
                required: "Password is a required field",
                minlength: "The password must have a minimum of 4 characters",
                maxlength: "The password must have a maximum of 10 characters",
                regex: "Only fields in the range [a-zA-Z0-9] are allowed."

            },
            Correo: {
                required: "Mail is a required field",
                email: "The mail should be in the following format: pw@domain.tld"
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
        Correo: $("#Correo").val()
    }
    console.log(Send);
    fns.PostDataAsync("api/PW/Account/AddOrUpdarte", JSON.stringify(Send), function (request) {

        if (request["state"]) {
            $(location).attr('href', "/Users/PerfectWorld?key=" + request["data"]);
        } else {
            if (!request["message"] == "") {

                alert(request["message"]);
            }
        }
    });
}


$("#pwform").submit(function (e) {
    e.preventDefault();
    $("#pwform").valid();
    GuardarRegistro();
});

$("#Usuario").keyup(function () {
    this.value = this.value.toLowerCase()

});
