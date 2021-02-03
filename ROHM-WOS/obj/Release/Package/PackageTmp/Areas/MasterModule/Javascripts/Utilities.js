$(function () {
    view_state_utilities('view')
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
        Deleteutilities();
    })

    $("#UtilitiesForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#UtilitiesID').val() == "") {
            AddUtilities($(this));
        } else {
            EditUtilities($(this));
        }
    });

    $("#checkall_utilities").on("change", function () {
        if (this.checked) {
            var data = table.row($(this).parents('tr')).data();
            $('.checkitems').each(function (i, obj) {
                chosenitem.push(obj.id);
            });
            $('.checkitems').prop('checked', true);
        }
        else {
            $('.checkitems').each(function (i, obj) {
                chosenitem = chosenitem.filter(e => e !== obj.id);
                // chosenitem.remove(obj.id);
            }); $('.checkitems').prop('checked', false);
        }
    })
    Initializepage_utilities();

});
var tableutilities;
function Initializepage_utilities() {

    tableutilities = $('#tbl_utilitiess').DataTable({
        ajax: {
            url: '../Utilities/GetUtilitiesList',
            type: "POST",
            datatype: "json",

        },
        lengthMenu: [[10, 50, 100], [10, 50, 100]],
        lengthChange: true,
        scrollCollapse: true,
        serverSide: "true",
        order: [0, "asc"],
        processing: "true",
        language: {
            "processing": "processing... please wait"
        },
        destroy: true,
        columnDefs: [
            {
                'targets': 0,
                'checkboxes': {
                    'selectRow': true
                }
            }
        ],
        select: {
            'style': 'multi'
        },

        columns: [

            {
                data: function (x) {
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkitems' />";
                }, sortable: false, searchable: false
            },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_utilities'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "UtilitiesName", name: "UtilitiesName" },



        ],

    });
    $('#tbl_utilitiess tbody').on('click', '.checkitems', function () {
        if (this.checked) {
            chosenitem.push(this.id);
            $(this).prop('checked', true);
        }
        else {
            //chosenitem.remove($(this).id);
            chosenitem = chosenitem.filter(e => e !== this.id);
            $(this).prop('checked', false);
        }
    })
    $('#tbl_utilitiess tbody').on('click', '.btn_edit_utilities', function () {
        var data = tableutilities.row($(this).parents('tr')).data();

        view_state_utilities('show');

        $('#UtilitiesID').val(data.ID);
        $('#UtilitiesName').val(data.UtilitiesName);

    });



}
var chosenitem = [];
function AddUtilities(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Utilities/CreateUtilities',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#UtilitiesForm').trigger("reset");
                tableutilities.ajax.reload();
                view_state_utilities('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("utilities Already Exist");
            }

        }
    });
}

function EditUtilities(data) {
    var datanow = data.serialize() + '&ID=' + $('#UtilitiesID').val();
    $.ajax({
        url: '../Utilities/UpdateUtilities',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#UtilitiesForm').trigger("reset");
                tableutilities.ajax.reload();
                view_state_utilities('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("utilities Already Exist");
            }

        }
    });
}

function Deleteutilities() {

    swal({
        title: "Are you sure?",
        text: "This will delete selected items!",
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
            $.ajax({
                url: '../Utilities/DeleteUtilities',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#UtilitiesForm').trigger("reset");
                        tableutilities.ajax.reload();
                        view_state_utilities('view');
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("utilities Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
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
