$(document).ready(function () {
    $(".dropdown-button").dropdown();
});
$(document).ready(function () {
    $(".button-collapse").sideNav();
});
$(document).ready(function () {
    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 15 // Creates a dropdown of 15 years to control year
    });
    $('.timepicker').pickatime({
        default: 'now',
        twelvehour: false, // change to 12 hour AM/PM clock from 24 hour
        donetext: 'OK',
        autoclose: false,
        vibrate: true // vibrate the device when dragging clock hand
    });
});

///for validation

function validateEmail(email) {
    var pattern = new RegExp("^[a-zA-Z]+([a-z]|\.|_|[0-9])+@[a-z]+\.[a-z]+$");
    var res = pattern.test(email);
    //alert(res);
    if (!res) {
        Materialize.toast('Invalid email format.', 3000);
        return false;
    }
    return true;
}
function validatePassword(password) {
    var pattern = new RegExp("^([a-zA-Z]|[0-9]){8,25}$");
    var res = pattern.test(password);
    //alert(res);
    if (!res) {
        Materialize.toast('Invalid password format. Use letters & digits (8-25).', 3000);
        return false;
    }
    return true;
}

function validateName(name) {
    var pattern = new RegExp("^([a-zA-Z]){4,20}$");
    var res = pattern.test(name);
    //alert(res);
    if (!res) {
        Materialize.toast('Invalid name format. Use letters only (4-20)', 3000);
        return false;
    }
    return true;
}

// остальное пока удалено. не работает