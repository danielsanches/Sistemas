
$(document).ready(function () {

    
    $(".navbar-expand-toggle").click(function () {
        $(".app-container").toggleClass("expanded");
        $(".navbar-expand-toggle").toggleClass("fa-rotate-90");
    });
    $(".navbar-right-expand-toggle").click(function () {
        $(".navbar-right").toggleClass("expanded");
        $(".navbar-right-expand-toggle").toggleClass("fa-rotate-90");
    });

    
    $('form').submit(function () {
        $.blockUI({
            message: '<h4>Salvando...</h4>',
        });
    });


    $('.checkbox-switch').bootstrapSwitch({
        size: "small",
        onText: "SIM",
        offText: "NÃO",
        handleWidth: 26
    });
    

    $('select').select2({
        placeholder: "Selecione um item...",
        allowClear: true
    });

    
    $('.datatable').DataTable({
        "dom": '<"top"fl<"clear">>rt<"bottom"ip<"clear">>'
    });
    
    $(".side-menu .nav .dropdown").on('show.bs.collapse', function () {
        $(".side-menu .nav .dropdown .collapse").collapse('hide');
    });

    jQuery(".calendario").mask("99/99/9999");
    jQuery(".calendario").datetimepicker({
        locale: 'pt',
        format: 'DD/MM/YYYY'
    });

    $('.foneMask').focusout(function () {
        var phone, element;
        element = $(this);
        element.unmask();
        phone = element.val().replace(/\D/g, '');
        if (phone.length > 10) {
            element.mask("99 99999-999?9");
        } else {
            element.mask("99 9999-9999?9");
        }
    }).trigger('focusout');
});