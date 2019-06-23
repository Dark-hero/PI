$(document).ready(function () {

    function GetAccounts() {
        $.ajax({
            url: '/Manager/GetAccounts',
            type: 'GET',
            contentType: "application/json",
            success: function (accounts) {
                var rows = "";
                $.each(accounts, function (index, account) {
                    rows += row(account);
                })
                $("table tbody").append(rows);
            }
        });
    }

    var row = function (account) {
        return "<tr class='Search' data-rowid='" + account.idUser + "'>" + "<td>" + account.idUser + "</td>" +
            "<td>" + account.name + "</td> <td>" + account.surname + "</td>" +
            "<td>" + account.patronymic + "</td> <td>" + account.phone + "</td>" +
            "<td>" + account.email + "</td></tr > ";
    }

    GetAccounts()

    function Contains(txt_one, txt_two) {
        if (txt_one.indexOf(txt_two) != -1)
            return true;
    }
    $("#Search").keyup(function () {
        var searchText = $("#Search").val().toLowerCase();
        $(".Search").each(function () {
            if (!Contains($(this).text().toLowerCase(), searchText)) {
                $(this).hide();
            }
            else {
                $(this).show();
            }
        })
    })
})