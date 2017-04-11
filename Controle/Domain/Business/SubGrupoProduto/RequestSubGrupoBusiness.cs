namespace Domain.Business.SubGrupoProduto
{
    using Domain.Model;
    using System.Collections.Generic;

    public class RequestSubGrupoBusiness
    {
        public RequestSubGrupoBusiness()
        {
            ListaSubGrupoProduto = new List<SubGrupoProduto>();
        }

        public bool Cadastrar { get; set; }

        public SubGrupoProduto SubGrupoProduto { get; set; }

        public List<SubGrupoProduto> ListaSubGrupoProduto { get; set; }
    }
}