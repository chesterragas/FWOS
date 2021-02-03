$(document).ready(function () {
    Dropdown_select('DivisionID', "/Helper/GetDropdown_Division");

    $("#DivisionID").on("change", function () {

        Dropdown_select('DepartmentID', "/Helper/GetDropdown_Department?DivisionID=" + $("#DivisionID").val());
    });

    $("#DepartmentID").on("change", function () {

        Dropdown_select('SectionID', "/Helper/GetDropdown_Section?DepartmentID=" + $("#DepartmentID").val());
    });

   
   

    $('#RegisterForm').on('submit', function (e) {
        e.preventDefault();
        RegisterData($(this));
    });


});

function RegisterData(data) {
    var datanow = data.serialize();
    $.ajax({
        url: '../Login/RegisterUser',
        data: datanow,
        type: 'POST',
        datatype: "json",
        success: function (returnData) {
            window.location = "/";
        }

    });
}