using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IRepository repository;

        public CategoryService(IRepository _repository)
        {
            repository = _repository;
        }
        public bool IsExists(int id)
        {
            return repository.Get<Category>(c => c.CategoryId == id).Any();
        }
        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await repository.AsQueryable<Category>().ToListAsync();
        }
        public async Task<Category> GetAsync(int id)
        {
            return await repository.Get<Category>(c => c.CategoryId == id).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Category entity)
        {
            var isExists = repository.Get<Category>(c => c.CategoryName.ToLower() == entity.CategoryName.ToLower()).Any();
            if (isExists)
            {
                throw new Exception(string.Format("Category with {0} name already exists.!!!", entity.CategoryName));
            }
            entity.CategoryId = 0;
            await repository.CreateAsync<Category>(entity);
        }
        public async Task UpdateAsync(Category entity)
        {
            var isExists = repository.Get<Category>(c => c.CategoryId != entity.CategoryId && c.CategoryName.ToLower() == entity.CategoryName.ToLower()).Any();
            if (isExists)
            {
                throw new Exception(string.Format("Category with {0} name already exists.!!!", entity.CategoryName));
            }
            await repository.UpdateAsync<Category>(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = repository.Get<Category>(c => c.CategoryId == id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception(string.Format("Category with {0} id Not Found.!!!", id));
            }

            var isUsed = repository.Get<Product>(c => c.CategoryId == id).Any();
            if (isUsed)
            {
                throw new Exception(string.Format("Category with {0} id is used in Product Master.!!!", id));
            }
            await repository.DeleteAsync<Category>(entity);
        }

    }
}
