namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class ProdutosRepository : IRepository<Produtos>
    {
        private Contexto _context;

        public ProdutosRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<Produtos> Model { get { return _context.Set<Produtos>(); } }

        public void Alterar(Produtos model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(Produtos model)
        {
            Model.Add(model);
        }

        public Produtos Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<Produtos> ObterPor(Expression<Func<Produtos, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<Produtos> ObterTodos()
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
