namespace Application.Services.SubGrupoProduto
{
    public class RequestSubGrupoProduto
    {
        public string Descricao { get; set; }

        public string Status { get; set; }

        public int? GrupoProdutoId { get; set; }
        public int Id { get;  set; }
    }
}