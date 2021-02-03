$(function () {
    var str = location.href.toLowerCase();
    $("#themenu li a").each(function () {
        if (str.indexOf(this.href.toLowerCase()) > -1) {
            $(this).parent("li").addClass("active");
           
        }
    });

    var link = str.toLowerCase();
    var res = link.split("/");

    var page = "";
    switch (res[3]) {
        case "mastermodule": page = "mastermodule"
            break;
        //case "correction": page = "correction"
        //    break;
        //case "summary": page = "summary"
        //    break;
        //case "forecast": page = "forecast"
        //    break;
    }

    //if (str.includes("masters")) {
    if (page == "mastermodule") {
        $("li").removeClass("menu-open");
        $('#Mastermodule').addClass("menu-open");
        $("#Mastermodule").children().show();

        //if (str.includes("/employee/employee")) {
        if(res[4] == "employee" && res[5] == "employee"){
            $("#dashi").removeClass("active menu-open");
        }
    }
 
    $("#useruploadpic").on("click", function () {
        $("#userpicto").click();
    })

    $(".file-userphoto").on('change', function () {
        var files = new FormData();
        var file1 = document.getElementById("userpicto").files[0];
        files.append('files[0]', file1);
        var sa = this;
        $.ajax({
            type: 'POST',
            url: '/MasterModule/Users/UploadEmployeePhoto',
            data: files,
            dataType: 'json',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                readURLlayout(sa);
                location.reload();
            },
            error: function (error) {
                $('#uploadMsg').text('Error has occured. Upload is failed');
            }
        });

    });
})

function readURLlayout(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#layoutpic').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}