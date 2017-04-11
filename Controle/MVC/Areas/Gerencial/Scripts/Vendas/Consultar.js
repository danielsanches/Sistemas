jQuery(document).ready(function () {
    PreencherGrid();

    jQuery("#DataFim").mask("99/99/9999")
    jQuery("#DataInicio").mask("99/99/9999")

    jQuery("#datetimepickerInicio").datetimepicker({
        locale: 'pt',
        format: 'DD/MM/YYYY'
    });
    jQuery("#datetimepickerFim").datetimepicker({
        locale: 'pt',
        format: 'DD/MM/YYYY'
    });
});

function PreencherGrid() {
    var table = jQuery('#GridVendas').DataTable({
        responsive: true,
        searching: false,        
        "ajax": {
            "url": "/Gerencial/Vendas/ObterItensGrid",
            "type": "POST",
            "data": PreencherParamentros
        },
        "columns": [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            { "data": "Id" },
            { "data": "DataVenda" },
            { "data": "ClienteDescricao" },
            { "data": "SubTotal" },
            {
                "render": function (data, type, row) {
                    var retorno = getButtonsGrid(row);
                    return retorno;
                }
            }
        ],
        "order": [[1, 'asc']]
    });

    $('#GridVendas tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(formatChild(row.data())).show();
            tr.addClass('shown');
        }
    });
}

function formatChild(d) {
    var table = '<table id="tableChild" class=" table table-striped table-bordered" cellpadding="4" cellspacing="0" border="0" >' +
         '<thead>' +
              '<tr>' +
                  '<th>Produto</th>' +
                  '<th>Qtd</th>' +
                  '<th>Valor Unt</th>' +
                  '<th>Valor Total</th>' +
              '</tr>' +
         '<thead>' +
         '<tbody>';

    for (var i = 0; i < d.ListaVendas.length; i++) {
        table += '<tr>' +
        '<td>' + d.ListaVendas[i].Descricao + '</td>' +
        '<td>' + d.ListaVendas[i].Quantidade + '</td>' +
        '<td>' + d.ListaVendas[i].ValorItem + '</td>' +
        '<td>' + d.ListaVendas[i].TotalItem + '</td>' +
     '</tr>';
    };

    table += '</tbody></table>';
    return table;
}

function getButtonsGrid(row) {
    return "<a class=\"btn btn-primary btn-sm\" target=\"_blank\" href=\"Imprimir/" + row.Id + "\">Imprimir&nbsp; <i class=\"glyphicon glyphicon-print\"></i></a> ";
}

function PreencherParamentros() {
    return jQuery("#formulario").serializeArray();
}

function RecarregarTabela() {
    jQuery("#GridVendas").DataTable().ajax.reload();
}

function RecarregarComFiltro() {
    RecarregarTabela();
}

function FiltroReset() {
    jQuery("#DataInicio").val("")
    jQuery("#DataFim").val("")
    jQuery("#ClienteId").select2("val", "");
    RecarregarTabela();
}