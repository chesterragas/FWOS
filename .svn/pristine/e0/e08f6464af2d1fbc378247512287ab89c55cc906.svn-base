$(function () {
    Dropdown_select('DivisionID', "/Helper/GetDropdown_Division");
    Dropdown_select('DepartmentID', "/Helper/GetDropdown_Department?approverID=" + "");
    Dropdown_select('SectionID', "/Helper/GetDropdown_Section?DepartmentID=" + "");
    $("#DivisionID").on("change", function () {
        Dropdown_select('DepartmentID', "/Helper/GetDropdown_Department?DivisionID=" + $("#DivisionID").val());
    });
    $("#DepartmentID").on("change", function () {

        Dropdown_select('SectionID', "/Helper/GetDropdown_Section?DepartmentID=" + $("#DepartmentID").val());
    });
    Dropdown_select('EmployeeNo', "/Helper/GetDropdown_Users");

    $("#EmployeeNo").on("change", function () {
        $.ajax({
            url: '../ApproverSequence/GetPosition',
            data: { UserID: $("#EmployeeNo").val() },
            type: 'GET',
            datatype: "json",
            success: function (returnData) {
                $("#Position").val(returnData.position);
            }
        });
    });

    view_state_approver('view')
    $('#btn_add_approver').on('click', function () {
        if ($('#ApprovalID').val() == "" || $('#ApprovalID').val() == null) {
            view_state_approver('create');
        } else {
            view_state_approver('update');
        }
    });

    $('#btn_clear_approver').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel_approver').on('click', function () {
        view_state_approver('view');
    });

    $('#btn_delete_approver').on('click', function () {
        Deleteapprover();
    })

    $("#ApprovalForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#ApprovalID').val() == "" || $('#ApprovalID').val() == null) {
            Addapprover($(this));
        } else {
            Editapprover($(this));
        }
    });

    $("#checkall_approver").on("change", function () {
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
    $('.default-select2').select2({
        dropdownParent: $('#ApprovalModal'),
        width: '100%'
    });
    //$(".default-select2").select2();
    Initializepage_approver();
    Initializepage();
});
var tableapprover;

var chosenitem = [];


function Initializepage() {

    tableapprover = $('#ApproversDetailsTable').DataTable({
        ajax: {
            url: '../ApproverSequence/GetApproverSequenceList',
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
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_approver'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false
            },
            { data: "DivisionName", name: "DivisionName" },
            { data: "DepartmentName", name: "DepartmentName" },
            { data: "SectionName", name: "SectionName" },
            { data: "EmployeeNo", name: "EmployeeNo" },
            { data: "Position", name: "Position" },
            { data: "OrderNo", name: "OrderNo" },
            {
                data: function (x) {
                    return (x.MainBackupApprover)?"Main":"Sub";
                }, sortable: false, searchable: false
            },



        ],

    });
    $('#ApproversDetailsTable tbody').on('click', '.checkitems', function () {
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
    $('#ApproversDetailsTable tbody').on('click', '.btn_edit_approver', function () {
        var data = tableapprover.row($(this).parents('tr')).data();

        view_state_approver('show');

        $('#ApprovalID').val(data.ID);
        $('#DivisionID').val(data.DivisionID);
        $('#DepartmentID').val(data.DepartmentID);
        $('#SectionID').val(data.SectionID);
        $('#EmployeeNo').val(data.EmployeeNo);
        $('#OrderNo').val(data.OrderNo);
        data.MainBackupApprover = (data.MainBackupApprover == true) ? 1 : 0;
        $('#MainBackupApprover').val(data.MainBackupApprover);
        $('#Position').val(data.Position);
        $("#ApprovalModal").modal("show");
    });



}


function Addapprover(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../ApproverSequence/CreateApprover',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#ApprovalForm').trigger("reset");
                tableapprover.ajax.reload();
                view_state_approver('view');
                $("#ApprovalModal").modal("hide");
            }
            else {
                swal("approver Already Exist");
            }

        }
    });
}

function Editapprover(data) {
    var datanow = data.serialize() + '&ID=' + $('#ApprovalID').val();
    $.ajax({
        url: '../ApproverSequence/UpdateApproverSequence',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $('#ApprovalForm').trigger("reset");
                tableapprover.ajax.reload();
                view_state_approver('view');
                $("#ApprovalModal").modal("hide");
            }
            else {
                swal("approver Already Exist");
            }

        }
    });
}

