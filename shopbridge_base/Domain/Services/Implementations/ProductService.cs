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
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IRepository repository;
        private readonly ICategoryService categoryService;
        private readonly IUnitService unitService;
        public ProductService(IRepository _repository, ICategoryService _categoryService, IUnitService _unitService)
        {
            repository = _repository;
            categoryService = _categoryService;
            unitService = _unitService;
        }
        public bool IsExists(int id)
        {
            return repository.Get<Product>(p => p.ProductId == id).Any();
        }
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await repository.Get<Product>(p => p.Category, p => p.Unit).ToListAsync();
        }
        public async Task<Product> GetAsync(int id)
        {
            return await repository.Get<Product>(p => p.ProductId == id, p => p.Category, p => p.Unit).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Product entity)
        {
            var isExists = repository.Get<Product>(p => p.ProductName.ToLower() == entity.ProductName.ToLower()).Any();
            if(isExists)
            {
                throw new Exception(string.Format("Product with {0} name already exists.!!!", entity.ProductName));
            }

            if(!unitService.IsExists(entity.UnitId))
            {
                throw new Exception(string.Format("Unit with {0} id Not Found.!!!", entity.UnitId));
            }

            if (!categoryService.IsExists(entity.CategoryId))
            {
                throw new Exception(string.Format("Category with {0} id Not Found.!!!", entity.CategoryId));
            }

            entity.ProductId = 0;
            await repository.CreateAsync<Product>(entity);
        }
        public async Task UpdateAsync(Product entity)
        {
            var isExists = repository.Get<Product>(p => p.ProductId != entity.ProductId && p.ProductName.ToLower() == entity.ProductName.ToLower()).Any();
            if (isExists)
            {
                throw new Exception(string.Format("Product with {0} name already exists.!!!", entity.ProductName));
            }

            if (!unitService.IsExists(entity.UnitId))
            {
                throw new Exception(string.Format("Unit with {0} id Not Found.!!!", entity.UnitId));
            }

            if (!categoryService.IsExists(entity.CategoryId))
            {
                throw new Exception(string.Format("Category with {0} id Not Found.!!!", entity.CategoryId));
            }

            await repository.UpdateAsync<Product>(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = repository.Get<Product>( p => p.ProductId == id).FirstOrDefault();
            if(entity == null)
            {
                throw new Exception(string.Format("Product with {0} id Not Found.!!!", id));
            }
            await repository.DeleteAsync<Product>(entity);
        }
    }
}
