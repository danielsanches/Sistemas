namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class VendasRepository : IRepository<Vendas>
    {
        private Contexto _context;

        public VendasRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<Vendas> Model { get { return _context.Set<Vendas>(); } }

        public void Alterar(Vendas model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(Vendas model)
        {
            Model.Add(model);
        }

        public Vendas Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<Vendas> ObterPor(Expression<Func<Vendas, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<Vendas> ObterTodos()
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
