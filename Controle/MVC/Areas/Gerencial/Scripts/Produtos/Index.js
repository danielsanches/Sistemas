jQuery(document).ready(function () {
    CarregarAbas();
    CarregarQtdAbas();
    PreencherGrid();
    Leitor();
});

function Leitor() {
    $("#CodigoBarra").keydown(function (e) {
        if (e.which == "17" || e.which == "74" || e.which == "13" || e.which == "10") {
            e.preventDefault();
        } else {
            console.log(e.which);
        }
    });
}

function PreencherGrid() {
    var params = PreencherParamentros();
    var situacao = jQuery("#myTabs .active")[0].id;

    jQuery('#GridProdutos').DataTable({
        responsive: true,
        searching: false,
        "ajax": {
            "url": "/Gerencial/Produtos/ObterProdutosGrid?descricao=" + params.descricao +
                "&codigo=" + params.codigo +
                "&situacao=" + situacao,
            "type": "POST"
        },
        "columns": [
            { "data": "Id", "visible": false },
            { "data": "CodigoBarra" },
            { "data": "Descricao" },
            { "data": "ValorAtacado" },
            { "data": "PercentualVenda" },
            { "data": "ValorVenda" },
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
    return "<a class=\"btn btn-primary btn-sm \"href=\"Produtos/Alterar/" + row.Id + "\">Alterar&nbsp; <i class=\"glyphicon glyphicon-edit\"></i></a> ";
}

function PreencherParamentros() {

    var codigo = document.getElementById("CodigoBarra").value;
    var descricao = document.getElementById("Descricao").value;

    var params = [];

    params = { codigo: codigo, descricao: descricao };

    return params;
}

function RecarregarTabela() {

    var table = jQuery("#GridProdutos").DataTable();

    var params = PreencherParamentros();

    var situacao = jQuery("#myTabs .active")[0].id;

    table.ajax.url("/Gerencial/Produtos/ObterProdutosGrid?descricao=" + params.descricao +
                "&codigo=" + params.codigo +
                "&situacao=" + situacao).load();
}

function CarregarAbas() {

    jQuery("#myTabs a").click(function (e) {
        e.preventDefault();

        jQuery(this).tab('show');
        var params = PreencherParamentros();

        if (params.descricao !== "" || params.codigo !== "") {
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
        url: "/Gerencial/Produtos/ObterQuantidadeAbas?descricao=" + params.descricao +
                "&codigo=" + params.codigo +
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

    document.getElementById("CodigoBarra").value = "";
    document.getElementById("Descricao").value = "";

    CarregarQtdAbas();
    RecarregarTabela();
}

function LimparCamposModal() {
    jQuery('#Descricao').val("");
    jQuery('#CodigoBarra').val("");
}