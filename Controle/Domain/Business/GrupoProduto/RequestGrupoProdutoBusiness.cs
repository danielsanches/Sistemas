namespace Domain.Business.GrupoProduto
{
    using Domain.Model;
    using System.Collections.Generic;

    public class RequestGrupoProdutoBusiness
    {
        public RequestGrupoProdutoBusiness()
        {
            ListaGrupos = new List<GrupoProduto>();
        }

        public bool Cadastrar { get; set; }

        public GrupoProduto GrupoProduto { get; set; }

        public List<GrupoProduto> ListaGrupos { get; set; }
    }
}