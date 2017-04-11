namespace Domain.Model
{
    public class ItensVenda
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorItem { get; set; }
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }

        public virtual Produtos Produtos { get; set; }
        public virtual Vendas Vendas { get; set; }
    }
}
