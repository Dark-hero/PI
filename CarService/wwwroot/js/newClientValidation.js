var errorNull = true, errorMail = true, errorSurname = true, errorName = true,
    errorPatronimyc = true;

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
$("#surname").focusout(checkNull);
$("#patronimyc").focusout(checkNull);

$("#name").keyup(function () {
    var value = $(this).val();
    if (value.length < 3 && value.length > 50) {
        $(this).notify("Имя не может быть меньше 3 символов", { position: "left" });
        $(this).val(value.slice(3, 50));
        $(this).addClass("errtextbox");
        errorName = true;
    }
    else {
        $(this).removeClass("errtextbox");
        errorName = false;
        $(this).addClass("oktextbox");
    }
});


$("#surname").keyup(function () {
    var value = $(this).val();
    if (value.length < 3 && value.length > 50) {
        $(this).notify("Фамилия не может быть меньше 3 символов", { position: "bottom" });
        $(this).val(value.slice(3, 50));
        $(this).addClass("errtextbox");
        errorName = true;
    }
    else {
        $(this).removeClass("errtextbox");
        errorName = false;
        $(this).addClass("oktextbox");
    }
});

$("#patronimyc").keyup(function () {
    var value = $(this).val();
    if (value.length < 3 && value.length > 50) {
        $(this).notify("Отчество не может быть меньше 3 символов", { position: "bottom" });
        $(this).val(value.slice(3, 50));
        $(this).addClass("errtextbox");
        errorName = true;
    }
    else {
        $(this).removeClass("errtextbox");
        errorName = false;
        $(this).addClass("oktextbox");
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


$(document).ready(function () {
    $("#email").keyup(function () {
        if (!$(this).val()) {
            $("#reg").prop("disabled", true);
        } else {
            $("#reg").prop("disabled", false);
        }
    });
})