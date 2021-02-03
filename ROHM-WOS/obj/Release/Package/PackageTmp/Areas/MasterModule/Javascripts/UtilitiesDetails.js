$(function () {
    Dropdown_select('details_Utilities', "/Helper/GetDropdown_Utilities");
    $("#details_Utilities").on("change", function () {
        Dropdown_select('details_Criteria', "/Helper/GetDropdown_UtilitiesDetails?UtilitiesID=" + $("#details_Utilities").val());
    });
    view_state_details('view')
    $('#btn_add_details').on('click', function () {
        if ($('#detailsUtilitiesID').val() == "") {
            view_state_details('create');
        } else {
            view_state_details('update');
        }
    });

    $('#btn_clear_details').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_details').on('click', function () {
        view_state_details('view');
    });

    $('#btn_delete_details').on('click', function () {
        Deletedetails();
    })

    $("#DetailsForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#detailsUtilitiesID').val() == "") {
            Adddetails($(this));
        } else {
            Editdetails($(this));
        }
    });

    $("#checkall_details").on("change", function () {
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
    Initializepage_details();

});
var tabledetails;
function Initializepage_details() {

    tabledetails = $('#tbl_details').DataTable({
        ajax: {
            url: '../Utilities/GetUtilitiesDetailsList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_details'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "UtilitiesName", name: "UtilitiesName" },
            { data: "CriteriaName", name: "CriteriaName" },
            { data: "DetailsName", name: "DetailsName" },
            { data: "Unit", name: "Unit" },



        ],

    });
    $('#tbl_details tbody').on('click', '.checkitems', function () {
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
    $('#tbl_details tbody').on('click', '.btn_edit_details', function () {
        var data = tabledetails.row($(this).parents('tr')).data();

        view_state_details('show');

        $('#detailsUtilitiesID').val(data.ID);
        $('#details_Utilities').val(data.UtilitiesID);
        $('#details_Criteria').val(data.CriteriaID);
        $('#DetailsName').val(data.DetailsName);
        $('#Unit').val(data.Unit);
    });



}
var chosenitem = [];
function Adddetails(data) {
    var datanow = data.serialize() + "&UtilitiesID=" + $('#details_Utilities').val() + "&CriteriaID=" + $('#details_Criteria').val();
    $.ajax({
        url: '../Utilities/CreateUtilitiesDetails',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#DetailsForm').trigger("reset");
                tabledetails.ajax.reload();
                view_state_details('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("details Already Exist");
            }

        }
    });
}

function Editdetails(data) {
    var datanow = data.serialize() + '&ID=' + $('#detailsUtilitiesID').val() + "&UtilitiesID=" + $('#details_Utilities').val() + "&CriteriaID=" + $('#details_Criteria').val();
    $.ajax({
        url: '../Utilities/UpdateUtilitiesDetails',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#DetailsForm').trigger("reset");
                tabledetails.ajax.reload();
                view_state_details('view');
                swal("Data has been saved.", "success");
            }
            else {
                swal("details Already Exist");
            }

        }
    });
}

function Deletedetails() {

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
                url: '../Utilities/DeleteUtilitiesDetails',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#DetailsForm').trigger("reset");
                        tabledetails.ajax.reload();
                        view_state_details('view');
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("details Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_details(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_details').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_details_div').show();
            $('#btn_save_details_div').hide();
            $('#btn_clear_details_div').hide();
            $('#btn_cancel_details_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_details').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_details_div').hide();
            $('#btn_save_details_div').show();
            $('#btn_clear_details_div').show();
            $('#btn_cancel_details_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_details').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_details_div').show();
            $('#btn_save_details_div').hide();
            $('#btn_clear_details_div').hide();
            $('#btn_cancel_details_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_details').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_details_div').hide();
            $('#btn_save_details_div').show();
            $('#btn_clear_details_div').hide();
            $('#btn_cancel_details_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}
