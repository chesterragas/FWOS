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

    $("#editbtn").on("click", function () {
        $("#Updatebtn").show();
        $("#editbtn").hide();
    })

    $("#Updatebtn").hide();
    Initializepage();


    //$("#imgInp").on("change", UploadPic);
    $("#imgInp").on('change', function () {
        var files = new FormData();
        var file1 = document.getElementById("imgInp").files[0];
        files.append('files[0]', file1);
        $.ajax({
            type: 'POST',
            url: '/Home/UpdatePic',
            data: files,
            dataType: 'json',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
             
            },
            error: function (error) {
            
            }
        });

    });


    $(document).on('change', '.btn-file :file', function () {
        var input = $(this),
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [label]);
    });

    $('.btn-file :file').on('fileselect', function (event, label) {

        var input = $(this).parents('.input-group').find(':text'),
            log = label;

        if (input.length) {
            input.val(log);
        } else {
            if (log) alert(log);
        }

    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#img-upload').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#imgInp").change(function () {
        readURL(this);
    }); 	

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
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            MobileNo: $("#MobileNo").val(),
            LocalNo: $("#LocalNo").val(),
            Email: $("#Email").val(),
            Division: $("#DivisionID").val(),
            Department: $("#DepartmentID").val(),
            Section: $("#SectionID").val()
        },
        dataType: 'json',
        success: function (returnData) {
           
            swal("Profile Updated");
            location.reload();
        }
    });
}
