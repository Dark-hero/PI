var errorNull = true, errorMail = true, errorPass = true;

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

$("#email").focusout(checkNull);
$("#password").focusout(checkNull);

$("#email").focusout(function () {
    var value = $(this).val().trim();

    if (value.search(/^(([^<>()\[\].,;:\s@"]+(\.[^<>()\[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/i) != 0) {
        $(this).notify("E-mail введён не корректно",{ position: "bottom" });
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
        $(this).notify("Пароль должен быть не менее 6 символов",  { position: "bottom" });
        $(this).addClass("errtextbox");
        errorPass = true;
    }
    else {
            errorPass = false;
        $(this).removeClass("errtextbox");
        $(this).addClass("oktextbox");

        }
});

$(document).ready(function () {
    $("#email").add("#password").keyup(function () {
        if (!$(this).val()) {
            $("#sign").prop("disabled", true);
        } else {
            $("#sign").prop("disabled", false);
        }
    });
})