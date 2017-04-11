namespace Application.Services.Estoque
{
    using Domain.Model;
    using System.Collections.Generic;

    public class ResponseEstoque
    {
        public ResponseEstoque()
        {
            ListaEstoque = new List<Estoque>();
        }

        public string Mensagem{ get; set; }
        public bool Sucesso { get; set; }
        public Estoque Estoque { get; set; }
        public List<Estoque> ListaEstoque { get; set; }
    }
}