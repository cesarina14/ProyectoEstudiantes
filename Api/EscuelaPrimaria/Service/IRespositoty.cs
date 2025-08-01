namespace EscuelaPrimaria.Service
{
    public interface IRespositoty<T> where T : class
    {
        void Update(T Entity);
        void Delete(T Entity);
        Task<T> Get(int Id);
        Task AddAsync (T Entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync();

    }
}
