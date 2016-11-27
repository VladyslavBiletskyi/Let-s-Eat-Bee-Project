$(document).ready(function () {

    var $form = $("#create-form");

    $form.submit(function (e) {
        e.preventDefault();

        var time = $("#timepicker").val();

        var valTime = validateTime(time, true);

        if (valTime) {
            markAsValid("#timepicker");
        }

        if (!valTime) {
            Materialize.toast(toastContent, 3000);
            markAsInvalid("#timepicker");

            return;
        }
        $form.get(0).submit();
    });
});