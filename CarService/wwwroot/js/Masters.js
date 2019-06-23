$(document).ready(function () {

    function GetMasters() {
        $.ajax({
            url: '/Manager/GetMasters',
            type: 'GET',
            contentType: "application/json",
            success: function (masters) {
                var rows = "";
                $.each(masters, function (index, master) {
                    rows += row(master);
                })
                $("table tbody").append(rows);
            }
        });
    }

    function GetMaster(id) {
        $.ajax({
            url: '/Manager/GetMaster/' + id,
            type: 'GET',
            contentType: "application/json",
            success: function (master) {
                var form = document.forms["MastersForm"];

                form.elements["hdEventID"].value = master.idMaster;
                form.elements["txtName"].value = master.name;
                form.elements["txtSurname"].value = master.surname;
                form.elements["txtPatronymic"].value = master.patronymic;
                form.elements["txtPassportID"].value = master.passportId;
                form.elements["txtSpecialty"].value = master.specialty;
                form.elements["txtBirthday"].value = master.birthday;
                form.elements["txtPostcode"].value = master.postcode;
                form.elements["txtCity"].value = master.city;
                form.elements["txtStreet"].value = master.street;
                form.elements["txtNumberHouse"].value = master.numberHouse;
                form.elements["txtNumberFlat"].value = master.numberFlat;
                form.elements["txtPhone"].value = master.phone;
            }
        });
    }

    function MasterAdd(name, surname, patronymic, passportID,
                       specialty, birthday, postcode, city,
                       street, numberHouse, numberFlat, phone) {
        $.ajax({
            url: "/Manager/MasterAdd",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                Name: name,
                Surname: surname,
                Patronymic: patronymic,
                PassportID: passportID,
                Specialty: specialty,
                Birthday: birthday,
                Postcode: postcode,
                City: city,
                Street: street,
                NumberHouse: numberHouse,
                NumberFlat: numberFlat,
                Phone: phone

            }),
            success: function (master) {
                $("table tbody").append(row(master));
                reset();
                $('#myModalSave').modal('hide');
            }
        })
    }


    function EditMaster(id,name, surname, patronymic, passportID,
        specialty, birthday, postcode, city,
        street, numberHouse, numberFlat, phone) {
        $.ajax({
            url: "/Manager/EditMaster",
            contentType: "application/json",
            method: "PUT",
            data: JSON.stringify({
                idMaster: id,
                Name: name,
                Surname: surname,
                Patronymic: patronymic,
                PassportID: passportID,
                Specialty: specialty,
                Birthday: birthday,
                Postcode: postcode,
                City: city,
                Street: street,
                NumberHouse: numberHouse,
                NumberFlat: numberFlat,
                Phone: phone
            }),
            success: function (master) {
                $("tr[data-rowid='" + master.idMaster + "']").replaceWith(row(master));
                reset();
                $('#myModalSave').modal('hide');

            }
        })
    }

    function DeleteMaster(id) {
        $.ajax({
            url: "/Manager/MasterRemove/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (master) {
                $("tr[data-rowid='" + master.idMaster + "']").remove();
            }
        })
    }


    var row = function (master) {
        return "<tr data-rowid='" + master.idMaster + "'>" +
            "<td>" + master.name + "</td> <td>" + master.surname + "</td>" +
            "<td>" + master.patronymic + "</td> <td>" + master.birthday + "</td>" +
            "<td>" + master.phone + "</td> <td>" + master.dateOfEmployment + "</td>" +
            "<td>" + master.isWork + "</td>" +
            "<td><a class='editLink' data-toggle='modal' data-target='#myModalSave' data-id='" + master.idMaster + "'>Изменить</a> | " +
            "<a class='removeLink' data-id='" + master.idMaster + "'>Удалить</a> | " +
            "<a href='' data-toggle='modal' data-target='#myModalSave'>Добавить</a></td ></tr > ";
    }

    $("form").submit(function (e) {
        e.preventDefault();
        var id = this.elements["hdEventID"].value;
        var name = this.elements["txtName"].value;
        var surname = this.elements["txtSurname"].value;
        var patronymic = this.elements["txtPatronymic"].value;
        var passportID = this.elements["txtPassportID"].value;
        var specialty = this.elements["txtSpecialty"].value;
        var birthday = this.elements["txtBirthday"].value;
        var postcode = this.elements["txtPostcode"].value;
        var city = this.elements["txtCity"].value;
        var street = this.elements["txtStreet"].value;
        var numberHouse = this.elements["txtNumberHouse"].value;
        var numberFlat = this.elements["txtNumberFlat"].value;
        var phone = this.elements["txtPhone"].value;
        if (id == 0)
            MasterAdd(name, surname, patronymic, passportID, specialty, birthday, postcode, city, street, numberHouse, numberFlat, phone);
        else
            EditMaster(id, name, surname, patronymic, passportID, specialty, birthday, postcode, city, street, numberHouse, numberFlat, phone);
    });

    $("body").on("click", ".editLink", function () {
        var id = $(this).attr("data-id");
        alert(id);
        GetMaster(id);
    })

    $("body").on("click", ".removeLink", function () {
        var id = $(this).attr("data-id");
        alert(id);
        DeleteMaster(id);
    })
   
    function reset() {
        var form = document.forms["MastersForm"];
        form.reset();
        form.elements["hdEventID"].value = 0;
    }

    GetMasters()

})