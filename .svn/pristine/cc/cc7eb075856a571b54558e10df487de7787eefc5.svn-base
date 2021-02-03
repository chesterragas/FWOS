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

    $('#btn_add_utilities').on('click', function () {
        if ($('#UtilitiesID').val() == "") {
            view_state_utilities('create');
        } else {
            view_state_utilities('update');
        }
    });

    $('#btn_clear_utilities').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_utilities').on('click', function () {
        view_state_utilities('view');
    });

    $('#btn_delete_utilities').on('click', function () {
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

    $('#btn_add_criterias').on('click', function () {
        if ($('#CriteriaID').val() == "") {
            view_state_criterias('create');
        } else {
            view_state_criterias('update');
        }
    });

    $('#btn_clear_criterias').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_criterias').on('click', function () {
        view_state_criterias('view');
    });

    $('#btn_delete_criterias').on('click', function () {
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

    $('#btn_add_details').on('click', function () {
        if ($('#DetailID').val() == "") {
            view_state_details('create');
        } else {
            view_state_details('update');
        }
    });

    $('#btn_clear_details').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_details').on('click', function () {
        view_state_details('view');
    });

    $('#btn_delete_details').on('click', function () {
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
    view_state_utilities('view')
    view_state_criterias('view')
    view_state_details('view')

    $('#tbl_utilities').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });

    $('#tbl_criterias').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });

    $('#tbl_details').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });
}

function view_state_utilities(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_utilities').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_utilities_div').show();
            $('#btn_save_utilities_div').hide();
            $('#btn_clear_utilities_div').hide();
            $('#btn_cancel_utilities_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_utilities').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_utilities_div').hide();
            $('#btn_save_utilities_div').show();
            $('#btn_clear_utilities_div').show();
            $('#btn_cancel_utilities_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_utilities').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_utilities_div').show();
            $('#btn_save_utilities_div').hide();
            $('#btn_clear_utilities_div').hide();
            $('#btn_cancel_utilities_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_utilities').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_utilities_div').hide();
            $('#btn_save_utilities_div').show();
            $('#btn_clear_utilities_div').hide();
            $('#btn_cancel_utilities_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

function view_state_criterias(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_criterias').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_criterias_div').show();
            $('#btn_save_criterias_div').hide();
            $('#btn_clear_criterias_div').hide();
            $('#btn_cancel_criterias_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_criterias').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_criterias_div').hide();
            $('#btn_save_criterias_div').show();
            $('#btn_clear_criterias_div').show();
            $('#btn_cancel_criterias_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_criterias').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_criterias_div').show();
            $('#btn_save_criterias_div').hide();
            $('#btn_clear_criterias_div').hide();
            $('#btn_cancel_criterias_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_criterias').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_criterias_div').hide();
            $('#btn_save_criterias_div').show();
            $('#btn_clear_criterias_div').hide();
            $('#btn_cancel_criterias_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

function view_state_details(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_details').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_details_div').show();
            $('#btn_save_details_div').hide();
            $('#btn_clear_details_div').hide();
            $('#btn_cancel_details_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_details').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_details_div').hide();
            $('#btn_save_details_div').show();
            $('#btn_clear_details_div').show();
            $('#btn_cancel_details_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_details').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_details_div').show();
            $('#btn_save_details_div').hide();
            $('#btn_clear_details_div').hide();
            $('#btn_cancel_details_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_details').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_details_div').hide();
            $('#btn_save_details_div').show();
            $('#btn_clear_details_div').hide();
            $('#btn_cancel_details_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}