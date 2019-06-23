$(document).ready(function () {
    $("body").on("click",".cancel" ,function () {

        var clJson = {
            IdClient: $(this).attr("data-cancel-id")
        }
        id_ = $(this).attr("data-cancel-id");
        $.ajax({
            url: '/Manager/CancelClient',
            method: 'post',
            data: { jClient: JSON.stringify(clJson) },
            success: function () {
                $('#' + id_).toggle();
            }

        })
    });

    $("body").on("click",".record",function () {

        var clJson = {
            IdClient: $(this).attr("data-id")
        }
        id_ = $(this).attr("data-id");
        $.ajax({
            url: '/Manager/RecordClient',
            method: 'post',
            data: { jClient: JSON.stringify(clJson) },
            success: function () {
                $('#' + id_).toggle();
                window.location.replace("https://localhost:44345/manager/record");
            }

        })
    });
});