namespace Infra.Reposiotry
{
    using Domain.Model;
    using Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class FornecedorRepository : IRepository<Fornecedor>
    {
        private Contexto _context;

        public FornecedorRepository()
        {
            if (_context == null)
                _context = new Contexto();
        }

        private DbSet<Fornecedor> Model { get { return _context.Set<Fornecedor>(); } }

        public void Alterar(Fornecedor model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(Fornecedor model)
        {
            Model.Add(model);
        }

        public Fornecedor Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<Fornecedor> ObterPor(Expression<Func<Fornecedor, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<Fornecedor> ObterTodos()
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
