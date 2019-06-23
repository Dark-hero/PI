$(document).ready(function () {

    var helpers =
    {
        buildDropdown: function (result, dropdown, emptyMessage) {
            dropdown.html('');
            if (result != '') {
                $.each(result, function (k, v) {
                    dropdown.append('<option value="' + v.IdMaster + '">' + v.Name + ' ' + v.Surname + ' ' + v.Patronymic + '</option>');
                });
            }
        }
    }

    $.ajax({
        url: "/Manager/DropDownListMasters",
        contentType: "application/json",
        method: "POST",
        success: function (data) {
            helpers.buildDropdown(data,$('#dropdown'),'Select an option');
        }
    })


  
    function CarAdd(vinCode, registerSign, mileage, mark, model, year, engineCapacity) {
        $.ajax({
            url: "/Manager/CarAdd",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                VinCode: vinCode,
                RegisterSign: registerSign,
                Mileage: mileage,
                Mark: mark,
                Model: model,
                Year: year,
                EngineСapacity: engineCapacity
            }),
            success: function () {
                $("input[name='Vin_']").val("" + $('#txtVin').val() + "");
                    reset();
                $('#myModalSave').modal('hide');
            },
            error: function () {
                alert('Failed');
            }
        })
    }

    $("form").submit(function (e) {
        e.preventDefault();
        var vinCode = this.elements["txtVin"].value;
        var registerSign = this.elements["txtRegisterSign"].value;
        var mileage = this.elements["txtMileage"].value;
        var mark = this.elements["txtMark"].value;
        var model = this.elements["txtModel"].value;
        var year = this.elements["txtYear"].value;
        var engineCapacity = this.elements["txtEngineCapacity"].value;
       CarAdd(vinCode, registerSign, mileage, mark, model, year, engineCapacity);
 });

    function reset() {
        var form = document.forms["CarsForm"];
        form.reset();
    }

    $('#saveOrders').click(function () {

        if ($('#txtxVinCode').val() == "") {
            alert('Данные об автомобиле не заполнены!');
            return;
        }
        if (($('#idClient_').text() || $('#idUser_').text()) == "") {
            alert('ID клиентов не заполнены!');
            return;
        }
        if ($("#dropdown option:selected").val() == "") {
            alert('Мастер не выбран!');
            return;
        }
        else {
            var startDate = moment($('#txtStart').val(), "DD/MM/YYYY HH:mm A").toDate();
            var endDate = moment($('#txtEnd').val(), "DD/MM/YYYY HH:mm A").toDate();
            if (startDate > endDate) {
                alert('Проверьте правильность заполнения даты!');
                return;
            }
        }

        var data = {
            VinCode: $('#txtxVinCode').val(),
            IdClient: $('#idClient_').text(),
            StartDate: $('#txtStart').val().trim(),
            EndDate: $('#txtEnd').val().trim(),
            IdUser: $('#idUser_').text(),
            IdMaster: $("#dropdown option:selected").val(),
        }
        SaveOrder(data);
    })

    function SaveOrder(data) {
        $.ajax({
            type: "POST",
            url: '/Manager/SaveOrder',
            data: data,
            success: function (data) {
                $("input[name='orderId']").val(""+data+"");
                alert('ok');
            },
            error: function () {
                alert('Failed');
            }
        })
    }

    var works =
    {
        buildDropdown: function (result, dropdown, emptyMessage) {
            dropdown.html('');
            if (result != '') {
                $.each(result, function (k, v) {
                    dropdown.append('<option value="' + v.JobCode + '">' + v.JobCode + ' ' + v.TypeOfWork + ' ' + v.Cost + '</option>');
                });
            }
        }
    }

    $.ajax({
        url: "/Manager/ListWorks",
        contentType: "application/json",
        method: "POST",
        success: function (data) {
            works.buildDropdown(data, $('#works'), 'Выберите тип работы');
        }
    })
    $("#works").select2();

    var parts =
    {
        buildDropdown: function (result, dropdown) {
            dropdown.html('');
            if (result != '') {
                $.each(result, function (k, v) {
                    dropdown.append('<option value="' + v.Artikul + '">' + v.Artikul + ' ' + v.Name + ' ' + v.Cost + '</option>');
                });
            }
        }
    }

    $.ajax({
        url: "/Manager/ListParts",
        contentType: "application/json",
        method: "POST",
        success: function (data) {
            parts.buildDropdown(data, $('#parts'));
        }
    })
    $("#parts").select2();

    $("#searchPartsVin").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Manager/SearchParts",
                dataType: "json",
                data: { search: $("#searchPartsVin").val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        $("#tab").append(
                            "<table class='table table-striped table-hover'><tbody>"+
                            "<tr data-rowid='" + item.artikul + "'>" +
                            "<td>" + item.artikul + "</td> <td>" + item.name + "</td>" +
                            "<td><b>" + item.cost + "</b></td></tr > " +
                            "</tbody></table>"
                        )
                    }));
                },
                error: function (xhr, status, error) {
                    alert("Error");
                }
            })
        }
    });

    $("#saveWorks").click(function () {

        var code = $("#works").val();
        var order = $('#idOrder').val();

        var dd = {
            IdOrder: order,
            JobCode: code
            }

        $.ajax({
            type: "POST",
            url: '/Manager/SaveOrderToWorks',
            data:dd,
            success: function (data) {
                $("#tableWork tr").remove();
                $.map(data, function (item) {
                    $("#tableWork").append(
                        "<tr data-rowid='" + item.JobCode + "'>" +
                        "<td>" + item.JobCode + "</td> <td>" + item.TypeOfWork + "</td>" +
                        "<td><b>" + item.Cost + "</b></td></tr > " 
                    )
                });
                var total = 0;
                $("#tableWork tr").each(function () {
                    total += parseFloat($(this).find("td b").text());
                });
               var t = total.toFixed(2);
                $("#SummWork").text(t);
                TotalSumm()
            },
            error: function () {
                alert('Failed');
            }
        })
           
    })

    $("#Total").text(parseFloat($("#SummWork").text()) + parseFloat($("#SummParts").text()));

    $("#saveParts").click(function () {

        var parts = $("#parts").val();
        var order = $('#idOrder').val();

        var dd = {
            IdOrder: order,
            Artikul: parts
        }

        $.ajax({
            type: "POST",
            url: '/Manager/SaveOrderToParts',
            data: dd,
            success: function (data) {
                $("#tablePart tr").remove();
                $.map(data, function (item) {
                    $("#tablePart").append(
                        "<tr data-rowid='" + item.Artikul + "'>" +
                        "<td>" + item.Artikul + "</td> <td>" + item.Name + "</td>" +
                        "<td><b>" + item.Cost + "</b></td></tr > "
                    )
                });
                var total = 0;
                $("#tablePart tr").each(function () {
                    total += parseFloat($(this).find("td b").text());
                });
                var t = total.toFixed(2);
                $("#SummParts").text(t);
                TotalSumm()
            },
            error: function () {
                alert('Failed');
            }
        })

    })
    function TotalSumm() {
        var sum = 0;
        var sum2 = 0
        $("#tableWork tr").each(function () {
            sum += parseFloat($(this).find("td b").text());
        });
        $("#tablePart tr").each(function () {
            sum2 += parseFloat($(this).find("td b").text());
        });

        $("#Total").text((sum + sum2).toFixed(2));

        var data = {
            Id: $('#idOrder').val(),
            OrderCost: $("#Total").text().replace('.', ',')
        }
        console.log(data);
        $.ajax({
            type: "POST",
            url: '/Manager/UpdOrder',
            data: data,
            success: function () {
               
            },
            error: function () {
                alert('Failed');
            }
        })

    }
})