namespace Application.Services.Produto
{
    public class RequestProduto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UidadeArmazenamento { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorAtacado { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal PercentualVenda { get; set; }
        public decimal Quantidade { get; set; }
        public string Status { get; set; }
        public string CodigoBarra { get; set; }

        public int SubGrupoProdutoId { get; set; }
    }
}