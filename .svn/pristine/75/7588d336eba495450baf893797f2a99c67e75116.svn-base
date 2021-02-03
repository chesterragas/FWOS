$(function () {
    Dropdown_select('DivisionID', "/Helper/GetDropdown_Division");

    $("#DivisionID").on("change", function () {

        Dropdown_select('DepartmentID', "/Helper/GetDropdown_Department?DivisionID=" + $("#DivisionID").val());
    });

    $("#DepartmentID").on("change", function () {

        Dropdown_select('SectionID', "/Helper/GetDropdown_Section?DepartmentID=" + $("#DepartmentID").val());
    });
    Initializepage();

    $('body').on('keydown', '.usr_ctrl', function (e) {

        var self = $(this)
            , form = self.parents('form:eq(0)')
            , focusable
            , next
            ;
        if (e.keyCode == 40) {
            focusable = form.find('.usr_ctrl').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);

            if (next.is(":disabled")) {
                next = focusable.eq(focusable.index(this) + 2);
            }

            if (next.length) {
                next.focus();
            }
            return false;
        }

        if (e.keyCode == 38) {
            focusable = form.find('.usr_ctrl').filter(':visible');
            next = focusable.eq(focusable.index(this) - 1);

            if (next.is(":disabled")) {
                next = focusable.eq(focusable.index(this) - 2);
            }

            if (next.length) {
                next.focus();
            }
            return false;
        }

        if (e.keyCode === 13) {
            focusable = form.find('.usr_ctrl').filter(':visible');
            next = focusable.eq(focusable.index(this));

            if (next.length) {
                switch (e.target.type) {
                    case "submit":
                        next.form.submit();
                        break;
                    default:
                        next.click();
                }
                next.focus();
            }
            return false;
        }

    });


    $('#UsersForm').on('submit', function (e) {
        e.preventDefault();

        if ($('#ID').val() == "") {
            AddUsers($(this));
        }
        else {
            EditUser($(this));
        }
    });

    $('#btn_add').on('click', function () {
        if ($('#ID').val() == "") {
            view_state('create');
        } else {
            view_state('update');
        }
    });

    $('#btn_clear').on('click', function () {
        clear();
        $('.usr_ctrl:first').focus();
    });

    $('#btn_cancel').on('click', function () {
        view_state('view');
    });

    $('#btn_delete').on('click', function () {
        DeleteUsers();
    });
    

    
    $("#checkall_emp").on("change", function () {
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

    $("#btn_save_PageAccess").on("click", SavePageAccess);

    $("#btn_approved").one("click", function () {
      
        $.ajax({
            url: '../Users/ApprovedUsers',
            data: JSON.stringify({
                datalist: chosenitem
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.msg == "Success") {
                    var tabledata = $('#UsersTable').DataTable();
                    $('#UsersForm').trigger("reset");
                    Initializepage();
                    chosenitem = [];
                    swal("Approve user successful");
                }
                else {
                    swal("Users Already Exist");
                }

            }
        });
    });

    $("#btn_reject").one("click", function () {

        $.ajax({
            url: '../Users/RejectUsers',
            data: JSON.stringify({
                datalist: chosenitem
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.msg == "Success") {
                    var tabledata = $('#UsersTable').DataTable();
                    $('#UsersForm').trigger("reset");
                    Initializepage();
                    chosenitem = [];
                }
                else {
                    swal("Users Already Exist");
                }

            }
        });
    });
    

    
});
var chosenitem = [];
function Initializepage() {
    GetUser();
    view_state('view');
}
var table;
var currentuser;
function view_state(state) {
    switch (state) {
        case 'view':
            clear();
            inputSate(true);

            $('#btn_add').html('<i class="fa fa-plus"></i> Add New');

            $('#btn_add_div').show();
            $('#btn_save_div').hide();
            $('#btn_clear_div').hide();
            $('#btn_cancel_div').hide();
            break;
        case 'create':
            inputSate(false);

            $('#btn_save').html('<i class="fa fa-save"></i> Save');

            $('#btn_add_div').hide();
            $('#btn_save_div').show();
            $('#btn_clear_div').show();
            $('#btn_cancel_div').show();

            $('.usr_ctrl:first').focus();
            break;
        case 'show':
            inputSate(true);

            $('#btn_add').html('<i class="fa fa-edit"></i> Edit');

            $('#btn_add_div').show();
            $('#btn_save_div').hide();
            $('#btn_clear_div').hide();
            $('#btn_cancel_div').show();
            break;
        case 'update':
            inputSate(false);

            $('#btn_save').html('<i class="fa fa-pencil"></i> Update');

            $('#btn_add_div').hide();
            $('#btn_save_div').show();
            $('#btn_clear_div').hide();
            $('#btn_cancel_div').show();

            $('.usr_ctrl:first').focus();
            break;
        default:
            break;
    }

}
function GetUser() {
        table = $('#UsersTable').DataTable({
        ajax: {
            url: '../Users/GetUsersList',
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
        createdRow: function (row, data, dataIndex) {
            if (data.IsApproved == 0) {
                $(row).addClass("warning");
            }
            if (data.IsApproved == 1) {
                $(row).addClass("success");
            }
            if (data.IsApproved == 2) {
                $(row).addClass("danger");
            }
            
           
        },
        columns: [
          
            {
                data: function (x) {
                    return "<input type='checkbox' id='" + x.ID +"' class='filled-in chk-col-light-blue checkitems' />";
                }, sortable: false, searchable: false
            },
             {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_user'>"+
                                    "<i class='fa fa-edit'></i>"+
                            "</button>";
                }, sortable: false, searchable: false
            },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btnPageAccess'>" +
                        "<i class='fa fa-universal-access' ></i> Page Access" +
                        "</button>"



                }, width: "11.11%"
            },
            { data: "EmployeeNo", name: "EmployeeNo" },
            { data: "FirstName", name: "FirstName" },
            { data: "LastName", name: "LastName" },
            { data: "Email", name: "Email" },
            { data: "MobileNo", name: "Mobile No" },
            { data: "LocalNo", name: "Local No" },
            { data: "Division", name: "Division" },
            { data: "Department", name: "Department" },
            { data: "Section", name: "Section" },
            //{ data: "CanReceive", name: "CanReceive" },
            
            {
                data: function (x) {

                    if (x.CanReceive) {
                        return "<button type='button' class='btn btn-xs btn-flat btn-primary'>" +
                            "<i class='fa fa-check ' ></i>" +
                            "</button>"
                    }
                    else {
                        return "<button type='button' class='btn btn-xs btn-flat btn-danger'>" +
                                "<i class='fa fa-times'></i>" +
                                "</button>"
                    }

                   
                }, name: "CanReceive"
            },
           
        ],

    });

    $('#UsersTable tbody').on('click', '.checkitems', function () {
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

    $('#UsersTable tbody').on('click', '.btn_edit_user', function () {
        var data = table.row($(this).parents('tr')).data();

        view_state('show');

        $('#ID').val(data.UserName);
        $('#EmployeeNo').val(data.EmployeeNo);
        $('#Password').val(data.Password);
        $('#FirstName').val(data.FirstName);
        $('#LastName').val(data.LastName);
        $('#Email').val(data.Email);
        $('#MobileNo').val(data.MobileNo);
        $('#LocalNo').val(data.LocalNo);
        var CanReceive = (data.CanReceive == true) ? "true" : "false";
        $('#CanReceive').val(CanReceive);
        $('#Division').val(data.Division);
        $('#Section').val(data.Section);
        $('#Department').val(data.Department);
    });

    $('#UsersTable tbody').on('click', '.btn_delete_user', function () {
        var data = table.row($(this).parents('tr')).data();

        swal({
            title: "Are you sure?",
            text: "This will delete "+data.UserName+"!",
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
                DeleteUser(data);
                swal("Deleted!", data.UserName + "'s account has been deleted.", "success");
            } else {
                swal("Cancelled", data.UserName + "'s account details is safe.", "error");
            }
        });
    });

    $('#UsersTable tbody').on('click', '.btnPageAccess', function () {
        $('#ModalPageAccess').modal({
            backdrop: 'static',
            keyboard: false
        })
        $("#ModalPageAccess").modal("show");
        var tabledata = $('#UsersTable').DataTable();
        var data = tabledata.row($(this).parents('tr')).data();
        currentuser = data.EmployeeNo;
        $.ajax({
            url: '../Users/GetPageAccess',
            data: { EmployeeNo: data.EmployeeNo },
            type: 'POST',
            datatype: "json",
            success: function (returnData) {
                GoodCount = returnData.GoodCount;
                SaveGoodCount = 0;
                //--Master
                $('#tbl_PageAccess_Master').DataTable({
                    destroy: true,
                    searching: false,
                    paging: false,
                    data: returnData.MasterPageList,
                    columns: [
                        { data: "PageName" },
                        {
                            data: function (data, type, row, meta) {
                                var checked = (data.AccessType == true) ? ' checked ' : '';
                                return " <input type='checkbox' id=Master_" + data.ID + " class='mastermod filled-in chk-col-light-blue' " + checked + " name=PageView_" + data.ID + "/>" +
                                    " <label class=checker for=Master_" + data.ID + "></label>"

                            }, orderable: false, searchable: false
                        },
                    ]
                });

                //--END of Master

                //--Transaction
                $('#tbl_PageAccess_Transaction').DataTable({
                    destroy: true,
                    searching: false,
                    paging: false,
                    data: returnData.TransactionPageList,
                    columns: [
                        { data: "PageName" },
                        {
                            data: function (data, type, row, meta) {
                                var checked = (data.AccessType == true) ? ' checked ' : '';
                                return " <input type='checkbox' id=Transaction_" + data.ID + " class='mastermod filled-in chk-col-light-blue' " + checked + " name=PageView_" + data.ID + "/>" +
                                    " <label class=checker for=Transaction_" + data.ID + "></label>"

                            }, orderable: false, searchable: false
                        },
                    ]
                });

                //--END of Transaction
                
            }
        });
    });
}

