$(function () {
    Dropdown_select('c_Utilities', "/Helper/GetDropdown_Utilities");

    view_state_criterias('view')
    $('#btn_add_criterias').on('click', function () {
        if ($('#CriteriaID').val() == "") {
            view_state_criterias('create');
        } else {
            view_state_criterias('update');
        }
    });

    $('#btn_clear_criterias').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_criterias').on('click', function () {
        view_state_criterias('view');
    });

    $('#btn_delete_criterias').on('click', function () {
        Deletecriterias();
    })

    $("#UtilitiesCriteriaForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#CriteriaID').val() == "") {
            Addcriterias($(this));
        } else {
            Editcriterias($(this));
        }
    });

    $("#checkall_criterias").on("change", function () {
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
    Initializepage_criterias();

});
var tablecriterias;
function Initializepage_criterias() {

    tablecriterias = $('#tbl_criterias').DataTable({
        ajax: {
            url: '../Utilities/GetUtilitiesCriteriaList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_criterias'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "UtilitiesName", name: "UtilitiesName" },
            { data: "CriteriaName", name: "CriteriaName" },



        ],

    });
    $('#tbl_criterias tbody').on('click', '.checkitems', function () {
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
    $('#tbl_criterias tbody').on('click', '.btn_edit_criterias', function () {
        var data = tablecriterias.row($(this).parents('tr')).data();

        view_state_criterias('show');

        $('#CriteriaID').val(data.ID);
        $('#c_Utilities').val(data.UtilitiesID);
        $('#CriteriaName').val(data.CriteriaName);

    });



}
var chosenitem = [];
function Addcriterias(data) {
    var datanow = data.serialize() + "&UtilitiesID=" + $('#c_Utilities').val();
    $.ajax({
        url: '../Utilities/CreateUtilitiesCriteria',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#UtilitiesCriteriaForm').trigger("reset");
                tablecriterias.ajax.reload();
                view_state_criterias('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("criterias Already Exist");
            }

        }
    });
}

function Editcriterias(data) {
    var datanow = data.serialize() + '&ID=' + $('#CriteriaID').val() + "&UtilitiesID=" + $('#c_Utilities').val();
    $.ajax({
        url: '../Utilities/UpdateUtilitiesCriteria',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#UtilitiesCriteriaForm').trigger("reset");
                tablecriterias.ajax.reload();
                view_state_criterias('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("criterias Already Exist");
            }

        }
    });
}

function Deletecriterias() {

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
                url: '../Utilities/DeleteUtilitiesCriteria',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#UtilitiesCriteriaForm').trigger("reset");
                        tablecriterias.ajax.reload();
                        view_state_criterias('view');
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("criterias Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_criterias(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_criterias').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_criterias_div').show();
            $('#btn_save_criterias_div').hide();
            $('#btn_clear_criterias_div').hide();
            $('#btn_cancel_criterias_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_criterias').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_criterias_div').hide();
            $('#btn_save_criterias_div').show();
            $('#btn_clear_criterias_div').show();
            $('#btn_cancel_criterias_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_criterias').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_criterias_div').show();
            $('#btn_save_criterias_div').hide();
            $('#btn_clear_criterias_div').hide();
            $('#btn_cancel_criterias_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_criterias').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_criterias_div').hide();
            $('#btn_save_criterias_div').show();
            $('#btn_clear_criterias_div').hide();
            $('#btn_cancel_criterias_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}
