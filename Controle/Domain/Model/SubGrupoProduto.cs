namespace Domain.Model
{
    using Enum;
    using System.Collections.Generic;

    public class SubGrupoProduto
    {
        public SubGrupoProduto()
        {
            Produtos = new List<Produtos>();
            Status = EnumSituacao.Ativo.ToString();
        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Status { get; set; }

        public int? GrupoProdutoId { get; set; }

        public virtual GrupoProduto GrupoProduto { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }
    }
}
