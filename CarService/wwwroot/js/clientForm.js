$(document).on("click", "#clientPost", function () {

    var clJson = {
        Name: $('#name').val(),
        Surname: $('#surname').val(),
        Patronimyc: $('#patronimyc').val(),
        Email: $('#email').val(),
        Phone: $('#phone').val(),
        Problem: $('#problem').val(),
        Date:new Date()
    }

    $.ajax({
        url: '/Home/ClientForm',
        method: 'post',
        data: { jClient: JSON.stringify(clJson) },
        success: function () { alert("OK"); }
     
    })
})