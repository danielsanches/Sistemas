namespace Domain.Business.GrupoProduto
{
    using Enum;
    using System;
    using System.Linq;

    public class GrupoProdutoBusiness
    {
        public void ValidarCadastro(RequestGrupoProdutoBusiness request)
        {
            if (string.IsNullOrWhiteSpace(request.GrupoProduto.Descricao))
                throw new InvalidOperationException("Favor informar uma descrição para o grupo de produtos.");

            if (request.Cadastrar && request.GrupoProduto.Status.Equals(EnumSituacao.Inativo.ToString()))
                throw new InvalidOperationException("Não é possível cadastrar um grupo de fornecedor com status inativo.");

            if (request.Cadastrar && request.ListaGrupos.Any(x => x.Descricao.Trim().ToUpper().Equals(request.GrupoProduto.Descricao.Trim().ToUpper())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");

            if (!request.Cadastrar && request.ListaGrupos.Any(x => x.Id != request.GrupoProduto.Id && x.Descricao.Trim().ToUpper().Equals(request.GrupoProduto.Descricao.Trim().ToUpper())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");
        }
    }
}
