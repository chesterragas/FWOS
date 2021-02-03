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

    $('#btn_add_division').on('click', function () {
        if ($('#DivisionID').val() == "") {
            view_state_division('create');
        } else {
            view_state_division('update');
        }
    });

    $('#btn_clear_division').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_division').on('click', function () {
        view_state_division('view');
    });

    $('#btn_delete_division').on('click', function () {
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

    $('#btn_add_department').on('click', function () {
        if ($('#DepartmentID').val() == "") {
            view_state_department('create');
        } else {
            view_state_department('update');
        }
    });

    $('#btn_clear_department').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_department').on('click', function () {
        view_state_department('view');
    });

    $('#btn_delete_department').on('click', function () {
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

    $('#btn_add_section').on('click', function () {
        if ($('#SectionID').val() == "") {
            view_state_section('create');
        } else {
            view_state_section('update');
        }
    });

    $('#btn_clear_section').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_section').on('click', function () {
        view_state_section('view');
    });

    $('#btn_delete_section').on('click', function () {
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
    view_state_division('view')
    view_state_department('view')
    view_state_section('view')

    $('#tbl_division').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });

    $('#tbl_department').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });

    $('#tbl_section').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });
}

function view_state_division(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_division').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_division_div').show();
            $('#btn_save_division_div').hide();
            $('#btn_clear_division_div').hide();
            $('#btn_cancel_division_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_division').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_division_div').hide();
            $('#btn_save_division_div').show();
            $('#btn_clear_division_div').show();
            $('#btn_cancel_division_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_division').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_division_div').show();
            $('#btn_save_division_div').hide();
            $('#btn_clear_division_div').hide();
            $('#btn_cancel_division_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_division').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_division_div').hide();
            $('#btn_save_division_div').show();
            $('#btn_clear_division_div').hide();
            $('#btn_cancel_division_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

function view_state_department(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_department').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_department_div').show();
            $('#btn_save_department_div').hide();
            $('#btn_clear_department_div').hide();
            $('#btn_cancel_department_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_department').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_department_div').hide();
            $('#btn_save_department_div').show();
            $('#btn_clear_department_div').show();
            $('#btn_cancel_department_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_department').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_department_div').show();
            $('#btn_save_department_div').hide();
            $('#btn_clear_department_div').hide();
            $('#btn_cancel_department_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_department').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_department_div').hide();
            $('#btn_save_department_div').show();
            $('#btn_clear_department_div').hide();
            $('#btn_cancel_department_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

function view_state_section(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_section').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_section_div').show();
            $('#btn_save_section_div').hide();
            $('#btn_clear_section_div').hide();
            $('#btn_cancel_section_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_section').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_section_div').hide();
            $('#btn_save_section_div').show();
            $('#btn_clear_section_div').show();
            $('#btn_cancel_section_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_section').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_section_div').show();
            $('#btn_save_section_div').hide();
            $('#btn_clear_section_div').hide();
            $('#btn_cancel_section_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_section').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_section_div').hide();
            $('#btn_save_section_div').show();
            $('#btn_clear_section_div').hide();
            $('#btn_cancel_section_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}