$(document).ready(function () {

    function GetUsers() {
        $.ajax({
            url: '/Admin/GetUsers',
            type: 'GET',
            success: function (users) {
                var rows = "";
                $.each(users, function (index, user) {
                    rows += row(user);
                })
                $("#tableBody").append(rows);
            }
        });
    }
    var row = function (user) {
        return "<tr data-rowid='" + user.IdUser + "'>" +
            "<td>" + user.Name + "</td> <td>" + user.Surname + "</td>" +
            "<td>" + user.Patronymic + "</td> <td>" + user.Email + "</td>" +
            "<td>" + user.Phone + "</td>" + "<td>" + user.IdRole + "</td>" +
            "<td><a class='editLink' data-toggle='modal' data-target='#myModalSave' data-role='" + user.IdRole + "' data-id='" + user.IdUser + "'>Назначить роль</a><td>" + "|" +
            "<td><a class='removeLink' data-id='" + user.IdUser + "'>Удалить</a><td></tr >";
    }

    $("body").on("click", ".editLink", function () {
        var role = $(this).attr("data-role");
        var id = $(this).attr("data-id");
        var nameRole = "";
        if (role == 0) {
            nameRole = "Администратор"
        }
        else if (role == 1) {
            nameRole = "Пользователь"
        }
        else {
            nameRole = "Менеджер"
        }

        $("#role_").text(nameRole);
        $("#id_").val(id);
    })
    $("#btnSave").click(function () {
        $.ajax({
            url: '/Admin/SaveRole',
            type: 'POST',
            data: {
                IdUser: $("#id_").val(),
                IdRole: $("#roleList").find(":selected").val()
            },
            success: function () {
                $('#myModalSave').modal('hide');
                alert("Данные были успешно изменены!")
            }
        });
    })

    $("body").on("click", ".removeLink", function () {
        var id = $(this).attr("data-id");
        AccountRemove(id);
    })

    function AccountRemove(id) {
        $.ajax({
            url: "/Admin/AccountRemove/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (account) {
                $("tr[data-rowid='" + account.idUser + "']").remove();
            }
        })
    }
    GetUsers();
})