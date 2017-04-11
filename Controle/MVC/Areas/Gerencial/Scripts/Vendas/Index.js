jQuery(document).ready(function (e) {
    //InserirItem();
    Tooglee();
    $("#ClienteId").focus();
    $("#ClienteId").change(function () {
        Tooglee();
    });

    //Remover um item da tabela
    $('body').on('click', 'button.deleteDep', function () {
        $(this).parents('tr').remove();
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

function InserirItem() {
    var quantidade = jQuery("#Quantidade").val();
    var codigoProduto = jQuery("#CodigoProduto").val();

    jQuery.post(content + "/Gerencial/Vendas/ObterInserirItemGrid?quantidade=" + quantidade + "&codigoProduto=" + codigoProduto)
    .success(function (data) {
        if (data.data != "Erro") {
            $("#LabelSubTotal").html("SubTotal: " + data.data.SubTotal);
            $("#Quantidade").val(data.data.Quantidade);
            $("#CodigoProduto").val("");
            $("#CodigoProduto").focus();
            $("#TableVendas tbody tr").remove();
            InseriItemTable(data.data);
        }
        //else {
        //    alert(data.message);
        //}
    });
}

function InseriItemTable(data) {
    var row = "";
    for (var i = 0; i < data.ListaVendas.length; i++) {
        row += '<tr>' +
                   '<td>' + data.ListaVendas[i].CodigoBarra + '</td>' +
                   '<td>' + data.ListaVendas[i].Descricao + '</td>' +
                   '<td>' + data.ListaVendas[i].Quantidade + '</td>' +
                   '<td>' + data.ListaVendas[i].ValorItemFormatado + '</td>' +
                   '<td>' + data.ListaVendas[i].TotalItemFormatado + '</td>' +
                   '<td><button class="deleteDep btn btn-sm btn-default" onclick="RemoverItem(' + data.ListaVendas[i].CodigoBarra + ');">Remover</button></td>'
        '</tr>';
    };

    $("#TableVendas").append(row);
}

function RemoverItem(id) {
    jQuery.post(content + "/Gerencial/Vendas/RemoverItemGrid?id=" + id)
   .success(function (data) {
       $("#LabelSubTotal").html("SubTotal: " + data.SubTotal);
       $("#Quantidade").val(data.Quantidade);
       $("#CodigoProduto").focus();
   });
}

function LimparCampos() {
    $("#CodigoProduto").val("");
    $("#ProdutoId").select2("val", "");
    $("#Quantidade").val("");
}

function Tooglee() {
    if ($("#ClienteId").val() == "") {
        document.getElementById("Quantidade").readOnly = true;
        document.getElementById("CodigoProduto").readOnly = true;
    } else {
        document.getElementById("Quantidade").readOnly = false;
        document.getElementById("CodigoProduto").readOnly = false;
        $("#CodigoProduto").focus();
    }
}