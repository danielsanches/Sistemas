namespace Application.Services.Fornecedor
{
    using Domain.Business.Fornecedor;
    using Domain.Enum;
    using Domain.Interfaces;
    using Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FornecedorAppService
    {
        private IRepository<Fornecedor> _fornecedorRepository;
        private FornecedorBusiness _fornecedorBusiness;

        public FornecedorAppService(IRepository<Fornecedor> fornecedorRepository, FornecedorBusiness fornecedorBusiness)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorBusiness = fornecedorBusiness;
        }

        public ResponseFornecedor Cadastrar(RequestFornecedor request)
        {
            try
            {
                var fornecedor = new Fornecedor
                {
                    NomeFantasia = request.NomeFantazia,
                    Celular = request.Celular,
                    FoneFixo = request.FoneFixo,
                    Email = request.Email
                };

                ValidarCamposFornecedor(fornecedor, true);

                fornecedor.Celular = !string.IsNullOrWhiteSpace(fornecedor.Celular) ? fornecedor.Celular.Replace("-", "").Replace(".", "") : "";
                fornecedor.FoneFixo = !string.IsNullOrWhiteSpace(fornecedor.FoneFixo) ? fornecedor.FoneFixo.Replace("-", "").Replace(".", "") : "";
                fornecedor.NomeFantasia = fornecedor.NomeFantasia.ToUpper();

                _fornecedorRepository.Cadastrar(fornecedor);
                _fornecedorRepository.SaveChanges();

                return new ResponseFornecedor { Sucesso = true, Mesagem = "Fornecedor cadastrado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseFornecedor { Sucesso = false, Mesagem = ex.Message };
            }
        }

        public ResponseFornecedor Alterar(RequestFornecedor request)
        {
            try
            {
                var fornecedor = _fornecedorRepository.Obter(request.Id);

                fornecedor.NomeFantasia = request.NomeFantazia;
                fornecedor.Celular = request.Celular;
                fornecedor.FoneFixo = request.FoneFixo;
                fornecedor.Email = request.Email;
                fornecedor.Status = request.Status;

                ValidarCamposFornecedor(fornecedor);

                fornecedor.Celular = !string.IsNullOrWhiteSpace(fornecedor.Celular) ? fornecedor.Celular.Replace("-", "").Replace(".", "") : "";
                fornecedor.FoneFixo = !string.IsNullOrWhiteSpace(fornecedor.FoneFixo) ? fornecedor.FoneFixo.Replace("-", "").Replace(".", "") : "";

                fornecedor.NomeFantasia = fornecedor.NomeFantasia.ToUpper();
                _fornecedorRepository.Alterar(fornecedor);
                _fornecedorRepository.SaveChanges();

                return new ResponseFornecedor { Sucesso = true, Mesagem = "Fornecedor alterado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseFornecedor { Sucesso = false, Mesagem = ex.Message };
            }
        }

        public ResponseFornecedor Obter(int id)
        {
            try
            {
                var fornecedor = _fornecedorRepository.Obter(id);
                if (fornecedor == null)
                    throw new Exception("Fornecedor não encontrado.");

                return new ResponseFornecedor { Sucesso = true, Fornecedor = fornecedor };
            }
            catch (Exception ex)
            {
                return new ResponseFornecedor { Sucesso = false, Mesagem = ex.Message };
            }
        }

        public ResponseFornecedor ObterLista(RequestFornecedor request)
        {
            try
            {
                var lista = ListarForncedor(request).Where(x => x.Status.ToUpper().Equals(request.Status.ToUpper())).ToList();

                return new ResponseFornecedor { Sucesso = true, ListaFornecedores = lista };
            }
            catch (Exception ex)
            {
                return new ResponseFornecedor { Sucesso = false, Mesagem = ex.Message };
            }
        }

        public ResponseFornecedor ObterListaAtivos()
        {
            try
            {
                var lista = _fornecedorRepository.ObterPor(x => x.Status.Equals("Ativo")).ToList();

                return new ResponseFornecedor { Sucesso = true, ListaFornecedores = lista };
            }
            catch (Exception ex)
            {
                return new ResponseFornecedor { Sucesso = false, Mesagem = ex.Message };
            }
        }

        public ResponseFornecedor ObterQuantidades(RequestFornecedor request)
        {
            try
            {
                var lista = ListarForncedor(request).ToList();
                var qtdAtivo = lista.Count(x => x.Status.Equals(EnumSituacao.Ativo.ToString()));
                var qtdInativo = lista.Count(x => x.Status.Equals(EnumSituacao.Inativo.ToString()));

                return new ResponseFornecedor { Sucesso = true, QtdAtivos = qtdAtivo, QtdInativos = qtdInativo };
            }
            catch (Exception ex)
            {
                return new ResponseFornecedor { Sucesso = false, Mesagem = ex.Message };
            }
        }

        private List<Fornecedor> ListarForncedor(RequestFornecedor request)
        {
            var descricaoVazia = string.IsNullOrWhiteSpace(request.NomeFantazia);
            var lista = _fornecedorRepository.ObterPor(x => descricaoVazia ? x.NomeFantasia != null : x.NomeFantasia.Trim().Equals(request.NomeFantazia)).ToList();
            return lista;
        }

        private void ValidarCamposFornecedor(Fornecedor fornecedor, bool cadastro = false)
        {
            var lista = _fornecedorRepository.ObterTodos().ToList();
            _fornecedorBusiness.ValidarCadastro(new RequestFornecedorBusiness
            {
                Cadastrar = cadastro,
                Fornecedor = fornecedor,
                ListaFornecedor = lista
            });
        }
    }
}
