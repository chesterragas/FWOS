$(function () {
    var WorkOrderRequestData = {};
    Dropdown_select('FacDivision', "/Helper/GetDropdown_Division");
    $("#btnAddRequest").click(function () {
        $("#mdlCreateRequest").modal("show");
    });
    $("#wizard").smartWizard({
        selected: 0,
        theme: "default",
        transitionEffect: "",
        transitionSpeed: 0,
        useURLhash: !1,
        showStepURLhash: !1,
        toolbarSettings: {
            toolbarPosition: "bottom"
        }
    })
    $('#wizard').on('leaveStep', function (e, anchorObject, stepNumber, stepDirection) {

    });
    $("#wizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection) {
        var WorkCategory = $('input[name="WorkCategory"]:checked').val() || "";
        compileWorkOrderRequestData();
        $("#step-5").html("");
        if (stepNumber == 3) {
            if (WorkCategory == "In-House") {
                $("#wizard > ul > li:nth-child(4) > a > span.info.text-ellipsis").text("In-House Allocation");
                $("#divFacilities").show();
                $("#divContractor").hide();
            } else {
                $("#wizard > ul > li:nth-child(4) > a > span.info.text-ellipsis").text("Contractor Work");
                $("#divContractor").show();
                $("#divFacilities").hide();
            }
        } else if (stepNumber == 4) {
            $("#step-1").clone().appendTo("#step-5");
            $("#step-5>div").first().removeAttr("id").show();
            $("#step-2").clone().appendTo("#step-5");
            $("#step-5>div:nth-child(2)").first().removeAttr("id").show();
            $("#step-3").clone().appendTo("#step-5");
            $("#step-5>div:nth-child(3)").first().removeAttr("id").show();
            $("#step-4").clone().appendTo("#step-5");
            $("#step-5>div:nth-child(4)").first().removeAttr("id").show();

            $("#step-5 select, #step-5 input").prop("disabled", true);
            $("#step-5 button").hide();
        }
    });
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
            Rejected: $("#Rejected").val() || "",
        }
    }
    $("#BuildingID").append("<option value='0'>Others</option>");
    $("#BuildingID").on("change", function () {
        if ($(this).val() == '0') {
            $("#BuildingOther").show();
        } else {
            $("#BuildingOther").val("").hide();
            Dropdown_select('FloorID', "/Helper/GetDropdown_Floor?BuildingID=" + $("#BuildingID").val());
            $("#FloorID").append("<option value='0'>Others</option>");
        }
    });
    $("#FloorID").append("<option value='0'>Others</option>");
    $("#FloorID").on("change", function () {
        if ($(this).val() == '0') {
            $("#FloorOther").show();
        } else {
            $("#FloorOther").val("").hide();
        }
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
                //validateQuotationFiles();
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
                                                    "<td class='text-center'>" + TimeFrameHeaderObj.PriorityLevelRemarks + "</td>";
                            if (ctrTimeFrame == 0) {
                                trTimeFrame += "<td class='text-center' colspan='3' rowspan='" + returnData.TimeFrameList.length + "' style='vertical-align: middle;'>" +
                                                    "<input type='date' class='form-control form-control-sm' id='Deadline' />" +
                                                "</td>";
                            }
                            trTimeFrame += "</tr>";
                            ctrTimeFrame++;
                        });

                        $("#trLevelOfPriorityHeader").before(trCriteria);
                        $("#trLevelOfPriorityHeader").after(trLevelOfPriority);
                        $("#trTimeFrameHeader").after(trTimeFrame);
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
        var Deadline = (Currentdate.getFullYear() + '-' + ((Currentdate.getMonth() > 8) ? (Currentdate.getMonth() + 1) : ('0' + (Currentdate.getMonth() + 1))) + '-' + ((Currentdate.getDate() > 9) ? Currentdate.getDate() : ('0' + Currentdate.getDate())));
        $("#Deadline").val(Deadline);
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
    $(".AddToContractorTable").change(function () {
        _.update(chosenContractors, function (obj) {
            obj.DateRequested = $("#DateRequested").val() || "";
            obj.QuotationSubmission = $("#QuotationSubmission").val() || "";
            obj.SurveryDateTime = $("#SurveryDateTime").val() || "";
        });
        console.log(chosenContractors);
    });
    var chosenContractors = [];
    var chosenFacilities = [];
    $('#tbl_suppliers').on('change', '.checkitems,#chkAllSupplier', function () {
        chosenContractors = [];
        $('.checkitems').each(function () {
            if ($(this).is(":checked")) {
                var rowData = tbl_suppliers.rows($(this).closest("tr")).data()[0];
                rowData.DateRequested = "";
                rowData.QuotationSubmission = "";
                rowData.SurveryDateTime = "";
                chosenContractors.push(rowData);
                $(this).prop('checked', true);
            }
        });
        chosenContractors = setDates(chosenContractors);
        console.log(chosenContractors);
    })
    $('#tblFacilities').on('change', '.checkFacilities,#chkAllFacilities', function () {
        chosenContractors = [];
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
            text: "Work Order Request will saved.",
            icon: "info",
            buttons: true,
        })
        .then((confirm) => {
            if (confirm) {
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
    function saveRequest() {
        $.ajax({
            url: '/WorkOrderRequest/Request/SaveWorkOrderRequestData',
            data: JSON.stringify({
                WorkOrderRequestData: WorkOrderRequestData
            }),

            type: 'POST',
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                if (returnData.success) {
                    swal({
                        title: "Iformation",
                        text: returnData.msg,
                        icon: "success",
                    })
                    clearWorkOrderForm();
                    tblRequest.ajax.reload(null, false)
                }
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
    }
    function setDates(arrData) {
        var CurrentDateRequested = new Date();
        var CurrentQuotationSubmission = new Date();
        var CurrentSurveryDateTime = new Date();
        CurrentDateRequested.setDate(CurrentDateRequested.getDate() + 1);
        CurrentQuotationSubmission.setDate(CurrentQuotationSubmission.getDate() + 2);
        CurrentSurveryDateTime.setDate(CurrentSurveryDateTime.getDate() + 3);
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
        $("#Deadline").val("");
    }
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
            { data: "DateRequested", name: "DateRequested" },
            { data: "SurveryDateTime", name: "SurveryDateTime" },
            { data: "QuotationSubmission", name: "QuotationSubmission" },
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
        columns: [
            { data: "DateRequest", name: "DateRequest" },
            { data: "WorkOrderTitle", name: "WorkOrderTitle" },
            { data: "ReferenceNo", name: "ReferenceNo" },
            { data: "WorkOrderNo", name: "WorkOrderNo" },
            { data: "EmployeeName", name: "EmployeeName" },
            { data: "DivisionName", name: "DivisionName" },
            { data: "DepartmentName", name: "DepartmentName" },
            { data: "SectionName", name: "SectionName" },
            { data: "Approved", name: "Approved" },
            { data: "ForApproval", name: "ForApproval" },
            { data: "Rejected", name: "Rejected" },
            { data: "RevisionNo", name: "RevisionNo" },
        ],

    });
});
