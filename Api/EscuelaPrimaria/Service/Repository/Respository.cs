
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimaria.Service.Repository
{
    public class Respository<T> : IRespositoty<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Respository( DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T Entity)
        {
            await _dbSet.AddAsync(Entity);
        }

        public void Delete(T Entity)
        {
            _dbSet.Remove(Entity);
        }

        public async Task<T> Get(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
        }
        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
