jQuery(document).ready(function () {
    CarregarAbas();
    CarregarQtdAbas();
    PreencherGrid();
});

function PreencherGrid() {
    var params = PreencherParamentros();
    var situacao = jQuery("#myTabs .active")[0].id;

    jQuery('#GridSubGrupoProdutos').DataTable({
        responsive: true,
        searching: false,
        "ajax": {
            "url": "/Gerencial/SubGrupoProdutos/ObterSubGruposGrid?descricao=" + params.descricao +
                "&situacao=" + situacao,
            "type": "POST"
        },
        "columns": [
            { "data": "Id" },
            { "data": "Descricao" },
            { "data": "DescricaoGrupo" },
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
    return "<a class=\"btn btn-primary btn-sm \"href=\"SubGrupoProdutos/Alterar/" + row.Id + "\">Alterar&nbsp; <i class=\"glyphicon glyphicon-edit\"></i></a> ";
}

function PreencherParamentros() {

    var descricao = document.getElementById("Descricao").value;

    var params = [];

    params = { descricao: descricao };

    return params;
}

function RecarregarTabela() {

    var table = jQuery("#GridSubGrupoProdutos").DataTable();

    var params = PreencherParamentros();

    var situacao = jQuery("#myTabs .active")[0].id;

    table.ajax.url("/Gerencial/SubGrupoProdutos/ObterSubGruposGrid?descricao=" + params.descricao +
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
        url: "/Gerencial/SubGrupoProdutos/ObterQuantidadeAbas?descricao=" + params.descricao +
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
    document.getElementById("Descricao").value = "";

    CarregarQtdAbas();
    RecarregarTabela();
}

function LimparCamposModal() {
    jQuery('#Descricao').val("");
}