$(document).ready(function () {
    $("#signup").attr("disabled", "true")

    $("#first_name").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var email = $("#email").val();
        var password = $("#password").val();
        var conf_password = $("#conf_password").val();

        var valFirstName = validateName(first_name, true);
        if (!valFirstName) {
            $("#first_name").removeClass('valid');
            $("#first_name").addClass('invalid');

            $("#signup").attr("disabled", "true");
            return;
        }
        $("#first_name").removeClass('invalid');
        $("#first_name").addClass("valid");
        if (valFirstName && validatePassword(password) && validateName(last_name) && validateEmail(email) &&
            (password === conf_password))
        {
            $("#signup").removeAttr("disabled");
        }
    });

    $("#last_name").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var email = $("#email").val();
        var password = $("#password").val();
        var conf_password = $("#conf_password").val();

        var valLastName = validateName(last_name, true);
        if (!valLastName) {
            $("#last_name").removeClass('valid');
            $("#last_name").addClass('invalid');

            $("#signup").attr("disabled", "true");
            return;
        }
        $("#last_name").removeClass('invalid');
        $("#last_name").addClass("valid");
        if (valLastName && validatePassword(password) && validateName(first_name) && validateEmail(email) &&
            (password === conf_password)) {
            $("#signup").removeAttr("disabled");
        }
    });

    $("#email").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var email = $("#email").val();
        var password = $("#password").val();
        var conf_password = $("#conf_password").val();

        var valEmail = validateEmail(email, true);
        if (!valEmail) {
            $("#email").removeClass('valid');
            $("#email").addClass('invalid');

            $("#signup").attr("disabled", "true");
            return;
        }
        $("#email").removeClass('invalid');
        $("#email").addClass("valid");
        if (valEmail && validatePassword(password) && validateName(first_name) && validateName(last_name) &&
            (password === conf_password)) {
            $("#signup").removeAttr("disabled");
        }
    });

    $("#password").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var email = $("#email").val();
        var password = $("#password").val();
        var conf_password = $("#conf_password").val();

        var valPassw = validatePassword(password, true);
        if (!valPassw) {
            $("#password").removeClass('valid');
            $("#password").addClass('invalid');

            $("#signup").attr("disabled", "true");
            return;
        }
        $("#password").removeClass('invalid');
        $("#password").addClass("valid");
        if (valPassw && validateEmail(email) && validateName(first_name) && validateName(last_name) &&
            (password === conf_password)) {
            $("#signup").removeAttr("disabled");
        }
    });

    $("#conf_password").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var email = $("#email").val();
        var password = $("#password").val();
        var conf_password = $("#conf_password").val();

        if (password !== conf_password) {
            Materialize.toast('Passwords have to match.', 3000);
            Materialize.toast(toatContent, 3000);
            $("#conf_password").removeClass('valid');
            $("#conf_password").addClass('invalid');

            $("#signup").attr("disabled", "true");
            return;
        }
        $("#conf_password").removeClass('invalid');
        $("#conf_password").addClass("valid");
        if (validatePassword(password) && validateEmail(email) && validateName(first_name) && validateName(last_name) &&
            (password === conf_password)) {
            $("#signup").removeAttr("disabled");
        }
    });
});