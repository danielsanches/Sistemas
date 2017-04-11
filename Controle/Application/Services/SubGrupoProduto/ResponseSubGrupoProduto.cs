namespace Application.Services.SubGrupoProduto
{
    using Domain.Model;
    using System.Collections.Generic;

    public class ResponseSubGrupoProduto
    {
        public ResponseSubGrupoProduto()
        {
            ListaSubGrupoProduto = new List<SubGrupoProduto>();
        }

        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }

        public SubGrupoProduto SubGrupoProduto { get; set; }
        public List<SubGrupoProduto> ListaSubGrupoProduto { get; set; }
        public int TotalInativos { get; internal set; }
        public int TotalAtivos { get; internal set; }
    }
}