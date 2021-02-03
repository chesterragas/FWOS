$(function () {
    Dropdown_select('CriteriaHeaderID', "/Helper/GetDropdown_Criteria");
    view_state_criteria('view')
    view_state_criteria_details('view')
    $("#WorkCategory").val("In-House");
    $('#btn_add_criteria').on('click', function () {
        if ($('#ID').val() == "") {
            view_state_criteria('create');
        } else {
            view_state_criteria('update');
        }
    });
    $('#btn_add_criteria_details').on('click', function () {
        if ($('#ID_details').val() == "" || $('#ID_details').val() == null) {
            view_state_criteria_details('create');
        } else {
            view_state_criteria_details('update');
        }
    });

    $('#btn_clear_criteria').on('click', function () {
        clear();
        //$('.usr_ctrl:first').focus();
    });

    $('#btn_clear_criteria_details').on('click', function () {
        clear();
        //$('.usr_ctrl_details:first').focus();
    });

    $('#btn_cancel_criteria').on('click', function () {
        view_state_criteria('view');
    });

    $('#btn_cancel_criteria_details').on('click', function () {
        view_state_criteria_details('view');
    });

    $('#btn_delete_criteria').on('click', function () {
        Deletecriteria();
    })


    $('#btn_delete_criteria_details').on('click', function () {
        Deletecriteria_details();
    })

    $("#CriteriaForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#ID').val() == "") {
            Addcriteria($(this));
        } else {
            Editcriteria($(this));
        }
    });

    $("#CriteriaDetailsForm").on("submit", function (e) {
        e.preventDefault();
        console.log($('#ID_details').val());
        if ($('#ID_details').val() == "" || $('#ID_details').val() == null) {
            Addcriteria_details($(this));
        } else {
            Editcriteria_details($(this));
        }
    });

    

    $("#checkall_criteria").on("change", function () {
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

    $('#criteriatab').on('shown.bs.tab', function (event) {
        var x = $(event.target)[0].id;   
        $("#WorkCategory").val(x);
        Initializepage_criteria();
        Initializepage_criteriaDetails(criteriaID);
        Initializepage_timeframe();
    });
    Initializepage_criteria();
    Initializepage_criteriaDetails(criteriaID);
    Initializepage_timeframe();
});
var tablecriteria;
var tablecriteriadetails;
var criteriaID;
function Initializepage_criteria() {

    tablecriteria = $('#tbl_criteria').DataTable({
        ajax: {
            url: '../Criteria/GetCriteriaList',
            data: { WorkCategory : $("#WorkCategory").val()},
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
            { data: "ID", name: "ID", visible:false },
            {
                data: function (x) {
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkitems' />";
                }, sortable: false, searchable: false
            },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_criteria'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "CriteriaName", name: "CriteriaName" },
            { data: "CriteriaOrder", name: "CriteriaOrder" },



        ],

    });
    $('#tbl_criteria tbody').on('click', '.checkitems', function () {
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
    $('#tbl_criteria tbody').on('click', '.btn_edit_criteria', function () {
        var data = tablecriteria.row($(this).parents('tr')).data();

        view_state_criteria('show');

        $('#ID').val(data.ID);
        $('#CriteriaName').val(data.CriteriaName);
        $('#CriteriaOrder').val(data.CriteriaOrder);

    });

    $('#tbl_criteria tbody').on('click', 'tr', function () {
        var table = $('#tbl_criteria').DataTable();
        var d = table.row(this).data();
        console.log(d);
        $("#CriteriaHeaderID").val(d.ID);
        Initializepage_criteriaDetails(d.ID);
    });



}
var chosenitem = [];
var chosenitem_details = [];
function Addcriteria(data) {
    var datanow = data.serialize() + '&WorkCategory=' + $('#WorkCategory').val();
    $.ajax({
        url: '../Criteria/CreateCriteria',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#CriteriaForm').trigger("reset");
             
                tablecriteria.ajax.reload();
                view_state_criteria('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("criteria Already Exist");
            }

        }
    });
}

function Editcriteria(data) {
    var datanow = data.serialize() + '&ID=' + $('#ID').val();
    $.ajax({
        url: '../Criteria/UpdateCriteria',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#CriteriaForm').trigger("reset");

                tablecriteria.ajax.reload();
                view_state_criteria('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("criteria Already Exist");
            }

        }
    });
}

