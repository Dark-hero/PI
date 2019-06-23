$(document).ready(function () {

    function GetTypeOfWorks() {
        $.ajax({
            url: '/Manager/GetTypeOfWork',
            type: 'GET',
            contentType: "application/json",
            success: function (tworks) {
                var rows = "";
                $.each(tworks, function (index, twork) {
                    rows += row(twork);
                })
                $("table tbody").append(rows);
            }
        });
    }

    function GetTypeOfWork(id) {
        $.ajax({
            url: '/Manager/GetTypeOfWork/' + id,
            type: 'GET',
            contentType: "application/json",
            success: function (twork) {
                var form = document.forms["TWorkForm"];

                form.elements["txtJobCode"].value = twork.jobCode;
                form.elements["txtTypeOfWork"].value = twork.typeOfWork;
                form.elements["txtCost"].value = twork.cost;
            }
        });
    }

    function TypeOfWorkAdd(typeOfWork, cost) {
        $.ajax({
            url: "/Manager/TypeOfWorkAdd",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                TypeOfWork: typeOfWork,
                Cost: cost
            }),
            success: function (twork) {
                $("table tbody").append(row(twork));
                reset();
                $('#myModalSave').modal('hide');
            }
        })
    }


    function EditTypeOfWork(jobCode, typeOfWork, cost) {
        $.ajax({
            url: "/Manager/EditTypeOfWork",
            contentType: "application/json",
            method: "PUT",
            data: JSON.stringify({
                JobCode: jobCode,
                TypeOfWork: typeOfWork,
                Cost: cost
            }),
            success: function (twork) {
                $("tr[data-rowid='" + twork.jobCode + "']").replaceWith(row(twork));
                reset();
                $('#myModalSave').modal('hide');

            }
        })
    }

    function DeleteTypeOfWork(jobCode) {
        $.ajax({
            url: "/Manager/DeleteTypeOfWork/" + jobCode,
            contentType: "application/json",
            method: "DELETE",
            success: function (twork) {
                $("tr[data-rowid='" + twork.jobCode + "']").remove();
            }
        })
    }


    var row = function (twork) {
        return "<tr data-rowid='" + twork.jobCode + "'>" +
            "<td>" + twork.jobCode + "</td> <td>" + twork.typeOfWork + "</td>" +
            "<td>" + twork.cost + "</td> " +
            "<td><a class='editLink' data-toggle='modal' data-target='#myModalSave' data-id='" + twork.jobCode + "'>Изменить</a> | " +
            "<a class='removeLink' data-id='" + twork.jobCode + "'>Удалить</a> | " +
            "<a href='' data-toggle='modal' data-target='#myModalSave'>Добавить</a></td ></tr > ";
    }

    $("form").submit(function (e) {
        e.preventDefault();
        var jobCode = this.elements["txtJobCode"].value;
        var typeOfWork = this.elements["txtTypeOfWork"].value;
        var cost = this.elements["txtCost"].value;
        if (jobCode == 0)
            TypeOfWorkAdd(typeOfWork, cost)
        else
            EditTypeOfWork(jobCode, typeOfWork, cost);
    });

    $("body").on("click", ".editLink", function () {
        var jobCode = $(this).attr("data-id");
        alert(jobCode);
        GetTypeOfWork(jobCode);
    })

    $("body").on("click", ".removeLink", function () {
        var jobCode = $(this).attr("data-id");
        alert(jobCode);
        DeleteTypeOfWork(jobCode);
    })

    function reset() {
        var form = document.forms["TWorkForm"];
        form.reset();
        form.elements["txtJobCode"].value = 0;
    }

    GetTypeOfWorks()

})