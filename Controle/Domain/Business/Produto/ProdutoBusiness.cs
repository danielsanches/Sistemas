namespace Domain.Business.Produto
{
    using Enum;
    using System;
    using System.Linq;

    public class ProdutoBusiness
    {
        public void ValidarCadastro(RequestProdutoBusiness request)
        {
            if (string.IsNullOrWhiteSpace(request.Produto.Descricao))
                throw new InvalidOperationException("Não é possível cadastrar um produto com descrição inválida.");

            if (request.Cadastrar && request.Produto.Status == EnumSituacao.Inativo.ToString())
                throw new InvalidOperationException("Não é possível cadastrar um produto com status inativo.");

            if (request.Cadastrar && request.ListaProdutos.Any(x => x.Descricao.Trim().Equals(request.Produto.Descricao.Trim())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");

            if (!request.Cadastrar && request.ListaProdutos.Any(x => x.Id != request.Produto.Id && x.Descricao.Trim().Equals(request.Produto.Descricao.Trim())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");
        }
    }
}
