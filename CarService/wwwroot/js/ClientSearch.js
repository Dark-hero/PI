$(document).ready(function () {

    function GetClients() {
        $.ajax({
            url: '/Manager/GetClients',
            type: 'GET',
            contentType: "application/json",
            success: function (clients) {
                var rows = "";
                $.each(clients, function (index, client) {
                    rows += row(client);
                })
                $("table tbody").append(rows);
            }
        });
    }

    var row = function (client) {
        return "<tr class='Search' data-rowid='" + client.idClient + "'>" + "<td>" + client.idClient + "</td>"+
            "<td>" + client.name + "</td> <td>" + client.surname + "</td>" +
            "<td>" + client.patronimyc + "</td> <td>" + client.phone + "</td>" +
            "<td>" + client.date + "</td> <td>" + client.problem + "</td></tr > ";
    }

    GetClients()

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