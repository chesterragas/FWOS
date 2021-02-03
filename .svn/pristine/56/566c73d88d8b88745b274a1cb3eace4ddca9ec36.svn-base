$(function () {
    view_state_division('view')
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
        Deletedivision();
    })

    $("#DivisionForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#DivisionID').val() == "") {
            AddDivision($(this));
        } else {
            EditDivision($(this));
        }
    });

    $("#checkall_division").on("change", function () {
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
    Initializepage_division();

});
var tabledivision;
function Initializepage_division() {

    tabledivision = $('#tbl_division').DataTable({
        ajax: {
            url: '../Division/GetDivisionList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_division'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "DivisionName", name: "DivisionName" },



        ],

    });
    $('#tbl_division tbody').on('click', '.checkitems', function () {
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
    $('#tbl_division tbody').on('click', '.btn_edit_division', function () {
        var data = tabledivision.row($(this).parents('tr')).data();

        view_state_division('show');

        $('#DivisionID').val(data.ID);
        $('#DivisionName').val(data.DivisionName);

    });



}
var chosenitem = [];
function AddDivision(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Division/CreateDivision',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#DivisionForm').trigger("reset");
                tabledivision.ajax.reload();
                view_state_division('view');

            }
            else {
                swal("division Already Exist");
            }

        }
    });
}

function EditDivision(data) {
    var datanow = data.serialize() + '&ID=' + $('#DivisionID').val();
    $.ajax({
        url: '../Division/UpdateDivision',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#DivisionForm').trigger("reset");
                tabledivision.ajax.reload();
                view_state_division('view');
            }
            else {
                swal("Division Already Exist");
            }

        }
    });
}

function Deletedivision() {

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
                url: '../Division/Deletedivision',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#DivisionForm').trigger("reset");
                        tabledivision.ajax.reload();
                        view_state_division('view');
                    }
                    else {
                        swal("Division Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
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
