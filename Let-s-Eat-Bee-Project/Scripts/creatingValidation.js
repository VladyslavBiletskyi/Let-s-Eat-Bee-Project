$(document).ready(function () {

    var $form = $("#create-form");

    $("#join").click(function (e) {
        e.preventDefault();

        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var time = $("#timepicker").val();

        var valFirstName = validateName(first_name, true);
        var valLastName = validateName(last_name, true);
        var valTime = validateTime(time, true);

        if (valFirstName) {
            markAsValid("#first_name");
        }
        if (valLastName) {
            markAsValid("#last_name");
        }
        if (valTime)
        {
            markAsValid("#timepicker");
        }

        if (!valFirstName || !valLastName || !valTime) {
            Materialize.toast(toastContent, 3000);

            if (!valFirstName) {
                markAsInvalid("#first_name");
            }
            if (!valLastName) {
                markAsInvalid("#last_name");
            }
            if (!valTime) {
                markAsInvalid("#timepicker");
            }
            return;
        }
        $form.get(0).submit();
    });
});