$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $(".auth-dialog").on("click", function (e) {
        e.preventDefault();

        $("<div id='auth-dialog-content'></div>")
            .addClass("dialog")
            .appendTo("body")
            .load(this.href)
            .dialog({
                title: $(this).attr("auth-dialog-title"),
                width: "400",
                resizable: false,
                close: function () { $(this).remove() },
                modal: true,
                buttons: {
                    "Войти": function () {
                        $.ajax({
                            url: "/Account/Login",
                            type: "POST",
                        data: $('form').serialize(),
                        datatype: "json",
                        success: function (result) {
                            $("#auth-dialog-content").html(result);
                        }
                    });
                }
            }
        });
    });

    $(".close").on("click", function (e) {
        e.preventDefault();
        $(this).closest(".dialog").dialog("close");
    });
});