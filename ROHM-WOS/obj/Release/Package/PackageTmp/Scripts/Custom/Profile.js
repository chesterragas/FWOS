$(function () {
    Dropdown_select('DivisionID', "/Helper/GetDropdown_Division");
    $("#DivisionID").on("change", function () {
        Dropdown_select('DepartmentID', "/Helper/GetDropdown_Department?DivisionID=" + $("#DivisionID").val());
    });
    $("#DepartmentID").on("change", function () {
        Dropdown_select('SectionID', "/Helper/GetDropdown_Section?DepartmentID=" + $("#DepartmentID").val());
    });

    $(".groups").hide();

    $("#editbtn").on("click", function () {
        $(".groupslabel").hide();
        $(".groups").show();
        $(".groups").prop("disabled", false);
        $(".groupsdata").prop("disabled", false);
        
    });
    $("#Updatebtn").on("click", UpdateProfile);

    
    Initializepage();
});

function Initializepage() {

    $.ajax({
        url: '../Home/GetUserProfile',
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            console.log(returnData);
            $("#FirstName").val(returnData.data.FirstName);
            $("#LastName").val(returnData.data.LastName);
            $("#MobileNo").val(returnData.data.MobileNo);
            $("#LocalNo").val(returnData.data.LocalNo);
            $("#Email").val(returnData.data.Email);
            $("#Division").val(returnData.data.Division);
            $("#Department").val(returnData.data.Department);
            $("#Section").val(returnData.data.Section);
        }
    });

}

function UpdateProfile() {
    $.ajax({
        url: '../Home/UpdateProfile',
        data: {
            FirstName : $("#FirstName").val(),
            LastName : $("#LastName").val(),
            MobileNo : $("#MobileNo").val(),
            LocalNo : $("#LocalNo").val(),
            Email:$("#Email").val(),
            Division: $("#DivisionID").val(),
            Department: $("#DepartmentID").val(),
            Section: $("#SectionID").val()
        },
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
           
           
        }
    });
}