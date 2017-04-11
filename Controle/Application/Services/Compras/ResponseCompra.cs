using Domain.Model;
using System.Collections.Generic;

namespace Application.Services.Compras
{
    public class ResponseCompra
    {
        public ResponseCompra()
        {
            ListaCompras = new List<Compra>();
        }

        public string Mensagem { get; internal set; }
        public bool Sucesso { get; internal set; }

        public Compra Compra { get; set; }

        public List<Compra> ListaCompras { get; set; }
    }
}