$(document).ready(function () {

    function GetParts() {
        $.ajax({
            url: '/Manager/GetParts',
            type: 'GET',
            contentType: "application/json",
            success: function (parts) {
                var rows = "";
                $.each(parts, function (index, part) {
                    rows += row(part);
                })
                $("table tbody").append(rows);
            }
        });
    }

    function GetPart(id) {
        $.ajax({
            url: '/Manager/GetPart/' + id,
            type: 'GET',
            contentType: "application/json",
            success: function (part) {
                var form = document.forms["PartsForm"];

                form.elements["txtArtikul"].value = part.artikul;
                form.elements["txtName"].value = part.name;
                form.elements["txtCost"].value = part.cost;
                form.elements["txtDateOfDelivery"].value = part.dateOfDelivery;
                form.elements["txtQuantity"].value = part.quantity;
            }
        });
    }

    function PartAdd(artikul, name, cost, quantity) {
        $.ajax({
            url: "/Manager/PartAdd",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                Artikul:artikul,
                Name: name,
                Cost: cost,
                Quantity: quantity
            }),
            success: function (part) {
                $("table tbody").append(row(part));
                resetAdd();
                $('#add').modal('hide');
            }
        })
    }


    function EditPart(artikul, name, cost, dateOfDelivery, quantity) {
        $.ajax({
            url: "/Manager/EditPart",
            contentType: "application/json",
            method: "PUT",
            data: JSON.stringify({
                Artikul: artikul,
                Name: name,
                Cost: cost,
                DateOfDelivery: dateOfDelivery,
                Quantity: quantity
            }),
            success: function (part) {
                $("tr[data-rowid='" + part.artikul + "']").replaceWith(row(part));
                reset();
                $('#myModalSave').modal('hide');

            }
        })
    }

    function DeletePart(artikul) {
        $.ajax({
            url: "/Manager/PartRemove/" + artikul,
            contentType: "application/json",
            method: "DELETE",
            success: function (part) {
                $("tr[data-rowid='" + part.artikul + "']").remove();
            }
        })
    }


    var row = function (part) {
        return "<tr data-rowid='" + part.artikul + "'>" +
            "<td>" + part.artikul + "</td> <td>" + part.name + "</td>" +
            "<td>" + part.cost + "</td> <td>" + part.dateOfDelivery + "</td>" +
            "<td>" + part.quantity + "</td> " +
            "<td><a class='editLink' data-toggle='modal' data-target='#myModalSave' data-id='" + part.artikul + "'>Изменить</a> | " +
            "<a class='removeLink' data-id='" + part.artikul + "'>Удалить</a> | " +
            "<a href='' data-toggle='modal' data-target='#add'>Добавить</a></td ></tr > ";
    }

    $("#partDataEdit").submit(function (e) {
        e.preventDefault();
        var artikul = this.elements["txtArtikul"].value;
        var name = this.elements["txtName"].value;
        var cost = this.elements["txtCost"].value;
        var dateOfDelivery = this.elements["txtDateOfDelivery"].value;
        var quantity = this.elements["txtQuantity"].value;

        EditPart(artikul, name, cost, dateOfDelivery, quantity);
    });

    $("#partDataAdd").submit(function (e) {
        e.preventDefault();
        var artikul = this.elements["txtArtikul"].value;
        var name = this.elements["txtName"].value;
        var cost = this.elements["txtCost"].value;
        var quantity = this.elements["txtQuantity"].value;

        PartAdd(artikul, name, cost, quantity);

    });

    $("body").on("click", ".editLink", function () {
        var artikul = $(this).attr("data-id");
        alert(artikul);
        GetPart(artikul);
    })

    $("body").on("click", ".removeLink", function () {
        var artikul = $(this).attr("data-id");
        alert(artikul);
        DeletePart(artikul);
    })

    function reset() {
        var form = document.forms["PartsForm"];
        form.reset();
        form.elements["txtArtikul"].value = 0;
    }
    function resetAdd() {
        var form = document.forms["PartsFormAdd"];
        form.reset();
    }

    GetParts()

})