$(function () {
    view_state_timeframe('view')
    $('#btn_add_timeframe').on('click', function () {
        if ($('#TimeFrameID').val() == "" || $('#TimeFrameID').val() == null) {
            view_state_timeframe('create');
        } else {
            view_state_timeframe('update');
        }
    });

    $('#btn_clear_timeframe').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_timeframe').on('click', function () {
        view_state_timeframe('view');
    });

    $('#btn_delete_timeframe').on('click', function () {
        DeleteTimeFrame();
    })

    $("#TimeFrameForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#TimeFrameID').val() == "" || $('#TimeFrameID').val() == null) {
            Addtimeframe($(this));
        } else {
            Edittimeframe($(this));
        }
    });

    $("#checkall_timeframe").on("change", function () {
        if (this.checked) {
            var data = table.row($(this).parents('tr')).data();
            $('.checkitems').each(function (i, obj) {
                chosenitemTimeFrom.push(obj.id);
            });
            $('.checkitems').prop('checked', true);
        }
        else {
            $('.checkitems').each(function (i, obj) {
                chosenitemTimeFrom = chosenitemTimeFrom.filter(e => e !== obj.id);
                // chosenitemTimeFrom.remove(obj.id);
            }); $('.checkitems').prop('checked', false);
        }
    })
    Initializepage_timeframe();

});
var tabletimeframe;
var chosenitemTimeFrom;
function Initializepage_timeframe() {

    tabletimeframe = $('#tbl_timeframe').DataTable({
        ajax: {
            url: '../Criteria/GetTimeFrameList',
            data: { WorkCategory: $("#WorkCategory").val() },
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_timeframe'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "WorkCategory", name: "WorkCategory" },
            { data: "WorkingDays", name: "WorkingDays" },
            { data: "PriorityLevel", name: "PriorityLevel" },
            { data: "ScoreFrom", name: "ScoreFrom" },
            { data: "ScoreTo", name: "ScoreTo" },
            { data: "PriorityLevelRemarks", name: "PriorityLevelRemarks" },



        ],

    });
    $('#tbl_timeframe tbody').on('click', '.checkitems', function () {
        if (this.checked) {
            chosenitemTimeFrom.push(this.id);
            $(this).prop('checked', true);
        }
        else {
            //chosenitemTimeFrom.remove($(this).id);
            chosenitemTimeFrom = chosenitemTimeFrom.filter(e => e !== this.id);
            $(this).prop('checked', false);
        }
    })
    $('#tbl_timeframe tbody').on('click', '.btn_edit_timeframe', function () {
        var data = tabletimeframe.row($(this).parents('tr')).data();

        view_state_timeframe('show');

        $('#TimeFrameID').val(data.ID);
        $('#WorkingDays').val(data.WorkingDays);
        $('#PriorityLevel').val(data.PriorityLevel);
        $('#ScoreFrom').val(data.ScoreFrom);
        $('#ScoreTo').val(data.ScoreTo);
        $('#PriorityLevelRemarks').val(data.PriorityLevelRemarks);

    });



}
var chosenitemTimeFrom = [];
function Addtimeframe(data) {
    var datanow = data.serialize() + '&WorkCategory=' + $('#WorkCategory').val();
    $.ajax({
        url: '../Criteria/CreateTimeFrame',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#timeframeForm').trigger("reset");
                tabletimeframe.ajax.reload();
                view_state_timeframe('view');

            }
            else {
                swal("timeframe Already Exist");
            }

        }
    });
}

function Edittimeframe(data) {
    var datanow = data.serialize() + '&ID=' + $('#TimeFrameID').val();
    $.ajax({
        url: '../Criteria/UpdateTimeFrame',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#timeframeForm').trigger("reset");
                tabletimeframe.ajax.reload();
                view_state_timeframe('view');
            }
            else {
                swal("timeframe Already Exist");
            }

        }
    });
}

function DeleteTimeFrame() {

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
                url: '../Criteria/DeleteTimeFrame',
                data: JSON.stringify({
                    datalist: chosenitemTimeFrom,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#timeframeForm').trigger("reset");
                        tabletimeframe.ajax.reload();
                        view_state_timeframe('view');
                    }
                    else {
                        swal("timeframe Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_timeframe(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_timeframe').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_timeframe_div').show();
            $('#btn_save_timeframe_div').hide();
            $('#btn_clear_timeframe_div').hide();
            $('#btn_cancel_timeframe_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_timeframe').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_timeframe_div').hide();
            $('#btn_save_timeframe_div').show();
            $('#btn_clear_timeframe_div').show();
            $('#btn_cancel_timeframe_div').show();

            $('.usr_ctrl_timeframe:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_timeframe').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_timeframe_div').show();
            $('#btn_save_timeframe_div').hide();
            $('#btn_clear_timeframe_div').hide();
            $('#btn_cancel_timeframe_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_timeframe').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_timeframe_div').hide();
            $('#btn_save_timeframe_div').show();
            $('#btn_clear_timeframe_div').hide();
            $('#btn_cancel_timeframe_div').show();

            $('.usr_ctrl_timeframe:first').focus();
            break;
        default:
            break;
    }

}
