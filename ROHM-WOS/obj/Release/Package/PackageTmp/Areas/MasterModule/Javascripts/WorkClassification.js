$(function () {
    view_state_classification('view')
    $('#btn_add_classification').on('click', function () {
        if ($('#ClassificationID').val() == "") {
            view_state_classification('create');
        } else {
            view_state_classification('update');
        }
    });

    $('#btn_clear_classification').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_classification').on('click', function () {
        view_state_classification('view');
    });

    $('#btn_delete_classification').on('click', function () {
        DeleteClassification();
    })

    $("#ClassificationForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#ClassificationID').val() == "") {
            AddClassification($(this));
        } else {
            EditClassification($(this));
        }
    });

    $("#checkall_classification").on("change", function () {
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
    Initializepage_WorkClassification();
    
});
var tableClassification;
function Initializepage_WorkClassification() {

     tableClassification = $('#tbl_classification').DataTable({
        ajax: {
            url: '../WorkLocations/GetWorkClassificationList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_classification'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "WorkClassificationName", name: "WorkClassificationName" },
            
           

        ],

    });
    $('#tbl_classification tbody').on('click', '.checkitems', function () {
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
    $('#tbl_classification tbody').on('click', '.btn_edit_classification', function () {
        var data = tableClassification.row($(this).parents('tr')).data();

        view_state_classification('show');

        $('#ClassificationID').val(data.ID);
        $('#WorkClassificationName').val(data.WorkClassificationName);
       
    });

   

}
var chosenitem = [];
function AddClassification(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../WorkLocations/CreateClassification',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#ClassificationForm').trigger("reset");
                tableClassification.ajax.reload();
                view_state_classification('view');

            }
            else {
                swal("Classification Already Exist");
            }

        }
    });
}

function EditClassification(data) {
    var datanow = data.serialize() + '&ID=' + $('#ClassificationID').val();
    $.ajax({
        url: '../WorkLocations/UpdateClassification',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#ClassificationForm').trigger("reset");
                tableClassification.ajax.reload();
                view_state_classification('view');
            }
            else {
                swal("Classification Already Exist");
            }

        }
    });
}

function DeleteClassification() {

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
                url: '../WorkLocations/DeleteClassification',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#ClassificationForm').trigger("reset");
                        tableClassification.ajax.reload();
                        view_state_classification('view');
                    }
                    else {
                        swal("Classification Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_classification(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_classification').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_classification_div').show();
            $('#btn_save_classification_div').hide();
            $('#btn_clear_classification_div').hide();
            $('#btn_cancel_classification_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_classification').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_classification_div').hide();
            $('#btn_save_classification_div').show();
            $('#btn_clear_classification_div').show();
            $('#btn_cancel_classification_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_classification').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_classification_div').show();
            $('#btn_save_classification_div').hide();
            $('#btn_clear_classification_div').hide();
            $('#btn_cancel_classification_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_classification').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_classification_div').hide();
            $('#btn_save_classification_div').show();
            $('#btn_clear_classification_div').hide();
            $('#btn_cancel_classification_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}
