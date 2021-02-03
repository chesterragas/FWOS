$(function () {
    view_state_ProcessArea('view')
    $('#btn_add_ProcessArea').on('click', function () {
        if ($('#ProcessAreaID').val() == "") {
            view_state_ProcessArea('create');
        } else {
            view_state_ProcessArea('update');
        }
    });

    $('#btn_clear_ProcessArea').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_ProcessArea').on('click', function () {
        view_state_ProcessArea('view');
    });

    $('#btn_delete_ProcessArea').on('click', function () {
        DeleteProcessArea();
    })

    $("#ProcessAreaForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#ProcessAreaID').val() == "") {
            AddProcessArea($(this));
        } else {
            EditProcessArea($(this));
        }
    });

    $("#checkall_ProcessArea").on("change", function () {
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
    Initializepage_ProcessArea();

});
var tableProcessArea;
function Initializepage_ProcessArea() {

    tableProcessArea = $('#tbl_ProcessArea').DataTable({
        ajax: {
            url: '../WorkLocations/GetProcessAreaList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_ProcessArea'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "DivisionID", name: "DivisionID" },
            { data: "ProcessAreaName", name: "ProcessAreaName" },



        ],

    });
    $('#tbl_ProcessArea tbody').on('click', '.checkitems', function () {
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
    $('#tbl_ProcessArea tbody').on('click', '.btn_edit_ProcessArea', function () {
        var data = tableProcessArea.row($(this).parents('tr')).data();

        view_state_ProcessArea('show');

        $('#ProcessAreaID').val(data.ID);
        $('#ProcessAreaName').val(data.ProcessAreaName);

    });



}
var chosenitem = [];
function AddProcessArea(data) {
    var datanow = data.serialize() + '&BuildingID=' + $('#BuildingIDdropdown').val();
    $.ajax({
        url: '../WorkLocations/CreateProcessArea',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#ProcessAreaForm').trigger("reset");
                tableProcessArea.ajax.reload();
                view_state_ProcessArea('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("ProcessArea Already Exist");
            }

        }
    });
}

function EditProcessArea(data) {
    var datanow = data.serialize() + '&ID=' + $('#ProcessAreaID').val() + '&BuildingID=' + $('#BuildingIDdropdown').val();
    $.ajax({
        url: '../WorkLocations/UpdateProcessArea',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#ProcessAreaForm').trigger("reset");
                tableProcessArea.ajax.reload();
                view_state_ProcessArea('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("ProcessArea Already Exist");
            }

        }
    });
}

function DeleteProcessArea() {

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
                url: '../WorkLocations/DeleteProcessArea',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#ProcessAreaForm').trigger("reset");
                        tableProcessArea.ajax.reload();
                        view_state_ProcessArea('view')
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("ProcessArea Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_ProcessArea(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_ProcessArea').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_ProcessArea_div').show();
            $('#btn_save_ProcessArea_div').hide();
            $('#btn_clear_ProcessArea_div').hide();
            $('#btn_cancel_ProcessArea_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_ProcessArea').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_ProcessArea_div').hide();
            $('#btn_save_ProcessArea_div').show();
            $('#btn_clear_ProcessArea_div').show();
            $('#btn_cancel_ProcessArea_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_ProcessArea').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_ProcessArea_div').show();
            $('#btn_save_ProcessArea_div').hide();
            $('#btn_clear_ProcessArea_div').hide();
            $('#btn_cancel_ProcessArea_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_ProcessArea').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_ProcessArea_div').hide();
            $('#btn_save_ProcessArea_div').show();
            $('#btn_clear_ProcessArea_div').hide();
            $('#btn_cancel_ProcessArea_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}
