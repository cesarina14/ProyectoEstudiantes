namespace EscuelaPrimaria.Service
{
    public interface IRespository<T> where T : class
    {
        void Update(T Entity);
        void Delete(T Entity);
        Task<T> Get(long Id);
        Task AddAsync (T Entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync();

    }
}
