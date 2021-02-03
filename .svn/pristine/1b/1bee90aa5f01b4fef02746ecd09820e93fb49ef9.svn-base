$(function () {
    view_state_section('view')
    Dropdown_select('sectionDivisionID', "/Helper/GetDropdown_Division");
    Dropdown_select('sectionDepartmentID', "/Helper/GetDropdown_Department?DivisionID=");

    $("#sectionDivisionID").on("change", function () {

        Dropdown_select('sectionDepartmentID', "/Helper/GetDropdown_Department?DivisionID=" + $("#sectionDivisionID").val());
    });


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
        Deletesection();
    })

    $("#SectionForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#SectionID').val() == "") {
            Addsection($(this));
        } else {
            Editsection($(this));
        }
    });

    $("#checkall_section").on("change", function () {
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
    Initializepage_section();

});
var tablesection;
function Initializepage_section() {

    tablesection = $('#tbl_section').DataTable({
        ajax: {
            url: '../Division/GetSectionList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_section'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "DivisionName", name: "DivisionName" },
            { data: "DepartmentName", name: "DepartmentName" },
            { data: "SectionName", name: "SectionName" },



        ],

    });
    $('#tbl_section tbody').on('click', '.checkitems', function () {
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
    $('#tbl_section tbody').on('click', '.btn_edit_section', function () {
        var data = tablesection.row($(this).parents('tr')).data();

        view_state_section('show');

        $('#SectionID').val(data.ID);
        $('#SectionName').val(data.SectionName);
        $('#sectionDivisionID').val(data.DivisionID);
        $('#sectionDepartmentID').val(data.DepartmentID);
        
    });



}
var chosenitem = [];
function Addsection(data) {
    var datanow = data.serialize() + '&DepartmentID=' + $('#sectionDepartmentID').val();
    $.ajax({
        url: '../Division/CreateSection',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#SectionForm').trigger("reset");
                tablesection.ajax.reload();
                view_state_section('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("section Already Exist");
            }

        }
    });
}

function Editsection(data) {
    var datanow = data.serialize() + '&ID=' + $('#SectionID').val() + '&DepartmentID=' + $('#sectionDepartmentID').val();
    $.ajax({
        url: '../Division/Updatesection',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#SectionForm').trigger("reset");
                tablesection.ajax.reload();
                view_state_section('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("section Already Exist");
            }

        }
    });
}

function Deletesection() {

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
                url: '../Division/DeleteSection',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#SectionForm').trigger("reset");
                        tablesection.ajax.reload();
                        view_state_section('view');
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("section Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



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
