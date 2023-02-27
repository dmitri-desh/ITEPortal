using ITEPortal.Data.Models;
using ITEPortal.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEPortal.Data.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        //protected readonly ApplicationContext context;
        private readonly DbSet<T> entities;

        //public Repository(ApplicationContext context)
        //{
        //    this.context = context;
        //    this.context.Database.EnsureCreated();
        //    entities = context.Set<T>();
        //}

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            //await context.Set<T>().AddAsync(entity);
            //await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            //context.Entry(entity).State = EntityState.Modified;
            //await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            //context.Set<T>().Remove(entity);
            //await context.SaveChangesAsync();
        }
    }
}
