$(document).ready(function () {

    var $form = $("#edit-profile-form");
    var $form2 = $("#change-pass");

    $form.submit(function (e) {
        e.preventDefault();

        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();

        var valFirstName = validateName(first_name, true);
        var valLastName = validateName(last_name, true);

        if (valFirstName) {
            markAsValid("#first_name");
        }
        if (valLastName) {
            markAsValid("#last_name");
        }

        if (!valFirstName || !valLastName) {
            Materialize.toast(toastContent, 3000);

            if (!valFirstName) {
                markAsInvalid("#first_name");
            }
            if (!valLastName) {
                markAsInvalid("#last_name");
            }
            return;
        }
        $form.get(0).submit();
    });

    $form2.submit(function (e) {
        e.preventDefault();

        var pass = $("#new_password").val();
        var conf = $("#conf_password").val();

        var valPassw = validatePassword(pass, true);

        if(valPassw)
        {
            markAsValid("#new_password");
        }
        if (pass === conf) {
            markAsValid("#conf_password");
        }

        if (!valPassw || (pass !== conf))
        {
            if (!valPassw) {
                markAsInvalid("#new_password");
                markAsInvalid("#conf_password");
            }
            if (pass !== conf) {
                markAsInvalid("#conf_password");
            }
            return;
        }

        $form2.get(0).submit();
    });
});