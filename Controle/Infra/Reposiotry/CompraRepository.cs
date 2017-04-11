namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class CompraRepository : IRepository<Compra>
    {
        private Contexto _context;

        public CompraRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<Compra> Model { get { return _context.Set<Compra>(); } }

        public void Alterar(Compra model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(Compra model)
        {
            Model.Add(model);
        }

        public Compra Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<Compra> ObterPor(Expression<Func<Compra, bool>> expression)
        {
            return Model.Include("ItensCompra.Produtos").Where(expression);
        }

        public IQueryable<Compra> ObterTodos()
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
