$(document).ready(function () {
    App.init();
    TableManageResponsive.init();
});

function clear() {
    $('.clear').val('');
}

function inputSate(state) {
    var input = $('.input_state');

    for (let index = 0; index < input.length; index++) {
        var type = $(input[index]).prop('nodeName');

        if (type == 'SELECT') {
            $(input[index]).prop('disabled', state);
        } else {
            $(input[index]).prop('readonly', state);
        }
    }
}