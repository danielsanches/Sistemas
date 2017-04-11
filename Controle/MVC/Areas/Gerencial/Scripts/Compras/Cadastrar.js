jQuery(document).ready(function () {

    jQuery("#DataCompra").mask("99/99/9999")

    jQuery("#datetimepickerCompra").datetimepicker({
        locale: 'pt',
        format: 'DD/MM/YYYY'
    });

    jQuery("#ValorProduto").maskMoney({ thousands: '.', decimal: ',' });
});