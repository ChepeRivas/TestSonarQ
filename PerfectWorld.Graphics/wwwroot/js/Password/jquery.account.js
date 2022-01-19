/*
 * Document Ready
 */
$().ready(function ($) {

   
    $("#forgot").validate({
        rules: {
            Correo: {
                required: true,
                email: true
            },
            Usuario: {
                required: true,
                regex: /[A-Za-z0-9]/
            }
        }, messages: {
           
            Correo: {
                required: "Mail is a required field",
                email: "The mail should be in the following format: pw@domain.tld"
            },
            Usuario: {
                required: "User is a required field",
                minlength: "The user must have a minimum of 4 characters",
                maxlength: "The user must have a maximum of 10 characters",
                regex: "Only fields in the range [a-zA-Z0-9] are allowed."
            },
           
        }
    });
});

//$("#forgot").submit(function (e) {
//    e.preventDefault();
//    $("#forgot").valid();
//    GuardarRegistro();
//});