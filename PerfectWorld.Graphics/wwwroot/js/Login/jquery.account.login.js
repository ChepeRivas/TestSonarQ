
$().ready(function ($) {

    $("#loginform").validate({
        ignore: [],
        rules: {
        
            Contrasenia: {
                required: true,
                minlength: 4,
                maxlength: 10,
                regex: /[a-zA-Z0-9]/
            },
            Usuario: {
                required: true,
                minlength: 4,
                maxlength: 10,
                regex: /[a-zA-Z0-9]/
            }
        }, messages: {
         
            Contrasenia: {
                required: "Password is a required field",
                minlength: "The password must have a minimum of 4 characters",
                maxlength: "The password must have a maximum of 10 characters",
                regex: "Only fields in the range [a-zA-Z0-9] are allowed."

            },
            Usuario: {
                required: "User is a required field",
                minlength: "The user must have a minimum of 4 characters",
                maxlength: "The user must have a maximum of 10 characters",
                regex: "Only fields in the range [a-zA-Z0-9] are allowed."
            }
        }
    });


});

function Login() {

    var Send = {
      
        Contrasenia: $("#Contrasenia").val(),
        Usuario: $("#Usuario").val()
    }
    if (Send.Contrasenia == "") {
        alert("Fields cannot be empty");
        return false;
    }
    if (Send.Usuario == "") {
        alert("Fields cannot be empty");
        return false;
    }
    fns.PostDataAsync("api/GPW/Log/Login", JSON.stringify(Send), function (request) {
        console.log(request);
        if (request["state"]) {
            $(location).attr('href', "/Home/Index");
        } else {
            if (!request["message"] == "") {

                alert(request["message"]);
            }
        }
    });
}


$("#loginform").submit(function (e) {
    e.preventDefault();
    $("#loginform").valid();
    Login();
});

$("#Usuario").keyup(function () {
    this.value = this.value.toLowerCase()

});