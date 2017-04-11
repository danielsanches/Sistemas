namespace Domain.Model
{
    public class ItensCompra
    {
        public int Id { get; set; }
        public long Quantidade { get; set; }
        public decimal? ValorItem { get; set; }
        public decimal? ValorTotalItem { get { return Quantidade * ValorItem; } }
        public int ProdutoId { get; set; }
        public int CompraId { get; set; }

        public virtual Compra Compra { get; set; }
        public virtual Produtos Produtos { get; set; }
    }
}
