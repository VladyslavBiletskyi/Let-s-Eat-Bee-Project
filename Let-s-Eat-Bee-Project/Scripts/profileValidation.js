$(document).ready(function () {

    var $form = $("#edit-profile-form");

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

});