function Deleteapprover() {

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
                url: '../ApproverSequence/DeleteApproverSequence',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),
                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        $('#ApprovalForm').trigger("reset");
                        tableapprover.ajax.reload();
                        view_state_approver('view');
                        $("#ApprovalModal").modal("hide");
                    }
                    else {
                        swal("approver Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}

function view_state_approver(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add_approver').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_approver_div').show();
            $('#btn_save_approver_div').hide();
            $('#btn_clear_approver_div').hide();
            $('#btn_cancel_approver_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save_approver').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_approver_div').hide();
            $('#btn_save_approver_div').show();
            $('#btn_clear_approver_div').show();
            $('#btn_cancel_approver_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add_approver').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_approver_div').show();
            $('#btn_save_approver_div').hide();
            $('#btn_clear_approver_div').hide();
            $('#btn_cancel_approver_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save_approver').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_approver_div').hide();
            $('#btn_save_approver_div').show();
            $('#btn_clear_approver_div').hide();
            $('#btn_cancel_approver_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}



function Initializepage_approver() {
   
    $.ajax({
        url: '/ApproverSequence/GetDivisionList',
        type: 'GET',
        datatype: "json",
        success: function (returnData) {
            var theLineView = "";

            $.each(returnData.data, function (index, val) {
                theLineView += "<div id='approverApp" + val.ID + "' class='card-accordion'>" +
                    "            <div class='card'>" +
                    "               <div class='card-header bg-black text-white pointer-cursor' data-toggle='collapse' data-target='#collapseOne" + val.ID + "'>" +
                    //"                    " + val.approverName + "" +
                    "<div class='row'>" +
                    "<div class='col-6'>" +
                    "    " + val.DivisionName + "" +
                    "</div>" +
                    "<div class='col-md-3 offset-3'>" +
                    "    <button type='button' class='btn btn-flat btn-success btn-sm btn-block usr_ctrl addApprover' id='addbtn' onclick=ShowApproverModal("+val.ID+",'','')>" +
                    "        <i class='fa fa-add'></i> Add" +
                    "    </button>" +
                    "</div>" +
                    "</div>" +
                    "                </div>" +
                   
                    "                <div id='collapseOne" + val.ID + "' class='collapse' data-parent='#approverApp" + val.ID + "'>" +
                    "                    <div class='card-body'>";

                    $.each(val.DepartmentList, function (index, val2) {
                        theLineView += "            <div id='DepartmentApp" + val2.ID + "' class='card-accordion'>" +
                            "                            <div class='card'>" +
                            "                                <div class='card-header bg-black text-white pointer-cursor' data-toggle='collapse' data-target='#collapseOne2" + val2.ID + "'>" +
                            //"                                    " + val2.DepartmentName + "" +
                                                                "<div class='row'>" +
                                                                "<div class='col-6'>" +
                                                                "    " + val2.DepartmentName + "" +
                                                                "</div>" +
                                                                "<div class='col-md-3 offset-3'>" +
                            "    <button type='button' class='btn btn-flat btn-success btn-sm btn-block usr_ctrl addApprover' id='addbtn' onclick=ShowApproverModal(" + val.ID + "," + val2.ID + ",'')>" +
                                                                "        <i class='fa fa-add'></i> Add" +
                                                                "    </button>" +
                                                                "</div>" +
                                                                "</div>" +
                            "                                </div>" +
                            "                                <div id='collapseOne2" + val2.ID + "' class='collapse' data-parent='#DepartmentApp" + val2.ID + "'>" +
                            "                                    <div class='card-body'>";


                            $.each(val2.SectionList, function (index, val3) {
                                theLineView += "                                           <div id='SectionApp" + val3.ID + "'>" +
                                                "                                            <div class='card'>" +
                                                "                                                <div class='card-header bg-black text-white pointer-cursor'>" +
                                                //"                                                    " + val3.SectionName+"" +
                                                                                                "<div class='row'>" +
                                                                                                "<div class='col-6'>" +
                                                                                                "    " + val3.SectionName + "" +
                                                                                                "</div>" +
                                                                                                "<div class='col-md-3 offset-3'>" +
                                    "    <button type='button' class='btn btn-flat btn-success btn-sm btn-block usr_ctrl addApprover' id='addbtn' onclick=ShowApproverModal(" + val.ID + "," + val2.ID + ","+val3.ID+")>" +
                                                                                                "        <i class='fa fa-add'></i> Add" +
                                                                                                "    </button>" +
                                                                                                "</div>" +
                                                                                                "</div>" +
                                                "                                                </div>" +
                                                "                                            </div>" +
                                                "                                        </div>";
                            });



                        theLineView += "                                    </div>" +
                            "                                </div>" +
                            "                            </div>" +
                            "                        </div>"

                    });
                 theLineView+=   "                    </div>" +
                                "                </div>" +
                                "            </div>" +
                                "</div>";
            });

           

            $("#Approverlist").html("");

            $("#Approverlist").html(theLineView);

            //$(".addApprover").on("click", function () {
            //    $('#ApprovalForm').trigger("reset");
            //    $("#ApprovalModal").modal("show");
            //});
        }
    });

       
}   

function ShowApproverModal(DivisionID,DepartmentID,SectionID) {
    $("#ApprovalModal").modal("show");
    $("#DivisionID").val(DivisionID);
    $("#DepartmentID").val(DepartmentID);
    $("#SectionID").val(SectionID);
    
}