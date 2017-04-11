namespace Domain.Model
{
    using Enum;
    using System;
    using System.Collections.Generic;

    public class Compra
    {
        public Compra()
        {
            ItensCompra = new List<ItensCompra>();
            DataLancamento = DateTime.Now;
            Status = EnumSituacao.Ativo.ToString();
        }

        public int Id { get; set; }
        public int? FornecedorId { get; set; }
        public DateTime? DataCompra { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public decimal? ValorCompra { get; set; }
        public string Status { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual ICollection<ItensCompra> ItensCompra { get; set; }
    }
}
