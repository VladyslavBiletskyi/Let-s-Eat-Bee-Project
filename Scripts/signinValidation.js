$(document).ready(function () {
    $("#signin").attr("disabled", "true");

    $("#email").blur(function () {
        var email = $("#email").val();
        var password = $("#password").val();
        var valEmail = validateEmail(email, true);
        if(!valEmail)
        {
            $("#email").removeClass('valid');
            $("#email").addClass('invalid');

            $("#signin").attr("disabled", "true");
            return;
        }
        $("#email").removeClass('invalid');
        $("#email").addClass("valid");
        if(valEmail && validatePassword(password))
        {
            $("#signin").removeAttr("disabled");
        }
    });

    $("#password").blur(function () {
        var email = $("#email").val();
        var password = $("#password").val();
        var valPassword = validatePassword(password, true);
        if (!valPassword) {
            $("#password").removeClass('valid');
            $("#password").addClass('invalid');

            $("#signin").attr("disabled", "true");
            return;
        }
        $("#password").removeClass('invalid');
        $("#password").addClass('valid');
        if (valPassword && validateEmail(email)) {
            $("#signin").removeAttr("disabled");
        }
    });
});