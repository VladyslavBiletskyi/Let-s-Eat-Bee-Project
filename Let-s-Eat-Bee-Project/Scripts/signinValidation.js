$(document).ready(function () {
    //$("#signin").attr("disabled", "true");
    var $form = $("#signin-form");

    $form.submit(function (e) {
        //alert($form.get(0));
        e.preventDefault();

        var email = $("#email").val();
        var password = $("#password").val();
        var valEmail = validateEmail(email, true);
        var valPassw = validatePassword(password, true);

        if (valEmail)
        {
            markAsValid("#email");
        }
        if (valPassw) {
            markAsValid("#password");
        }

        if (!valEmail || !valPassw)
        {
            Materialize.toast(toastContent, 3000);
            if(!valEmail)
            {
                markAsInvalid("#email")
            }
            if (!valPassw)
            {
                markAsInvalid("#password");
            }
            return;
        }
        //alert($form.get(0));
        $form.get(0).submit();
    });
});