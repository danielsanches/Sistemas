namespace Domain.Business.SubGrupoProduto
{
    using System;
    using System.Linq;

    public class SubGrupoProdutoBusiness
    {
        public void ValidarCadastro(RequestSubGrupoBusiness request)
        {
            if (string.IsNullOrEmpty(request.SubGrupoProduto.Descricao))
                throw new InvalidOperationException("Não é possível cadastrar um subgrupo sem descrição.");

            if (!request.SubGrupoProduto.GrupoProdutoId.HasValue || request.SubGrupoProduto.GrupoProdutoId <= 0)
                throw new InvalidOperationException("Não é possível cadastrar um subgrupo sem grupo de produto");

            if (request.Cadastrar && request.ListaSubGrupoProduto.Any(x => x.Descricao.Trim().ToUpper().Equals(request.SubGrupoProduto.Descricao.Trim().ToUpper())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");

            if (!request.Cadastrar && request.ListaSubGrupoProduto.Any(x => x.Id != request.SubGrupoProduto.Id && x.Descricao.Trim().ToUpper().Equals(request.SubGrupoProduto.Descricao.Trim().ToUpper())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");
        }
    }
}
