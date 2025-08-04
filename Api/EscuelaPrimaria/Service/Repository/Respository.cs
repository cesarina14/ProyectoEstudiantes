
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimaria.Service.Repository
{
    public class Respository<T> : IRespository<T> where T : class
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
            
             _dbSet.Add(Entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(T Entity)
        {
            _dbSet.Remove(Entity);
             _context.SaveChanges();
        }

        public async Task<T> Get(long Id)
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
             _context.SaveChanges();
        }
        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