function Deletecriteria() {

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
                url: '../Criteria/DeleteCriteria',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#CriteriaForm').trigger("reset");

                        tablecriteria.ajax.reload();
                        view_state_criteria('view')
                        swal("Data has been saved.", "success");
                    }
                    else {
                        swal("criteria Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_criteria(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate_criteria(true);

            $('#btn_add_criteria').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_criteria_div').show();
            $('#btn_save_criteria_div').hide();
            $('#btn_clear_criteria_div').hide();
            $('#btn_cancel_criteria_div').hide();
            break;
        case 'create':
            inputSate_criteria(false);

            $('#btn_save_criteria').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_criteria_div').hide();
            $('#btn_save_criteria_div').show();
            $('#btn_clear_criteria_div').show();
            $('#btn_cancel_criteria_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate_criteria(true);

            $('#btn_add_criteria').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_criteria_div').show();
            $('#btn_save_criteria_div').hide();
            $('#btn_clear_criteria_div').hide();
            $('#btn_cancel_criteria_div').show();
            break;
        case 'update':
            inputSate_criteria(false);

            $('#btn_save_criteria').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_criteria_div').hide();
            $('#btn_save_criteria_div').show();
            $('#btn_clear_criteria_div').hide();
            $('#btn_cancel_criteria_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}

function inputSate_criteria(state) {
    var input = $('.input_state_criteria');

    for (let index = 0; index < input.length; index++) {
        var type = $(input[index]).prop('nodeName');

        if (type == 'SELECT') {
            $(input[index]).prop('disabled', state);
        } else {
            $(input[index]).prop('readonly', state);
        }
    }
}



//FOR CRITERIA DETAILS


function Initializepage_criteriaDetails(criteriaID) {

    tablecriteriadetails = $('#tbl_detail').DataTable({
        ajax: {
            url: '../Criteria/GetCriteriaDetailsList',
            data: {
                WorkCategory: $("#WorkCategory").val(),
                CriteriaHeaderID: criteriaID,
            },
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_criteria_details'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "CriteriaName", name: "CriteriaName" },
            { data: "DetailName", name: "DetailName" },
            { data: "DetailPoints", name: "DetailPoints" },
            { data: "DetailOrder", name: "DetailOrder" },
            
        ],

    });
    $('#tbl_detail tbody').on('click', '.checkitems', function () {
        if (this.checked) {
            chosenitem_details.push(this.id);
            $(this).prop('checked', true);
        }
        else {
            //chosenitem.remove($(this).id);
            chosenitem_details = chosenitem_details.filter(e => e !== this.id);
            $(this).prop('checked', false);
        }
    })
    $('#tbl_detail tbody').on('click', '.btn_edit_criteria_details', function () {
        var data = tablecriteriadetails.row($(this).parents('tr')).data();

        view_state_criteria_details('show');

        $('#ID_details').val(data.ID);
        $('#CriteriaHeaderID').val(data.CriteriaHeaderID);
        $('#DetailName').val(data.DetailName);
        $('#DetailPoints').val(data.DetailPoints);
        $('#DetailOrder').val(data.DetailOrder);

    });



}


function Addcriteria_details(data) {
    var datanow = data.serialize() + '&ID=' + $('#ID_details').val() + '&CriteriaHeaderID=' + $('#CriteriaHeaderID').val(); 
    $.ajax({
        url: '../Criteria/CreateCriteriaDetails',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#CriteriaDetailsForm').trigger("reset");

                tablecriteriadetails.ajax.reload();
                view_state_criteria_details('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("criteria Already Exist");
            }

        }
    });
}

function Editcriteria_details(data) {
    var datanow = data.serialize() + '&ID=' + $('#ID_details').val();
    $.ajax({
        url: '../Criteria/UpdateCriteriaDetails',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#CriteriaDetailsForm').trigger("reset");

                tablecriteriadetails.ajax.reload();
                view_state_criteria_details('view')
                swal("Data has been saved.", "success");
            }
            else {
                swal("criteria Already Exist");
            }

        }
    });
}

function Deletecriteria_details() {

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
                url: '../Criteria/DeleteCriteriaDetails',
                data: JSON.stringify({
                    datalist: chosenitem_details,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#CriteriaDetailsForm').trigger("reset");

                        tablecriteriadetails.ajax.reload();
                        view_state_criteria_details('view')
                        swal("Data has been saved.", "success");
                    }
                    else {
                        swal("criteria Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_criteria_details(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate_criteria_details(true);

            $('#btn_add_criteria_details').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_criteria_details_div').show();
            $('#btn_save_criteria_details_div').hide();
            $('#btn_clear_criteria_details_div').hide();
            $('#btn_cancel_criteria_details_div').hide();
            break;
        case 'create':
            inputSate_criteria_details(false);

            $('#btn_save_criteria_details').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_criteria_details_div').hide();
            $('#btn_save_criteria_details_div').show();
            $('#btn_clear_criteria_details_div').show();
            $('#btn_cancel_criteria_details_div').show();

            $('.usr_ctrl_details:first').focus();
            break;
        case 'show':
            inputSate_criteria_details(true);

            $('#btn_add_criteria_details').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_criteria_details_div').show();
            $('#btn_save_criteria_details_div').hide();
            $('#btn_clear_criteria_details_div').hide();
            $('#btn_cancel_criteria_details_div').show();
            break;
        case 'update':
            inputSate_criteria_details(false);

            $('#btn_save_criteria_details').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_criteria_details_div').hide();
            $('#btn_save_criteria_details_div').show();
            $('#btn_clear_criteria_details_div').hide();
            $('#btn_cancel_criteria_details_div').show();

            $('.usr_ctrl_details:first').focus();
            break;
        default:
            break;
    }

}

function inputSate_criteria_details(state) {
    var input = $('.input_state_criteria_details');

    for (let index = 0; index < input.length; index++) {
        var type = $(input[index]).prop('nodeName');

        if (type == 'SELECT') {
            $(input[index]).prop('disabled', state);
        } else {
            $(input[index]).prop('readonly', state);
        }
    }
}