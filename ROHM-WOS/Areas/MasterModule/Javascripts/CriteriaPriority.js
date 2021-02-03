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

    $('#btn_add').on('click', function () {
        if ($('#ID').val() == "") {
            view_state('create');
        } else {
            view_state('update');
        }
    });

    $('#btn_clear').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel').on('click', function () {
        view_state('view');
    });

    $('#btn_delete').on('click', function () {
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
    view_state('view');

    $('#tbl_criteria').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });

    $('#tbl_detail').DataTable({
        initComplete: function () {
            $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
            $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
        }
    });
}

function view_state(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_div').show();
            $('#btn_save_div').hide();
            $('#btn_clear_div').hide();
            $('#btn_cancel_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_div').hide();
            $('#btn_save_div').show();
            $('#btn_clear_div').show();
            $('#btn_cancel_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_div').show();
            $('#btn_save_div').hide();
            $('#btn_clear_div').hide();
            $('#btn_cancel_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_div').hide();
            $('#btn_save_div').show();
            $('#btn_clear_div').hide();
            $('#btn_cancel_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

