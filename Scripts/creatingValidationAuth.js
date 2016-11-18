$(document).ready(function () {
    $("#create").attr("disabled", "true");

    $("#timepicker").blur(function () {
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
    });
});