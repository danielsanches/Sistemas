namespace Domain.Business.Fornecedor
{
    using Enum;
    using System;
    using System.Linq;

    public class FornecedorBusiness
    {
        private ValidacoesCadastro _validar;

        public FornecedorBusiness()
        {
            _validar = new ValidacoesCadastro();
        }

        public void ValidarCadastro(RequestFornecedorBusiness request)
        {
            if (string.IsNullOrWhiteSpace(request.Fornecedor.NomeFantasia))
                throw new InvalidOperationException("Favor informar uma descrição para o fornecedor.");

            if (string.IsNullOrWhiteSpace(request.Fornecedor.Celular) && string.IsNullOrWhiteSpace(request.Fornecedor.FoneFixo))
                throw new InvalidOperationException("Favor informar um telefone fixo ou móvel ao fornecedor.");

            if (request.Cadastrar && request.Fornecedor.Status.Equals(EnumSituacao.Inativo.ToString()))
                throw new InvalidOperationException("Não é possível cadastrar um fornecedor com status inativo.");

            if (!string.IsNullOrWhiteSpace(request.Fornecedor.Email) && !new ValidacoesCadastro().ValidarEmail(request.Fornecedor.Email))
                throw new InvalidOperationException("Não é possível cadastrar um fornecedor com email inválido.");

            if (request.Cadastrar == true && request.ListaFornecedor.Any(x => x.NomeFantasia.Trim().Equals(request.Fornecedor.NomeFantasia.Trim())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");

            if (!request.Cadastrar && request.ListaFornecedor.Any(x => x.Id != request.Fornecedor.Id && x.NomeFantasia.Trim().Equals(request.Fornecedor.NomeFantasia.Trim())))
                throw new InvalidOperationException("A descrição informada já esta sendo usada. Favor informar outra descrição.");

            if (!string.IsNullOrEmpty(request.Fornecedor.CpfCnpj) && !_validar.ValidarCpfCnpj(request.Fornecedor.CpfCnpj))
                throw new InvalidOperationException("Favor informar um CPF ou CNPJ válido.");
        }
    }
}
