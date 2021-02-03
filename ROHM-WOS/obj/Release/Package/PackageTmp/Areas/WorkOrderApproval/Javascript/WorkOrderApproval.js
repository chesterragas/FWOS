$(function () {
    Initializepage();
});


function Initializepage() {
    //view_state('view');
    var table = $('#tbl_WorkOrder').DataTable({
        ajax: {
            url: '../WorkOrderApproval/GetWorkOrderAppovalList',
            type: "POST",
            datatype: "json",
            //data: { supersection: $("#Section").val() }
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
            { data: "ID", name: "ID", visible: false },
            {
                data: function (x) {
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkitems' />";
                }, sortable: false, searchable: false
            },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_wo_details'>" +
                        "<i class='fa fa-bars'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "Division", name: "Division" },
            { data: "Department", name: "Department" },
            { data: "Section", name: "Section" },
            { data: "Building", name: "Building" },
            { data: "Floor", name: "Floor" },
            { data: "ProcessArea", name: "ProcessArea" },
            { data: "ReferenceNo", name: "ReferenceNo" },
            { data: "WorkOrderNo", name: "WorkOrderNo" },
            { data: "Requestor", name: "Requestor" },
            { data: "RequestAge", name: "RequestAge" },

        ],

    });
    $("#tbl_WorkOrder").on('click', '.btn_wo_details', function () {
        var rowdata = table.rows($(this).closest("tr")).data()[0];
        $.ajax({
            url: '../WorkOrderApproval/GetDetails',
            data: JSON.stringify({
                ID: rowdata.ID
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {

                if (returnData.buttonHideShow == "H") {
                    $("#btnApproveRequest, #btnRejectRequest").hide();
                    $("#mdlCheckRequest").modal("show");

                }
                else {
                    if (returnData.buttonStatus == "D") {
                        $("#btnApproveRequest, #btnRejectRequest").show();
                        $("#btnApproveRequest, #btnRejectRequest").prop('disabled', true);
                        $("#mdlCheckRequest").modal("show");

                    }
                    else {
                        $("#btnApproveRequest, #btnRejectRequest").show();
                        $("#btnApproveRequest, #btnRejectRequest").prop('disabled', true);
                        $("#mdlCheckRequest").modal("show");

                    }

                }
                
            }
        });
    })
}