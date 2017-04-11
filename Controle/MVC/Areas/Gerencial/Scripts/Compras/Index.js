jQuery(document).ready(function () {
    PreencherGrid();

    jQuery("#DataFim").mask("99/99/9999");
    jQuery("#DataInicio").mask("99/99/9999");

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
    var table = jQuery('#GridCompras').DataTable({
        responsive: true,
        searching: false,
        "ajax": {
            "url": "/Gerencial/Compras/ObterItensGrid",
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
            { "data": "DataCompraFormatada" },
            { "data": "DataLancamentoFormatada" },
            { "data": "FornecedorDescricao" },
            { "data": "ValorCompra" },
            {
                "render": function (data, type, row) {
                    var retorno = getButtonsGrid(row);
                    return retorno;
                }
            }
        ],
        "order": [[1, 'asc']]
    });

    $('#GridCompras tbody').on('click', 'td.details-control', function () {
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
                  '<th>Descrição</th>' +
                  '<th>Qtd</th>' +
                  '<th>Valor Unt</th>' +
                  '<th>Valor Total</th>' +
              '</tr>' +
         '<thead>' +
         '<tbody>';

    for (var i = 0; i < d.ListaItens.length; i++) {
        table += '<tr>' +
        '<td>' + d.ListaItens[i].Descricao + '</td>' +
        '<td>' + d.ListaItens[i].Quantidade + '</td>' +
        '<td>' + d.ListaItens[i].ValorUnitario + '</td>' +
        '<td>' + d.ListaItens[i].ValorTotal + '</td>' +
     '</tr>';
    };

    table += '</tbody></table>';
    return table;
}

function getButtonsGrid(row) {
    return "<a class=\"btn btn-primary btn-sm \"href=\"Compras/Remover/" + row.Id + "\">Remover&nbsp; <i class=\"glyphicon glyphicon-trash\"></i></a> ";
}

function PreencherParamentros() {
    return jQuery("#formularioCompras").serializeArray();
}

function RecarregarTabela() {
    jQuery("#GridCompras").DataTable().ajax.reload();
}

function RecarregarComFiltro() {
    RecarregarTabela();
}

function FiltroReset() {
    jQuery("#DataInicio").val("");
    jQuery("#DataFim").val("");
    jQuery("#FornecedorId").select2("val", "");
    RecarregarTabela();
}