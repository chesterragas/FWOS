$(document).ready(function () {
    
    $('#Loginbtn').on('click', function (e) {
        e.preventDefault();
        window.location = "/Login/SignIn?Username="+$("#Username").val() + "&password=" + $("#Password").val() + "&isChecked=" + $("#remember_checkbox").is(":checked");
    });

   
});