$(function () {
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
    $('#Suppliers_Form').on('submit', function (e) {
        e.preventDefault();

        if ($('#ID').val() == "") {
            AddSuppliers($(this));
        }
        else {
            EditSuppliers($(this));
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
        DeleteSuppliers();
    });

    $('#tbl_suppliers tbody').on('click', '.open_modal_email', function () {
        $('#modal_emails').modal('show');
    });

    $('#tbl_suppliers tbody').on('click', '.open_modal_tel', function () {
        $('#modal_tels').modal('show');
    });

    $('#tbl_suppliers tbody').on('click', '.open_modal_cel', function () {
        $('#modal_cels').modal('show');
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



    $("#SupplierDetailForm").on("submit", function (e) {
        e.preventDefault();
        if ($('#SupplierDetailID').val() == "") {
            AddSuppliersDetails($(this));
        }
        else {
            EditSuppliersDetails($(this));
        }
    })

    $('#btn_delete_details').on('click', function () {
        DeleteSuppliersDetails();
    });

});
var chosenitem = [];
var chosenitemdetails = [];
function Initializepage() {
    view_state('view');
    GetSupplier();
    //$('#tbl_suppliers').DataTable({
    //    initComplete: function () {
    //        $('.dataTables_length select').addClass('form-control-sm').css('font-sise', '11px');
    //        $('.dataTables_filter input').addClass('form-control-sm').css('font-sise', '11px');
    //    }
    //});
}

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

function GetSupplier() {
    var table = $('#tbl_suppliers').DataTable({
        ajax: {
            url: '../Suppliers/GetSuppliersList',
            type: "POST",
            datatype: "json",
            data: { supersection: $("#Section").val() }
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
                }, sortable: false, searchable: false, 
            },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_user'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false, 
            },
            { data: "SupplierName", name: "SupplierName" },
            { data: "Address", name: "Address" },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_contact'>" +
                            "<i class='fa fa-user-md'> Contact Details </i>" +
                            "</button>";
                }, sortable: false, searchable: false, 
            },
            //{
            //    data: function (x) {
            //        return "<button type='button' class='btn btn-xs btn-flat btn-primary'>" +
            //            "<i class='fa fa-user-md'> Telephone No </i>" +
            //            "</button>";
            //    }, sortable: false, searchable: false, 
            //},
           
        ],

    });
    $('#tbl_suppliers tbody').on('change', '.checkitems', function () {
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
    $('#tbl_suppliers tbody').on('click', '.btn_edit_user', function () {
        var data = table.row($(this).parents('tr')).data();

        view_state('show');

        $('#ID').val(data.ID);
        $('#SupplierName').val(data.SupplierName);
        $('#Address').val(data.Address);
       
    });

    $('#tbl_suppliers tbody').on('click', '.btn_contact', function () {
        var data = table.row($(this).parents('tr')).data();
        $("#ID").val(data.ID);
        ShowContactDetails(data.ID, data.SupplierName);
    });

  
}

function AddSuppliers(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Suppliers/CreateSupplier',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                var tabledata = $('#tbl_suppliers').DataTable();
                $('#Suppliers_Form').trigger("reset");
                tabledata.ajax.reload();
                swal("Data has been saved.", "success");
            }
            else {
                swal("Supplier Already Exist");
            }

        }
    });
}

function EditSuppliers(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Suppliers/UpdateSuppliers',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                var tabledata = $('#tbl_suppliers').DataTable();
                $('#Suppliers_Form').trigger("reset");
                tabledata.ajax.reload();
                swal("Data has been saved.", "success");
            }
            else {
                swal("Supplier Already Exist");
            }

        }
    });
}

