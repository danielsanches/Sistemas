namespace Domain.Model
{
    using Enum;
    using System;
    using System.Collections.Generic;

    public class Fornecedor
    {
        public Fornecedor()
        {
            Compra = new List<Compra>();
            Status = EnumSituacao.Ativo.ToString();
            DataCadastro = DateTime.Now;
        }

        public int Id { get; set; }

        public string NomeFantasia { get; set; }

        public string Status { get; set; }

        public string FoneFixo { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual ICollection<Compra> Compra { get; set; }
    }
}
