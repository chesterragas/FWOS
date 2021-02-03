function Dropdown_select(id, url) {
    var option = '<option value="">--SELECT--</option>';
    $('#' + id).html(option);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'JSON',
    }).done(function (data, textStatus, xhr) {
        $.each(data.list, function (i, x) {
            option = '<option value="' + x.ID + '">' + x.ItemName + '</option>';
            $('#' + id).append(option);
        });

    }).fail(function (xhr, textStatus, errorThrown) {
        console.log(errorThrown, textStatus);
    });
}

$(function () {
    $("#logoutbtn").on("click", function () {

        window.location = "/Login/SignOut";
    })
    $('.select2').select2({
        width: '100%'
    });
})