namespace Domain.Interfaces
{
    public interface IApplicationService<TResponse, TRequest> where TResponse : class where TRequest : class
    {
        TResponse Cadastrar(TRequest request);

        TResponse Alterar(TRequest request);

        TResponse Consultar(object id);

        TResponse ConsultarTodos(TRequest request);

        TResponse ObterLista(TRequest request);
    }
}
