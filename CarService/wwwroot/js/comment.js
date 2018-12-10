$(document).on("click", "#subComment", function () {
    var x=0;
    $('.rating').children().each(function () {
        if ($(this).is(':checked')) {
            x = $(this).val();
        }
    })
    var comJson = {
        Comment: $('#textComment').val(),
        Score: x
    }

    $.ajax({
        url: '/Home/CommentAdd',
        method: 'post',
        data: { jComment: JSON.stringify(comJson) },
        success: function (d) {
            $('#commentContainer').append(d);
        }
    })
})

