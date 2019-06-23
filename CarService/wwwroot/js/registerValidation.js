var errorNull = true, errorMail = true, errorPass = true, errorName = true;

var checkNull = function () {
    $(this).val($(this).val().trim());
    if ($(this).val() == "") {

        $(this).notify("Поле нужно заполнить", "error");
        $(this).addClass("errtextbox");
        errorNull = true;
    } else {
        errorNull = false;
        $(this).removeClass("errtextbox");
    }
};

$("#name").focusout(checkNull);
$("#email").focusout(checkNull);
$("#password").focusout(checkNull);

$("#name").keyup(function () {
    var value = $(this).val();
    if (value.length < 3 && value.length > 50) {
        $(this).notify("Имя не может быть меньше 3 символов", { position: "bottom" });
        $(this).val(value.slice(3, 50));
    }
});


$("#email").focusout(function () {
    var value = $(this).val().trim();

    if (value.search(/^(([^<>()\[\].,;:\s@"]+(\.[^<>()\[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/i) != 0) {
        $(this).notify("E-mail введён не корректно", { position: "bottom" });
        $(this).addClass("errtextbox");
        errorMail = true;
    } else {
        $(this).removeClass("errtextbox");
        errorMail = false;
        $(this).addClass("oktextbox");

    }

});

$("#password").focusout(function () {
    var value = $(this).val();
    if (value.length <= 5) {
        $(this).notify("Пароль должен быть не менее 6 символов", { position: "bottom" });
        $(this).addClass("errtextbox");
        errorPass = true;
    }
    else {
        errorPass = false;
        $(this).removeClass("errtextbox");
        $(this).addClass("oktextbox");

    }
});

$("#confirmPassword").focusout(function () {
    var value = $(this).val();
    if (value != $("#password").val()) {
        $(this).notify("Пароль не совпадает", "error");
        $(this).addClass("errtextbox");
        errorPass = true;
    } else {
        errorPass = false;
        $(this).removeClass("errtextbox");
        $(this).addClass("oktextbox");

    }
});

$(document).ready(function () {
    $("#email").keyup(function () {
        if (!$(this).val()) {
            $("#reg").prop("disabled", true);
        } else {
            $("#reg").prop("disabled", false);
        }
    });
})