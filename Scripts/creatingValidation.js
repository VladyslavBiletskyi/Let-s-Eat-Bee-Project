$(document).ready(function () {
    $("#create").attr("disabled", "true");

    $("#first_name").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var time = $("#timepicker").val();

        var valFirstName = validateName(first_name, true);
        if (!valFirstName) {
            $("#first_name").removeClass('valid');
            $("#first_name").addClass('invalid');

            $("#create").attr("disabled", "true");
            return;
        }
        $("#first_name").removeClass('invalid');
        $("#first_name").addClass("valid");
        if (valFirstName && validateName(last_name) && validateTime(time) ) {
            $("#create").removeAttr("disabled");
        }
    });

    $("#last_name").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var time = $("#timepicker").val();

        var valLastName = validateName(last_name, true);
        if (!valLastName) {
            $("#last_name").removeClass('valid');
            $("#last_name").addClass('invalid');

            $("#create").attr("disabled", "true");
            return;
        }
        $("#last_name").removeClass('invalid');
        $("#last_name").addClass("valid");
        if (valLastName && validateName(first_name) && validateTime(time)) {
            $("#create").removeAttr("disabled");
        }
    });

    $("#timepicker").blur(function () {
        var first_name = $("#first_name").val();
        var last_name = $("#last_name").val();
        var time = $("#timepicker").val();

        var valTime = validateTime(time, true);
        if (!valTime) {
            $("#timepicker").removeClass('valid');
            $("#timepicker").addClass('invalid');

            $("#create").attr("disabled", "true");
            return;
        }
        $("#timepicker").removeClass('invalid');
        $("#timepicker").addClass("valid");

        $("#create").removeAttr("disabled");

        if (valTime && validateName(last_name) && validateName(first_name)) {
            $("#create").removeAttr("disabled");
        }
    });
});