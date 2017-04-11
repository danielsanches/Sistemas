
$(document).ready(function () {
    $("#NomeLogin").focus();

    $('form').submit(function () {
        $.blockUI({
            message: '<h4>Autenticando usuário...</h4>',
        });
    });
});