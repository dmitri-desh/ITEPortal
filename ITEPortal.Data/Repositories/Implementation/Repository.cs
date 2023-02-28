using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext _context;
        private readonly DbSet<T> entities;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {

            }
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
