$(function () {
    view_state_department('view')
    Dropdown_select('d_DivisionID', "/Helper/GetDropdown_Division");
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
        Deletedepartment();
    })

    $("#DepartmentForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#DepartmentID').val() == "") {
            Adddepartment($(this));
        } else {
            Editdepartment($(this));
        }
    });

    $("#checkall_department").on("change", function () {
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
    Initializepage_department();

});
var tabledepartment;
function Initializepage_department() {

    tabledepartment = $('#tbl_department').DataTable({
        ajax: {
            url: '../Division/GetDepartmentList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_department'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "DivisionName", name: "DivisionName" },
            { data: "DepartmentName", name: "DepartmentName" },



        ],

    });
    $('#tbl_department tbody').on('click', '.checkitems', function () {
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
    $('#tbl_department tbody').on('click', '.btn_edit_department', function () {
        var data = tabledepartment.row($(this).parents('tr')).data();

        view_state_department('show');

        $('#DepartmentID').val(data.ID);
        $('#DepartmentName').val(data.DepartmentName);
        $('#d_DivisionID').val(data.DivisionID);

    });



}
var chosenitem = [];
function Adddepartment(data) {
    var datanow = data.serialize() + '&DivisionID=' + $('#d_DivisionID').val();;
    $.ajax({
        url: '../Division/CreateDepartment',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#departmentForm').trigger("reset");
                tabledepartment.ajax.reload();
                view_state_department('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("department Already Exist");
            }

        }
    });
}

function Editdepartment(data) {
    var datanow = data.serialize() + '&ID=' + $('#DepartmentID').val() + '&DivisionID=' + $('#d_DivisionID').val();;
    $.ajax({
        url: '../Division/UpdateDepartment',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#departmentForm').trigger("reset");
                tabledepartment.ajax.reload();
                view_state_department('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("department Already Exist");
            }

        }
    });
}

function Deletedepartment() {

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
                url: '../Division/DeleteDepartment',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#departmentForm').trigger("reset");
                        tabledepartment.ajax.reload();
                        view_state_department('view');
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("department Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



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
