jQuery(document).ready(function () {

    jQuery("#GridEstoque").DataTable({
        responsive: true,
        searching: false,
        "ajax": {
            "url": "/Gerencial/Estoque/ObterItensGrid",
            "type": "POST"
        },
        "columns": [
            { "data": "ProdutoId", "visible": false },
            { "data": "CodigoBarra" },
            { "data": "Descricao" },
            { "data": "Quantidade" }
        ]
    });
    Leitor();
});

function Leitor() {
    $("#CodigoProduto").keydown(function (e) {
        if (e.which == "17" || e.which == "74" || e.which == "10") {
            e.preventDefault();
        }
    });
}
