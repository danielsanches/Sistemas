using System.Collections.Generic;

namespace Domain.Model
{
    public class Estoque
    {
        public Estoque()
        {
            Produtos = new List<Produtos>();
        }

        public long Quantidade { get; set; }
        public int ProdutoId { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }
    }
}
