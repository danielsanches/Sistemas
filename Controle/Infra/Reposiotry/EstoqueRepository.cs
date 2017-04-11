namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class EstoqueRepository : IRepository<Estoque>
    {
        private Contexto _context;

        public EstoqueRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<Estoque> Model { get { return _context.Set<Estoque>(); } }

        public void Alterar(Estoque model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(Estoque model)
        {
            Model.Add(model);
        }

        public Estoque Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<Estoque> ObterPor(Expression<Func<Estoque, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<Estoque> ObterTodos()
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
