namespace Application.Services.Compras
{
    using System;

    public class RequestCompra
    {
        public int Id { get; set; }
        public int? FornecedorId { get; set; }
        public DateTime? DataCompra { get; set; }
        public decimal? ValorCompra { get; set; }
        public string Status { get; set; }

        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}