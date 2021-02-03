﻿$(function () {
    Initializepage();
    Initializepage_approved();
    Initializepage_ongoing();
    $("#btnApproveRequest").on("click", UpdateWOStatus);
    $("#btnRejectRequest").on("click", UpdateWOStatus_reject);
});

var chosenWO;
var table;
var tablerejected;
function Initializepage() {
    //view_state('view');
        table = $('#tbl_WorkOrder').DataTable({
        ajax: {
            url: '../WorkOrderApproval/GetWorkOrderApprovalList',
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
        createdRow: function (row, data, dataIndex) {
            if (data.RequestAge <= 5 && data.RequestAge >=3) {
                $(row).addClass("warning");
            }
            if (data.RequestAge >= 6) {
                $(row).addClass("danger");
            }
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
                    if (!x.Rejected) {
                        return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_wo_details'>" +
                            "<i class='fa fa-bars'></i>" +
                            "</button>";
                    }
                    else {
                        return "";
                    }
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
        chosenWO = rowdata.ID;
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
                    $("#btnApproveRequest, #btnRejectRequest, #Remarks, #remarkslabel").hide();
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
                        $("#btnApproveRequest, #btnRejectRequest").prop('disabled', false);
                        $("#mdlCheckRequest").modal("show");

                    }

                }
                
            }
        });
    })
}

function Initializepage_approved() {
    //view_state('view');
        tablerejected = $('#tbl_WorkOrder_approved').DataTable({
        ajax: {
            url: '../WorkOrderApproval/GetWorkOrderApprovalList_approved',
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
        createdRow: function (row, data, dataIndex) {
                 if (data.Rejected == true) {
                    $(row).addClass("danger");
                 }
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
                    if (!x.Rejected) {
                        return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_wo_details'>" +
                            "<i class='fa fa-bars'></i>" +
                            "</button>";
                    }
                    else {
                        return "";
                    }
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
        chosenWO = rowdata.ID;
        $.ajax({
            url: '../WorkOrderApproval/GetDetails',
            data: JSON.stringify({
                ID: rowdata.ID
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                $("#Remarks").val("");
                if (returnData.buttonHideShow == "H") {
                    $("#btnApproveRequest, #btnRejectRequest, #Remarks, #remarkslabel").hide();
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
                        $("#btnApproveRequest, #btnRejectRequest").prop('disabled', false);
                        $("#mdlCheckRequest").modal("show");

                    }

                }

            }
        });
    })
}

function Initializepage_ongoing() {
    //view_state('view');
    tablerejected = $('#tbl_WorkOrder_ongoing').DataTable({
        ajax: {
            url: '../WorkOrderApproval/GetWorkOrderApprovalList_ongoing',
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
        createdRow: function (row, data, dataIndex) {
            if (data.Rejected == true) {
                $(row).addClass("danger");
            }
        },
        columns: [
            { data: "ID", name: "ID", visible: false },
          
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
        chosenWO = rowdata.ID;
        $.ajax({
            url: '../WorkOrderApproval/GetDetails',
            data: JSON.stringify({
                ID: rowdata.ID
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                $("#Remarks").val("");
                if (returnData.buttonHideShow == "H") {
                    $("#btnApproveRequest, #btnRejectRequest, #Remarks, #remarkslabel").hide();
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
                        $("#btnApproveRequest, #btnRejectRequest").prop('disabled', false);
                        $("#mdlCheckRequest").modal("show");

                    }

                }

            }
        });
    })
}

function UpdateWOStatus() {
    $("#btnApproveRequest").prop("disabled", true);
    $("#btnRejectRequest").prop("disabled", true);
    //alert(chosenWO);
    $.ajax({
        url: '../WorkOrderApproval/UpdateWOStatus',
        data: {
            ID: chosenWO,
            approvedreject: 1,
            Remarks: ""
        },
        type: 'GET',
        datatype: "json",
        success: function (returnData) {
            $("#mdlCheckRequest").modal("hide");
            table.ajax.reload();
            tablerejected.ajax.reload();
            swal("Request Approved");
        }
    });
}

function UpdateWOStatus_reject() {
  
    if ($("#Remarks").val() != "" && $("#Remarks").val() != null) {
        $("#btnApproveRequest").prop("disabled", true);
        $("#btnRejectRequest").prop("disabled", true);
        $.ajax({
            url: '../WorkOrderApproval/UpdateWOStatus',
            data: {
                ID: chosenWO,
                approvedreject: 2,
                Remarks: $("#Remarks").val()
            },
            type: 'GET',
            datatype: "json",
            success: function (returnData) {
                $("#mdlCheckRequest").modal("hide");
                table.ajax.reload();
                tablerejected.ajax.reload();
                swal("Rejected Request");
            }
        });
    }
    else {
        swal("Please input remarks");
    }
}

