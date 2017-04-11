jQuery(document).ready(function () {
    CarregarAbas();
    CarregarQtdAbas();
    PreencherGrid();

    jQuery("#DataNascimento").mask("99/9999")

    jQuery("#datetimepickerNascimento").datetimepicker({
        locale: 'pt',
        format: 'MM/YYYY'
    });
});

function PreencherGrid() {
    var params = PreencherParamentros();
    var situacao = jQuery("#myTabs .active")[0].id;

    jQuery("#GridClientes").DataTable({
        responsive: true,
        searching: false,
        "ajax": {
            "url": "/Gerencial/Clientes/ObterItensGrid?descricao=" + params.descricao +
                "&aniverssario=" + params.aniverssario +
                "&situacao=" + situacao,
            "type": "POST"
        },
        "columns": [
            { "data": "Id" },
            { "data": "Nome" },
            { "data": "Cpf" },
            { "data": "Email" },
            { "data": "FoneMovel1" },
            { "data": "FoneMovel2" },
            { "data": "FoneFixo" },
            { "data": "DataNascimentoFormatada" },
            { "data": "DataCadastroFormatada" },
            {
                "render": function (data, type, row) {
                    var retorno = getButtonsGrid(row);
                    return retorno;
                }
            }
        ]
    });
}

function getButtonsGrid(row) {
    return "<a class=\"btn btn-primary btn-sm \"href=\"Clientes/Alterar/" + row.Id + "\">Alterar&nbsp; <i class=\"glyphicon glyphicon-edit\"></i></a> ";
}

function PreencherParamentros() {

    var descricao = document.getElementById("Nome").value;
    var aniverssario = document.getElementById("DataNascimento").value;

    var params = [];

    params = { descricao: descricao, aniverssario: aniverssario };

    return params;
}

function RecarregarTabela() {

    var table = jQuery("#GridClientes").DataTable();

    var params = PreencherParamentros();

    var situacao = jQuery("#myTabs .active")[0].id;

    table.ajax.url("/Gerencial/Clientes/ObterItensGrid?descricao=" + params.descricao +
                "&aniverssario=" + params.aniverssario +
                "&situacao=" + situacao).load();
}

function CarregarAbas() {

    jQuery("#myTabs a").click(function (e) {
        e.preventDefault();

        jQuery(this).tab('show');
        var params = PreencherParamentros();

        if (params.descricao !== "") {
            CarregarQtdAbas();
        }

        RecarregarTabela();
    });
}

function CarregarQtdAbas() {

    var params = PreencherParamentros();
    var situacao = jQuery("#myTabs .active")[0].id;

    jQuery.ajax(
    {
        url: "/Gerencial/Clientes/ObterQuantidadeAbas?descricao=" + params.descricao +
                "&aniverssario=" + params.aniverssario +
                "&situacao=" + situacao,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            jQuery("#ativo-tab").text("Ativos (" + (data["ativo"] || "0") + ")");
            jQuery("#inativo-tab").text("Inativos (" + (data["inativo"] || "0") + ")");
        }
    });
}

function CarregarAbasFiltro() {

    CarregarQtdAbas();
    RecarregarTabela();
}

function CarregarAbasFiltroReset() {
    document.getElementById("Nome").value = "";
    document.getElementById("DataNascimento").value = "";

    CarregarQtdAbas();
    RecarregarTabela();
}
