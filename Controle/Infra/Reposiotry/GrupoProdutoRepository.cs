namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class GrupoProdutoRepository : IRepository<GrupoProduto>
    {
        private Contexto _context;

        public GrupoProdutoRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<GrupoProduto> Model { get { return _context.Set<GrupoProduto>(); } }

        public void Alterar(GrupoProduto model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(GrupoProduto model)
        {
            Model.Add(model);
        }

        public GrupoProduto Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<GrupoProduto> ObterPor(Expression<Func<GrupoProduto, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<GrupoProduto> ObterTodos()
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
