using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        bool IsExists(int id);
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetAsync(int id);
        Task CreateAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);
    }
}