function DeleteSuppliers() {

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
                url: '../Suppliers/DeleteSuppliers',
                data: JSON.stringify({
                    datalist: chosenitem,
                }),

                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        var tabledata = $('#tbl_suppliers').DataTable();
                        $('#Suppliers_Form').trigger("reset");
                        tabledata.ajax.reload();
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("Supplier Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}


function AddSuppliersDetails(data) {
    var datanow = data.serialize() + '&SupplierID=' + $('#ID').val();
    $.ajax({
        url: '../Suppliers/CreateSupplierDetails',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                var tabledata = $('#tbl_details').DataTable();
                $('#SupplierDetailForm').trigger("reset");
                tabledata.ajax.reload();
                swal("Data has been saved.", "success");
            }
            else {
                swal("Supplier Already Exist");
            }

        }
    });
}

function EditSuppliersDetails(data) {
    var datanow = data.serialize() + '&ID=' + $('#SupplierDetailID').val();
    $.ajax({
        url: '../Suppliers/UpdateSuppliersDetails',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            if (returnData.msg == "Success") {
                var tabledata = $('#tbl_details').DataTable();
                $('#SupplierDetailForm').trigger("reset");
                tabledata.ajax.reload();
                swal("Data has been saved.", "success");
            }
            else {
                swal("Supplier Already Exist");
            }

        }
    });
}

function DeleteSuppliersDetails() {

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
                url: '../Suppliers/DeleteSuppliersDetails',
                data: JSON.stringify({
                    datalist: chosenitemdetails,
                }),

                type: 'POST',
                datatype: "json",
                contentType: "application/json; charset=utf-8",
                success: function (returnData) {
                    if (returnData.msg == "Success") {
                        var tabledata = $('#tbl_details').DataTable();
                        $('#SupplierDetailForm').trigger("reset");
                        tabledata.ajax.reload();
                        swal("Data has been deleted.", "success");
                    }
                    else {
                        swal("Supplier Contact Already Exist");
                    }

                }
            });

        } else {
            swal("Cancelled", data.UserName + "'s account details is safe.", "error");
        }
    });



}


function ShowContactDetails(ID, Supply){
    $("#SupplierDetails_Modal").modal("show");
    $(".modal-title").text("Contact Details - " + Supply);
    var tabledetails = $('#tbl_details').DataTable({
        ajax: {
            url: '../Suppliers/GetContactDetails',
            type: "POST",
            datatype: "json",
            data: { SupplierID: ID }
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
                }, sortable: false, searchable: false,
            },
            {
                data: function (x) {
                    return "<button type='button' class='btn btn-xs btn-flat btn-primary btn_edit_user'>" +
                        "<i class='fa fa-edit'></i>" +
                        "</button>";
                }, sortable: false, searchable: false,
            },
            { data: "ContactTitle", name: "ContactTitle" },
            { data: "ContactFirstName", name: "ContactFirstName" },
            { data: "ContactLastName", name: "ContactLastName" },
            { data: "ContactPosition", name: "ContactPosition" },
            { data: "ContactEmail", name: "ContactEmail" },
            { data: "ContactTelephone", name: "ContactTelephone" },
            { data: "ContactCellphone", name: "ContactCellphone" },
           

           

        ],

    });

    $('#tbl_details tbody').on('click', '.btn_edit_user', function () {
        var data = tabledetails.row($(this).parents('tr')).data();
        
        $('#SupplierDetailID').val(data.ID);
        $('#ContactTitle').val(data.ContactTitle);
        $('#ContactFirstName').val(data.ContactFirstName);
        $('#ContactLastName').val(data.ContactLastName);
        $('#ContactPosition').val(data.ContactPosition);
        $('#ContactEmail').val(data.ContactEmail);
        $('#ContactTelephone').val(data.ContactTelephone);
        $('#ContactCellphone').val(data.ContactCellphone);

        $("#btn_save_details").html('<i class="fa fa-edit"></i> Update');
    });

    $('#tbl_details tbody').on('change', '.checkitems', function () {
        if (this.checked) {
            chosenitemdetails.push(this.id);
            $(this).prop('checked', true);
        }
        else {
            //chosenitem.remove($(this).id);
            chosenitemdetails = chosenitem.filter(e => e !== this.id);
            $(this).prop('checked', false);
        }
    })
    
}