using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly Shopbridge_Context dbcontext;

        public Repository(Shopbridge_Context _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            return dbcontext.Set<T>().AsQueryable();
        }

        public IQueryable<T> Get<T>(params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            IQueryable<T> dbQuery = dbcontext.Set<T>().AsQueryable();

            foreach (Expression<Func<T, object>> nProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(nProperty);
            }
            return dbQuery;
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            IQueryable<T> dbQuery = dbcontext.Set<T>().AsQueryable();
            if (where != null)
            {
                dbQuery = dbQuery.Where(where);
            }

            foreach (Expression<Func<T, object>> nProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(nProperty);
            }

            return dbQuery;
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            return dbcontext.Set<T>().AsEnumerable();
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            dbcontext.Entry(entity).State = EntityState.Added;
            await dbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync<T>(T entity) where T : class
        {
            dbcontext.Entry(entity).State = EntityState.Modified;
            await dbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync<T>(T entity) where T : class
        {
            dbcontext.Entry(entity).State = EntityState.Deleted;
            await dbcontext.SaveChangesAsync();
        }
        
    }
}
