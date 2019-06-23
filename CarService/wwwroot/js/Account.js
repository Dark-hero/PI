$(document).ready(function () {

    function GetOrders() {
        $.ajax({
            url: '/Account/GetOrders',
            type: 'GET',
            success: function (orders) {
                var rows = "";
                $.each(orders, function (index, order) {
                    rows += row(order);
                })
                $("#tableOrders").append(rows);
            }
        });
    }
    var row = function (order) {
        return "<tr data-rowid='" + order.Id + "'>" +
            "<td>" + order.Id + "</td> <td>" + order.VinCode + "</td>" +
            "<td>" + order.StartDate + "</td> <td>" + order.EndDate + "</td>" +
            "<td>" + order.OrderCost + "</td>" +
            "<td><a class='editLink' data-toggle='modal' data-target='#myModalSave' data-cost='" + order.OrderCost + "' data-id='" + order.Id + "'>Подробнее</a><td></tr >";
    }




    $("body").on("click", ".editLink", function () {
        var id = $(this).attr("data-id");
        $("#numb").text(id);
        var total = $(this).attr("data-cost");
        $("#total").text(total);
        $.ajax({
            url: "/Account/GetUserInf",
            contentType: "application/json",
            data: {
                IdUser: $("#IdUser").val(),
                Id: id
            },
            method: "GET",
            success: function (data) {
                $("#name").text(data.Name);
                $("#surname").text(data.Surname);
                $("#patronymic").text(data.Patronymic);

                $.ajax({
                    url: "/Account/GetAutoInfo",
                    data: {
                        IdUser: $("#IdUser").val(),
                        Id: id
                    },
                    method: "GET",
                    success: function (dataCar) {
                        $.map(dataCar, function (item) {
                            $("#mark").text(item.Mark);
                            $("#model").text(item.Model);
                            $("#mileage").text(item.Mileage);
                            $("#year").text(item.Year);
                            $("#registerSign").text(item.RegisterSign);
                        })

                        $.ajax({
                            url: "/Account/GetPartsInfo",
                            data: {
                                IdUser: $("#IdUser").val(),
                                Id:id
                            },
                            method: "GET",
                            success: function (dataParts) {
                                $.map(dataParts, function (item) {
                                    $("#tableParts").append(
                                        "<tr data-rowid='" + item.Artikul + "'>" +
                                        "<td>" + item.Artikul + "</td> <td>" + item.Name + "</td>" +
                                        "<td><b>" + item.Cost + "</b></td></tr > "
                                    )
                                });

                                $.ajax({
                                    url: "/Account/GetWorksInfo",
                                    data: {
                                        IdUser: $("#IdUser").val(),
                                        Id: id
                                    },
                                    method: "GET",
                                    success: function (dataWorks) {
                                        $.map(dataWorks, function (item) {
                                            $("#tablesWorks").append(
                                                "<tr data-rowid='" + item.JobCode + "'>" +
                                                "<td>" + item.JobCode + "</td> <td>" + item.TypeOfWork + "</td>" +
                                                "<td><b>" + item.Cost + "</b></td></tr > "
                                            )
                                        });

                                        $.ajax({
                                            url: "/Account/GetMasterInfo",
                                            data: {
                                                IdUser: $("#IdUser").val(),
                                                Id: id
                                            },
                                            method: "GET",
                                            success: function (master) {
                                                $.map(master, function (item) {
                                                    $("#name_").text(item.name);
                                                    $("#surname_").text(item.surname);
                                                    $("#patronymic_").text(item.patronymic);
                                                });
                                            }
                                        })
                                    }
                                })
                            }
                        })
                    }
                })
            }
        })
    })

    GetOrders();

})
