﻿$(function () {
    Dropdown_select('BuildingIDdropdown', "/Helper/GetDropdown_Building");

    view_state_Floor('view')
    $('#btn_add_Floor').on('click', function () {
        if ($('#FloorID').val() == "") {
            view_state_Floor('create');
        } else {
            view_state_Floor('update');
        }
    });

    $('#btn_clear_Floor').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_Floor').on('click', function () {
        view_state_Floor('view');
    });

    $('#btn_delete_Floor').on('click', function () {
        DeleteFloor();
    })

    $("#FloorForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#FloorID').val() == "") {
            AddFloor($(this));
        } else {
            EditFloor($(this));
        }
    });

    $("#checkall_Floor").on("change", function () {
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
    Initializepage_Floor();

});
var tableFloor;
function Initializepage_Floor() {

    tableFloor = $('#tbl_Floor').DataTable({
        ajax: {
            url: '../WorkLocations/GetFloorList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_Floor'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "BuildingName", name: "BuildingName" },
            { data: "FloorName", name: "FloorName" },



        ],

    });
    $('#tbl_Floor tbody').on('click', '.checkitems', function () {
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
    $('#tbl_Floor tbody').on('click', '.btn_edit_Floor', function () {
        var data = tableFloor.row($(this).parents('tr')).data();

        view_state_Floor('show');

        $('#FloorID').val(data.ID);
        $('#FloorName').val(data.FloorName);

    });



}
var chosenitem = [];
function AddFloor(data) {
    var datanow = data.serialize() + '&BuildingID=' + $('#BuildingIDdropdown').val();
    $.ajax({
        url: '../WorkLocations/CreateFloor',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#FloorForm').trigger("reset");
                tableFloor.ajax.reload();
                view_state_Floor('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("Floor Already Exist");
            }

        }
    });
}

function EditFloor(data) {
    var datanow = data.serialize() + '&ID=' + $('#FloorID').val() + '&BuildingID=' + $('#BuildingIDdropdown').val();
    $.ajax({
        url: '../WorkLocations/UpdateFloor',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#FloorForm').trigger("reset");
                tableFloor.ajax.reload();
                view_state_Floor('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("Floor Already Exist");
            }

        }
    });
}

function DeleteFloor() {

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
                url: '../WorkLocations/DeleteFloor',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#FloorForm').trigger("reset");
                        tableFloor.ajax.reload();
                        view_state_Floor('view')
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("Floor Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_Floor(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_Floor').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_Floor_div').show();
            $('#btn_save_Floor_div').hide();
            $('#btn_clear_Floor_div').hide();
            $('#btn_cancel_Floor_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_Floor').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_Floor_div').hide();
            $('#btn_save_Floor_div').show();
            $('#btn_clear_Floor_div').show();
            $('#btn_cancel_Floor_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_Floor').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_Floor_div').show();
            $('#btn_save_Floor_div').hide();
            $('#btn_clear_Floor_div').hide();
            $('#btn_cancel_Floor_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_Floor').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_Floor_div').hide();
            $('#btn_save_Floor_div').show();
            $('#btn_clear_Floor_div').hide();
            $('#btn_cancel_Floor_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}