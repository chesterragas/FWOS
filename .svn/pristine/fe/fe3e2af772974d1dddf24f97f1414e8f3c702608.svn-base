$(function () {
    view_state_holidays('view')
    $('#btn_add_holidays').on('click', function () {
        if ($('#ID').val() == "") {
            view_state_holidays('create');
        } else {
            view_state_holidays('update');
        }
    });

    $('#btn_clear_holidays').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_holidays').on('click', function () {
        view_state_holidays('view');
    });

    $('#btn_delete_holidays').on('click', function () {
        Deleteholidays();
    })

    $("#holidaysForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#ID').val() == "") {
            Addholidays($(this));
        } else {
            Editholidays($(this));
        }
    });

    $("#checkall_holidays").on("change", function () {
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
    $("#Type").val("Holidays");

    $('#holidaytab').on('shown.bs.tab', function (event) {
        var x = $(event.target)[0].id;
        $("#Type").val(x);
        switch (x) {
            case "Holidays":
                $(".Holi").text("Normal Holiday")
                Initializepage_holidays();
                break;
            case "Companyholidays":
                $(".Holi").text("Company Holiday")
                Initializepage_companyholidays();
                break;
        }
        
      
    });

    Initializepage_holidays();

});
var tableholidays;
function Initializepage_holidays() {

    tableholidays = $('#tbl_holidays').DataTable({
        ajax: {
            url: '../Holidays/GetholidaysList',
            type: "POST",
            datatype: "json",
            data: { Type: $("#Type").val()}
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
        initComplete() {

            $(".Holi").text("Normal Holiday")
        },
        columns: [

            {
                data: function (x) {
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkitems' />";
                }, sortable: false, searchable: false
            },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_holidays'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "HolidayName", name: "HolidayName", className:"Holi" },
            {
                data: function (x) {
                    return (x.HolidayDate != null) ? moment(x.HolidayDate).format('MMM d') : ""
                }, name: "HolidayDate"
            },



        ],

    });
    $('#tbl_holidays tbody').on('click', '.checkitems', function () {
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
    $('#tbl_holidays tbody').on('click', '.btn_edit_holidays', function () {
        var data = tableholidays.row($(this).parents('tr')).data();

        view_state_holidays('show');

        $('#ID').val(data.ID);
        $('#HolidayName').val(data.HolidayName);

    });



}

function Initializepage_companyholidays() {

    tableholidays = $('#tbl_holidays').DataTable({
        ajax: {
            url: '../Holidays/GetholidaysList',
            type: "POST",
            datatype: "json",
            data: { Type: $("#Type").val() }
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_holidays'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "HolidayName", name: "HolidayName" },
            {
                data: function (x) {
                    return (x.HolidayDate != null) ? moment(x.HolidayDate).format('MMM d, YYYY') : ""
                }, name: "HolidayDate"
            },



        ],

    });
    $('#tbl_holidays tbody').on('click', '.checkitems', function () {
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
    $('#tbl_holidays tbody').on('click', '.btn_edit_holidays', function () {
        var data = tableholidays.row($(this).parents('tr')).data();

        view_state_holidays('show');

        $('#ID').val(data.ID);
        $('#HolidayName').val(data.HolidayName);

    });



}
var chosenitem = [];
function Addholidays(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Holidays/Createholidays',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#holidaysForm').trigger("reset");
                tableholidays.ajax.reload();
                view_state_holidays('view');

            }
            else {
                swal("holidays Already Exist");
            }

        }
    });
}

function Editholidays(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Holidays/Updateholidays',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#holidaysForm').trigger("reset");
                tableholidays.ajax.reload();
                view_state_holidays('view');
            }
            else {
                swal("holidays Already Exist");
            }

        }
    });
}

function Deleteholidays() {

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
                url: '../Holidays/Deleteholidays',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#holidaysForm').trigger("reset");
                        tableholidays.ajax.reload();
                        view_state_holidays('view');
                    }
                    else {
                        swal("holidays Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_holidays(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_holidays').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_holidays_div').show();
            $('#btn_save_holidays_div').hide();
            $('#btn_clear_holidays_div').hide();
            $('#btn_cancel_holidays_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_holidays').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_holidays_div').hide();
            $('#btn_save_holidays_div').show();
            $('#btn_clear_holidays_div').show();
            $('#btn_cancel_holidays_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_holidays').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_holidays_div').show();
            $('#btn_save_holidays_div').hide();
            $('#btn_clear_holidays_div').hide();
            $('#btn_cancel_holidays_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_holidays').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_holidays_div').hide();
            $('#btn_save_holidays_div').show();
            $('#btn_clear_holidays_div').hide();
            $('#btn_cancel_holidays_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}
