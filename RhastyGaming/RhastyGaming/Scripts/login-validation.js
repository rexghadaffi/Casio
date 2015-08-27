function ValidateLoginFields(e) {

    var checker = 0;

    $(".textclear").each(function () {        
        if ($(this).val().trim() == "") {
            $(this).parent().addClass("error");
            $(this).popover('show');
            $("#Username").focus();
            e.preventDefault();
            checker++;
        }
    });

    if (checker == 0)
    {
       
    }
    
}

$(".textclear").change(function () {
    $(this).parent().removeClass("error");    
});