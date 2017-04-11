jQuery(document).ready(function () {
    jQuery("#GrupoProduto").change(
                function () {
                    jQuery.ajax({
                        type: "POST",
                        url: "/Gerencial/Produtos/ObterListaComboSubGrupoGrupo?id=" + jQuery("#GrupoProduto").val(),
                        success: function (data) {

                            var lista = jQuery("#ListaSubGrupo");

                            lista.select2("val", "");
                            lista.empty();
                            lista.append("<option value=''/>");

                            for (var i = 0; i < data.length; i++) {
                                var option = $("<option />");
                                option.attr('value', data[i].Id).text(data[i].Descricao);
                                lista.append(option);
                            }
                        }
                    });
                }
            );
});