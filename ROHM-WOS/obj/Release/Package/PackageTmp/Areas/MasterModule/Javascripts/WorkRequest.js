﻿$(function () {
    view_state_request('view')
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
        Deleterequest();
    })

    $("#requestForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#RequestID').val() == "") {
            Addrequest($(this));
        } else {
            Editrequest($(this));
        }
    });

    $("#checkall_request").on("change", function () {
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
    Initializepage_Workrequest();

});
var tablerequest;
function Initializepage_Workrequest() {

    tablerequest = $('#tbl_request').DataTable({
        ajax: {
            url: '../WorkLocations/GetWorkrequestList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_request'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "WorkRequestName", name: "WorkRequestName" },



        ],

    });
    $('#tbl_request tbody').on('click', '.checkitems', function () {
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
    $('#tbl_request tbody').on('click', '.btn_edit_request', function () {
        var data = tablerequest.row($(this).parents('tr')).data();

        view_state_request('show');

        $('#RequestID').val(data.ID);
        $('#WorkRequestName').val(data.WorkRequestName);

    });



}
var chosenitem = [];
function Addrequest(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../WorkLocations/Createrequest',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#requestForm').trigger("reset");
                tablerequest.ajax.reload();
                view_state_request('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("request Already Exist");
            }

        }
    });
}

function Editrequest(data) {
    var datanow = data.serialize() + '&ID=' + $('#RequestID').val();
    $.ajax({
        url: '../WorkLocations/Updaterequest',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#requestForm').trigger("reset");
                tablerequest.ajax.reload();
                view_state_request('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("request Already Exist");
            }

        }
    });
}

function Deleterequest() {

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
                url: '../WorkLocations/Deleterequest',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#requestForm').trigger("reset");
                        tablerequest.ajax.reload();
                        view_state_request('view')
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("request Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



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
