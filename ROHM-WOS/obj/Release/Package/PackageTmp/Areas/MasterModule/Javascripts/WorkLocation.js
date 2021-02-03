$(function () {
    Initializepage();

    $('body').on('keydown', '.usr_ctrl', function (e) {

        var self = $(this)
            , form = self.parents('form:eq(0)')
            , focusable
            , next
            ;
        if (e.keyCode == 40) {
            focusable = form.find('.usr_ctrl').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);

            if (next.is(":disabled")) {
                next = focusable.eq(focusable.index(this) + 2);
            }

            if (next.length) {
                next.focus();
            }
            return false;
        }

        if (e.keyCode == 38) {
            focusable = form.find('.usr_ctrl').filter(':visible');
            next = focusable.eq(focusable.index(this) - 1);

            if (next.is(":disabled")) {
                next = focusable.eq(focusable.index(this) - 2);
            }

            if (next.length) {
                next.focus();
            }
            return false;
        }

        if (e.keyCode === 13) {
            focusable = form.find('.usr_ctrl').filter(':visible');
            next = focusable.eq(focusable.index(this));

            if (next.length) {
                switch (e.target.type) {
                    case "submit":
                        next.form.submit();
                        break;
                    default:
                        next.click();
                }
                next.focus();
            }
            return false;
        }

    });

    $('#btn_add_location').on('click', function () {
        if ($('#LocationID').val() == "") {
            view_state_location('create');
        } else {
            view_state_location('update');
        }
    });

    $('#btn_clear_location').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_location').on('click', function () {
        view_state_location('view');
    });

    $('#btn_delete_location').on('click', function () {
        swal({
            title: "Are you sure?",
            text: "This will delete this data!",
            icon: "warning",
            buttons: {
                cancel: {
                    text: "No, cancel it!",
                    value: false,
                    visible: true,
                    className: 'btn btn-default',
                    closeModal: true,
                },
                confirm: {
                    text: "Yes, delete it!",
                    value: true,
                    visible: true,
                    className: 'btn btn-danger',
                    closeModal: true
                }
            }
        }).then(function (isConfirm) {
            if (isConfirm) {
                swal("Deleted!", "This account has been deleted.", "success");
            } else {
                swal("Cancelled", "This account details is safe.", "error");
            }
        });
    })

    $('#btn_add_request').on('click', function () {
        if ($('#RequestID').val() == "") {
            view_state_request('create');
        } else {
            view_state_request('update');
        }
    });

    $('#btn_clear_request').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_request').on('click', function () {
        view_state_request('view');
    });

    $('#btn_delete_request').on('click', function () {
        swal({
            title: "Are you sure?",
            text: "This will delete this data!",
            icon: "warning",
            buttons: {
                cancel: {
                    text: "No, cancel it!",
                    value: false,
                    visible: true,
                    className: 'btn btn-default',
                    closeModal: true,
                },
                confirm: {
                    text: "Yes, delete it!",
                    value: true,
                    visible: true,
                    className: 'btn btn-danger',
                    closeModal: true
                }
            }
        }).then(function (isConfirm) {
            if (isConfirm) {
                swal("Deleted!", "This account has been deleted.", "success");
            } else {
                swal("Cancelled", "This account details is safe.", "error");
            }
        });
    })

   
});

function Initializepage() {
    view_state_location('view')
    view_state_request('view')
    

    $('#tbl_location').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });

    $('#tbl_request').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });

    //$('#tbl_classification').DataTable({
    //    initComplete: function () {
    //        $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
    //        $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
    //    }
    //});
}

function view_state_location(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_location').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_location_div').show();
            $('#btn_save_location_div').hide();
            $('#btn_clear_location_div').hide();
            $('#btn_cancel_location_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_location').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_location_div').hide();
            $('#btn_save_location_div').show();
            $('#btn_clear_location_div').show();
            $('#btn_cancel_location_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_location').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_location_div').show();
            $('#btn_save_location_div').hide();
            $('#btn_clear_location_div').hide();
            $('#btn_cancel_location_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_location').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_location_div').hide();
            $('#btn_save_location_div').show();
            $('#btn_clear_location_div').hide();
            $('#btn_cancel_location_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

function view_state_request(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_request').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_request_div').show();
            $('#btn_save_request_div').hide();
            $('#btn_clear_request_div').hide();
            $('#btn_cancel_request_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_request').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_request_div').hide();
            $('#btn_save_request_div').show();
            $('#btn_clear_request_div').show();
            $('#btn_cancel_request_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_request').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_request_div').show();
            $('#btn_save_request_div').hide();
            $('#btn_clear_request_div').hide();
            $('#btn_cancel_request_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_request').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_request_div').hide();
            $('#btn_save_request_div').show();
            $('#btn_clear_request_div').hide();
            $('#btn_cancel_request_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

