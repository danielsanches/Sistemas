namespace Application.Services.GrupoProduto
{
    using Domain.Model;
    using System.Collections.Generic;

    public class ResponseGrupoProduto
    {
        public ResponseGrupoProduto()
        {
            ListaGrupoProduto = new List<GrupoProduto>();
        }

        public string Mensagem { get; set; }

        public bool Sucesso { get; set; }

        public int QtdAtivo { get; set; }

        public int QtdInativo { get; set; }

        public GrupoProduto GrupoProduto { get; set; }

        public List<GrupoProduto> ListaGrupoProduto { get; set; }
        public int TotalAtivos { get; internal set; }
        public int TotalInativos { get; internal set; }
    }
}