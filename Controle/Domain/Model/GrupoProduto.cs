namespace Domain.Model
{
    using Enum;
    using System.Collections.Generic;

    public class GrupoProduto
    {
        public GrupoProduto()
        {
            SubGrupoProduto = new List<SubGrupoProduto>();
            Status = EnumSituacao.Ativo.ToString();
        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Status { get; set; }

        public virtual ICollection<SubGrupoProduto> SubGrupoProduto { get; set; }
    }
}