function AddUsers(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Users/CreateUsers',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                var tabledata = $('#UsersTable').DataTable();
                $('#UsersForm').trigger("reset");
                Initializepage();
            }
            else {
                swal("Users Already Exist");
            }

        }
    });
}

function EditUser(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Users/UpdateUsers',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                var tabledata = $('#UsersTable').DataTable();
                $('#UsersForm').trigger("reset");
                Initializepage();
            }
            else {
                swal("Users Already Exist");
            }

        }
    });
}

function DeleteUsers() {
    
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
                url: '../Users/DeleteUsers',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),

                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        var tabledata = $('#UsersTable').DataTable();
                        $('#UsersForm').trigger("reset");
                        Initializepage();
                        swal("Deleted!", data.UserName + "'s account has been deleted.", "success");
                    }
                    else {
                        swal("Users Already Exist");
                    }

                }
            });
           
        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });
    
   

}

function SavePageAccess() {
    $("#loading_modal").modal("show")

    var table = $('#tbl_PageAccess_Master').DataTable();
    var pagelist = [];
    table.rows().every(function () {
        var d = this.data();
        d.counter++;
        var item = {
            EmployeeNo: currentuser,
            PageID: d.ID,
            PageAccess: ($('#Master_' + d.ID).is(":checked")) ? true : false
        }
        pagelist.push(item);
    });

    table = $('#tbl_PageAccess_Transaction').DataTable();
    table.rows().every(function () {
        var d = this.data();
        d.counter++;
        var item = {
            EmployeeNo: currentuser,
            PageID: d.ID,
            PageAccess: ($('#Transaction_' + d.ID).is(":checked")) ? true : false
        }
        pagelist.push(item);
    });
   
    $.ajax({
        url: '../Users/UpdatePageAccess',
        type: 'POST',
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            PA_userpage: pagelist,
        }),
        success: function (returnData) {
            if (returnData.msg == "Success") {
                $("#loading_modal").modal("hide")

                var tabledata = $('#UsersTable').DataTable();
                var info = tabledata.page.info();
                pagecount = pagecount + (info.page * 10);
                Initializepage();
                notify("Saved!", "Successfully Saved", "success");

            }
            else {

            }

        }
    });



}