namespace Infra.Reposiotry
{
    using Domain.Interfaces;
    using Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;

    public class UsuarioRepository : IRepository<Usuario>
    {
        private Contexto _contexto;
        public UsuarioRepository()
        {
            if (_contexto == null)
                _contexto = new Contexto();
        }

        public DbSet<Usuario> Model
        {
            get { return _contexto.Set<Usuario>(); }
        }

        public void Alterar(Usuario model)
        {
            _contexto.Entry(model).State = EntityState.Modified;
            _contexto.ChangeTracker.DetectChanges();
        }

        public void Cadastrar(Usuario model)
        {
            Model.Add(model);
        }

        public Usuario Obter(object id)
        {
            return Model.Find(id);
        }

        public IEnumerable<Usuario> ObterPor(Expression<Func<Usuario, bool>> expression)
        {
            return Model.Where(expression);
        }

        public IQueryable<Usuario> ObterTodos()
        {
            return Model;
        }

        public void Remover(object id)
        {
            Model.Remove(Model.Find(id));
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
