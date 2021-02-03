﻿$(function () {
    view_state_Building('view')
    $('#btn_add_Building').on('click', function () {
        if ($('#BuildingID').val() == "") {
            view_state_Building('create');
        } else {
            view_state_Building('update');
        }
    });

    $('#btn_clear_Building').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_Building').on('click', function () {
        view_state_Building('view');
    });

    $('#btn_delete_Building').on('click', function () {
        DeleteBuilding();
    })

    $("#BuildingForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#BuildingID').val() == "") {
            AddBuilding($(this));
        } else {
            EditBuilding($(this));
        }
    });

    $("#checkall_Building").on("change", function () {
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
    Initializepage_Building();

});
var tableBuilding;
function Initializepage_Building() {

    tableBuilding = $('#tbl_Building').DataTable({
        ajax: {
            url: '../WorkLocations/GetBuildingList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_Building'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "BuildingName", name: "BuildingName" },



        ],

    });
    $('#tbl_Building tbody').on('click', '.checkitems', function () {
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
    $('#tbl_Building tbody').on('click', '.btn_edit_Building', function () {
        var data = tableBuilding.row($(this).parents('tr')).data();

        view_state_Building('show');

        $('#BuildingID').val(data.ID);
        $('#BuildingName').val(data.BuildingName);

    });



}
var chosenitem = [];
function AddBuilding(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../WorkLocations/CreateBuilding',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#BuildingForm').trigger("reset");
                tableBuilding.ajax.reload();
                view_state_Building('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("Building Already Exist");
            }

        }
    });
}

function EditBuilding(data) {
    var datanow = data.serialize() + '&ID=' + $('#BuildingID').val();
    $.ajax({
        url: '../WorkLocations/UpdateBuilding',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#BuildingForm').trigger("reset");
                tableBuilding.ajax.reload();
                view_state_Building('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("Building Already Exist");
            }

        }
    });
}

function DeleteBuilding() {

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
                url: '../WorkLocations/DeleteBuilding',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#BuildingForm').trigger("reset");
                        tableBuilding.ajax.reload();
                        view_state_Building('view')
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("Building Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_Building(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_Building').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_Building_div').show();
            $('#btn_save_Building_div').hide();
            $('#btn_clear_Building_div').hide();
            $('#btn_cancel_Building_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_Building').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_Building_div').hide();
            $('#btn_save_Building_div').show();
            $('#btn_clear_Building_div').show();
            $('#btn_cancel_Building_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_Building').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_Building_div').show();
            $('#btn_save_Building_div').hide();
            $('#btn_clear_Building_div').hide();
            $('#btn_cancel_Building_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_Building').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_Building_div').hide();
            $('#btn_save_Building_div').show();
            $('#btn_clear_Building_div').hide();
            $('#btn_cancel_Building_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}