namespace Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        T Obter(object id);
        IQueryable<T> ObterTodos();
        IEnumerable<T> ObterPor(Expression<Func<T, bool>> expression);

        void Cadastrar(T model);
        void Alterar(T model);
        void Remover(object id);

        void SaveChanges();
    }
}
