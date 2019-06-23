$(document).ready(function () {

    function GetUser() {
        $.ajax({
            url: '/Account/GetUser',
            type: 'GET',
            success: function (user) {
                $("#name").text(user.Name);
                $("#surname").text(user.Surname);
                $("#patronymic").text(user.Patronymic);
                $("#phone").text(user.Phone);
                $("#email").text(user.Email);
            }
        });
    }

    $("#upd").click(function () {
        var form = document.forms["FormUpdate"];
        form.elements["txtName"].value = $("#name").text()
        form.elements["txtSurname"].value = $("#surname").text()
        form.elements["txtPatronymic"].value = $("#patronymic").text()
        form.elements["txtEmail"].value = $("#email").text()
        form.elements["txtPhone"].value = $("#phone").text();

    })

    $("form").submit(function (e) {
        e.preventDefault();
        var id = $("#IdUser").val();
        var name = this.elements["txtName"].value;
        var surname = this.elements["txtSurname"].value;
        var patronymic = this.elements["txtPatronymic"].value;
        var phone = this.elements["txtPhone"].value;
        var email = this.elements["txtEmail"].value;

       EditUser(id, name, surname, patronymic,phone,email);
    });

    function EditUser(id, name, surname, patronymic, phone, email) {
        $.ajax({
            url: "/Account/EditUser",
            contentType: "application/json",
            method: "PUT",
            data: JSON.stringify({
                IdUser: id,
                Name: name,
                Surname: surname,
                Patronymic: patronymic,
                Phone: phone,
                Email: email
            }),
            success: function () {
                reset();
                $('#myModalSave').modal('hide');
                alert("Данные были успешно изменены!")
            }
        })
    }

    function reset() {
        var form = document.forms["FormUpdate"];
        form.reset();
    }

    $("#btnSavePass").click(function () {
        $.ajax({
            url: "/Account/EditPassword",
            method: "POST",
            data: {
                Password: $("#txtNewRepeat").val(),
                OldPass: $("#txtOld").val(),
                IdUser: $("#IdUser").val()
            }
            ,
            success: function () {
                reset2();
                $('#Pass').modal('hide');
                alert("Данные были успешно изменены!")
            }
        })
    })

    function reset2() {
        var form = document.forms["FormPass"];
        form.reset();
    }
    GetUser();

})
