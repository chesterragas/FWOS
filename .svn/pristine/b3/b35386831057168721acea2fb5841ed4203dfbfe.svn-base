$(function () {
    var WorkOrderRequestData = {};
    var base64Drawing = {};
    var arrBase64Drawing = {};
    var arrDrawing = [];
    var chosenContractors = [];
    var chosenFacilities = [];
    var WorkCategoryForFinalView = "In-House"
    var IsSubmitted = false;
    var tbl_suppliers = $('#tbl_suppliers').DataTable({
        ajax: {
            url: '/WorkOrderRequest/Request/GetSuppliersList',
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
            {
                data: function (x) {
                    var checked = _.find(chosenContractors, ["ID", x.ID]) ? "checked" : "";
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkitems' " + checked + " />";
                }, sortable: false, searchable: false,
            },
            { data: "SupplierName", name: "SupplierName" },
            { data: "Address", name: "Address" },
        ],

    });
    var tblFacilities = $('#tblFacilities').DataTable({
        ajax: {
            url: '/WorkOrderRequest/Request/GetFacilitiesList',
            type: "POST",
            datatype: "json",
            data: function (data) {
                data.SectionID = $("#FacSection").val() || 0
            }
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
                    var checked = _.find(chosenFacilities, ["ID", x.ID]) ? "checked" : "";
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkFacilities' " + checked + " />";
                }, sortable: false, searchable: false,
            },
            { data: "SectionName", name: "SectionName" },
            { data: "EmpName", name: "EmpName" },
        ],

    });
    var tblSelectedContractors = $('#tblSelectedContractors').DataTable({
        lengthMenu: [[10, 50, 100], [10, 50, 100]],
        lengthChange: true,
        scrollCollapse: true,
        order: [0, "asc"],
        data: [],
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
            //{ data: "ID", name: "ID", visible: false },
            {
                data: function (x) {
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkContractors' />";
                }, sortable: false, searchable: false,
            },
            { data: "SupplierName", name: "SupplierName" },
            { data: "Address", name: "Address" },
            {
                data: "DateRequested", name: "DateRequested", render: function (data, type, row) {
                    return "<input type='date' class='form-control form-control-sm ContractorDates DateRequested' value='" + data + "'/>";
                }
            },
            {
                data: "SurveryDateTime", name: "SurveryDateTime", render: function (data, type, row) {
                    return "<input type='date' class='form-control form-control-sm ContractorDates SurveryDateTime' value='" + data + "'/>";
                }
            },
            {
                data: "QuotationSubmission", name: "QuotationSubmission", render: function (data, type, row) {
                    return "<input type='date' class='form-control form-control-sm ContractorDates QuotationSubmission' value='" + data + "'/>";
                }
            },
        ],

    });
    var tblSelectedFacilities = $('#tblSelectedFacilities').DataTable({
        lengthMenu: [[10, 50, 100], [10, 50, 100]],
        lengthChange: true,
        scrollCollapse: true,
        order: [0, "asc"],
        data: [],
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
            //{ data: "ID", name: "ID", visible: false },
            {
                data: function (x) {
                    return "<input type='checkbox' id='" + x.ID + "' class='filled-in chk-col-light-blue checkSelectedFacilities' />";
                }, sortable: false, searchable: false,
            },
            { data: "SectionName", name: "SectionName" },
            { data: "EmpName", name: "EmpName" },
            { data: "DateRequested", name: "DateRequested" },
            { data: "SurveryDateTime", name: "SurveryDateTime" },
            { data: "QuotationSubmission", name: "QuotationSubmission" },
        ],

    });
    var tblRequest = $('#tblRequest').DataTable({
        ajax: {
            url: '/WorkOrderRequest/Request/GetRequestList',
            type: "POST",
            datatype: "json",
            data: function (data) {

            },
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
        select: {
            'style': 'single'
        },
        columns: [
            { data: "DateRequest", name: "DateRequest" },
            { data: "WorkOrderTitle", name: "WorkOrderTitle" },
            { data: "ReferenceNo", name: "ReferenceNo" },
            { data: "WorkOrderNo", name: "WorkOrderNo" },
            { data: "EmployeeName", name: "EmployeeName" },
            { data: "DivisionName", name: "DivisionName" },
            { data: "DepartmentName", name: "DepartmentName" },
            { data: "SectionName", name: "SectionName" },
            { data: "IsSubmitted", name: "IsSubmitted" },
            { data: "Approved", name: "Approved" },
            { data: "ForApproval", name: "ForApproval" },
            { data: "Rejected", name: "Rejected" },
            { data: "RevisionNo", name: "RevisionNo" },
        ],
        createdRow: function (row, data, index) {
            if (data.IsSubmitted) {
                $(row).addClass("bg-yellow");
            } else {
                $(row).removeClass("bg-yellow");
            }
        }
    });
    tblRequest.on('select', function (e, dt, type, indexes) {
        var rowData = tblRequest.rows({ selected: true }).data()[0];
        if (rowData.IsSubmitted) {
            $("#btnDeleteRequest").prop("disabled", true);
            $("#btnEditRequest").prop("disabled", false);

        }
        else
            $("#btnDeleteRequest").prop("disabled", false);

    });
    tblRequest.on('deselect', function (e, dt, type, indexes) {
        $("#btnEditRequest").prop("disabled", true);
        $("#btnDeleteRequest").prop("disabled", true);
    });

    Dropdown_select('FacDivision', "/Helper/GetDropdown_Division");
    $("#btnAddRequest").click(function () {
        $.ajax({
            url: '/WorkOrderRequest/Request/GetUserDetails',
            data: JSON.stringify({
                EmployeeNo: $("#HdnEmployeeNo").val() || "",
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading_modal").modal("show");
            },
            success: function (returnData) {
                populateUserDetails(returnData.WorkOrderRequestData);
                $("#mdlCreateRequest").modal("show");
                $("#loading_modal").modal("hide");
            }
        });
        
    });
    function populateUserDetails(requestData) {
        $("#RequestorEmployeeNo").val(requestData.RequestorEmployeeNo);
        $("#RequestorName").val(requestData.EmployeeName);
        $("#RequestorMobileNo").val(requestData.MobileNo);
        $("#LocalNo").val(requestData.LocalNo);
        $("#Email").val(requestData.Email);
        $("#DivisionID option").removeAttr("selected");
        $("#DepartmentID option").removeAttr("selected");
        $("#SectionID option").removeAttr("selected");
        $("#DivisionID").prop("disabled", false).val(requestData.DivisionID).prop("disabled", true);
        $("#DepartmentID").prop("disabled", false).val(requestData.DepartmentID).prop("disabled", true);
        $("#SectionID").prop("disabled", false).val(requestData.SectionID).prop("disabled", true);
        $("#DivisionID option[value='" + requestData.DivisionID + "']").attr('selected', 'selected');
        $("#DepartmentID option[value='" + requestData.DepartmentID + "']").attr('selected', 'selected');
        $("#SectionID option[value='" + requestData.SectionID + "']").attr('selected', 'selected');
    }
    $("#wizard").smartWizard({
        selected: 0,
        theme: "default",
        transitionEffect: "",
        transitionSpeed: 0,
        useURLhash: !1,
        showStepURLhash: !1,
        toolbarSettings: {
            toolbarPosition: "bottom"
        },
        keyboardSettings: {
            keyNavigation: 0, // Enable/Disable keyboard navigation(left and right keys are used if enabled)
            //keyLeft: [37], // Left key code
            //keyRight: [39] // Right key code
        },
    })
    $('#wizard').on('leaveStep', function (e, anchorObject, stepNumber, stepDirection) {

    });
    $("#wizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection) {

        //compileWorkOrderRequestData();
        $("#step-5").html("");
        $("#step-2 select").prop("disabled", false);
        $("#step-3 select").prop("disabled", false);
        if (stepNumber == 3) {

        } else if (stepNumber == 4) {

            $("#step-2 select").prop("disabled", true);
            $("#step-3 select").prop("disabled", true);

            $("#step-1").clone().appendTo("#step-5");
            $("#step-5>div").first().removeAttr("id").show();
            $("#step-2").clone().appendTo("#step-5");
            $("#step-5>div:nth-child(2)").first().removeAttr("id").show();
            $("#step-3").clone().appendTo("#step-5");
            $("#step-5>div:nth-child(3)").first().removeAttr("id").show();
            $("#step-4").clone().appendTo("#step-5");
            $("#step-5>div:nth-child(4)").first().removeAttr("id").show();


            $("#step-5 *").removeAttr("id").removeAttr("name");
            $('input[name="WorkCategory"][value=' + WorkCategoryForFinalView + ']').prop('checked', true);

            $("#step-5 select, #step-5 input").prop("disabled", true);
            $("#step-5 select, #step-5 textarea").prop("disabled", true);
            $("#step-5 button").hide();
        }
    });
    $('input[name="WorkCategory"]').click(function () {
        var WorkCategory = $('input[name="WorkCategory"]:checked').val() || "";
        WorkCategoryForFinalView = WorkCategory;
        if (WorkCategory == "In-House") {
            $("#wizard > ul > li:nth-child(4) > a > span.info.text-ellipsis").text("In-House Allocation");
            $("#divFacilities").show();
            $("#divContractor").hide();
        } else {
            $("#wizard > ul > li:nth-child(4) > a > span.info.text-ellipsis").text("Contractor Work");
            $("#divContractor").show();
            $("#divFacilities").hide();
        }
    })
    $("#BuildingID").append("<option value='0'>Others</option>");
    $("#BuildingID").on("change", function () {
        $("#BuildingID option").removeAttr('selected');
        $("#BuildingID option[value='" + $(this).val() + "']").attr('selected', 'selected');
        if ($(this).val() == '0') {
            $("#BuildingOther").show();
        } else {
            $("#BuildingOther").val("").hide();
            Dropdown_select('FloorID', "/Helper/GetDropdown_Floor?BuildingID=" + $("#BuildingID").val());
            $("#FloorID").append("<option value='0'>Others</option>");
        }
    });
    $("#FloorID").on("change", function () {
        $("#FloorID option").removeAttr('selected');
        $("#FloorID option[value='" + $(this).val() + "']").attr('selected', 'selected');
        if ($(this).val() == '0') {
            $("#FloorOther").show();
        } else {
            $("#FloorOther").val("").hide();
        }
    });
    $("#ProcessAreaID").append("<option value='0'>Others</option>");
    $("#ProcessAreaID").on("change", function () {
        $("#ProcessAreaID option").removeAttr('selected');
        $("#ProcessAreaID option[value='" + $(this).val() + "']").attr('selected', 'selected');
        if ($(this).val() == '0') {
            $("#ProcessAreaOther").show();
        } else {
            $("#ProcessAreaOther").val("").hide();
        }
    });
    $("#RequestAssigned").on("change", function () {
        $("#RequestAssigned option").removeAttr('selected');
        $("#RequestAssigned option[value='" + $(this).val() + "']").attr('selected', 'selected');
    });
    $("#chkWorkRequest_Others").click(function () {
        if ($(this).is(":checked"))
            $("#WorkRequest_Others").prop("disabled", false);
        else
            $("#WorkRequest_Others").prop("disabled", true);
    });
    $("#chkWorkClassification_Others").click(function () {
        if ($(this).is(":checked"))
            $("#WorkClassification_Others").prop("disabled", false);
        else
            $("#WorkClassification_Others").prop("disabled", true);
    });
    $('#flDrawing').change(function () {
        arrDrawing = [];
        if ($('#flDrawing')[0].files.length) {
            var files = $('#flDrawing')[0].files;
            var fileNames = [];
            var err = false;
            var arrErr = [];
            for (var i = 0; i <= files.length - 1; i++) {
                var fname = files.item(i).name;
                var fsize = files.item(i).size;
                if (fsize / 1000000 > 50) {
                    //arrErr.push("File size is above 50MB. Filename: " + fname);
                    swal("File size is above 50MB. Filename: " + fname);
                    err = true;
                } else {
                    fileNames.push(fname);
                    arrDrawing.push({
                        RequestorInformationID: $("#ID").val() || 0,
                        FileName: fname,
                    })
                }
            }
            if (err) {
                //PriceMaster.msgType = "error";
                //PriceMaster.msgTitle = "Error!";
                //PriceMaster.msg = arrErr;
                //PriceMaster.showToastrMsg();
            } else {
                $("#lblflDrawing").text(fileNames.join(", "));
                $("#lblflDrawing").prop("readonly", true);
                validateDrawingFiles(fileNames.join());
            }

        } else {
            base64ReferenceFile = {};
            $("#lblflDrawing").text("Choose files");
            $("#lblflDrawing").prop("readonly", false);
        }
    });
    $("#btnCriteria").click(function () {
        var WorkCategory = $('input[name="WorkCategory"]:checked').val() || "";
        var CriteriaHTML = $(".Criteria").length;
        if (CriteriaHTML == 0) {
            if (WorkCategory) {
                $.ajax({
                    url: '/WorkOrderRequest/Request/GetDropdown_Criteria',
                    data: JSON.stringify({
                        WorkCategory: WorkCategory
                    }),

                    type: 'POST',
                    datatype: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (returnData) {
                        var alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        var ctr = 0;
                        var ctrTimeFrame = 0;
                        var trCriteria = "";
                        var trLevelOfPriority = "";
                        var trTimeFrame = "";
                        $.each(returnData.Criteria, function () {
                            var CriteriaHeaderObj = this;
                            if (CriteriaHeaderObj.CriteriaDetailsList.length > 0) {
                                trCriteria += "<tr class='Criteria trCriteria' data-criteriaid='" + CriteriaHeaderObj.ID + "'>" +
                                                "<td colspan='6' class='f-w-700'>" + alpha[ctr] + '. ' + CriteriaHeaderObj.CriteriaName + "</td>" +
                                            "</tr>";
                                $.each(CriteriaHeaderObj.CriteriaDetailsList, function () {
                                    DetailsObj = this;
                                    trCriteria += "<tr class='Criteria '>" +
                                                        "<td colspan='4' class='p-l-25'>" +
                                                            DetailsObj.DetailName +
                                                        "</td>" +
                                                        "<td class='text-right'>" +
                                                            DetailsObj.DetailPoints +
                                                        "</td>" +
                                                        "<td>" +
                                                            "<label class='m-l-30 m-b-0'>" +
                                                                "<input type='checkbox' class='chkCriteriaDetails Details_" + CriteriaHeaderObj.ID + "' name='CriteriaDetails' value='" + DetailsObj.ID + "'" +
                                                                       "data-criteriaid='" + CriteriaHeaderObj.ID + "'" +
                                                                       "data-detailid='" + DetailsObj.ID + "'" +
                                                                       "data-detailname='" + DetailsObj.DetailName + "'" +
                                                                       "data-detailpoints='" + DetailsObj.DetailPoints + "' />" +
                                                            "</label>" +
                                                        "</td>" +
                                                    "</tr>";
                                })
                                trLevelOfPriority += "<tr id='Criteria_" + CriteriaHeaderObj.ID + "' class='trLevelOfPriority Criteria'>" +
                                                        "<td colspan='4' class='f-w-700' style='vertical-align: middle;'>" + alpha[ctr] + '. ' + CriteriaHeaderObj.CriteriaName + "</td>" +
                                                        "<td class='tdScore text-right' style='vertical-align: middle;'>0</td>" +
                                                        "<td style='width:90px;'><input type='text' class='form-control form-control-sm Remarks' /></td>" +
                                                    "</tr>";
                                ctr++;
                            }
                        });
                        $.each(returnData.TimeFrameList, function () {
                            var TimeFrameHeaderObj = this;
                            trTimeFrame += "<tr class='Criteria trTimeFrame'" +
                                                    "data-workingdays='" + TimeFrameHeaderObj.WorkingDays + "'" +
                                                    "data-prioritylevel='" + TimeFrameHeaderObj.PriorityLevel + "'" +
                                                    "data-scorefrom='" + TimeFrameHeaderObj.ScoreFrom + "'" +
                                                    "data-scoreto='" + TimeFrameHeaderObj.ScoreTo + "'" +
                                                    "data-remarks='" + TimeFrameHeaderObj.PriorityLevelRemarks + "'>" +
                                                    "<td>" + alpha[ctrTimeFrame] + '. ' + TimeFrameHeaderObj.WorkingDays + " WORKING DAYS/LEVEL " + TimeFrameHeaderObj.PriorityLevel + "</td>" +
                                                    "<td class='text-center'>" + TimeFrameHeaderObj.ScoreFrom + " - " + TimeFrameHeaderObj.ScoreTo + "</td>" +
                                                    "<td class='text-center' colspan='4'>" + TimeFrameHeaderObj.PriorityLevelRemarks + "</td>";
                            //if (ctrTimeFrame == 0) {
                            //    trTimeFrame += "<td class='text-center' colspan='3' rowspan='" + returnData.TimeFrameList.length + "' style='vertical-align: middle;'>" +
                            //                        "<input type='date' class='form-control form-control-sm' id='DeadlineDate' />" +
                            //                    "</td>";
                            //}
                            trTimeFrame += "</tr>";
                            ctrTimeFrame++;
                        });

                        $("#trLevelOfPriorityHeader").before(trCriteria);
                        $("#trLevelOfPriorityHeader").after(trLevelOfPriority);
                        $("#trTimeFrameHeader").after(trTimeFrame);
                        if (EditCriteriaDetailsPointsList) {
                            $.each(EditCriteriaDetailsPointsList, function () {
                                $(".chkCriteriaDetails[value='" + this.CriteriaDetailsID + "']").trigger("click");
                            });
                            EditCriteriaDetailsPointsList = "";
                        }
                        if (EditCriteriaHeaderRemarksList) {
                            $.each(EditCriteriaHeaderRemarksList, function () {
                                $("#Criteria_" + this.CriteriaHeaderID).find(".Remarks").val(this.Remarks);
                            });
                            EditCriteriaHeaderRemarksList = "";
                        }
                        $("#mdlCriteria").modal("show");
                    }
                });
            } else {
                swal("Please Select Work Category first!");
            }
        } else {
            $("#mdlCriteria").modal("show");
        }
    });
    $("#tblCriteria").on("click", ".chkCriteriaDetails", function () {
        var TotalScore = 0;
        $(".trCriteria").each(function () {
            var CriteriaID = +$(this).attr("data-criteriaid") || 0;
            var CriteriaTotalPoints = 0;
            $(".Details_" + CriteriaID).each(function () {
                var DetailsID = +$(this).attr("data-detailid") || 0;
                var DetailName = $(this).attr("data-detailname") || "";
                var DetailPoint = +$(this).attr("data-detailpoints") || 0;
                if ($(this).is(":checked")) {
                    CriteriaTotalPoints += DetailPoint;
                }
            });
            $("#Criteria_" + CriteriaID).find(".tdScore").text(CriteriaTotalPoints);
            TotalScore += CriteriaTotalPoints;
        });
        $("#tdLevelOfPriorityTotal").text(TotalScore);
        var Currentdate = new Date();
        var CurrentWorkingDays = 0;
        var PriorityLevel = 0;
        $(".trTimeFrame").each(function () {
            var scorefrom = +$(this).attr("data-scorefrom");
            var scoreto = +$(this).attr("data-scoreto");
            var workingdays = +$(this).attr("data-workingdays");
            var prioritylevel = +$(this).attr("data-prioritylevel");
            if (TotalScore >= scorefrom && TotalScore <= scoreto) {
                CurrentWorkingDays = workingdays;
                PriorityLevel = prioritylevel;
            }
        });
        Currentdate.setDate(Currentdate.getDate() + CurrentWorkingDays);
        var DeadlineDate = (Currentdate.getFullYear() + '-' + ((Currentdate.getMonth() > 8) ? (Currentdate.getMonth() + 1) : ('0' + (Currentdate.getMonth() + 1))) + '-' + ((Currentdate.getDate() > 9) ? Currentdate.getDate() : ('0' + Currentdate.getDate())));
        $("#DeadlineDate").val(DeadlineDate);
        $("#PriorityLevel").val(PriorityLevel);
    });
    $("#btnConfirmCriteria").click(function () {
        $("#mdlCriteria").modal("hide");
    });
    $("#btnCancelCriteria").click(function () {
        resetCriteria();
    });
    $("#btnAddContractorSupplier").click(function () {
        $("#mdlContractorSupplierList").modal("show");
        tbl_suppliers.ajax.reload(null, false);
        $("#chkAllSupplier").prop("checked", false);
    });
    $("#btnAddFacilities").click(function () {
        $("#mdlFacilitiesList").modal("show");
        tblFacilities.ajax.reload(null, false);
        $("#chkAllFacilities").prop("checked", false);
    });
    $("#FacDivision").change(function () {
        $("#FacDepartment").val("");
        $("#FacSection").val("").trigger("change");
        if ($(this).val()) {
            Dropdown_select('FacDepartment', "/Helper/GetDropdown_Department?DivisionID" + $(this).val());
        }
    });
    $("#FacDepartment").change(function () {
        $("#FacSection").val("").trigger("change");
        if ($(this).val()) {
            Dropdown_select('FacSection', "/Helper/GetDropdown_Section?DepartmentID" + $(this).val());
        }
    });
    $("#FacSection").change(function () {
        tblFacilities.ajax.reload(null, false);
    });
    $("#btnSelectContractorSupplier").click(function () {
        tblSelectedContractors.clear().draw();
        tblSelectedContractors.rows.add(chosenContractors); // Add new data
        tblSelectedContractors.columns.adjust().draw();
        $("#mdlContractorSupplierList").modal("hide");
        $("#chkAllSelectedContractors").prop("checked", false);
    });
    $("#tblSelectedContractors").on("change", ".ContractorDates", function () {
        var row = $(this).closest("tr");
        var rowData = tblSelectedContractors.rows(row).data()[0];
        var foundRow = _.find(chosenContractors, ["ID", rowData.ID]);
        foundRow.DateRequested = row.find(".DateRequested").val() || "";
        foundRow.SurveryDateTime = row.find(".SurveryDateTime").val() || "";
        foundRow.QuotationSubmission = row.find(".QuotationSubmission").val() || "";
    });

    $("#btnDeleteSelectedContractors").click(function () {
        $('.checkContractors').each(function () {
            if ($(this).is(":checked")) {
                var rowData = tblSelectedContractors.rows($(this).closest("tr")).data()[0];
                _.remove(chosenContractors, {
                    ID: rowData.ID
                });
            }
        });
        tblSelectedContractors.clear().draw();
        tblSelectedContractors.rows.add(chosenContractors); // Add new data
        tblSelectedContractors.columns.adjust().draw();
        $("#chkAllSelectedContractors").prop("checked", false);
    });
    $("#btnSelectFacilities").click(function () {
        tblSelectedFacilities.clear().draw();
        tblSelectedFacilities.rows.add(chosenFacilities); // Add new data
        tblSelectedFacilities.columns.adjust().draw();
        $("#mdlFacilitiesList").modal("hide");
        $("#chkAllSelectedFacilities").prop("checked", false);
    });
    $("#btnDeleteSelectedFacilities").click(function () {
        $('.checkSelectedFacilities').each(function () {
            if ($(this).is(":checked")) {
                var rowData = tblSelectedFacilities.rows($(this).closest("tr")).data()[0];
                _.remove(chosenFacilities, {
                    ID: rowData.ID
                });
            }
        });
        tblSelectedFacilities.clear().draw();
        tblSelectedFacilities.rows.add(chosenFacilities); // Add new data
        tblSelectedFacilities.columns.adjust().draw();
        $("#chkAllSelectedFacilities").prop("checked", false);
    });
    $("#chkAllSelectedContractors").click(function () {
        if ($(this).is(":checked")) {
            $("#tblSelectedContractors tbody .checkContractors").prop("checked", true);
        } else {
            $("#tblSelectedContractors tbody .checkContractors").prop("checked", false);
        }
    });
    $("#chkAllSelectedFacilities").click(function () {
        if ($(this).is(":checked")) {
            $("#tblSelectedFacilities tbody .checkSelectedFacilities").prop("checked", true);
        } else {
            $("#tblSelectedFacilities tbody .checkSelectedFacilities").prop("checked", false);
        }
    });
    $("#chkAllSupplier").click(function () {
        if ($(this).is(":checked")) {
            $("#tbl_suppliers tbody .checkitems").prop("checked", true);
        } else {
            $("#tbl_suppliers tbody .checkitems").prop("checked", false);
        }
    });
    $("#chkAllFacilities").click(function () {
        if ($(this).is(":checked")) {
            $("#tblFacilities tbody .checkFacilities").prop("checked", true);
        } else {
            $("#tblFacilities tbody .checkFacilities").prop("checked", false);
        }
    });
    //$(".AddToContractorTable").change(function () {
    //    _.update(chosenContractors, function (obj) {
    //        obj.DateRequested = $("#DateRequested").val() || "";
    //        obj.QuotationSubmission = $("#QuotationSubmission").val() || "";
    //        obj.SurveryDateTime = $("#SurveryDateTime").val() || "";
    //    });
    //    console.log(chosenContractors);
    //});
    $('#tbl_suppliers').on('click', '.checkitems,#chkAllSupplier', function () {
        //chosenContractors = [];
        $('.checkitems').each(function () {
            var rowData = tbl_suppliers.rows($(this).closest("tr")).data()[0];
            if ($(this).is(":checked")) {
                var foundRow = _.find(chosenContractors, ["ID", rowData.ID]);
                if (!foundRow) {
                    rowData.DateRequested = "";
                    rowData.QuotationSubmission = "";
                    rowData.SurveryDateTime = "";
                    chosenContractors.push(rowData);
                    $(this).prop('checked', true);
                }
            } else {
                _.remove(chosenContractors, ["ID", rowData.ID]);
            }
        });
        chosenContractors = setDates(chosenContractors);
        console.log(chosenContractors);
    })
    $('#tblFacilities').on('click', '.checkFacilities,#chkAllFacilities', function () {
        chosenFacilities = [];
        $('.checkFacilities').each(function () {
            if ($(this).is(":checked")) {
                var rowData = tblFacilities.rows($(this).closest("tr")).data()[0];
                rowData.DateRequested = "";
                rowData.QuotationSubmission = "";
                rowData.SurveryDateTime = "";
                chosenFacilities.push(rowData);
                $(this).prop('checked', true);
            }
        });
        chosenFacilities = setDates(chosenFacilities);
        console.log(chosenFacilities);
    })
    $("#btnSaveRequest").click(function () {
        swal({
            title: "Are you sure?",
            text: "Work Order Request will be saved.",
            icon: "info",
            buttons: true,
        })
        .then((confirm) => {
            if (confirm) {
                IsSubmitted = false;
                compileWorkOrderRequestData();
                saveRequest();
            } else {
                //swal("Your imaginary file is safe!");
            }
        });
    });
    $("#btnPostRequest").click(function () {
        swal({
            title: "Are you sure?",
            text: "Work Order Request will be posted.",
            icon: "info",
            buttons: true,
        })
        .then((confirm) => {
            if (confirm) {
                IsSubmitted = true;
                compileWorkOrderRequestData();
                saveRequest();
            } else {
                //swal("Your imaginary file is safe!");
            }
        });
    });
    $("#btnCancelRequest").click(function () {
        swal({
            title: "Are you sure?",
            text: "Unsaved Work Order Request data will be lost.",
            icon: "info",
            buttons: true,
        })
        .then((confirm) => {
            if (confirm) {
                clearWorkOrderForm();
            } else {
                //swal("Your imaginary file is safe!");
            }
        });
    });
    $("#btnEditRequest").click(function () {
        getRequestDetails();
    });
    $("#btnDeleteDrawingFiles").click(function () {
        if (!$("#flDrawing").val() && $("#lblflDrawing").text() != "Choose files") {
            swal({
                title: "Are you sure?",
                text: "Drawing Files will be deleted permanently.",
                icon: "info",
                buttons: true,
            })
        .then((confirm) => {
            if (confirm) {
                deleteDrawingFiles();
            } else {
                //swal("Your imaginary file is safe!");
            }
        });
        }
    });
    $("#btnDeleteRequest").click(function () {
        swal({
            title: "Are you sure?",
            text: "Request will be deleted.",
            icon: "info",
            buttons: true,
        })
        .then((confirm) => {
            if (confirm) {
                deleteRequest();
            } else {
                //swal("Your imaginary file is safe!");
            }
        });
    });
    function deleteRequest() {
        var rowData = tblRequest.rows({ selected: true }).data()[0];
        $.ajax({
            url: '/WorkOrderRequest/Request/DeleteRequest',
            data: JSON.stringify({
                ID: rowData.ID || 0,

            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading_modal").modal("show");
            },
            success: function (returnData) {
                if (returnData.success) {
                    swal({
                        title: "Iformation",
                        text: returnData.msg,
                        icon: "success",
                    })
                    tblRequest.ajax.reload(null, false);
                }
                $("#loading_modal").modal("hide");
            }
        });
    }
    function deleteDrawingFiles() {

        $.ajax({
            url: '/WorkOrderRequest/Request/DeleteDrawingFiles',
            data: JSON.stringify({
                ID: $("#ID").val() || 0,
                Files: $("#lblflDrawing").text() || ""
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading_modal").modal("show");
            },
            success: function (returnData) {
                if (returnData.success) {
                    swal({
                        title: "Iformation",
                        text: returnData.msg,
                        icon: "success",
                    })
                    $("#flDrawing").prop("disabled", false);
                    $("#lblflDrawing").text("Choose Files");
                }
                $("#loading_modal").modal("hide");
            }
        });
    }
    function getRequestDetails() {
        var rowData = tblRequest.rows({ selected: true }).data()[0];
        $.ajax({
            url: '/WorkOrderRequest/Request/GetRequestDetails',
            data: JSON.stringify({
                ID: rowData.ID
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading_modal").modal("show");
            },
            success: function (returnData) {
                if (returnData.success) {
                    populateData(returnData.WorkOrderRequestData, returnData);
                }
                $("#loading_modal").modal("hide");
            }
        });
    }
    function populateData(requestData, returnData) {
        $("#ID").val(requestData.ID);
        $("#DateRequest").val(formatDate(requestData.DateRequest, "yyyy-mm-dd"));
        $("#WorkOrderTitle").val(requestData.WorkOrderTitle);
        $("#RequestorEmployeeNo").val(requestData.RequestorEmployeeNo);
        $("#RequestorName").val(requestData.EmployeeName);
        $("#RequestorMobileNo").val(requestData.MobileNo);
        $("#LocalNo").val(requestData.LocalNo);
        $("#Email").val(requestData.Email);
        $("#DivisionID option").removeAttr("selected");
        $("#DepartmentID option").removeAttr("selected");
        $("#SectionID option").removeAttr("selected");
        $("#DivisionID").prop("disabled", false).val(requestData.DivisionID).prop("disabled", true);
        $("#DepartmentID").prop("disabled", false).val(requestData.DepartmentID).prop("disabled", true);
        $("#SectionID").prop("disabled", false).val(requestData.SectionID).prop("disabled", true);
        $("#DivisionID option[value='" + requestData.DivisionID + "']").attr('selected', 'selected');
        $("#DepartmentID option[value='" + requestData.DepartmentID + "']").attr('selected', 'selected');
        $("#SectionID option[value='" + requestData.SectionID + "']").attr('selected', 'selected');

        $("#ReferenceNo").val(requestData.ReferenceNo);
        $("#WorkOrderNo").val(requestData.WorkOrderNo);
        $("#RevisionNo").val(requestData.RevisionNo);
        $("#IsSubmitted").val(requestData.IsSubmitted);
        $("#Approved").val(requestData.Approved);
        $("#ForApproval").val(requestData.ForApproval);
        $("#Rejected").val(requestData.Rejected);
        $("#RevisionNo").val(requestData.RevisionNo);
        PopulateWO_WorkDescriptionDetailsData(requestData.WO_WorkDescriptionDetailsData)
        PopulateWO_FacilitiesData(requestData.WO_FacilitiesData, requestData.WO_ContractorDetailsDataList, returnData)
        $("#mdlCreateRequestTitle").text("Update Request");
        $("#lblSaveRequest").text("Update Request");
        $("#mdlCreateRequest").modal("show");
    }
    function PopulateWO_WorkDescriptionDetailsData(WO_WorkDescriptionDetailsData) {
        if (WO_WorkDescriptionDetailsData.BuildingOther == "" && WO_WorkDescriptionDetailsData.BuildingID == 0) {
            $("#BuildingID").val("");
            $("#BuildingID option[value='']").attr('selected', 'selected');
        } else {
            $("#BuildingID").val(WO_WorkDescriptionDetailsData.BuildingID);
            $("#BuildingID option[value='" + WO_WorkDescriptionDetailsData.BuildingID + "']").attr('selected', 'selected');
            $("#BuildingOther").val(WO_WorkDescriptionDetailsData.BuildingOther);
            if (WO_WorkDescriptionDetailsData.BuildingOther)
                $("#BuildingOther").show();
        }
        Dropdown_selectHere('FloorID', "/Helper/GetDropdown_Floor?BuildingID=" + WO_WorkDescriptionDetailsData.BuildingID, WO_WorkDescriptionDetailsData.FloorID);
        $("#FloorID").append("<option value='0'>Others</option>");
        if (WO_WorkDescriptionDetailsData.FloorOther == "" && WO_WorkDescriptionDetailsData.FloorID == 0) {
            $("#FloorID").val("");
            $("#FloorID option[value='']").attr('selected', 'selected');
        } else {
            $("#FloorID").val(WO_WorkDescriptionDetailsData.FloorID);
            $("#FloorID option[value='" + WO_WorkDescriptionDetailsData.FloorID + "']").attr('selected', 'selected');
            $("#FloorOther").val(WO_WorkDescriptionDetailsData.FloorOther);
            if (WO_WorkDescriptionDetailsData.FloorOther)
                $("#FloorOther").show();
        }
        if (WO_WorkDescriptionDetailsData.ProcessAreaOther == "" && WO_WorkDescriptionDetailsData.ProcessAreaID == 0) {
            $("#ProcessAreaID").val("");
            $("#ProcessAreaID option[value='']").attr('selected', 'selected');
        } else {
            $("#ProcessAreaID").val(WO_WorkDescriptionDetailsData.ProcessAreaID);
            $("#ProcessAreaID option[value='" + WO_WorkDescriptionDetailsData.ProcessAreaID + "']").attr('selected', 'selected');
            $("#ProcessAreaOther").val(WO_WorkDescriptionDetailsData.ProcessAreaOther);
            if (WO_WorkDescriptionDetailsData.ProcessAreaOther)
                $("#ProcessAreaOther").show();
        }
        $("#Details").val(WO_WorkDescriptionDetailsData.Details);
        var arrFileNames = [];
        $.each(WO_WorkDescriptionDetailsData.WO_DrawingList, function () {
            arrFileNames.push(this.FileName);
        })
        $("#lblflDrawing").text(arrFileNames.join(", "));
        if (arrFileNames.length) {
            $("#flDrawing").prop("disabled", true)
            $("#btnDeleteDrawingFiles").show()
        } else {
            $("#flDrawing").prop("disabled", false)
            $("#btnDeleteDrawingFiles").hide()
            $("#lblflDrawing").text("Choose Files");
        }
        $.each(WO_WorkDescriptionDetailsData.WO_WorkRequestList, function () {
            $(".chkWorkRequest[value='" + this.WorkRequestID + "']").prop("checked", true);
            $("#WorkRequest_Others").val(this.WorkRequestOther).prop("disabled", this.WorkRequestOther ? false : true);;
        })
        $.each(WO_WorkDescriptionDetailsData.WO_WorkClassificationList, function () {
            $(".chkWorkClassification[value='" + this.WorkClassificationID + "']").prop("checked", true);
            $("#WorkClassification_Others").val(this.WorkClassificationOther).prop("disabled", this.WorkClassificationOther ? false : true);
        })
    }
    var EditCriteriaDetailsPointsList = "";
    var EditCriteriaHeaderRemarksList = "";
    function PopulateWO_FacilitiesData(WO_FacilitiesData, WO_ContractorDetailsDataList, returnData) {
        $("#ReceivedBy").val(WO_FacilitiesData.ReceivedBy);
        $("#ReceivedDate").val(formatDate(WO_FacilitiesData.ReceivedDate, "yyyy-mm-dd"));
        $('input[name="WorkCategory"][value=' + WO_FacilitiesData.WorkCategory + ']').prop('checked', true).trigger("click");
        $("#RequestAssigned").val(WO_FacilitiesData.RequestAssigned);
        $("#RequestAssigned option[value='" + WO_FacilitiesData.RequestAssigned + "']").attr('selected', 'selected');
        $("#Notes").val(WO_FacilitiesData.Notes);
        $("#PriorityLevel").val(WO_FacilitiesData.PriorityLevel);
        $("#DeadlineDate").val(formatDate(WO_FacilitiesData.DeadlineDate, "yyyy-mm-dd"));
        EditCriteriaDetailsPointsList = WO_FacilitiesData.WO_CriteriaDetailsPointsList;
        EditCriteriaHeaderRemarksList = WO_FacilitiesData.WO_CriteriaHeaderRemarksList;

        if (WO_FacilitiesData.WorkCategory == "In-House") {
            tblSelectedFacilities.clear().draw();
            tblSelectedFacilities.rows.add(WO_ContractorDetailsDataList); // Add new data
            tblSelectedFacilities.columns.adjust().draw();
            chosenFacilities = WO_ContractorDetailsDataList;
            $('#FacDivision').val(returnData.DivisionID);
            Dropdown_selectFailitiesDepartment(returnData);
        } else {
            tblSelectedContractors.clear().draw();
            tblSelectedContractors.rows.add(WO_ContractorDetailsDataList); // Add new data
            tblSelectedContractors.columns.adjust().draw();
            chosenContractors = WO_ContractorDetailsDataList;
        }
    }

    function Dropdown_selectFailitiesDepartment(returnData) {
        var option = '<option value="">--SELECT--</option>';
        $('#FacDepartment').html(option);
        $.ajax({
            url: "/Helper/GetDropdown_Department?DivisionID" + returnData.DivisionID,
            type: 'GET',
            dataType: 'JSON',
        }).done(function (data, textStatus, xhr) {
            $.each(data.list, function (i, x) {
                option = '<option value="' + x.ID + '">' + x.ItemName + '</option>';
                $('#FacDepartment').append(option);
            });
            $('#FacDepartment').val(returnData.DepartmentID);
            Dropdown_selectFailitiesSection(returnData)
        }).fail(function (xhr, textStatus, errorThrown) {
            console.log(errorThrown, textStatus);
        });
    }
    function Dropdown_selectFailitiesSection(returnData) {
        var option = '<option value="">--SELECT--</option>';
        $('#FacSection').html(option);
        $.ajax({
            url: "/Helper/GetDropdown_Section?DepartmentID" + returnData.DepartmentID,
            type: 'GET',
            dataType: 'JSON',
        }).done(function (data, textStatus, xhr) {
            $.each(data.list, function (i, x) {
                option = '<option value="' + x.ID + '">' + x.ItemName + '</option>';
                $('#FacSection').append(option);
            });
            if (returnData.WorkOrderRequestData.WO_ContractorDetailsDataList.length)
                $('#FacSection').val(returnData.WorkOrderRequestData.WO_ContractorDetailsDataList[0].SectionID).trigger("change");

        }).fail(function (xhr, textStatus, errorThrown) {
            console.log(errorThrown, textStatus);
        });
    }
    function validateDrawingFiles(fileNames) {
        $.ajax({
            url: '/WorkOrderRequest/Request/ValidateDrawingFiles',
            data: JSON.stringify({
                FileNames: fileNames
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (!returnData.success) {
                    swal({
                        title: "Iformation",
                        text: returnData.errors,
                        icon: "error",
                    })
                    $('#flDrawing').val("");
                    $("#lblflDrawing").text("Choose files");
                }
            }
        });
    }
    function compileWorkOrderRequestData() {
        var WorkCategory = $('input[name="WorkCategory"]:checked').val() || "";
        WorkOrderRequestData = {
            ID: $("#ID").val() || 0,
            DateRequest: $("#DateRequest").val() || "",
            WorkOrderTitle: $("#WorkOrderTitle").val() || "",
            ReferenceNo: $("#ReferenceNo").val() || "",
            WorkOrderNo: $("#WorkOrderNo").val() || "",
            RequestorEmployeeNo: $("#RequestorEmployeeNo").val() || "",
            DivisionID: $("#DivisionID").val() || "",
            DepartmentID: $("#DepartmentID").val() || "",
            SectionID: $("#SectionID").val() || "",
            IsSubmitted: IsSubmitted,
            WO_WorkDescriptionDetailsData: getWO_WorkDescriptionDetailsData(),
            WO_FacilitiesData: getWO_FacilitiesData(),
            WO_ContractorDetailsDataList: getWO_ContractorDetailsDataList(),
        }
    }
    function getWO_ContractorDetailsDataList() {
        var WO_ContractorDetailsDataList = [];
        var WorkCategory = $('input[name="WorkCategory"]:checked').val()
        if (WorkCategory == "In-House") {
            WO_ContractorDetailsDataList = tblSelectedFacilities.rows().data();
        } else {
            WO_ContractorDetailsDataList = tblSelectedContractors.rows().data();
        }
        const updatedState = _.map(WO_ContractorDetailsDataList, function (stateItem) {
            stateItem.RequestorInformationID = $("#ID").val() || 0;
            return stateItem;
        });
        return updatedState;
    }
    function getWO_FacilitiesData() {
        var WorkCategory = $('input[name="WorkCategory"]:checked').val() || "";
        var FacilitiesData = {
            RequestorInformationID: $("#ID").val() || 0,
            ReceivedBy: $("#ReceivedBy").val(),
            ReceivedDate: $("#ReceivedDate").val(),
            WorkCategory: WorkCategory,
            RequestAssigned: $("#RequestAssigned").val(),
            Notes: $("#Notes").val(),
            PriorityLevel: $("#PriorityLevel").val(),
            DeadlineDate: $("#DeadlineDate").val(),
            WO_CriteriaDetailsPointsList: getWO_CriteriaDetailsPointsListData(),
            WO_CriteriaHeaderRemarksList: getWO_CriteriaHeaderRemarksListData(),
        }

        if (!$(".Criteria").length) {
            FacilitiesData.WO_CriteriaDetailsPointsList = EditCriteriaDetailsPointsList;
            FacilitiesData.WO_CriteriaHeaderRemarksList = EditCriteriaHeaderRemarksList;
        }
        return FacilitiesData;
    }
    function getWO_CriteriaDetailsPointsListData() {
        var WO_CriteriaDetailsPointsList = [];
        $(".chkCriteriaDetails").each(function () {
            if ($(this).is(":checked")) {
                WO_CriteriaDetailsPointsList.push({
                    RequestorInformationID: $("#ID").val() || 0,
                    CriteriaDetailsID: $(this).attr("data-detailid"),
                    CriteriaDetailsPoints: $(this).attr("data-detailpoints"),
                });
            }
        });
        return WO_CriteriaDetailsPointsList;
    }
    function getWO_CriteriaHeaderRemarksListData() {
        var WO_CriteriaHeaderRemarksList = [];
        $(".Criteria.trCriteria").each(function () {
            WO_CriteriaHeaderRemarksList.push({
                RequestorInformationID: $("#ID").val() || 0,
                CriteriaHeaderID: $(this).attr("data-criteriaid"),
                CriteriaPoints: $("#Criteria_" + $(this).attr("data-criteriaid")).find(".tdScore").text() || 0,
                Remarks: $("#Criteria_" + $(this).attr("data-criteriaid")).find(".Remarks").val() || "",
            });
        });
        return WO_CriteriaHeaderRemarksList;
    }
    function getWO_WorkDescriptionDetailsData() {
        var getWO_WorkDescriptionDetailsData = {
            ID: $("#WO_WorkDescriptionDetailsID").val() || 0,
            RequestorInformationID: $("#ID").val() || 0,
            BuildingID: $("#BuildingID").val() || 0,
            BuildingOther: $("#BuildingOther").val() || "",
            FloorID: $("#FloorID").val() || 0,
            FloorOther: $("#FloorOther").val() || "",
            ProcessAreaID: $("#ProcessAreaID").val() || 0,
            ProcessAreaOther: $("#ProcessAreaOther").val() || "",
            Details: $("#Details").val() || "",
            WO_DrawingList: arrDrawing,
            WO_WorkRequestList: getWO_WorkRequestListData(),
            WO_WorkClassificationList: getWO_WorkClassificationListData(),
        }
        return getWO_WorkDescriptionDetailsData;
    }
    function getWO_WorkClassificationListData() {
        var WO_WorkClassificationList = [];
        $(".chkWorkClassification").each(function () {
            if ($(this).is(":checked")) {
                WO_WorkClassificationList.push({
                    RequestorInformationID: $("#ID").val() || 0,
                    WorkClassificationID: $(this).val(),
                    WorkClassificationOther: $(this).val() == 0 ? $(this).closest(".input-group").find("#WorkClassification_Others").val() : "",
                });
            }
        })
        return WO_WorkClassificationList;
    }
    function getWO_WorkRequestListData() {
        var WO_WorkRequestList = [];
        $(".chkWorkRequest").each(function () {
            if ($(this).is(":checked")) {
                WO_WorkRequestList.push({
                    RequestorInformationID: $("#ID").val() || 0,
                    WorkRequestID: $(this).val(),
                    WorkRequestOther: $(this).val() == 0 ? $(this).closest(".input-group").find("#WorkRequest_Others").val() : "",
                });
            }
        })
        return WO_WorkRequestList;
    }
    function saveRequest() {
        $.ajax({
            url: '/WorkOrderRequest/Request/SaveWorkOrderRequestData',
            data: JSON.stringify({
                WorkOrderRequestData: WorkOrderRequestData
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loading_modal").modal("show");
            },
            success: function (returnData) {
                if (returnData.success) {
                    if ($("#flDrawing").val())
                        saveDrawingFiles(returnData.msg);
                    else {
                        swal({
                            title: "Iformation",
                            text: returnData.msg,
                            icon: "success",
                        })
                        clearWorkOrderForm();
                        tblRequest.ajax.reload(null, false)
                        $("#loading_modal").modal("hide");
                    }
                } else {
                    swal({
                        title: "Error",
                        text: returnData.errors,
                        icon: "error",
                    })
                    $("#loading_modal").modal("hide");
                }
            }
        });
    }
    function saveDrawingFiles(endMsg) {
        var uploadFile = new FormData();
        var files = $("#flDrawing").get(0).files;
        for (var i = 0; i < files.length; i++) {
            uploadFile.append(files[i].name, files[i]);
        }
        $.ajax({
            url: '/WorkOrderRequest/Request/SaveDrawingFiles',
            data: uploadFile,
            type: 'POST',
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#loading_modal").modal("show");
            },
            success: function (returnData) {
                if (returnData.success) {
                    swal({
                        title: "Iformation",
                        text: endMsg,
                        icon: "success",
                    })
                    clearWorkOrderForm();
                    tblRequest.ajax.reload(null, false)
                }
                $("#loading_modal").modal("hide");
            }
        });
    }
    function clearWorkOrderForm() {
        $("#mdlCreateRequest").modal("hide");
        $("#lnkStep1").trigger("click");
        $('#wizard').smartWizard("reset");
        chosenContractors = [];
        chosenFacilities = [];

        $("input[type='text']").val("").prop("checked");
        $("input[type='checkbox']").prop("checked", false);
        $("select").val("");
        $("#frmRequest textarea").val("");
        $("#frmRequest #lblflDrawing").text("");
        $("#frmRequest #step-5").html("");
        tblSelectedFacilities.clear().draw();
        tblSelectedContractors.clear().draw();
        $("#mdlCreateRequestTitle").text("Create Request");
        $("#lblSaveRequest").text("Save Request");
        $("#flDrawing").prop("disabled", false)
        $("#btnDeleteDrawingFiles").hide()
        $("#lblflDrawing").text("Choose Files")
    }
    function setDates(arrData) {
        var CurrentDateRequested = new Date();
        var CurrentQuotationSubmission = new Date();
        var CurrentSurveryDateTime = new Date();
        CurrentDateRequested.setDate(CurrentDateRequested.getDate() + 1);
        CurrentSurveryDateTime.setDate(CurrentSurveryDateTime.getDate() + 2);
        CurrentQuotationSubmission.setDate(CurrentQuotationSubmission.getDate() + 3);
        var DateRequested = (CurrentDateRequested.getFullYear() + '-' + ((CurrentDateRequested.getMonth() > 8) ? (CurrentDateRequested.getMonth() + 1) : ('0' + (CurrentDateRequested.getMonth() + 1))) + '-' + ((CurrentDateRequested.getDate() > 9) ? CurrentDateRequested.getDate() : ('0' + CurrentDateRequested.getDate())));
        var QuotationSubmission = (CurrentQuotationSubmission.getFullYear() + '-' + ((CurrentQuotationSubmission.getMonth() > 8) ? (CurrentQuotationSubmission.getMonth() + 1) : ('0' + (CurrentQuotationSubmission.getMonth() + 1))) + '-' + ((CurrentQuotationSubmission.getDate() > 9) ? CurrentQuotationSubmission.getDate() : ('0' + CurrentQuotationSubmission.getDate())));
        var SurveryDateTime = (CurrentSurveryDateTime.getFullYear() + '-' + ((CurrentSurveryDateTime.getMonth() > 8) ? (CurrentSurveryDateTime.getMonth() + 1) : ('0' + (CurrentSurveryDateTime.getMonth() + 1))) + '-' + ((CurrentSurveryDateTime.getDate() > 9) ? CurrentSurveryDateTime.getDate() : ('0' + CurrentSurveryDateTime.getDate())));
        const updatedState = _.map(arrData, function (stateItem) {
            if (!stateItem.DateRequested)
                stateItem.DateRequested = DateRequested || "";
            if (!stateItem.QuotationSubmission)
                stateItem.QuotationSubmission = QuotationSubmission || "";
            if (!stateItem.SurveryDateTime)
                stateItem.SurveryDateTime = SurveryDateTime || "";
            return stateItem;
        });
        return updatedState;
    }
    function resetCriteria() {
        $(".Criteria").remove();
        $(".trCriteria .tdScore").text("0");
        $("#tdLevelOfPriorityTotal").text("0");
        $("#PriorityLevel").val("");
        $("#DeadlineDate").val("");
    }
    function Dropdown_selectHere(id, url, defaultVal) {
        var option = '<option value="">--SELECT--</option>';
        $('#' + id).html(option);
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'JSON',
        }).done(function (data, textStatus, xhr) {
            $.each(data.list, function (i, x) {
                option = '<option value="' + x.ID + '">' + x.ItemName + '</option>';
                $('#' + id).append(option);
            });
            if (defaultVal) {
                $('#' + id).val(defaultVal);
                $('#' + id + " option[value='" + defaultVal + "']").attr('selected', 'selected');
            }

        }).fail(function (xhr, textStatus, errorThrown) {
            console.log(errorThrown, textStatus);
        });
    }
    function formatDate(dataToFormat, dateFormat) {
        if (dataToFormat) {
            if (dateFormat) {
                if (dateFormat === "dd/mm/yyyy") {
                    var today = new Date(dataToFormat);
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }
                    var today = dd + '/' + mm + '/' + yyyy;
                }
                if (dateFormat === "mm/dd/yyyy") {
                    var today = new Date(dataToFormat);
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }
                    var today = mm + '/' + dd + '/' + yyyy;
                }
                if (dateFormat === "mmyyyy") {
                    var today = new Date(dataToFormat);
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }
                    var today = mm + '' + yyyy;
                }
                if (dateFormat === "yyyy-mm-dd") {
                    var today = new Date(dataToFormat);
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1; //January is 0!

                    var yyyy = today.getFullYear();
                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }
                    var today = yyyy + '-' + mm + '-' + dd;
                }
                return today;
            } else {
                return dataToFormat;
            }

        } else {
            return "";
        }
    }
});
