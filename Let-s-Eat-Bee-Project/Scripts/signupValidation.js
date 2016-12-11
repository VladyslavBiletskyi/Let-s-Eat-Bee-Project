$(document).ready(function () {

    var $form = $("#signup-form");

    $form.submit(function (e) {
        e.preventDefault();

        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var email = $("#email").val();
        var password = $("#password").val();
        var confPassword = $("#conf_password").val();

        var valFirstName = validateName(first_name, true);
        var valLastName = validateName(last_name, true);
        var valEmail = validateEmail(email, true);
        var valPassw = validatePassword(password, true);

        if (valFirstName) {
            markAsValid("#first_name");
        }
        if (valLastName) {
            markAsValid("#last_name");
        }
        if (valEmail) {
            markAsValid("#email");
        }
        if (valPassw) {
            markAsValid("#password");
        }
        if (password === confPassword)
        {
            markAsValid("#conf_password");
        }

        if (!valFirstName || !valLastName || !valEmail || !valPassw || (password !== confPassword))
        {
            Materialize.toast(toastContent, 3000);

            if (!valFirstName) {
                markAsInvalid("#first_name");
            }
            if (!valLastName) {
                markAsInvalid("#last_name");
            }
            if (!valEmail) {
                markAsInvalid("#email")
            }
            if (!valPassw) {
                markAsInvalid("#password");
            }
            if (password !== confPassword)
            {
                markAsInvalid("#conf_password");
            }
            return;
        }
        $form.get(0).submit();
    });
});