namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class SubGrupoProdutoRepository : IRepository<SubGrupoProduto>
    {
        private Contexto _context;

        public SubGrupoProdutoRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<SubGrupoProduto> Model { get { return _context.Set<SubGrupoProduto>(); } }

        public void Alterar(SubGrupoProduto model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(SubGrupoProduto model)
        {
            Model.Add(model);
        }

        public SubGrupoProduto Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<SubGrupoProduto> ObterPor(Expression<Func<SubGrupoProduto, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<SubGrupoProduto> ObterTodos()
        {
            return Model;
        }

        public void Remover(object id)
        {
            Model.Remove(Model.Find(id));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
