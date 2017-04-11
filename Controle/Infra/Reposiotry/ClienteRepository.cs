namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;

    public class ClienteRepository : IRepository<Cliente>
    {
        private Contexto _context;

        public ClienteRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<Cliente> Model { get { return _context.Set<Cliente>(); } }

        public void Alterar(Cliente model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(Cliente model)
        {
            Model.Add(model);
        }

        public Cliente Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<Cliente> ObterPor(Expression<Func<Cliente, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<Cliente> ObterTodos()
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
