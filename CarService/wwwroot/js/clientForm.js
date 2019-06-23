$(document).on("click", "#clientPost", function () {

    var clJson = {
        Name: $('#name').val(),
        Surname: $('#surname').val(),
        Patronimyc: $('#patronimyc').val(),
        Email: $('#email').val(),
        Phone: $('#phone').val(),
        Problem: $('#problem').val(),
        IsCancel: false,
        IsRecord: false
    }

    $.ajax({
        url: '/Home/ClientForm',
        method: 'post',
        data: {
            jClient: JSON.stringify(clJson)
        },
        success: function ()
        {
            alert("Ваша заявка была успешно отправлена!");
            reset();
        }
     
    })

    function reset() {
        $("#name").val('');
        $("#surname").val('');
        $("#patronimyc").val('');
        $("#email").val('');
        $("#phone").val('');
        $("#problem").val('');
    }
})